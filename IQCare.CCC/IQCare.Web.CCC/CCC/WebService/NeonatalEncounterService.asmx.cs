using Entities.CCC.Neonatal;
using System;
using System.Web.Services;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Encounter;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Services;


namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for NeonatalEncounterService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NeonatalEncounterService : System.Web.Services.WebService
    {

        private string Msg { get; set; }
        private int Result{get; set;}

        [WebMethod(EnableSession = true)]
        public string addNeonatalMilestones(int patientId, int patientMasterVisitId, int createdBy, int milestoneAssessed, DateTime milestoneOnsetDate, int milestoneAchieved, int milestoneStatus, string milestoneComment)
        {
            try
            {
                PatientMilestone patientNeonatal = new PatientMilestone()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    CreatedBy = createdBy,
                    milestoneAssessedId = milestoneAssessed,
                    milestoneDate = milestoneOnsetDate,
                    milestoneAchievedId = milestoneAchieved,
                    milestoneStatusId = milestoneStatus,
                    milestoneComments = milestoneComment
                };
                var neonatal = new PatientNeonatalManager();
                //Check if milestone assessed exists
                var MilestonesLogic = new NeonatalHistoryLogic();
                List<PatientMilestone> list = new List<PatientMilestone>();
                list = MilestonesLogic.getMilestoneAssessed(milestoneAssessed);
                int existingMilestone = 0;
                foreach (var items in list)
                {
                    existingMilestone = items.milestoneAssessedId;
                }

                if(existingMilestone == milestoneAssessed)
                {
                    Msg = "Existing";
                }
                else
                {
                    Result = neonatal.AddPatientNeonatal(patientNeonatal);
                    if (Result > 0)
                    {
                        Msg = "Neonatal Milestone Added Successfully!";
                    }
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addImmunizationHistory(int patientId, int patientMasterVisitId, int createdBy, int immunizationPeriod, int immunizationGiven, DateTime immunizationDate)
        {
            try
            {
                PatientImmunizationHistory immunizationHistory = new PatientImmunizationHistory()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    CreatedBy = createdBy,
                    ImmunizationPeriodId = immunizationPeriod,
                    ImmunizationGivenId = immunizationGiven,
                    ImmunizationDate = immunizationDate
                };
                var ImmunizationHistory = new PatientNeonatalManager();
                Result = ImmunizationHistory.AddImmunizationHistory(immunizationHistory);
                if (Result > 0)
                {
                    Msg = "Immunization History Added Successfully!"; 
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addPatientNeonatalHistory(int patientId, int patientMasterVisitId, string notes, int recordNeonatalHistory)
        {
            try
            {
                var NeonatalNotes = new PatientNeonatalManager();
                Result = NeonatalNotes.addPatientNeonatalHistory(patientId,patientMasterVisitId, notes, recordNeonatalHistory);
                if(Result > 0)
                {
                    Msg = "Neonatal notes added successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string updatePatientNeonatalHistory(int patientId, int patientMasterVisitId, string notes,int recordNeonatalHistory, int notesid)
        {
            try
            {
                var SH = new NeonatalHistoryLogic();
                Result = SH.updateNeonatalNotes(patientId, patientMasterVisitId, notes, recordNeonatalHistory, notesid);
                if (Result > 0)
                {
                    Msg = "Neonatal Notes Updated Successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadMilestones()
        {
            ArrayList rows = new ArrayList();
            var MilestonesLogic = new NeonatalHistoryLogic();
            List<PatientMilestone> list = new List<PatientMilestone>();
            list = MilestonesLogic.getPatientMilestones(Convert.ToInt32(Session["PatientPK"]));
            foreach(var items in list)
            {
                string[] i = new string[7] { items.Id.ToString(), LookupLogic.GetLookupNameById(items.milestoneAssessedId).ToString(), items.milestoneDate.ToString("dd-MMM-yyyy"), items.milestoneAchievedId == 1?"Yes":"No", LookupLogic.GetLookupNameById(items.milestoneStatusId).ToString(),items.milestoneComments.ToString(),"<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"};
                rows.Add(i);
            }
            return rows;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadImmunization()
        {
            ArrayList rows = new ArrayList();
            var immunizationLogic = new ImmunizationLogic();
            List<PatientImmunizationHistory> list = new List<PatientImmunizationHistory>();
            list = immunizationLogic.getPatientImmunization(Convert.ToInt32(Session["PatientPK"]));
            foreach(var items in list)
            {
                string[] i = new string[5] { items.Id.ToString(), LookupLogic.GetLookupNameById(Convert.ToInt32(items.ImmunizationPeriodId)).ToString(), LookupLogic.GetLookupNameById(Convert.ToInt32(items.ImmunizationGivenId)).ToString(), Convert.ToDateTime(items.ImmunizationDate).ToString("dd-MMM-yyyy"), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod]
        public string DeleteMilestone(int milestoneId)
        {
            try
            {
                var milestoneLogic  = new NeonatalHistoryLogic();
                milestoneLogic.DeleteMilestone(milestoneId);
                Msg = "Deleted";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string DeleteImmunization(int ImmunizationId)
        {
            try
            {
                var immunizationLogic = new PatientNeonatalManager();
                immunizationLogic.DeleteImmunization(ImmunizationId);
                Msg = "Deleted";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}
