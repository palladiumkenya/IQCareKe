using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;
using Interface.CCC.Visit;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

using DataAccess.Base;
using DataAccess.CCC.Interface;

namespace BusinessProcess.CCC.visit
{
    public class BPatientLabOrdermanager : ProcessBase, IPatientLabOrderManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientLabTracker(PatientLabTracker patientLabTracker)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientLabTrackerRepository.Add(patientLabTracker);
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }
        public int AddPatientLabOrder(LabOrderEntity labOrderEntity)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientLabOrderRepository.Add(labOrderEntity);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }           
        }

        public int UpdatePatientLabOrder(PatientLabTracker patientLabTracker)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientLabTrackerRepository.Update(patientLabTracker);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
           
        }

        public int DeletePatientLabOrder(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientLabOrder = _unitOfWork.PatientLabTrackerRepository.GetById(id);
                _unitOfWork.PatientLabTrackerRepository.Remove(patientLabOrder);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }

        }


        public List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientLabTracker> patientLabOrders =
                 _unitOfWork.PatientLabTrackerRepository.FindBy(
                         x =>
                             x.PatientId == patientId &
                             DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                             !x.DeleteFlag)
                     .OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
   
        }

        public List<PatientLabTracker> GetPatientLabOrdersAll(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientLabTracker> patientLabOrders =
    _unitOfWork.PatientLabTrackerRepository.FindBy(
            x =>
                x.PatientId == patientId &
                !x.DeleteFlag)
        .OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }

        }
        public List<LabResultsEntity> GetPatientVL(int labOrderId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var patientVL= _unitOfWork.PatientLabResultsRepository.FindBy(x => x.LabOrderId == labOrderId)
                .Where(x => x.LabTestId == 3)
                .OrderBy(x => x.Id)
                .ToList();

                _unitOfWork.Dispose();
                return patientVL;
            }
        }

        public LabOrderEntity GetPatientLabOrder(int ptnPk)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var labOrder= _unitOfWork.PatientLabOrderRepository.FindBy(x => x.Ptn_pk == ptnPk)
                        .Where(x => x.LabTestId == 3)
                        .FirstOrDefault();
                _unitOfWork.Dispose();
                return labOrder;
            }

        }

        public LabOrderEntity GetPatientCurrentviralLoadInfo(int ptnPk)
        {
 
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vlInfo = _unitOfWork.PatientLabOrderRepository.FindBy(x => x.Ptn_pk == ptnPk).OrderByDescending(x => x.Id).FirstOrDefault();
                _unitOfWork.Dispose();
                return vlInfo;
            }
        }

        public List<LabOrderEntity> GetVlPendingCount(int facilityId)
        {
            throw new NotImplementedException();
        }

        public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        {
            throw new NotImplementedException();
        }

        //public List<LabOrderEntity> GetVlPendingCount(int facilityId)
        //{
        //    using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
        //    {
        //        List<LabOrderEntity> facilityVlPending = _unitOfWork.PatientLabOrderRepository.GetVlPendingCount(facilityId);
        //        return facilityVlPending;
        //    }
        //}

        //public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        //{
        //    using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
        //    {

        //    }
        //}

    }
}

