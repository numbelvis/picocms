using System;
using System.Web;
using System.Web.UI;
using picocms.data;

namespace picocms.web
{
    /// <summary>
    /// Control used to create editable regions.  
    /// </summary>
    public class content : UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Webmaster doesnt need to worry about including picocms on every page; it gets included when it is
            // needed and only when the user is authenticated.  This way, it does cost the extra server connection and
            // doesn't present editing overlays to users which cannot edit!
            if (picocms.web.ContentHandler.CanUsePicoCms(HttpContext.Current))
                Page.ClientScript.RegisterClientScriptInclude("picocms", "/picocms.ashx?script");
        }

        /// <summary>
        /// Tag to emit for this element.
        /// </summary>
        public string Tag
        { get; set; }

        /// <summary>
        /// The content region key to use for filling this element.
        /// </summary>
        public string Key
        { get; set; }

        /// <summary>
        /// Override render and spit out the element indicated in Tag.  Any additional attributes applied to this control will also be applied
        /// to the element which is produced, so you can easily use pico:content ALMOST as a regular html tag or replace existing tags with it.
        /// Not 100% simple, BUT EASIER AND SIMPLER than writing an HttpModule which attempts to scrape outgoing html and inject content and also
        /// simpler and more efficient (and search-friendly) than using ajax on the client-side to inject content.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag(Tag);
            writer.WriteAttribute("contentregion", Key);
            foreach (string attr in this.Attributes.Keys)
                writer.WriteAttribute(attr, this.Attributes[attr]);

            writer.Write(">");

            string content = ContentRegion.Get(Key) ?? "";
            writer.Write(content);

            writer.WriteEndTag(Tag);
        }
    }
}
