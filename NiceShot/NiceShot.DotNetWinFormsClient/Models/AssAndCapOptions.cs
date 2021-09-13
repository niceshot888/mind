namespace NiceShot.DotNetWinFormsClient.Models
{

    /// <summary>
    /// 其他流动资产类型
    /// </summary>
    public enum othercurrassetype
    {
        /// <summary>
        /// 金融资产
        /// </summary>
        totalfinasset = 1,
        /// <summary>
        /// 营运资产
        /// </summary>
        subtotaloperasset = 2,
        /// <summary>
        /// 营运负债
        /// </summary>
        totaloperliab = 3
    }

    /// <summary>
    /// 其他流动负债类型
    /// </summary>
    public enum othercurreliabitype
    {
        /// <summary>
        /// 营运资产
        /// </summary>
        subtotaloperasset = 1,
        /// <summary>
        /// 营运负债
        /// </summary>
        totaloperliab = 2,
        /// <summary>
        /// 短期债务
        /// </summary>
        totalshorttermliab = 3,
    }

    /// <summary>
    /// 一年内到期的非流动资产类型
    /// </summary>
    public enum expinoncurrassettype
    {
        /// <summary>
        /// 金融资产
        /// </summary>
        totalfinasset = 1,
        /// <summary>
        /// 营运资产
        /// </summary>
        subtotaloperasset = 2,
    }

}
