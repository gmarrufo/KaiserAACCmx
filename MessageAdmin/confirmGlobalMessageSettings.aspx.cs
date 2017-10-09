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


namespace KaiserAACCmx.MessageAdmin
{
	/// <summary>
	/// 9999
	/// </summary>
	public partial class confirmGlobalMessageSettings : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{				
				Play ply = new Play();
				Prompt p = new Prompt();
				p.TimeOut = 0;
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9999.vox", true);
				p.BargeIn = false;
				ply.setPrompt(p);
				ply.NextURL = "adminEndCall.aspx";
				ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "adminEndCall.aspx"));
				
				Response.Write(ply.getVXML());				
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
