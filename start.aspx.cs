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
using System.Configuration;
using System.IO;
using System.Threading;

namespace KaiserAACCmx
{
	/// <summary>
	/// Summary description for start.
	/// </summary>
	public partial class start : XMLPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!isConfigurationFileOK()) 
			{
				this.hangURL(); 
				return;
			}
			
			switch (Request.QueryString["event"].ToString().Trim()) 
			{			
				case "500":
					this.ivrNode = "500";
					greeting();
					break;
				case "600":
					this.ivrNode = "600";
					languageSelect();
					break;
				case "700":					
					this.ivrNode = "700";
					emergency();
					break;
				case "1000":
					this.ivrNode = "1000";
					mainMenu();
					break;
				case "1500":
					this.ivrNode = "1500";
					cityYN();
					break;
				case "1050":
					this.ivrNode = "1050";
					antiochMenu();
					break;
				case "1100":
					this.ivrNode = "1100";
					antiochYN();
					break;
				case "2000":
					this.ivrNode = "2000";
					secondTier();
					break;
				case "2500":
					this.ivrNode = "2500";
					department();
					break;
				case "3000":
					this.ivrNode = "3000";
					transfer();
					break;
				case "5000":
					this.ivrNode = "5000";
					exit();
					break;
				case "6000":
					this.ivrNode = "6000";
					error();
					break;
				case "endCall":
					endCall();
					break;
				case "root":
					rootDoc(URLParams);
					break;
				case "logData":
					try
					{
						logData();
					}
					catch (System.IO.IOException)
					{
						int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
						Thread.Sleep(sleepTime);
						try
						{
							logData();
						}
						catch
						{
						}
					}
					break;
				case "cpaResult":
					cpaResult();
					break;
				
			}			
		}		
	//500
	private void greeting()
	{
		DateTime timeStart = DateTime.Now;
		this.timeStart = timeStart.ToString();
		this.language = "en";

		Play ply = new Play();
		Prompt p = new Prompt();
		p.TimeOut = 0;
		p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/500.vox",false);
		p.BargeIn = false;
		ply.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
		ply.setPrompt(p);
		ply.NextURL = "start.aspx" + URLParams + "&amp;event=600";
		ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
		ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.PHONE_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
		Response.Write(ply.getVXML());		
	}
	//600
	private void languageSelect()
	{
		Subdialog sd = new Subdialog("languageSelect");
		sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "customcontext";
		
		// URLs
		sd.failureURL = "start.aspx" + URLParams + "&amp;event=700&amp;lang=en&amp;failure=1";
		sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";		
		sd.successURL = "start.aspx" + URLParams + "&amp;event=700&amp;success=1";	
		
		sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");		
		
		//VXML properties
		sd.addProperty("com.telera.speechenabled","true");
		sd.addProperty("inputmodes","dtmf voice");
		sd.addProperty("confidencelevel","0.70");
		sd.addProperty("sensitivity","0.60");		
		sd.addProperty("interdigittimeout","1000ms");       
		
		//OSDM params
		sd.addParam("dmname", "languageSelect");
		sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "languageCollection.grxml");
		sd.addParam("collection_dtmfparallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "languageDtmfCollection.grxml");
		sd.addParam("confirmation_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "languageCollection.grxml");
		sd.addParam("confirmation_dtmfparallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "languageDtmfCollection.grxml");
		sd.addParam("defaultconfirmation","NEVER");		
		sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + "en/603.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/603b.vox");
		sd.addParam("confirmation_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + "en/602.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/604.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/604b.vox");
		sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + "en/601.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/603.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/603b.vox");
		sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + "en/604.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/604b.vox");
		sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + "en/602.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/603.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/603b.vox");
		sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + "en/604.vox|" + ConfigurationSettings.AppSettings["voxURL"] + "en/604b.vox");
		sd.addParam("wronganswerapologies1","");
		sd.addParam("wronganswerapologies2","");
		sd.addParam("collection_helpprompts1","");
		sd.addParam("collection_reprompts1","");
		sd.addParam("successprompts1","");
		sd.addParam("failureprompt","");       

		//osdm params for logging
		sd.addOsdmOutputParam("confidencescore");
		sd.addOsdmOutputParam("failurereason");
		sd.addOsdmOutputParam("inputmode");
		sd.addOsdmOutputParam("noinputs");
		sd.addOsdmOutputParam("nomatches");
		sd.addOsdmOutputParam("rawanswer");
		sd.addOsdmOutputParam("returncode");
		
		Response.Write(sd.getVXML());	
	}   

	//700
	private void emergency()
	{
		string strReturnValue = Request.QueryString["languageSelect.returnvalue"] != "" ? Request.QueryString["languageSelect.returnvalue"].ToString().Trim() : "";
		string voxLang;
        URLRedirect u = new URLRedirect();

        if (strReturnValue == "admin")
        {
            Session["sessionID"] = Request.QueryString["SESSIONID"];
            u.NextURL = ConfigurationSettings.AppSettings["ivrURL"] + "MessageAdmin/globalMessageMenu.aspx";
            Response.Write(u.getVXML());
        }
        else
        {

            if (strReturnValue == "spanish")
            {
                this.language = "mx";
                voxLang = "mx/";
            }
            else
            {
                this.language = "en";
                voxLang = "en/";
            }

            this.failure600 = Request.QueryString["failure"] != null ? Request.QueryString["failure"].ToString().Trim() : "";
            this.success600 = Request.QueryString["success"] != null ? Request.QueryString["success"].ToString().Trim() : "";

            Play ply = new Play();
            Prompt p = new Prompt();
            p.TimeOut = 0;
            p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "700.vox", false);
            p.BargeIn = false;
            ply.setPrompt(p);
            ply.NextURL = "start.aspx" + URLParams + "&amp;event=1000";
            ply.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
            ply.addCatchBlock(getCatchEvent(new string[] { "" }, 1, CatchEvent.CONNECTION_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
            ply.addCatchBlock(getCatchEvent(new string[] { "" }, 1, CatchEvent.PHONE_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
            Response.Write(ply.getVXML());
        }
	}
	//1000
	private void mainMenu()
	{
		string cityYN = Request.QueryString["cityYN"] != null ? Request.QueryString["cityYN"].ToString().Trim()  : "";
		string strYNCount = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "0";
		int ynCount = Convert.ToInt32(strYNCount.ToString());
		
		if (ynCount == 3)
		{
			URLRedirect r = new URLRedirect();
			r.NextURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1&amp;ynCount=max";
			r.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
			Response.Write(r.getVXML());	
		}
		else 
		{
			string voxLang;
			if (this.language == "mx") 
			{
				voxLang = "mx/";				
			}
			else 
			{
				voxLang = "en/";
			}

			Subdialog sd = new Subdialog("location");
			sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "customcontext";

			// URLs
			sd.failureURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1";
			sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";			
			sd.successURL = "start.aspx" + URLParams + "&amp;event=1500&amp;ynCount=" + ynCount;
            			
			sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
			
			//VXML properties
			sd.addProperty("com.telera.speechenabled","true");
			sd.addProperty("inputmodes","voice");
            sd.addProperty("grammarmaxage", "5s");
            sd.addProperty("audiomaxage", "5s");	
			
			
			//OSDM params
			if (this.language == "mx") 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "mainMenuCollectionMx.grxml");
				sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "mainMenuCollection.grxml");
				sd.addParam("collection_parallelgrammar3",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommandMx.grxml");
				sd.addProperty("confidencelevel","0.60");
				sd.addParam("collection_confidencelevel","0.60");
				sd.addProperty("sensitivity","0.60");
			}
			else 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "mainMenuCollection.grxml");
				sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommand.grxml");
				sd.addProperty("confidencelevel","0.70");
				sd.addParam("collection_confidencelevel","0.70");
				sd.addProperty("sensitivity","0.70");
			}
			
			sd.addParam("dmname", "location");
			sd.addParam("defaultconfirmation","NEVER");
			sd.addParam("property_collection_inputmodes","voice");
			
			// needs to be configured if using different voxs in confirm_string in the grammar
			sd.addParam("baselinepromptsdirectory",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "collection/");
			
			//prompts
			if (cityYN == "no") 
			{
				sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "20.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1000.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1001.vox");
			}
			else 
			{
				sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1000.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1001.vox");
			}

			sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1003.vox");
			sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1004.vox");
			sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1005.vox");
			sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1006.vox");
			sd.addParam("collection_helpprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1007.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1002.vox");
			sd.addParam("collection_reprompts1","");
			sd.addParam("successprompts1","");
			sd.addParam("failureprompt","");

			//osdm params for logging
			sd.addOsdmOutputParam("confidencescore");
			sd.addOsdmOutputParam("failurereason");
			sd.addOsdmOutputParam("inputmode");
			sd.addOsdmOutputParam("noinputs");
			sd.addOsdmOutputParam("nomatches");
			sd.addOsdmOutputParam("rawanswer");
			sd.addOsdmOutputParam("returncode");

			Response.Write(sd.getVXML());
		}
	}
	//1500
		private void cityYN()
		{
			string ynCountStr = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "";
			int ynCount = Convert.ToInt32(ynCountStr.ToString());
			ynCount = ynCount + 1;
			string location = Request.QueryString["location.returnvalue"] != "" ? Request.QueryString["location.returnvalue"].ToString().Trim() : "";
		
			try
			{
				logShadowVars("location");
			}
			catch (System.IO.IOException)
			{
				int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
				Thread.Sleep(sleepTime);
				try
				{
					logShadowVars("location");
				}
				catch
				{
				}
			}
			
			string voxLang;
			if (this.language == "mx") 
			{
				voxLang = "mx/";
			}
			else 
			{
				voxLang = "en/";
			}

			Subdialog sd = new Subdialog("locationYN");
			sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "yesno";

			// URLs
			sd.failureURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1";
			sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";

			if(location == "1008")
			{
				sd.successURL = "start.aspx" + URLParams + "&amp;event=1050&amp;location=" + location + "&amp;success2=1&amp;ynCount=" + ynCount;
			}
			else
			{
				sd.successURL = "start.aspx" + URLParams + "&amp;event=2000&amp;location=" + location + "&amp;success2=1&amp;ynCount=" + ynCount;
			}
			
			sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
			
			//VXML properties
			sd.addProperty("com.telera.speechenabled","true");
			sd.addProperty("inputmodes","dtmf voice");
			sd.addProperty("confidencelevel","0.70");			
			
			//OSDM params
			if (this.language == "mx") 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["spOSDMGrammarURL"] + "boolean.gram");
				sd.addParam("collection_confidencelevel","0.60");
				sd.addProperty("sensitivity","0.60");				
			}
			else 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["osdmGrammarURL"] + "boolean.gram");
				sd.addParam("collection_confidencelevel","0.70");
				sd.addProperty("sensitivity","0.70");				
			}
			
			//settings
			sd.addParam("dmname", "locationYN");			
			sd.addParam("defaultconfirmation","NEVER");
			
			// needs to be configured if using different voxs in confirm_string in the grammar
			sd.addParam("baselinepromptsdirectory",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "collection/");
			
			//prompts
			sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "35.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + location + ".vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "36.vox");
			sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "105.vox");
			sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "105.vox");
			sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "101.vox");
			sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "102.vox");
			sd.addParam("collection_helpprompts1","");
			sd.addParam("collection_reprompts1","");
			sd.addParam("successprompts1","");
			sd.addParam("failureprompt","");

			//osdm params for logging
			sd.addOsdmOutputParam("confidencescore");
			sd.addOsdmOutputParam("failurereason");
			sd.addOsdmOutputParam("inputmode");
			sd.addOsdmOutputParam("noinputs");
			sd.addOsdmOutputParam("nomatches");
			sd.addOsdmOutputParam("rawanswer");
			sd.addOsdmOutputParam("returncode");

			Response.Write(sd.getVXML());
		}
	//1050	
		private void antiochMenu()
		{
			string cityYN = Request.QueryString["cityYN"] != null ? Request.QueryString["cityYN"].ToString().Trim()  : "";
			string strYNCount = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "0";
			string strReturnValue = Request.QueryString["locationYN.returnvalue"] != null ? Request.QueryString["locationYN.returnvalue"].ToString().Trim() : "";
			string strIvrNode = Request.QueryString["ivrNode"] != null ? Request.QueryString["ivrNode"].ToString().Trim()  : "";
			int ynCount = Convert.ToInt32(strYNCount.ToString());		
			

			if (strReturnValue == "no") 
			{
				URLRedirect u = new URLRedirect();
				u.NextURL = "start.aspx" + URLParams + "&amp;event=1000&amp;cityYN=no&amp;ynCount=" + ynCount;
				Response.Write(u.getVXML());
			}
			else
			{				
				if (ynCount == 3)
				{
					URLRedirect r = new URLRedirect();
					r.NextURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1&amp;ynCount=max";
					r.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
					Response.Write(r.getVXML());	
				}
				else 
				{
					string voxLang;
					if (this.language == "mx") 
					{
						voxLang = "mx/";					
					}
					else 
					{
						voxLang = "en/";
					}

					Subdialog sd = new Subdialog("location");
                    sd.method = "post";
					sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "customcontext";

					// URLs
					sd.failureURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1";
					sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";
					sd.successURL = "start.aspx" + URLParams + "&amp;event=1100&amp;ynCount=" + ynCount;
					
					sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
				
					//VXML properties
					sd.addProperty("com.telera.speechenabled","true");
					sd.addProperty("inputmodes","voice");				
				
					//OSDM params
					if (this.language == "mx") 
					{
						sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "antiochMenuCollectionMx.grxml");
						sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "antiochMenuCollection.grxml");
						sd.addParam("collection_parallelgrammar3",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommandMx.grxml");
						sd.addParam("collection_confidencelevel","0.60");
						sd.addProperty("confidencelevel","0.60");					
						sd.addProperty("sensitivity","0.60");
					}
					else 
					{
						sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "antiochMenuCollection.grxml");
						sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommand.grxml");
						sd.addParam("collection_confidencelevel","0.70");
						sd.addProperty("confidencelevel","0.70");					
						sd.addProperty("sensitivity","0.70");
					}
					//settings
					sd.addParam("dmname", "location");				
					sd.addParam("defaultconfirmation","NEVER");
					sd.addParam("property_collection_inputmodes","voice");

					// needs to be configured if using different voxs in confirm_string in the grammar
					sd.addParam("baselinepromptsdirectory",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "collection/");
				
					//prompts
					if (cityYN == "no") 
					{
						sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "20.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1000.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1001.vox");
					}
					else 
					{
						sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1101.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1001.vox");
					}

					sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1103.vox");
					sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1104.vox");
					sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1105.vox");
					sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1106.vox");
					sd.addParam("collection_helpprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1107.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1002.vox");
					sd.addParam("collection_reprompts1","");
					sd.addParam("successprompts1","");
					sd.addParam("failureprompt","");

					//osdm params for logging
					sd.addOsdmOutputParam("confidencescore");
					sd.addOsdmOutputParam("failurereason");
					sd.addOsdmOutputParam("inputmode");
					sd.addOsdmOutputParam("noinputs");
					sd.addOsdmOutputParam("nomatches");
					sd.addOsdmOutputParam("rawanswer");
					sd.addOsdmOutputParam("returncode");

					Response.Write(sd.getVXML());
				}
			}
			
		}
		//1100
		private void antiochYN()
		{
			string ynCountStr = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "";
			
			int ynCount = Convert.ToInt32(ynCountStr.ToString());
			ynCount = ynCount + 1;
			
			string location = Request.QueryString["location.returnvalue"] != "" ? Request.QueryString["location.returnvalue"].ToString().Trim() : "";
		
			try
			{
				logShadowVars("location");
			}
			catch (System.IO.IOException)
			{
				int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
				Thread.Sleep(sleepTime);
				try
				{
					logShadowVars("location");
				}
				catch
				{
				}
			}
			
			string voxLang;
			if (this.language == "mx") 
			{
				voxLang = "mx/";				
			}
			else 
			{
				voxLang = "en/";
			}

			Subdialog sd = new Subdialog("locationYN");
			sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "yesno";

			// URLs
			sd.failureURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1";
			sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";
			sd.successURL = "start.aspx" + URLParams + "&amp;event=2000&amp;location=" + location + "&amp;success2=1&amp;ynCount=" + ynCount;

			sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");

		//VXML properties
			sd.addProperty("com.telera.speechenabled","true");
			sd.addProperty("inputmodes","dtmf voice");			
			
			//OSDM params
			if (this.language == "mx") 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["spOSDMGrammarURL"] + "boolean.gram");
				sd.addParam("collection_confidencelevel","0.60");
				sd.addProperty("confidencelevel","0.60");				
				sd.addProperty("sensitivity","0.60");
			}
			else 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["osdmGrammarURL"] + "boolean.gram");
				sd.addParam("collection_confidencelevel","0.70");
				sd.addProperty("confidencelevel","0.70");				
				sd.addProperty("sensitivity","0.70");
			}

			//settings
			sd.addParam("dmname", "locationYN");
			sd.addParam("defaultconfirmation","NEVER");
			
			// needs to be configured if using different voxs in confirm_string in the grammar
			sd.addParam("baselinepromptsdirectory",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "collection/");
			
			//prompts
			sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "35.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + location + ".vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "36.vox");
			sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "105.vox");
			sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "105.vox");
			sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "101.vox");
			sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "102.vox");
			sd.addParam("collection_helpprompts1","");
			sd.addParam("collection_reprompts1","");
			sd.addParam("successprompts1","");
			sd.addParam("failureprompt","");

			//osdm params for logging
			sd.addOsdmOutputParam("confidencescore");
			sd.addOsdmOutputParam("failurereason");
			sd.addOsdmOutputParam("inputmode");
			sd.addOsdmOutputParam("noinputs");
			sd.addOsdmOutputParam("nomatches");
			sd.addOsdmOutputParam("rawanswer");
			sd.addOsdmOutputParam("returncode");

			Response.Write(sd.getVXML());
		}
	//2000
	private void secondTier()
	{
		try 
		{
			string strReturnValue = Request.QueryString["locationYN.returnvalue"] != "" ? Request.QueryString["locationYN.returnvalue"].ToString().Trim() : "";
			string ynCount = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : ""; 
			string strEvent = Request.QueryString["event"] != null ? Request.QueryString["event"].ToString().Trim()  : ""; 
			string strIvrNode = Request.QueryString["ivrNode"] != null ? Request.QueryString["ivrNode"].ToString().Trim()  : "";
			
			if (strReturnValue == "no") 
			{
				string strDest = strIvrNode == "1100" ? "1050" : "1000"; 
			
				URLRedirect u = new URLRedirect();				
				u.NextURL = "start.aspx" + URLParams + "&amp;event=" + strDest + "&amp;cityYN=no&amp;ynCount=" + ynCount;
				
				Response.Write(u.getVXML());
			}
			else
			{
			
				string location = Request.QueryString["location"] != null ? Request.QueryString["location"].ToString().Trim()  : ""; 
				this.success1000 = Request.QueryString["success2"] != null ? Request.QueryString["success2"].ToString().Trim()  : "";

				this.facilityNumber = location;
				String fLocationNum;
				String f1stNumber = "";
				String f2ndNumber = "";
				String fLocation;
				String f2ndTier;
				String dataFile = "";
				if (this.language == "mx")
				{
					dataFile = "locationDataMx.txt";
				}
				else
				{
					dataFile = "locationData.txt";
				}
				StreamReader objReader = new StreamReader(ConfigurationSettings.AppSettings["filePath"] + dataFile);
				String sLine="";
				char[] sep = {':'};
				ArrayList arrText = new ArrayList();

				while (sLine != null)
				{
					sLine = objReader.ReadLine();
					if (sLine != null) 
					{
						arrText.Add(sLine);
					}
				}
				objReader.Close();

				Boolean is2ndTier = false;

				foreach (string sOutput in arrText) 
				{
					Array data = sOutput.Split(sep);
					fLocationNum = data.GetValue(0).ToString();
					fLocation = data.GetValue(1).ToString();
					f1stNumber = data.GetValue(2).ToString();
					f2ndNumber = data.GetValue(3).ToString();
					f2ndTier = data.GetValue(4).ToString();
					this.facilityName = fLocation;
					this.facilityNumber = fLocationNum;
					this.f1stNumber = f1stNumber;
					this.f2ndNumber = f2ndNumber;
					this.transferNumber = f1stNumber;
									
					if ((location == fLocationNum) && (f2ndTier == "1")) 
					{
						is2ndTier = true;
						break;
					}	
					else 
					{
						is2ndTier = false;
						if (location == fLocationNum) 
						{
							break;
						}
					}
				}

				Play ply = new Play();
				Prompt p = new Prompt();
				p.TimeOut = 0;
				p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/silence.vox",false);
				if(location == "1009")
				{
					if (this.language == "mx")
					{
						p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "mx/dv.vox",false);
					}
					else
					{
						p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + "en/dv.vox",false);
					}
				}
				p.BargeIn = false;
				ply.setPrompt(p);
				ply.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");

				if (is2ndTier == true) 
				{
					ply.NextURL = "start.aspx" + URLParams + "&amp;event=2500";
				}	
				else 
				{
					ply.NextURL = "start.aspx" + URLParams + "&amp;event=3000&amp;transferNum=" + this.f1stNumber;
				}
				ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
				ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.PHONE_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
				Response.Write(ply.getVXML());
			}
		}
		catch (Exception ex)
		{
            Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "General Exception Policy");
			this.logError(ex);
			this.SystemDifficulties();
		}
			
	}
	//2500
	private void department()
	{
		string strYNCount = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "0";
		int ynCount = Convert.ToInt32(strYNCount.ToString());		
		
		if (ynCount == 3)
		{
			URLRedirect r = new URLRedirect();
			r.NextURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure2=1&amp;ynCount=max";
			r.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
			Response.Write(r.getVXML());	
		}
		else
		{
			string voxLang;
			if (this.language == "mx") 
			{
				voxLang = "mx/";		
			}
			else 
			{
				voxLang = "en/";
			}

			Subdialog sd = new Subdialog("department");
			sd.method = "post";
			sd.src = ConfigurationSettings.AppSettings["osdmURL"] + "customcontext";

			// URLs
			sd.failureURL = "start.aspx" + URLParams + "&amp;event=5000&amp;failure3=1";
			sd.errorURL = "globalError.aspx" + URLParams + "&amp;event=osdmError";
			sd.successURL = "start.aspx" + URLParams + "&amp;event=3000&amp;success3=1";			
            
			sd.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");

			//VXML properties
			sd.addProperty("com.telera.speechenabled","true");
			sd.addProperty("inputmodes","voice");
			sd.addProperty("confidencelevel","0.70");
			sd.addProperty("sensitivity","0.70");	
				
			//OSDM params
			if (this.language == "mx") 
			{
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "departmentCollectionMx.grxml");
				sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommandMx.grxml");
				sd.addParam("confirmation_parallelgrammar1",ConfigurationSettings.AppSettings["spOSDMGrammarURL"] + "boolean.gram");
			}
			else 
			{				
				sd.addParam("collection_parallelgrammar1",ConfigurationSettings.AppSettings["grammarURL"] + "departmentCollection.grxml");
				sd.addParam("collection_parallelgrammar2",ConfigurationSettings.AppSettings["grammarURL"] + "globalCommand.grxml");
				sd.addParam("confirmation_parallelgrammar1",ConfigurationSettings.AppSettings["osdmGrammarURL"] + "boolean.gram");
			}
		
			//settings
			sd.addParam("dmname", "department");
			sd.addParam("collection_confidencelevel","0.70");
			sd.addParam("defaultconfirmation","IF_NECESSARY");
			sd.addParam("property_collection_inputmodes","voice");
		
			// needs to be configured if using different voxs in confirm_string in the grammar
			sd.addParam("baselinepromptsdirectory",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "collection/");
		
			//prompts
			sd.addParam("collection_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2500.vox");
			sd.addParam("confirmation_initialprompt",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "35.vox|es;confirmations.es;confirmCustomContext;1|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "36.vox");
			sd.addParam("collection_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2502.vox");
			sd.addParam("collection_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2503.vox");
			sd.addParam("confirmation_noinputprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2502.vox");
			sd.addParam("confirmation_noinputprompts2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2503.vox");
			sd.addParam("noanswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2504.vox");
			sd.addParam("noanswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2505.vox");
			sd.addParam("wronganswerapologies1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "20.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2500.vox");
			sd.addParam("wronganswerapologies2",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "21.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2500.vox");
			sd.addParam("collection_helpprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2506.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1002.vox");
			sd.addParam("confirmation_helpprompts1",ConfigurationSettings.AppSettings["voxURL"] + voxLang + "2506.vox|" + ConfigurationSettings.AppSettings["voxURL"] + voxLang + "1002.vox");
			sd.addParam("collection_reprompts1","");
			sd.addParam("confirmation_reprompts1","");
			sd.addParam("successprompts1","");
			sd.addParam("failureprompt","");		

			//osdm params for logging
			sd.addOsdmOutputParam("confidencescore");
			sd.addOsdmOutputParam("failurereason");
			sd.addOsdmOutputParam("inputmode");
			sd.addOsdmOutputParam("noinputs");
			sd.addOsdmOutputParam("nomatches");
			sd.addOsdmOutputParam("rawanswer");
			sd.addOsdmOutputParam("returncode");

			Response.Write(sd.getVXML());
		}
	}
	//3000
	private void transfer()
	{
		string strReturnValue = Request.QueryString["department.returnvalue"] != null ? Request.QueryString["department.returnvalue"].ToString().Trim() : "";
		string transferNum = Request.QueryString["transferNum"] != null ? Request.QueryString["transferNum"].ToString().Trim()  : ""; 
		
		if (strReturnValue != "")
		{
			if (strReturnValue == "adult") 
			{
				transferNum = this.f1stNumber;				
			}
			else 
			{
				transferNum = this.f2ndNumber;				
			}	
		}

		this.transferNumber = transferNum;
		this.success2500 = Request.QueryString["success3"] != null ? Request.QueryString["success3"].ToString().Trim()  : "";
		DateTime timeTransfer = DateTime.Now;
		this.timeTransfer = timeTransfer.ToString();
		
		string voxLang = "en/";
		if (this.language == "mx") 
		{
			voxLang = "mx/";
		}

		Transfer t = new Transfer(Transfer.TransferType.CreateLegAndDialWithCPA, transferNum);
		t.ANI = this.ani;
		t.ACTimeOut = 0;
		t.CPAResultURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx" + URLParams + "&amp;event=cpaResult";
		//t.MusicOnHoldURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx" + URLParams + "&amp;event=holdPage";
		t.MusicOnHoldURL = ConfigurationSettings.AppSettings["ivrURL"] + "hold.aspx" + URLParams;
        if (File.Exists(ConfigurationSettings.AppSettings["recFilePath"] + voxLang + "\\dynamicGlobalMessage.vox"))
        {
            t.addBeforeTransferVoiceFile(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "dynamicGlobalMessage.vox");
        }
        t.addBeforeTransferVoiceFile(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "3000.vox");
		Response.Write(t.getVXML());	
	}
	//cpaResult
		private void cpaResult()
		{
			string cpaResult = Request.QueryString["cparesult"] != null ? Request.QueryString["cparesult"].ToString().Trim()  : "";
			CPA cpa = new CPA();
			cpa.CPAResult = cpaResult;
			this.transferCPA = cpaResult;
			Session["transferCPA"] = cpaResult;
			cpa.NotNormalCPAResultURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx" + URLParams + "&amp;event=logData";
			cpa.DisconnectPageURL = ConfigurationSettings.AppSettings["ivrURL"] + "start.aspx" + URLParams + "&amp;event=logData";
			Response.Write(cpa.getVXML());
		}
	//5000
	private void exit()
	{
		string ynCount = Request.QueryString["ynCount"] != null ? Request.QueryString["ynCount"].ToString().Trim()  : "";
		if (ynCount != "max") 
		{	
			try
			{
				logShadowVars("location");
			}
			catch (System.IO.IOException)
			{
				int sleepTime = Convert.ToInt16(ConfigurationSettings.AppSettings["sleepTime"]);
				Thread.Sleep(sleepTime);
				try
				{
					logShadowVars("location");
				}
				catch
				{
				}
			} 
		}

		this.failure1000 = Request.QueryString["failure2"] != null ? Request.QueryString["failure2"].ToString().Trim()  : "";
		this.failure2500 = Request.QueryString["failure3"] != null ? Request.QueryString["failure3"].ToString().Trim()  : "";		

		string voxLang = "en/";
		if (this.language == "mx") 
		{
			voxLang = "mx/";
		}
		Play ply = new Play();
		Prompt p = new Prompt();
		p.TimeOut = 0;
		p.addVoiceFileToPrompt(ConfigurationSettings.AppSettings["voxURL"] + voxLang + "5000.vox",false);
		p.BargeIn = false;
		ply.setPrompt(p);
		ply.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
		ply.NextURL = "start.aspx" + URLParams + "&amp;event=logData";
		ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
		ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.PHONE_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
		Response.Write(ply.getVXML());		
	}
	//6000
	private void error()
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
	//	ply.setGlobalCommands("start.aspx" + URLParams + "&amp;event=root");
	//	ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.CONNECTION_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
	//	ply.addCatchBlock(getCatchEvent(new string[]{""}, 1, CatchEvent.PHONE_HANG_UP, "start.aspx" + URLParams + "&amp;event=logData"));
		ply.setPrompt(p);
		ply.NextURL = "start.aspx" + URLParams + "&amp;event=endCall";
		Response.Write(ply.getVXML());		
	}
	//endCall
	private void endCall()
	{
		Disconnect d = new Disconnect();
		Response.Write(d.getVXML());
	}

		// root
		private void rootDoc(string url)
		{
			GlobalCommands gc = new GlobalCommands(true);
			CatchEvent e1 = new CatchEvent(1, CatchEvent.CONNECTION_HANG_UP);
			e1.NextURL = "start.aspx" + URLParams + "&amp;event=logData";
			e1.Reprompt = false;
			CatchEvent e2 = new CatchEvent(1, CatchEvent.PHONE_HANG_UP);
			e2.NextURL =  "start.aspx" + URLParams + "&amp;event=logData";
			e2.Reprompt = false;
			gc.addCatchBlock(e1);
			gc.addCatchBlock(e2);
			Response.Write(gc.getVXML());
		}

		// logData
		private void logData()
		{
			this.transferCPA = Session["transferCPA"] != null ? Session["transferCPA"].ToString().Trim() : "";
			// Calculate time of call in secs
			double timeCall = 0;
			DateTime timeEnd = DateTime.Now;
			this.timeEnd = timeEnd.ToString();
			TimeSpan diff;
			if (this.timeStart == "") { this.timeStart = DateTime.Now.ToString(); }
			DateTime timeStart = Convert.ToDateTime(this.timeStart);
			diff = timeEnd - timeStart;
			timeCall = diff.TotalSeconds;
			timeCall = Convert.ToInt32(timeCall);
			this.timeCall = timeCall.ToString();

            string logName = DateTime.Now.ToString("yyyy-MM-dd") + Environment.MachineName + ".log";

			FileStream fs = new FileStream(ConfigurationSettings.AppSettings["logFilePath"].ToString() + logName,FileMode.Append);
			StreamWriter w = new StreamWriter(fs);
			if (this.sessionID == "") { this.sessionID = "0"; }
			if (this.did == "") { this.did = "0"; }
			if (this.ani == "") { this.ani = "0"; }
			if (this.timeStart == "") { this.timeStart = "0"; }
			if (this.timeEnd == "") { this.timeEnd = "0"; }
			if (this.timeCall == "") { this.timeCall = "0"; }
			if (this.facilityName == "") { this.facilityName = "none"; }
			if (this.facilityNumber == "") { this.facilityNumber = "0"; }
			if (this.transferNumber == "") { this.transferNumber = "0"; }
			if (this.transferCPA == "") { this.transferCPA = "none"; }
			if (this.timeTransfer == "") { this.timeTransfer = "0"; }
			if (this.language == "") { this.language = "en"; }
			if (this.ivrNode == "") { this.ivrNode = "0"; }
			if (this.failure600 == "") { this.failure600 = "0"; }
			if (this.failure1000 == "") { this.failure1000 = "0"; }
			if (this.failure2500 == "") { this.failure2500 = "0"; }
			if (this.success600 == "") { this.success600 = "0"; }
			if (this.success1000 == "") { this.success1000 = "0"; }
			if (this.success2500 == "") { this.success2500 = "0"; }

			w.WriteLine(this.sessionID + "," + this.did + "," + this.ani + "," + this.timeStart + "," + this.timeEnd + "," + this.timeCall + "," + this.facilityName + "," + this.facilityNumber + "," + this.transferNumber + "," + this.transferCPA + "," + this.timeTransfer + "," + this.language + "," + this.ivrNode + "," + this.failure600 + "," + this.failure1000 + "," + this.failure2500 + "," + this.success600 + "," + this.success1000 + "," + this.success2500);
			w.Close();
			fs.Close();

			Disconnect d = new Disconnect();
			Response.Write(d.getVXML());
		}		
		private bool isConfigurationFileOK()
		{
			bool isOK = false;
			try
			{
				String fLocationNum;
				String dataFile = "";
				dataFile = "locationData.txt";
				StreamReader objReader = new StreamReader(ConfigurationSettings.AppSettings["filePath"] + dataFile);
				String sLine="";
				char[] sep = {':'};
				ArrayList arrText = new ArrayList();
				while (sLine != null)
				{
					sLine = objReader.ReadLine();
					if (sLine != null) 
					{
						arrText.Add(sLine);
					}
				}
				objReader.Close();
				foreach (string sOutput in arrText) 
				{
					Array data = sOutput.Split(sep);
					fLocationNum = data.GetValue(0).ToString();									
					if (fLocationNum != "1008")
					{
						isOK = false;
						break;
					}	
				}
				// spanish datafile
				String fLocationNumMx;
				String dataFileMx = "";
				dataFileMx = "locationDataMx.txt";
				StreamReader objReaderMx = new StreamReader(ConfigurationSettings.AppSettings["filePath"] + dataFileMx);
				String sLineMx="";
				char[] sepMx = {':'};
				ArrayList arrTextMx = new ArrayList();
				while (sLineMx != null)
				{
					sLineMx = objReaderMx.ReadLine();
					if (sLineMx != null) 
					{
						arrTextMx.Add(sLineMx);
					}
				}
				objReaderMx.Close();
				foreach (string sOutputMx in arrTextMx) 
				{
					Array dataMx = sOutputMx.Split(sepMx);
					fLocationNumMx = dataMx.GetValue(0).ToString();									
					if (fLocationNumMx != "1008")
					{
						isOK = false;
						break;
					}	
				}
				isOK = true;
			}
			catch(Exception ex)
			{
				isOK = false;
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "General Exception Policy");
				this.logError(ex);
				this.SystemDifficulties();
			}
			finally
			{
			}
			return isOK;
		}
		private void hangURL()
		{
			System.Threading.Thread.Sleep(6000);
		}
		private void logShadowVars(string strDMName)
		{
			// shadow vars
			string confidencescore = Request.QueryString[strDMName + "." + "confidencescore"] != null ? Request.QueryString[strDMName + "." + "confidencescore"].ToString().Trim()  : ""; 
			string failurereason = Request.QueryString[strDMName + "." + "failurereason"] != null ? Request.QueryString[strDMName + "." + "failurereason"].ToString().Trim()  : ""; 
			string inputmode = Request.QueryString[strDMName + "." + "inputmode"] != null ? Request.QueryString[strDMName + "." + "inputmode"].ToString().Trim()  : ""; 
			string noinputs = Request.QueryString[strDMName + "." + "noinputs"] != null ? Request.QueryString[strDMName + "." + "noinputs"].ToString().Trim()  : ""; 
			string nomatches = Request.QueryString[strDMName + "." + "nomatches"] != null ? Request.QueryString[strDMName + "." + "nomatches"].ToString().Trim()  : ""; 
			string rawanswer = Request.QueryString[strDMName + "." + "rawanswer"] != null ? Request.QueryString[strDMName + "." + "rawanswer"].ToString().Trim()  : ""; 
			string returncode = Request.QueryString[strDMName + "." + "returncode"] != null ? Request.QueryString[strDMName + "." + "returncode"].ToString().Trim()  : ""; 
			string returnvalue = Request.QueryString[strDMName + "." + "returnvalue"] != null ? Request.QueryString[strDMName + "." + "returnvalue"].ToString().Trim()  : "";

			// log the shadows

            string logName = DateTime.Now.ToString("yyyy-MM-dd") + Environment.MachineName + "shadow.log";

			FileStream fs = new FileStream(ConfigurationSettings.AppSettings["logFilePath"].ToString() + logName,FileMode.Append);
			StreamWriter w = new StreamWriter(fs);
			if (this.sessionID == "") { this.sessionID = "0"; }
			if (this.did == "") { this.did = "0"; }
			if (this.ani == "") { this.ani = "0"; }

			if (confidencescore == "") { confidencescore = "0"; }
			if (failurereason == "") { failurereason = "0"; }
			if (inputmode == "") { inputmode = "0"; }
			if (noinputs == "") { noinputs = "0"; }
			if (nomatches == "") { nomatches = "0"; }
			if (rawanswer == "") { rawanswer = "0"; }
			if (returncode == "") { returncode = "0"; }
			if (returnvalue == "") { returnvalue = "0"; }

			w.WriteLine(this.sessionID + "," + this.did + "," + this.ani + "," + confidencescore + "," + failurereason + "," + inputmode + "," + noinputs + "," + nomatches + "," + rawanswer + "," + returncode + "," + returnvalue);
			w.Close();
			fs.Close();
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
