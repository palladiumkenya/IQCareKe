using System;
using System.Web.Services;
using Entities.Common;
using Entities.PatientCore;
using IQCare.CCC.UILogic;
using Newtonsoft.Json;


namespace IQCare.Web.CCC.WebService
{
   // <summary>
     /// Summary description for PersonSeervice
     /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LabService : System.Web.Services.WebService
    {

        private int patientID { get; set; }       
        private string Msg { get; set; }
        private int Result { get; set; }

      
        [WebMethod(EnableSession = true)]
        public string AddLabOrder(int patientId, int visitId,string patientLabOrder)
        {
            
            try
            {

                 var labOrder = new PatientLabOrderManager();
                Result = labOrder.savePatientLabOrder(patientId,visitId,patientLabOrder);
                if (Result > 0)
                {
                    Msg = "Patient Lab Order Recorded Successfully .";
                }

            }
            catch (Exception e)
            {
                Msg = "Error Message: " + e.Message + ' ' + " Exception: " + e.InnerException;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string GetLookupPreviousLabsList(string patient_ID)
        {

            //var patient_ID = JsonConvert.SerializeObject(patient_id);    //clean object
            //var patient_id = JSON.parse(patientID);
             int patientId = Convert.ToInt32(patient_ID);
            // int patientId = int.Parse(patient_Id);
          //  patientID = Convert.ToInt32(Session["PersonId"]);
            string jsonObject = LookupLogic.GetLookupPreviousLabsListJson(patientId);

            return jsonObject;
        }
        // pw .lookup previous lablist             
        // pw .grid



    }

}

