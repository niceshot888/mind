using Newtonsoft.Json;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class EmFinReportForm : Form
    {
        private string em_notices_url = "http://data.eastmoney.com/notices/getdata.ashx?StockCode={0}&CodeType=A&PageIndex=1&PageSize=500&jsObj=uPYYCldN&SecNodeType=1&FirstNodeType=1&rt=53237860";

        private string em_companynews_url = "http://emweb.securities.eastmoney.com/PC_HKF10/CompanyNews/GetRecordByPageNo?code={0}&pageno={1}";

        private string _tscode = string.Empty;
        private string _stockName = string.Empty;
        private MarketType _marketType;
        private BackgroundWorker _backgroundWorker = null;
        private List<em_companynews_gsgg_data_item> _gsgg_datalist;
        private List<em_notice_data> _notice_datalist;

        public EmFinReportForm(MarketType marketType, string stockName, string tscode)
        {
            InitializeComponent();

            this.lsbFinReports.DisplayMember = "title";
            this.lsbFinReports.ValueMember = "art_code";
            this._tscode = tscode;
            this._stockName = stockName;
            this._marketType = marketType;
            Control.CheckForIllegalCrossThreadCalls = false;
            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        #region 异步

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lsbFinReports.Items.Clear();
            if(_marketType == MarketType.A)
                lsbFinReports.DataSource = _notice_datalist;
            else if (_marketType == MarketType.HK)
                lsbFinReports.DataSource = _gsgg_datalist;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindListboxEmFinReports();
        }

        #endregion

        private void BindListboxEmFinReports()
        {
            string news_url = string.Empty;
            if (_marketType == MarketType.A)
                news_url = string.Format(em_notices_url, _tscode.Substring(0, 6));
            else if (_marketType == MarketType.HK)
                news_url = string.Format(em_companynews_url, _tscode, 1);

            if (_marketType == MarketType.A)
            {
                string jsonStr = HttpHelper.DownloadUrl(news_url, Encoding.GetEncoding("GB2312"));
                jsonStr = jsonStr.Replace("var uPYYCldN = ", "").TrimEnd(';');

                var fileList = JsonConvert.DeserializeObject<em_notice_list_jo>(jsonStr).data.list.Where(s => !s.title.Contains("摘要") && !s.title.Contains("正文")).ToList();

                _notice_datalist = new List<em_notice_data>();
                
                //foreach (var file in fileList)
                //    _notice_datalist.Add(file);
            }
            else
            {
                string jsonStr = HttpHelper.DownloadUrl(news_url, Encoding.UTF8);
                var data = JsonConvert.DeserializeObject<em_companynews_jo>(jsonStr).gsgg.data;
                if (data != null)
                {
                    _gsgg_datalist = new List<em_companynews_gsgg_data_item>();
                    var pages = data.pageTotal;
                    for (int page =1; page <= pages; page++)
                    {
                        if (_marketType == MarketType.A)
                            news_url = string.Format(em_notices_url, _tscode.Substring(0, 6));
                        else if (_marketType == MarketType.HK)
                            news_url = string.Format(em_companynews_url, _tscode, page);

                        jsonStr = HttpHelper.DownloadUrl(news_url, Encoding.UTF8);
                        data = JsonConvert.DeserializeObject<em_companynews_jo>(jsonStr).gsgg.data;
                        var fileList = data.items.Where(s => s.title.Contains("營運") || s.title.Contains("報告")||s.title.Contains("年報")).ToList();
                        foreach (var file in fileList)
                            _gsgg_datalist.Add(file); 
                    }
                }  
            }

        }

        private void tsmi_viewpdf_Click(object sender, EventArgs e)
        {
            ShowWebBrowserPdfViewer();
        }
        private void lsbFinReports_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowWebBrowserPdfViewer();
        }

        private void ShowWebBrowserPdfViewer()
        {
            if(_marketType == MarketType.A)
            {
                var selectedItem = (em_notice_data)lsbFinReports.SelectedItem;
                var art_code = selectedItem.list[0].title;
                var title = selectedItem.list[0].title;

                ViewPdfForm pdfForm = new ViewPdfForm(_tscode, title, art_code);
                pdfForm.Text = title;
                pdfForm.Show();
            }
            else if (_marketType == MarketType.HK)
            {
                var selectedItem = (em_companynews_gsgg_data_item)lsbFinReports.SelectedItem;
                var infoCode = selectedItem.infoCode;
                var title = selectedItem.title;

                ViewPdfForm pdfForm = new ViewPdfForm(_tscode, title, infoCode);
                pdfForm.Text = title;
                pdfForm.Show();
            }
        }

        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        private void lsbFinReports_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                cmsEmReport.Show(MousePosition.X, MousePosition.Y);
        }

        private void lsbFinReports_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            string text = string.Empty;

            if(_marketType == MarketType.A)
                text = ((em_notice_data)lsbFinReports.Items[e.Index]).list[0].title.ToString();
            else
                text = ((em_companynews_gsgg_data_item)lsbFinReports.Items[e.Index]).title.ToString();

            e.DrawBackground();
            e.DrawFocusRectangle();

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoWrap;

            Rectangle bound = e.Bounds;
            Rectangle rect = new Rectangle(bound.Left, bound.Top, bound.Width, bound.Height);
            e.Graphics.DrawString(text, e.Font, new SolidBrush(Color.Black), rect, sf);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Graphics g = e.Graphics;
                bound = e.Bounds;
                rect = new Rectangle(bound.Left, bound.Top, bound.Width, bound.Height);
                g.FillRectangle(Brushes.MediumSlateBlue, rect);
                TextRenderer.DrawText(g, text, this.Font, rect, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
        }
    }
}
