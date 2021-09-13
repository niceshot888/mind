
namespace NiceShot.SpeechQuartz
{
    partial class MainForm
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
            this.cbxSexy = new System.Windows.Forms.ComboBox();
            this.Resume = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.cbxlang = new System.Windows.Forms.ComboBox();
            this.Start = new System.Windows.Forms.Button();
            this.tbxContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbxSexy
            // 
            this.cbxSexy.FormattingEnabled = true;
            this.cbxSexy.Items.AddRange(new object[] {
            "女",
            "男",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxSexy.Location = new System.Drawing.Point(484, 551);
            this.cbxSexy.Name = "cbxSexy";
            this.cbxSexy.Size = new System.Drawing.Size(174, 20);
            this.cbxSexy.TabIndex = 13;
            this.cbxSexy.Text = "女";
            // 
            // Resume
            // 
            this.Resume.Location = new System.Drawing.Point(522, 589);
            this.Resume.Name = "Resume";
            this.Resume.Size = new System.Drawing.Size(75, 41);
            this.Resume.TabIndex = 12;
            this.Resume.Text = "恢复";
            this.Resume.UseVisualStyleBackColor = true;
            this.Resume.Click += new System.EventHandler(this.Resume_Click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(441, 589);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 41);
            this.Pause.TabIndex = 11;
            this.Pause.Text = "暂停";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(603, 589);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 41);
            this.Stop.TabIndex = 10;
            this.Stop.Text = "停止";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // cbxlang
            // 
            this.cbxlang.FormattingEnabled = true;
            this.cbxlang.Items.AddRange(new object[] {
            "中文",
            "english"});
            this.cbxlang.Location = new System.Drawing.Point(382, 551);
            this.cbxlang.Name = "cbxlang";
            this.cbxlang.Size = new System.Drawing.Size(79, 20);
            this.cbxlang.TabIndex = 9;
            this.cbxlang.Text = "中文";
            this.cbxlang.SelectedIndexChanged += new System.EventHandler(this.cbxlang_SelectedIndexChanged);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(360, 589);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 41);
            this.Start.TabIndex = 8;
            this.Start.Text = "朗读";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // tbxContent
            // 
            this.tbxContent.Location = new System.Drawing.Point(12, 12);
            this.tbxContent.Multiline = true;
            this.tbxContent.Name = "tbxContent";
            this.tbxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxContent.Size = new System.Drawing.Size(998, 514);
            this.tbxContent.TabIndex = 7;
            this.tbxContent.Text = "最近使用C#重做了点名系统（要用到TTS，让计算机点名）使用了SAPI，在这里总结一下SpVoice的使用方法";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 646);
            this.Controls.Add(this.cbxSexy);
            this.Controls.Add(this.Resume);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.cbxlang);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.tbxContent);
            this.Name = "MainForm";
            this.Text = "语音自动播报";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxSexy;
        private System.Windows.Forms.Button Resume;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.ComboBox cbxlang;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox tbxContent;
    }
}