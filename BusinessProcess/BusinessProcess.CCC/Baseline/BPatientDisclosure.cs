using Interface.CCC.Baseline;
using Entities.CCC.Baseline;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using System;
using DataAccess.Base;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientDisclosure : ProcessBase, IPatientDisclosureManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientDisclosure(PatientDisclosure patientDisclosure)
        {
            _unitOfWork.PatientDisclosureRepository.Add(patientDisclosure);
            return Result = _unitOfWork.Complete();
        }

        public int DeletePatientDisclosure(int id)
        {
            var personEncounter = _unitOfWork.PatientDisclosureRepository.GetById(id);
            _unitOfWork.PatientDisclosureRepository.Remove(personEncounter);
            return Result = _unitOfWork.Complete();
        }

        /*public List<PatientDisclosure> GetPatientDisclosureAll(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientDisclosure> GetPatientDisclosures(int patientId, DateTime visitDate)
        {
            _unitOfWork.PatientEncounterRepository.Update(patientEncounter);
            return Result = _unitOfWork.Complete();
        }*/

        public int UpdatePatientDisclosure(PatientDisclosure patientDisclosure)
        {
            _unitOfWork.PatientDisclosureRepository.Update(patientDisclosure);
            return Result = _unitOfWork.Complete();
        }
    }
}
