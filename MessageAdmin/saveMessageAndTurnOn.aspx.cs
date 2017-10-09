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
	/// 9968
	/// </summary>
	public partial class saveMessageAndTurnOn : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				
				string strDelete = Request.QueryString["delete"] != null ? Request.QueryString["delete"].ToString().Trim() : "y";				
				string strOldFileName = Request.QueryString["message"].ToString().Trim();				
				string strLang = Request.QueryString["lang"].ToString().Trim();
				string strDir = ConfigurationSettings.AppSettings["recFilePath"];				
				URLRedirect u = new URLRedirect();				
				
				if (strDelete == "y")
				{
					File.Copy(strDir + strLang + "\\" + Session["sessionID"].ToString() + strOldFileName + ".vox", strDir + strLang + "\\dynamicGlobalMessage.vox", true);
					File.Delete(strDir + strLang + "\\" + Session["sessionID"].ToString() + strOldFileName + ".vox");
					u.NextURL = "confirmGlobalMessageMenu.aspx?lang=" + strLang;
				}
				else
				{
					File.Copy(strDir + "en\\" + strOldFileName + ".vox", strDir + "en\\dynamicGlobalMessage.vox", true);
					File.Copy(strDir + "mx\\" + strOldFileName + ".vox", strDir + "mx\\dynamicGlobalMessage.vox", true);
					u.NextURL = "confirmGlobalMessageSettings.aspx?lang=" + strLang;
					
				}				
							
				Response.Write(u.getVXML());
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
