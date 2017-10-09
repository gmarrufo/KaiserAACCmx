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
using Qwest.LangPack.VXML21;
using System.IO;
using System.Configuration;

namespace KaiserAACCmx.MessageAdmin
{
	/// <summary>
	/// Summary description for GlobalError.
	/// </summary>
	public partial class GlobalError : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string errorEvent = Request.QueryString["_event"] != null ? Request.QueryString["_event"].ToString().Trim()  : "";
			string errorName = Request.QueryString["telera.error.name"] != null ? Request.QueryString["telera.error.name"].ToString().Trim()  : "";
			string errorDes = Request.QueryString["telera.error.description"] != null ? Request.QueryString["telera.error.description"].ToString().Trim()  : "";
			string strDir = ConfigurationSettings.AppSettings["recFilePath"];	
				
			//clean up
			File.Delete(strDir + "en\\" + Session["sessionID"].ToString() + "dynamicGlobalMessage.vox");
			File.Delete(strDir + "mx\\" + Session["sessionID"].ToString() + "dynamicGlobalMessage.vox");

            Disconnect d = new Disconnect();
			Response.Write(d.getVXML());
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
