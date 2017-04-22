using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using Application.Presentation;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientPregnancyIndicatorManager
    {
        private IpatientPregnancyIndicatorManager _PregnancyIndicator = (IpatientPregnancyIndicatorManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.PatientPregnancyIndicatorManager, BusinessProcess.CCC");

        public int AddPregnancyIndicator(int patientId,int patientMasterVisitId,DateTime lmp,DateTime edd,int pregnancyStatusId,bool ancProfile,DateTime ancProfileDate,int userId)
        {
            try
            {
                var PG = new PatientPregnancyIndicator()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    LMP = lmp,
                    EDD = edd,
                    PregnancyStatusId = pregnancyStatusId,
                    ANCProfile = ancProfile,
                    ANCProfileDate = ancProfileDate,
                    CreatedBy = userId
                };

                return _PregnancyIndicator.AddPregnancyIndicator(PG);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckIfPregnancyIndicatorExisists(int patientId)
        {
            return _PregnancyIndicator.CheckIfPregnancyIndicatorExisists(patientId);
        }

        public int DeletePregnancyIndicator(int Id)
        {
            return _PregnancyIndicator.DeletePregnancyIndicator(Id);
        }

        public List<PatientPregnancyIndicator> GetPregnancyIndicator(int patientId)
        {
            try
            {
                return _PregnancyIndicator.GetPregnancyIndicator(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdatePreganacyIndcator(int Id,DateTime lmp, DateTime edd, int pregnancyStatusId, bool ancProfile, DateTime ancProfileDate)
        {
            try
            {
                var PG = new PatientPregnancyIndicator()
                {
                    Id = Id,
                    LMP = lmp,
                    EDD = edd,
                    PregnancyStatusId = pregnancyStatusId,
                    ANCProfile = ancProfile,
                    ANCProfileDate = ancProfileDate
                };
                return _PregnancyIndicator.UpdatePreganacyIndcator(PG);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
