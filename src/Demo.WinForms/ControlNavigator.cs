using System.Windows.Forms;
using SubCSS.Matcher;

namespace Demo.WinForms
{
    internal class ControlNavigator : IObjectNavigator
    {
        public string GetType(object subject)
        {
            return GetElementType(subject as Control);
        }

        private string GetElementType(Control ctrl)
        {
            return ctrl != null ? ctrl.GetType().Name.ToLowerInvariant() : null;
        }

        public bool IsPseudoClass(object x, string pseudoClass)
        {
            return false;
        }

        public bool IsClass(object x, string @class)
        {
            return false;
        }
    }
}