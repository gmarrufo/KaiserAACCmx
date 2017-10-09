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
	/// 9974
	/// </summary>
	public partial class confirmGlobalMessageMenu : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{				
				string strLang = Request.QueryString["lang"].ToString().Trim();				

				switch (strLang) 
				{
					case "en":
						strLang = "mx";
						break;
					case "mx":
						strLang = "en";
						break;								
				}				
				
				Prompt p = new Prompt();
				p.BargeIn = true;
				p.TimeOut = 5;				

				Menu m = new Menu("change");
				
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + "9974.vox", true);
				p.addTTSToPrompt("<break time=\"1000\"/>");
				m.addMenuChoice("1", "recordGlobalMessage.aspx?event=recordPrompt&amp;lang=" + strLang);
							
				m.setPrompt(p);			
				
				m.addCatchBlock(getCatchEvent(new string[]{"AdminMaxError.vox"}, 1, CatchEvent.NO_INPUT, "adminEndCall.aspx"));

				m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9974.vox"}, 1, CatchEvent.NO_MATCH, ""));
				m.addCatchBlock(getCatchEvent(new string[]{"9375inv.vox", "9974.vox"}, 2, CatchEvent.NO_MATCH, ""));
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
