using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NBD_TractionFive.Utilities
{
    public class MaintainURL
    {
        public static string ReturnURL(HttpContext _context, string ControllerName)
        {
            string cookieName = ControllerName + "URL";
            string SearchText = "/" + ControllerName + "?";
            //Get the URL of the page that sent us here
            string returnURL = _context.Request.Headers["Referer"].ToString();
            if (returnURL.Contains(SearchText))
            {
                //Came here from the Index with some parameters
                //Save the Parameters in a Cookie
                returnURL = returnURL.Substring(returnURL.LastIndexOf(SearchText));
                CookieHelper.CookieSet(_context, cookieName, returnURL, 30);
                return returnURL;
            }
            else
            {
                //Get it from the Cookie
                //Note that this might return an empty string but we will
                //change it to go back to the Index of the Controller.
                returnURL = _context.Request.Cookies[cookieName];
                return returnURL ?? "/" + ControllerName;
            }
        }
    }
}
