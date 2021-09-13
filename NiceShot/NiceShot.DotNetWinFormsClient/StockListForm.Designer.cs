namespace NiceShot.DotNetWinFormsClient
{
    partial class StockListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockListForm));
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.tbl_datalist = new System.Windows.Forms.DataGridView();
            this.pnl_top = new System.Windows.Forms.Panel();
            this.scp_toppanel = new NiceShot.DotNetWinFormsClient.UserControls.SearchConditionPanel();
            this.uc_topMenu = new NiceShot.DotNetWinFormsClient.UserControls.TopMenu();
            this.cms_stock_details = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_stock_analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lsgz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gdrs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_recentreport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_askdm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ltgd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_srgc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bizmodel_ana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_view_major_biz_scope = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bizmodel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gsmx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_measuringthemoat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_otherfunc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_jgcc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_copyseccode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_fin_add_details = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_markedas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_isbestbiz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pendingresearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_followup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_is_follow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_quant_fx = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).BeginInit();
            this.pnl_top.SuspendLayout();
            this.cms_stock_details.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 848);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(1450, 13);
            this.pnl_bottom.TabIndex = 2;
            // 
            // tbl_datalist
            // 
            this.tbl_datalist.AllowUserToAddRows = false;
            this.tbl_datalist.AllowUserToDeleteRows = false;
            this.tbl_datalist.AllowUserToResizeRows = false;
            this.tbl_datalist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbl_datalist.BackgroundColor = System.Drawing.Color.White;
            this.tbl_datalist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbl_datalist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.tbl_datalist.ColumnHeadersHeight = 40;
            this.tbl_datalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.DefaultCellStyle = dataGridViewCellStyle8;
            this.tbl_datalist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_datalist.GridColor = System.Drawing.Color.LightSteelBlue;
            this.tbl_datalist.Location = new System.Drawing.Point(0, 176);
            this.tbl_datalist.Name = "tbl_datalist";
            this.tbl_datalist.ReadOnly = true;
            this.tbl_datalist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.tbl_datalist.RowTemplate.Height = 23;
            this.tbl_datalist.Size = new System.Drawing.Size(1450, 672);
            this.tbl_datalist.TabIndex = 0;
            this.tbl_datalist.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tbl_datalist_CellContentDoubleClick);
            this.tbl_datalist.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.tbl_datalist_CellFormatting);
            this.tbl_datalist.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tbl_datalist_CellMouseDown);
            this.tbl_datalist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.tbl_datalist_CellPainting);
            this.tbl_datalist.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.tbl_datalist_CellToolTipTextNeeded);
            this.tbl_datalist.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.tbl_datalist_RowPostPaint);
            // 
            // pnl_top
            // 
            this.pnl_top.Controls.Add(this.scp_toppanel);
            this.pnl_top.Controls.Add(this.uc_topMenu);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(1450, 176);
            this.pnl_top.TabIndex = 1;
            this.pnl_top.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_top_MouseClick);
            // 
            // scp_toppanel
            // 
            this.scp_toppanel.Location = new System.Drawing.Point(0, 26);
            this.scp_toppanel.Name = "scp_toppanel";
            this.scp_toppanel.Size = new System.Drawing.Size(1443, 150);
            this.scp_toppanel.TabIndex = 19;
            // 
            // uc_topMenu
            // 
            this.uc_topMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.uc_topMenu.Location = new System.Drawing.Point(0, 0);
            this.uc_topMenu.Name = "uc_topMenu";
            this.uc_topMenu.Size = new System.Drawing.Size(1450, 25);
            this.uc_topMenu.TabIndex = 20;
            // 
            // cms_stock_details
            // 
            this.cms_stock_details.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_stock_analysis,
            this.tsmi_quant_fx,
            this.tsmi_lsgz,
            this.tsmi_gdrs,
            this.tsmi_recentreport,
            this.tsmi_askdm,
            this.tsmi_ltgd,
            this.tsmi_srgc,
            this.tsmi_bizmodel_ana,
            this.tsmi_otherfunc,
            this.tsmi_markedas});
            this.cms_stock_details.Name = "cms_stock_details";
            this.cms_stock_details.Size = new System.Drawing.Size(181, 268);
            this.cms_stock_details.Opening += new System.ComponentModel.CancelEventHandler(this.cms_stock_details_Opening);
            // 
            // tsmi_stock_analysis
            // 
            this.tsmi_stock_analysis.Name = "tsmi_stock_analysis";
            this.tsmi_stock_analysis.Size = new System.Drawing.Size(180, 22);
            this.tsmi_stock_analysis.Text = "个股分析";
            this.tsmi_stock_analysis.Click += new System.EventHandler(this.tsmi_stock_analysis_Click);
            // 
            // tsmi_lsgz
            // 
            this.tsmi_lsgz.Name = "tsmi_lsgz";
            this.tsmi_lsgz.Size = new System.Drawing.Size(180, 22);
            this.tsmi_lsgz.Text = "历史估值";
            this.tsmi_lsgz.Click += new System.EventHandler(this.tsmi_lsgz_Click);
            // 
            // tsmi_gdrs
            // 
            this.tsmi_gdrs.Name = "tsmi_gdrs";
            this.tsmi_gdrs.Size = new System.Drawing.Size(180, 22);
            this.tsmi_gdrs.Text = "股东人数";
            this.tsmi_gdrs.Click += new System.EventHandler(this.tsmi_gdrs_Click);
            // 
            // tsmi_recentreport
            // 
            this.tsmi_recentreport.Name = "tsmi_recentreport";
            this.tsmi_recentreport.Size = new System.Drawing.Size(180, 22);
            this.tsmi_recentreport.Text = "近期报表";
            this.tsmi_recentreport.Click += new System.EventHandler(this.tsmi_recentreport_Click);
            // 
            // tsmi_askdm
            // 
            this.tsmi_askdm.Name = "tsmi_askdm";
            this.tsmi_askdm.Size = new System.Drawing.Size(180, 22);
            this.tsmi_askdm.Text = "问董秘";
            this.tsmi_askdm.Click += new System.EventHandler(this.tsmi_askdm_Click);
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
            // tsmi_bizmodel_ana
            // 
            this.tsmi_bizmodel_ana.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_view_major_biz_scope,
            this.tsmi_bizmodel,
            this.tsmi_gsmx,
            this.tsmi_measuringthemoat});
            this.tsmi_bizmodel_ana.Name = "tsmi_bizmodel_ana";
            this.tsmi_bizmodel_ana.Size = new System.Drawing.Size(180, 22);
            this.tsmi_bizmodel_ana.Text = "商业模式";
            // 
            // tsmi_view_major_biz_scope
            // 
            this.tsmi_view_major_biz_scope.Name = "tsmi_view_major_biz_scope";
            this.tsmi_view_major_biz_scope.Size = new System.Drawing.Size(136, 22);
            this.tsmi_view_major_biz_scope.Text = "主营构成";
            this.tsmi_view_major_biz_scope.Click += new System.EventHandler(this.tsmi_view_major_biz_scope_Click);
            // 
            // tsmi_bizmodel
            // 
            this.tsmi_bizmodel.Name = "tsmi_bizmodel";
            this.tsmi_bizmodel.Size = new System.Drawing.Size(136, 22);
            this.tsmi_bizmodel.Text = "商模画布";
            this.tsmi_bizmodel.Click += new System.EventHandler(this.tsmi_bizmodel_Click);
            // 
            // tsmi_gsmx
            // 
            this.tsmi_gsmx.Name = "tsmi_gsmx";
            this.tsmi_gsmx.Size = new System.Drawing.Size(136, 22);
            this.tsmi_gsmx.Text = "郭氏模型";
            this.tsmi_gsmx.Click += new System.EventHandler(this.tsmi_gsmx_Click);
            // 
            // tsmi_measuringthemoat
            // 
            this.tsmi_measuringthemoat.Name = "tsmi_measuringthemoat";
            this.tsmi_measuringthemoat.Size = new System.Drawing.Size(136, 22);
            this.tsmi_measuringthemoat.Text = "丈量护城河";
            this.tsmi_measuringthemoat.Click += new System.EventHandler(this.tsmi_measuringthemoat_Click);
            // 
            // tsmi_otherfunc
            // 
            this.tsmi_otherfunc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_bond,
            this.tsmi_jgcc,
            this.tsmi_copyseccode,
            this.tsmi_fin_add_details});
            this.tsmi_otherfunc.Name = "tsmi_otherfunc";
            this.tsmi_otherfunc.Size = new System.Drawing.Size(180, 22);
            this.tsmi_otherfunc.Text = "其他功能";
            // 
            // tsmi_bond
            // 
            this.tsmi_bond.Name = "tsmi_bond";
            this.tsmi_bond.Size = new System.Drawing.Size(148, 22);
            this.tsmi_bond.Text = "债券情况";
            this.tsmi_bond.Click += new System.EventHandler(this.tsmi_bond_Click);
            // 
            // tsmi_jgcc
            // 
            this.tsmi_jgcc.Name = "tsmi_jgcc";
            this.tsmi_jgcc.Size = new System.Drawing.Size(148, 22);
            this.tsmi_jgcc.Text = "机构持仓";
            this.tsmi_jgcc.Click += new System.EventHandler(this.tsmi_jgcc_Click);
            // 
            // tsmi_copyseccode
            // 
            this.tsmi_copyseccode.Name = "tsmi_copyseccode";
            this.tsmi_copyseccode.Size = new System.Drawing.Size(148, 22);
            this.tsmi_copyseccode.Text = "复制股票代码";
            this.tsmi_copyseccode.Click += new System.EventHandler(this.tsmi_copyseccode_Click);
            // 
            // tsmi_fin_add_details
            // 
            this.tsmi_fin_add_details.Name = "tsmi_fin_add_details";
            this.tsmi_fin_add_details.Size = new System.Drawing.Size(148, 22);
            this.tsmi_fin_add_details.Text = "财务附注明细";
            this.tsmi_fin_add_details.Click += new System.EventHandler(this.tsmi_fin_add_details_Click);
            // 
            // tsmi_markedas
            // 
            this.tsmi_markedas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_isbestbiz,
            this.tsmi_pendingresearch,
            this.tsmi_followup,
            this.tsmi_normal,
            this.tsmi_is_follow});
            this.tsmi_markedas.Name = "tsmi_markedas";
            this.tsmi_markedas.Size = new System.Drawing.Size(180, 22);
            this.tsmi_markedas.Text = "标记为";
            // 
            // tsmi_isbestbiz
            // 
            this.tsmi_isbestbiz.Name = "tsmi_isbestbiz";
            this.tsmi_isbestbiz.Size = new System.Drawing.Size(124, 22);
            this.tsmi_isbestbiz.Text = "好生意";
            this.tsmi_isbestbiz.Click += new System.EventHandler(this.tsmi_isbestbiz_Click);
            // 
            // tsmi_pendingresearch
            // 
            this.tsmi_pendingresearch.Name = "tsmi_pendingresearch";
            this.tsmi_pendingresearch.Size = new System.Drawing.Size(124, 22);
            this.tsmi_pendingresearch.Text = "待研究";
            this.tsmi_pendingresearch.Click += new System.EventHandler(this.tsmi_pendingresearch_Click);
            // 
            // tsmi_followup
            // 
            this.tsmi_followup.Name = "tsmi_followup";
            this.tsmi_followup.Size = new System.Drawing.Size(124, 22);
            this.tsmi_followup.Text = "长期跟踪";
            this.tsmi_followup.Click += new System.EventHandler(this.tsmi_followup_Click);
            // 
            // tsmi_normal
            // 
            this.tsmi_normal.Name = "tsmi_normal";
            this.tsmi_normal.Size = new System.Drawing.Size(124, 22);
            this.tsmi_normal.Text = "普通";
            this.tsmi_normal.Click += new System.EventHandler(this.tsmi_normal_Click);
            // 
            // tsmi_is_follow
            // 
            this.tsmi_is_follow.Name = "tsmi_is_follow";
            this.tsmi_is_follow.Size = new System.Drawing.Size(124, 22);
            this.tsmi_is_follow.Text = "关注";
            this.tsmi_is_follow.Click += new System.EventHandler(this.tsmi_is_follow_Click);
            // 
            // tsmi_quant_fx
            // 
            this.tsmi_quant_fx.Name = "tsmi_quant_fx";
            this.tsmi_quant_fx.Size = new System.Drawing.Size(180, 22);
            this.tsmi_quant_fx.Text = "量化分析";
            this.tsmi_quant_fx.Click += new System.EventHandler(this.tsmi_quant_fx_Click);
            // 
            // StockListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 861);
            this.Controls.Add(this.tbl_datalist);
            this.Controls.Add(this.pnl_top);
            this.Controls.Add(this.pnl_bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "StockListForm";
            this.Text = "好球投资";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).EndInit();
            this.pnl_top.ResumeLayout(false);
            this.cms_stock_details.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.DataGridView tbl_datalist;
        private System.Windows.Forms.ContextMenuStrip cms_stock_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_stock_analysis;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdrs;
        //private System.Windows.Forms.ToolStripMenuItem tsmi_lsgz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jgcc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gsmx;
        public System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.ToolStripMenuItem tsmi_is_follow;
        //private System.Windows.Forms.ToolStripMenuItem tsmi_tdx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bizmodel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_copyseccode;
        private System.Windows.Forms.ToolStripMenuItem tsmi_fin_add_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_view_major_biz_scope;
        //private System.Windows.Forms.ToolStripMenuItem tsmi_historyinfo;
        private System.Windows.Forms.ToolStripMenuItem tsmi_otherfunc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bizmodel_ana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_markedas;
        private System.Windows.Forms.ToolStripMenuItem tsmi_isbestbiz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pendingresearch;
        private System.Windows.Forms.ToolStripMenuItem tsmi_followup;
        private System.Windows.Forms.ToolStripMenuItem tsmi_normal;
        private System.Windows.Forms.ToolStripMenuItem tsmi_measuringthemoat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bond;
        private UserControls.TopMenu uc_topMenu;
        private UserControls.SearchConditionPanel scp_toppanel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_recentreport;
        private System.Windows.Forms.ToolStripMenuItem tsmi_askdm;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ltgd;
        private System.Windows.Forms.ToolStripMenuItem tsmi_srgc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lsgz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_quant_fx;
    }
}