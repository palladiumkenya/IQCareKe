using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Baseline;
using System;
using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientHivTestingManager : ProcessBase, IPatientHivTesting
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;
        public int AddPatientHivTesting(PatientHivTesting p)
        {
            _unitOfWork.PatientHivTestingRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientHivTesting GetPatientHivTesting(int id)
        {
            PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
            return hivTesting;
        }

        public void DeletePatientHivTesting(int id)
        {
            PatientHivTesting hivTesting = _unitOfWork.PatientHivTestingRepository.GetById(id);
            _unitOfWork.PatientHivTestingRepository.Remove(hivTesting);
            _unitOfWork.Complete();
        }

        public int UpdatePatientHivTesting(PatientHivTesting p)
        {
            _unitOfWork.PatientHivTestingRepository.Update(p);
            _result = _unitOfWork.Complete();
            return _result;
        }
    }
}