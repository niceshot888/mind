namespace NiceShot.DotNetWinFormsClient.DataSyncs
{
    partial class XQBasicDataSyncForm
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
            this.pnlChrome = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSyncBasicData = new System.Windows.Forms.Button();
            this.btnSyncBalData = new System.Windows.Forms.Button();
            this.tbxCookie = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pnlChrome
            // 
            this.pnlChrome.Location = new System.Drawing.Point(83, 271);
            this.pnlChrome.Name = "pnlChrome";
            this.pnlChrome.Size = new System.Drawing.Size(562, 100);
            this.pnlChrome.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cookie：";
            // 
            // btnSyncBasicData
            // 
            this.btnSyncBasicData.Location = new System.Drawing.Point(176, 225);
            this.btnSyncBasicData.Name = "btnSyncBasicData";
            this.btnSyncBasicData.Size = new System.Drawing.Size(141, 23);
            this.btnSyncBasicData.TabIndex = 4;
            this.btnSyncBasicData.Text = "同步雪球基本数据";
            this.btnSyncBasicData.UseVisualStyleBackColor = true;
            this.btnSyncBasicData.Click += new System.EventHandler(this.btnSyncBasicData_Click);
            // 
            // btnSyncBalData
            // 
            this.btnSyncBalData.Location = new System.Drawing.Point(381, 225);
            this.btnSyncBalData.Name = "btnSyncBalData";
            this.btnSyncBalData.Size = new System.Drawing.Size(117, 23);
            this.btnSyncBalData.TabIndex = 5;
            this.btnSyncBalData.Text = "同步资产负债表";
            this.btnSyncBalData.UseVisualStyleBackColor = true;
            this.btnSyncBalData.Click += new System.EventHandler(this.btnSyncBalData_Click);
            // 
            // tbxCookie
            // 
            this.tbxCookie.Location = new System.Drawing.Point(83, 22);
            this.tbxCookie.Multiline = true;
            this.tbxCookie.Name = "tbxCookie";
            this.tbxCookie.Size = new System.Drawing.Size(562, 181);
            this.tbxCookie.TabIndex = 1;
            // 
            // XQBasicDataSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 408);
            this.Controls.Add(this.btnSyncBalData);
            this.Controls.Add(this.btnSyncBasicData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlChrome);
            this.Controls.Add(this.tbxCookie);
            this.Name = "XQBasicDataSyncForm";
            this.Text = "XQBasicDataSyncForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlChrome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSyncBasicData;
        private System.Windows.Forms.Button btnSyncBalData;
        private System.Windows.Forms.TextBox tbxCookie;
    }
}