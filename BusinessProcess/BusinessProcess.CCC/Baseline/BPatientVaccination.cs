using DataAccess.Base;
using Interface.CCC.Baseline;
using System;
using Entities.CCC.Encounter;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientVaccination : ProcessBase, IPatientVaccinationManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int addPatientVaccination(PatientVaccination patientVaccination)
        {
            _unitOfWork.PatientVaccinationRepository.Add(patientVaccination);
            Result = _unitOfWork.Complete();
            return Result;
        }

        public int DeletePatientVaccination(int id)
        {
            throw new NotImplementedException();
        }

        public int updatePatientVaccination(PatientVaccination patientVaccination)
        {
            throw new NotImplementedException();
        }
    }
}
