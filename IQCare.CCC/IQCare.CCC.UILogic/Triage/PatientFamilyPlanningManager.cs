using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Triage;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientFamilyPlanningManager 
    {
        private IpatientFamilyPlanningManager _PatientFamilyPlanning = (IpatientFamilyPlanningManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientFamilyPlanningManager, BusinessProcess.CCC");

        public int AddFamilyPlanningStatus(int patientId,int patientMasterVisitId,int FamilyPlanningStatusId,int ReasonNoOnFp,int userId )
        {
            try
            {
                var FPLoad = new PatientFamilyPlanning()
                {
                    PatientId=patientId,
                    PatientMasterVisitId= patientMasterVisitId,
                    FamilyPlaningStatusId=FamilyPlanningStatusId,
                    ReasonNotOnFP=ReasonNoOnFp,
                    CreatedBy=userId
                };

                return _PatientFamilyPlanning.AddFamilyPlanningStatus(FPLoad);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateFamilyPlanningStatus(int FamilyPlanningStatusId, int ReasonNoOnFp,int id)
        {
            try
            {
                var FPLoad = new PatientFamilyPlanning()
                {
                    Id=id,
                    FamilyPlaningStatusId = FamilyPlanningStatusId,
                    ReasonNotOnFP = ReasonNoOnFp
                };

                return _PatientFamilyPlanning.UpdateFamilyPlanningStatus(FPLoad);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int CheckFamilyPlanningExists(int patientId)
        {
            try
            {
                return _PatientFamilyPlanning.CheckFamilyPlanningExists(patientId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int DeleteFamilyPlanningStatus(int Id)
        {
            try
            {
                return _PatientFamilyPlanning.DeleteFamilyPlanningStatus(Id);
            }
            catch (Exception ex)
            {
              throw new Exception(ex.Message);
            }
        }

        public List<PatientFamilyPlanning> GetPatientFamilyPlanningStatus(int patientId)
        {
            try
            {
                return _PatientFamilyPlanning.GetPatientFamilyPlanningStatus(patientId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
 
    }
}
