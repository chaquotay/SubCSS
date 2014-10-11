using System.Drawing;
using System.Globalization;

namespace Demo.WinForms
{
    internal static class CssFont
    {
        public static Font ApplyFamily(Font font, object family)
        {
            if (family is string)
            {
                return SetFamily(font, (string)family);
            }
            return font;
        }

        public static Font ApplySize(Font font, object size)
        {
            if (size is string)
            {
                var parsed = ParseFontSize((string)size);
                if (parsed.HasValue)
                    return SetSize(font, parsed.Value);
            }
            return font;
        }

        private static float? ParseFontSize(string fontSize)
        {
            float size;
            if (float.TryParse(fontSize, NumberStyles.Float, CultureInfo.InvariantCulture, out size))
            {
                return size;
            }

            if (fontSize.EndsWith("pt"))
            {
                return ParseFontSize(fontSize.Substring(0, fontSize.Length - 2));
            }

            return null;
        }

        private static Font SetSize(Font f, float size)
        {
            return new Font(f.Name, size, f.Style, f.Unit);
        }

        private static Font SetFamily(Font f, string family)
        {
            var newFont = new Font(family, f.Size, f.Style, f.Unit);
            if (newFont.Name == family)
                return newFont;

            return f;
        }
    }
}