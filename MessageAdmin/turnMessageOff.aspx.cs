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
	/// 9990
	/// </summary>
	public partial class turnMessageOff : XMLPage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				
				string strDir = ConfigurationSettings.AppSettings["recFilePath"];				
				File.Delete(strDir + "en\\dynamicGlobalMessage.vox");	
				File.Delete(strDir + "mx\\dynamicGlobalMessage.vox");	
				
				URLRedirect u = new URLRedirect();
				u.NextURL = "confirmGlobalMessageSettings.aspx";				
							
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
