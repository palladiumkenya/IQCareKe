using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using System.Linq;
using DataAccess.Base;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPatientCategorization : ProcessBase, IPatientCategorizationManager
    {
        private int _result;

        public int AddPatientCategorization(PatientCategorization p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientCategorizationRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientCategorization GetPatientCategorization(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var consent = unitOfWork.PatientCategorizationRepository.FindBy(x => x.PatientId == id)
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return consent;
            }
        }

        public void DeletePatientCategorization(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var categorization = unitOfWork.PatientCategorizationRepository.GetById(id);
                unitOfWork.PatientCategorizationRepository.Remove(categorization);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public int UpdatePatientCategorization(PatientCategorization p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var categorization = new PatientCategorization()
                {
                    Categorization = p.Categorization,
                    DateAssessed = p.DateAssessed
                };
                unitOfWork.PatientCategorizationRepository.Update(categorization);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }
    }
}