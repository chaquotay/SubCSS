using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Demo.WinForms
{
    internal static class CssColorConverter
    {
        private static readonly Regex HexColorRegex = new Regex("^#([0-9a-fA-F]{6})$");

        public static Color? ConvertFrom(object obj)
        {
            Color? c = null;
            var s = obj as string;
            if (s != null)
            {
                var hexMatch = HexColorRegex.Match(s);

                if (hexMatch.Success)
                {
                    var x = hexMatch.Groups[1].Value;
                    var r = ParseHex(x.Substring(0, 2));
                    var g = ParseHex(x.Substring(2, 2));
                    var b = ParseHex(x.Substring(4, 2));
                    c = Color.FromArgb(r, g, b);
                }
                else
                {
                    var knownColor = Color.FromName(s);
                    if (knownColor.IsKnownColor)
                        c = knownColor;
                }
            }
            return c;
        }

        private static int ParseHex(string hex)
        {
            return int.Parse(hex, NumberStyles.HexNumber);
        }
    }
}