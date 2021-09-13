using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace NiceShot.DotNetWinFormsClient
{
    public partial class TopHoldersForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private string _tscode;
        private DataTable _dataTable;
        public TopHoldersForm(string ts_code)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            this._tscode = ts_code;

            Type type = tblTopHolders.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(tblTopHolders, true, null);


            WinFormHelper.InitDataGridViewStyle(tblTopHolders);
            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindTopHoldersData();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tblTopHolders.DataSource = _dataTable;
            tblTopHolders.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            for (int i = 0; i < this.tblTopHolders.Columns.Count; i++)
            {
                this.tblTopHolders.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BuildDataTableData(List<xq_top_holders_data_item> dataList)
        {
            foreach (var dataItem in dataList)
            {
                var newRow = _dataTable.NewRow();
                newRow["股东名称"] = dataItem.holder_name;
                newRow["持股数量"] = dataItem.held_num;
                newRow["持股比例"] = dataItem.held_ratio;
                if (dataItem.chg == null)
                    newRow["较上期变动"] = DBNull.Value;
                else
                    newRow["较上期变动"] = dataItem.chg;

                _dataTable.Rows.Add(newRow);
            }
        }

        private void BindTopHoldersData()
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = @"SELECT s.sdgd,s.sdltgd from xq_stock s where s.ts_code = '{0}'";
                sql = string.Format(sql, _tscode);
                var model = con.Query<TopHolderModel>(sql).FirstOrDefault();

                if (model == null)
                    return;

                _dataTable = new DataTable("sdgdtable");
                _dataTable.Columns.Add("股东名称", typeof(string));
                _dataTable.Columns.Add("持股数量", typeof(decimal));
                _dataTable.Columns.Add("持股比例", typeof(decimal));
                _dataTable.Columns.Add("较上期变动", typeof(decimal));

                /*var newRow = _dataTable.NewRow();
                newRow["股东名称"] = "----十大股东----------------";
                newRow["持股数量"] = DBNull.Value;
                newRow["持股比例"] = DBNull.Value;
                newRow["较上期变动"] = DBNull.Value;
                _dataTable.Rows.Add(newRow);

                var sdgd_data = JsonConvert.DeserializeObject<xq_top_holders_jo>(model.sdgd).data;
                var sdgd_data_list = sdgd_data.quit.Union(sdgd_data.items).OrderByDescending(s=>s.held_num).ToList();
                BuildDataTableData(sdgd_data_list);

                newRow = _dataTable.NewRow();
                newRow["股东名称"] = "----十大流通股东------------";
                newRow["持股数量"] = DBNull.Value;
                newRow["持股比例"] = DBNull.Value;
                newRow["较上期变动"] = DBNull.Value;
                _dataTable.Rows.Add(newRow);*/

                var sdltgd_data = JsonConvert.DeserializeObject<xq_top_holders_jo>(model.sdltgd).data;
                var sdltgd_data_list = sdltgd_data.quit.Union(sdltgd_data.items).OrderByDescending(s => s.held_num).ToList();
                BuildDataTableData(sdltgd_data_list);

            }
        }

        private void tbl_stock_details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.tblTopHolders.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tblTopHolders.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void tblTopHolders_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.tblTopHolders.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tblTopHolders.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }
    }
}
