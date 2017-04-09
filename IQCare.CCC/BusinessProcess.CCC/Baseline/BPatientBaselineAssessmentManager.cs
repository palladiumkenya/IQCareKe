using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientBaselineAssessmentManager:ProcessBase,IPatientBaselineAssessmentManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        internal int Result;

        public int AddPatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {
            try
            {
                _unitOfWork.PatientBaselineAssessmentRepository.Add(patientBaselineAssessment);
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

        public int UpdatePatientBaselineAssessment(PatientBaselineAssessment patientBaselineAssessment)
        {

            try
            {
                var patientBaseline =
                    _unitOfWork.PatientBaselineAssessmentRepository.FindBy(
                        x => x.PatientId == patientBaselineAssessment.PatientId & !x.DeleteFlag).FirstOrDefault();
                if (patientBaseline != null)
                {
                    patientBaseline.Breastfeeding = patientBaselineAssessment.Breastfeeding;
                    patientBaseline.CD4Count = patientBaselineAssessment.CD4Count;
                    patientBaseline.HBVInfected = patientBaselineAssessment.HBVInfected;
                    patientBaseline.Height = patientBaselineAssessment.Height;
                    patientBaseline.MUAC = patientBaselineAssessment.MUAC;
                    patientBaseline.Pregnant = patientBaselineAssessment.Pregnant;
                    patientBaseline.TBInfected = patientBaselineAssessment.TBInfected;
                    patientBaseline.Weight = patientBaselineAssessment.Weight;
                    patientBaseline.WHOStage = patientBaselineAssessment.WHOStage;
                    _unitOfWork.PatientBaselineAssessmentRepository.Update(patientBaseline);
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

        public int DeletePatientBaselineAssessment(int id)
        {
            try
            {
                var patientArt = _unitOfWork.PatientBaselineAssessmentRepository.GetById(id);
                _unitOfWork.PatientBaselineAssessmentRepository.Remove(patientArt);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
    
        }

        public List<PatientBaselineAssessment> GetPatientBaselineAssessment(int patientId)
        {
            try
            {
                var patientBaseline =
                    _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
                        .Take(1)
                        .Distinct()
                        .OrderByDescending(x => x.Id)
                        .ToList();
                return patientBaseline;
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

        public int CheckIfPatientBaselineExists(int patientId)
        {
            try
            {
                var recordExists =
                    _unitOfWork.PatientBaselineAssessmentRepository.FindBy(x => x.PatientId == patientId)
                        .Select(x => x.Id)
                        .FirstOrDefault();
                return Convert.ToInt32(recordExists);
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
