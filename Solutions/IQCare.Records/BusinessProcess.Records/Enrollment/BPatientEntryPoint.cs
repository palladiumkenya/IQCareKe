using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
 
    public class BPatientEntryPoint : ProcessBase, IPatientEntryPointManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext());
        // internal int Result;

        public int AddPatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                _unitOfWork.PatientEntryPointRepository.Add(patientEntryPoint);
                _unitOfWork.Complete();
                var Id = patientEntryPoint.Id;
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
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PatientEntryPointRepository.Update(patientEntryPoint);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientEntryPoint.Id;
            }
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var entryPointList = _unitOfWork.PatientEntryPointRepository.FindBy(x => x.PatientId == patientId && !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return entryPointList;
            }
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId, int serviceAreaId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var entryPointList = _unitOfWork.PatientEntryPointRepository.FindBy(x => x.PatientId == patientId && !x.DeleteFlag && x.ServiceAreaId == serviceAreaId).ToList();
                _unitOfWork.Dispose();
                return entryPointList;
            }
        }
    }
}
