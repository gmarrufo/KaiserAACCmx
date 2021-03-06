using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Qwest.LangPack.VXML21;


namespace KaiserAACCmx.MessageAdmin
{
	/// <summary>
	/// 9984
	/// </summary>
	public partial class playPreRecorded : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{				
				Prompt p = new Prompt();
				p.BargeIn = true;
				p.TimeOut = 5;
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9984a.vox", true);
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + Request.QueryString["lang"].ToString().Trim() + "/dynamicGlobalMessage" + Request.QueryString["message"].ToString().Trim() + ".vox", true, "Dynamic global message not found");				
								
				Menu m = new Menu("mssgMenu");
				
				if (Request.QueryString["lang"].ToString().Trim() == "en")
				{
					p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9984b.vox", true);
					m.addMenuChoice("1", "playPreRecorded.aspx?lang=mx&amp;message=" + Request.QueryString["message"].ToString().Trim());
					
					m.addCatchBlock(getCatchEvent(new string[]{"9000nr.vox", "9984b.vox", "9984c.vox"}, 1, CatchEvent.NO_INPUT, ""));
					m.addCatchBlock(getCatchEvent(new string[]{"9000nr.vox", "9984b.vox", "9984c.vox"}, 2, CatchEvent.NO_INPUT, ""));
					
					m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9984b.vox", "9984c.vox"}, 1, CatchEvent.NO_MATCH, ""));
					m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9984b.vox", "9984c.vox"}, 2, CatchEvent.NO_MATCH, ""));
					
				}
				else
				{
					m.addCatchBlock(getCatchEvent(new string[]{"9000nr.vox", "9984c.vox"}, 1, CatchEvent.NO_INPUT, ""));
					m.addCatchBlock(getCatchEvent(new string[]{"9000nr.vox", "9984c.vox"}, 2, CatchEvent.NO_INPUT, ""));
					
					m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9984c.vox"}, 1, CatchEvent.NO_MATCH, ""));
					m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9984c.vox"}, 2, CatchEvent.NO_MATCH, ""));
					
				}

				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9984c.vox", true);
				
				m.addMenuChoice("2", "saveMessageAndTurnOn.aspx?delete=n&amp;lang=" + Request.QueryString["lang"].ToString().Trim() + "&amp;message=dynamicGlobalMessage" + Request.QueryString["message"].ToString().Trim());
				m.addMenuChoice("3", "preRecordedMssgList.aspx");
				m.addMenuChoice("4", "recordGlobalMessage.aspx?event=recordPrompt&amp;lang=en");
				m.setPrompt(p);				
				
				m.addCatchBlock(getCatchEvent(new string[]{"AdminMaxError.vox"}, 3, CatchEvent.NO_INPUT, "adminEndCall.aspx"));
				
				m.addCatchBlock(getCatchEvent(new string[]{"AdminMaxError.vox"}, 3, CatchEvent.NO_MATCH, "adminEndCall.aspx"));
				
				m.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "adminEndCall.aspx"));
			
				Response.Write(m.getVXML());
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
