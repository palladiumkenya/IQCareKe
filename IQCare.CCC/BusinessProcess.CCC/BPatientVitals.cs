using System;
using DataAccess.Base;
using Entities.CCC.Triage;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC
{
    public class BPatientVitals : ProcessBase, IPatientVitals
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;
        public int AddPatientVitals(PatientVital p)
        {
            try
            {
                _unitOfWork.PatientVitalsRepository.Add(p);
                _result = _unitOfWork.Complete();
                return _result;
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

        public PatientVital GetPatientVitals(int patientId)
        {
            try
            {
                var vitals =
                        _unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                            .OrderBy(x => x.Id)
                            .FirstOrDefault()
                    ;
                return vitals;
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

        public void DeletePatientVitals(int id)
        {
            try
            {
                PatientVital vital = _unitOfWork.PatientVitalsRepository.GetById(id);
                _unitOfWork.PatientVitalsRepository.Remove(vital);
                _unitOfWork.Complete();
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

        public int UpdatePatientVitals(PatientVital p)
        {
            try
            {
                var vitals = new PatientVital()
                {
                    Temperature = p.Temperature,
                    RespiratoryRate = p.RespiratoryRate,
                    HeartRate = p.HeartRate,
                    BpSystolic = p.BpSystolic,
                    Bpdiastolic = p.Bpdiastolic,
                    Height = p.Height,
                    Weight = p.Weight,
                    Muac = p.Muac,
                    SpO2 = p.SpO2

                };
                _unitOfWork.PatientVitalsRepository.Update(vitals);
                _result = _unitOfWork.Complete();
                return _result;
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

        public PatientVital GetByPatientId(int patientId)
        {
            try
            {
                PatientVital vital =
                    _unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefault();
                return vital;
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
            //PatientVital vital = _unitOfWork.PatientVitalsRepository.GetByPatientId(patientId);
      
        }

        public List<PatientVital> GetCurrentPatientVital(int patientId)
        {
            try
            {
                var vitals =
                        _unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                            .OrderBy(x => x.Id)
                            .ToList()
                    ;
                return vitals;
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