using Newtonsoft.Json;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using System.Collections.Generic;

namespace NiceShot.Core.Crawlers
{
    public  class SinaCrawler
    {
        private sina_bond_jo _sina_bond_jo;
        public SinaCrawler(sina_bond_jo jo)
        {
            _sina_bond_jo = jo;
        }

        public void CrawlSinaBondData()
        {
            Logger.Info(string.Format("抓取SINA公司债数据"));

            SinaDataAccess.OperateSinaBond(_sina_bond_jo);
        }

    }
}
