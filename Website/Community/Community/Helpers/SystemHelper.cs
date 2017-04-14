using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class SystemHelper
    {
        /***************************************************************************************
        * Title: ResolveServerUrl
        * Author: Wood, Jonathon
        * Date: 2012
        * Code version: 1.0
        * Availability: http://stackoverflow.com/questions/2069922/getting-full-url-of-any-file-in-asp-net-mvc
        * Purpose: Convert relative file path to absolute path
        **************************************************************************************/
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
    }
}
