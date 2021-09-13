using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class EditMajorBizScopeForm : Form
    {
        public BackgroundWorker _backgroundWorker = null;
        private string _id = string.Empty;
        private string _stockName = string.Empty;
        private em_major_business_scope _majorbizdata;

        public EditMajorBizScopeForm(string id, string stockName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            _id = id;
            _stockName = stockName;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindMajorBizScopeData();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BindMajorBizScopeData();

            if (_majorbizdata != null)
            {
                tbxZygc.Text = _majorbizdata.zygc;
                tbxZysr.Text = _majorbizdata.zysr;
                tbxSrbl.Text = _majorbizdata.srbl.ConvertDecimalToString(false);

                tbxZycb.Text = _majorbizdata.zycb;
                tbxCbbl.Text = _majorbizdata.cbbl.ConvertDecimalToString(false);
                tbxZylr.Text = _majorbizdata.zylr;
                tbxLrbl.Text = _majorbizdata.lrbl.ConvertDecimalToString(false);
                tbxMll.Text = _majorbizdata.mll.ConvertDecimalToString(false);
            }
        }

        private void BindMajorBizScopeData()
        {
            string sql = "select * from em_major_business_scope where id='{0}'";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _id);
                _majorbizdata = conn.Query<em_major_business_scope>(sql).FirstOrDefault();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sbUpdateFields = new StringBuilder();

            var zygc = tbxZygc.Text.Trim();
            if (!string.IsNullOrEmpty(zygc))
                sbUpdateFields.AppendFormat("zygc='{0}',", zygc);
            var zysr = tbxZysr.Text.Trim();
            if (!string.IsNullOrEmpty(zysr))
                sbUpdateFields.AppendFormat("zysr='{0}',", zysr);
            var srbl = tbxSrbl.Text.Trim();
            if (!string.IsNullOrEmpty(srbl))
                sbUpdateFields.AppendFormat("srbl={0},", srbl);
            var zycb = tbxZycb.Text.Trim();
            if (!string.IsNullOrEmpty(zycb))
                sbUpdateFields.AppendFormat("zycb='{0}',", zycb);
            var cbbl = tbxCbbl.Text.Trim();
            if (!string.IsNullOrEmpty(cbbl))
                sbUpdateFields.AppendFormat("cbbl={0},", cbbl);
            var zylr = tbxZylr.Text.Trim();
            if (!string.IsNullOrEmpty(zylr))
                sbUpdateFields.AppendFormat("zylr='{0}',", zylr);
            var lrbl = tbxLrbl.Text.Trim();
            if (!string.IsNullOrEmpty(lrbl))
                sbUpdateFields.AppendFormat("lrbl={0},", lrbl);
            var mll = tbxMll.Text.Trim();
            if (!string.IsNullOrEmpty(mll))
                sbUpdateFields.AppendFormat("mll={0},", mll);

            //更新主营构成数据
            if (!string.IsNullOrEmpty(sbUpdateFields.ToString()))
            {
                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    string sqlUpdate = "update em_major_business_scope set {1} where id={0}";
                    sqlUpdate = string.Format(sqlUpdate, _id, sbUpdateFields.ToString().TrimEnd(','));
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
