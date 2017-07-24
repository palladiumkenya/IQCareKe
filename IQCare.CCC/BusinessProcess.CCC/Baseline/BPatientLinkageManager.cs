using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientLinkageManager : ProcessBase, IPatientLinkageManager
    {
        public int AddPatientLinkage(PatientLinkage patientLinkage)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientLinkageRepository.Add(patientLinkage);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientLinkage.Id;
            }
        }

        public List<PatientLinkage> GetPatientLinkage(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var linkages = unitOfWork.PatientLinkageRepository.FindBy(x => x.PersonId == personId);
                unitOfWork.Dispose();
                return linkages.ToList();
            }
        }
    }
}
