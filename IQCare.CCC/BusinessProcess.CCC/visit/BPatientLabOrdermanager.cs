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
    public class BPatientLabOrdermanager : IPatientLabOrderManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientLabOrder(PatientLabTracker patientLabTracker)
        {
            _unitOfWork.PatientLabTrackerRepository.Add(patientLabTracker);
            return Result = _unitOfWork.Complete();
        }

        public int UpdatePatientLabOrder(PatientLabTracker patientLabTracker)
        {
            _unitOfWork.PatientLabTrackerRepository.Update(patientLabTracker);
            return Result = _unitOfWork.Complete();
        }

        public int DeletePatientLabOrder(int id)
        {
            var patientLabOrder = _unitOfWork.PatientLabTrackerRepository.GetById(id);
            _unitOfWork.PatientLabTrackerRepository.Remove(patientLabOrder);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate)
        {
            List<PatientLabTracker> patientLabOrders =
                _unitOfWork.PatientLabTrackerRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                            !x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
            return patientLabOrders;
        }

       public List<PatientLabTracker> GetPatientLabOrdersAll(int patientId)
        {
            List<PatientLabTracker> patientLabOrders =
                _unitOfWork.PatientLabTrackerRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            !x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
            return patientLabOrders;
        }
    }
}

