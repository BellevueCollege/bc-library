using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace BCLibrary.Globals
{
    public class Functions
    {
        public static void parseCssControls(ControlCollection all, String identifier)
        {
            String _globalsURI = ConfigurationManager.AppSettings["GlobalsURI"];
            parseCssControls(all, identifier, _globalsURI);
        }
        public static void parseCssControls(ControlCollection all, String identifier, String globalsURI)
        {
            /** Loop through all controls to find all globals-related CSS links and update hrefs to use globals 
             * URI.
             * */
            foreach (Control c in all)
            {
                if (!String.IsNullOrEmpty(c.ID) && c.ID.StartsWith(identifier, true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    HtmlLink stylesheet = (HtmlLink)c;
                    string href = globalsURI + stylesheet.Href;
                    stylesheet.Href = href;
                }

                if (c.HasControls())
                {
                    parseCssControls(c.Controls, identifier, globalsURI);
                }
            }
        }
    }
}