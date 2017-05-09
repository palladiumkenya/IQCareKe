using System.Collections.Generic;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientTreatmentTrackerManager : ProcessBase,IPatientTreatmentTrackerManager
    {
        public PatientTreamentTrackerLookup GetCurrentPatientRegimen(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var regimen = unitOfWork.PatientTreatmentTrackerLookupRepository.GetCurrentPatientRegimen(patientId);
                unitOfWork.Dispose();
                return regimen;
            }
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentInterrupList(int patientId)
        {
            using (UnitOfWork unitOfWork=new UnitOfWork(new LookupContext()))
            {
                var interruptions =
                    unitOfWork.PatientTreatmentTrackerLookupRepository.GetPatientTreatmentInterrupList(patientId);
                unitOfWork.Dispose();
                return interruptions;
            }
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSubstitutionList(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var substitution =
                    unitOfWork.PatientTreatmentTrackerLookupRepository.GetPatientTreatmentSubstitutionList(patientId);
                unitOfWork.Dispose();
                return substitution;
            }
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSwitchesList(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var switches =
                    unitOfWork.PatientTreatmentTrackerLookupRepository.GetPatientTreatmentSwitchesList(patientId);
                unitOfWork.Dispose();
                return switches;

            }
        }
    }
}
