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
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientHivTesting(PatientHivTesting p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientHivTestingRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientHivTesting GetPatientHivTesting(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
                _unitOfWork.Dispose();
                return hivTesting;
            }
        }

        public void DeletePatientHivTesting(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
                _unitOfWork.PatientHivTestingRepository.Remove(hivTesting);
                _result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public int UpdatePatientHivTesting(PatientHivTesting p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientHivTestingRepository.Update(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientHivTesting> GetAll()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientHivTesting> hivTestings = _unitOfWork.PatientHivTestingRepository.GetAll().ToList();
                _unitOfWork.Dispose();
                return hivTestings;
            } 
        }
    }
}