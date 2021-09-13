using System.Drawing;

namespace NiceShot.DotNetWinFormsClient.Core
{
    public enum ColorType
    {
        Transparent = 0,
        LightPink = 1,
        LightBlue = 2,
        LightGreen = 3,
        Purple = 4
    }

    public class ColorTypeHelper
    {
        public static Color ConverToColor(ColorType colorType)
        {
            var color = Color.Transparent;
            switch (colorType)
            {
                case ColorType.LightPink:
                    color = Color.LightPink;
                    break;
                case ColorType.LightBlue:
                    color = Color.LightBlue;
                    break;
                case ColorType.LightGreen:
                    color = Color.PaleGoldenrod;
                    break;
                case ColorType.Purple:
                    color = Color.Thistle;
                    break;
            }
            return color;
        }
    }
}
