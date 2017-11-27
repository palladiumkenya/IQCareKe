using System;
using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientEntryPoint : ProcessBase, IPatientEntryPointManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
       // internal int Result;

        public int AddPatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientEntryPointRepository.Add(patientEntryPoint);
                _unitOfWork.Complete();
                var Id= patientEntryPoint.Id;
                _unitOfWork.Dispose();
                return Id;
            }
      
        }

        public int DeletePatientEntryPoint(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientEntryPointRepository.Update(patientEntryPoint);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientEntryPoint.Id;
            }
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var entryPointList= _unitOfWork.PatientEntryPointRepository.FindBy(x => x.PatientId == patientId && !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return entryPointList;
            }       
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId, int serviceAreaId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var entryPointList = _unitOfWork.PatientEntryPointRepository.FindBy(x => x.PatientId == patientId && !x.DeleteFlag && x.ServiceAreaId == serviceAreaId).ToList();
                _unitOfWork.Dispose();
                return entryPointList;
            }
        }
    }
}
