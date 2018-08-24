using Interface.CCC.Screening;
using System;
using System.Collections.Generic;
using Entities.CCC.Screening;
using Application.Presentation;

namespace IQCare.CCC.UILogic.Screening
{
    public class PatientScreeningManager 
    {
        private IPatientScreeningManager _patientScreening = (IPatientScreeningManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Screening.BPatientScreeningManager, BusinessProcess.CCC");


        public int AddPatientScreening(int patientId,int patientMasterVisitid,DateTime visitDate,int screeningTypeId,bool screeningDone,DateTime screeningDate,int screeningCategoryId,int screeningValueId,string comment,int userId)
        {
            try
            {
                var PS = new PatientScreening()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitid,
                    VisitDate = visitDate,
                    ScreeningTypeId = screeningTypeId,
                    ScreeningDone = screeningDone,
                    ScreeningDate = screeningDate,
                    ScreeningCategoryId = screeningCategoryId,
                    ScreeningValueId = screeningValueId,
                    Comment = comment,
                    CreatedBy = userId
                };
                return _patientScreening.AddPatientScreening(PS);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckIfPatientScreeningExists(int patientId)
        {
            try
            {
                return _patientScreening.CheckIfPatientScreeningExists(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckIfPatientScreeningExists(int patientId, DateTime visitDate, int screeningCategoryId, int screeningTypeId)
        {
            try
            {
                return _patientScreening.CheckIfPatientScreeningExists(patientId, visitDate, screeningCategoryId, screeningTypeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeletePatientScreening(int Id)
        {
            try
            {
                return _patientScreening.DeletePatientScreening(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PatientScreening> GetPatientScreening(int patientId)
        {
            try
            {
                return _patientScreening.GetPatientScreening(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PatientScreening> GetPatientScreening(int patientId, DateTime visitDate, int screeningCategoryId)
        {
            try
            {
                return _patientScreening.GetPatientScreening(patientId, visitDate, screeningCategoryId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdatePatientScreening(int id,DateTime visitDate ,int screeningTypeId, bool screeningDone, DateTime screeningDate, int screeningCategoryId, int screeningValueId, string comment)
        {
            try
            {
                var PS = new PatientScreening()
                {
                    Id = id,
                    VisitDate = visitDate,
                    ScreeningTypeId = screeningTypeId,
                    ScreeningDone = screeningDone,
                    ScreeningDate = screeningDate,
                    ScreeningCategoryId = screeningCategoryId,
                    ScreeningValueId = screeningValueId,
                    Comment = comment
                };

                return _patientScreening.UpdatePatientScreening(PS);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
