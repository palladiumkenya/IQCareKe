using Application.Presentation;
using Entities.CCC.HIVEducation;
using Interface.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;

namespace IQCare.CCC.UILogic.HIVEducation
{
    public class HIVEducationLogic
    {
        // private IHIVEducation _hivEducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BPatientClinicalNotes, BusinessProcess.CCC");
        private IHIVEducation _hiveducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.HIVEducation.BHIVEducation, BusinessProcess.CCC.HIVEducation");

        private string Msg { get; set; }
        private int Result { get; set; }
        [WebMethod]
        public int AddPatientHIVEducation(int patientId, DateTime visitdate, int councellingTypeId, string councellingType , int councellingTopicId, string councellingTopic, string comments, string other)
        {
            try
            {
                
                    var PHEF = new HIVEducationFollowup()
                    {
                        PatientId = patientId,
                        VisitDate = visitdate,
                        CouncellingTypeId = councellingTypeId,
                        CouncellingType = councellingType,
                        CouncellingTopicId = councellingTopicId,
                        CouncellingTopic = councellingTopic,
                        Comments = comments,
                        CouncellingTopicOther = other
                    };
                    return _hiveducation.AddPatientHIVEducation(PHEF);
               // }
                //}
                //else
                //{
                //    var PCN = new PatientClinicalNotes()
                //    {
                //        PatientId = patientId,
                //        PatientMasterVisitId = patientMasterVisitId,
                //        ServiceAreaId = serviceAreaId,
                //        ClinicalNotes = clinicalNotes,
                //        CreatedBy = userId,
                //        //VersionStamp = DateTime.UtcNow,
                //        NotesCategoryId = notesCategoryId
                //    };
                //    return _patientNotes.AddPatientClinicalNotes(PCN);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable getCounsellingTopics(string counsellingtopics)
    {
        IHIVEducation hiveducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.HIVEducation.BHIVEducation, BusinessProcess.CCC.HIVEducation");
        return hiveducation.getCounsellingTopics(counsellingtopics);

    }
    }
}
