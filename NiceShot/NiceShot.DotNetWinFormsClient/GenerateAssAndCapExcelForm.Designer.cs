namespace NiceShot.DotNetWinFormsClient
{
    partial class GenerateAssAndCapExcelForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxReportType = new System.Windows.Forms.ComboBox();
            this.cbx_othercurrassetype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_othercurreliabitype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_expinoncurrassettype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGengerate = new System.Windows.Forms.Button();
            this.link_excel = new System.Windows.Forms.LinkLabel();
            this.cbxIsLatest5years = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "报表类型：";
            // 
            // cbxReportType
            // 
            this.cbxReportType.FormattingEnabled = true;
            this.cbxReportType.Location = new System.Drawing.Point(222, 23);
            this.cbxReportType.Name = "cbxReportType";
            this.cbxReportType.Size = new System.Drawing.Size(121, 20);
            this.cbxReportType.TabIndex = 1;
            // 
            // cbx_othercurrassetype
            // 
            this.cbx_othercurrassetype.FormattingEnabled = true;
            this.cbx_othercurrassetype.Location = new System.Drawing.Point(222, 62);
            this.cbx_othercurrassetype.Name = "cbx_othercurrassetype";
            this.cbx_othercurrassetype.Size = new System.Drawing.Size(121, 20);
            this.cbx_othercurrassetype.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "其他流动资产类型：";
            // 
            // cbx_othercurreliabitype
            // 
            this.cbx_othercurreliabitype.FormattingEnabled = true;
            this.cbx_othercurreliabitype.Location = new System.Drawing.Point(222, 98);
            this.cbx_othercurreliabitype.Name = "cbx_othercurreliabitype";
            this.cbx_othercurreliabitype.Size = new System.Drawing.Size(121, 20);
            this.cbx_othercurreliabitype.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "其他流动负债类型：";
            // 
            // cbx_expinoncurrassettype
            // 
            this.cbx_expinoncurrassettype.FormattingEnabled = true;
            this.cbx_expinoncurrassettype.Location = new System.Drawing.Point(222, 134);
            this.cbx_expinoncurrassettype.Name = "cbx_expinoncurrassettype";
            this.cbx_expinoncurrassettype.Size = new System.Drawing.Size(121, 20);
            this.cbx_expinoncurrassettype.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "一年内到期的非流动资产类型：";
            // 
            // btnGengerate
            // 
            this.btnGengerate.Location = new System.Drawing.Point(152, 203);
            this.btnGengerate.Name = "btnGengerate";
            this.btnGengerate.Size = new System.Drawing.Size(138, 23);
            this.btnGengerate.TabIndex = 8;
            this.btnGengerate.Text = "生成EXCEL文件";
            this.btnGengerate.UseVisualStyleBackColor = true;
            this.btnGengerate.Click += new System.EventHandler(this.btnGengerate_Click);
            // 
            // link_excel
            // 
            this.link_excel.AutoSize = true;
            this.link_excel.Location = new System.Drawing.Point(312, 208);
            this.link_excel.Name = "link_excel";
            this.link_excel.Size = new System.Drawing.Size(83, 12);
            this.link_excel.TabIndex = 9;
            this.link_excel.TabStop = true;
            this.link_excel.Text = "打开excel文件";
            this.link_excel.Visible = false;
            this.link_excel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_excel_LinkClicked);
            // 
            // cbxIsLatest5years
            // 
            this.cbxIsLatest5years.AutoSize = true;
            this.cbxIsLatest5years.Checked = true;
            this.cbxIsLatest5years.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsLatest5years.Location = new System.Drawing.Point(222, 171);
            this.cbxIsLatest5years.Name = "cbxIsLatest5years";
            this.cbxIsLatest5years.Size = new System.Drawing.Size(15, 14);
            this.cbxIsLatest5years.TabIndex = 10;
            this.cbxIsLatest5years.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "最近5年报表：";
            // 
            // GenerateAssAndCapExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 253);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxIsLatest5years);
            this.Controls.Add(this.link_excel);
            this.Controls.Add(this.btnGengerate);
            this.Controls.Add(this.cbx_expinoncurrassettype);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_othercurreliabitype);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_othercurrassetype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxReportType);
            this.Controls.Add(this.label1);
            this.Name = "GenerateAssAndCapExcelForm";
            this.Text = "生成资产资本表数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxReportType;
        private System.Windows.Forms.ComboBox cbx_othercurrassetype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_othercurreliabitype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_expinoncurrassettype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGengerate;
        private System.Windows.Forms.LinkLabel link_excel;
        private System.Windows.Forms.CheckBox cbxIsLatest5years;
        private System.Windows.Forms.Label label5;
    }
}