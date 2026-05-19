namespace Gomoku
{
    partial class Gomoku
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.palBoard = new System.Windows.Forms.Panel();
            this.grpGomoku = new System.Windows.Forms.GroupBox();
            this.grpReset = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpOperation = new System.Windows.Forms.GroupBox();
            this.btnRegretChess = new System.Windows.Forms.Button();
            this.picBWChess = new System.Windows.Forms.PictureBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.btnTwoPlayer = new System.Windows.Forms.Button();
            this.btnAIPlayer = new System.Windows.Forms.Button();
            this.grpGomoku.SuspendLayout();
            this.grpReset.SuspendLayout();
            this.grpOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBWChess)).BeginInit();
            this.grpMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // palBoard
            // 
            this.palBoard.BackColor = System.Drawing.Color.BurlyWood;
            this.palBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.palBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.palBoard.Location = new System.Drawing.Point(0, 0);
            this.palBoard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.palBoard.Name = "palBoard";
            this.palBoard.Size = new System.Drawing.Size(826, 680);
            this.palBoard.TabIndex = 0;
            this.palBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoard_Paint);
            this.palBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PalBoard_MouseClick);
            // 
            // grpGomoku
            // 
            this.grpGomoku.BackColor = System.Drawing.Color.LightGray;
            this.grpGomoku.Controls.Add(this.grpReset);
            this.grpGomoku.Controls.Add(this.grpOperation);
            this.grpGomoku.Controls.Add(this.picBWChess);
            this.grpGomoku.Controls.Add(this.lblCondition);
            this.grpGomoku.Controls.Add(this.grpMode);
            this.grpGomoku.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpGomoku.Location = new System.Drawing.Point(848, 0);
            this.grpGomoku.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGomoku.Name = "grpGomoku";
            this.grpGomoku.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGomoku.Size = new System.Drawing.Size(427, 680);
            this.grpGomoku.TabIndex = 1;
            this.grpGomoku.TabStop = false;
            this.grpGomoku.Text = "【 狀態區 】";
            // 
            // grpReset
            // 
            this.grpReset.BackColor = System.Drawing.Color.LightGray;
            this.grpReset.Controls.Add(this.btnReset);
            this.grpReset.ForeColor = System.Drawing.Color.Firebrick;
            this.grpReset.Location = new System.Drawing.Point(12, 491);
            this.grpReset.Name = "grpReset";
            this.grpReset.Size = new System.Drawing.Size(396, 158);
            this.grpReset.TabIndex = 8;
            this.grpReset.TabStop = false;
            this.grpReset.Text = "【 危險操作區 】";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.LightGray;
            this.btnReset.ForeColor = System.Drawing.Color.Firebrick;
            this.btnReset.Location = new System.Drawing.Point(12, 43);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(171, 106);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "重置棋桌";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // grpOperation
            // 
            this.grpOperation.Controls.Add(this.btnRegretChess);
            this.grpOperation.Location = new System.Drawing.Point(0, 335);
            this.grpOperation.Name = "grpOperation";
            this.grpOperation.Size = new System.Drawing.Size(408, 150);
            this.grpOperation.TabIndex = 5;
            this.grpOperation.TabStop = false;
            this.grpOperation.Text = "【 操作 】";
            // 
            // btnRegretChess
            // 
            this.btnRegretChess.BackColor = System.Drawing.Color.Silver;
            this.btnRegretChess.ForeColor = System.Drawing.Color.Black;
            this.btnRegretChess.Location = new System.Drawing.Point(24, 33);
            this.btnRegretChess.Name = "btnRegretChess";
            this.btnRegretChess.Size = new System.Drawing.Size(171, 98);
            this.btnRegretChess.TabIndex = 3;
            this.btnRegretChess.Text = "悔棋";
            this.btnRegretChess.UseVisualStyleBackColor = false;
            this.btnRegretChess.Click += new System.EventHandler(this.btnRegretChess_Click);
            // 
            // picBWChess
            // 
            this.picBWChess.Location = new System.Drawing.Point(152, 19);
            this.picBWChess.Name = "picBWChess";
            this.picBWChess.Size = new System.Drawing.Size(130, 130);
            this.picBWChess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBWChess.TabIndex = 7;
            this.picBWChess.TabStop = false;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(7, 65);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(126, 51);
            this.lblCondition.TabIndex = 6;
            this.lblCondition.Text = "現在輪到:";
            this.lblCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.btnTwoPlayer);
            this.grpMode.Controls.Add(this.btnAIPlayer);
            this.grpMode.Location = new System.Drawing.Point(0, 155);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(434, 174);
            this.grpMode.TabIndex = 4;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "【 模式選擇 】";
            // 
            // btnTwoPlayer
            // 
            this.btnTwoPlayer.BackColor = System.Drawing.Color.Silver;
            this.btnTwoPlayer.ForeColor = System.Drawing.Color.Black;
            this.btnTwoPlayer.Location = new System.Drawing.Point(24, 42);
            this.btnTwoPlayer.Name = "btnTwoPlayer";
            this.btnTwoPlayer.Size = new System.Drawing.Size(171, 98);
            this.btnTwoPlayer.TabIndex = 0;
            this.btnTwoPlayer.Text = "雙人對打";
            this.btnTwoPlayer.UseVisualStyleBackColor = false;
            this.btnTwoPlayer.Click += new System.EventHandler(this.btnTwoPlayer_Click);
            // 
            // btnAIPlayer
            // 
            this.btnAIPlayer.BackColor = System.Drawing.Color.Silver;
            this.btnAIPlayer.ForeColor = System.Drawing.Color.Black;
            this.btnAIPlayer.Location = new System.Drawing.Point(228, 41);
            this.btnAIPlayer.Name = "btnAIPlayer";
            this.btnAIPlayer.Size = new System.Drawing.Size(171, 98);
            this.btnAIPlayer.TabIndex = 1;
            this.btnAIPlayer.Text = "挑戰電腦";
            this.btnAIPlayer.UseVisualStyleBackColor = false;
            this.btnAIPlayer.Click += new System.EventHandler(this.btnAIPlayer_Click);
            // 
            // Gomoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(1275, 680);
            this.Controls.Add(this.palBoard);
            this.Controls.Add(this.grpGomoku);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Gomoku";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gomoku";
            this.grpGomoku.ResumeLayout(false);
            this.grpReset.ResumeLayout(false);
            this.grpOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBWChess)).EndInit();
            this.grpMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palBoard;
        private System.Windows.Forms.GroupBox grpGomoku;
        private System.Windows.Forms.Button btnTwoPlayer;
        private System.Windows.Forms.Button btnAIPlayer;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRegretChess;
        private System.Windows.Forms.GroupBox grpOperation;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.PictureBox picBWChess;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.GroupBox grpReset;
    }
}

