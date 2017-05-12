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

        public int AddFamilyPlanningStatus(int patientId,int patientMasterVisitId,DateTime visitDate,int familyPlanningStatusId,int reasonNoOnFp,int userId )
        {
            try
            {
                var fpLoad = new PatientFamilyPlanning()
                {
                    PatientId=patientId,
                    PatientMasterVisitId= patientMasterVisitId,
                    VisitDate = visitDate,
                    FamilyPlanningStatusId=familyPlanningStatusId,
                    ReasonNotOnFPId=reasonNoOnFp,
                    CreatedBy=userId
                };

                return _PatientFamilyPlanning.AddFamilyPlanningStatus(fpLoad);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateFamilyPlanningStatus(DateTime visitDate,int familyPlanningStatusId, int reasonNoOnFp,int id)
        {
            try
            {
                var FPLoad = new PatientFamilyPlanning()
                {
                    Id=id,
                    VisitDate = visitDate,
                    FamilyPlanningStatusId = familyPlanningStatusId,
                    ReasonNotOnFPId = reasonNoOnFp
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

        public int DeleteFamilyPlanningStatus(int id)
        {
            try
            {
                return _PatientFamilyPlanning.DeleteFamilyPlanningStatus(id);
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
