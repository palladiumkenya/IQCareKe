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
        private int Id=0;

        public int ManagePatientTransferIn(int patientId, int patientMastervisitId, int serviceAreaId, DateTime transferinDate, DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom, string transferInNotes,int userId)
        {
            Id = _patientTranfersIn.CheckifPatientTransferExisits(patientId);

            PatientTransferIn patientTransferIn = new PatientTransferIn()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMastervisitId,
                ServiceAreaId = 1,
                TransferInDate = transferinDate,
                TreatmentStartDate = treatmentStartDate,
                CurrentTreatment = currentTreatment,
                FacilityFrom = facilityFrom,
                MflCode = mflCode,
                CountyFrom = countyFrom,
                TransferInNotes = transferInNotes,
                CreatedBy = userId
            };
            _result = (Id > 0)? _patientTranfersIn.UpdatePatientTransferIn(patientTransferIn): _patientTranfersIn.AddPatientTranferIn(patientTransferIn);
           // _result = _patientTranfersIn.AddPatientTranferIn(patientTransferIn);
            return _result;
        }

        public int UpdatePatientTransferIn(int patientId, int patientMastervisitId, int serviceAreaId,
            DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes,int userId)
        {
            PatientTransferIn patientTransferIn = new PatientTransferIn()
            {
                TransferInDate = transferinDate,
                TreatmentStartDate = treatmentStartDate,
                CurrentTreatment = currentTreatment,
                FacilityFrom = facilityFrom,
                MflCode = mflCode,
                CountyFrom = countyFrom,
                TransferInNotes = transferInNotes,
        };

            return _result = _patientTranfersIn.UpdatePatientTransferIn(patientTransferIn);
        }

        public int DeletePatientTransferIn(int id)
        {
            return _result=_patientTranfersIn.DeletePatientTransferIn(id);
        }

        public List<PatientTransferIn> GetPatientTransferIns(int patientId)
        {
            return _patientTranfersIn.GetPatientTransferIns(patientId);
        }

        public int CheckIfPatientTransferExisit(int patientId)
        {
            return _patientTranfersIn.CheckifPatientTransferExisits(patientId);
        }

    }
}
