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
	/// 9962
	/// </summary>
	public partial class recordGlobalMessage : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string strReRecord = Request.QueryString["reRecord"] != null ? Request.QueryString["reRecord"].ToString().Trim() : "n";
			
				switch (Request.QueryString["event"].ToString().Trim()) 
				{
					case "recordPrompt":
						recordPrompt(Request.QueryString["lang"].ToString().Trim(), strReRecord);
						break;
					case "recordPromptR":
						recordPromptR(Request.QueryString["lang"].ToString().Trim());
						break;
				}
			}
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "General Exception Policy");
            }	
		}

		private void recordPrompt(string strLang, string strReRecord)
		{
						
			if (strReRecord == "y")
			{
				File.Delete(ConfigurationSettings.AppSettings["recFilePath"] + strLang + "\\" + Session["sessionID"].ToString() + "dynamicGlobalMessage" + ".vox");				
			}

			Record r = new Record("newPrompt");
			Prompt p = new Prompt();			
			p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9800.vox", false);
			p.addTTSToPrompt("<break time=\"500\"/>");

			p.BargeIn = false;			
			r.setPrompt(p);				
			r.Beep = true;
			r.FinalSilence = 2;
			r.MaxTime = 300;
			r.VoiceFileFormat = Record.FileFormat.vox2;
			r.NextURL = "recordGlobalMessage.aspx?event=recordPromptR&amp;lang=" + strLang;
					
			r.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "adminEndCall.aspx"));
			
            Response.Write(r.getVXML());
		}

		private void recordPromptR(string strLang)
		{
			HttpPostedFile f = Request.Files["newPrompt"];			
			string fileName = Session["sessionID"].ToString() + "dynamicGlobalMessage";
			f.SaveAs(ConfigurationSettings.AppSettings["recFilePath"] + strLang + "\\" + fileName + ".vox");
			
			URLRedirect u = new URLRedirect();
			u.NextURL = "recordGlobalOptionsMenu.aspx?message=dynamicGlobalMessage&amp;lang=" + strLang;

			Response.Write(u.getVXML());
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
