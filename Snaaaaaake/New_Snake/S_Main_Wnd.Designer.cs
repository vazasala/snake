namespace New_Snake
{
    partial class S_Main_Wnd
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.P_Game_board = new System.Windows.Forms.Panel();
            this.btn_Start_Game = new System.Windows.Forms.Button();
            this.lbl_Score_info = new System.Windows.Forms.Label();
            this.chkb_Check_speed_mode = new System.Windows.Forms.CheckBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // P_Game_board
            // 
            this.P_Game_board.BackColor = System.Drawing.Color.White;
            this.P_Game_board.Location = new System.Drawing.Point(67, 38);
            this.P_Game_board.Name = "P_Game_board";
            this.P_Game_board.Size = new System.Drawing.Size(410, 520);
            this.P_Game_board.TabIndex = 0;
            // 
            // btn_Start_Game
            // 
            this.btn_Start_Game.Location = new System.Drawing.Point(532, 38);
            this.btn_Start_Game.Name = "btn_Start_Game";
            this.btn_Start_Game.Size = new System.Drawing.Size(75, 23);
            this.btn_Start_Game.TabIndex = 1;
            this.btn_Start_Game.Text = "Start";
            this.btn_Start_Game.UseVisualStyleBackColor = true;
            this.btn_Start_Game.Click += new System.EventHandler(this.btn_Start_Game_Click);
            // 
            // lbl_Score_info
            // 
            this.lbl_Score_info.AutoSize = true;
            this.lbl_Score_info.Location = new System.Drawing.Point(541, 210);
            this.lbl_Score_info.Name = "lbl_Score_info";
            this.lbl_Score_info.Size = new System.Drawing.Size(0, 13);
            this.lbl_Score_info.TabIndex = 2;
            // 
            // chkb_Check_speed_mode
            // 
            this.chkb_Check_speed_mode.AutoSize = true;
            this.chkb_Check_speed_mode.Location = new System.Drawing.Point(532, 169);
            this.chkb_Check_speed_mode.Name = "chkb_Check_speed_mode";
            this.chkb_Check_speed_mode.Size = new System.Drawing.Size(86, 17);
            this.chkb_Check_speed_mode.TabIndex = 3;
            this.chkb_Check_speed_mode.Text = "Speed mode";
            this.chkb_Check_speed_mode.UseVisualStyleBackColor = true;
            this.chkb_Check_speed_mode.CheckedChanged += new System.EventHandler(this.chkb_Check_speed_mode_CheckedChanged);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(532, 96);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // S_Main_Wnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(654, 528);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.chkb_Check_speed_mode);
            this.Controls.Add(this.lbl_Score_info);
            this.Controls.Add(this.btn_Start_Game);
            this.Controls.Add(this.P_Game_board);
            this.KeyPreview = true;
            this.Name = "S_Main_Wnd";
            this.Text = "Snake 3.0";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.S_Main_Wnd_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start_Game;
        public System.Windows.Forms.Panel P_Game_board;
        public System.Windows.Forms.Label lbl_Score_info;
        public System.Windows.Forms.CheckBox chkb_Check_speed_mode;
        private System.Windows.Forms.Button btn_stop;
    }
}

