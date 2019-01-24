using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientOIManager : ProcessBase, IPatientOIManager
    {
        public PatientOI addPatientOI(PatientOI patientoi)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientOIRepository.Add(patientoi);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientoi;
            }
        }

        public PatientOI GetPatientOI(int patientId,int patientMasterVisitId,int OI)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PatientOI = unitOfWork.PatientOIRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.OIId==OI ).FirstOrDefault();
                unitOfWork.Dispose();
                return PatientOI;
            }
        }
        public List<PatientOI> GetPatientOIs(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var OI = unitOfWork.PatientOIRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId ).ToList();
                unitOfWork.Dispose();
                return OI.ToList();
            }
        }

        public List<PatientOI> GetOIListByPatient(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var OI = unitOfWork.PatientOIRepository.FindBy(
                    x => x.PatientId == patientId && !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return OI.ToList();
            }
        }
        public PatientOI GetPatientOIbyId(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientoi = unitOfWork.PatientOIRepository.GetById(entityId);
                unitOfWork.Dispose();
                return patientoi;
            }
        }

        public PatientOI UpdatePatientOI(PatientOI patientOI)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientOIRepository.Update(patientOI);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientOI;
            }
        }
    }
}
