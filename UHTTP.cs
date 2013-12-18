using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace com.lexnetcrm.common
{
    public class UHTTP
    {
        public static string buildUrl(string baseUrl, string[] urlValues)
        {
            if (String.IsNullOrEmpty(baseUrl) == true)
            {
                return baseUrl;
            }

            if (urlValues == null || urlValues.Length == 0)
            {
                return baseUrl;
            }

            return String.Format(baseUrl, urlValues);
        }

        private static bool IsValidPath(string path)
        {
            if (path.Trim().EndsWith("/"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ValidateURLSeparator(string path)
        {
            if (IsValidPath(path) == true)
            {
                return path;
            }
            else
            {
                return path.Trim() + "/";
            }
        }

        public static HttpWebResponse getWebSiteResponse(string url)
        {
            // Create the web request  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (InvalidOperationException e)
            {
                //ULogging.writeToErrorLog(AppGlobal.getAppLogger(), "InvalidOperationException in getWebSiteResponse", e);
            }
            catch (NotSupportedException e)
            {
                //ULogging.writeToErrorLog(AppGlobal.getAppLogger(), "NotSupportedException in getWebSiteResponse", e);
            }
            catch (Exception e)
            {
                //ULogging.writeToErrorLog(AppGlobal.getAppLogger(), "Exception in getWebSiteResponse", e);
            }

            return response;
        }
    }
}
