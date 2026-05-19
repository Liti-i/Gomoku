using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Gomoku : Form
    {
        private const int BOARD_SIZE = 15;
        private const int OFFSET = 30;
        private const int DOT_RADIUS = 4;
        
        // 棋盤狀態：0=空、1=黑棋、2=白棋
        private int[,] boardState = new int[BOARD_SIZE, BOARD_SIZE];
        
        // 遊戲狀態
        private bool isBlackTurn = true;  // true=黑棋回合，false=白棋回合
        private bool gameOver = false;
        private string gameResult = "";
        private List<(int row, int col)> moveHistory = new List<(int, int)>();
        private bool gameStarted = false;  // 遊戲是否已開始
        
        // 棋盤繪製參數
        private int cellSize = 0;
        private int gridSize = 0;

        // 新增欄位以追蹤悔棋狀態
        private bool undoRequested = false;  // 是否有人提出悔棋請求
        private int undoRequestPlayer = 0;   // 提出悔棋的玩家（1=黑棋，2=白棋）

        // 新增欄位以追蹤遊戲模式
        private bool isAIMode = false;  // 是否為 AI 模式
        private GomokuAI gomokuAI = null;  // AI 物件

        public Gomoku()
        {
            InitializeComponent();
        }

        private void ChessBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 計算格子大小
            int availableWidth = palBoard.Width - (OFFSET * 2);
            int availableHeight = palBoard.Height - (OFFSET * 2);
            gridSize = Math.Min(availableWidth, availableHeight);
            cellSize = gridSize / (BOARD_SIZE - 1);

            // 繪製棋盤網格
            using (Pen pen = new Pen(Color.Black, 2))
            {
                for (int i = 0; i < BOARD_SIZE; i++)
                {
                    g.DrawLine(pen, OFFSET, OFFSET + i * cellSize, OFFSET + (BOARD_SIZE - 1) * cellSize, OFFSET + i * cellSize);
                    g.DrawLine(pen, OFFSET + i * cellSize, OFFSET, OFFSET + i * cellSize, OFFSET + (BOARD_SIZE - 1) * cellSize);
                }
            }

            // 繪製星位
            int[] starPoints = { 3, 7, 11 };
            foreach (int r in starPoints)
            {
                foreach (int c in starPoints)
                {
                    g.FillEllipse(Brushes.Black, OFFSET + c * cellSize - DOT_RADIUS, OFFSET + r * cellSize - DOT_RADIUS, DOT_RADIUS * 2, DOT_RADIUS * 2);
                }
            }

            // 繪製棋子（使用 PNG 圖片）
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    if (boardState[row, col] != 0)
                    {
                        int x = OFFSET + col * cellSize;
                        int y = OFFSET + row * cellSize;
                        int pieceRadius = cellSize / 2 - 2;

                        // 使用 PNG 圖片
                        Image chessImage = boardState[row, col] == 1 ? Properties.Resources.BlackChess : Properties.Resources.WhiteChess;
                        
                        // 計算圖片的繪製位置（以棋盤交點為中心）
                        int imgWidth = pieceRadius * 2;
                        int imgHeight = pieceRadius * 2;
                        g.DrawImage(chessImage, x - pieceRadius, y - pieceRadius, imgWidth, imgHeight);
                    }
                }
            }

            // 繪製遊戲狀態文字
            DrawGameStatus(g);
        }

        private void DrawGameStatus(Graphics g)
        {
            string statusText;
            if (!gameStarted)
            {
                statusText = "請選擇遊戲模式";
            }
            else if (gameOver)
            {
                statusText = gameResult;
            }
            else
            {
                statusText = isBlackTurn ? "黑棋回合" : "白棋回合";
            }

            using (Font font = new Font("微軟正黑體", 14, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(statusText, font, brush, OFFSET + gridSize + 20, OFFSET + 20);
            }

            // 繪製移動歷史
            int historyY = OFFSET + 60;
            using (Font font = new Font("微軟正黑體", 10))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString($"總步數: {moveHistory.Count}", font, brush, OFFSET + gridSize + 20, historyY);
            }
        }

        private void Gomoku_Load(object sender, EventArgs e)
        {
            this.KeyDown += Gomoku_KeyDown;
            
            // 初始化時禁用棋盤點擊
            UpdatePictureBox();
        }

        private void UpdatePictureBox()
        {
            if (gameStarted)
            {
                // 顯示當前棋子的圖片
                if (isBlackTurn)
                {
                    picBWChess.Image = Properties.Resources.BlackChess;
                }
                else
                {
                    picBWChess.Image = Properties.Resources.WhiteChess;
                }
            }
            else
            {
                picBWChess.Image = null;
            }
        }

        private void PalBoard_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Click detected! gameStarted={gameStarted}, gameOver={gameOver}");
            
            if (!gameStarted || gameOver)
            {
                System.Diagnostics.Debug.WriteLine("Click rejected: game not started or game over");
                return;
            }

            // 計算點擊位置對應的棋盤座標
            int col = (e.X - OFFSET + cellSize / 2) / cellSize;
            int row = (e.Y - OFFSET + cellSize / 2) / cellSize;

            // 檢查座標是否合法
            if (row < 0 || row >= BOARD_SIZE || col < 0 || col >= BOARD_SIZE)
                return;

            // 檢查該位置是否已有棋子
            if (boardState[row, col] != 0)
                return;

            // AI 模式中，只允許玩家（黑棋）下棋
            if (isAIMode && !isBlackTurn)
                return;

            // 放置棋子
            int piece = isBlackTurn ? 1 : 2;
            boardState[row, col] = piece;
            moveHistory.Add((row, col));

            // 檢查是否獲勝
            if (CheckWin(row, col, piece))
            {
                gameOver = true;
                gameResult = isBlackTurn ? "黑棋獲勝！" : "白棋獲勝！";
            }

            // 切換回合
            isBlackTurn = !isBlackTurn;

            // 重新繪製
            UpdatePictureBox();
            palBoard.Invalidate();

            // 如果是 AI 模式且遊戲未結束，電腦自動下棋
            if (isAIMode && !gameOver && !isBlackTurn)
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;  // 延遲 1 秒，讓玩家看清楚
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    ComputerMove();
                };
                timer.Start();
            }
        }
        private void ComputerMove()
        {
            if (isAIMode && gomokuAI != null)
            {
                // 使用 AI 取得最佳移動
                var (bestRow, bestCol) = gomokuAI.GetBestMove();
                
                // 電腦放置棋子 (白棋 = 2)
                boardState[bestRow, bestCol] = 2;
                moveHistory.Add((bestRow, bestCol));

                // 檢查電腦是否獲勝
                if (CheckWin(bestRow, bestCol, 2))
                {
                    gameOver = true;
                    gameResult = "電腦(白棋)獲勝！";
                }
                else
                {
                    // 切換回玩家
                    isBlackTurn = true;
                }
            }
            else
            {
                // 原來的邏輯：從左上角開始找，找到第一個空位就下
                int bestRow = -1;
                int bestCol = -1;

                for (int r = 0; r < BOARD_SIZE; r++)
                {
                    for (int c = 0; c < BOARD_SIZE; c++)
                    {
                        if (boardState[r, c] == 0)
                        {
                            bestRow = r;
                            bestCol = c;
                            break;
                        }
                    }
                    if (bestRow != -1) break;
                }

                // 電腦放置棋子 (白棋 = 2)
                if (bestRow != -1 && bestCol != -1)
                {
                    boardState[bestRow, bestCol] = 2;
                    moveHistory.Add((bestRow, bestCol));

                    // 檢查電腦是否獲勝
                    if (CheckWin(bestRow, bestCol, 2))
                    {
                        gameOver = true;
                        gameResult = "電腦(白棋)獲勝！";
                    }

                    // 切換回玩家
                    isBlackTurn = true;
                }
            }

            // 重新繪製
            UpdatePictureBox();
            palBoard.Invalidate();
        }


        private void Gomoku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                ResetGame();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Z && e.Control)
            {
                UndoMove();
                e.Handled = true;
            }
        }

        private bool CheckWin(int row, int col, int piece)
        {
            // 檢查四個方向：橫、縱、斜（\）、斜（/）
            int[][] directions = new int[][]
            {
                new int[] { 0, 1 },   // 橫向
                new int[] { 1, 0 },   // 縱向
                new int[] { 1, 1 },   // 斜向 \
                new int[] { 1, -1 }   // 斜向 /
            };

            foreach (var dir in directions)
            {
                int count = 1;  // 包括當前棋子
                
                // 正向搜尋
                int r = row + dir[0];
                int c = col + dir[1];
                while (r >= 0 && r < BOARD_SIZE && c >= 0 && c < BOARD_SIZE && boardState[r, c] == piece)
                {
                    count++;
                    r += dir[0];
                    c += dir[1];
                }

                // 反向搜尋
                r = row - dir[0];
                c = col - dir[1];
                while (r >= 0 && r < BOARD_SIZE && c >= 0 && c < BOARD_SIZE && boardState[r, c] == piece)
                {
                    count++;
                    r -= dir[0];
                    c -= dir[1];
                }

                if (count >= 5)
                    return true;
            }

            return false;
        }

        private void ResetGame()
        {
            boardState = new int[BOARD_SIZE, BOARD_SIZE];
            isBlackTurn = true;
            gameOver = false;
            gameResult = "";
            moveHistory.Clear();
            gameStarted = false;
            isAIMode = false;  // 重置 AI 模式
            gomokuAI = null;   // 清除 AI 物件
            undoRequested = false;
            undoRequestPlayer = 0;
            UpdatePictureBox();
            palBoard.Invalidate();
        }

        private void UndoMove()
        {
            if (moveHistory.Count == 0)
                return;

            var lastMove = moveHistory[moveHistory.Count - 1];
            boardState[lastMove.row, lastMove.col] = 0;
            moveHistory.RemoveAt(moveHistory.Count - 1);
            
            isBlackTurn = moveHistory.Count % 2 == 0;
            gameOver = false;
            gameResult = "";
            
            UpdatePictureBox();
            palBoard.Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "確定要重置棋桌嗎？目前的遊戲進度將會遺失。",
                "確認重置",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ResetGame();
            }
        }

        private void btnTwoPlayer_Click(object sender, EventArgs e)
        {
            boardState = new int[BOARD_SIZE, BOARD_SIZE];
            isBlackTurn = true;
            gameOver = false;
            gameResult = "";
            moveHistory.Clear();
            gameStarted = true;  // 開始遊戲
            UpdatePictureBox();
            palBoard.Invalidate();
        }

        private void btnAIPlayer_Click(object sender, EventArgs e)
        {
            boardState = new int[BOARD_SIZE, BOARD_SIZE];
            isBlackTurn = true;
            gameOver = false;
            gameResult = "";
            moveHistory.Clear();
            gameStarted = true;  // 開始遊戲
            isAIMode = true;  // 設定為 AI 模式
            gomokuAI = new GomokuAI(boardState);  // 初始化 AI
            UpdatePictureBox();
            palBoard.Invalidate();
        }

        private void btnRegretChess_Click(object sender, EventArgs e)
        {
            if (!gameStarted || gameOver)
            {
                MessageBox.Show("遊戲未開始或已結束，無法悔棋", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (moveHistory.Count < 2)
            {
                MessageBox.Show("棋盤上棋子不足，無法悔棋", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 獲取當前玩家和上一手棋的玩家
            int currentPlayer = isBlackTurn ? 1 : 2;
            int lastMovePlayer = isBlackTurn ? 2 : 1;  // 上一手棋的玩家（對方）
            string lastMovePlayerName = isBlackTurn ? "白" : "黑";
            string currentPlayerName = isBlackTurn ? "黑" : "白";

            // 如果沒有悔棋請求，提出悔棋請求
            if (!undoRequested)
            {
                undoRequestPlayer = lastMovePlayer;  // 記錄要悔棋的玩家（上一手棋的人）
                undoRequested = true;
                
                // 當前玩家對悔棋請求做出回應
                DialogResult result = MessageBox.Show(
                    $"您同意{lastMovePlayerName}子的悔棋請求嗎？\n\n按「是」同意回到上一回合，按「否」拒絕",
                    "請確認悔棋請求",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 執行悔棋
                    PerformUndo();
                    undoRequested = false;
                    undoRequestPlayer = 0;
                }
                else
                {
                    // 拒絕悔棋
                    MessageBox.Show("悔棋請求被拒絕，遊戲繼續", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    undoRequested = false;
                    undoRequestPlayer = 0;
                }
            }
            else
            {
                MessageBox.Show("請等待對手的回應", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PerformUndo()
        {
            if (moveHistory.Count < 1)
                return;

            // 悔棋一步（回到上一步）
            var lastMove = moveHistory[moveHistory.Count - 1];
            boardState[lastMove.row, lastMove.col] = 0;
            moveHistory.RemoveAt(moveHistory.Count - 1);

            isBlackTurn = moveHistory.Count % 2 == 0;
            gameOver = false;
            gameResult = "";

            UpdatePictureBox();
            palBoard.Invalidate();
            MessageBox.Show("悔棋成功，回到上一步", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
