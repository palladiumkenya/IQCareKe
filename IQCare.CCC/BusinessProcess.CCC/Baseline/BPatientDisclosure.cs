using Interface.CCC.Baseline;
using Entities.CCC.Baseline;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Base;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientDisclosure : ProcessBase, IPatientDisclosureManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientDisclosure(PatientDisclosure patientDisclosure)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientDisclosureRepository.Add(patientDisclosure);
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientDisclosure(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var personEncounter = _unitOfWork.PatientDisclosureRepository.GetById(id);
                _unitOfWork.PatientDisclosureRepository.Remove(personEncounter);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }            
        }

        public int UpdatePatientDisclosure(PatientDisclosure patientDisclosure)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientDisclosureRepository.Update(patientDisclosure);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientDisclosure> GetPatientDisclosures(int patientId, string category, string disclosureStage)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
              var patientDisclosureList=  _unitOfWork.PatientDisclosureRepository.FindBy( x =>
                  x.PatientId == patientId && x.Category == category &&
                  x.DisclosureStage == disclosureStage)
                  .ToList();
                _unitOfWork.Dispose();
                return patientDisclosureList;
            }
        }
    }
}
