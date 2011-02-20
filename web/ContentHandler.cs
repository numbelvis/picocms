using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using picocms.data;

namespace picocms.web
{
    public class ContentHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // User not authenticated then return nothing.
            if (!CanUsePicoCms(context))
                return;


            var request = context.Request;

            // append ?script to the handler to get the picocms.js script.  Used by the ContentRegionControl
            // as a script source.  
            var script = request.Url.Query.ToLower().StartsWith("?script");
            if (script)
                WritePicoCmsScript(context.Response);

            else
            {
                var region = request["contentregion"];
                var html = request["html"];

                if (!String.IsNullOrEmpty(region))
                    ContentRegion.Update(region, html);
            }
        }

        /// <summary>
        /// This method indicates whether the request contained in the context is allowed to make content
        /// changes with picocms.  You can change the contents of this method to suit your needs, but to start, the user
        /// must be authenticated via whatever ASP.NET authentication you are using.
        /// </summary>
        /// <returns></returns>
        public static bool CanUsePicoCms(HttpContext context)
        {
            //  User must be authenticated to use this handler.  
            //  Want only a certain role to be able to edit content?
            //  Add this:   && Roles.IsUserInRole("your role name")
            return (context != null && context.User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// Take the text from /js/picocms.js and write it to the HttpResponse.
        /// </summary>
        /// <param name="response"></param>
        public static void WritePicoCmsScript(HttpResponse response)
        {
            var app = System.Reflection.Assembly.GetExecutingAssembly();
            var file = app.GetManifestResourceStream("picocms.js.picocms.js");
            var text = new System.IO.StreamReader(file).ReadToEnd();

            response.Write(text);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
