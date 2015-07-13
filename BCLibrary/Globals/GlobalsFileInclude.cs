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
    [DefaultProperty("FilePath")]
    [ToolboxData("<{0}:GlobalsFileInclude runat=server></{0}:GlobalsFileInclude>")]
    public class GlobalsFileInclude : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        
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