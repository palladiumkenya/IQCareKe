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
            try
            {
                _unitOfWork.PatientTransferInRepository.Add(patientTransferIn);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
         
        }

        public int UpdatePatientTransferIn(PatientTransferIn patientTransferIn)
        {
            try
            {
                var patientTransfer =
                    _unitOfWork.PatientTransferInRepository.FindBy(
                            x => x.PatientId == patientTransferIn.PatientId & !x.DeleteFlag)
                        .FirstOrDefault();
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
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public int DeletePatientTransferIn(int id)
        {
            try
            {
                var patientTransferIn = _unitOfWork.PatientTransferInRepository.GetById(id);
                _unitOfWork.PatientTransferInRepository.Remove(patientTransferIn);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
   
        }

        public List<PatientTransferIn> GetPatientTransferIns(int patientId)
        {
            try
            {
                var patientTransferIn =
                    _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .OrderByDescending(x => x.Id)
                        .ToList();
                return patientTransferIn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public int CheckifPatientTransferExisits(int patientId)
        {
            try
            {
                var patientTrnasferId =
                    _unitOfWork.PatientTransferInRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .Select(x => x.Id)
                        .FirstOrDefault();
                return Convert.ToInt32(patientTrnasferId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }


        }
    }
}
