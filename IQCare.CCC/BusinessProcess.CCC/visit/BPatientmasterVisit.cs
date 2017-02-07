using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using DataAccess.Base;

namespace BusinessProcess.CCC.visit
{
    public class BPatientmasterVisit : ProcessBase, IPatientMasterVisitManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientmasterVisit(PatientMasterVisit patientMasterVisit)
        {
            _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
            Result = _unitOfWork.Complete();
            return patientMasterVisit.Id;
        }

        public int DeletePatientVisit(int id)
        {
            var patientmasterVisit = _unitOfWork.PatientMasterVisitRepository.GetById(id);
            _unitOfWork.PatientMasterVisitRepository.Remove(patientmasterVisit);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate)
        {
            List<PatientMasterVisit> patientMasterVisitList =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) & x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
            return patientMasterVisitList;
        }

        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            List<PatientMasterVisit> patientVisitList =
                _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id).ToList();
            return patientVisitList;
        }

        public int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
            return Result = _unitOfWork.Complete();
        }
    }
}
