using Entities.CCC.pharmacy;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Interface.CCC.Pharmacy;

namespace BusinessProcess.CCC.Pharmacy
{
    public class BPatientPharmacyDispense : ProcessBase, IPatientPharmacyDispenseManager
    {
        private int _result;
        public int AddPatientPharmacyDispense(PatientPharmacyDispense p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPharmacyDispenseRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdatePatientPharmacyDispense(PatientPharmacyDispense p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPharmacyDispenseRepository.Update(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public void DeletePatientPharmacyDispense(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientPharmacyDispense pharmacyDispense = unitOfWork.PatientPharmacyDispenseRepository.GetById(id);
                unitOfWork.PatientPharmacyDispenseRepository.Remove(pharmacyDispense);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public PatientPharmacyDispense GetPatientPharmacyDispense(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pharmacyDispense = unitOfWork.PatientPharmacyDispenseRepository.FindBy(x => x.Id == id)
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return pharmacyDispense;
            }
        }

        public List<PatientPharmacyDispense> GetByPharmacyOrderId(int pharmacyOrderId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientPharmacyDispense> pharmacyDispenses = unitOfWork.PatientPharmacyDispenseRepository.GetByPharmacyOrder(pharmacyOrderId);
                unitOfWork.Dispose();
                return pharmacyDispenses;
            }
        }
    }
}