using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    public class TdxDataResult
    {
        public List<TdxResultSets> ResultSets { get; set; }
        public string ErrorCode { get; set; }
        public int ResultSetNum { get; set; }
    }

    public class TdxResultSets
    {
        public List<TdxColDes> ColDes { get; set; }
        public int ColNum { get; set; }

        public List<List<string>> Content { get; set; }
        public int RowNum { get; set; }
    }

    public class TdxColDes
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

}
