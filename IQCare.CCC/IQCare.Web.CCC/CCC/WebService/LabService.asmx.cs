using System;
using System.Web.Services;
using Entities.Common;
using Entities.PatientCore;
using IQCare.CCC.UILogic;


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
        public string AddLabOrder(int patientId, string patientLabOrder)
        {
            
            try
            {

                 var labOrder = new PatientLabOrderManager();
                Result = labOrder.savePatientLabOrder(patientId, patientLabOrder);
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
       }

    }

