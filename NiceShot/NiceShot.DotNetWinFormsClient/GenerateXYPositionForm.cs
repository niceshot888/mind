using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class GenerateXYPositionForm : Form
    {
        public GenerateXYPositionForm()
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
        }

        private void btnGenerateXY_Click(object sender, EventArgs e)
        {
            //生成百度坐标
            //GenerateVankeXYDatas();
            //GenerateZoinaXYDatas();
            //GenerateGreenTownXYDatas();
            //GenerateSeazenXYDatas();
            //GenerateYangoXYDatas();
            //GenerateJinKeXYDatas();
            //GenerateBrcXYDatas();
            //GenerateGemdaleXYDatas();
            //GenerateLongForXYDatas();
            //GeneratePolyXYDatas();
            //GenerateRiseSunXYDatas();
            GenerateZLDCXYDatas();
        }

        private void GenerateBrcXYDatas()
        {
            string company = "brc";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add("jinke2020");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList("jinke", "jinke2020", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add("jinke2019");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList("jinke", "jinke2019", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add("jinke2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList("jinke", "jinke2018", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GeneratePolyXYDatas()
        {
            string company = "poly";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;

                var year = 2020;

                //now year
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2017 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add(company + "2019");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList(company, company + "2019", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2018", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2017");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2017", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateGreenTownXYDatas()
        {
            string company = "greentown";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;

                var year = 2020;

                //now year
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList(company, company + "2019", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2018", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateGemdaleXYDatas()
        {
            string company = "gemdale";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2017
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add(company + "2019");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList(company, company + "2019", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2018", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2017");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2017", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateVankeXYDatas()
        {
            string company = "vanke";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2021;

                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                package.Save();
            }
        }


        private void GenerateZLDCXYDatas()
        {
            string company = "zldc";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;

                var year = 2020;

                //now year
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                package.Save();
            }
        }

        private void GenerateRiseSunXYDatas()
        {
            string company = "risesun";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;

                var year = 2020;

                //now year
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add(company + "2019");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList(company, company + "2019", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateSeazenXYDatas()
        {
            string company = "seazen";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                //year 2020
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //year 2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //year 2018
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                package.Save();
            }
        }

        private void GenerateCifiXYDatas()
        {
            string company = "cifi";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                //year 2020
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //year 2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //year 2018
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add(company + "2019");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList(company, company + "2019", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add(company + "2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList(company, company + "2018", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateJinKeXYDatas()
        {
            string company = "jinke";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add("jinke2020");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList("jinke", "jinke2020", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add("jinke2019");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList("jinke", "jinke2019", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add("jinke2018");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList("jinke", "jinke2018", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateZoinaXYDatas()
        {
            string company = "zonia";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2021;

                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }
                */
                package.Save();
            }
        }

        private void GenerateYangoXYDatas()
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string company = "yango";
            var excelFilePath = Path.Combine(dirPath, company+"_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;

                var year = 2020;

                //now year
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2019 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2018 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //2017 year
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add("yango2020");
                var rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                var xyDataList = GetXYDataList("yango", "yango2020", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }

                ws = package.Workbook.Worksheets.Add("yango2019");
                rowIndex = 1;

                ws.Cells[rowIndex, 1].Value = "name";
                ws.Cells[rowIndex, 2].Value = "lng";
                ws.Cells[rowIndex, 3].Value = "lat";

                xyDataList = GetXYDataList("yango", "yango2019", false);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = data.name;
                    ws.Cells[rowIndex, 2].Value = data.lng;
                    ws.Cells[rowIndex, 3].Value = data.lat;
                }*/

                package.Save();
            }
        }

        private void GenerateLongForXYDatas()
        {
            string company = "longfor";
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var excelFilePath = Path.Combine(dirPath, company + "_land.xlsx");
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                ExcelWorksheet ws;
                int rowIndex = 1;
                List<LandXYPosition> xyDataList;
                var year = 2020;

                //year 2020
                ws = package.Workbook.Worksheets.Add(company + year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                //year 2019
                ws = package.Workbook.Worksheets.Add(company + --year);
                rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                xyDataList = GetXYDataList(company, company + year, true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }

                /*var ws = package.Workbook.Worksheets.Add(company + "2019");
                var rowIndex = 1;

                SetXYExcelHeader(ws, rowIndex);

                var xyDataList = GetXYDataList(company, company + "2019", true);
                foreach (var data in xyDataList)
                {
                    rowIndex++;
                    SetXYExcelContent(ws, rowIndex, data);
                }*/

                package.Save();
            }
        }

        private void btnOpenSourceDir_Click(object sender, EventArgs e)
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs");
            System.Diagnostics.Process.Start(dirPath);
        }

        private void btnSavedFileDir_Click(object sender, EventArgs e)
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            System.Diagnostics.Process.Start(dirPath);
        }

        private void btnArcGisDataDir_Click(object sender, EventArgs e)
        {
            string dirPath = "E:\\kim\\arcgis\\data";
            System.Diagnostics.Process.Start(dirPath);
        }

        private void SetXYExcelHeader(ExcelWorksheet ws, int rowIndex)
        {
            ws.Cells[rowIndex, 1].Value = "name";
            ws.Cells[rowIndex, 2].Value = "lng";
            ws.Cells[rowIndex, 3].Value = "lat";
            ws.Cells[rowIndex, 4].Value = "floorprice";
            ws.Cells[rowIndex, 5].Value = "month";
        }

        private void SetXYExcelContent(ExcelWorksheet ws, int rowIndex, LandXYPosition data)
        {
            ws.Cells[rowIndex, 1].Value = data.name;
            ws.Cells[rowIndex, 2].Value = data.lng;
            ws.Cells[rowIndex, 3].Value = data.lat;
            ws.Cells[rowIndex, 4].Value = data.floorprice;
            ws.Cells[rowIndex, 5].Value = data.month;
        }

        private List<LandXYPosition> GetXYDataList(string company, string sheetName, bool includePosition)
        {
            var xyDataList = new List<LandXYPosition>();
            var dataList = GetPositionsBySheetName(company, sheetName);
            foreach (var data in dataList)
            {
                var xy = includePosition ? GetXY(data.city + data.position) : GetXY(data.city);

                if (xy == null)
                    continue;

                xyDataList.Add(new LandXYPosition { city = data.city, name = data.name, month = data.month, floorprice = data.floorprice, position = data.position, lng = xy[0], lat = xy[1] });
            }

            return xyDataList;
        }


        /// <summary>
        /// 获取经纬度
        /// </summary>
        /// <param name="address"></param>
        private double[] GetXY(string address)
        {
            try
            {
                string url = String.Format("http://api.map.baidu.com/geocoding/v3/?address={0}&output=json&ak={1}", address, "j4IGnWHslPYlGRcL4IQc2Yz8aViDnP5v");

                HttpClient client = new HttpClient();

                string result = client.GetStringAsync(url).Result;

                var locationResult = (JObject)JsonConvert.DeserializeObject(result);
                if (result.Contains("Error"))
                    return null;

                string lngStr = locationResult["result"]["location"]["lng"].ToString();

                string latStr = locationResult["result"]["location"]["lat"].ToString();

                double lng = double.Parse(lngStr);

                double lat = double.Parse(latStr);

                return new double[] { lng, lat };
            }
            catch (Exception ex)
            {
                Logger.Error(address + ":坐标获取失败," + ex.Message);
                return null;
            }

        }

        private List<LandXYPosition> GetPositionsBySheetName(string company, string sheetName)
        {
            var datalist = new List<LandXYPosition>();
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", company + "_land.xlsx");

            //设置源文件名称
            //dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "yango_land.xlsx");
            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (worksheet.Cells[row, 3].Value == null)
                        break;

                    var floorprice = worksheet.Cells[row, 4].Value == null ? 0 : worksheet.Cells[row, 4].Value.ToString().Round0Decimals();

                    try
                    {
                        datalist.Add(new LandXYPosition
                        {
                            name = worksheet.Cells[row, 3].Value.ToEmptyString().Replace("\\n", ""),
                            position = worksheet.Cells[row, 2].Value.ToEmptyString(),
                            city = worksheet.Cells[row, 1].Value.ToEmptyString(),
                            floorprice = floorprice,
                            month = worksheet.Cells[row, 5].Text.ToEmptyString().ConvertToDate().GetValueOrDefault().Month,
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("error:" + ex.Message);
                    }

                }
            }

            return datalist;
        }


    }
}
