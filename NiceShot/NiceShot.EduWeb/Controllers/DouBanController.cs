using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.EduWeb.Models;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace NiceShot.EduWeb.Controllers
{
    public class DouBanController : Controller
    {
        public ActionResult BookList()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult GetBookList(string tag, string bookname, string keywords, string rating_score, string rate_people_num, string sort)
        {
            string sqlSelect = @"SELECT * from douban_book 
where 1 = 1
$wherecond$
and rate_people_num>= {1}
and rating_score>= {0}
order by {2} desc";

            var sbWhereCond = new StringBuilder();
            if (!string.IsNullOrEmpty(tag))
                sbWhereCond.AppendFormat(" and tag like '%{0}%' ", tag);
            if (!string.IsNullOrEmpty(bookname))
                sbWhereCond.AppendFormat(" and book_name like '%{0}%' ", bookname);
            if (!string.IsNullOrEmpty(keywords))
                sbWhereCond.AppendFormat(" and (tag like '%{0}%' or summary like '%{0}%' or book_name like '%{0}%') ", keywords);
            
            Logger.Info(sqlSelect);

            sqlSelect = string.IsNullOrEmpty(sbWhereCond.ToString()) ? sqlSelect.Replace("$wherecond$", string.Empty) : sqlSelect.Replace("$wherecond$", sbWhereCond.ToString());

            using (var conn = new MySqlConnection(MySqlDbHelper.BIGDATADB_CONN_STRING))
            {
                sqlSelect = string.Format(sqlSelect, rating_score, rate_people_num, sort);
                var books = conn.Query<douban_book>(sqlSelect, new
                {
                    tag = tag
                }).ToList();

                var data_list = new douban_book_list
                {
                    code = 0,
                    count = books.Count,
                    data = books,
                    msg = ""
                };

                return Json(data_list, JsonRequestBehavior.AllowGet);
            }

        }

    }
}