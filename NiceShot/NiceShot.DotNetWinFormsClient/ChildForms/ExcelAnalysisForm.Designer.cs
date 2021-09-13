
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class ExcelAnalysisForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbxIsLatest5years = new System.Windows.Forms.CheckBox();
            this.link_excel = new System.Windows.Forms.LinkLabel();
            this.btnGengerate = new System.Windows.Forms.Button();
            this.cbx_expinoncurrassettype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_othercurreliabitype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_othercurrassetype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxReportType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "最近5年报表：";
            // 
            // cbxIsLatest5years
            // 
            this.cbxIsLatest5years.AutoSize = true;
            this.cbxIsLatest5years.Checked = true;
            this.cbxIsLatest5years.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsLatest5years.Location = new System.Drawing.Point(188, 169);
            this.cbxIsLatest5years.Name = "cbxIsLatest5years";
            this.cbxIsLatest5years.Size = new System.Drawing.Size(15, 14);
            this.cbxIsLatest5years.TabIndex = 22;
            this.cbxIsLatest5years.UseVisualStyleBackColor = true;
            // 
            // link_excel
            // 
            this.link_excel.AutoSize = true;
            this.link_excel.Location = new System.Drawing.Point(278, 206);
            this.link_excel.Name = "link_excel";
            this.link_excel.Size = new System.Drawing.Size(83, 12);
            this.link_excel.TabIndex = 21;
            this.link_excel.TabStop = true;
            this.link_excel.Text = "打开excel文件";
            this.link_excel.Visible = false;
            this.link_excel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_excel_LinkClicked);
            // 
            // btnGengerate
            // 
            this.btnGengerate.Location = new System.Drawing.Point(118, 201);
            this.btnGengerate.Name = "btnGengerate";
            this.btnGengerate.Size = new System.Drawing.Size(138, 23);
            this.btnGengerate.TabIndex = 20;
            this.btnGengerate.Text = "生成EXCEL文件";
            this.btnGengerate.UseVisualStyleBackColor = true;
            this.btnGengerate.Click += new System.EventHandler(this.btnGengerate_Click);
            // 
            // cbx_expinoncurrassettype
            // 
            this.cbx_expinoncurrassettype.FormattingEnabled = true;
            this.cbx_expinoncurrassettype.Location = new System.Drawing.Point(188, 132);
            this.cbx_expinoncurrassettype.Name = "cbx_expinoncurrassettype";
            this.cbx_expinoncurrassettype.Size = new System.Drawing.Size(121, 20);
            this.cbx_expinoncurrassettype.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "一年内到期的非流动资产类型：";
            // 
            // cbx_othercurreliabitype
            // 
            this.cbx_othercurreliabitype.FormattingEnabled = true;
            this.cbx_othercurreliabitype.Location = new System.Drawing.Point(188, 96);
            this.cbx_othercurreliabitype.Name = "cbx_othercurreliabitype";
            this.cbx_othercurreliabitype.Size = new System.Drawing.Size(121, 20);
            this.cbx_othercurreliabitype.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "其他流动负债类型：";
            // 
            // cbx_othercurrassetype
            // 
            this.cbx_othercurrassetype.FormattingEnabled = true;
            this.cbx_othercurrassetype.Location = new System.Drawing.Point(188, 60);
            this.cbx_othercurrassetype.Name = "cbx_othercurrassetype";
            this.cbx_othercurrassetype.Size = new System.Drawing.Size(121, 20);
            this.cbx_othercurrassetype.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "其他流动资产类型：";
            // 
            // cbxReportType
            // 
            this.cbxReportType.FormattingEnabled = true;
            this.cbxReportType.Location = new System.Drawing.Point(188, 21);
            this.cbxReportType.Name = "cbxReportType";
            this.cbxReportType.Size = new System.Drawing.Size(121, 20);
            this.cbxReportType.TabIndex = 13;
            this.cbxReportType.SelectedIndexChanged += new System.EventHandler(this.cbxReportType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "报表类型：";
            // 
            // ExcelAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 250);
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
            this.Name = "ExcelAnalysisForm";
            this.Text = "ExcelAnalysisForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbxIsLatest5years;
        private System.Windows.Forms.LinkLabel link_excel;
        private System.Windows.Forms.Button btnGengerate;
        private System.Windows.Forms.ComboBox cbx_expinoncurrassettype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_othercurreliabitype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_othercurrassetype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxReportType;
        private System.Windows.Forms.Label label1;
    }
}