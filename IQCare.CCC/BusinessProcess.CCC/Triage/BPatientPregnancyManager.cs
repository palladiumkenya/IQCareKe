using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientPregnancyManager :IPatientPregnancyManager
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

        public int UpdatePatientPreganacy(PatientPreganancy u)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PG =
                   _unitOfWork.PatientPregnancyRepository.FindBy(
                           x => x.PatientId == u.PatientId & !x.DeleteFlag)
                       .FirstOrDefault();
                if (PG != null)
                {
                    PG.LMP = u.LMP;
                    PG.EDD = u.EDD;
                    PG.Gravidae = u.Gravidae;
                    PG.parity = u.parity;
                    PG.Outcome = u.Outcome;
                    PG.DateOfOutcome = u.DateOfOutcome;
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
                var PG =
                  _unitOfWork.PatientPregnancyRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag & x.Outcome==0)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                _unitOfWork.Dispose();
                return Convert.ToInt32(PG);
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
    }
}
