using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SubCSS;
using SubCSS.Matcher;

namespace Demo.WinForms
{
    internal class Styler
    {
        private readonly StyleSheets _styleSheets;
        private readonly IObjectNavigator _navigator;

        public Styler(StyleSheets styleSheets)
        {
            _styleSheets = styleSheets;
            _navigator = new ControlNavigator();
        }

        public void Style(Control ctrl)
        {
            if (ctrl == null)
                return;

            var properties = _styleSheets.QueryProperties(ctrl, _navigator);
            ApplyStyle(ctrl, properties);

            foreach (Control child in ctrl.Controls)
            {
                Style(child);
            }
        }

        private void ApplyStyle(Control ctrl, Dictionary<string, object> props)
        {
            foreach (var prop in props)
            {
                if ("font-family".Equals(prop.Key))
                {
                    var fontProp = ctrl.GetType().GetProperty("Font");
                    if (fontProp != null && fontProp.PropertyType == typeof(Font))
                    {
                        var font = fontProp.GetValue(ctrl, null) as Font;
                        if (font != null)
                        {
                            //font = new Font(prop.Value, font.Size, font.Style);
                            font = CssFont.ApplyFamily(font, prop.Value);
                            fontProp.SetValue(ctrl, font, null);
                        }
                    }
                }
                else
                {
                    var clrName = PropertyNameTranslator.CssToClr(prop.Key);

                    var dp = ctrl.GetType().GetProperty(clrName);
                    if (dp != null)
                    {
                        var type = dp.PropertyType;
                        if (typeof(Color) == type)
                        {
                            var c = CssColorConverter.ConvertFrom(prop.Value);
                            if (c.HasValue)
                                dp.SetValue(ctrl, c.Value, null);
                        }
                        else if (type.IsEnum)
                        {
                            var e = EnumConverter.ConvertFrom(type, prop.Value);
                            if (e != null)
                            {
                                dp.SetValue(ctrl, e, null);
                            }
                        }
                    }
                }
            }
        }
    }
}
