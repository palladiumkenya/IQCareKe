using Entities.CCC.pharmacy;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Interface.CCC.Pharmacy;

namespace BusinessProcess.CCC.Pharmacy
{
    public class BPharmacyOrder : ProcessBase, IPharmacyOrderManager
    {
        private int _result;
        public int AddPharmacyOrder(PharmacyOrder p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PharmacyOrderRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdatePharmacyOrder(PharmacyOrder p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PharmacyOrderRepository.Update(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public void DeletePharmacyOrder(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PharmacyOrder pharmacyOrder = unitOfWork.PharmacyOrderRepository.GetById(id);
                unitOfWork.PharmacyOrderRepository.Remove(pharmacyOrder);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public PharmacyOrder GetPharmacyOrder(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pharmacyOrder = unitOfWork.PharmacyOrderRepository.FindBy(x => x.ptn_pharmacy_pk == id)
                    .OrderBy(x => x.ptn_pharmacy_pk)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return pharmacyOrder;
            }
        }

        public List<PharmacyOrder> GetByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PharmacyOrder> pharmacyOrders = unitOfWork.PharmacyOrderRepository.GetByPatientId(patientId);
                unitOfWork.Dispose();
                return pharmacyOrders;
            }
        }
    }
}