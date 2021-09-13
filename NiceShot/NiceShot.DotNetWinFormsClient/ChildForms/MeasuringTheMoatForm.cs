using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.DotNetWinFormsClient.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class MeasuringTheMoatForm : Form
    {

        public BackgroundWorker _backgroundWorker = null;
        private measuring_the_moat _measuring_the_moat;
        private string _ts_code;
        private ToolTip tooltip;
        public MeasuringTheMoatForm(string ts_code)
        {
            InitializeComponent();

            this._ts_code = ts_code;

            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();

            tooltip = new ToolTip
            {
                AutoPopDelay = 30000,
                ShowAlways = true,
                //ToolTipTitle = "分拆商业模式",
                InitialDelay = 200,
                ReshowDelay = 200,
                UseAnimation = true
            };

            tooltip.SetToolTip(this.pb_comp_life_cycle_stage,
                @"通过观察一个公司的竞争生命周期，我们可以看到可持续的价值创造。公司通常会经历以下四个阶段：
*创新。年轻的公司通常会意识到投资回报的迅速增长和巨大的投资机会。在生命周期的这个阶段，大量（公司）进入和退出该行业非常常见。
*回报下降。高回报吸引竞争，通常导致经济回报向资本成本靠拢。在这个阶段，公司仍然赚取超额回报，但回报曲线是下降的。投资需求也在放缓，进入和退出的速度也在放缓。
*成熟。在这个阶段，公司竞争趋向于达到均衡状态。因此，公司获得的投资回报与行业平均水平相当，而行业内的竞争确保了总回报不会更高。投资需求继续放缓。
*低于标准。竞争力量和技术变革会使回报低于资本成本，这就要求公司进行重组。这些公司可以通过剥离资产、改变商业模式、降低投资水平或出售自己来提高回报。或者，这些公司可以申请破产重组或清算公司的资产。
");

            tooltip.SetToolTip(this.pb_five_forces,
                @"*供应商力量是指供应商在价格、质量和服务等方面与客户之间的影响力。一个无法将来自于强大供应商那里的涨价压力转嫁的行业，注定没有吸引力。如果供应商比他们的下游客户更为集中、如果替代产品不会产生影响、或者产品有很高的转换成本，那么供应商就处于有利地位。如果他们所服务的行业在销售额中所占比例相对较小，或者产品对客户而言至关重要，他们也处于有利地位。客户买家集中、数量很少要比客户分散更让供应商头疼。
*客户力量是指产品或服务的购买者的讨价还价能力。它是客户集中度、转换成本、信息水平、替代产品以及产品对客户重要性的总和。与不知情、分散的客户相比，信息灵通的大客户对他们的供应商有更大的影响力。
*替代威胁涉及替代产品或服务的存在，以及潜在客户转向替代产品的可能性。如果一个企业的价格不具竞争力，如果竞争对手有可比产品，企业就会面临替代威胁。替代产品限制了公司可以收取的价格，为潜在回报设置了上限。  
");

            tooltip.SetToolTip(this.pb_entry_problem,
                @"实证研究：
*在行业发展的早期阶段，市场对所青睐的产品不确定，这就鼓励了小型、灵活的企业进入行业并进行创新。随着行业的成熟，市场确定了产品，需求趋于稳定。老公司受益于规模经济和根深蒂固的优势，导致高退出率和走向稳定的寡头垄断。
*每个行业的退出和进入速度之间存在很强的相关性。例如，制造业的进入和退出速度较低，而建筑业的进入和退出速度非常高，说明制造业具有更强的进入和退出壁垒。
①退出和进入将无处不在。
②进入和退出的公司往往比现存公司的规模小。
③不同行业的进入和退出速度差别很大。
④大多数进入者无法生存10年，但那些成功活下来的进入者却欣欣向荣。
什么影响了挑战者进入的决定？
*从广义上讲，潜在进入者权衡现任者的预期反应、预期回报和退出成本的大小。研究人员还发现，挑战者忽视了企业失败的可能性，从而导致过度自信。
有四个具体因素可以预测现有企业的反应及程度：资产专用性、最低有效生产规模水平、过剩产能和在位声誉。
");

            tooltip.SetToolTip(this.pb_asset_special,
                @"资产专用性：
*长期以来，经济学家认为，公司对市场的承诺与该在资产上的投资有关。更仔细的分析表明，重要的不是资产的数量，而是这些资产对市场的专用程度。一家公司如果拥有只在特定市场上才有价值的资产，将会为了维持其地位而奋力拼搏。
*一个清楚的例子是铁路和航空线路的对比。假设一家公司建造了一条从纽约到芝加哥的铁路。它只能将这些资产用于一件事：让火车在这两个城市之间来回。因此，该公司将不遗余力地保护自己的地位。现在考虑一家从纽约飞往芝加哥的航空公司。如果这条航线被证明不划算，航空公司可以将飞机改道至一个更有吸引力的目的地。
*资产专用性有多种形式，包括场地专用性，即公司的资产在客户旁边以提高效率；物理专用性，即公司根据特定交易定制资产；专用资产，即公司收购资产以满足特定买家的需要；以及人的特殊性，即公司在这种情况下培养员工的技能、知识或专业方法。
");

            tooltip.SetToolTip(this.pb_min_pro_scale,
                @"生产规模：
*对许多行业来说，单位成本随着产量的增加而下降，但这只会在一定程度上发生。这对于固定成本高的行业尤其重要。当单位成本随着产量的增加而下降时，企业就享有规模经济。然而，在某个时候单位成本会随着产量的增加而停止下降，企业会获得恒定的规模回报。最小有效生产规模是企业为了使单位成本最小化而必须生产的最小数量。
*最低有效生产规模告诉潜在的进入者，它必须获得多少市场份额，才能使其商品具有竞争力的价格并获得利润。它还表明了进入者的前期资本投入的规模。当最低有效生产规模相对于整个市场的规模来说是很高的时候，潜在的进入者必须忍受在一段时间内将其产品定价低于平均成本以达到规模。成本曲线下降越陡，进入的可能性就越小。进入者试图抵消其生产成本劣势的主要方式是使其产品差异化，从而使其相对于行业内其他产品获得溢价。
*最低有效规模通常与制造业相关，包括汽车和半导体制造厂。例如，英特尔生产第一台Xeon微处理器的成本超过100亿美元，包括制造厂和相关研发。但一旦芯片设计好，晶圆厂开始运转，生产芯片的成本就大幅下降。
 最小有效规模的概念也适用于知识型企业，即一家公司以非常高的成本创建内容，然后将其复制到市场上。软件和硬件的成本曲线是一样的，在大多数情况下甚至更陡峭。
");

            tooltip.SetToolTip(this.pb_reputation,
                @"企业声誉：
*随着时间的推移，企业通常会在不同的市场竞争。因此，他们获得了“随时准备为最小的挑衅而战”或“乐于助人”的名声。一个公司的声誉，背后有对应的行动，可以影响进入者的决定。
");

            tooltip.SetToolTip(this.pb_excess_capacity,
                @"产能过剩：
*假设需求保持稳定，进入产能过剩行业的进入者会增加现有企业的过剩产能。如果该行业在生产上具有规模经济，那么现有企业的闲置产能成本就会上升。因此，现有企业有动力保持其市场份额。因此，新进入者的将引发价格下跌。这种前景阻碍了进入。
");

            tooltip.SetToolTip(this.pb_antici_payoff_for_new_entrant,
                @"进入者预期收益的大小：
*如果现有企业拥有不可克服的优势，那么进入者就不能确定自己能获得有吸引力的经济利润。现有企业的优势包括预先承诺合同、许可和专利、学习曲线和网络效应。
");
            tooltip.SetToolTip(this.pb_precommit_contracts,
                @"预先承诺合同：
*第一个优势是预先承诺合同。通常，公司通过签订长期合同来确保未来的业务。这些合同可以有效地降低供应商和客户的搜索成本。一个有合约的强势现有企业会阻碍进入。
*预先承诺合同有多种形式。一个是现有企业是否能获得必要的原材料。第二次世界大战结束后不久就出现了这样一个例子。铝生产商美国铝业(Alcoa)与生产铝的一种重要原料——高品位铝土矿的所有生产商签订了独家合同。潜在进入者无法以如此优惠的条件获得铝土矿，使得进入这一行业变得非常困难。
*另一种形式的预先承诺合同是与客户的长期交易。在20世纪80年代中期，孟山都(NutraSweet)和荷兰甜味剂公司是阿斯巴甜（甜味剂）的生产商。1987年阿斯巴甜的专利在欧洲到期后，荷兰甜味剂公司进入市场与孟山都展开竞争。竞争使阿斯巴甜的价格下降了60%，荷兰甜味剂公司赔钱了。
*但荷兰甜味剂公司把目光投向了真正的战利品——美国市场，这项专利将于1992年在美国到期。在一个典型的预先承诺行动中，孟山都与阿斯巴甜的最大买家可口可乐和百事可乐签署了长期合同，负责供应阿斯巴甜，并有效地将荷兰甜味剂公司排除在美国市场之外。这给企业和投资者上了重要的一课：所有买家都希望拥有多个供应商，但这并不意味着他们会使用多个供应商。荷兰甜味剂公司为可口可乐和百事可乐创造了巨大的价值，却没有为自己创造价值。
*预先承诺还包括准合同，例如承诺总是以最低的成本提供商品或服务。这样的承诺，如果可以执行，就会阻碍新进入者进入，因为新进入者很少有足够的规模与现有企业竞争。
");
            tooltip.SetToolTip(this.pb_licenses_or_patents,
                @"许可和专利：
*许可和专利也会影响潜在进入者的回报，这是有道理的。许多行业需要政府的许可或证书才能开展业务。获取许可或证书的成本很高，因此为进入者设置了障碍。
*专利也是一个重要的进入壁垒。但是专利与许可的内核是不同的。专利的目的是让创新者获得适当的投资回报。大多数创新都需要大量的前期成本。因此，一个自由市场体系需要一种补偿创新者的方法来鼓励他们的创新活动。专利不会阻碍创新，但它们确实会在有限的时间内阻止人们进入受保护的专利所在的行业。
");
            tooltip.SetToolTip(this.pb_learning_curve_benefits,
                @"学习曲线：
*学习曲线也可以作为进入的障碍。学习曲线作为累积经验的函数是指降低单位成本的能力。研究人员已经研究了数百种产品的学习曲线。数据显示，对于中等规模的企业来说，累计产量翻番可使单位成本降低约20%。一家公司可以享受学习曲线带来的好处，而不必考虑规模经济，反之亦然。但一般来说，这两者是相辅相成的。
");

            tooltip.SetToolTip(this.pb_network_radial_or_interactive,
                @"网络效应：
*网络效应可能是消费者优势的重要来源，尤其是在基于信息的企业中。有两种类型的网络，一种是中心辐射型网络，包括航空公司和零售商。这种情况下网络效应存在，但是比较一般。
*第二种类型是交互式网络，其中节点在实体上（电话线）或虚拟地（同一软件）相互连接。网络效应对于交互式网络往往很重要，因为随着越来越多的人使用它，商品或服务变得越来越有价值。例如，由于Visa和MasterCard强大的网络效应，它们在支付系统市场上具有强大的优势。
*正面反馈对交互式网络来说很重要。如果多于一个交互式网络在争夺消费者，那么有先发优势的一个会有正面反馈效应，导致赢家。占主导地位的网络得益于拥有最多的用户和规模，并且随着网络的发展，其客户的转换本也随之增加。一个典型的例子就是微软的个人计算机操作系统业务。
*交互式网络的累积用户模式遵循S型曲线，类似于其他创新的传播，但是交互式网络的S型曲线更加陡峭。著名的社会学家埃弗里特·罗杰斯（EverettRogers）发现，技术或网络的新采用者的分布遵循正态分布。判断公司价值创造的来源和时间长度对于了解可持续价值创造的可能性至关重要。包括AOL，MySpace和Friendster在内的许多公司似乎已经建立了有价值的网络，但是我们却看到它们的价值缩水。
");

            tooltip.SetToolTip(this.pb_external_sources_added_value,
                @"外部因素：
*最后一个价值创造的来源的来自外部的，或者政府相关的，包括补贴、关税、配额，还有竞争法规和环境法规。政府政策的变化会对价值创造产生重大影响，比如放松管制对航空业和卡车业的影响、巴塞尔III法例对金融业的影响、关税对光伏行业的影响等等。
");
            
            tooltip.SetToolTip(this.pb_production_advantages,
                @"生产优势：
*具有生产优势的公司在消费者感知的获益（付费的意愿）和生产成本（机会成本）之间的差异要大于竞争对手，从而创造价值，这主要是通过在成本方面超越竞争对手。我们将生产优势分为两部分：流程和规模经济。
");
            tooltip.SetToolTip(this.pb_how_quick_process_costs_chg,
                @"流程优势体现在：
*不可分割性：规模效应固定成本高的企业特别相关。固定成本的一个重要决定因素是生产过程中的不可分割性。不可分割性意味着即使产量很低，公司也无法将其生产成本降至最低水平以下。面包店就是一个例子。如果烘焙店想要服务一个地区，它必须具备烘焙工具、货车和司机。这些部分是不可分割的，无论面包的需求如何，企业都必须承担成本。同时，如果卡车从半空变成全满，固定成本也不会有太大变化。
*复杂性：简单的流程易于模仿，并且不太可能成为优势的来源。相反，更复杂的流程需要更多的专业知识或协调能力，并且可以成为优势的来源。例如，宝洁花了8年和几亿美金去开发汰渍洗衣球。很多的花销去到了研发人员、测试客户以及数百个包装和产品草图。政府给宝洁授予了有关洗涤剂化学、洗衣球外包装和制造工艺的多项专利。鲍勃·麦克唐纳，宝洁当时的CEO，对这个知识产权和专利的信心：“不认为任何汰渍洗衣球的复制品会构成任何威胁。”
*流程成本变化率：对一些行业来说，生产成本会随着科技发展而降低。建立电子商务公司的过程相关成本比过去要少得多，因为你可以直接购买大多数必要的部件。但是未来的成本也会由于同样原因比现在低得多。对于流程成本下降的行业，现有玩家具有学习曲线的优势，新进玩家会有成本优势。因此，分析必须着重于学习优势与未来成本优势之间的权衡。
*保护：寻找保护公司流程的专利、版权、商标和经营权。研究表明，具有专利保护的产品整体上比任何单个行业产生更高的经济回报。
*资源独特性：美铝的铝土矿合同很好地说明了获得独特资源的可能性。
");
            
             tooltip.SetToolTip(this.pb_economies_scale,
                @"规模经济是潜在生产优势的第二类：
*一个公司创造的价值是营业收入和成本（包括机会成本）之间的差，公司可以通过提高售价或者降低成本来创造更大的价值。有证据表明，更多的价值创造来自于客户支付意愿的差异，而不是跟竞争对手的成本差异。
*当制造公司增加其产出，它的边际成本和平均单位成本会下降到一个点，这是经典的规模收益递增。因为公司从供给端的积极反馈中受益，一切都是为了降低成本。但是这个积极的反馈到某一个点之后会消失，因为官僚主义、复杂性、输入稀缺性等原因。通常，这种情况发生在主导地位之前的水平：工业界的市场份额很少超过50％。需求端的积极反馈主要来自网络效应。
判断一个公司是否有供给端规模优势的几个方面：
*渠道：判断公司是否有本地、区域、全国的销售渠道。非常少的公司有全国性的销售渠道，一个例子是零售。沃尔玛在20世纪70-80年代建立起区域性的渠道优势。大多数的零售商只有区域性的渠道优势，很难在他们核心市场外获得显著经济利益。
*一个判断渠道优势的方法是在地图上看一个公司的营收和运营，一个公司倾向于在资产和营收密集的地方有优势。
*采购：一些公司因为规模优势以更低的价格采购原材料。在1990年代后期，家得宝通过产品线审核的和增加进口产品的采购量，降低商品采购成本，使其毛利率增加了2个百分比。家得宝通过其规模从供应商那里获得最佳的成本。大公司通过给供应商提供需求的信息，降低供应商的机会成本。
*研发：当公司降低单位成本时，就存在与规模经济相关的范围经济。比如研发溢出，一个项目的研发成果可以应用到其他项目中。辉瑞寻找一种可以治疗高血压的药物，然后以为它可以治疗心绞痛，然后发现一种不寻常的副作用，从而发现了重磅炸弹药物伟哥。拥有丰富研发管线的公司更容易找到为他们的产品找到应用领域。
*广告：每位消费者的广告成本是发送消息和覆盖范围的成本的函数。如果广告的固定成本（包括广告准备和与广播公司的谈判）对于大小公司大致相同，则大公司在每个潜在消费者的成本上具有优势，因为它们可以将成本分散到更大的基础上。
");

            tooltip.SetToolTip(this.pb_consumer_advantages,
                @"消费者优势是价值创造的第二个来源。有消费者优势的公司通过客户认知价值与成本之间的差比竞争对手大来创造价值。他们通常在收益端来超越竞争对手。
有消费者价值的公司的特征：
*习惯和高“水平差异”：当某些消费者喜欢某产品而不是其竞争产品时，该产品就有了“水平差异”。这个产品不一定比竞争对手显著要好，但有一些消费者觉得特别吸引的特点。软饮为例，与可口可乐竞争特别困难，很多消费者就是习惯性并且长期喝可口可乐。
*体验型商品：体验型商品就是顾客只有在试过之后才可以评价的产品。搜索型产品是消费者在购买的时候就可以评价的产品。体验型产品通常科技含量更高，公司可以享受基于形象、声誉或信誉的差异化。
*转换成本和客户锁定：消费者要承担从一个产品转换到另一个产品的成本，转换成本的大小决定了顾客的锁定程度。有时转换成本很高且显而易见（比如一家公司更换网络需要1亿美元），有时成本却很小却很可观（比如每100万客户转换保险提供商，每位客户100美元）。
*具有高转换成本的产品的一个例子企业资源计划（ERP）系统。除了高昂的初始成本外，实施新ERP系统的公司还必须花费大量内部资源来进行用户培训和IT支持等工作。而且，由于公司必须根据其业务流程定制ERP系统，因此更换供应商的成本甚至更高。
*网络效应。网络效应可能是消费者优势的重要来源，尤其是在基于信息的企业中。有两种类型的网络，一种是中心辐射型网络，第二种类型是交互式网络。
");
            tooltip.SetToolTip(this.pb_good_company,
                @"*行为科学教授Jerker Denrell为认真对待战略评估的人提供了两个关键看法，一是幸存者偏见，仅通过对过去的赢家进行抽样，无法回答一个关键问题：实施某种特定策略的公司中有多少实际上成功了？
*Denrell的第二个看法是，我们可能很难从成功的结果中学习。卓越的公司绩效通常是运气累积的结果，也就是说，如果时间倒流并重新开始，相同的公司不会每次都成功。由于一些成功的公司靠运气获得成功，因此可以向他们学习到的东西很少。实际上，那些在成功累积不太明显的行业中竞争胜出、具有良好财务业绩的公司，会是更好的学习样本。
*通过确定真正优秀公司的样本，作者能够研究其绩效优势背后的行为。他们在查看特定行为时找不到共同点，但在研究这些公司的总体思维方式时取得了突破。这种方式是高度一致的，并且符合通用的差异化策略。雷诺和艾哈迈德认为，在考虑业务决策时，成功的公司的行为遵循两个基本规则：
1. 产品高于价格：在差异化产品上竞争，而不是在价格上竞争。
2. 收入高于成本：优先考虑增加收入而不是降低成本。
*基于此分析，他们为管理者提出了两个建议。首先是要清楚地了解公司的竞争地位和利润构成（资产收益率=销售收益率x总资产周转率）。公司经常将当前的财务业绩与过去进行比较，而不是与竞争对手进行比较。商业是相对的，而不是绝对性的游戏。
*第二步是使资源分配决策与规则一致。当在面临决策时，是提供低价和最低标准的产品和服务，还是高价和优越的收益（例如强大的品牌或更大的便利性），高管应选择后者。或者公司应该选择能够实现扩展机会的合并，而不是仅仅实现规模经济的合并。
");
            

            tooltip.SetToolTip(this.pb_val_source,
                @"三大价值创造的来源：生产优势、消费者优势和外部优势。
（1）生产优势分为两部分：流程和规模经济；
流程优势体现在：
*不可分割性：规模效应固定成本高的企业特别相关。固定成本的一个重要决定因素是生产过程中的不可分割性。不可分割性意味着即使产量很低，公司也无法将其生产成本降至最低水平以下。
*复杂性：简单的流程易于模仿，并且不太可能成为优势的来源。相反，更复杂的流程需要更多的专业知识或协调能力，并且可以成为优势的来源。
*流程成本变化率：对一些行业来说，生产成本会随着科技发展而降低。对于流程成本下降的行业，现有玩家具有学习曲线的优势，新进玩家会有成本优势。因此，分析必须着重于学习优势与未来成本优势之间的权衡。
*保护：寻找保护公司流程的专利、版权、商标和经营权。研究表明，具有专利保护的产品整体上比任何单个行业产生更高的经济回报。
*资源独特性：美铝的铝土矿合同很好地说明了获得独特资源的可能性。
规模经济优势：
一个公司创造的价值是营业收入和成本（包括机会成本）之间的差，公司可以通过提高售价或者降低成本来创造更大的价值。有证据表明，更多的价值创造来自于客户支付意愿的差异，而不是跟竞争对手的成本差异。
判断一个公司是否有供给端规模优势的几个方面：
*渠道：判断公司是否有本地、区域、全国的销售渠道。非常少的公司有全国性的销售渠道，一个例子是零售。
*采购：一些公司因为规模优势以更低的价格采购原材料。
*研发：当公司降低单位成本时，就存在与规模经济相关的范围经济。比如研发溢出，一个项目的研发成果可以应用到其他项目中。
*广告：每位消费者的广告成本是发送消息和覆盖范围的成本的函数。如果广告的固定成本（包括广告准备和与广播公司的谈判）对于大小公司大致相同，则大公司在每个潜在消费者的成本上具有优势，因为它们可以将成本分散到更大的基础上。
");

            ShowPictureBoxTooltip();

        }

        private void ShowPictureBoxTooltip()
        {
            var children = this.tc_main.TabPages;
            foreach (TabPage child in children)
            {
                var subChildren = child.Controls;
                foreach (Control subChild in subChildren)
                {
                    if(subChild is PictureBox)
                        subChild.DoubleClick += PictureBoxClick;

                    var thirdChildren = subChild.Controls;

                    foreach (Control thirdchild in thirdChildren)
                    {
                        if (!(thirdchild is TabPage))
                            continue;

                        var forthChildren = thirdchild.Controls;
                        foreach (Control forthChild in forthChildren)
                        {
                            if (forthChild is PictureBox)
                                forthChild.DoubleClick += PictureBoxClick;
                        }
                    }

                }
            }
        }

        private void PictureBoxClick(object sender, EventArgs e)
        {
            var content = tooltip.GetToolTip((Control)sender);
            MessageBox.Show(content);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindMeasuringTheMoatData();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BindMeasuringTheMoatData();

            if (_measuring_the_moat == null)
            {
                InsertMeasuringTheMoatData();
                return;
            }

            //第一部分：概况
            cbx_competitive_life_cycle_stage.SelectedText = _measuring_the_moat.competitive_life_cycle_stage;
            cb_is_return_above_cost.Checked = _measuring_the_moat.is_return_above_cost.GetValueOrDefault();
            cbx_roic_status.SelectedText = _measuring_the_moat.roic_status;
            tbx_roic_status_remarks.Text = _measuring_the_moat.roic_status_remarks;
            tbx_invest_spending_trend.Text = _measuring_the_moat.invest_spending_trend;

            //第二部分
            //2.1了解情况
            tbx_each_player_represent_percent.Text = _measuring_the_moat.each_player_represent_percent;
            tbx_each_player_profit_level.Text = _measuring_the_moat.each_player_profit_level;
            tbx_market_share_historical_trends.Text = _measuring_the_moat.market_share_historical_trends;
            tbx_market_share_how_stable.Text = _measuring_the_moat.market_share_how_stable;
            tbx_pricing_trends.Text = _measuring_the_moat.pricing_trends;
            tbx_industry_class.Text = _measuring_the_moat.industry_class;
            //2.2五力模型的前三个
            tbx_suppliers_leverage.Text = _measuring_the_moat.suppliers_leverage;
            cb_pass_supplier_incto_customers.Checked = _measuring_the_moat.pass_supplier_incto_customers.GetValueOrDefault();
            cb_substi_prod_available.Checked = _measuring_the_moat.substi_prod_available.GetValueOrDefault();
            cb_have_switching_costs.Checked = _measuring_the_moat.have_switching_costs.GetValueOrDefault();
            tbx_buyers_leverage.Text = _measuring_the_moat.buyers_leverage;
            tbx_buyers_informed.Text = _measuring_the_moat.buyers_informed;
            //2.3进入的障碍
            tbx_entry_and_exit_rates.Text = _measuring_the_moat.entry_and_exit_rates;
            tbx_reactions_newentrants.Text = _measuring_the_moat.reactions_newentrants;
            tbx_reputation.Text = _measuring_the_moat.reputation;
            tbx_asset_specificity.Text = _measuring_the_moat.asset_specificity;
            tbx_mini_effic_prod_scale.Text = _measuring_the_moat.mini_effic_prod_scale;
            cb_excess_capacity.Checked = _measuring_the_moat.excess_capacity.GetValueOrDefault();
            cb_way_to_diff_prod.Checked = _measuring_the_moat.way_to_diff_prod.GetValueOrDefault();
            tbx_antici_payoff_for_new_entrant.Text = _measuring_the_moat.antici_payoff_for_new_entrant;
            cb_precommit_contracts.Checked = _measuring_the_moat.precommit_contracts.GetValueOrDefault();
            cb_licenses_or_patents.Checked = _measuring_the_moat.licenses_or_patents.GetValueOrDefault();
            tbx_learning_curve_benefits.Text = _measuring_the_moat.learning_curve_benefits;
            //2.4竞争对手
            tbx_pricing_coordination.Text = _measuring_the_moat.pricing_coordination;
            tbx_industry_concentration.Text = _measuring_the_moat.industry_concentration;
            tbx_size_distribution.Text = _measuring_the_moat.size_distribution;
            tbx_incent_corp_ownership_structure.Text = _measuring_the_moat.incent_corp_ownership_structure;
            cb_demand_variability.Checked = _measuring_the_moat.demand_variability.GetValueOrDefault();
            cb_high_fixed_costs.Checked = _measuring_the_moat.high_fixed_costs.GetValueOrDefault();
            cb_industry_growing.Checked = _measuring_the_moat.industry_growing.GetValueOrDefault();
            //2.5破坏和瓦解
            cb_vulnerable_disruptive_innovation.Checked = _measuring_the_moat.vulnerable_disruptive_innovation.GetValueOrDefault();
            cb_innovations_foster_prod_improve.Checked = _measuring_the_moat.innovations_foster_prod_improve.GetValueOrDefault();
            cb_inno_progre_faster_market_needs.Checked = _measuring_the_moat.inno_progre_faster_market_needs.GetValueOrDefault();
            cb_estab_players_passed_perform_threshold.Checked = _measuring_the_moat.estab_players_passed_perform_threshold.GetValueOrDefault();
            tbx_organized_type.Text = _measuring_the_moat.organized_type;
            //公司分析
            //3.1公司相关
            tbx_value_chain_diff_rivals.Text = _measuring_the_moat.value_chain_diff_rivals;
            cb_production_advantages.Checked = _measuring_the_moat.production_advantages.GetValueOrDefault();
            cb_biz_stuct_instable.Checked = _measuring_the_moat.biz_stuct_instable.GetValueOrDefault();
            cb_complex_co_cap.Checked = _measuring_the_moat.complex_co_cap.GetValueOrDefault();
            tbx_how_quick_process_costs_chg.Text = _measuring_the_moat.how_quick_process_costs_chg;
            tbx_patents_copyrights.Text = _measuring_the_moat.patents_copyrights;
            cb_economies_scale.Checked = _measuring_the_moat.economies_scale.GetValueOrDefault();
            tbx_distribution_scale.Text = _measuring_the_moat.distribution_scale;
            cb_ass_reve_clustered_geo.Checked = _measuring_the_moat.ass_reve_clustered_geo.GetValueOrDefault();
            cb_purchasing_advantages.Checked = _measuring_the_moat.purchasing_advantages.GetValueOrDefault();
            cb_economies_scope.Checked = _measuring_the_moat.economies_scope.GetValueOrDefault();
            cb_diverse_research_profiles.Checked = _measuring_the_moat.diverse_research_profiles.GetValueOrDefault();
            cb_consumer_advantages.Checked = _measuring_the_moat.consumer_advantages.GetValueOrDefault();
            cb_habit_horizontal_diff.Checked = _measuring_the_moat.habit_horizontal_diff.GetValueOrDefault();
            tbx_prefer_pro_or_competing_pro.Text = _measuring_the_moat.prefer_pro_or_competing_pro;
            cb_customers_weigh_lots_pro_attrs.Checked = _measuring_the_moat.customers_weigh_lots_pro_attrs.GetValueOrDefault();
            cb_customers_through_trial.Checked = _measuring_the_moat.customers_through_trial.GetValueOrDefault();
            tbx_customer_lockin_switching_costs.Text = _measuring_the_moat.customer_lockin_switching_costs;
            tbx_network_radial_or_interactive.Text = _measuring_the_moat.network_radial_or_interactive;
            tbx_source_longevity_added_value.Text = _measuring_the_moat.source_longevity_added_value;
            tbx_external_sources_added_value.Text = _measuring_the_moat.external_sources_added_value;

            //3.2企业互动
            tbx_industry_include_complementors.Text = _measuring_the_moat.industry_include_complementors;
            tbx_value_pie_growing.Text = _measuring_the_moat.value_pie_growing;

            //品牌
            cb_hire_brand_for_job.Checked = _measuring_the_moat.hire_brand_for_job.GetValueOrDefault();
            cb_brand_increase_will_to_pay.Checked = _measuring_the_moat.brand_increase_will_to_pay.GetValueOrDefault();
            cb_emotional_conn_brand.Checked = _measuring_the_moat.emotional_conn_brand.GetValueOrDefault();
            cb_trust_pro_cause_name.Checked = _measuring_the_moat.trust_pro_cause_name.GetValueOrDefault();
            cb_brand_imply_social_status.Checked = _measuring_the_moat.brand_imply_social_status.GetValueOrDefault();
            cb_reduce_supplier_opercost_with_name.Checked = _measuring_the_moat.reduce_supplier_opercost_with_name.GetValueOrDefault();
        }

        private void BindMeasuringTheMoatData()
        {
            string sql = "select * from measuring_the_moat where ts_code='{0}'";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _ts_code);
                _measuring_the_moat = conn.Query<measuring_the_moat>(sql).FirstOrDefault();
            }
        }

        private void InsertMeasuringTheMoatData()
        {
            string sql = "insert into measuring_the_moat(ts_code) values('{0}')";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _ts_code);
                conn.Execute(sql);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sbUpdateFields = new StringBuilder();

            //第一部分：概况
            var competitive_life_cycle_stage = cbx_competitive_life_cycle_stage.Text;
            if (!string.IsNullOrEmpty(competitive_life_cycle_stage))
                sbUpdateFields.AppendFormat("competitive_life_cycle_stage='{0}',", competitive_life_cycle_stage);
            
            var is_return_above_cost = cb_is_return_above_cost.Checked;
            sbUpdateFields.AppendFormat("is_return_above_cost={0},", is_return_above_cost ? 1 : 0);
            
            var roic_status = cbx_roic_status.Text;
            if (!string.IsNullOrEmpty(roic_status))
                sbUpdateFields.AppendFormat("roic_status='{0}',", roic_status);

            var roic_status_remarks = tbx_roic_status_remarks.Text;
            if (!string.IsNullOrEmpty(roic_status_remarks))
                sbUpdateFields.AppendFormat("roic_status_remarks='{0}',", roic_status_remarks);

            var invest_spending_trend = tbx_invest_spending_trend.Text;
            if (!string.IsNullOrEmpty(invest_spending_trend))
                sbUpdateFields.AppendFormat("invest_spending_trend='{0}',", invest_spending_trend);

            //第二部分
            //2.1了解情况
            var each_player_represent_percent = tbx_each_player_represent_percent.Text;
            if (!string.IsNullOrEmpty(each_player_represent_percent))
                sbUpdateFields.AppendFormat("each_player_represent_percent = '{0}',", each_player_represent_percent);
            var each_player_profit_level = tbx_each_player_profit_level.Text;
            if (!string.IsNullOrEmpty(each_player_profit_level))
                sbUpdateFields.AppendFormat("each_player_profit_level = '{0}',", each_player_profit_level);
            var market_share_historical_trends = tbx_market_share_historical_trends.Text;
            if (!string.IsNullOrEmpty(market_share_historical_trends))
                sbUpdateFields.AppendFormat("market_share_historical_trends = '{0}',", market_share_historical_trends);
            var market_share_how_stable = tbx_market_share_how_stable.Text;
            if (!string.IsNullOrEmpty(market_share_how_stable))
                sbUpdateFields.AppendFormat("market_share_how_stable = '{0}',", market_share_how_stable);
            var pricing_trends = tbx_pricing_trends.Text;
            if (!string.IsNullOrEmpty(pricing_trends))
                sbUpdateFields.AppendFormat("pricing_trends = '{0}',", pricing_trends);
            var industry_class = tbx_industry_class.Text;
            if (!string.IsNullOrEmpty(industry_class))
                sbUpdateFields.AppendFormat("industry_class = '{0}',", industry_class);

            //2.2五力模型的前三个
            var suppliers_leverage = tbx_suppliers_leverage.Text;
            if (!string.IsNullOrEmpty(suppliers_leverage))
                sbUpdateFields.AppendFormat("suppliers_leverage = '{0}',", suppliers_leverage);
            var pass_supplier_incto_customers = cb_pass_supplier_incto_customers.Checked;
            sbUpdateFields.AppendFormat("pass_supplier_incto_customers ={0},", pass_supplier_incto_customers ? 1 : 0);
            var substi_prod_available = cb_substi_prod_available.Checked;
            sbUpdateFields.AppendFormat("substi_prod_available ={0},", substi_prod_available ? 1 : 0);
            var have_switching_costs = cb_have_switching_costs.Checked;
            sbUpdateFields.AppendFormat("have_switching_costs ={0},", have_switching_costs ? 1 : 0);
            var buyers_leverage = tbx_buyers_leverage.Text;
            if (!string.IsNullOrEmpty(buyers_leverage))
                sbUpdateFields.AppendFormat("buyers_leverage = '{0}',", buyers_leverage);
            var buyers_informed = tbx_buyers_informed.Text;
            if (!string.IsNullOrEmpty(buyers_informed))
                sbUpdateFields.AppendFormat("buyers_informed = '{0}',", buyers_informed);

            //2.3进入的障碍
            var entry_and_exit_rates = tbx_entry_and_exit_rates.Text;
            if (!string.IsNullOrEmpty(entry_and_exit_rates))
                sbUpdateFields.AppendFormat("entry_and_exit_rates = '{0}',", entry_and_exit_rates);
            var reactions_newentrants = tbx_reactions_newentrants.Text;
            if (!string.IsNullOrEmpty(reactions_newentrants))
                sbUpdateFields.AppendFormat("reactions_newentrants = '{0}',", reactions_newentrants);
            var reputation = tbx_reputation.Text;
            if (!string.IsNullOrEmpty(reputation))
                sbUpdateFields.AppendFormat("reputation = '{0}',", reputation);
            var asset_specificity = tbx_asset_specificity.Text;
            if (!string.IsNullOrEmpty(asset_specificity))
                sbUpdateFields.AppendFormat("asset_specificity = '{0}',", asset_specificity);
            var mini_effic_prod_scale = tbx_mini_effic_prod_scale.Text;
            if (!string.IsNullOrEmpty(mini_effic_prod_scale))
                sbUpdateFields.AppendFormat("mini_effic_prod_scale = '{0}',", mini_effic_prod_scale);
            var excess_capacity = cb_excess_capacity.Checked;
            sbUpdateFields.AppendFormat("excess_capacity ={0},", excess_capacity ? 1 : 0);
            var way_to_diff_prod = cb_way_to_diff_prod.Checked;
            sbUpdateFields.AppendFormat("way_to_diff_prod ={0},", way_to_diff_prod ? 1 : 0);
            var antici_payoff_for_new_entrant = tbx_antici_payoff_for_new_entrant.Text;
            if (!string.IsNullOrEmpty(antici_payoff_for_new_entrant))
                sbUpdateFields.AppendFormat("antici_payoff_for_new_entrant = '{0}',", antici_payoff_for_new_entrant);
            var precommit_contracts = cb_precommit_contracts.Checked;
            sbUpdateFields.AppendFormat("precommit_contracts ={0},", precommit_contracts ? 1 : 0);
            var licenses_or_patents = cb_licenses_or_patents.Checked;
            sbUpdateFields.AppendFormat("licenses_or_patents ={0},", licenses_or_patents ? 1 : 0);
            var learning_curve_benefits = tbx_learning_curve_benefits.Text;
            if (!string.IsNullOrEmpty(learning_curve_benefits))
                sbUpdateFields.AppendFormat("learning_curve_benefits = '{0}',", learning_curve_benefits);

            //2.4竞争对手
            var pricing_coordination = tbx_pricing_coordination.Text;
            if (!string.IsNullOrEmpty(pricing_coordination))
                sbUpdateFields.AppendFormat("pricing_coordination = '{0}',", pricing_coordination);
            var industry_concentration = tbx_industry_concentration.Text;
            if (!string.IsNullOrEmpty(industry_concentration))
                sbUpdateFields.AppendFormat("industry_concentration = '{0}',", industry_concentration);
            var size_distribution = tbx_size_distribution.Text;
            if (!string.IsNullOrEmpty(size_distribution))
                sbUpdateFields.AppendFormat("size_distribution = '{0}',", size_distribution);
            var incent_corp_ownership_structure = tbx_incent_corp_ownership_structure.Text;
            if (!string.IsNullOrEmpty(incent_corp_ownership_structure))
                sbUpdateFields.AppendFormat("incent_corp_ownership_structure = '{0}',", incent_corp_ownership_structure);
            var demand_variability = cb_demand_variability.Checked;
            sbUpdateFields.AppendFormat("demand_variability ={0},", demand_variability ? 1 : 0);
            var high_fixed_costs = cb_high_fixed_costs.Checked;
            sbUpdateFields.AppendFormat("high_fixed_costs ={0},", high_fixed_costs ? 1 : 0);
            var industry_growing = cb_industry_growing.Checked;
            sbUpdateFields.AppendFormat("industry_growing ={0},", industry_growing ? 1 : 0);

            //2.5破坏和瓦解
            var vulnerable_disruptive_innovation = cb_vulnerable_disruptive_innovation.Checked;
            sbUpdateFields.AppendFormat("vulnerable_disruptive_innovation ={0},", vulnerable_disruptive_innovation ? 1 : 0);
            var innovations_foster_prod_improve = cb_innovations_foster_prod_improve.Checked;
            sbUpdateFields.AppendFormat("innovations_foster_prod_improve ={0},", innovations_foster_prod_improve ? 1 : 0);
            var inno_progre_faster_market_needs = cb_inno_progre_faster_market_needs.Checked;
            sbUpdateFields.AppendFormat("inno_progre_faster_market_needs ={0},", inno_progre_faster_market_needs ? 1 : 0);
            var estab_players_passed_perform_threshold = cb_estab_players_passed_perform_threshold.Checked;
            sbUpdateFields.AppendFormat("estab_players_passed_perform_threshold ={0},", estab_players_passed_perform_threshold ? 1 : 0);
            var organized_type = tbx_organized_type.Text;
            if (!string.IsNullOrEmpty(organized_type))
                sbUpdateFields.AppendFormat("organized_type = '{0}',", organized_type);

            //公司分析
            //3.1公司相关
            var value_chain_diff_rivals = tbx_value_chain_diff_rivals.Text;
            if (!string.IsNullOrEmpty(value_chain_diff_rivals))
                sbUpdateFields.AppendFormat("value_chain_diff_rivals = '{0}',", value_chain_diff_rivals);
            var production_advantages = cb_production_advantages.Checked;
            sbUpdateFields.AppendFormat("production_advantages ={0},", production_advantages ? 1 : 0);
            var biz_stuct_instable = cb_biz_stuct_instable.Checked;
            sbUpdateFields.AppendFormat("biz_stuct_instable ={0},", biz_stuct_instable ? 1 : 0);
            var complex_co_cap = cb_complex_co_cap.Checked;
            sbUpdateFields.AppendFormat("complex_co_cap ={0},", complex_co_cap ? 1 : 0);
            var how_quick_process_costs_chg = tbx_how_quick_process_costs_chg.Text;
            if (!string.IsNullOrEmpty(how_quick_process_costs_chg))
                sbUpdateFields.AppendFormat("how_quick_process_costs_chg = '{0}',", how_quick_process_costs_chg);
            var patents_copyrights = tbx_patents_copyrights.Text;
            if (!string.IsNullOrEmpty(patents_copyrights))
                sbUpdateFields.AppendFormat("patents_copyrights = '{0}',", patents_copyrights);
            var economies_scale = cb_economies_scale.Checked;
            sbUpdateFields.AppendFormat("economies_scale ={0},", economies_scale ? 1 : 0);
            var distribution_scale = tbx_distribution_scale.Text;
            if (!string.IsNullOrEmpty(distribution_scale))
                sbUpdateFields.AppendFormat("distribution_scale = '{0}',", distribution_scale);
            var ass_reve_clustered_geo = cb_ass_reve_clustered_geo.Checked;
            sbUpdateFields.AppendFormat("ass_reve_clustered_geo ={0},", ass_reve_clustered_geo ? 1 : 0);
            var purchasing_advantages = cb_purchasing_advantages.Checked;
            sbUpdateFields.AppendFormat("purchasing_advantages ={0},", purchasing_advantages ? 1 : 0);
            var economies_scope = cb_economies_scope.Checked;
            sbUpdateFields.AppendFormat("economies_scope ={0},", economies_scope ? 1 : 0);
            var diverse_research_profiles = cb_diverse_research_profiles.Checked;
            sbUpdateFields.AppendFormat("diverse_research_profiles ={0},", diverse_research_profiles ? 1 : 0);
            var consumer_advantages = cb_consumer_advantages.Checked;
            sbUpdateFields.AppendFormat("consumer_advantages ={0},", consumer_advantages ? 1 : 0);
            var habit_horizontal_diff = cb_habit_horizontal_diff.Checked;
            sbUpdateFields.AppendFormat("habit_horizontal_diff ={0},", habit_horizontal_diff ? 1 : 0);
            var prefer_pro_or_competing_pro = tbx_prefer_pro_or_competing_pro.Text;
            if (!string.IsNullOrEmpty(prefer_pro_or_competing_pro))
                sbUpdateFields.AppendFormat("prefer_pro_or_competing_pro = '{0}',", prefer_pro_or_competing_pro);
            var customers_weigh_lots_pro_attrs = cb_customers_weigh_lots_pro_attrs.Checked;
            sbUpdateFields.AppendFormat("customers_weigh_lots_pro_attrs ={0},", customers_weigh_lots_pro_attrs ? 1 : 0);
            var customers_through_trial = cb_customers_through_trial.Checked;
            sbUpdateFields.AppendFormat("customers_through_trial ={0},", customers_through_trial ? 1 : 0);
            var customer_lockin_switching_costs = tbx_customer_lockin_switching_costs.Text;
            if (!string.IsNullOrEmpty(customer_lockin_switching_costs))
                sbUpdateFields.AppendFormat("customer_lockin_switching_costs = '{0}',", customer_lockin_switching_costs);
            var network_radial_or_interactive = tbx_network_radial_or_interactive.Text;
            if (!string.IsNullOrEmpty(network_radial_or_interactive))
                sbUpdateFields.AppendFormat("network_radial_or_interactive = '{0}',", network_radial_or_interactive);
            var source_longevity_added_value = tbx_source_longevity_added_value.Text;
            if (!string.IsNullOrEmpty(source_longevity_added_value))
                sbUpdateFields.AppendFormat("source_longevity_added_value = '{0}',", source_longevity_added_value);
            var external_sources_added_value = tbx_external_sources_added_value.Text;
            if (!string.IsNullOrEmpty(external_sources_added_value))
                sbUpdateFields.AppendFormat("external_sources_added_value = '{0}',", external_sources_added_value);

            //3.2企业互动
            var industry_include_complementors = tbx_industry_include_complementors.Text;
            if (!string.IsNullOrEmpty(industry_include_complementors))
                sbUpdateFields.AppendFormat("industry_include_complementors = '{0}',", industry_include_complementors);
            var value_pie_growing = tbx_value_pie_growing.Text;
            if (!string.IsNullOrEmpty(value_pie_growing))
                sbUpdateFields.AppendFormat("value_pie_growing = '{0}',", value_pie_growing);

            //品牌
            var hire_brand_for_job = cb_hire_brand_for_job.Checked;
            sbUpdateFields.AppendFormat("hire_brand_for_job ={0},", hire_brand_for_job ? 1 : 0);
            var brand_increase_will_to_pay = cb_brand_increase_will_to_pay.Checked;
            sbUpdateFields.AppendFormat("brand_increase_will_to_pay ={0},", brand_increase_will_to_pay ? 1 : 0);
            var emotional_conn_brand = cb_emotional_conn_brand.Checked;
            sbUpdateFields.AppendFormat("emotional_conn_brand ={0},", emotional_conn_brand ? 1 : 0);
            var trust_pro_cause_name = cb_trust_pro_cause_name.Checked;
            sbUpdateFields.AppendFormat("trust_pro_cause_name ={0},", trust_pro_cause_name ? 1 : 0);
            var brand_imply_social_status = cb_brand_imply_social_status.Checked;
            sbUpdateFields.AppendFormat("brand_imply_social_status ={0},", brand_imply_social_status ? 1 : 0);
            var reduce_supplier_opercost_with_name = cb_reduce_supplier_opercost_with_name.Checked;
            sbUpdateFields.AppendFormat("reduce_supplier_opercost_with_name ={0},", reduce_supplier_opercost_with_name ? 1 : 0);

            if (!string.IsNullOrEmpty(sbUpdateFields.ToString()))
            {
                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    string sqlUpdate = "update measuring_the_moat set {1} where ts_code='{0}'";
                    sqlUpdate = string.Format(sqlUpdate, _ts_code, sbUpdateFields.ToString().TrimEnd(','));
                    //sqlUpdate = sqlUpdate.Replace("%", "％");
                    conn.Execute(sqlUpdate);
                    MessageBox.Show("数据更新成功");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sbUpdateFields.ToString()))
                {
                    MessageBox.Show("请录入数据");
                    return;
                }
            }
        }
    }
}
