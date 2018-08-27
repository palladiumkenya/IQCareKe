using IQCare.CCC.UILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for HIVEducationService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class HIVEducationService1 : System.Web.Services.WebService
    {

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetCounsellingTopics(string counsellingtopics)
        {
            String TopicName = "";
            if(counsellingtopics== "Progression,RX")
            {
                TopicName = "ProgressionRX";
            }
            else if(counsellingtopics == "BasicPreventionDisclosureEducation")
            {
                TopicName = "BasicPreventionDisclosureEducation";
               
            }
            else
            {
                TopicName = counsellingtopics.Replace(@",", "");
            }

            LookupLogic lookUp = new LookupLogic();
            DropDownList dll = new DropDownList();
            lookUp.populateDDL(dll, TopicName);
            ArrayList rows = new ArrayList();
            int x = dll.Items.Count;
            if (x > 0)
            {
                for (int k = 0; k <= x -1; k++)
                {
                    string[] i = new string[2] { dll.Items[k].Value.ToString(), dll.Items[k].Text.ToString() };
                    rows.Add(i);
                }
            }

            return rows;

        }
    }
}
