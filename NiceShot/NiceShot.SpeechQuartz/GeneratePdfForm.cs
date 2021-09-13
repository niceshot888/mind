using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NiceShot.SpeechQuartz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceShot.SpeechQuartz
{
    public partial class GeneratePdfForm : Form
    {
        private IList<Dictionary<string, object>> _allbookmarks = new List<Dictionary<string, object>>();
        public GeneratePdfForm()
        {
            InitializeComponent();


            //var splitted = input.Split(new[] { ' ' }, 2);

            var sourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files\\swgwsm.pdf");
            var outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");


            using (var pdfReader = new PdfReader(sourceFile))
            {
                _allbookmarks = SimpleBookmark.GetBookmark(pdfReader);
            }

            SplitPdf(sourceFile, outputDir, 1, 611, "大脑与认知", 0);
            SplitPdf(sourceFile, outputDir, 612, 1224, "地球", 1);
            SplitPdf(sourceFile, outputDir, 1225, 1838, "电子与信息",2);
            SplitPdf(sourceFile, outputDir, 1839, 2500, "动物",3);
            SplitPdf(sourceFile, outputDir, 2501, 3128, "古生物",4);
            SplitPdf(sourceFile, outputDir, 3129, 3759, "海洋",5);
            SplitPdf(sourceFile, outputDir, 3760, 4315, "航空与航天",6);
            SplitPdf(sourceFile, outputDir, 4316, 4964, "化学",7);
            SplitPdf(sourceFile, outputDir, 4965, 5593, "建筑与交通",8);
            SplitPdf(sourceFile, outputDir, 5594, 6121, "能源与环境",9);
            SplitPdf(sourceFile, outputDir, 6122, 6687, "生命",10);
            SplitPdf(sourceFile, outputDir, 6688, 7277, "数学",11);
            SplitPdf(sourceFile, outputDir, 7278, 7932, "天文",12);
            SplitPdf(sourceFile, outputDir, 7933, 8626, "武器与国防",13);
            SplitPdf(sourceFile, outputDir, 8627, 9269, "物理",14);
            SplitPdf(sourceFile, outputDir, 9270, 9822, "医学",15);
            SplitPdf(sourceFile, outputDir, 9823, 10319, "灾难与防护",16);
            SplitPdf(sourceFile, outputDir, 10320, 10858, "植物",17);
        }

        private IList<Dictionary<string, object>> GetBookMarks(IList<Dictionary<string, object>> allbookmarks, int startPage, int index)
        {
            IList<Dictionary<string, object>> bookmarks = new List<Dictionary<string, object>>();

            var first_dic = new Dictionary<string, object>();
            var sb = new StringBuilder();

            foreach (KeyValuePair<string, object> kvp in allbookmarks[index])
            {
                if (kvp.Value is List<Dictionary<string, object>>)
                {
                    sb.AppendLine(string.Format("Key = {0}, Value = ", kvp.Key, "List"));
                    var child_list = kvp.Value as List<Dictionary<string, object>>;
                    var new_child_list = new List<Dictionary<string, object>>();
                    foreach (Dictionary<string, object> child_dic in child_list)
                    {
                        var new_child_dic = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, object> child_kvp in child_dic)
                        {
                            sb.AppendLine(string.Format("Key = {0}, Value = {1}", child_kvp.Key, child_kvp.Value));

                            if (child_kvp.Value is List<Dictionary<string, object>>)
                            {
                                sb.AppendLine(string.Format("Key = {0}, Value = ", kvp.Key, "List"));
                                var third_list = child_kvp.Value as List<Dictionary<string, object>>;
                                var new_third_list = new List<Dictionary<string, object>>();
                                foreach (Dictionary<string, object> third_dic in third_list)
                                {
                                    var new_third_dic = new Dictionary<string, object>();
                                    foreach (KeyValuePair<string, object> third_kvp in third_dic)
                                    {
                                        sb.AppendLine(string.Format("Key = {0}, Value = {1}", third_kvp.Key, third_kvp.Value));
                                        //new_third_dic.Add(third_kvp.Key, third_kvp.Value);
                                        if (third_kvp.Key == "Page")
                                        {
                                            var third_val = third_kvp.Value.ToString().Split(new[] { ' ' }, 2);
                                            new_third_dic.Add(third_kvp.Key, string.Format("{0} {1}", int.Parse(third_val[0]) - startPage + 1, third_val[1]));
                                        }
                                        else
                                        {
                                            new_third_dic.Add(third_kvp.Key, third_kvp.Value);
                                        }

                                    }
                                    new_third_list.Add(new_third_dic);
                                }
                                new_child_dic.Add(child_kvp.Key, new_third_list);
                            }
                            else
                            {
                                sb.AppendLine(string.Format("Key = {0}, Value = {1}", child_kvp.Key, child_kvp.Value));

                                if (child_kvp.Key == "Page")
                                {
                                    var child_val = child_kvp.Value.ToString().Split(new[] { ' ' }, 2);
                                    new_child_dic.Add(child_kvp.Key, string.Format("{0} {1}", int.Parse(child_val[0]) - startPage + 1, child_val[1]));
                                }
                                else
                                {
                                    new_child_dic.Add(child_kvp.Key, child_kvp.Value);
                                }


                            }
                        }
                        new_child_list.Add(new_child_dic);
                    }
                    first_dic.Add(kvp.Key, new_child_list);
                }
                else
                {
                    sb.AppendLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                    first_dic.Add(kvp.Key, kvp.Value);
                }
            }

            bookmarks.Add(first_dic);
            //PrintDic(dic,sb);

            Console.WriteLine(sb.ToString());
            return bookmarks;
        }

        private void PrintDic(Dictionary<string, object> dic, StringBuilder sb)
        {
            foreach (KeyValuePair<string, object> kvp in dic)
            {
                PrintChildDic(kvp, sb);
            }
        }

        private void PrintChildDic(KeyValuePair<string, object> kvp, StringBuilder sb)
        {
            if (kvp.Value is List<Dictionary<string, object>>)
            {
                sb.AppendLine(string.Format("Key = {0}, Value = ", kvp.Key, "List"));
                var child_list = kvp.Value as List<Dictionary<string, object>>;
                foreach (Dictionary<string, object> child_dic in child_list)
                {
                    StringBuilder sbchild = new StringBuilder();
                    foreach (KeyValuePair<string, object> child_kvp in child_dic)
                    {
                        sbchild.AppendLine(string.Format("Key = {0}, Value = {1}", child_kvp.Key, child_kvp.Value));

                        PrintChildDic(child_kvp, sb);
                    }
                    sb.AppendLine(sbchild.ToString());
                }
            }
            else
            {
                sb.AppendLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }
        }

        private void SplitPdf(string pdfFilePath, string outputPath, int startPage, int endPage, string pdfFileName, int bookindex)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();

                for (int pagenumber = startPage; pagenumber <= endPage; pagenumber++)
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, pagenumber));
                    }
                    else
                    {
                        break;
                    }

                }

                var bookmarks = GetBookMarks(_allbookmarks, startPage, bookindex);
                copy.Outlines = bookmarks;

                document.Close();
            }
        }

        private void SplitPdf2(string pdfFilePath, string outputPath, int startPage, int interval, string pdfFileName)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();

                for (int pagenumber = startPage; pagenumber < (startPage + interval); pagenumber++)
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, pagenumber));
                    }
                    else
                    {
                        break;
                    }

                }

                document.Close();
            }
        }

        public void GeneratePdf()
        {
            var pageList = new List<PageHomeIndex>
            {
                new PageHomeIndex{ Title="写给孩子的哲学启蒙书(橙色卷·漫画绘本)", Url="https://www.zhlzw.com/lzsj/xll/89808.html"},
                new PageHomeIndex{ Title="写给孩子的哲学启蒙书(紫色卷·漫画绘本)", Url="https://www.zhlzw.com/lzsj/xll/93299.html"},
                new PageHomeIndex{ Title="写给孩子的哲学启蒙书(绿色卷·漫画绘本)", Url="https://www.zhlzw.com/lzsj/xll/89809.html"},
                new PageHomeIndex{ Title="", Url=""},
            };

            var web = new HtmlWeb();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            web.OverrideEncoding = Encoding.GetEncoding("gb2312");
            var doc = web.Load("");

            var lsgbbdtab = doc.GetElementbyId("lsgbbdtab");
        }

    }




}
