using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.Core
{
    public enum MarkedAsType
    {
        Normal = 0,
        IsBestBiz = 1,
        PendingResearch = 2,
        FollowUp = 3,
        Other = 4
    }

    public class MarkedAsTypeHelper
    {
        public static Color ConverToColor(MarkedAsType markedAsType)
        {
            var color = Color.Transparent;
            switch (markedAsType)
            {
                case MarkedAsType.Normal:
                    color = Color.Transparent;
                    break;
                case MarkedAsType.IsBestBiz:
                    color = Color.LightBlue;
                    break;
                case MarkedAsType.PendingResearch:
                    color = Color.PaleGoldenrod;
                    break;
                case MarkedAsType.FollowUp:
                    color = Color.LightPink;
                    break;
            }
            return color;
        }
    }

}
