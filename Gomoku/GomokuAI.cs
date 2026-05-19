using System;

namespace Gomoku
{
    // 簡單的 GomokuAI 範例，僅隨機選擇一個可下的位置
    public class GomokuAI
    {
        private int[,] board;
        private static Random rand = new Random();

        public GomokuAI(int[,] boardState)
        {
            // 建立 board 的參考
            board = boardState;
        }

        public (int row, int col) GetBestMove()
        {
            int size = board.GetLength(0);
            // 隨機尋找一個空格
            while (true)
            {
                int row = rand.Next(size);
                int col = rand.Next(size);
                if (board[row, col] == 0)
                    return (row, col);
            }
        }
    }
}

