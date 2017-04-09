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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddpatientEncounter(PatientEncounter patientEncounter)
        {
            try
            {
                _unitOfWork.PatientEncounterRepository.Add(patientEncounter);
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

        public int UpdatePatientEncounter(PatientEncounter patientEncounter)
        {
            try
            {
                _unitOfWork.PatientEncounterRepository.Update(patientEncounter);
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

        public int DeletePatientEncounter(int id)
        {
            try
            {
                var personEncounter = _unitOfWork.PatientEncounterRepository.GetById(id);
                _unitOfWork.PatientEncounterRepository.Remove(personEncounter);
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

        public List<PatientEncounter> GetPatientCurrentEncounters(int patientId, DateTime visitDate)
        {
            try
            {
                List<PatientEncounter> patientEncounters =
                    _unitOfWork.PatientEncounterRepository.FindBy(
                            x =>
                                x.PatientId == patientId &
                                DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                                !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).Take(1).ToList();
                return patientEncounters;
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

        public List<PatientEncounter> GetPatientEncounterAll(int patientId)
        {
            try
            {
                List<PatientEncounter> patientEncounters =
                    _unitOfWork.PatientEncounterRepository.FindBy(
                            x =>
                                x.PatientId == patientId &
                                !x.DeleteFlag)
                        .OrderByDescending(x => x.Id).Take(1).ToList();
                return patientEncounters;
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
