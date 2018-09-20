

using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.pharmacy;
using Interface.CCC.Pharmacy;

namespace BusinessProcess.CCC.Pharmacy
{
    public class BDrug : ProcessBase, IDrugManager
    {
        private int _result;
        public int AddDrug(Drug p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.DrugRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdateDrug(Drug p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.DrugRepository.Update(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public void DeleteDrug(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                Drug drug = unitOfWork.DrugRepository.GetById(id);
                unitOfWork.DrugRepository.Remove(drug);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public Drug GetDrug(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var drug = unitOfWork.DrugRepository.FindBy(x => x.Drug_pk == id)
                    .OrderBy(x => x.Drug_pk)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return drug;
            }
        }

        public List<Drug> GetDrugsByName(string name)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var drugs = unitOfWork.DrugRepository.FindBy(x => x.DrugName == name)
                    .OrderBy(x => x.Drug_pk).ToList();
                unitOfWork.Dispose();
                return drugs;
            }
        }
    }
}