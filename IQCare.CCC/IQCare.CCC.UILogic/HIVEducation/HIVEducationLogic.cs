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

        public int AddPatientHIVEducation(int ptn_pk, int locationId, int userId, int visit_Pk, DateTime visitdate, int councellingTypeId, string councellingType , int councellingTopicId, string councellingTopic, string comments, string other)
        {
            try
            {
                return _hiveducation.AddPatientHIVEducation(0, ptn_pk, visit_Pk, locationId, councellingTypeId,
                    councellingTopicId, visitdate, comments, null, userId, false, 203);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPatientFollowupEducationData(int ptnPk)
        {
            try
            {
                return _hiveducation.GetPatientFollowupEducationData(ptnPk);
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
