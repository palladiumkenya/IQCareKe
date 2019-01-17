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
using Entities.CCC.Lookup;
using DataAccess.Entity;
using DataAccess.Common;
using System.Data;

namespace BusinessProcess.CCC.visit
{
    public class BPatientLabOrdermanager : ProcessBase, IPatientLabOrderManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
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
                return labOrderEntity.Id;
            }
        }
        public int AddLabOrderDetails(LabDetailsEntity labDetailsEntity)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientLabDetailsRepository.Add(labDetailsEntity);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return labDetailsEntity.Id;
            }
        }
        public int AddPatientLabResults(LabResultsEntity entity)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                // _unitOfWork.PatientLabResultsRepository.Remove(labResultsEntity);
                var request = _unitOfWork.PatientLabResultsRepository.FindBy(re =>
                             re.LabOrderTestId == entity.LabOrderTestId  && 
                             re.ParameterId == entity.ParameterId && 
                             re.LabOrderId ==entity.LabOrderId).DefaultIfEmpty(null).FirstOrDefault();
                if (request == null )
                {
                   // _unitOfWork.Context.Set<LabResultsEntity>().Attach(entity);
                    _unitOfWork.PatientLabResultsRepository.Add(entity);
                }
                else
                {
                    //_unitOfWork.PatientLabResultsRepository.ExecuteProcedure("")
                    request.ResultText = entity.ResultText;
                    request.ResultUnit = entity.ResultUnit;
                    request.ResultUnitId = entity.ResultUnitId;
                    request.ResultValue = entity.ResultValue;
                    request.Undetectable = entity.Undetectable;
                    request.StatusDate = DateTime.Now;
                    _unitOfWork.PatientLabResultsRepository.Update(request);
                }
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
        public List<PatientLabTracker> GetPatientVL(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var complete = "Complete";
                var patientVL = _unitOfWork.PatientLabTrackerRepository.FindBy(
                x =>
                  x.PatientId == patientId &
                  x.Results == complete &
                  x.LabTestId == 3)
                 .OrderBy(x => x.Id)
                 .ToList();

                _unitOfWork.Dispose();
                return patientVL;
            }
        }

        public List<PatientLabTracker> GetAllPatientVLs(int patientId)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientId.ToString());

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getAllViralLoads", ClsUtility.ObjectEnum.DataTable);

                List<PatientLabTracker> list = new List<PatientLabTracker>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    PatientLabTracker vl = new PatientLabTracker();
                    vl.ResultValues = Convert.ToDecimal(theDT.Rows[i]["resultvalue"]);
                    vl.CreateDate = Convert.ToDateTime(theDT.Rows[i]["resultdate"]);

                    list.Add(vl);
                }

                return list;
            }
        }

        public PatientLabTracker GetPatientLastVL(int patientId)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientId.ToString());

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getAllViralLoads", ClsUtility.ObjectEnum.DataTable);

                PatientLabTracker lastVL = new PatientLabTracker();

                if (theDT.Rows.Count > 0)
                {
                    lastVL.ResultValues = Convert.ToDecimal(theDT.Rows[0]["resultvalue"]);
                    lastVL.CreateDate = Convert.ToDateTime(theDT.Rows[0]["resultdate"]);
                    lastVL.Results = theDT.Rows[0]["orderstatus"].ToString();
                    lastVL.SampleDate = Convert.ToDateTime(theDT.Rows[0]["orderdate"]);
                    lastVL.LabTestId = Convert.ToInt32(theDT.Rows[0]["parameterid"]);
                }


                return lastVL;
            }

        }
        public List<PatientLabTracker> GetPatientVlById(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientVL = _unitOfWork.PatientLabTrackerRepository.FindBy(
                x =>
                  x.Id == Id &
                  x.LabTestId == 3)
                 .OrderBy(x => x.Id)
                 .ToList();

                _unitOfWork.Dispose();
                return patientVL;
            }
        }
        public PatientLabTracker GetPatientLabTestId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var labTestId = _unitOfWork.PatientLabTrackerRepository.FindBy(x => x.PatientId == patientId)
                         .Where(x => x.LabTestId == 3 &
                          x.ResultValues >= 0)
                         .OrderByDescending(x => x.Id)
                         .FirstOrDefault();
                _unitOfWork.Dispose();
                return labTestId;
            }

        }



        public PatientLabTracker GetPatientCurrentviralLoadInfo(int patientId)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vlInfo = _unitOfWork.PatientLabTrackerRepository.FindBy(x => x.PatientId == patientId)
                     .Where(x => x.LabTestId == 3)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                _unitOfWork.Dispose();
                return vlInfo;
            }
        }

        public List<LabOrderEntity> GetPatientLabOrdersByDate(int patientId, DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<LabOrderEntity> patientLabOrders =
                    _unitOfWork.PatientLabOrderRepository.FindBy(
                            x =>
                                x.Ptn_pk == patientId &
                                DbFunctions.TruncateTime(x.OrderDate) == DbFunctions.TruncateTime(visitDate) &
                                !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
        }
        public LabOrderEntity GetLabOrderById(int labOrderId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                LabOrderEntity patientLabOrders = _unitOfWork.PatientLabOrderRepository.GetById(labOrderId);
                //.FindBy(
                //            x =>                                x.Id == labOrderId)
                //        .OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
        }
        public List<LabDetailsEntity> GetPatientLabDetailsByLabOrderId(int labOrderId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<LabDetailsEntity> patientLabOrders =
                    _unitOfWork.PatientLabDetailsRepository.FindBy(x => x.LabOrderId == labOrderId && !x.DeleteFlag)
                        .ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
        }
        public List<LabDetailsEntity> GetPatientLabDetailsByDate(int labOrderId, DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<LabDetailsEntity> patientLabOrders =
                    _unitOfWork.PatientLabDetailsRepository.FindBy(
                            x =>
                                x.LabOrderId == labOrderId &
                                DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                                !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
        }

        public void EditPatientLabOrder(LabOrderEntity labOrder)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientLabOrderRepository.Update(labOrder);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public LabOrderEntity GetLabOrderEntityById(int labOrderId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var labOrderEntity = _unitOfWork.PatientLabOrderRepository.GetById(labOrderId);
                _unitOfWork.Dispose();

                return labOrderEntity;
            }
        }

        public List<PatientLabTracker> GetPatientLabTracker(int patientId, int patientMasterVisitId, int labOrderId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientLabTracker> patientLabOrders = _unitOfWork.PatientLabTrackerRepository.FindBy(x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.LabOrderId == labOrderId).ToList();
                _unitOfWork.Dispose();
                return patientLabOrders;
            }
        }
    }
}

