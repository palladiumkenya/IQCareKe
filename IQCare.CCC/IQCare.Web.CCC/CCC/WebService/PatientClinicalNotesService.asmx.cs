using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic.Encounter;
using Entities.CCC.Encounter;

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
    }
}
