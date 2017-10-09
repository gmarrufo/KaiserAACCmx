using System;
using System.Diagnostics;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Qwest.LangPack.VXML21;
using System.Threading;

namespace KaiserAACCmx
{
	public class XMLPage : System.Web.UI.Page
	{
		protected string did = "";
		protected string f1stNumber = "";
		protected string f2ndNumber = "";
		protected string sessionID = "";
		protected string ani = "";
		protected string transferNumber = "";
		protected string language = "";
		protected string timeStart = "";
		protected string timeEnd = "";
		protected string timeTransfer = "";
		protected string timeCall = "";
		protected string facilityName = "";
		protected string facilityNumber = "";
		protected string transferCPA = "";
		protected string ivrNode = "";
		protected string failure600 = "";
		protected string success600 = "";
		protected string failure1000 = "";
		protected string success1000 = "";
		protected string failure2500 = "";
		protected string success2500 = "";
		// shadow vars
		/*protected string confidencescore600 = "";
		protected string failurereason600 = "";
		protected string inputmode600 = "";
		protected string noinputs600 = "";
		protected string nomatches600 = "";
		protected string rawanswer600 = "";
		protected string returncode600 = "";
		protected string returnvalue600 = "";*/

		/*protected string confidencescore1000 = "";
		protected string failurereason1000 = "";
		protected string inputmode1000 = "";
		protected string noinputs1000 = "";
		protected string nomatches1000 = "";
		protected string rawanswer1000 = "";
		protected string returncode1000 = "";
		protected string returnvalue1000 = "";*/

		/*protected string confidencescore2500 = "";
		protected string failurereason2500 = "";
		protected string inputmode2500 = "";
		protected string noinputs2500 = "";
		protected string nomatches2500 = "";
		protected string rawanswer2500 = "";
		protected string returncode2500 = "";
		protected string returnvalue2500 = "";*/



		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);

			this.sessionID = Request.QueryString["SESSIONID"] != null ? Request.QueryString["SESSIONID"].ToString().Trim()  : "";
			this.ani = Request.QueryString["ANI"] != null ? Request.QueryString["ANI"].ToString().Trim()  : "";
			this.did = Request.QueryString["DID"] != null ? Request.QueryString["DID"].ToString().Trim()  : "";
			this.transferNumber = Request.QueryString["transferNumber"] != null ? Request.QueryString["transferNumber"].ToString().Trim()  : ""; 
			this.f1stNumber = Request.QueryString["f1stNumber"] != null ? Request.QueryString["f1stNumber"].ToString().Trim()  : ""; 
			this.f2ndNumber = Request.QueryString["f2ndNumber"] != null ? Request.QueryString["f2ndNumber"].ToString().Trim()  : ""; 
			this.language = Request.QueryString["language"] != null ? Request.QueryString["language"].ToString().Trim()  : ""; 
			this.timeStart = Request.QueryString["timeStart"] != null ? Request.QueryString["timeStart"].ToString().Trim()  : ""; 
			this.timeEnd = Request.QueryString["timeEnd"] != null ? Request.QueryString["timeEnd"].ToString().Trim()  : ""; 
			this.timeTransfer = Request.QueryString["timeTransfer"] != null ? Request.QueryString["timeTransfer"].ToString().Trim()  : ""; 
			this.timeCall = Request.QueryString["timeCall"] != null ? Request.QueryString["timeCall"].ToString().Trim()  : ""; 
			this.facilityName = Request.QueryString["facilityName"] != null ? Request.QueryString["facilityName"].ToString().Trim()  : ""; 
			this.facilityNumber = Request.QueryString["facilityNumber"] != null ? Request.QueryString["facilityNumber"].ToString().Trim()  : ""; 
			this.transferCPA = Request.QueryString["transferCPA"] != null ? Request.QueryString["transferCPA"].ToString().Trim()  : ""; 
			this.ivrNode = Request.QueryString["ivrNode"] != null ? Request.QueryString["ivrNode"].ToString().Trim()  : ""; 
			this.failure600 = Request.QueryString["failure600"] != null ? Request.QueryString["failure600"].ToString().Trim()  : ""; 
			this.success600 = Request.QueryString["success600"] != null ? Request.QueryString["success600"].ToString().Trim()  : ""; 
			this.failure1000 = Request.QueryString["failure1000"] != null ? Request.QueryString["failure1000"].ToString().Trim()  : ""; 
			this.success1000 = Request.QueryString["success1000"] != null ? Request.QueryString["success1000"].ToString().Trim()  : ""; 
			this.failure2500 = Request.QueryString["failure2500"] != null ? Request.QueryString["failure2500"].ToString().Trim()  : ""; 
			this.success2500 = Request.QueryString["success2500"] != null ? Request.QueryString["success2500"].ToString().Trim()  : ""; 
			// shadow vars
			/* this.confidencescore1000 = Request.QueryString["confidencescore1000"] != null ? Request.QueryString["confidencescore1000"].ToString().Trim()  : ""; 
			this.failurereason1000 = Request.QueryString["failurereason1000"] != null ? Request.QueryString["failurereason1000"].ToString().Trim()  : ""; 
			this.inputmode1000 = Request.QueryString["inputmode1000"] != null ? Request.QueryString["inputmode1000"].ToString().Trim()  : ""; 
			this.noinputs1000 = Request.QueryString["noinputs1000"] != null ? Request.QueryString["noinputs1000"].ToString().Trim()  : ""; 
			this.nomatches1000 = Request.QueryString["nomatches1000"] != null ? Request.QueryString["nomatches1000"].ToString().Trim()  : ""; 
			this.rawanswer1000 = Request.QueryString["rawanswer1000"] != null ? Request.QueryString["rawanswer1000"].ToString().Trim()  : ""; 
			this.returncode1000 = Request.QueryString["returncode1000"] != null ? Request.QueryString["returncode1000"].ToString().Trim()  : ""; 
			this.returnvalue1000 = Request.QueryString["returnvalue1000"] != null ? Request.QueryString["returnvalue1000"].ToString().Trim()  : ""; */
		}
		
		public XMLPage() : base()
		{
		}
		protected void logError(Exception ex)
		{
			try
			{
				FileStream fs = new FileStream(ConfigurationSettings.AppSettings["errorLogFile"].ToString() + DateTime.Now.ToString("yyMMdd") + ".error.log",FileMode.Append);
				StreamWriter w = new StreamWriter(fs);
				w.WriteLine(System.DateTime.Now.ToString() + " " + ex);
				w.Close();
				fs.Close();
			}
			catch
			{
				int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
				Thread.Sleep(sleepTime);
				try
				{
					FileStream fs = new FileStream(ConfigurationSettings.AppSettings["errorLogFile"].ToString() + DateTime.Now.ToString("yyMMdd") + ".error.log",FileMode.Append);
					StreamWriter w = new StreamWriter(fs);
					w.WriteLine(System.DateTime.Now.ToString() + " " + ex);
					w.Close();
					fs.Close();
				}
				catch
				{
				}
			}
		}
		protected void logError(string strMessage)
		{
			try
			{
				FileStream fs = new FileStream(ConfigurationSettings.AppSettings["errorLogFile"].ToString() + DateTime.Now.ToString("yyMMdd") + ".error.log",FileMode.Append);
				StreamWriter w = new StreamWriter(fs);
				w.WriteLine(System.DateTime.Now.ToString() + " " + strMessage);
				w.Close();
				fs.Close();
			}
			catch
			{
				int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
				Thread.Sleep(sleepTime);
				try
				{
					FileStream fs = new FileStream(ConfigurationSettings.AppSettings["errorLogFile"].ToString() + DateTime.Now.ToString("yyMMdd") + ".error.log",FileMode.Append);
					StreamWriter w = new StreamWriter(fs);
					w.WriteLine(System.DateTime.Now.ToString() + " " + strMessage);
					w.Close();
					fs.Close();
				}
				catch
				{
				}
			}
		}
		protected void SystemDifficulties()
		{
			string voxLang = "en/";
			if (this.language == "mx") 
			{
				voxLang = "mx/";
			}
			Play ply = new Play();
			Prompt p = new Prompt();
			p.TimeOut = 0;
			p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "6000.vox",false);
			p.BargeIn = false;
			ply.setGlobalCommands("start.aspx?event=root");
			ply.setPrompt(p);
			ply.NextURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx?event=endCall";
			Response.Write(ply.getVXML());			
		}
		protected void SystemDifficulties(Exception ex)
		{
			string voxLang = "en/";
			if (this.language == "mx") 
			{
				voxLang = "mx/";
			}
			this.logError(ex);
			Play ply = new Play();
			Prompt p = new Prompt();
			p.TimeOut = 0;
			p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "6000.vox",false);
			p.BargeIn = false;
			ply.setGlobalCommands("start.aspx?event=root");
			ply.setPrompt(p);
			ply.NextURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx?event=endCall";
			Response.Write(ply.getVXML());		
		}
		protected CatchEvent getCatchEvent(string[] arPrompts, int iCount, string strCatchType, string strNextURL)
		{
			Prompt p = new Prompt();
			p.BargeIn = true;
			p.TimeOut = 5;
			for(int i = 0; i < arPrompts.Length; i++)
				if(!arPrompts[i].Equals(""))
				{
					p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/" + arPrompts[i], false, arPrompts[i]);
				}
			CatchEvent e = new CatchEvent(iCount, strCatchType);
			e.setPrompt(p);
			e.Disconnect = false;
			e.Reprompt = false;
			e.NextURL = strNextURL;
			return e;
		}
		private string strURLParams = "";
		protected string URLParams
		{
			get
			{
				if (strURLParams == "")
					strURLParams = "?SESSIONID=" + this.sessionID + 
						"&amp;ANI=" + this.ani +
						"&amp;DID=" + this.did +
						"&amp;transferNumber=" + this.transferNumber +
						"&amp;f1stNumber=" + this.f1stNumber +
						"&amp;f2ndNumber=" + this.f2ndNumber +
						"&amp;timeStart=" + this.timeStart +
						"&amp;timeEnd=" + this.timeEnd +
						"&amp;timeTransfer=" + this.timeTransfer +
						"&amp;timeCall=" + this.timeCall +
						"&amp;facilityName=" + this.facilityName +
						"&amp;facilityNumber=" + this.facilityNumber +
						"&amp;transferCPA=" + this.transferCPA +
						"&amp;ivrNode=" + this.ivrNode +
						"&amp;failure600=" + this.failure600 +
						"&amp;success600=" + this.success600 +
						"&amp;failure1000=" + this.failure1000 +
						"&amp;success1000=" + this.success1000 +
						"&amp;failure2500=" + this.failure2500 +
						"&amp;success2500=" + this.success2500 +
						"&amp;language=" + this.language;
						/* "&amp;confidencescore1000=" + this.confidencescore1000 + 
						"&amp;failurereason1000=" + this.failurereason1000 + 
						"&amp;inputmode1000=" + this.inputmode1000 + 
						"&amp;noinputs1000=" + this.noinputs1000 + 
						"&amp;nomatches1000=" + this.nomatches1000 + 
						"&amp;rawanswer1000=" + this.rawanswer1000 + 
						"&amp;returncode1000=" + this.returncode1000 + 
						"&amp;returnvalue1000=" + this.returnvalue1000; */
				return strURLParams;
			}
		}
	}
}
