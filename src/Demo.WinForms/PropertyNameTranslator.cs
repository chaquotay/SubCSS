using System.Linq;

namespace Demo.WinForms
{
    internal static class PropertyNameTranslator
    {
        public static string CssToClr(string cssName)
        {
            if (string.IsNullOrEmpty(cssName))
                return cssName;

            var parts = cssName.Split("-".ToCharArray());
            return string.Concat(from part in parts select char.ToUpperInvariant(part[0]) + part.Substring(1));
        }
    }
}