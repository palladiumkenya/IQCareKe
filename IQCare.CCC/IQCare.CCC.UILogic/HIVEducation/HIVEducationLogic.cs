using Application.Presentation;
using Interface.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using Entities.CCC;

namespace IQCare.CCC.UILogic.HIVEducation
{
    public class HIVEducationLogic
    {
        private IHIVEducation _hiveducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.HIVEducation.BHIVEducation, BusinessProcess.CCC");

        private string Msg { get; set; }
        private int Result { get; set; }

        public int AddPatientHIVEducation(int patientId, DateTime visitdate, int councellingTypeId, string councellingType , int councellingTopicId, string councellingTopic, string comments, string other)
        {
            try
            {
                
                    var PHEF = new HIVEducationFollowup()
                    {
                        Ptn_pk = patientId,
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCounsellingTopics(string counsellingtopics)
    {
        IHIVEducation hiveducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.HIVEducation.BHIVEducation, BusinessProcess.CCC.HIVEducation");
        return hiveducation.getCounsellingTopics(counsellingtopics);

    }
    }
}
