using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class SupplyFinDataForm : Form
    {
        public BackgroundWorker _backgroundWorker = null;
        private string _tscode = string.Empty;
        private List<string> _reportDateList;
        private bool _isInitStatus = true;
        private string _selectedRepDate = string.Empty;
        private CashflowAdditionalData _cashflowAdditionalData;
        public SupplyFinDataForm(string ts_code)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this._tscode = ts_code;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_isInitStatus)
            {
                BindReportDateList();
            }
            else
            {
                BindCashflowAdditionalData();
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isInitStatus)
            {
                cbxReportDate.DataSource = _reportDateList;
                cbxReportDate.SelectedIndex = 0;
                _selectedRepDate = cbxReportDate.SelectedValue.ToString();

                //绑定最近一期现金流量表补充数据
                BindCashflowAdditionalData();
                _isInitStatus = false;
            }
            else
            {
                if (_cashflowAdditionalData != null)
                {
                    tbx_asseimpa.Text = _cashflowAdditionalData.asseimpa.ConvertDecimalToString(true);
                    tbx_creditimpa.Text = _cashflowAdditionalData.creditimpa.ConvertDecimalToString(true);
                    tbx_assedepr.Text = _cashflowAdditionalData.assedepr.ConvertDecimalToString(true);
                    tbx_realestadep.Text = _cashflowAdditionalData.realestadep.ConvertDecimalToString(true);
                    tbx_intaasseamor.Text = _cashflowAdditionalData.intaasseamor.ConvertDecimalToString(true);
                    tbx_longdefeexpenamor.Text = _cashflowAdditionalData.longdefeexpenamor.ConvertDecimalToString(true);
                    tbx_dispfixedassetloss.Text = _cashflowAdditionalData.dispfixedassetloss.ConvertDecimalToString(true);
                    tbx_fixedassescraloss.Text = _cashflowAdditionalData.fixedassescraloss.ConvertDecimalToString(true);
                }
            }
        }

        private void btn_savedata_Click(object sender, EventArgs e)
        {
            StringBuilder sbUpdateFields = new StringBuilder();
            if (!string.IsNullOrEmpty(tbx_asseimpa.Text.Trim()))
                sbUpdateFields.AppendFormat("asseimpa={0},", tbx_asseimpa.Text.Trim().Replace(",","")); ;
            if (!string.IsNullOrEmpty(tbx_creditimpa.Text.Trim()))
                sbUpdateFields.AppendFormat("creditimpa={0},", tbx_creditimpa.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_assedepr.Text.Trim()))
                sbUpdateFields.AppendFormat("assedepr={0},", tbx_assedepr.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_realestadep.Text.Trim()))
                sbUpdateFields.AppendFormat("realestadep={0},", tbx_realestadep.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_intaasseamor.Text.Trim()))
                sbUpdateFields.AppendFormat("intaasseamor={0},", tbx_intaasseamor.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_longdefeexpenamor.Text.Trim()))
                sbUpdateFields.AppendFormat("longdefeexpenamor={0},", tbx_longdefeexpenamor.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_dispfixedassetloss.Text.Trim()))
                sbUpdateFields.AppendFormat("dispfixedassetloss={0},", tbx_dispfixedassetloss.Text.Trim().Replace(",", ""));
            if (!string.IsNullOrEmpty(tbx_fixedassescraloss.Text.Trim()))
                sbUpdateFields.AppendFormat("fixedassescraloss={0},", tbx_fixedassescraloss.Text.Trim().Replace(",", ""));

            if (string.IsNullOrEmpty(sbUpdateFields.ToString()))
            {
                MessageBox.Show("请录入数据");
                return;
            }

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sqlUpdate = "update em_cashflow_common set {2} where securitycode='{0}' and reportdate='{1}'";
                sqlUpdate = string.Format(sqlUpdate, _tscode, _selectedRepDate, sbUpdateFields.ToString().TrimEnd(','));
                conn.Execute(sqlUpdate);
                MessageBox.Show("数据保存成功");
            }
        }

        private void BindCashflowAdditionalData()
        {
            string sql = "select id,asseimpa,creditimpa,assedepr,realestadep,intaasseamor,longdefeexpenamor,dispfixedassetloss,fixedassescraloss from em_cashflow_common where securitycode='{0}' and reportdate='{1}'";
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _tscode, _selectedRepDate);
                _cashflowAdditionalData = conn.Query<CashflowAdditionalData>(sql).FirstOrDefault();
            }
        }

        private void BindReportDateList()
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select DATE_FORMAT(reportdate,'%Y-%m-%d') reportdate from em_cashflow_common where securitycode='{0}' and ((month(reportdate)=6 and reportdate>'2020-1-1') or (month(reportdate)=12 and reportdate>'1990-1-1')) order by reportdate desc";

                sql = string.Format(sql, _tscode);
                _reportDateList = conn.Query<string>(sql).ToList();
            }
        }

        private void cbxReportDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedRepDate = cbxReportDate.SelectedValue.ToString();
            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }
    }
}
