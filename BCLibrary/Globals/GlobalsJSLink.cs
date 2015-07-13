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
    /**
     * The GlobalsJSLink custom control is used to translate globals' partial paths into complete <script> 
     * references to globals Javascript resources. This method is necessary both because .NET assumes control of 
     * <script> references as controls, but also because .NET doesn't allow dynamic rendering of paths (at least 
     * not in the needed context).
     * Example usage: <bc:GlobalsJSLink runat="server" FilePath="j/ghead.js?ver=3.3" />
     * Example output (before .NET changes rendering): <script type='text/javascript' src='[globalsURI]j/ghead.js?ver=3.3'></script>
     * **/
    [DefaultProperty("PathAndFile")]
    [ToolboxData("<{0}:GlobalsJSLink runat=server></{0}:GlobalsJSLink>")]
    public class GlobalsJSLink : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        /**
         * Optional parameter. Can be specified to override app-level globals URI setting.
         * **/
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

        /**
         * Partial path to needed globals Javascript resource.
         * **/
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