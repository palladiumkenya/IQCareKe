using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientArtDistribution : ProcessBase, IPatientArtDistributionManager
    {
        //internal int Result;
        public int AddPatientArtDistribution(PatientArtDistribution p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientArtDistributionRepository.Add(p);
                _unitOfWork.Complete();
                var Id = p.Id;
                _unitOfWork.Dispose();
                return Id;
            }
        }

        public PatientArtDistribution GetPatientArtDistribution(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePatientArtDistribution(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientArtDistribution(PatientArtDistribution p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientArtDistributionRepository.Update(p);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return p.Id;
            }
        }

        public List<PatientArtDistribution> GetByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public PatientArtDistribution GetPatientArtDistributionByPatientIdAndVisitId(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientArtDistributions = unitOfWork.PatientArtDistributionRepository.FindBy(x =>
                    x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientArtDistributions.FirstOrDefault();
            }
        }
    }
}