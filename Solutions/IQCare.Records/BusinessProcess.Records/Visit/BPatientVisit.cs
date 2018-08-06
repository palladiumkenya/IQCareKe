using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Visit
{
    public class BPatientVisit:ProcessBase,IPatientVisitManager
    {

        internal int Result;
        public int AddPatientVisit(PatientVisit patientVisit)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                _unitOfWork.PatientVisitRepository.Add(patientVisit);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return patientVisit.Visit_Id;
            }

        }

    }
}
