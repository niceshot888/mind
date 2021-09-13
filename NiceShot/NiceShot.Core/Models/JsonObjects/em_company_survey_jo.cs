using Newtonsoft.Json;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class em_company_survey_jo
    {
        public string SecuCode { get; set; }

        public em_company_survey_jbzl jbzl { get; set; }
    }

    public class em_company_survey_jbzl
    {
        public string sshy { get; set; }
        public string sszjhhy { get; set; }
        public string lssws { get; set; }
        public string kjssws { get; set; }
        public string dm { get; set; }
        public string dsz { get; set; }
        public string gsjj { get; set; }
        public string jyfw { get; set; }
        public string gyrs { get; set; }
        public string glryrs { get; set; }
    }

}
