using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.ModelBinding;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic.Visit
{
    public class PatientMasterVisitManager
    {
        private readonly IPatientMasterVisitManager _patientMasterVisitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        private int _result = 0;
        private int _facilityId = 0;

        public PatientMasterVisitManager()
        {
            _facilityId = SessionManager.FacilityId;
        }

        public int AddPatientMasterVisit(PatientMasterVisit pm)
        {

            try
            {
                pm.FacilityId = _facilityId;
                return _result = _patientMasterVisitManager.AddPatientmasterVisit(pm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public int AddPatientMasterVisit(int patientId, int userId, int visitType)
        {
            try
            {
                PatientMasterVisit visit = new PatientMasterVisit
                {
                    PatientId = patientId,
                    ServiceId = 1,
                    Start = DateTime.Now,
                    Active = true,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false,
                    VisitDate = DateTime.Now,
                    CreatedBy = userId,
                    VisitType = visitType,
                    FacilityId = _facilityId
                };

                return _result = _patientMasterVisitManager.AddPatientmasterVisit(visit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            try
            {
                return _patientMasterVisitManager.GetByDate(date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int PatientMasterVisitCheckin(int patientId,int userId)
        {


            var objPpatientMasterVisit=new PatientMasterVisit
            {
                PatientId = patientId,
                ServiceId = 1,
                Status = 1,
                Start = DateTime.Now,
                VisitDate = DateTime.Now,
                CreatedBy =userId,
                FacilityId = _facilityId
            };

            if (patientId > 0)
            {
                _result = _patientMasterVisitManager.PatientMasterVisitCheckin(patientId,objPpatientMasterVisit);
            }

            return _result> 0 ?_result:0;
        }

        public int PatientMasterVisitCheckout(int id,int patientId, int visitSchedule, int visitBy, int visitType, DateTime visitDate)
        {
            _result = _patientMasterVisitManager.PatientMasterVisitCheckout(patientId, id,visitSchedule,visitBy,visitType,visitDate);
            return _result > 0 ? _result : 0;
        }

        public List<PatientMasterVisit> GetNonEnrollmentVisits(int patientId, int visitType)
        {
            try
            {
                return _patientMasterVisitManager.GetNonEnrollmentVisits(patientId, visitType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PatientMasterVisit GetLastPatientVisit(int patientId)
        {
            try
            {
                return _patientMasterVisitManager.GetLastPatientVisit(patientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            try
            {
                return _patientMasterVisitManager.GetPatientVisits(patientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<PatientMasterVisit> GetVisitDateByMasterVisitId(int patientId,int patientmasterVisitId)
        {
            try
            {
                return _patientMasterVisitManager.GetVisitDateByMasterVisitId(patientId,patientmasterVisitId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    
            public PatientMasterVisit GetVisitById(int id)
        {
            try
            {
                return _patientMasterVisitManager.GetVisitById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

     
        public List<PatientMasterVisit> GetPatientMasterVisitBasedonVisitDate(int patientId, DateTime visitDate)
        {
            try
            {
                return _patientMasterVisitManager.GetPatientMasterVisitBasedonVisitDate(patientId, visitDate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
