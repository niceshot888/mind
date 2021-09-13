namespace NiceShot.DotNetWinFormsClient
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ms_mainform = new System.Windows.Forms.MenuStrip();
            this.tsm_a = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_a_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_hk_common = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_a_insur = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_datasync = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_market_datasync = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_commonlink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_zdgqzycx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_hsgt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lbty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ths = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gdzjc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_generatezoniaxy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ecdata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_money_supply = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bdi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_invest_kownage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_moat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_rulesforsuccess = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_makefriendwithtime = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.tbl_datalist = new System.Windows.Forms.DataGridView();
            this.pnl_top = new System.Windows.Forms.Panel();
            this.scp_toppanel = new NiceShot.DotNetWinFormsClient.UserControls.SearchConditionPanel();
            this.cms_stock_details = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_bizmodel_ana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_view_major_biz_scope = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_stock_analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bizmodel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gsmx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_measuringthemoat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_historyinfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_gdrs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lsgz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_tdx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_otherfunc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_jgcc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_copyseccode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_fin_add_details = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_is_follow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_markedas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_isbestbiz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pendingresearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_followup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.ms_mainform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).BeginInit();
            this.pnl_top.SuspendLayout();
            this.cms_stock_details.SuspendLayout();
            this.SuspendLayout();
            // 
            // ms_mainform
            // 
            this.ms_mainform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_a,
            this.tsmi_datasync,
            this.tsmi_commonlink,
            this.tsmi_ecdata,
            this.tsmi_invest_kownage});
            this.ms_mainform.Location = new System.Drawing.Point(0, 0);
            this.ms_mainform.Name = "ms_mainform";
            this.ms_mainform.Size = new System.Drawing.Size(1262, 25);
            this.ms_mainform.TabIndex = 0;
            this.ms_mainform.Text = "menuStrip1";
            // 
            // tsm_a
            // 
            this.tsm_a.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_a_normal,
            this.tsm_hk_common,
            this.tsm_a_insur});
            this.tsm_a.Name = "tsm_a";
            this.tsm_a.Size = new System.Drawing.Size(68, 21);
            this.tsm_a.Text = "市场数据";
            // 
            // tsm_a_normal
            // 
            this.tsm_a_normal.Name = "tsm_a_normal";
            this.tsm_a_normal.Size = new System.Drawing.Size(100, 22);
            this.tsm_a_normal.Text = "A股";
            this.tsm_a_normal.Click += new System.EventHandler(this.tsm_a_normal_Click);
            // 
            // tsm_hk_common
            // 
            this.tsm_hk_common.Name = "tsm_hk_common";
            this.tsm_hk_common.Size = new System.Drawing.Size(100, 22);
            this.tsm_hk_common.Text = "港股";
            this.tsm_hk_common.Click += new System.EventHandler(this.tsm_hk_common_Click);
            // 
            // tsm_a_insur
            // 
            this.tsm_a_insur.Name = "tsm_a_insur";
            this.tsm_a_insur.Size = new System.Drawing.Size(100, 22);
            this.tsm_a_insur.Text = "美股";
            // 
            // tsmi_datasync
            // 
            this.tsmi_datasync.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_market_datasync});
            this.tsmi_datasync.Name = "tsmi_datasync";
            this.tsmi_datasync.Size = new System.Drawing.Size(68, 21);
            this.tsmi_datasync.Text = "数据同步";
            // 
            // tsmi_market_datasync
            // 
            this.tsmi_market_datasync.Name = "tsmi_market_datasync";
            this.tsmi_market_datasync.Size = new System.Drawing.Size(148, 22);
            this.tsmi_market_datasync.Text = "市场数据同步";
            this.tsmi_market_datasync.Click += new System.EventHandler(this.tsmi_market_datasync_Click);
            // 
            // tsmi_commonlink
            // 
            this.tsmi_commonlink.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_zdgqzycx,
            this.tsmi_hsgt,
            this.tsmi_lbty,
            this.tsmi_ths,
            this.tsmi_gdzjc,
            this.tsmi_generatezoniaxy});
            this.tsmi_commonlink.Name = "tsmi_commonlink";
            this.tsmi_commonlink.Size = new System.Drawing.Size(68, 21);
            this.tsmi_commonlink.Text = "常用链接";
            // 
            // tsmi_zdgqzycx
            // 
            this.tsmi_zdgqzycx.Name = "tsmi_zdgqzycx";
            this.tsmi_zdgqzycx.Size = new System.Drawing.Size(172, 22);
            this.tsmi_zdgqzycx.Text = "中登股权质押查询";
            this.tsmi_zdgqzycx.Click += new System.EventHandler(this.tsmi_zdgqzycx_Click);
            // 
            // tsmi_hsgt
            // 
            this.tsmi_hsgt.Name = "tsmi_hsgt";
            this.tsmi_hsgt.Size = new System.Drawing.Size(172, 22);
            this.tsmi_hsgt.Text = "沪深港通数据查询";
            this.tsmi_hsgt.Click += new System.EventHandler(this.tsmi_hsgt_Click);
            // 
            // tsmi_lbty
            // 
            this.tsmi_lbty.Name = "tsmi_lbty";
            this.tsmi_lbty.Size = new System.Drawing.Size(172, 22);
            this.tsmi_lbty.Text = "萝卜投研数据查询";
            this.tsmi_lbty.Click += new System.EventHandler(this.tsmi_lbty_Click);
            // 
            // tsmi_ths
            // 
            this.tsmi_ths.Name = "tsmi_ths";
            this.tsmi_ths.Size = new System.Drawing.Size(172, 22);
            this.tsmi_ths.Text = "同花顺数据查询";
            this.tsmi_ths.Click += new System.EventHandler(this.tsmi_ths_Click);
            // 
            // tsmi_gdzjc
            // 
            this.tsmi_gdzjc.Name = "tsmi_gdzjc";
            this.tsmi_gdzjc.Size = new System.Drawing.Size(172, 22);
            this.tsmi_gdzjc.Text = "股东增减持";
            this.tsmi_gdzjc.Click += new System.EventHandler(this.tsmi_gdzjc_Click);
            // 
            // tsmi_generatezoniaxy
            // 
            this.tsmi_generatezoniaxy.Name = "tsmi_generatezoniaxy";
            this.tsmi_generatezoniaxy.Size = new System.Drawing.Size(172, 22);
            this.tsmi_generatezoniaxy.Text = "转换拿地城市坐标";
            this.tsmi_generatezoniaxy.Click += new System.EventHandler(this.tsmi_generatezoniaxy_Click);
            // 
            // tsmi_ecdata
            // 
            this.tsmi_ecdata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_money_supply,
            this.tsmi_bdi});
            this.tsmi_ecdata.Name = "tsmi_ecdata";
            this.tsmi_ecdata.Size = new System.Drawing.Size(68, 21);
            this.tsmi_ecdata.Text = "经济数据";
            // 
            // tsmi_money_supply
            // 
            this.tsmi_money_supply.Name = "tsmi_money_supply";
            this.tsmi_money_supply.Size = new System.Drawing.Size(136, 22);
            this.tsmi_money_supply.Text = "货币供应量";
            this.tsmi_money_supply.Click += new System.EventHandler(this.tsmi_money_supply_Click);
            // 
            // tsmi_bdi
            // 
            this.tsmi_bdi.Name = "tsmi_bdi";
            this.tsmi_bdi.Size = new System.Drawing.Size(136, 22);
            this.tsmi_bdi.Text = "BDI指数";
            this.tsmi_bdi.Click += new System.EventHandler(this.tsmi_bdi_Click);
            // 
            // tsmi_invest_kownage
            // 
            this.tsmi_invest_kownage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_moat,
            this.tsmi_rulesforsuccess,
            this.tsmi_makefriendwithtime});
            this.tsmi_invest_kownage.Name = "tsmi_invest_kownage";
            this.tsmi_invest_kownage.Size = new System.Drawing.Size(68, 21);
            this.tsmi_invest_kownage.Text = "投资常识";
            // 
            // tsmi_moat
            // 
            this.tsmi_moat.Name = "tsmi_moat";
            this.tsmi_moat.Size = new System.Drawing.Size(180, 22);
            this.tsmi_moat.Text = "护城河概念";
            this.tsmi_moat.Click += new System.EventHandler(this.tsmi_moat_Click);
            // 
            // tsmi_rulesforsuccess
            // 
            this.tsmi_rulesforsuccess.Name = "tsmi_rulesforsuccess";
            this.tsmi_rulesforsuccess.Size = new System.Drawing.Size(180, 22);
            this.tsmi_rulesforsuccess.Text = "股市真规则";
            this.tsmi_rulesforsuccess.Click += new System.EventHandler(this.tsmi_rulesforsuccess_Click);
            // 
            // tsmi_makefriendwithtime
            // 
            this.tsmi_makefriendwithtime.Name = "tsmi_makefriendwithtime";
            this.tsmi_makefriendwithtime.Size = new System.Drawing.Size(180, 22);
            this.tsmi_makefriendwithtime.Text = "做时间的朋友";
            this.tsmi_makefriendwithtime.Click += new System.EventHandler(this.tsmi_makefriendwithtime_Click);
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 848);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(1262, 13);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tbl_datalist.ColumnHeadersHeight = 40;
            this.tbl_datalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.DefaultCellStyle = dataGridViewCellStyle2;
            this.tbl_datalist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_datalist.GridColor = System.Drawing.Color.LightSteelBlue;
            this.tbl_datalist.Location = new System.Drawing.Point(0, 94);
            this.tbl_datalist.Name = "tbl_datalist";
            this.tbl_datalist.ReadOnly = true;
            this.tbl_datalist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_datalist.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.tbl_datalist.RowTemplate.Height = 23;
            this.tbl_datalist.Size = new System.Drawing.Size(1262, 754);
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
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 25);
            this.pnl_top.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(1262, 69);
            this.pnl_top.TabIndex = 1;
            this.pnl_top.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_top_MouseClick);
            // 
            // scp_toppanel
            // 
            this.scp_toppanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scp_toppanel.Location = new System.Drawing.Point(0, 0);
            this.scp_toppanel.Name = "scp_toppanel";
            this.scp_toppanel.Size = new System.Drawing.Size(1262, 69);
            this.scp_toppanel.TabIndex = 19;
            // 
            // cms_stock_details
            // 
            this.cms_stock_details.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_bizmodel_ana,
            this.tsmi_historyinfo,
            this.tsmi_otherfunc,
            this.tsmi_markedas});
            this.cms_stock_details.Name = "cms_stock_details";
            this.cms_stock_details.Size = new System.Drawing.Size(125, 92);
            this.cms_stock_details.Opening += new System.ComponentModel.CancelEventHandler(this.cms_stock_details_Opening);
            // 
            // tsmi_bizmodel_ana
            // 
            this.tsmi_bizmodel_ana.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_view_major_biz_scope,
            this.tsmi_stock_analysis,
            this.tsmi_bizmodel,
            this.tsmi_gsmx,
            this.tsmi_measuringthemoat});
            this.tsmi_bizmodel_ana.Name = "tsmi_bizmodel_ana";
            this.tsmi_bizmodel_ana.Size = new System.Drawing.Size(124, 22);
            this.tsmi_bizmodel_ana.Text = "商业模式";
            // 
            // tsmi_view_major_biz_scope
            // 
            this.tsmi_view_major_biz_scope.Name = "tsmi_view_major_biz_scope";
            this.tsmi_view_major_biz_scope.Size = new System.Drawing.Size(136, 22);
            this.tsmi_view_major_biz_scope.Text = "主营构成";
            this.tsmi_view_major_biz_scope.Click += new System.EventHandler(this.tsmi_view_major_biz_scope_Click);
            // 
            // tsmi_stock_analysis
            // 
            this.tsmi_stock_analysis.Name = "tsmi_stock_analysis";
            this.tsmi_stock_analysis.Size = new System.Drawing.Size(136, 22);
            this.tsmi_stock_analysis.Text = "个股分析";
            this.tsmi_stock_analysis.Click += new System.EventHandler(this.tsmi_stock_analysis_Click);
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
            // tsmi_historyinfo
            // 
            this.tsmi_historyinfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_gdrs,
            this.tsmi_lsgz,
            this.tsmi_tdx});
            this.tsmi_historyinfo.Name = "tsmi_historyinfo";
            this.tsmi_historyinfo.Size = new System.Drawing.Size(124, 22);
            this.tsmi_historyinfo.Text = "历史数据";
            // 
            // tsmi_gdrs
            // 
            this.tsmi_gdrs.Name = "tsmi_gdrs";
            this.tsmi_gdrs.Size = new System.Drawing.Size(136, 22);
            this.tsmi_gdrs.Text = "股东人数";
            this.tsmi_gdrs.Click += new System.EventHandler(this.tsmi_gdrs_Click);
            // 
            // tsmi_lsgz
            // 
            this.tsmi_lsgz.Name = "tsmi_lsgz";
            this.tsmi_lsgz.Size = new System.Drawing.Size(136, 22);
            this.tsmi_lsgz.Text = "历史估值";
            this.tsmi_lsgz.Click += new System.EventHandler(this.tsmi_lsgz_Click);
            // 
            // tsmi_tdx
            // 
            this.tsmi_tdx.Name = "tsmi_tdx";
            this.tsmi_tdx.Size = new System.Drawing.Size(136, 22);
            this.tsmi_tdx.Text = "通达信分析";
            this.tsmi_tdx.Click += new System.EventHandler(this.tsmi_tdx_Click);
            // 
            // tsmi_otherfunc
            // 
            this.tsmi_otherfunc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_bond,
            this.tsmi_jgcc,
            this.tsmi_copyseccode,
            this.tsmi_fin_add_details,
            this.tsmi_is_follow});
            this.tsmi_otherfunc.Name = "tsmi_otherfunc";
            this.tsmi_otherfunc.Size = new System.Drawing.Size(124, 22);
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
            // tsmi_is_follow
            // 
            this.tsmi_is_follow.Name = "tsmi_is_follow";
            this.tsmi_is_follow.Size = new System.Drawing.Size(148, 22);
            this.tsmi_is_follow.Text = "关注";
            this.tsmi_is_follow.Click += new System.EventHandler(this.tsmi_is_follow_Click);
            // 
            // tsmi_markedas
            // 
            this.tsmi_markedas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_isbestbiz,
            this.tsmi_pendingresearch,
            this.tsmi_followup,
            this.tsmi_normal});
            this.tsmi_markedas.Name = "tsmi_markedas";
            this.tsmi_markedas.Size = new System.Drawing.Size(124, 22);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 861);
            this.Controls.Add(this.tbl_datalist);
            this.Controls.Add(this.pnl_top);
            this.Controls.Add(this.ms_mainform);
            this.Controls.Add(this.pnl_bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.ms_mainform;
            this.Name = "MainForm";
            this.Text = "好球投资";
            this.ms_mainform.ResumeLayout(false);
            this.ms_mainform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_datalist)).EndInit();
            this.pnl_top.ResumeLayout(false);
            this.cms_stock_details.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms_mainform;
        private System.Windows.Forms.ToolStripMenuItem tsm_a;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_normal;
        private System.Windows.Forms.ToolStripMenuItem tsm_hk_common;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_insur;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.DataGridView tbl_datalist;
        private System.Windows.Forms.ContextMenuStrip cms_stock_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_stock_analysis;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdrs;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lsgz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jgcc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gsmx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_datasync;
        private UserControls.SearchConditionPanel scp_toppanel;
        public System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.ToolStripMenuItem tsmi_is_follow;
        private System.Windows.Forms.ToolStripMenuItem tsmi_tdx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_commonlink;
        private System.Windows.Forms.ToolStripMenuItem tsmi_zdgqzycx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_hsgt;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lbty;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ths;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdzjc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bizmodel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ecdata;
        private System.Windows.Forms.ToolStripMenuItem tsmi_money_supply;
        private System.Windows.Forms.ToolStripMenuItem tsmi_copyseccode;
        private System.Windows.Forms.ToolStripMenuItem tsmi_market_datasync;
        private System.Windows.Forms.ToolStripMenuItem tsmi_fin_add_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_generatezoniaxy;
        private System.Windows.Forms.ToolStripMenuItem tsmi_view_major_biz_scope;
        private System.Windows.Forms.ToolStripMenuItem tsmi_historyinfo;
        private System.Windows.Forms.ToolStripMenuItem tsmi_otherfunc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bizmodel_ana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_markedas;
        private System.Windows.Forms.ToolStripMenuItem tsmi_isbestbiz;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pendingresearch;
        private System.Windows.Forms.ToolStripMenuItem tsmi_followup;
        private System.Windows.Forms.ToolStripMenuItem tsmi_normal;
        private System.Windows.Forms.ToolStripMenuItem tsmi_invest_kownage;
        private System.Windows.Forms.ToolStripMenuItem tsmi_moat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_rulesforsuccess;
        private System.Windows.Forms.ToolStripMenuItem tsmi_makefriendwithtime;
        private System.Windows.Forms.ToolStripMenuItem tsmi_measuringthemoat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bond;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bdi;
    }
}