using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using IQCare.CCC.UILogic;
using System.Collections;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Screening;
using Entities.CCC.Screening;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for EnhanceAdherenceService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EnhanceAdherenceService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdherenceQuestions(string LookupName)
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions(LookupName).ToArray();
            ArrayList questions = new ArrayList();
            foreach (var value in questionsList)
            {
                string radioItems = "";
                LookupItemView[] itemList = lookUp.getQuestions(value.ItemName).ToArray();
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        radioItems = radioItems + "<input type='radio' id='"+items.ItemId+"' name='"+items.MasterId+ "' value='"+items.ItemId+ "'/><label for= '" + items.ItemId + "' >" + items.ItemDisplayName+"</label>";
                    }
                }
                string[] i = new string[1] { "<div class='row'><div class='col-md-8'><label>" + value.ItemDisplayName + "" + " </label></div>" +
                    "<div class='col-md-4'></div>"+radioItems};
                questions.Add(i);
            }
            return questions;
        }
    }
}
