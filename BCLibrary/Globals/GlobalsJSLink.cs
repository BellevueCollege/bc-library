using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BCLibrary.Globals
{
    [DefaultProperty("PathAndFile")]
    [ToolboxData("<{0}:GlobalsJSLink runat=server></{0}:GlobalsJSLink>")]
    public class GlobalsJSLink : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public string GlobalsURI
        {
            get
            {
                String s = (String)ViewState["GlobalsURI"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["GlobalsURI"] = value;
            }
        }

        public string FilePath
        {
            get
            {
                String s = (String)ViewState["FilePath"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["FilePath"] = value;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            string srcPath = System.Configuration.ConfigurationManager.AppSettings["GlobalsURI"] + FilePath;
            if (!String.IsNullOrEmpty(GlobalsURI))
            {
                srcPath = GlobalsURI + FilePath;
            }

            output.Write(String.Format("<script type='text/javascript' src='{0}'></script>", srcPath));
        }
    }
}