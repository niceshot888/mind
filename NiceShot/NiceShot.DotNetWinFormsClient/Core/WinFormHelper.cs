using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.Core
{
    public class WinFormHelper
    {
        public static void InitChildWindowStyle(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.StartPosition = FormStartPosition.CenterScreen;
            //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        public static void InitDataGridViewStyle(DataGridView dataGrid)
        {
            dataGrid.BorderStyle = BorderStyle.None;
            Type type = dataGrid.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGrid, true, null);

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGrid.DefaultCellStyle.BackColor = Color.SeaShell;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dataGrid.BackgroundColor = Color.White;
            //dataGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public static void OpenPageInChrome(string url)
        {
            var pro = new Process();
            pro.StartInfo.FileName = "chrome.exe";
            pro.StartInfo.Arguments = url;
            pro.Start();
        }

        public static void OpenPdfInAdobeAcrobat(string pdfpath)
        {
            var myProcess = new Process();
            myProcess.StartInfo.FileName = pdfpath;
            myProcess.StartInfo.Verb = "Open";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
        }

        public static void OpenExeProgram(string filePath)
        {
            var pro = new Process();
            pro.StartInfo.FileName = filePath;
            pro.Start();
        }

    }
}
