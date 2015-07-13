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
        /**
         * This snazzy function updates all href attributes for elements with IDs that begin with the specified identifier. 
         * The href is updated to the complete globals path. Typical usage is in a code behind file for updating CSS controls 
         * dynamically. Necessary because .NET doesn't allow dynamic generation of paths in some contexts.
         * Example usage: <link id="[identifier]restofyourID" rel="stylesheet" href="c/g.css?ver=3.3">
         * Example output (before .NET rendering changes): <link id="globalscssg" rel="stylesheet" href="[globalsURI]c/g.css?ver=3.3">
         * **/
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

        /**
         * Override function that allows parseCssControls to be called without specifying the globals URI.
         * **/
        public static void parseCssControls(ControlCollection all, String identifier)
        {
            String _globalsURI = ConfigurationManager.AppSettings["GlobalsURI"];
            parseCssControls(all, identifier, _globalsURI);
        }
    }
}