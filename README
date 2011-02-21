title:  picocms README
author:  Shawn Kent
targets:  ASP.NET with .NET 3.5
requires:  jQuery 1.4.1 or greater (the one that comes with Visual Studio 2010 projects works ;P)


##What is it and how does it works

###What is it?
Picocms offers a simple way of adding inline copy editing to web pages.  Similar to larger content management systems, this allows website copy to be changed very quick and easily.  Unlike the
larger systems, it offers nothing other than copy editing.  I made this system because some websites I develop are far too simple for a large content management system, but I still want my users to
be able to change their copy without contacting me.


###How does it work?

As simple as I could make it and avoid postbacks!  The system has four parts:

	1. server-side:  .NET UserControl for outputting copy to web pages.
	2. client-side:  javascript for overlays and updating content.
	3. server-side:  HttpHandler for making updates to content.
	4. server-side:  Database

Now I will explain each of these:

####.NET UserControl: content.cs
The control takes a Tag and Key value and outputs an html tag as specified in Tag with its inner html set to the content specified by the Key.  To make it really useful,
I also output ANY OTHER attributes on this control into the html tag.  It is important to use this as you would a normal html tag AND NOT AS YOU WOULD AN ASP.NET TAG!
It really doesn't make sense to fill asp:TextBox with copy content, you can do that yourself if you'd like using the same mechanism I do to get content in content.cs.
The intended use here is to fill simple html elements like div.  So an example of how to fill a div using content.cs is:

	<picocms:content Tag="div" Key="welcomemessage" style="padding:9px" title="Some Title.. is this even valid on divs?">
	</picocms:content>

This will output a tag as such:

	<div contentregion="welcomemessage" style="padding:9px" title="Some Title.. is this even valid on divs?">Content in here is the value stored for the key "welcomemessage"</div>

There are two reasons I am using the picocms:content control:
	1.  It kept me from having to write an HttpModule which scrapes outgoing html and inserts content region copy.  This would be buggy and slower, I believe.
	2.  If I did client-side content inject with ajax calls, then the copy would not get picked up by search engines.  It is important to send our content along in the page so search engines can snag it.

When content.cs is rendered, it gets content from the ASP.NET cache and stuffs it into the control!  So there is caching and having lots of these content controls will not kill your database with connections.


####Javascript for editing content copy
In the web browser, an overlay is added on mouse-over to html tags that contain an attribute: contentregion.  A button is then displayed
so the content of that html can be edited.  When clicked a TextArea appears containing the element's innerHtml.  The user can edit the text
and then click Save or Cancel.  When Save is clicked, the value of the TextArea is inserted into the element and the text is sent back
to the HttpHandler in #2 above.


####HttpHandler
This is an http handler which does two things:
	1.  Returns the picocms.js script (Keeps you from having to copy files around as it is an embedded resource)
	2.  Accepts AUTHENTICATED requests to make content changes.

If you are curious about anything more with the HttpHandler, go take a look.  It is very simple.


####Database
Currently, I am using Linq2Sql to store content in a single database table.  It wouldn't be too hard to get into /data/ContentRegion.cs and change what I've done to suit your own data needs!  Any changes made here are BEHIND the cache so you will retain advantages of the ASP.NET cache even if you change how the data is stored/retrieved.



##What and How to Install

###Database

1.  In the database of your application, you must create a table called picocms_ContentRegion.  You can use this SQL script:

CREATE TABLE [dbo].[picocms_ContentRegion](
	[ContentRegionID] [uniqueidentifier] NOT NULL,
	[Key] [varchar](100) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_picocms_ContentRegion] PRIMARY KEY CLUSTERED 
(
	[ContentRegionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


###web.config changes

1.  Add name of connection string to the database holding table picocms_ContentRegion.

  <appSettings>
    <add key="picocms.ConnectionString" value="The name of your connection string or a connection string itself"/>
  </appSettings>
	
2.  Add the handler used for making updates to <system.web><httpHandlers> section:

	<system.web>
		<httpHandlers>
			<add verb="POST" path="picocms.ashx" type="picocms.web.ContentHandler, picocms" />
		</httpHandlers>
	</system.web>

3.  Add a pages/controls directive to every webpage on your site.

	<pages>
		<controls>
			<add tagPrefix="pico" namespace="picocms.web" assembly="picocms" />
		</controls>
	</pages>


##Your Responsibilities


1.  Authentication!  Picocms checks that certain requests are authenticated before granting them.  For instance, the 
main client-side script is not output when a page is requested unless the user is logged-in.  You must not turn this off!  Otherwise
anyone visiting the site can edit your content.

If you have everything installed and there are no errors, yet you see no editing overlays then most likely it is because you
are not logged-in.




HAVE FUN!