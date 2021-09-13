namespace NiceShot.Core.Models.Entities
{
    public class sina_bond
    {
        public long id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public decimal? trade { get; set; }
        public decimal? changepercent { get; set; }
        public decimal? settlement { get; set; }
        
    }
}
