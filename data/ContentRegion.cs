using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace picocms.data
{
    public partial class ContentRegion
    {
        #region Public Methods

        /// <summary>
        /// Takes a content region key and html to update it with.  This is how the content gets IN.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="html"></param>
        public static void Update(string key, string html)
        {
            using (var ctx = new picoDataContextDataContext(ContentRegion.ConnectionString))
            {
                var content = GetContentRegion(key, ctx);

                if (content == null)
                    ctx.ContentRegions.InsertOnSubmit(
                        new ContentRegion()
                        {
                            ContentRegionID = Guid.NewGuid(),
                            Key = key,
                            Text = html
                        });

                else
                    content.Text = html;
                
                ctx.SubmitChanges();
            }
        }

        /// <summary>
        /// Returns a content region's html contents by its region code.  This is how the content gets OUT.
        /// </summary>
        /// <param name="regioncode"></param>
        /// <returns></returns>
        public static string Get(string regioncode)
        {
            var content = GetContentRegion(regioncode);
            if (content != null)
                return content.Text;

            return null;
        }

        #endregion


        #region Internals

        /// <summary>
        /// Returns a content region by its key, tied to the data context provided.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        static internal ContentRegion GetContentRegion(string key, picoDataContextDataContext ctx)
        {
            if (ctx == null || String.IsNullOrEmpty(key)) return null;

            var query = from cr in ctx.ContentRegions
                        where cr.Key == key
                        select cr;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Return a disconnected content region by its key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static internal ContentRegion GetContentRegion(string key)
        {
            return GetContentRegion(key, new picoDataContextDataContext(ContentRegion.ConnectionString));
        }

        /// <summary>
        /// Returns the connection string for ConnectionSetting called picocms
        /// </summary>
        static internal string ConnectionString
        {
            get
            {
                if (_connectionstring != null) return _connectionstring;

                // First check for picocms in ConnectionStrings
                var connectionsetting = ConfigurationManager.ConnectionStrings["picocms"];
                if (connectionsetting != null)
                {
                    _connectionstring = connectionsetting.ConnectionString;
                }
                else
                {
                    // Next check for an AppSetting indicating the name of the connection string to use.
                    var appsetting = ConfigurationManager.AppSettings["picocms.ConnectionString"];
                    if (!String.IsNullOrEmpty(appsetting))
                    {
                        // If the appsetting matches a ConnectionString name, use the connection string otherwise use the appsetting as the
                        // actual connection string.
                        connectionsetting = ConfigurationManager.ConnectionStrings[appsetting];
                        
                        _connectionstring = connectionsetting != null ? connectionsetting.ConnectionString : appsetting;
                    }
                }
                // Got this far then there was no connection string indicated.
                if(String.IsNullOrEmpty(_connectionstring))
                    throw new Exception("picocms requires a ConnectionString named picocms.");

                return _connectionstring;
            }
        }
        static string _connectionstring = null;

        #endregion
    }
}
