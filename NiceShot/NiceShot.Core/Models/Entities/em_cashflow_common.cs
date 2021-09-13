using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace NiceShot.Core.Models.Entities
{
    public class em_cashflow_common
    {
        public long id { get; set; }
        public string securitycode { get; set; }
        public int reporttype { get; set; }
        public int type { get; set; }
        public DateTime? reportdate { get; set; }
        public decimal? salegoodsservicerec { get; set; }
        public decimal? nideposit { get; set; }
        public decimal? niborrowfromcbank { get; set; }
        public decimal? niborrowfromfi { get; set; }
        public decimal? premiumrec { get; set; }
        public decimal? netrirec { get; set; }
        public decimal? niinsureddepositinv { get; set; }
        public decimal? nidisptradefasset { get; set; }
        public decimal? intandcommrec { get; set; }
        public decimal? niborrowfund { get; set; }
        public decimal? ndloanadvances { get; set; }
        public decimal? nibuybackfund { get; set; }
        public decimal? taxreturnrec { get; set; }
        public decimal? otheroperaterec { get; set; }
        public decimal? sumoperateflowin { get; set; }
        public decimal? buygoodsservicepay { get; set; }
        public decimal? niloanadvances { get; set; }
        public decimal? nidepositincbankfi { get; set; }
        public decimal? indemnitypay { get; set; }
        public decimal? intandcommpay { get; set; }
        public decimal? divipay { get; set; }
        public decimal? employeepay { get; set; }
        public decimal? taxpay { get; set; }
        public decimal? otheroperatepay { get; set; }
        public decimal? sumoperateflowout { get; set; }
        public decimal? netoperatecashflow { get; set; }
        public decimal? disposalinvrec { get; set; }
        public decimal? invincomerec { get; set; }
        public decimal? dispfilassetrec { get; set; }
        public decimal? dispsubsidiaryrec { get; set; }
        public decimal? reducepledgetdeposit { get; set; }
        public decimal? otherinvrec { get; set; }
        public decimal? suminvflowin { get; set; }
        public decimal? buyfilassetpay { get; set; }
        public decimal? invpay { get; set; }
        public decimal? nipledgeloan { get; set; }
        public decimal? getsubsidiarypay { get; set; }
        public decimal? addpledgetdeposit { get; set; }
        public decimal? otherinvpay { get; set; }
        public decimal? suminvflowout { get; set; }
        public decimal? netinvcashflow { get; set; }
        public decimal? acceptinvrec { get; set; }
        public decimal? subsidiaryaccept { get; set; }
        public decimal? loanrec { get; set; }
        public decimal? issuebondrec { get; set; }
        public decimal? otherfinarec { get; set; }
        public decimal? sumfinaflowin { get; set; }
        public decimal? repaydebtpay { get; set; }
        public decimal? diviprofitorintpay { get; set; }
        public decimal? subsidiarypay { get; set; }
        public decimal? buysubsidiarypay { get; set; }
        public decimal? otherfinapay { get; set; }
        public decimal? subsidiaryreductcapital { get; set; }
        public decimal? sumfinaflowout { get; set; }
        public decimal? netfinacashflow { get; set; }
        public decimal? effectexchangerate { get; set; }
        public decimal? nicashequi { get; set; }
        public decimal? cashequibeginning { get; set; }
        public decimal? cashequiending { get; set; }

        //现金流量表补充资料
        public decimal? asseimpa { get; set; }
        public decimal? assedepr { get; set; }
        public decimal? intaasseamor { get; set; }
        public decimal? longdefeexpenamor { get; set; }
        public decimal? dispfixedassetloss { get; set; }
        public decimal? fixedassescraloss { get; set; }
        public decimal? valuechgloss { get; set; }
        public decimal? realestadep { get; set; }

    }
}
