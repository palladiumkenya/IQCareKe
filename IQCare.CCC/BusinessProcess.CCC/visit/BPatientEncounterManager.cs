using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC.visit
{

    public class BPatientEncounterManager :IPatientEncounterManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddpatientEncounter(PatientEncounter patientEncounter)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientEncounterRepository.Add(patientEncounter);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientEncounter(PatientEncounter patientEncounter)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientEncounterRepository.Update(patientEncounter);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientEncounter(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var personEncounter = _unitOfWork.PatientEncounterRepository.GetById(id);
                _unitOfWork.PatientEncounterRepository.Remove(personEncounter);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientEncounter> GetPatientCurrentEncounters(int patientId, DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientEncounter> patientEncounters =
                   _unitOfWork.PatientEncounterRepository.FindBy(
                           x =>
                               x.PatientId == patientId &
                               DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                               !x.DeleteFlag)
                       .OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return patientEncounters;
            }
        }

        public List<PatientEncounter> GetPatientEncounterAll(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientEncounter> patientEncounters =
                   _unitOfWork.PatientEncounterRepository.FindBy(
                           x =>
                               x.PatientId == patientId &
                               !x.DeleteFlag)
                       .OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return patientEncounters;
            }
        }
    }
}
