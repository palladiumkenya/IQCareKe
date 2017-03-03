using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientTransferInManager:ProcessBase,IPatientTranfersInManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientTranferIn(PatientTransferIn patientTransferIn)
        {
            _unitOfWork.PatientTransferInRepository.Add(patientTransferIn);
            return Result = _unitOfWork.Complete();
        }

        public int UpdatePatientTransferIn(PatientTransferIn patientTransferIn)
        {
            var patientTransfer =
                _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientTransferIn.PatientId & !x.DeleteFlag)
                    .FirstOrDefault();
            if (patientTransfer != null)
            {
                patientTransfer.CountyFrom = patientTransferIn.CountyFrom;
                patientTransfer.CurrentTreatment = patientTransferIn.CurrentTreatment;
                patientTransfer.FacilityFrom = patientTransferIn.FacilityFrom;
                patientTransfer.MflCode = patientTransferIn.MflCode;
                patientTransfer.TransferInDate = patientTransferIn.TransferInDate;
                patientTransfer.TreatmentStartDate = patientTransferIn.TreatmentStartDate;
            }
            _unitOfWork.PatientTransferInRepository.Update(patientTransferIn);
            return Result = _unitOfWork.Complete();
        }

        public int DeletePatientTransferIn(int id)
        {
            var patientTransferIn = _unitOfWork.PatientTransferInRepository.GetById(id);
            _unitOfWork.PatientTransferInRepository.Remove(patientTransferIn);
            return Result=_unitOfWork.Complete();
        }

        public List<PatientTransferIn> GetPatientTransferIns(int patientId)
        {
            var patientTransferIn =
                _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id)
                    .Take(1)
                    .ToList();
            return patientTransferIn;
        }

        public int CheckifPatientTransferExisits(int patientId)
        {
            var patientTrnasferId =
                _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            return Convert.ToInt32(patientTrnasferId);

        }
    }
}
