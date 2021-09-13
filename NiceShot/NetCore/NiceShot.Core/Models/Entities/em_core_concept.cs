namespace NiceShot.Core.Models.Entities
{
    public class em_core_concept
    {
        public long id { get; set; }
        public string ts_code { get; set; }
        public string board_name { get; set; }
        public string pinyin { get; set; }
        public string fullpinyin { get; set; }
        public int is_precise { get; set; }
    }
}
