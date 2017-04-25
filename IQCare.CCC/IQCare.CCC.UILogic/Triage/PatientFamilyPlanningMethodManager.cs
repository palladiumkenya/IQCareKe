using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using Application.Presentation;
using Interface.CCC.Triage;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientFamilyPlanningMethodManager 
    {
        private IPatientFamilyPlanningMethodManager _PatientFamilyPlanningMethod = (IPatientFamilyPlanningMethodManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientFamilyPlanningMethod, BusinessProcess.CCC");

        public int AddFamilyPlanningMethod(int patientId,int PatientFPId,int userId)
        {
            try
            {
                var FPLoad = new PatientFamilyPlanningMethod()
                {
                    PatientId = patientId,
                    PatientFPId = PatientFPId,
                    CreatedBy = userId
                };

                return _PatientFamilyPlanningMethod.AddFamilyPlanningMethod(FPLoad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateFamilyPlanningMethod (int patientFPId,int id)
        {
            try
            {
                var FPLoad = new PatientFamilyPlanningMethod()
                {
                    Id = id,
                    PatientFPId = patientFPId
                };

                return _PatientFamilyPlanningMethod.UpdateFamilyPlanningMethod(FPLoad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteFamilyPlanningMethod(int id)
        {
            try
            {
                return _PatientFamilyPlanningMethod.DeleteFamilyPlanningMethod(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PatientFamilyPlanningMethod> GetPatientFamilyPlanningMethod(int patientId)
        {
            try
            {
                return _PatientFamilyPlanningMethod.GetPatientFamilyPlanningMethod(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckIfPatientHasFamilyPlanningMethod (int patientId)
        {
            try
            {
                return _PatientFamilyPlanningMethod.CheckIfPatientHasFamilyPlanningMethod(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
