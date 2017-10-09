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
using Qwest.LangPack.VXML21;
using System.IO;

namespace KaiserAACCmx.MessageAdmin
{
	/// <summary>
	/// Summary description for adminEndCall.
	/// </summary>
	public partial class adminEndCall : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{			
				string strMaxError = Request.QueryString["maxerror"] != null ? Request.QueryString["maxerror"].ToString().Trim() : "n";
				string strDir = ConfigurationSettings.AppSettings["recFilePath"];	
				
				//clean up
                File.Delete(strDir + "en\\" + Session["sessionID"].ToString() + "dynamicGlobalMessage.vox");
                File.Delete(strDir + "mx\\" + Session["sessionID"].ToString() + "dynamicGlobalMessage.vox");
                								
				Disconnect d = new Disconnect();
				Prompt p = new Prompt();
				p.TimeOut = 0;
				p.BargeIn = false;
				if(strMaxError=="y")
				{
                    p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "AdminMaxError.vox", true);                    
				}				
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "AdminEndCall.vox", true);
						
				d.setPrompt(p);				
				
				Response.Write(d.getVXML());		
			}
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "General Exception Policy");
            }				
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
