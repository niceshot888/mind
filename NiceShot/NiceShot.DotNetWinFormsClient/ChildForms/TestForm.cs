using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            //SyncDouBanBookData();
            var tag_arr = new[] { "通信","逻辑","郭敬明","金庸","金融","钱钟书","阿加莎·克里斯蒂","随笔","青春","青春文学","韩寒","音乐","高木直子","魔幻","鲁迅" };
            //"摄影","政治","政治学","教育","散文","散文随笔","数学","文化","文学","方法论","旅行","日本","日本文学","日本漫画","杂文","村上春树","杜拉斯","校园","武侠","沧月","港台","游记","漫画","灵修","爱情","物理","王小波","理财","生活","用户体验","电影","社会","社会学","神经网络","科学","科幻","科幻小说","科技","科普","程序","穿越","童话","策划","算法","管理","米兰·昆德拉","经典","经济","经济学","绘本","绘画","编程","网络小说","美术","美食","考古","耽美","职场","股票","自助游","自我管理","自由主义","艺术","艺术史","英语学习","茨威格","营销","西方哲学","言情","计算机","设计","诗歌","诗词","轻小说","近代史",
            //"J.K.罗琳","UCD","UE","web","三毛","东野圭吾","两性","中国历史","中国文学","二战","互联网","交互","交互设计","亦舒","人性","人文","人物传记","人际关系","企业史","传记","余华","余秋雨","佛教","作文","健康","儿童文学","养生","军事","几米","创业","励志","化学","医学","历史","古典文学","古龙","名著","哲学","商业","回忆录","国学","外国名著","外国文学","奇幻","女性","好书，值得一读","学习方法","安妮宝贝","宗教","家居","小说","工具书","幾米","广告","建筑","张小娴","张悦然","张爱玲","当代文学","心理","心理学","思想","思维","悬疑","情感","戏剧","成长","我想读这本书","手工","投资","推理","推理小说",
            //var tag_arr = new[] { "宗教", "电影", "回忆录", "国学", "艺术史", "音乐", "戏剧", "西方哲学", "近代史", "二战", "军事", "佛教", "考古", "自由主义", "爱情", "生活", "心理", "女性", "旅行", "摄影", "职场", "美食", "灵修", "健康", "情感", "人际关系", "两性", "养生", "手工", "家居", "自助游", "经济", "投资", "营销", "理财", "创业", "股票", "广告", "企业史", "策划", "用户体验", "科技", "web", };
            //"社会", "政治学", "宗教", "电影", "回忆录", "国学", "艺术史", "音乐", "戏剧", "西方哲学", "近代史", "二战", "军事", "佛教", "考古", "自由主义", "爱情", "生活", "心理", "女性", "旅行", "摄影", "职场", "美食", "灵修", "健康", "情感", "人际关系", "两性", "养生", "手工", "家居", "自助游", "经济", "投资", "营销", "理财", "创业", "股票", "广告", "企业史", "策划", "用户体验", "科技", "web", "交互", "通信", "UE", "神经网络", "UCD", "程序"
            //"外国文学", "文学", "中国文学", "日本文学", "村上春树", "古典文学", "余华", "王小波", "当代文学", "杂文", "张爱玲", "外国名著", "鲁迅", "钱钟书", "茨威格", "米兰·昆德拉", "杜拉斯", "港台", "漫画", "推理", "东野圭吾", "悬疑", "青春", "科幻", "言情", "推理小说", "奇幻", "武侠", "日本漫画", "耽美", "科幻小说",
            // "网络小说", "韩寒", "三毛", "亦舒", "阿加莎·克里斯蒂", "金庸", "安妮宝贝", "穿越", "轻小说", "魔幻", "郭敬明", "青春文学", "几米", "J.K.罗琳", "幾米", "张小娴", "校园", "古龙", "高木直子", "沧月", "余秋雨", "张悦然", "社会", "政治学", "宗教", "电影", "回忆录", "国学", "艺术史", "音乐", "戏剧", "西方哲学", "近代史", "二战", "军事", "佛教", "考古", "自由主义", "爱情", "生活", "心理", "女性", "旅行", "摄影", "职场", "美食", "灵修", "健康", "情感", "人际关系", "两性", "养生", "手工", "家居", "自助游", "经济", "投资", "营销", "理财", "创业", "股票", "广告", "企业史", "策划", "用户体验", "科技", "web", "交互", "通信", "UE", "神经网络", "UCD", "程序" 
            foreach (var tag in tag_arr)
            {
                SyncDouBanBookDataByOneSheet(tag);
            }

            //GetAllTags();
        }

        private void SyncDouBanBookDataByOneSheet(string tag)
        {
            var filePath = string.Format(@"F:\Projects\pythontests\files\book_list_{0}.xlsx", tag);

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Sheet"];

                int rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    var book_name = worksheet.Cells[row, 1].Value.ToEmptyString();
                    var rating_score = StringUtils.ConvertToNotNullDecimal(worksheet.Cells[row, 2].Value.ToEmptyString());
                    var rate_people_num = StringUtils.ConvertToNotNullInt(worksheet.Cells[row, 3].Value.ToEmptyString());
                    var pub_info = worksheet.Cells[row, 4].Value.ToEmptyString();
                    var summary = worksheet.Cells[row, 5].Value.ToEmptyString();
                    var exists_book = GetDouBanBook(book_name);
                    if (exists_book == null)
                    {
                        var doubanbook = new douban_book
                        {
                            book_name = book_name,
                            rating_score = rating_score,
                            rate_people_num = rate_people_num,
                            pub_info = pub_info,
                            summary = summary,
                            tag = tag
                        };
                        InsertDouBanBook(doubanbook);
                    }
                    else
                    {
                        if (exists_book.rate_people_num < rate_people_num)
                        {
                            exists_book.rating_score = rating_score;
                            exists_book.rate_people_num = rate_people_num;
                            exists_book.summary = summary;

                            var tag_arr = exists_book.tag.Split(',');
                            if (!tag_arr.Contains(tag))
                            {
                                exists_book.tag = exists_book.tag + "," + tag;
                            }

                            UpdateDouBanBook(exists_book);
                        }
                    }
                }
            }

        }

        private void SyncDouBanBookData()
        {
            var datalist = new List<douban_book>();
            string rootDir = @"F:\Projects\pythontests\files";

            var files = new DirectoryInfo(rootDir).GetFiles();
            foreach (var file in files)
            {
                var filePath = file.FullName;
                var tag = file.Name.Replace("book_list_", "").Replace(".xlsx", "");

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets["Sheet"];

                    int rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var book_name = worksheet.Cells[row, 1].Value.ToEmptyString();
                        var rating_score = StringUtils.ConvertToNotNullDecimal(worksheet.Cells[row, 2].Value.ToEmptyString());
                        var rate_people_num = StringUtils.ConvertToNotNullInt(worksheet.Cells[row, 3].Value.ToEmptyString());
                        var pub_info = worksheet.Cells[row, 4].Value.ToEmptyString();
                        var summary = worksheet.Cells[row, 5].Value.ToEmptyString();
                        var exists_book = datalist.FirstOrDefault(d => d.book_name == book_name);
                        if (exists_book == null)
                        {
                            datalist.Add(new douban_book
                            {
                                book_name = book_name,
                                rating_score = rating_score,
                                rate_people_num = rate_people_num,
                                pub_info = pub_info,
                                summary = summary,
                                tag = tag
                            });
                        }
                        else
                        {
                            if (exists_book.rating_score < rating_score)
                            {
                                exists_book.rating_score = rating_score;
                                exists_book.rate_people_num = rate_people_num;
                                exists_book.summary = summary;
                            }
                            //Console.WriteLine(string.Format("name:{0},rating_score={1},rate_people_num={2},pub_info={3}", book_name, rating_score, rate_people_num, pub_info));
                        }
                    }
                }
            }

            Console.WriteLine(datalist);

            foreach (var item in datalist)
            {
                InsertDouBanBook(item);
            }

        }

        private void InsertDouBanBook(douban_book book)
        {
            using (var conn = new MySqlConnection(MySqlDbHelper.BIGDATADB_CONN_STRING))
            {
                string sqlInsert = "insert into douban_book(book_name,rating_score,rate_people_num,pub_info,summary,tag) values(@book_name,@rating_score,@rate_people_num,@pub_info,@summary,@tag)";

                conn.Execute(sqlInsert, new
                {
                    book_name = FiliterEmojo(book.book_name),
                    rating_score = book.rating_score,
                    rate_people_num = book.rate_people_num,
                    pub_info = FiliterEmojo(book.pub_info),
                    summary = FiliterEmojo(book.summary),
                    tag = book.tag
                });

                Console.WriteLine(string.Format("insert name={0},rating_score={1},rate_people_num={2},tag={3}", book.book_name, book.rating_score, book.rate_people_num, book.tag));
            }
        }

        private void UpdateDouBanBook(douban_book book)
        {
            using (var conn = new MySqlConnection(MySqlDbHelper.BIGDATADB_CONN_STRING))
            {
                string sqlUpdate = "update douban_book set rating_score=@rating_score,rate_people_num=@rate_people_num,tag=@tag where id=@id";

                conn.Execute(sqlUpdate, new
                {
                    id = book.id,
                    rating_score = book.rating_score,
                    rate_people_num = book.rate_people_num,
                    tag = book.tag
                });

                Console.WriteLine(string.Format("update name={0},rating_score={1},rate_people_num={2},tag={3}", book.book_name, book.rating_score, book.rate_people_num, book.tag));
            }
        }

        private douban_book GetDouBanBook(string bookname)
        {
            using (var conn = new MySqlConnection(MySqlDbHelper.BIGDATADB_CONN_STRING))
            {
                string sqlSelect = "select * from douban_book where book_name=@book_name";

                return conn.QueryFirstOrDefault<douban_book>(sqlSelect, new
                {
                    book_name = bookname
                });
            }
        }

        private string FiliterEmojo(string str)
        {
            foreach (var a in str)
            {
                byte[] bts = Encoding.UTF32.GetBytes(a.ToString());

                if (bts[0].ToString() == "253" && bts[1].ToString() == "255")
                {
                    str = str.Replace(a.ToString(), "");
                }

            }
            return str;
        }

        private void GetAllTags()
        {
            string rootDir = @"F:\Projects\pythontests\files";

            var files = new DirectoryInfo(rootDir).GetFiles();
            var sbTags = new StringBuilder();
            foreach (var file in files)
            {
                var tag = file.Name.Replace("book_list_", "").Replace(".xlsx", "");
                sbTags.Append("\"" + tag + "\",");
            }

            Console.WriteLine(sbTags.ToString().TrimEnd(','));

        }

    }
}
