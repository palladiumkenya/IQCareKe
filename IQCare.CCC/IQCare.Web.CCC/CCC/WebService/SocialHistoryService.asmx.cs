using Entities.CCC.Tanners;
using System;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using Entities.CCC.Encounter;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for SocialHistoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SocialHistoryService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }
        [WebMethod]
        public string addSocialHistory(int patientId, int patientMasterVisitId, int createdBy,int DrinkAlcohol,int Smoke,int UseDrugs,string SocialHistoryNotes, int RecordSocialHistory)
        {
            try
            {
                var SH = new SocialHistoryLogic();
                Result = SH.addSocialHistory(patientId,patientMasterVisitId,createdBy,DrinkAlcohol,Smoke,UseDrugs,SocialHistoryNotes,RecordSocialHistory);
                if (Result > 0)
                {
                    Msg = "Social History Added Successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string updateSocialHistory(int patientId, int patientMasterVisitId, int createdBy, int DrinkAlcohol, int Smoke, int UseDrugs, string SocialHistoryNotes, int SocialHistoryId, int RecordSocialHistory)
        {
            try
            {
                var SH = new SocialHistoryLogic();
                Result = SH.updateSocialHistory(patientId, patientMasterVisitId, createdBy, DrinkAlcohol, Smoke, UseDrugs, SocialHistoryNotes, SocialHistoryId, RecordSocialHistory);
                if (Result > 0)
                {
                    Msg = "Social History Updated Successfully";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

    }
}
