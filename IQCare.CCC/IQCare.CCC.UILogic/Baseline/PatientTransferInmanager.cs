using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientTransferInmanager
    {
        private readonly IPatientTranfersInManager  _patientTranfersIn = (IPatientTranfersInManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientTransferInManager, BusinessProcess.CCC");
        private int _result;

        public int AddpatientTransferIn(int patientId, int patientMastervisitId, int serviceAreaId, DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes,int userId)
        {
            PatientTransferIn patientTransferIn=new PatientTransferIn()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMastervisitId,
                ServiceAreaId = serviceAreaId,
                TransferInDate = transferinDate,
                TreatmentStartDate = treatmentStartDate,
                CurrentTreatment = currentTreatment,
                FacilityFrom = facilityFrom,
                MflCode = mflCode,
                CountyFrom = countyFrom,
                TransferInNotes = transferInNotes,
                CreatedBy = userId
            };

            return _result=_patientTranfersIn.AddPatientTranferIn(patientTransferIn);
        }

        public int UpdatePatientTransferIn(int patientId, int patientMastervisitId, int serviceAreaId,
            DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes,int userId)
        {
            PatientTransferIn patientTransferIn = new PatientTransferIn()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMastervisitId,
                ServiceAreaId = serviceAreaId,
                TransferInDate = transferinDate,
                TreatmentStartDate = treatmentStartDate,
                CurrentTreatment = currentTreatment,
                FacilityFrom = facilityFrom,
                MflCode = mflCode,
                CountyFrom = countyFrom,
                TransferInNotes = transferInNotes,
                CreatedBy = userId
            };

            return _result = _patientTranfersIn.AddPatientTranferIn(patientTransferIn);
        }

        public int DeletePatientTransferIn(int id)
        {
            return _result=_patientTranfersIn.DeletePatientTransferIn(id);
        }

        public List<PatientTransferIn> GetPatientTransferIns(int patientId)
        {
            return _patientTranfersIn.GetPatientTransferIns(patientId);
        }

    }
}
