namespace TrackerServer
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbS1Players = new System.Windows.Forms.ListBox();
            this.lblS1 = new System.Windows.Forms.Label();
            this.lbS2Players = new System.Windows.Forms.ListBox();
            this.lblS2 = new System.Windows.Forms.Label();
            this.rtbS1Log = new System.Windows.Forms.RichTextBox();
            this.rtbS2Log = new System.Windows.Forms.RichTextBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbS1Players
            // 
            this.lbS1Players.FormattingEnabled = true;
            this.lbS1Players.Location = new System.Drawing.Point(12, 24);
            this.lbS1Players.Name = "lbS1Players";
            this.lbS1Players.Size = new System.Drawing.Size(170, 420);
            this.lbS1Players.TabIndex = 0;
            // 
            // lblS1
            // 
            this.lblS1.AutoSize = true;
            this.lblS1.Location = new System.Drawing.Point(12, 8);
            this.lblS1.Name = "lblS1";
            this.lblS1.Size = new System.Drawing.Size(84, 13);
            this.lblS1.TabIndex = 1;
            this.lblS1.Text = "Server 1 Players";
            // 
            // lbS2Players
            // 
            this.lbS2Players.FormattingEnabled = true;
            this.lbS2Players.Location = new System.Drawing.Point(185, 24);
            this.lbS2Players.Name = "lbS2Players";
            this.lbS2Players.Size = new System.Drawing.Size(170, 420);
            this.lbS2Players.TabIndex = 0;
            // 
            // lblS2
            // 
            this.lblS2.AutoSize = true;
            this.lblS2.Location = new System.Drawing.Point(185, 8);
            this.lblS2.Name = "lblS2";
            this.lblS2.Size = new System.Drawing.Size(84, 13);
            this.lblS2.TabIndex = 1;
            this.lblS2.Text = "Server 2 Players";
            // 
            // rtbS1Log
            // 
            this.rtbS1Log.Location = new System.Drawing.Point(358, 24);
            this.rtbS1Log.Name = "rtbS1Log";
            this.rtbS1Log.Size = new System.Drawing.Size(570, 205);
            this.rtbS1Log.TabIndex = 2;
            this.rtbS1Log.Text = "";
            // 
            // rtbS2Log
            // 
            this.rtbS2Log.Location = new System.Drawing.Point(358, 239);
            this.rtbS2Log.Name = "rtbS2Log";
            this.rtbS2Log.Size = new System.Drawing.Size(570, 205);
            this.rtbS2Log.TabIndex = 2;
            this.rtbS2Log.Text = "";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(358, 450);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(570, 205);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.frmMain_Load);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 666);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.rtbS2Log);
            this.Controls.Add(this.rtbS1Log);
            this.Controls.Add(this.lblS2);
            this.Controls.Add(this.lblS1);
            this.Controls.Add(this.lbS2Players);
            this.Controls.Add(this.lbS1Players);
            this.Name = "FrmMain";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbS1Players;
        private System.Windows.Forms.Label lblS1;
        private System.Windows.Forms.ListBox lbS2Players;
        private System.Windows.Forms.Label lblS2;
        private System.Windows.Forms.RichTextBox rtbS1Log;
        private System.Windows.Forms.RichTextBox rtbS2Log;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Timer timer;
    }
}