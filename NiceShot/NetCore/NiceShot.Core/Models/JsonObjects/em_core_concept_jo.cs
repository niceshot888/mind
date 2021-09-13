using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class em_core_concept_jo
    {
        public List<em_core_concept_item> ssbk { get; set; }
    }

    public class em_core_concept_item
    {
        public string SECUCODE { get; set; }
        public string BOARD_NAME { get; set; }
        public string IS_PRECISE { get; set; }
    }
}
