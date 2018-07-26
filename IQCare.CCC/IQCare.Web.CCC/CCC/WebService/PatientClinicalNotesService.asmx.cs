using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using Entities.CCC.Encounter;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic;
using System.Web.Script.Serialization;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientClinicalNotesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientClinicalNotesService : System.Web.Services.WebService
    {

        private string Msg { get; set; }
        private int Result { get; set; }
        [WebMethod]
        public string addPatientClinicalNotes(int patientId, int patientMasterVisitId, int serviceAreaId, int notesCategoryId, string clinicalNotes, int userId)
        {
            try
            {
                var PCN = new PatientClinicalNotesLogic();
                Result = PCN.addPatientClinicalNotes(patientId,patientMasterVisitId,serviceAreaId,notesCategoryId,clinicalNotes,userId);
                if (Result > 0)
                {
                    Msg = "Notes Added";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public List<PatientClinicalNotes> getPatientClinicalNotesByCategory(int patientId,int notesCategoryId)
        {
            try
            {
                var PCN = new PatientClinicalNotesLogic();
                return PCN.getPatientClinicalNotesById(patientId,notesCategoryId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [WebMethod]
        public string getDepressionSeverityNotes(int depressionFrequency)
        {
            string depressionSeverity = "";
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("DepressionSeverity");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (Convert.ToInt32(value.OrdRank) > depressionFrequency)
                {
                    break;
                }
                depressionSeverity = value.DisplayName;
            }
            return depressionSeverity;
        }
        [WebMethod]
        public string getDepressionRMNotes(int depressionFrequency)
        {
            string recommendedManagement = "";
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("RecommendedManagement");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (Convert.ToInt32(value.OrdRank) > depressionFrequency)
                {
                    break;
                }
                recommendedManagement = value.DisplayName;
            }
            return recommendedManagement;
        }
        [WebMethod]
        public string getAlcoholRiskNotes(int alcoholScore)
        {
            string depressionSeverity = "";
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("AlcoholRiskLevel");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (Convert.ToInt32(value.OrdRank) == alcoholScore)
                {
                    depressionSeverity = value.DisplayName;
                    break;
                }
            }
            return depressionSeverity;
        }
        [WebMethod]
        public string getMmasRating(string MmasScore)
        {
            string mmasRating = "";
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("MmasRating");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (value.OrdRank.ToString() == MmasScore)
                {
                    mmasRating = value.DisplayName;
                    break;
                }
            }
            return mmasRating;
        }
        [WebMethod]
        public string getMmasRecommendation(string MmasScore)
        {
            string mmasRecommendation = "";
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("MmasRecommendation");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (value.OrdRank.ToString() == MmasScore)
                {
                    mmasRecommendation = value.DisplayName;
                    break;
                }
            }
            return mmasRecommendation;
        }
    }
}
