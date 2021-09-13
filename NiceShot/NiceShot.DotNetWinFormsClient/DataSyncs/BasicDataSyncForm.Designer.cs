namespace NiceShot.DotNetWinFormsClient.DataSyncs
{
    partial class BasicDataSyncForm
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
            this.btnSyncXQABSD = new System.Windows.Forms.Button();
            this.btnSyncEMABSD = new System.Windows.Forms.Button();
            this.btnSyncXQHKBSD = new System.Windows.Forms.Button();
            this.btnSyncEMHKBSD = new System.Windows.Forms.Button();
            this.btnSyncTdxForeEarn = new System.Windows.Forms.Button();
            this.btnSyncSinaBonds = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSyncXQABSD
            // 
            this.btnSyncXQABSD.Location = new System.Drawing.Point(39, 36);
            this.btnSyncXQABSD.Name = "btnSyncXQABSD";
            this.btnSyncXQABSD.Size = new System.Drawing.Size(161, 42);
            this.btnSyncXQABSD.TabIndex = 0;
            this.btnSyncXQABSD.Text = "同步雪球A股基本数据";
            this.btnSyncXQABSD.UseVisualStyleBackColor = true;
            this.btnSyncXQABSD.Click += new System.EventHandler(this.btnSyncXQABSD_Click);
            // 
            // btnSyncEMABSD
            // 
            this.btnSyncEMABSD.Location = new System.Drawing.Point(39, 106);
            this.btnSyncEMABSD.Name = "btnSyncEMABSD";
            this.btnSyncEMABSD.Size = new System.Drawing.Size(161, 41);
            this.btnSyncEMABSD.TabIndex = 1;
            this.btnSyncEMABSD.Text = "同步东财A股基本数据";
            this.btnSyncEMABSD.UseVisualStyleBackColor = true;
            this.btnSyncEMABSD.Click += new System.EventHandler(this.btnSyncEMABSD_Click);
            // 
            // btnSyncXQHKBSD
            // 
            this.btnSyncXQHKBSD.Location = new System.Drawing.Point(244, 36);
            this.btnSyncXQHKBSD.Name = "btnSyncXQHKBSD";
            this.btnSyncXQHKBSD.Size = new System.Drawing.Size(155, 42);
            this.btnSyncXQHKBSD.TabIndex = 2;
            this.btnSyncXQHKBSD.Text = "同步雪球HK股基本数据";
            this.btnSyncXQHKBSD.UseVisualStyleBackColor = true;
            this.btnSyncXQHKBSD.Click += new System.EventHandler(this.btnSyncXQHKBSD_Click);
            // 
            // btnSyncEMHKBSD
            // 
            this.btnSyncEMHKBSD.Location = new System.Drawing.Point(244, 106);
            this.btnSyncEMHKBSD.Name = "btnSyncEMHKBSD";
            this.btnSyncEMHKBSD.Size = new System.Drawing.Size(155, 41);
            this.btnSyncEMHKBSD.TabIndex = 3;
            this.btnSyncEMHKBSD.Text = "同步东财HK股基本数据";
            this.btnSyncEMHKBSD.UseVisualStyleBackColor = true;
            this.btnSyncEMHKBSD.Click += new System.EventHandler(this.btnSyncEMHKBSD_Click);
            // 
            // btnSyncTdxForeEarn
            // 
            this.btnSyncTdxForeEarn.Location = new System.Drawing.Point(39, 179);
            this.btnSyncTdxForeEarn.Name = "btnSyncTdxForeEarn";
            this.btnSyncTdxForeEarn.Size = new System.Drawing.Size(161, 39);
            this.btnSyncTdxForeEarn.TabIndex = 4;
            this.btnSyncTdxForeEarn.Text = "同步通达信未来业绩预期";
            this.btnSyncTdxForeEarn.UseVisualStyleBackColor = true;
            this.btnSyncTdxForeEarn.Click += new System.EventHandler(this.btnSyncTdxForeEarn_Click);
            // 
            // btnSyncSinaBonds
            // 
            this.btnSyncSinaBonds.Location = new System.Drawing.Point(238, 179);
            this.btnSyncSinaBonds.Name = "btnSyncSinaBonds";
            this.btnSyncSinaBonds.Size = new System.Drawing.Size(161, 39);
            this.btnSyncSinaBonds.TabIndex = 5;
            this.btnSyncSinaBonds.Text = "同步SINA债券数据";
            this.btnSyncSinaBonds.UseVisualStyleBackColor = true;
            this.btnSyncSinaBonds.Click += new System.EventHandler(this.btnSyncSinaBonds_Click);
            // 
            // BasicDataSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 253);
            this.Controls.Add(this.btnSyncSinaBonds);
            this.Controls.Add(this.btnSyncTdxForeEarn);
            this.Controls.Add(this.btnSyncEMHKBSD);
            this.Controls.Add(this.btnSyncXQHKBSD);
            this.Controls.Add(this.btnSyncEMABSD);
            this.Controls.Add(this.btnSyncXQABSD);
            this.Name = "BasicDataSyncForm";
            this.Text = "BasicDataSyncForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSyncXQABSD;
        private System.Windows.Forms.Button btnSyncEMABSD;
        private System.Windows.Forms.Button btnSyncXQHKBSD;
        private System.Windows.Forms.Button btnSyncEMHKBSD;
        private System.Windows.Forms.Button btnSyncTdxForeEarn;
        private System.Windows.Forms.Button btnSyncSinaBonds;
    }
}