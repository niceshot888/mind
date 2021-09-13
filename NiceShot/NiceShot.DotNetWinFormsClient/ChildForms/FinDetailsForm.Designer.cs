
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class FinDetailsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbl_stock_details = new System.Windows.Forms.DataGridView();
            this.cms_stockdetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_addhd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_view_report = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_mainbiz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_wdm = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cbxReportType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tnlpnlmain = new System.Windows.Forms.TableLayoutPanel();
            this.tsmi_gdqk = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lsgz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ltgd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_srgc = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_stock_details)).BeginInit();
            this.cms_stockdetails.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tnlpnlmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_stock_details
            // 
            this.tbl_stock_details.AllowUserToAddRows = false;
            this.tbl_stock_details.AllowUserToDeleteRows = false;
            this.tbl_stock_details.AllowUserToResizeColumns = false;
            this.tbl_stock_details.AllowUserToResizeRows = false;
            this.tbl_stock_details.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)), true);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_stock_details.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.tbl_stock_details.ColumnHeadersHeight = 40;
            this.tbl_stock_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tbl_stock_details.DefaultCellStyle = dataGridViewCellStyle6;
            this.tbl_stock_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_stock_details.GridColor = System.Drawing.Color.LightSteelBlue;
            this.tbl_stock_details.Location = new System.Drawing.Point(3, 32);
            this.tbl_stock_details.Name = "tbl_stock_details";
            this.tbl_stock_details.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_stock_details.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.tbl_stock_details.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tbl_stock_details.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.tbl_stock_details.RowTemplate.Height = 23;
            this.tbl_stock_details.Size = new System.Drawing.Size(794, 357);
            this.tbl_stock_details.TabIndex = 1;
            this.tbl_stock_details.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tbl_stock_details_CellMouseDown);
            this.tbl_stock_details.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.tbl_stock_details_RowPostPaint);
            // 
            // cms_stockdetails
            // 
            this.cms_stockdetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_addhd,
            this.tsmi_view_report,
            this.tsmi_mainbiz,
            this.tsmi_wdm,
            this.tsmi_lsgz,
            this.tsmi_gdqk,
            this.tsmi_ltgd,
            this.tsmi_srgc});
            this.cms_stockdetails.Name = "cms_stockdetails";
            this.cms_stockdetails.Size = new System.Drawing.Size(181, 202);
            // 
            // tsmi_addhd
            // 
            this.tsmi_addhd.Name = "tsmi_addhd";
            this.tsmi_addhd.Size = new System.Drawing.Size(180, 22);
            this.tsmi_addhd.Text = "补充研发投入数据";
            this.tsmi_addhd.Click += new System.EventHandler(this.tsmi_addhd_Click);
            // 
            // tsmi_view_report
            // 
            this.tsmi_view_report.Name = "tsmi_view_report";
            this.tsmi_view_report.Size = new System.Drawing.Size(180, 22);
            this.tsmi_view_report.Text = "查询财务报表";
            this.tsmi_view_report.Click += new System.EventHandler(this.tsmi_view_report_Click);
            // 
            // tsmi_mainbiz
            // 
            this.tsmi_mainbiz.Name = "tsmi_mainbiz";
            this.tsmi_mainbiz.Size = new System.Drawing.Size(180, 22);
            this.tsmi_mainbiz.Text = "主营构成";
            this.tsmi_mainbiz.Click += new System.EventHandler(this.tsmi_mainbiz_Click);
            // 
            // tsmi_wdm
            // 
            this.tsmi_wdm.Name = "tsmi_wdm";
            this.tsmi_wdm.Size = new System.Drawing.Size(180, 22);
            this.tsmi_wdm.Text = "问董秘";
            this.tsmi_wdm.Click += new System.EventHandler(this.tsmi_wdm_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cbxReportType);
            this.pnlTop.Controls.Add(this.label5);
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(794, 23);
            this.pnlTop.TabIndex = 2;
            // 
            // cbxReportType
            // 
            this.cbxReportType.FormattingEnabled = true;
            this.cbxReportType.Location = new System.Drawing.Point(79, 3);
            this.cbxReportType.Name = "cbxReportType";
            this.cbxReportType.Size = new System.Drawing.Size(91, 20);
            this.cbxReportType.TabIndex = 46;
            this.cbxReportType.SelectedIndexChanged += new System.EventHandler(this.cbxReportType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "报表类型：";
            // 
            // tnlpnlmain
            // 
            this.tnlpnlmain.ColumnCount = 1;
            this.tnlpnlmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tnlpnlmain.Controls.Add(this.pnlTop, 0, 0);
            this.tnlpnlmain.Controls.Add(this.tbl_stock_details, 0, 1);
            this.tnlpnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tnlpnlmain.Location = new System.Drawing.Point(0, 0);
            this.tnlpnlmain.Name = "tnlpnlmain";
            this.tnlpnlmain.RowCount = 2;
            this.tnlpnlmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tnlpnlmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tnlpnlmain.Size = new System.Drawing.Size(800, 392);
            this.tnlpnlmain.TabIndex = 3;
            // 
            // tsmi_gdqk
            // 
            this.tsmi_gdqk.Name = "tsmi_gdqk";
            this.tsmi_gdqk.Size = new System.Drawing.Size(180, 22);
            this.tsmi_gdqk.Text = "股东情况";
            this.tsmi_gdqk.Click += new System.EventHandler(this.tsmi_gdqk_Click);
            // 
            // tsmi_lsgz
            // 
            this.tsmi_lsgz.Name = "tsmi_lsgz";
            this.tsmi_lsgz.Size = new System.Drawing.Size(180, 22);
            this.tsmi_lsgz.Text = "历史估值";
            this.tsmi_lsgz.Click += new System.EventHandler(this.tsmi_lsgz_Click);
            // 
            // tsmi_ltgd
            // 
            this.tsmi_ltgd.Name = "tsmi_ltgd";
            this.tsmi_ltgd.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ltgd.Text = "流通股东";
            this.tsmi_ltgd.Click += new System.EventHandler(this.tsmi_ltgd_Click);
            // 
            // tsmi_srgc
            // 
            this.tsmi_srgc.Name = "tsmi_srgc";
            this.tsmi_srgc.Size = new System.Drawing.Size(180, 22);
            this.tsmi_srgc.Text = "收入构成";
            this.tsmi_srgc.Click += new System.EventHandler(this.tsmi_srgc_Click);
            // 
            // FinDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 392);
            this.Controls.Add(this.tnlpnlmain);
            this.Name = "FinDetailsForm";
            this.Text = "FinDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_stock_details)).EndInit();
            this.cms_stockdetails.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tnlpnlmain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tbl_stock_details;
        private System.Windows.Forms.ContextMenuStrip cms_stockdetails;
        private System.Windows.Forms.ToolStripMenuItem tsmi_addhd;
        private System.Windows.Forms.ToolStripMenuItem tsmi_view_report;
        private System.Windows.Forms.Panel pnlTop;
        public System.Windows.Forms.ComboBox cbxReportType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tnlpnlmain;
        private System.Windows.Forms.ToolStripMenuItem tsmi_wdm;
        private System.Windows.Forms.ToolStripMenuItem tsmi_mainbiz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdqk;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lsgz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ltgd;
        private System.Windows.Forms.ToolStripMenuItem tsmi_srgc;
    }
}