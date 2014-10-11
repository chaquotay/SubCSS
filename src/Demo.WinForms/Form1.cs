using System.Windows.Forms;
using SubCSS;

namespace Demo.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var stylesheets = new StyleSheets();
            stylesheets.LoadCss(@"
* { 
    fore-color: red;
    font-family: Tahoma;
}

button { fore-color:blue; }

textbox { text-align: right; }
");
            var styler = new Styler(stylesheets);
            styler.Style(this);
        }
    }
}
