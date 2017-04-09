using Interface.CCC.Baseline;
using Entities.CCC.Baseline;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using System;
using DataAccess.Base;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientDisclosure : ProcessBase, IPatientDisclosureManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientDisclosure(PatientDisclosure patientDisclosure)
        {
            try
            {
                _unitOfWork.PatientDisclosureRepository.Add(patientDisclosure);
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

        public int DeletePatientDisclosure(int id)
        {
            try
            {
                var personEncounter = _unitOfWork.PatientDisclosureRepository.GetById(id);
                _unitOfWork.PatientDisclosureRepository.Remove(personEncounter);
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

        public int UpdatePatientDisclosure(PatientDisclosure patientDisclosure)
        {
            try
            {
                _unitOfWork.PatientDisclosureRepository.Update(patientDisclosure);
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

        public List<PatientDisclosure> GetPatientDisclosures(int patientId, string category, string disclosureStage)
        {
            try
            {
                return
                    _unitOfWork.PatientDisclosureRepository.FindBy(
                            x =>
                                x.PatientId == patientId && x.Category == category &&
                                x.DisclosureStage == disclosureStage)
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
    }
}
