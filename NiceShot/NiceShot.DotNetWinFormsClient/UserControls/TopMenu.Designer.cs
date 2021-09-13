
namespace NiceShot.DotNetWinFormsClient.UserControls
{
    partial class TopMenu
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
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
            this.tsmi_googlemap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ecdata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_money_supply = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_bdi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_invest_know = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_moat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_rulesforsuccess = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_makefriendwithtime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_aizhihui = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_douban = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_unlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_unlock_media = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_unlock_book = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_basic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_basic_literacy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_basic_skills = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l1_rw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l1_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l2_rw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l2_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l3_rw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l3_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l4_rw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l4_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l5_rw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ul_l5_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pwup_l6 = new System.Windows.Forms.ToolStripMenuItem();
            this.ms_mainform.SuspendLayout();
            this.SuspendLayout();
            // 
            // ms_mainform
            // 
            this.ms_mainform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_a,
            this.tsmi_datasync,
            this.tsmi_commonlink,
            this.tsmi_ecdata,
            this.tsmi_invest_know,
            this.tsmi_aizhihui});
            this.ms_mainform.Location = new System.Drawing.Point(0, 0);
            this.ms_mainform.Name = "ms_mainform";
            this.ms_mainform.Size = new System.Drawing.Size(1184, 25);
            this.ms_mainform.TabIndex = 1;
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
            this.tsm_a_insur.Click += new System.EventHandler(this.tsm_a_insur_Click);
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
            this.tsmi_generatezoniaxy,
            this.tsmi_googlemap});
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
            // tsmi_googlemap
            // 
            this.tsmi_googlemap.Name = "tsmi_googlemap";
            this.tsmi_googlemap.Size = new System.Drawing.Size(172, 22);
            this.tsmi_googlemap.Text = "谷歌地图";
            this.tsmi_googlemap.Click += new System.EventHandler(this.tsmi_googlemap_Click);
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
            this.tsmi_money_supply.Size = new System.Drawing.Size(180, 22);
            this.tsmi_money_supply.Text = "货币供应量";
            this.tsmi_money_supply.Click += new System.EventHandler(this.tsmi_money_supply_Click);
            // 
            // tsmi_bdi
            // 
            this.tsmi_bdi.Name = "tsmi_bdi";
            this.tsmi_bdi.Size = new System.Drawing.Size(180, 22);
            this.tsmi_bdi.Text = "BDI指数";
            this.tsmi_bdi.Click += new System.EventHandler(this.tsmi_bdi_Click);
            // 
            // tsmi_invest_know
            // 
            this.tsmi_invest_know.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_moat,
            this.tsmi_rulesforsuccess,
            this.tsmi_makefriendwithtime});
            this.tsmi_invest_know.Name = "tsmi_invest_know";
            this.tsmi_invest_know.Size = new System.Drawing.Size(68, 21);
            this.tsmi_invest_know.Text = "投资常识";
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
            // tsmi_aizhihui
            // 
            this.tsmi_aizhihui.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_douban,
            this.tsmi_unlock,
            this.tsmi_pwup});
            this.tsmi_aizhihui.Name = "tsmi_aizhihui";
            this.tsmi_aizhihui.Size = new System.Drawing.Size(56, 21);
            this.tsmi_aizhihui.Text = "爱智慧";
            // 
            // tsmi_douban
            // 
            this.tsmi_douban.Name = "tsmi_douban";
            this.tsmi_douban.Size = new System.Drawing.Size(180, 22);
            this.tsmi_douban.Text = "豆瓣读书";
            this.tsmi_douban.Click += new System.EventHandler(this.tsmi_douban_Click);
            // 
            // tsmi_unlock
            // 
            this.tsmi_unlock.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_unlock_media,
            this.tsmi_unlock_book});
            this.tsmi_unlock.Name = "tsmi_unlock";
            this.tsmi_unlock.Size = new System.Drawing.Size(180, 22);
            this.tsmi_unlock.Text = "UNLOCK";
            // 
            // tsmi_unlock_media
            // 
            this.tsmi_unlock_media.Name = "tsmi_unlock_media";
            this.tsmi_unlock_media.Size = new System.Drawing.Size(180, 22);
            this.tsmi_unlock_media.Text = "媒体库";
            this.tsmi_unlock_media.Click += new System.EventHandler(this.tsmi_unlock_media_Click);
            // 
            // tsmi_unlock_book
            // 
            this.tsmi_unlock_book.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_basic,
            this.tsmi_ul_l1,
            this.tsmi_ul_l2,
            this.tsmi_ul_l3,
            this.tsmi_ul_l4,
            this.tsmi_ul_l5});
            this.tsmi_unlock_book.Name = "tsmi_unlock_book";
            this.tsmi_unlock_book.Size = new System.Drawing.Size(180, 22);
            this.tsmi_unlock_book.Text = "教材";
            // 
            // tsmi_ul_basic
            // 
            this.tsmi_ul_basic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_basic_literacy,
            this.tsmi_ul_basic_skills});
            this.tsmi_ul_basic.Name = "tsmi_ul_basic";
            this.tsmi_ul_basic.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_basic.Text = "Basic";
            // 
            // tsmi_ul_l1
            // 
            this.tsmi_ul_l1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_l1_rw,
            this.tsmi_ul_l1_ls});
            this.tsmi_ul_l1.Name = "tsmi_ul_l1";
            this.tsmi_ul_l1.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l1.Text = "Level1";
            // 
            // tsmi_ul_l2
            // 
            this.tsmi_ul_l2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_l2_rw,
            this.tsmi_ul_l2_ls});
            this.tsmi_ul_l2.Name = "tsmi_ul_l2";
            this.tsmi_ul_l2.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l2.Text = "Level2";
            // 
            // tsmi_ul_l3
            // 
            this.tsmi_ul_l3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_l3_rw,
            this.tsmi_ul_l3_ls});
            this.tsmi_ul_l3.Name = "tsmi_ul_l3";
            this.tsmi_ul_l3.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l3.Text = "Level3";
            // 
            // tsmi_ul_l4
            // 
            this.tsmi_ul_l4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_l4_rw,
            this.tsmi_ul_l4_ls});
            this.tsmi_ul_l4.Name = "tsmi_ul_l4";
            this.tsmi_ul_l4.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l4.Text = "Level4";
            // 
            // tsmi_ul_l5
            // 
            this.tsmi_ul_l5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ul_l5_rw,
            this.tsmi_ul_l5_ls});
            this.tsmi_ul_l5.Name = "tsmi_ul_l5";
            this.tsmi_ul_l5.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l5.Text = "Level5";
            // 
            // tsmi_ul_basic_literacy
            // 
            this.tsmi_ul_basic_literacy.Name = "tsmi_ul_basic_literacy";
            this.tsmi_ul_basic_literacy.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_basic_literacy.Text = "基本素养";
            this.tsmi_ul_basic_literacy.Click += new System.EventHandler(this.tsmi_ul_basic_literacy_Click);
            // 
            // tsmi_ul_basic_skills
            // 
            this.tsmi_ul_basic_skills.Name = "tsmi_ul_basic_skills";
            this.tsmi_ul_basic_skills.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_basic_skills.Text = "基本技能";
            this.tsmi_ul_basic_skills.Click += new System.EventHandler(this.tsmi_ul_basic_skills_Click);
            // 
            // tsmi_ul_l1_rw
            // 
            this.tsmi_ul_l1_rw.Name = "tsmi_ul_l1_rw";
            this.tsmi_ul_l1_rw.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l1_rw.Text = "读写";
            this.tsmi_ul_l1_rw.Click += new System.EventHandler(this.tsmi_ul_l1_rw_Click);
            // 
            // tsmi_ul_l1_ls
            // 
            this.tsmi_ul_l1_ls.Name = "tsmi_ul_l1_ls";
            this.tsmi_ul_l1_ls.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l1_ls.Text = "听说";
            this.tsmi_ul_l1_ls.Click += new System.EventHandler(this.tsmi_ul_l1_ls_Click);
            // 
            // tsmi_ul_l2_rw
            // 
            this.tsmi_ul_l2_rw.Name = "tsmi_ul_l2_rw";
            this.tsmi_ul_l2_rw.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l2_rw.Text = "读写";
            this.tsmi_ul_l2_rw.Click += new System.EventHandler(this.tsmi_ul_l2_rw_Click);
            // 
            // tsmi_ul_l2_ls
            // 
            this.tsmi_ul_l2_ls.Name = "tsmi_ul_l2_ls";
            this.tsmi_ul_l2_ls.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l2_ls.Text = "听说";
            this.tsmi_ul_l2_ls.Click += new System.EventHandler(this.tsmi_ul_l2_ls_Click);
            // 
            // tsmi_ul_l3_rw
            // 
            this.tsmi_ul_l3_rw.Name = "tsmi_ul_l3_rw";
            this.tsmi_ul_l3_rw.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l3_rw.Text = "读写";
            this.tsmi_ul_l3_rw.Click += new System.EventHandler(this.tsmi_ul_l3_rw_Click);
            // 
            // tsmi_ul_l3_ls
            // 
            this.tsmi_ul_l3_ls.Name = "tsmi_ul_l3_ls";
            this.tsmi_ul_l3_ls.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l3_ls.Text = "听说";
            this.tsmi_ul_l3_ls.Click += new System.EventHandler(this.tsmi_ul_l3_ls_Click);
            // 
            // tsmi_ul_l4_rw
            // 
            this.tsmi_ul_l4_rw.Name = "tsmi_ul_l4_rw";
            this.tsmi_ul_l4_rw.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l4_rw.Text = "读写";
            this.tsmi_ul_l4_rw.Click += new System.EventHandler(this.tsmi_ul_l4_rw_Click);
            // 
            // tsmi_ul_l4_ls
            // 
            this.tsmi_ul_l4_ls.Name = "tsmi_ul_l4_ls";
            this.tsmi_ul_l4_ls.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l4_ls.Text = "听说";
            this.tsmi_ul_l4_ls.Click += new System.EventHandler(this.tsmi_ul_l4_ls_Click);
            // 
            // tsmi_ul_l5_rw
            // 
            this.tsmi_ul_l5_rw.Name = "tsmi_ul_l5_rw";
            this.tsmi_ul_l5_rw.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l5_rw.Text = "读写";
            this.tsmi_ul_l5_rw.Click += new System.EventHandler(this.tsmi_ul_l5_rw_Click);
            // 
            // tsmi_ul_l5_ls
            // 
            this.tsmi_ul_l5_ls.Name = "tsmi_ul_l5_ls";
            this.tsmi_ul_l5_ls.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ul_l5_ls.Text = "听说";
            this.tsmi_ul_l5_ls.Click += new System.EventHandler(this.tsmi_ul_l5_ls_Click);
            // 
            // tsmi_pwup
            // 
            this.tsmi_pwup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_pwup_l1,
            this.tsmi_pwup_l2,
            this.tsmi_pwup_l3,
            this.tsmi_pwup_l4,
            this.tsmi_pwup_l5,
            this.tsmi_pwup_l6});
            this.tsmi_pwup.Name = "tsmi_pwup";
            this.tsmi_pwup.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup.Text = "POWER UP";
            // 
            // tsmi_pwup_l1
            // 
            this.tsmi_pwup_l1.Name = "tsmi_pwup_l1";
            this.tsmi_pwup_l1.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l1.Text = "Level1";
            this.tsmi_pwup_l1.Click += new System.EventHandler(this.tsmi_pwup_l1_Click);
            // 
            // tsmi_pwup_l2
            // 
            this.tsmi_pwup_l2.Name = "tsmi_pwup_l2";
            this.tsmi_pwup_l2.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l2.Text = "Level2";
            this.tsmi_pwup_l2.Click += new System.EventHandler(this.tsmi_pwup_l2_Click);
            // 
            // tsmi_pwup_l3
            // 
            this.tsmi_pwup_l3.Name = "tsmi_pwup_l3";
            this.tsmi_pwup_l3.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l3.Text = "Level3";
            this.tsmi_pwup_l3.Click += new System.EventHandler(this.tsmi_pwup_l3_Click);
            // 
            // tsmi_pwup_l4
            // 
            this.tsmi_pwup_l4.Name = "tsmi_pwup_l4";
            this.tsmi_pwup_l4.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l4.Text = "Level4";
            this.tsmi_pwup_l4.Click += new System.EventHandler(this.tsmi_pwup_l4_Click);
            // 
            // tsmi_pwup_l5
            // 
            this.tsmi_pwup_l5.Name = "tsmi_pwup_l5";
            this.tsmi_pwup_l5.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l5.Text = "Level5";
            this.tsmi_pwup_l5.Click += new System.EventHandler(this.tsmi_pwup_l5_Click);
            // 
            // tsmi_pwup_l6
            // 
            this.tsmi_pwup_l6.Name = "tsmi_pwup_l6";
            this.tsmi_pwup_l6.Size = new System.Drawing.Size(180, 22);
            this.tsmi_pwup_l6.Text = "Level6";
            this.tsmi_pwup_l6.Click += new System.EventHandler(this.tsmi_pwup_l6_Click);
            // 
            // TopMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ms_mainform);
            this.Name = "TopMenu";
            this.Size = new System.Drawing.Size(1184, 26);
            this.ms_mainform.ResumeLayout(false);
            this.ms_mainform.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms_mainform;
        private System.Windows.Forms.ToolStripMenuItem tsm_a;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_normal;
        private System.Windows.Forms.ToolStripMenuItem tsm_hk_common;
        private System.Windows.Forms.ToolStripMenuItem tsm_a_insur;
        private System.Windows.Forms.ToolStripMenuItem tsmi_datasync;
        private System.Windows.Forms.ToolStripMenuItem tsmi_market_datasync;
        private System.Windows.Forms.ToolStripMenuItem tsmi_commonlink;
        private System.Windows.Forms.ToolStripMenuItem tsmi_zdgqzycx;
        private System.Windows.Forms.ToolStripMenuItem tsmi_hsgt;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lbty;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ths;
        private System.Windows.Forms.ToolStripMenuItem tsmi_gdzjc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_generatezoniaxy;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ecdata;
        private System.Windows.Forms.ToolStripMenuItem tsmi_money_supply;
        private System.Windows.Forms.ToolStripMenuItem tsmi_bdi;
        private System.Windows.Forms.ToolStripMenuItem tsmi_invest_know;
        private System.Windows.Forms.ToolStripMenuItem tsmi_moat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_rulesforsuccess;
        private System.Windows.Forms.ToolStripMenuItem tsmi_makefriendwithtime;
        private System.Windows.Forms.ToolStripMenuItem tsmi_googlemap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_aizhihui;
        private System.Windows.Forms.ToolStripMenuItem tsmi_douban;
        private System.Windows.Forms.ToolStripMenuItem tsmi_unlock;
        private System.Windows.Forms.ToolStripMenuItem tsmi_unlock_media;
        private System.Windows.Forms.ToolStripMenuItem tsmi_unlock_book;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_basic;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_basic_literacy;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_basic_skills;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l1_rw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l1_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l2_rw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l2_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l3_rw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l3_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l4_rw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l4_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l5_rw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ul_l5_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pwup_l6;
    }
}
