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

namespace BusinessProcess.CCC.visit
{
    public class BPatientLabOrdermanager : ProcessBase, IPatientLabOrderManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientLabTracker(PatientLabTracker patientLabTracker)
        {
            try
            {
                _unitOfWork.PatientLabTrackerRepository.Add(patientLabTracker);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //finally
            //{
            //    _unitOfWork.Dispose();
            //}
        }
        public int AddPatientLabOrder(LabOrderEntity labOrderEntity)
        {
            try
            {
                _unitOfWork.PatientLabOrderRepository.Add(labOrderEntity);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
           
        }
        public int UpdatePatientLabOrder(PatientLabTracker patientLabTracker)
        {
            try
            {
                _unitOfWork.PatientLabTrackerRepository.Update(patientLabTracker);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
           
        }

        public int DeletePatientLabOrder(int id)
        {
            try
            {
                var patientLabOrder = _unitOfWork.PatientLabTrackerRepository.GetById(id);
                _unitOfWork.PatientLabTrackerRepository.Remove(patientLabOrder);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }


        public List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
   
        }

        public List<PatientLabTracker> GetPatientLabOrdersAll(int patientId)
        {
            try
            {
                List<PatientLabTracker> patientLabOrders =
                    _unitOfWork.PatientLabTrackerRepository.FindBy(
                            x =>
                                x.PatientId == patientId &
                                !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).Take(1).ToList();
                return patientLabOrders;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
        public List<LabResultsEntity> GetPatientVL(int labOrderId)
        {
            try
            {
                    return
         _unitOfWork.PatientLabResultsRepository.FindBy(x => x.LabOrderId == labOrderId)
             .Where(x => x.LabTestId == 3)
             .OrderBy(x => x.Id)
             .ToList();

     
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
    


        }

        public LabOrderEntity GetPatientLabOrder(int ptnPk)

        {
            try
            {
                return
                    _unitOfWork.PatientLabOrderRepository.FindBy(x => x.Ptn_pk == ptnPk)
                        .Where(x => x.LabTestId == 3)
                        .FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //finally
            //{
            //    _unitOfWork.Dispose();
            //}

        }
        public List<LabOrderEntity> GetVlPendingCount(int facilityId)
        {
            try
            {
                List<LabOrderEntity> facilityVlPending = _unitOfWork.PatientLabOrderRepository.GetVlPendingCount(facilityId);
                return facilityVlPending;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
        public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        {
            try
            {
                List<LabOrderEntity> facilityVlComplete = _unitOfWork.PatientLabOrderRepository.GetVlCompleteCount(facilityId);
                return facilityVlComplete;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
    }
}

