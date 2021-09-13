namespace NiceShot.DotNetWinFormsClient
{
    partial class GridForm
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
            this.tsmi_gsmx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_em_finreports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lsgz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gdrs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_stock_analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_stock_details = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_jgcc = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_kfjlr_zs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxKfjlr = new System.Windows.Forms.TextBox();
            this.lblKfjlr = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblpercent = new System.Windows.Forms.Label();
            this.tbl_datalist = new System.Windows.Forms.DataGridView();
            this.ms_mainform = new System.Windows.Forms.MenuStrip();
            this.tsm_a = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_a_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_hk_common = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_a_insur = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_datasync = new System.Windows.Forms.ToolStripMenuItem();
            this.tbxRoe = new System.Windows.Forms.TextBox();
            this.lblroe = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lstStockName = new System.Windows.Forms.ListBox();
            this.tbxStockName = new System.Windows.Forms.TextBox();
            this.lstIndustry = new System.Windows.Forms.ListBox();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.pnl_top = new System.Windows.Forms.Panel();
            this.tbxIndustry = new System.Windows.Forms.TextBox();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.cbxIndustry = new System.Windows.Forms.ComboBox();
            this.cms_stock_details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).BeginInit();
            this.ms_mainform.SuspendLayout();
            this.pnl_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmi_gsmx
            // 
            this.tsmi_gsmx.Name = "tsmi_gsmx";
            this.tsmi_gsmx.Size = new System.Drawing.Size(124, 22);
            this.tsmi_gsmx.Text = "郭氏模型";
            // 
            // tsmi_em_finreports
            // 
            this.tsmi_em_finreports.Name = "tsmi_em_finreports";
            this.tsmi_em_finreports.Size = new System.Drawing.Size(124, 22);
            this.tsmi_em_finreports.Text = "财务报表";
            // 
            // tsmi_lsgz
            // 
            this.tsmi_lsgz.Name = "tsmi_lsgz";
            this.tsmi_lsgz.Size = new System.Drawing.Size(124, 22);
            this.tsmi_lsgz.Text = "历史估值";
            // 
            // tsmi_gdrs
            // 
            this.tsmi_gdrs.Name = "tsmi_gdrs";
            this.tsmi_gdrs.Size = new System.Drawing.Size(124, 22);
            this.tsmi_gdrs.Text = "股东人数";
            // 
            // tsmi_stock_analysis
            // 
            this.tsmi_stock_analysis.Name = "tsmi_stock_analysis";
            this.tsmi_stock_analysis.Size = new System.Drawing.Size(124, 22);
            this.tsmi_stock_analysis.Text = "个股分析";
            // 
            // cms_stock_details
            // 
            this.cms_stock_details.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_stock_analysis,
            this.tsmi_gdrs,
            this.tsmi_lsgz,
            this.tsmi_em_finreports,
            this.tsmi_jgcc,
            this.tsmi_gsmx});
            this.cms_stock_details.Name = "cms_stock_details";
            this.cms_stock_details.Size = new System.Drawing.Size(125, 136);
            // 
            // tsmi_jgcc
            // 
            this.tsmi_jgcc.Name = "tsmi_jgcc";
            this.tsmi_jgcc.Size = new System.Drawing.Size(124, 22);
            this.tsmi_jgcc.Text = "机构持仓";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(906, 14);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "清除条件";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(792, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "%";
            // 
            // tbx_kfjlr_zs
            // 
            this.tbx_kfjlr_zs.Location = new System.Drawing.Point(750, 16);
            this.tbx_kfjlr_zs.Name = "tbx_kfjlr_zs";
            this.tbx_kfjlr_zs.Size = new System.Drawing.Size(36, 21);
            this.tbx_kfjlr_zs.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(636, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "扣非净利润增速 >= ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(598, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "亿元";
            // 
            // tbxKfjlr
            // 
            this.tbxKfjlr.Location = new System.Drawing.Point(544, 15);
            this.tbxKfjlr.Name = "tbxKfjlr";
            this.tbxKfjlr.Size = new System.Drawing.Size(50, 21);
            this.tbxKfjlr.TabIndex = 13;
            // 
            // lblKfjlr
            // 
            this.lblKfjlr.AutoSize = true;
            this.lblKfjlr.Location = new System.Drawing.Point(453, 19);
            this.lblKfjlr.Name = "lblKfjlr";
            this.lblKfjlr.Size = new System.Drawing.Size(89, 12);
            this.lblKfjlr.TabIndex = 12;
            this.lblKfjlr.Text = "扣非净利润 >= ";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(825, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "开始查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblpercent
            // 
            this.lblpercent.AutoSize = true;
            this.lblpercent.Location = new System.Drawing.Point(435, 19);
            this.lblpercent.Name = "lblpercent";
            this.lblpercent.Size = new System.Drawing.Size(11, 12);
            this.lblpercent.TabIndex = 10;
            this.lblpercent.Text = "%";
            // 
            // tbl_datalist
            // 
            this.tbl_datalist.AllowUserToAddRows = false;
            this.tbl_datalist.AllowUserToDeleteRows = false;
            this.tbl_datalist.AllowUserToResizeRows = false;
            this.tbl_datalist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbl_datalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tbl_datalist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_datalist.Location = new System.Drawing.Point(0, 75);
            this.tbl_datalist.Name = "tbl_datalist";
            this.tbl_datalist.ReadOnly = true;
            this.tbl_datalist.RowTemplate.Height = 23;
            this.tbl_datalist.Size = new System.Drawing.Size(800, 362);
            this.tbl_datalist.TabIndex = 3;
            // 
            // ms_mainform
            // 
            this.ms_mainform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_a,
            this.tsmi_datasync});
            this.ms_mainform.Location = new System.Drawing.Point(0, 50);
            this.ms_mainform.Name = "ms_mainform";
            this.ms_mainform.Size = new System.Drawing.Size(800, 25);
            this.ms_mainform.TabIndex = 4;
            this.ms_mainform.Text = "menuStrip1";
            // 
            // tsm_a
            // 
            this.tsm_a.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_a_normal,
            this.tsm_hk_common,
            this.tsm_a_insur});
            this.tsm_a.Name = "tsm_a";
            this.tsm_a.Size = new System.Drawing.Size(56, 21);
            this.tsm_a.Text = "A股(A)";
            // 
            // tsm_a_normal
            // 
            this.tsm_a_normal.Name = "tsm_a_normal";
            this.tsm_a_normal.Size = new System.Drawing.Size(100, 22);
            this.tsm_a_normal.Text = "A股";
            // 
            // tsm_hk_common
            // 
            this.tsm_hk_common.Name = "tsm_hk_common";
            this.tsm_hk_common.Size = new System.Drawing.Size(100, 22);
            this.tsm_hk_common.Text = "港股";
            // 
            // tsm_a_insur
            // 
            this.tsm_a_insur.Name = "tsm_a_insur";
            this.tsm_a_insur.Size = new System.Drawing.Size(100, 22);
            this.tsm_a_insur.Text = "美股";
            // 
            // tsmi_datasync
            // 
            this.tsmi_datasync.Name = "tsmi_datasync";
            this.tsmi_datasync.Size = new System.Drawing.Size(68, 21);
            this.tsmi_datasync.Text = "数据同步";
            // 
            // tbxRoe
            // 
            this.tbxRoe.Location = new System.Drawing.Point(395, 15);
            this.tbxRoe.Name = "tbxRoe";
            this.tbxRoe.Size = new System.Drawing.Size(36, 21);
            this.tbxRoe.TabIndex = 9;
            // 
            // lblroe
            // 
            this.lblroe.AutoSize = true;
            this.lblroe.Location = new System.Drawing.Point(350, 20);
            this.lblroe.Name = "lblroe";
            this.lblroe.Size = new System.Drawing.Size(41, 12);
            this.lblroe.TabIndex = 8;
            this.lblroe.Text = "ROE >=";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(166, 19);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(101, 12);
            this.lblStockName.TabIndex = 5;
            this.lblStockName.Text = "股票名称或代码：";
            // 
            // lstStockName
            // 
            this.lstStockName.FormattingEnabled = true;
            this.lstStockName.ItemHeight = 12;
            this.lstStockName.Location = new System.Drawing.Point(268, 38);
            this.lstStockName.Name = "lstStockName";
            this.lstStockName.Size = new System.Drawing.Size(71, 136);
            this.lstStockName.TabIndex = 4;
            this.lstStockName.Visible = false;
            // 
            // tbxStockName
            // 
            this.tbxStockName.Location = new System.Drawing.Point(268, 15);
            this.tbxStockName.Name = "tbxStockName";
            this.tbxStockName.Size = new System.Drawing.Size(71, 21);
            this.tbxStockName.TabIndex = 3;
            // 
            // lstIndustry
            // 
            this.lstIndustry.FormattingEnabled = true;
            this.lstIndustry.ItemHeight = 12;
            this.lstIndustry.Location = new System.Drawing.Point(78, 38);
            this.lstIndustry.Name = "lstIndustry";
            this.lstIndustry.ScrollAlwaysVisible = true;
            this.lstIndustry.Size = new System.Drawing.Size(72, 136);
            this.lstIndustry.TabIndex = 2;
            this.lstIndustry.Visible = false;
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Location = new System.Drawing.Point(12, 19);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(65, 12);
            this.lblIndustry.TabIndex = 0;
            this.lblIndustry.Text = "所属行业：";
            // 
            // pnl_top
            // 
            this.pnl_top.Controls.Add(this.cbxIndustry);
            this.pnl_top.Controls.Add(this.lstIndustry);
            this.pnl_top.Controls.Add(this.btnClear);
            this.pnl_top.Controls.Add(this.label2);
            this.pnl_top.Controls.Add(this.tbx_kfjlr_zs);
            this.pnl_top.Controls.Add(this.label3);
            this.pnl_top.Controls.Add(this.label1);
            this.pnl_top.Controls.Add(this.tbxKfjlr);
            this.pnl_top.Controls.Add(this.lblKfjlr);
            this.pnl_top.Controls.Add(this.btnSearch);
            this.pnl_top.Controls.Add(this.lblpercent);
            this.pnl_top.Controls.Add(this.tbxRoe);
            this.pnl_top.Controls.Add(this.lblroe);
            this.pnl_top.Controls.Add(this.lblStockName);
            this.pnl_top.Controls.Add(this.lstStockName);
            this.pnl_top.Controls.Add(this.tbxStockName);
            this.pnl_top.Controls.Add(this.tbxIndustry);
            this.pnl_top.Controls.Add(this.lblIndustry);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(800, 50);
            this.pnl_top.TabIndex = 5;
            // 
            // tbxIndustry
            // 
            this.tbxIndustry.Location = new System.Drawing.Point(638, 10);
            this.tbxIndustry.Name = "tbxIndustry";
            this.tbxIndustry.Size = new System.Drawing.Size(72, 21);
            this.tbxIndustry.TabIndex = 1;
            this.tbxIndustry.Text = "房地产";
            this.tbxIndustry.Visible = false;
            this.tbxIndustry.TextChanged += new System.EventHandler(this.tbxIndustry_TextChanged);
            this.tbxIndustry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxIndustry_KeyUp);
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 437);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(800, 13);
            this.pnl_bottom.TabIndex = 6;
            // 
            // cbxIndustry
            // 
            this.cbxIndustry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxIndustry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxIndustry.FormattingEnabled = true;
            this.cbxIndustry.Location = new System.Drawing.Point(78, 13);
            this.cbxIndustry.Name = "cbxIndustry";
            this.cbxIndustry.Size = new System.Drawing.Size(72, 20);
            this.cbxIndustry.TabIndex = 19;
            this.cbxIndustry.TextUpdate += new System.EventHandler(this.cbxIndustry_TextUpdate);
            // 
            // GridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbl_datalist);
            this.Controls.Add(this.ms_mainform);
            this.Controls.Add(this.pnl_top);
            this.Controls.Add(this.pnl_bottom);
            this.Name = "GridForm";
            this.Text = "GridForm";
            this.cms_stock_details.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).EndInit();
            this.ms_mainform.ResumeLayout(false);
            this.ms_mainform.PerformLayout();
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmi_gsmx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_em_finreports;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lsgz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdrs;
        private System.Windows.Forms.ToolStripMenuItem tsmi_stock_analysis;
        private System.Windows.Forms.ContextMenuStrip cms_stock_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jgcc;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_kfjlr_zs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxKfjlr;
        private System.Windows.Forms.Label lblKfjlr;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblpercent;
        private System.Windows.Forms.DataGridView tbl_datalist;
        private System.Windows.Forms.MenuStrip ms_mainform;
        private System.Windows.Forms.ToolStripMenuItem tsm_a;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_normal;
        private System.Windows.Forms.ToolStripMenuItem tsm_hk_common;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_insur;
        private System.Windows.Forms.ToolStripMenuItem tsmi_datasync;
        private System.Windows.Forms.TextBox tbxRoe;
        private System.Windows.Forms.Label lblroe;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.ListBox lstStockName;
        private System.Windows.Forms.TextBox tbxStockName;
        private System.Windows.Forms.ListBox lstIndustry;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.TextBox tbxIndustry;
        private System.Windows.Forms.ComboBox cbxIndustry;
    }
}