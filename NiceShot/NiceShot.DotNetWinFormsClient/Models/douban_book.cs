namespace NiceShot.DotNetWinFormsClient.Models
{
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
