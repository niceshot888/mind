using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class BizModelGeneration : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private BizModel _bizModelData = null;
        private string _tscode = string.Empty;
        private ToolTip tooltip;
        public BizModelGeneration(string ts_code)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
           

            WinFormHelper.InitChildWindowStyle(this);

            this.Text = "商业模式画布：一个商业模式描述的是一个组织创造、传递以及获得价值的基本原理";
            this._tscode = ts_code;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();

            tooltip = new ToolTip{
                AutoPopDelay = 30000,
                ShowAlways = true,
                //ToolTipTitle = "分拆商业模式",
                InitialDelay = 200,
                ReshowDelay = 200,
                UseAnimation = true
            };

            tooltip.SetToolTip(this.pbCs, 
                @"任何一个组织都会服务于一个或多个客户群体。
--细分客户群体的条件：
*他们的需求催生了一项新的供给；
*需要建立一个新的分销渠道；
*他们产生的利润率显著不同；
*他们原以为某方面的特殊改进而买单。
--客户群体的划分：
*大众市场，常见于消费电子产业；
*小众市场，常见于供应商---采购商关系中，如很多汽车零部件
制造商依赖于来自主流汽车制造商的采购；
*求同存异的客户群体，如瑞士信贷集团、微型精密仪器公司等；
*多元化的客户群体，如亚马逊的云计算服务科同时满足
它们的零售商终端和新的云计算服务业务；
*多边平台（多边市场），如信用卡公司既需要一个基数庞大的持卡人群体，
又需要一个庞大的接受卡片的商家群体；
 又如一家提供免费报纸的企业，一方面需要一个庞大的读者
群体来吸引广告商家，另一方面也需要广告商家来为报纸的生产和分销买单。
");
            tooltip.SetToolTip(this.pbVp, @"一个组织的价值主张在于解决客户的问题和满足客户的需求。
价值主张就是一家公司为客户提供的利益的集合或组合；
一份对有益于客户价值创造的因素的不完全罗列：
*创新：满足的是客户之前未曾察觉的全新的需求；
*性能：改进产品或服务的性能；
*定制：针对某些客户或客户群体的某项需求提供定制的产品或服务
能够创造价值；
*保姆式服务：简单地帮客户完成任务； 
*设计：如时尚产业和消费电子产业；
*品牌/地位：客户可以简单地通过使用和展示某一品牌而获得价值，
如劳力士手表；
*价格：以更低的价格提供相同的价值是满足价格敏感性客户群体的
需求的普遍方式，如西南航空公司、印度塔塔集团生产的新车、
免费的新闻、免费的电子邮箱服务等；
*缩减成本：帮助客户节约成本，如salesforce提供CRM软件；
*风险控制：为客户购买的产品或服务降低风险；
*可获得性：帮助客户获得之前他们无法获得的产品或服务；
*便利性/实用性：让产品使用起来更方便或操作起来更简单。");
            tooltip.SetToolTip(this.pbCh,
    @"价值主张通过沟通、分发以及销售渠道传递给客户。
--渠道类型分为：
*直接：销售人员、网络销售；
*间接：自有商铺、合作方商铺、批发商；
--渠道阶段：
1.知名度：扩大公司产品和服务的知名度；
2.评价：帮助客户评价我们的价值主张；
3.购买：客户如何能够购买到我们的某项产品和服务；
4.传递：向客户传递我们的价值主张；
5.售后：向客户提供售后支持。");
            tooltip.SetToolTip(this.pbCr,
    @"客户关系以客户群体为单位建立和维护。
--客户关系可能是由以下动机驱动的：
*开发新的客户；
*留住原有客户；
*增加销售量（或单价）。
--客户关系类型：
*私人服务：客户可以与客户代表进行交流并在销售过程中以及
购买完成之后获得相应的帮助；
*专属私人服务：要求为每一个客户指定一个固定的客户经理，如私人银行服务，
为高净值客户指定专门的银行经理；
*自助服务：企业只需为客户提供一切自助服务所需要的渠道，
无需直接维护与客户的关系；
*自动化服务：将相对复杂的客户自助服务形式与自动化流程结合；
*社区：使用用户社区来融入客户以预判市场未来发展的方向，
促进社区中成员之间的联系；
*与客户协作，共同创造：超越传统的买卖关系，与客户合作共同
创造价值，如亚马逊邀请其客户撰写书评。
");
            tooltip.SetToolTip(this.pbRevenue, 
                @"收入来源于将价值主张成功地提供给客户。
--创造收入来源的方式：
*资产销售：实物产品所有权的出售；
*使用费：对某种具体服务的使用而产生的；
对该服务使用德越多，消费者支付的就越多；
*会员费：通过向用户销售某项服务持续的使用权限；
*租赁：将某一特定资产在某一个时期专门供给某人使用并收取一定费用；
*使用许可费：向用户授予某种受保护知识产权的使用权，并向其收取许可收用费；
*经纪人佣金：向双方或多方提供的中介服务，如信用卡发卡机构、房产中介等；
*广告费：为某种产品、服务或品牌做广告的费用，如传统的传媒业和活动策划、
软件业和服务业。
");

            tooltip.SetToolTip(this.pbDingJia, 
                @"主要的定价机制有两种：固定价格和浮动价格。
1.固定价格（基于静态变量预定的价格）：
*目录价：对个别产品、服务或其他价值主张设定的固定价格；
*基于产品特性的：基于某项价值主张的数量或质量的定价；
*基于客户群的：基于某一客户群体的类型和特征的定价；
*基于数量的：基于购买数量的定价。
2.浮动价格（价格依据市场条件变化）：
*谈判/议价：双方或多方的价格谈判，取决于谈判各方的谈判能力或技巧；
*收益管理：价格取决于库存及发生购买的时间（通常用于易耗资源，如宾馆房间和航班机位）；
*实时市场价格：价格根据需求变化动态变化；
*拍卖：根据竞价的结果决定。");

            tooltip.SetToolTip(this.pbKr, 
                @"核心资源是指为实现上述各项元素的供给和交付而必需的资源。
核心资源的分类：
*实物资源：如生产设备、房屋、车辆、机器、系统、销售点管理系统及分销渠道；
*知识性资源：如品牌、专营权、专利权、版权、合作关系及客户数据库等；
*人力资源：在知识密集型产业和创新产业中，人力资源是最关键的，如诺华制药
公司依赖其人力资源；
*金融资源：有些商业模式依赖金融资源或金融保障，比如现金、信用额度或用于
吸引关键雇员的股票期权池；");

            tooltip.SetToolTip(this.pbKa, 
                @"为实现供给和交付所需完成的关键业务活动。
*生产：涉及以较大的数量或上乘的质量，设计、制造以及分销产品，如制造业。
*解决方案：涉及为个体客户的问题提供新的解决方案，如咨询公司、医院及
其他服务性机构。这类商业模式需要的活动包括知识管理以及持续的培训等；
*平台/网络：网络、配对平台、软件甚至品牌都可以发挥平台的作用。
");

            tooltip.SetToolTip(this.pbKp, 
                @"部分活动需要外包，部分资源需要从其它企业获得。
*重要合作分为四种不同的类型：
1.非竞争者之间的战略联盟；
2.合作：竞争者之间的战略合作；
3.为新业务建立合资公司；
4.为保证可靠的供应而建立的供应商和采购商关系。
*区分建立合作伙伴关系的三种动机：
1.优化及规模效应：以优化资源及活动的配置为目的；
一家公司拥有全部所需的资源并亲自完成所有的生产、
服务环节并不合理；此类合作关系通常是为了降低成本，
主要采取外包及基础设施共享的形式；
2.降低风险和不确定性：互为竞争对手的企业在某一个
领域建立战略联盟，而在其他领域保持竞争关系是很常见的；
3.特殊资源及活动的获得：通过依赖其他占有某项资源或
专注于某种生产活动的公司来实现其能力的拓展；这种类型
的合作关系的动机在于获得知识、获得某种资质或接近某个客户群体，
如华为手机需要搭载安卓系统。
");
            tooltip.SetToolTip(this.pbCost, 
                @"成本结构取决于经济模式中的各项元素。
创造和传递价值，维护客户关系，以及创造收益都会发生成本。
--将商业模式的成本结构宽泛地分为两个等级：
1.成本导向型：聚焦于最大限度地将成本最小化；
采取的是低价的价值主张、自动化生产最大化以及广泛地业务外包，如西南航空；
2.价值导向型：更多地关注价值创造。通常表现为更高端的价值主张以及高度的
个性化服务，如豪华酒店。
--成本结构的特点：
*固定成本：不因产品及服务的产量而改变的成本，包括员工工资、租金、生产设备等。
有的生产型企业，以高比例的固定成本为特点。
*可变成本：随着产品及服务的产量而同比例变化的成本，比如音乐节；
*规模经济：企业的产出扩大，会带来成本优势。如大型企业享有大宗商品采购价。
*范围经济：企业的经营范围扩大，会带来成本优势。如在大型企业中，同一个营销
活动或分销渠道上可以供多个产品使用。
");

            tooltip.SetToolTip(this.pb_m_fenchai,
                @"分拆商业模式的三种核心商业类型：
1.新产品开发
*经济规则：早期市场进入获得高溢价和大量市场份额；速度是关键；
*竞争规则：能力之争，进入门槛低；大量小玩家争奇斗艳；
*以雇员为中心：呵护创意明星；
2.客户关系管理
*经济规则：高昂的客户开发成本要求从每个客户手中获取高份额；范围经济是关键；
*竞争规则：少量的大玩家主导市场；
*以雇员为中心：高度服务导向；顾客第一心态；
3.基础设施管理
*经济规则：高固定成本使得高产量成为获得低单位成本的关键；规模经济是关键；
*竞争规则：迅速固化的市场；少量大玩家主导市场；
*以雇员为中心：强调标准化、可预期性和生产效率。
");

            tooltip.SetToolTip(this.pb_m_longtail,
                @"长尾商业模式：
*客户细分：聚焦于小众客户；创造了一个同时服务于用户和生产者的多边平台；
*客户通道：通常依赖互联网来维护客户关系或作为交易渠道；
*价值主张：提供宽范围的非热销品，这些产品与热销品共存；
     长尾商业模式可能也会建立在用户创造产品上，同时促进这些产品的开发；
*重要合作：小众产品提供者为重要合作伙伴；
*核心资源：核心资源是平台；关键业务包括平台开发和维护，以及小众产品的获得与生产；
*成本结构：主要发生于平台开发和维护；
*收入来源：基于汇总从大规模的品类中获得的小规模收益；
     收入来源五花八门，它们可能来自广告、产品销售或订阅费。
");

            tooltip.SetToolTip(this.pb_m_muiltplantform,
                @"多边平台商业模式：
多边平台：将两个或更多独立但相互依存的客户群体连接起来的平台；
网络效应：一个多边平台的价值提升在于它所吸引的用户数量的增加；
*客户细分：有两个或更多的客户细分群体，每一个都有各自的价值主张和收益流，
而且，这些客户群体之间相互依存，哪一个也无法独立存在；
*客户通道：吸引用户群体；将客户群体进行配对；通过平台提供的交易渠道降低交易成本；
*核心资源：平台；关键业务：平台管理、服务实现以及平台升级；
*成本结构：平台的维护和开发、商业补贴；
*收入来源：每个客户群体产生一个收益流；一个或更多的群体可享受免费服务，
或者享受来自另一个客户群体的收益流产生的折价补贴；选择对的客户群体作为补贴对象是
一个关键的定价决策，这决定着该多边平台的商业模式成功与否；
");

            tooltip.SetToolTip(this.pb_m_free,
                @"免费商业模式：
分为三种类型：
1.基于多边平台的免费商品（基于广告），如Metro地铁广告；
2.免费的基本服务，可选的增值服务（“免费增值”模式），如有道笔记；
3.钓鱼模式，以一个免费或很便宜的初始价格吸引客户，并引诱客户
使其进入重复购买的状态，如吉利刀片、爱普生墨盒；
*共同点：至少有一个客户群体会持续获得免费的商品；
常用计算公式：
*收入=(用户数量*增值用户百分比*增值服务的价格)*增长率*顾客流失率；
*服务成本=(用户数量*免费用户百分比*免费用户服务成本)
+(用户数量*增值用户百分比%*增值用户服务成本)；
*运营利润=收入-服务成本-固定成本-客户获取成本；
");

            tooltip.SetToolTip(this.pb_m_open,
                @"开放商业模式：
背景：研发资源和关键活动都聚焦于企业内部：
*理念全数来自“内部”
*成果全数用于“内部”
挑战：研发活动成本高，且效率降低；
解决方案：内部研发资源和活动因得到外部合作者的使用而被激活；
内部研发成果转换为价值主张并提供给感兴趣的客户群体；
理论依据：从外部渠道获得研发成果可能成本更低，并缩短产品上市的时间；
将未利用的创新成果向外部销售，从而可能带来更多的收益。
案例：如宝洁、葛兰素史克等。
");
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindBizModelData();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_bizModelData != null)
            {
                tbxCs.Text = _bizModelData.bm_customer_segmentation;
                tbxCr.Text = _bizModelData.bm_customer_relationship;
                tbxCh.Text = _bizModelData.bm_channel_pathway;
                tbxVp.Text = _bizModelData.bm_value_proposition;
                tbxKa.Text = _bizModelData.bm_key_activities;
                tbxKr.Text = _bizModelData.bm_key_resources;
                tbxKp.Text = _bizModelData.bm_key_partnerships;
                tbxCost.Text = _bizModelData.bm_cost;
                tbxRevenue.Text = _bizModelData.bm_revenue;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            StringBuilder sbUpdateFields = new StringBuilder();

            if (!string.IsNullOrEmpty(tbxCs.Text))
                sbUpdateFields.AppendFormat("bm_customer_segmentation='{0}',", tbxCs.Text);
            if (!string.IsNullOrEmpty(tbxCr.Text))
                sbUpdateFields.AppendFormat("bm_customer_relationship='{0}',", tbxCr.Text);
            if (!string.IsNullOrEmpty(tbxCh.Text))
                sbUpdateFields.AppendFormat("bm_channel_pathway='{0}',", tbxCh.Text);
            if (!string.IsNullOrEmpty(tbxVp.Text))
                sbUpdateFields.AppendFormat("bm_value_proposition='{0}',", tbxVp.Text);
            if (!string.IsNullOrEmpty(tbxKa.Text))
                sbUpdateFields.AppendFormat("bm_key_activities='{0}',", tbxKa.Text);
            if (!string.IsNullOrEmpty(tbxKr.Text))
                sbUpdateFields.AppendFormat("bm_key_resources='{0}',", tbxKr.Text);
            if (!string.IsNullOrEmpty(tbxKp.Text))
                sbUpdateFields.AppendFormat("bm_key_partnerships='{0}',", tbxKp.Text);
            if (!string.IsNullOrEmpty(tbxCost.Text))
                sbUpdateFields.AppendFormat("bm_cost='{0}',", tbxCost.Text);
            if (!string.IsNullOrEmpty(tbxRevenue.Text))
                sbUpdateFields.AppendFormat("bm_revenue='{0}',", tbxRevenue.Text);

            if (string.IsNullOrEmpty(sbUpdateFields.ToString()))
            {
                MessageBox.Show("请录入数据");
                return;
            }

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sqlUpdate = "update xq_stock set {1} where ts_code='{0}'";
                sqlUpdate = string.Format(sqlUpdate, _tscode, sbUpdateFields.ToString().TrimEnd(','));
                conn.Execute(sqlUpdate);
                MessageBox.Show("数据保存成功");
            }
        }

        private void BindBizModelData()
        {
            string sql = "select ts_code,bm_customer_segmentation,bm_customer_relationship,bm_channel_pathway,bm_value_proposition,bm_key_activities,bm_key_resources,bm_key_partnerships,bm_cost,bm_revenue from xq_stock where ts_code='{0}'";
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _tscode);
                _bizModelData = conn.Query<BizModel>(sql).FirstOrDefault();
            }
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //Pen pp = new Pen(Color.FromArgb(176 ,226, 255));
            //e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width-1, e.CellBounds.Y + e.CellBounds.Height - 2);
        }

        private void tableLayoutPanel2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //Pen pp = new Pen(Color.FromArgb(250, 250, 250));
            //e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 2);
        }

        private void tableLayoutPanel3_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            Pen pp = new Pen(Color.FromArgb(250, 250, 250));
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 2, e.CellBounds.Y + e.CellBounds.Height-2);
        }

        private void tableLayoutPanel4_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            Pen pp = new Pen(Color.FromArgb(250, 250, 250));
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 2, e.CellBounds.Y + e.CellBounds.Height - 3);
        }
    }
}
