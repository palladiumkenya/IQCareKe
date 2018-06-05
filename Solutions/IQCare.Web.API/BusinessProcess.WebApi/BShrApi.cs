using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using DataAccess.Base;

using DataAccess.WebApi;
using DataAccess.WebApi.Repository;

using Interface.WebApi;
using Entities.CCC.PSmart;
using Entities.PSmart;

namespace BusinessProcess.WebApi
{
    public class BShrApi:ProcessBase,IShrApiManager
    {
      
        public SHR GetShrByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                var shr = unitOfWork.ShrRepository
                    .FindBy(x => x.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[0].personId == patientId)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return shr;
            }

        }

        public SHR GetShrBtPatientCardSerialNumber(string serial)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                var shr = unitOfWork.ShrRepository
                    .FindBy(x => x.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[0].ASSIGNING_FACILITY == "O")
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return shr;
            }
        }

        public int LogPSmartRequest(TransactionLog transactionLog)
        {
           using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                unitOfWork.LogRepository.Add(transactionLog);
               int result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int UpdateRequestLog(TransactionLog transactionLog)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                unitOfWork.LogRepository.Update(transactionLog);
                int result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
    }
}