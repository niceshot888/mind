using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class MajorBusinessScopeForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private string _stockName;
        private string _tscode;
        private DataSet _dataTable;
        public MajorBusinessScopeForm(string stockName, string ts_code)
        {
            InitializeComponent();

            _tscode = ts_code;
            _stockName = stockName;

            WinFormHelper.InitDataGridViewStyle(dgv_m_biz_scope);
            WinFormHelper.InitChildWindowStyle(this);

            this.StartPosition = FormStartPosition.CenterParent;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindMajorBizScopeData(_tscode);
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv_m_biz_scope.DataSource = _dataTable.Tables[0];
            dgv_m_biz_scope.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgv_m_biz_scope.Columns[0].Visible = false;
            dgv_m_biz_scope.Columns[11].Visible = false;
            dgv_m_biz_scope.Columns[12].Visible = false;
            for (int i = 0; i < this.dgv_m_biz_scope.Columns.Count; i++)
            {
                this.dgv_m_biz_scope.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BindMajorBizScopeData(string ts_code)
        {
            using (var con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                //and month(date)=12 and lrbl>=10
                string sql = @"SELECT id,case type when 'cp' then '产品' when 'hy' then '行业' end 类型,date 日期,zygc 主营构成,zysr 主营收入,srbl 收入比例,zycb 主营成本,cbbl 成本比例,zylr 主营利润,lrbl 利润比例,mll 毛利率,color,ts_code from em_major_business_scope where ts_code='{0}' and month(date)=12 order by type asc,date desc,(case when LOCATE('亿',zysr)>0 then zysr*100000000 when LOCATE('万',zysr)>0 then zysr*10000 end ) desc";

                sql = string.Format(sql, ts_code);
                var da = new MySqlDataAdapter(sql, con);
                _dataTable = new DataSet();
                da.Fill(_dataTable);
            }
        }

        private void dgv_m_biz_scope_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var b = new SolidBrush(this.dgv_m_biz_scope.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(CultureInfo.CurrentUICulture), this.dgv_m_biz_scope.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private List<string> dic_bizdate_list = new List<string>();
        private List<string> dic_biztype_list = new List<string>();
        private Color default_biz_color = Color.LightPink;
        private string default_biz_type = "产品";

        private void dgv_m_biz_scope_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (var rowIndex = 0; rowIndex < dgv_m_biz_scope.Rows.Count; rowIndex++)
            {
                var type = dgv_m_biz_scope.Rows[rowIndex].Cells[1].Value.ToString();
                var date = dgv_m_biz_scope.Rows[rowIndex].Cells[2].Value.ToString();
                var color = dgv_m_biz_scope.Rows[rowIndex].Cells[11].Value.ToString();

                if (!dic_biztype_list.Contains(type))
                {
                    default_biz_type = type;
                    if (dic_biztype_list.Count > 0)
                        default_biz_color = Color.LightBlue;
                    dic_biztype_list.Add(type);
                }

                if (!dic_bizdate_list.Contains(date + default_biz_type))
                {
                    dgv_m_biz_scope.Rows[rowIndex].DefaultCellStyle.BackColor = default_biz_color;
                    dic_bizdate_list.Add(date + default_biz_type);
                }

                if (!string.IsNullOrEmpty(color))
                {
                    var colorType = (ColorType)int.Parse(color);
                    if (colorType == ColorType.Transparent)
                        dgv_m_biz_scope.Rows[rowIndex].DefaultCellStyle.BackColor = dgv_m_biz_scope.RowsDefaultCellStyle.BackColor;
                    else
                        dgv_m_biz_scope.Rows[rowIndex].DefaultCellStyle.BackColor = ColorTypeHelper.ConverToColor(colorType);
                }
            }
        }

        private void dgv_m_biz_scope_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgv_m_biz_scope.Rows[e.RowIndex].Selected == false)
                    {
                        dgv_m_biz_scope.ClearSelection();
                        dgv_m_biz_scope.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgv_m_biz_scope.SelectedRows.Count == 1 && e.ColumnIndex != -1)
                        dgv_m_biz_scope.CurrentCell = dgv_m_biz_scope.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    //弹出操作菜单
                    cms_details.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void tsmi_setpink_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;
            var id = selectedRow.Cells["id"].Value.ToString();
            UpdateRowColorInDb(id, (int)ColorType.LightPink);
            selectedRow.DefaultCellStyle.BackColor = ColorTypeHelper.ConverToColor(ColorType.LightPink);
        }

        private void tsmi_setblue_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;
            var id = selectedRow.Cells["id"].Value.ToString();
            UpdateRowColorInDb(id, (int)ColorType.LightBlue);
            selectedRow.DefaultCellStyle.BackColor = ColorTypeHelper.ConverToColor(ColorType.LightBlue);
        }

        private void tsmi_setgreen_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;
            var id = selectedRow.Cells["id"].Value.ToString();
            UpdateRowColorInDb(id, (int)ColorType.LightGreen);//Color.MediumAquamarine
            selectedRow.DefaultCellStyle.BackColor = ColorTypeHelper.ConverToColor(ColorType.LightGreen);
        }

        private void tsmi_setpurple_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;
            var id = selectedRow.Cells["id"].Value.ToString();
            UpdateRowColorInDb(id, (int)ColorType.Purple);//Color.Thistle
            selectedRow.DefaultCellStyle.BackColor = ColorTypeHelper.ConverToColor(ColorType.Purple);
        }

        private void tsmi_settransparent_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;
            var id = selectedRow.Cells["id"].Value.ToString();
            UpdateRowColorInDb(id, (int)ColorType.Transparent);
            selectedRow.DefaultCellStyle.BackColor = dgv_m_biz_scope.RowsDefaultCellStyle.BackColor;
        }

        private void tsmi_clearall_Click(object sender, System.EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认清空所有行背景色？", "删除提示", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            var rows = dgv_m_biz_scope.Rows;
            foreach (DataGridViewRow row in rows)
            {
                var id = row.Cells["id"].Value.ToString();

                if (row.DefaultCellStyle.BackColor != dgv_m_biz_scope.RowsDefaultCellStyle.BackColor)
                {
                    row.DefaultCellStyle.BackColor = dgv_m_biz_scope.RowsDefaultCellStyle.BackColor;
                    UpdateRowColorInDb(id, (int)ColorType.Transparent);
                }
            }
        }

        private void tsmi_hideotherdata_Click(object sender, System.EventArgs e)
        {
            var rows = dgv_m_biz_scope.Rows;
            foreach (DataGridViewRow row in rows)
            {
                if (row.DefaultCellStyle.BackColor == dgv_m_biz_scope.RowsDefaultCellStyle.BackColor)
                {
                    //与货币管理器的位置关联的行不能设置为不可见异常解决方案
                    var currmanager = (CurrencyManager)BindingContext[dgv_m_biz_scope.DataSource];
                    currmanager.SuspendBinding();
                    row.Visible = false;
                    currmanager.ResumeBinding();
                }
            }
        }

        private void tsmi_showalldata_Click(object sender, System.EventArgs e)
        {
            var rows = dgv_m_biz_scope.Rows;
            foreach (DataGridViewRow row in rows)
            {
                if (!row.Visible)
                {
                    var currmanager = (CurrencyManager)BindingContext[dgv_m_biz_scope.DataSource];
                    currmanager.SuspendBinding();
                    row.Visible = true;
                    currmanager.ResumeBinding();
                }
            }
        }

        private void tsmi_update_data_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            if (selectedRow == null)
                return;

            var id = selectedRow.Cells["id"].Value.ToString();

            var frmFinData = new EditMajorBizScopeForm(id, _stockName);
            frmFinData.Text = _stockName + "-更新主营构成数据";
            frmFinData.Show();

            SetParent((int)frmFinData.Handle, (int)this.Handle);
        }

        private void UpdateRowColorInDb(string id, int color)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sqlUpdate = "update em_major_business_scope set color={1} where id={0}";
                sqlUpdate = string.Format(sqlUpdate, id, color);
                conn.Execute(sqlUpdate);
            }
        }

        [DllImport("user32", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        private void tsmi_view_report_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dgv_m_biz_scope.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var simplecode = ts_code.Substring(0, 6);
            var date = StringUtils.ConvertToDate(selectedRow.Cells["日期"].Value.ToString()).GetValueOrDefault();
            var jsonStr = HttpHelper.DownloadUrl(string.Format(NoticePdfHelper.em_notices_url, simplecode), Encoding.GetEncoding("utf-8"));

            NoticePdfHelper.ShowPdfViewer(jsonStr, date);
        }

        private void tsmi_wdm_Click(object sender, EventArgs e)
        {
            var simplecode = _tscode.Substring(0, 6);

            var pro = new Process();
            pro.StartInfo.FileName = "chrome.exe";
            pro.StartInfo.Arguments = "https://guba.eastmoney.com/qa/search?type=1&code=" + simplecode;
            pro.Start();
        }
    }
}
