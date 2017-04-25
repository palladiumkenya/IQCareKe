using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using DataAccess.Base;
namespace BusinessProcess.CCC.visit
{
    public class BPatientVisit : ProcessBase, IPatientVisitManager
    {
        internal int Result;
        public int AddPatientVisit(PatientVisit patientVisit)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientVisitRepository.Add(patientVisit);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return patientVisit.Visit_Id;
            }

        }
       
    }
}
