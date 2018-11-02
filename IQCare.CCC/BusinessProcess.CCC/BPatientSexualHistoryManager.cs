using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.CCC
{
    public class BPatientSexualHistoryManager:ProcessBase,IPatientSexualHistoryManager
    {

        internal int Result = 0;

        public PatientSexualHistory AddPatientSexualHistory(PatientSexualHistory a)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientSexualHistoryRepository.Add(a);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return a;
            }
        }

        public PatientSexualHistory UpdatePatientSexualHistory(PatientSexualHistory u)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientSexualHistoryRepository.Update(u);
                Result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return u;
                
            }
           
            
        }

       public List<PatientSexualHistory> GetPatientSexualHistoryList(int patientId,int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var sexualhistory = unitOfWork.PatientSexualHistoryRepository.FindBy(x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return sexualhistory;
            }

        }
        public PatientSexualHistory GetPatientSexualHistory(int patientId,int patientMasterVisitId,int Id )
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var sexualhistory = unitOfWork.PatientSexualHistoryRepository.FindBy(x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.Id == Id && !x.DeleteFlag).FirstOrDefault();
                unitOfWork.Dispose();
                return sexualhistory;
            }
        }

        
    }

}
