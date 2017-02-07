using System;
using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientEntryPoint : ProcessBase, IPatientEntryPointManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            _unitOfWork.PatientEntryPointRepository.Add(patientEntryPoint);
            Result = _unitOfWork.Complete();
            return patientEntryPoint.Id;
        }

        public int DeletePatientEntryPoint(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            throw new NotImplementedException();
        }
    }
}
