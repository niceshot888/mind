using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class AddHDForm : Form
    {

        public BackgroundWorker _backgroundWorker = null;
        private string _tscode = string.Empty;
        private string _selectedRepDate = string.Empty;
        private string _date;
        private ThsHDData _thsHDData;
        private MainIndexData _mainIndexData;

        public AddHDForm(string stockName, string ts_code, string date)
        {
            InitializeComponent();

            cbxDate.Items.Clear();
            cbxDate.Items.Add("2020-12-31");
            cbxDate.Items.Add("2019-12-31");
            cbxDate.Items.Add("2018-12-31");
            cbxDate.Items.Add("2017-12-31");
            cbxDate.Items.Add("2016-12-31");
            //cbxDate.Items.Add("2017-12-31");

            this.StartPosition = FormStartPosition.CenterParent;
            this._tscode = ts_code;
            this._date = date;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sbUpdateFields_ThsHD = new StringBuilder();
            var sbUpdateFields_MainIndex = new StringBuilder();
            var sbInsertFields = new StringBuilder();

            var hd = tbxHd.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxHd.Text.Trim()))
            {
                sbUpdateFields_ThsHD.AppendFormat("rdspendsum={0},", hd);
            }
            sbInsertFields.AppendFormat("{0},", !string.IsNullOrEmpty(hd) ? hd : "null");

            var rdspendsum_ratio = tbxHdRatio.Text.Trim().Replace(",", "").Replace("%", "");
            if (!string.IsNullOrEmpty(tbxHdRatio.Text.Trim()))
            {
                sbUpdateFields_ThsHD.AppendFormat("rdspendsum_ratio={0},", rdspendsum_ratio);
            }
            sbInsertFields.AppendFormat("{0},", !string.IsNullOrEmpty(rdspendsum_ratio) ? rdspendsum_ratio : "null");

            var rdinvest = tbxZbh.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxZbh.Text.Trim()))
            {
                sbUpdateFields_ThsHD.AppendFormat("rdinvest={0},", rdinvest);
            }
            sbInsertFields.AppendFormat("{0},", !string.IsNullOrEmpty(rdinvest) ? rdinvest : "null");

            var rdinvest_ratio = tbxZbhRatio.Text.Trim().Replace(",", "").Replace("%", "");
            if (!string.IsNullOrEmpty(tbxZbhRatio.Text.Trim()))
            {
                sbUpdateFields_ThsHD.AppendFormat("rdinvest_ratio={0},", rdinvest_ratio);
            }
            sbInsertFields.AppendFormat("{0},", !string.IsNullOrEmpty(rdinvest_ratio) ? rdinvest_ratio : "null");

            var bonus = tbxBonus.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxBonus.Text.Trim()))
            {
                sbUpdateFields_MainIndex.AppendFormat("bonus={0},", bonus);
            }

            var sales = tbxSales.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxSales.Text.Trim()))
            {
                sbUpdateFields_MainIndex.AppendFormat("sales={0},", sales);
            }

            var lxzbh = tbxLXZBH.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxLXZBH.Text.Trim()))
            {
                sbUpdateFields_MainIndex.AppendFormat("lxzbh={0},", lxzbh);
            }

            var totalJkfy = tbxTotalJKFY.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(tbxTotalJKFY.Text.Trim()))
            {
                sbUpdateFields_MainIndex.AppendFormat("total_jkfy={0},", totalJkfy);
            }

            //更新MainIndex表数据
            if (!string.IsNullOrEmpty(sbUpdateFields_MainIndex.ToString()))
            {
                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    string sqlUpdate = "update em_a_mainindex set {2} where ts_code='{0}' and date='{1}'";
                    sqlUpdate = string.Format(sqlUpdate, _tscode, _selectedRepDate, sbUpdateFields_MainIndex.ToString().TrimEnd(','));
                    conn.Execute(sqlUpdate);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sbUpdateFields_ThsHD.ToString()))
                {
                    MessageBox.Show("请录入数据");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(sbUpdateFields_ThsHD.ToString()))
            {
                var existsData = ExistsThsHDData();
                if (existsData)
                {
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        string sqlUpdate = "update ths_hd set {2} where ts_code='{0}' and reportdate='{1}'";
                        sqlUpdate = string.Format(sqlUpdate, _tscode, _selectedRepDate, sbUpdateFields_ThsHD.ToString().TrimEnd(','));
                        conn.Execute(sqlUpdate);
                    }
                }
                else
                {
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        var sqlInsert = "insert into ths_hd(rdspendsum,rdspendsum_ratio,rdinvest,rdinvest_ratio,ts_code,reportdate) values ({0}'{1}','{2}')";
                        sqlInsert = string.Format(sqlInsert, sbInsertFields.ToString(), _tscode, _selectedRepDate);
                        conn.Execute(sqlInsert);
                    }
                }
            }

            MessageBox.Show("数据保存成功");
        }

        private void cbxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedRepDate = cbxDate.SelectedItem.ToString();
            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindThsHDAndMainIndexData();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (cbxDate.SelectedItem == null)
            {
                cbxDate.SelectedItem = _date;
                _selectedRepDate = _date;
            }
           
            //绑定最近一期研发投入数据
            BindThsHDAndMainIndexData();

            if (_thsHDData != null)
            {
                tbxHd.Text = _thsHDData.rdspendsum.ConvertDecimalToString(true);
                tbxHdRatio.Text = _thsHDData.rdspendsum_ratio.ConvertDecimalToString(true);
                tbxZbh.Text = _thsHDData.rdinvest.ConvertDecimalToString(true);
                tbxZbhRatio.Text = _thsHDData.rdinvest_ratio.ConvertDecimalToString(true);
            }

            if (_mainIndexData != null)
            {
                tbxBonus.Text = _mainIndexData.bonus.ConvertDecimalToString(true);
                tbxSales.Text = _mainIndexData.sales.ConvertDecimalToString(true);
                tbxLXZBH.Text = _mainIndexData.lxzbh.ConvertDecimalToString(true);
                tbxTotalJKFY.Text = _mainIndexData.total_jkfy.ConvertDecimalToString(true);
            }
        }

        private void BindThsHDAndMainIndexData()
        {
            string sql = "select id,rdspendsum,rdspendsum_ratio,rdinvest,rdinvest_ratio from ths_hd where ts_code='{0}' and reportdate='{1}'";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _tscode, _selectedRepDate);
                _thsHDData = conn.Query<ThsHDData>(sql).FirstOrDefault();
            }

            sql = "select id,bonus,sales,lxzbh,total_jkfy from em_a_mainindex where ts_code='{0}' and date='{1}'";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _tscode, _selectedRepDate);
                _mainIndexData = conn.Query<MainIndexData>(sql).FirstOrDefault();
            }
        }

        private bool ExistsThsHDData()
        {
            string sql = "select count(1) from ths_hd where ts_code='{0}' and reportdate='{1}'";
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                sql = string.Format(sql, _tscode, _selectedRepDate);
                return conn.ExecuteScalar<int>(sql) > 0;
            }
        }

        private void cbxDate_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
