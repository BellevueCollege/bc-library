using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCLibrary.Globals
{
    /**
    * The GlobalsFileInclude custom control translates the given file path, reads the file, and includes the 
    * content during rendering. This control is necesssary to replace server side includes since those do not 
    * allow dynamic path creation.
    * Example usage: <bc:GlobalsFileInclude runat="server" FilePath="h/gabranded.html" />
    * **/
    [DefaultProperty("FilePath")]
    [ToolboxData("<{0}:GlobalsFileInclude runat=server></{0}:GlobalsFileInclude>")]
    public class GlobalsFileInclude : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        /**
         * Optional parameter. Can be specified to override app-level globals path setting.
         * **/
        public string GlobalsPath
        {
            get
            {
                String s = (String)ViewState["GlobalsPath"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["GlobalsPath"] = value;
            }
        }

        /**
        * Required, partial path to file to include.
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
            string _globalsPath = System.Configuration.ConfigurationManager.AppSettings["GlobalsPath"];
            if (!String.IsNullOrEmpty(GlobalsPath))
            {
                _globalsPath = GlobalsPath;
            }

            if (String.IsNullOrEmpty(FilePath))
            {
                output.Write("");
            }
            else
            {
                try
                {
                    //generate path, read file, and write to page output
                    string appRootPath = System.Web.HttpContext.Current.Server.MapPath("~");
                    string includePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(appRootPath, _globalsPath));
                    System.IO.TextReader tr = new System.IO.StreamReader(includePath + FilePath);
                    output.Write(tr.ReadToEnd());
                }
                catch
                {
                    output.Write("");
                }
            }
        }
    }
}