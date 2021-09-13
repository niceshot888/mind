using System.Collections.Generic;

namespace NiceShot.EduWeb.Models
{
    public class douban_book_list
    {
        public douban_book_list()
        {
            data = new List<douban_book>();
        }
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }

        public List<douban_book> data { get; set; }
    }

    public class douban_book
    {
        public long id { get; set; }
        public string book_name { get; set; }
        public decimal? rating_score { get; set; }
        public int? rate_people_num { get; set; }
        public string pub_info { get; set; }
        public string summary { get; set; }
        public string tag { get; set; }
    }
}
