using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KaiserAACCmx
{
	/// <summary>
	/// Summary description for hold.
	/// </summary>
	public partial class hold : XMLPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			System.Text.StringBuilder s = new System.Text.StringBuilder("<?xml version='1.0'?>");
			s.Append("<vxml version=\"2.0\" xmlns=\"http://www.w3.org/2001/vxml\" application=\"start.aspx" + URLParams + "&amp;event=root\">");
			s.Append("<form>");
			s.Append("<property name=\"com.telera.speechenabled\" value=\"false\"/>");
			s.Append("<field type=\"digits\">");
			s.Append("<prompt bargein=\"false\" timeout=\"20s\">");
			s.Append("<audio src=\"" + ConfigurationSettings.AppSettings["voxURL"] + "en/silence.vox\"/>");
			s.Append("</prompt>");
			s.Append("</field>");
			s.Append("</form>");
			s.Append("<catch event=\"connection.disconnect.hangup\">");
			s.Append("<submit next=\"start.aspx" + this.URLParams + "&amp;event=logData\"/>");
			s.Append("</catch>");
			s.Append("<catch event=\"telephone.disconnect.hangup\">");
			s.Append("<submit next=\"start.aspx" + this.URLParams + "&amp;event=logData\"/>");
			s.Append("</catch>");
			s.Append("<catch event=\"error\">");
			s.Append("<submit next=\"GlobalError.aspx\" namelist=\"_event telera.error.name telera.error.description telera.error.currenturl\"/>");
			s.Append("</catch>");
			s.Append("</vxml>");
			Response.ContentType = "text/xml";
			Response.Clear();
			Response.Write(s.ToString());


		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
