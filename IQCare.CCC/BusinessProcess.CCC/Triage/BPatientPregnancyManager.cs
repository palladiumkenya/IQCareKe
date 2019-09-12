using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;
using DataAccess.Base;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientPregnancyManager :ProcessBase, IPatientPregnancyManager
    {
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int result;

        public int AddPatientPregnancy(PatientPreganancy a)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientPregnancyRepository.Add(a);
                result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int UpdatePatientPreganacy(PatientPreganancy patientPreganancy)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PG =
                   _unitOfWork.PatientPregnancyRepository.FindBy(
                           x => x.PatientId == patientPreganancy.PatientId & !x.DeleteFlag)
                       .FirstOrDefault();
                if (PG != null)
                {
                    PG.LMP = patientPreganancy.LMP;
                    PG.EDD = patientPreganancy.EDD;
                    PG.Gravidae = patientPreganancy.Gravidae;
                    PG.Parity = patientPreganancy.Parity;
                    PG.Outcome = patientPreganancy.Outcome;
                    PG.DateOfOutcome = patientPreganancy.DateOfOutcome;
                }
                _unitOfWork.PatientPregnancyRepository.Update(PG);
                result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }


        public int DeletePatientPregnancy(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PG = _unitOfWork.PatientPregnancyRepository.GetById(Id);
                _unitOfWork.PatientPregnancyRepository.Remove(PG);
                result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int CheckIfPatientPregnancyExisists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientPreganancy pregnancy =_unitOfWork.PatientPregnancyRepository.FindBy(x => x.PatientId == patientId && !x.DeleteFlag && (x.Outcome == 0 || x.Outcome == null)).FirstOrDefault();
                _unitOfWork.Dispose();
                if (pregnancy != null)
                    return pregnancy.Id;
                return 0;

               
            }
        }


        public List<PatientPreganancy> GetPatientPregnancy(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var pg= _unitOfWork.PatientPregnancyRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return pg;
            }
        }

        public int UpdatePatientPregnancyOutcome(PatientPreganancy pregnancy)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPregnancyRepository.Update(pregnancy);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return pregnancy.Id;
            }
        }
    }
}
