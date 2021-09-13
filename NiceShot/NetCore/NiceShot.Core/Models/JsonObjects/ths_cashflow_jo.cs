using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class ths_cashflow_jo
    {
        public ths_cashflow_flashData flashData { get; set; }
        public ths_cashflow_fieldflashData fieldflashData { get; set; }
    }

    public class ths_cashflow_flashData
    {
        public object[] title { get; set; }
        public object[] report { get; set; }
    }

    public class ths_cashflow_fieldflashData
    {

    }
}
