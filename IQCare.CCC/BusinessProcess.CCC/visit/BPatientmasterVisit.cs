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
            try
            {
                _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                Result = _unitOfWork.Complete();
                return patientMasterVisit.Id;
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

        public int DeletePatientVisit(int id)
        {
            try
            {
                var patientmasterVisit = _unitOfWork.PatientMasterVisitRepository.GetById(id);
                _unitOfWork.PatientMasterVisitRepository.Remove(patientmasterVisit);
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

        public List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate)
        {
            try
            {
                List<PatientMasterVisit> patientMasterVisitList =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(
                            x =>
                                x.PatientId == patientId &
                                DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                                x.DeleteFlag)
                        .OrderByDescending(x => x.Id).Take(1).ToList();
                return patientMasterVisitList;
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

        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            try
            {
                List<PatientMasterVisit> patientVisitList =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).ToList();
                return patientVisitList;
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

        public int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            try
            {
                _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
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

        public int PatientMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit)
        {
            try
            {
                PatientMasterVisitAutoClosure(patientId);

                var visitId =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                            //x.PatientId == patientId & DbFunctions.AddHours(x.Start,-24) <= DateTime.Now &
                                x.PatientId == patientId & DbFunctions.DiffHours(x.Start, DateTime.Now) <= 24 &
                                x.End == null & !x.Active & x.Status == 1).Select(x => x.Id).FirstOrDefault();
                if (visitId == 0)
                {
                    _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                    _unitOfWork.Complete();
                    visitId = patientMasterVisit.Id;
                }
                return visitId;
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

        public int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit)
        {
            try
            {
                var patientVisit =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(
                            x => x.PatientId == patientId & x.Status == 1 & !x.DeleteFlag & x.End == null & !x.Active)
                        .FirstOrDefault();
                if (patientVisit != null)
                {
                    patientVisit.Status = 2;
                    patientVisit.End = patientMasterVisit.End;
                    patientVisit.VisitDate = patientMasterVisit.VisitDate;
                    patientVisit.VisitScheduled = patientMasterVisit.VisitScheduled;
                    patientVisit.VisitType = patientMasterVisit.VisitType;
                    patientVisit.VisitBy = patientMasterVisit.VisitBy;
                    patientVisit.Patient = null;
                    //call the update function here....
                    _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                    Result = _unitOfWork.Complete();
                }
                return Result;
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

        public int PatientMasterVisitCheckout(int patientId, int masterVisitId,int visitSchedule, int visitBy,int visitType,DateTime visitDate)
        {
            try
            {
                var patientVisit = _unitOfWork.PatientMasterVisitRepository.GetById(masterVisitId);
                if (null != patientVisit)
                {
                    patientVisit.Status = 2;
                    patientVisit.End = DateTime.Now;
                    patientVisit.Active = true;
                    patientVisit.VisitDate = visitDate;
                    patientVisit.VisitScheduled = visitSchedule;
                    patientVisit.VisitBy = visitBy;
                    patientVisit.VisitType = visitType;

                    _unitOfWork.PatientMasterVisitRepository.Update(patientVisit);
                    Result = _unitOfWork.Complete();
                }
                return Result;
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

        public void PatientMasterVisitAutoClosure(int patientId)
        {


            try
            {
                List<PatientMasterVisit> patientMasterVisits =_unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & x.End == null & DbFunctions.DiffHours(x.Start, DateTime.Now) > 24).OrderBy(x => x.Id).ToList();

                if (patientMasterVisits.Count > 0)
                {
                    foreach (var item in patientMasterVisits)
                    {
                        item.Status = 3;
                        item.End = DateTime.Now;
                        item.Active = true;
                        item.VisitDate = null;
                        item.VisitScheduled = null;
                        item.VisitBy = null;
                        item.VisitType = null;

                        _unitOfWork.PatientMasterVisitRepository.Update(item);
                        Result = _unitOfWork.Complete();
                    }
                }
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

        public DateTime GetPatientLastVisitDate(int patientId)
        {
            try
            {
                return
                Convert.ToDateTime(
                    _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId)
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Start));
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

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            try
            {
                List<PatientMasterVisit> patientVisitList =_unitOfWork.PatientMasterVisitRepository.GetByDate(date);
                return patientVisitList;
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
