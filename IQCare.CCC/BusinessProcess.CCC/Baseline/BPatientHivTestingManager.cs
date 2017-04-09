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
    public class BPatientHivTestingManager : ProcessBase, IPatientHivTestingManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientHivTesting(PatientHivTesting p)
        {
            try
            {
                _unitOfWork.PatientHivTestingRepository.Add(p);
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

        public PatientHivTesting GetPatientHivTesting(int id)
        {
            try
            {
                PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
                return hivTesting;
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

        public void DeletePatientHivTesting(int id)
        {
            try
            {
                PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
                _unitOfWork.PatientHivTestingRepository.Remove(hivTesting);
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

        public int UpdatePatientHivTesting(PatientHivTesting p)
        {
            try
            {
                _unitOfWork.PatientHivTestingRepository.Update(p);
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

        public List<PatientHivTesting> GetAll()
        {
            try
            {
                List<PatientHivTesting> hivTestings = _unitOfWork.PatientHivTestingRepository.GetAll().ToList();
                return hivTestings;
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