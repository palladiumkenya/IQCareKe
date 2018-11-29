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


        public int AddPatientScreening(int patientId,int patientMasterVisitid,DateTime visitDate,int screeningTypeId,bool? screeningDone,DateTime screeningDate,int screeningCategoryId,int screeningValueId,string comment,int userId)
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
        public int AddUpdatePatientScreening(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                int screeningResult;
                if (patientMasterVisitId > 0)
                {
                    screeningResult = _patientScreening.checkScreeningByVisitId(patientId, patientMasterVisitId, screeningType, screeningCategory);
                }
                else {
                    //(screening>0) ? update:add
                    screeningResult = _patientScreening.checkScreeningByScreeningCategoryId(patientId, screeningType, screeningCategory);
                }
               if (screeningResult > 0)
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId,
                        Id = screeningResult
                    };
                    return _patientScreening.updatePatientScreeningById(PS);
                }
                else
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId
                    };
                    return _patientScreening.AddPatientScreening(PS);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int AddUpdatePatientScreeningByVisitId(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                //(screening>0) ? update:add
                int screeningResult = _patientScreening.checkScreeningByVisitId(patientId, patientMasterVisitId, screeningType, screeningCategory);
                if (screeningResult > 0)
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId,
                        Id = screeningResult
                    };
                    return _patientScreening.updatePatientScreeningById(PS);
                }
                else
                {
                    var PS = new PatientScreening()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        VisitDate = DateTime.Today,
                        ScreeningTypeId = screeningType,
                        ScreeningDone = true,
                        ScreeningDate = DateTime.Today,
                        ScreeningCategoryId = screeningCategory,
                        ScreeningValueId = screeningValue,
                        Comment = null,
                        CreatedBy = userId
                    };
                    return _patientScreening.AddPatientScreening(PS);
                }
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
        public PatientScreening GetCurrentPatientScreening(int patientId, int patientmastervisitid)
        {
            try
            {
              
               var psc = _patientScreening.GetCurrentPatientScreening(patientId,patientmastervisitid);
                
                return psc;
             
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int UpdateCurrentPatientScreening(PatientScreening psc)
        {
            try
            {
               

                return _patientScreening.UpdatePatientScreening(psc);
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
                    ScreeningDone = Convert.ToBoolean(screeningDone),
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
        public List<PatientScreening> GetPatientScreeningByVisitId(int patientId, int patientMasterVisitId)
        {
            try
            {
                return _patientScreening.GetPatientScreeningByVisitId(patientId, patientMasterVisitId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<PatientScreening> GetPatientScreeningStatus(int patientId, int statusId)
        {
            try
            {
                return _patientScreening.GetPatientScreeningStatus(patientId, statusId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
