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
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class ZhangAanalysisForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private string _tscode;
        private DataTable _dataTable;
        public ZhangAanalysisForm(string ts_code)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            this._tscode = ts_code;

            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();

            StringBuilder sbMsg = new StringBuilder();
            sbMsg.AppendLine("（一）资产显著增长：");
            sbMsg.AppendLine("1.企业可支配资源在增加；");
            sbMsg.AppendLine("2.企业经历业务增长；");
            sbMsg.AppendLine("3.技术装备水平的升级；");
            sbMsg.AppendLine("（二）资产显著减少：");
            sbMsg.AppendLine("1.企业业务规模在萎缩；");
            sbMsg.AppendLine("2.企业的某些资产出现重大问题而被减值处理；");
            sbMsg.AppendLine("3.企业正在经历业务结构的战略调整等；");
            sbMsg.AppendLine("（三）资产保持基本稳定：");
            sbMsg.AppendLine("企业业务规模处于稳定状态；");
            sbMsg.AppendLine("PS：如果企业的资产结构出现重大调整，");
            sbMsg.AppendLine("也可能意味着企业正在经历业务结构的战略调整等。");

            //tt_sumass_info.AutoPopDelay = 10000;
            //tt_sumass_info.InitialDelay = 500;
            //tt_sumass_info.ReshowDelay = 500;
            tt_sumass_info.ShowAlways = true;
            tt_sumass_info.SetToolTip(ll_sumass_more, sbMsg.ToString());
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void ll_sumass_more_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}