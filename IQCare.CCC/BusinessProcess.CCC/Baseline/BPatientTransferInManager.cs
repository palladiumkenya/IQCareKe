using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientTransferInManager:ProcessBase,IPatientTranfersInManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientTranferIn(PatientTransferIn patientTransferIn)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientTransferInRepository.Add(patientTransferIn);            
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }        
        }

        public int UpdatePatientTransferIn(PatientTransferIn patientTransferIn)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTransfer = _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientTransferIn.PatientId & !x.DeleteFlag).FirstOrDefault();
                if (patientTransfer != null)
                {
                    patientTransfer.CountyFrom = patientTransferIn.CountyFrom;
                    patientTransfer.CurrentTreatment = patientTransferIn.CurrentTreatment;
                    patientTransfer.FacilityFrom = patientTransferIn.FacilityFrom;
                    patientTransfer.MflCode = patientTransferIn.MflCode;
                    patientTransfer.TransferInDate = Convert.ToDateTime(patientTransferIn.TransferInDate);
                    patientTransfer.TreatmentStartDate = patientTransferIn.TreatmentStartDate;
                    patientTransfer.TransferInNotes = patientTransferIn.TransferInNotes;
                }
                _unitOfWork.PatientTransferInRepository.Update(patientTransfer);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientTransferIn(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTransferIn = _unitOfWork.PatientTransferInRepository.GetById(id);
                _unitOfWork.PatientTransferInRepository.Remove(patientTransferIn);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientTransferIn> GetPatientTransferIns(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTransferIn = _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).OrderByDescending(x => x.Id).ToList();
                _unitOfWork.Dispose();
                return patientTransferIn;
            }
        }

        public int CheckifPatientTransferExisits(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTrnasferId = _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).Select(x => x.Id).FirstOrDefault();
                _unitOfWork.Dispose();
                return Convert.ToInt32(patientTrnasferId);
            }
        }

        public void UpdateBlueCardBaselineTransferInHistory(int? ptn_pk, DateTime? confirmHIVPosDate, DateTime? dateEnrolledInCare, int whostage)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, ptn_pk);
            ClsUtility.AddExtendedParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, confirmHIVPosDate);
            ClsUtility.AddExtendedParameters("@DateEnrolledInCare", SqlDbType.DateTime, dateEnrolledInCare);
            ClsUtility.AddExtendedParameters("@WHOStage", SqlDbType.Int, whostage);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "sp_UpdateBlueCardBaselineTransferInHistory", ClsUtility.ObjectEnum.DataTable);
        }

        public void UpdateBlueCardBaselineTransferInTreatment(int? ptn_pk, DateTime? transferInDate, DateTime? treatmentStartDate,
            int currentTreatment, string facilityFrom, int countyFrom)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, ptn_pk);
            ClsUtility.AddExtendedParameters("@TransferInDate", SqlDbType.DateTime, transferInDate);
            ClsUtility.AddExtendedParameters("@TreatmentStartDate", SqlDbType.DateTime, treatmentStartDate);
            ClsUtility.AddExtendedParameters("@CurrentTreatment", SqlDbType.Int, currentTreatment);
            ClsUtility.AddExtendedParameters("@FacilityFrom", SqlDbType.VarChar, facilityFrom);
            ClsUtility.AddExtendedParameters("@CountyFrom", SqlDbType.Int, countyFrom);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "sp_UpdateBlueCardBaselineTransferInTreatment", ClsUtility.ObjectEnum.DataTable);
        }
    }
}
