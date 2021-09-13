using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class SinaBondForm : Form
    {
        private DataSet _dataTable;
        private BackgroundWorker _backgroundWorker = null;
        private string _stockName;
        private string _tscode;
        public SinaBondForm(string stockName,string ts_code)
        {
            InitializeComponent();

            _stockName = stockName;
            _tscode = ts_code;

            if (stockName.Length == 3 && Encoding.Default.GetBytes(stockName[2].ToString()).Length > 1)
                tbxKeywords.Text = stockName[0] + "" + stockName[2];
            else
                tbxKeywords.Text = stockName.Substring(0, 2);

            WinFormHelper.InitDataGridViewStyle(dgv_bond);
            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var keywords = tbxKeywords.Text.Trim();
            BindSinaBondData(keywords);
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv_bond.DataSource = _dataTable.Tables[0];
            dgv_bond.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            for (int i = 0; i < this.dgv_bond.Columns.Count; i++)
            {
                this.dgv_bond.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BindSinaBondData(string keywords)
        {
            using (var con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                var sbCondition = new StringBuilder();
                var key_arr = keywords.Split(',');
                sbCondition.Append("(");
                var index = 0;
                foreach (var key in key_arr)
                {
                    index++;
                    if (index != key_arr.Length)
                        sbCondition.AppendFormat("name like '%{0}%' or ", key);
                    else
                        sbCondition.AppendFormat("name like '%{0}%'", key);
                }
                sbCondition.Append(")");

                string sql = @"SELECT name 名称,trade 最新价,changepercent 涨跌幅,settlement 昨收 from sina_bond where {0}";
                sql = string.Format(sql, sbCondition.ToString());
                var da = new MySqlDataAdapter(sql, con);
                _dataTable = new DataSet();
                da.Fill(_dataTable);
            }
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void dgv_bond_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var b = new SolidBrush(this.dgv_bond.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(CultureInfo.CurrentUICulture), this.dgv_bond.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private string em_notices_url = "http://np-anotice-stock.eastmoney.com/api/security/ann?sr=-1&page_size=500&page_index=1&ann_type=A&client_source=web&stock_list={0}&f_node=1&s_node=1";
        private string em_notices_cookie = "em_hq_fls=js; em-quote-version=topspeed; ct=fXcz3YE96C_i8i38WlhqDeA9jvpker_pOF35i8T0jTJtygRODbS9serG3_zwDr6SeFkjrMfUroN_ZiF19S6CqAwRKdW1Br5M6wTNST6kb3ZZK18m4u21Mr3WXqUExBhh8l0UX1Wf8Js6UoTURTL8eecFfhPaN07Lv9rGjYfkHHQ; ut=FobyicMgeV5yNnZj1TOuToUxigtdNW0WLhLF_nrDS5MTZkofxFtpDca-W1GABWrmg1QQX47XJ6HtA9HfA5gSZgTheg5drPAcfODS2DZac0oi7W-YwCEiENzU0N_ckJzyyGiQ4vLZitXsGXdFRg_cWMhZ1Tl0xYq9ZqSxBh9120TaeWsjDhnhml4FcCK3Coft-_zi7dX67UzzIUOQMM4NcLtaASyYics-N4p_XGnRknyvgvXMzHxUxMyZG8unK6K2Gp_mz1JmzUU; pi=1060555200541870%3bj1060555200541870%3b%e9%95%bf%e7%ba%bf%e4%b8%ba%e7%8e%8b01%3b6fWR4MK6IFZyjAEGucQAkBYht0W36q2l2rbcQN7WnVNpp8C101ibquv2LEICQhgUSNduu9LvOAi4I8ojWliPT8b5W0CFKh6LHZaPXvcd8pmzH7HhHEKERA0mvs9va2NJNN0SvI2ssoV90jSTOsRjt896sTPTwOy7Ds%2bC%2f09KRZpIZkUgAaQmfygce2cl8Tvp9GJxEIA5%3bHKDE%2bfGt6zMtjMv8bDun2AxPsMv5uXVvFZAtRbLkLFzFII6X6l9yCnejgqnq8LdfT3YJcnCsa%2fOU6aC4hZ9MjqR1rFl8FaEpXQWB7D%2fpZVuqp9nQl9BGc0jUPh8%2f6lP%2bpNHCQdsQm7T4ii9tUScnTc6rWCLZVA%3d%3d; uidal=1060555200541870%e9%95%bf%e7%ba%bf%e4%b8%ba%e7%8e%8b01; sid=121732702; vtpst=|; HAList=d-hk-09961%2Ca-sz-300976-%u8FBE%u745E%u7535%u5B50%2Ca-sz-002002-%u9E3F%u8FBE%u5174%u4E1A%2Ca-sz-000961-%u4E2D%u5357%u5EFA%u8BBE%2Cd-hk-00177%2Ca-sz-002440-%u95F0%u571F%u80A1%u4EFD%2Cf-0-000013-%u4F01%u503A%u6307%u6570%2Ca-sh-603087-%u7518%u674E%u836F%u4E1A; st_si=37574115537906; cowCookie=true; emshistory=%5B%22%E9%98%B3%E5%85%89%E5%9F%8E%22%2C%22%E5%80%BA%E5%88%B8%22%2C%22(000961)(%E4%B8%AD%E5%8D%97%E5%BB%BA%E8%AE%BE)%22%2C%22%E4%B8%AD%E5%8D%97%E5%BB%BA%E8%AE%BE%22%2C%22%E7%8E%B0%E9%87%91%E6%B5%81%E9%87%8F%E8%A1%A8%E8%A1%A5%E5%85%85%E8%B5%84%E6%96%99%22%5D; intellpositionL=1602px; st_asi=delete; intellpositionT=8115px; qgqp_b_id=4a3502c72018e8c5125d8bf859390436; st_pvi=16756717426163; st_sp=2020-09-07%2000%3A33%3A30; st_inirUrl=https%3A%2F%2Fwww.baidu.com%2Flink; st_sn=141; st_psi=20210429132801570-111000300841-8173337033";
        private void btnViewFinReport_Click(object sender, System.EventArgs e)
        {
            var simplecode = _tscode.Substring(0, 6);
            var date = new DateTime(2020, 12, 31);
            var jsonStr = HttpHelper.DownloadUrl(string.Format(em_notices_url, simplecode), Encoding.GetEncoding("utf-8"), em_notices_cookie);
            var noticelist = JsonConvert.DeserializeObject<em_notice_list_jo>(jsonStr);

            var keywords = GetReportTitleKeywords(date);

            ShowPdfViewer(noticelist, _stockName, keywords);
        }

        private string GetReportTitleKeywords(DateTime date)
        {
            var keywords = string.Empty;
            if (date.Month == 3)
                keywords = string.Format("{0}年第一季度报告全文,{0}年第一季度报告,{0}年第一季度季报", date.Year);
            else if (date.Month == 6)
                keywords = string.Format("{0}年半年度报告,{0}年半年度报告,{0}年半年报", date.Year);
            else if (date.Month == 9)
                keywords = string.Format("{0}年第三季度报告全文,{0}年第三季度报告,{0}年第三季度季报", date.Year);
            else if (date.Month == 12)
                keywords = string.Format("{0}年年度报告,{0}年度报告,{0}年年报", date.Year);

            return keywords;
        }

        private void ShowPdfViewer(em_notice_list_jo noticelist, string stockName, string keywords)
        {
            em_notice_data_item notice;
            var keywords_arr = keywords.Split(',');
            if (keywords_arr.Length > 1)
                notice = GetNoticeDataItem(keywords_arr, noticelist);
            else
                notice = noticelist.data.list.FirstOrDefault(s => s.title.Contains(keywords) && !s.title.Contains("摘要"));
            if (notice != null)
            {
                var link = string.Format("https://pdf.dfcfw.com/pdf/H2_{0}_1.PDF", notice.art_code);
                var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
                {
                    Width = 1300,
                    Height = 750,
                    IsExternalLink = true,
                    Url = link
                });
                //wb.MdiParent = this;
                wb.Text = stockName + "-" + keywords_arr[0];
                wb.Show();
            }
        }

        private em_notice_data_item GetNoticeDataItem(string[] keywords_arr, em_notice_list_jo noticelist)
        {
            foreach (var keywords in keywords_arr)
            {
                var notice = noticelist.data.list.FirstOrDefault(s => s.title.Contains(keywords) && !s.title.Contains("摘要"));
                if (notice != null)
                    return notice;
            }
            return null;
        }
    }
}
