using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Records;
using DataAccess.Records;
using Entities.Records;
using DataAccess.Lookup;
using DataAccess.Records.Repository;

namespace BusinessProcess.Records
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        public string GetLookupNameFromId(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    unitOfWork.LookupRepository.FindBy(x => x.ItemId == id).Select(x => x.ItemName).FirstOrDefault();
                unitOfWork.Dispose();
                return item;
            }
        }

        public LookupFacility GetFacility()

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupFacilityRepository.GetFacility();
                unitOfWork.Dispose();
                return item;
            }
        }
        public List<LookupItemView> GetGenderOptions()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var gender = unitOfWork.LookupRepository.GetLookupItemViews("Gender");
                unitOfWork.Dispose();
                return gender;
            }
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupRepository.GetLookupItemViews(groupname);
                ;
                unitOfWork.Dispose();
                return item;
            }
        }
        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    unitOfWork.LookupRepository.FindBy(x => x.MasterName.ToLower() == groupName.ToLower() && x.ItemName.ToLower() == itemName.ToLower())
                        .ToList();
                unitOfWork.Dispose();
                return item;
            }
        }
        public string GetLookupItemId(string lookupItemName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    unitOfWork.LookupRepository.FindBy(x => x.ItemName == lookupItemName).Select(x => x.ItemId).FirstOrDefault().ToString();
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname, string anotherGroupname)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var items = unitOfWork.LookupRepository.FindBy(
                    y => y.MasterName == groupname || y.MasterName == anotherGroupname);
                unitOfWork.Dispose();
                return items;
            }
        }

        public List<LookupCounty> GetLookupCounties()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupCountyRepository.GetCounties();
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupItemView> GetLookUpItemViewByMasterName(string masterName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName)
                        .OrderBy(l => l.OrdRank)
                        .ToList();
                unitOfWork.Dispose();
                return item;

            }
        }

        
        public List<LookupCounty> GetLookupSubcounty(string county)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupCountyRepository.GetSubCounties(county);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupCounty> GetLookupWards(string subcounty)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupCountyRepository.GetWardsList(subcounty);
                unitOfWork.Dispose();
                return item;
            }
        }


        public string GetCountyByCountyId(int countyId)
        {
            LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            return lookupCountyRepository.GetCountyByCountyId(countyId);
        }

        public string GetCountyNameBySubCountyId(int subCountyId)
        {
            LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            return lookupCountyRepository.GetSubCountyNameBySubCountyId(subCountyId);
        }

        public string GetWardNameByWardId(int wardId)
        {
            LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            return lookupCountyRepository.GetWardNameByWardId(wardId);
        }

        public LookupCounty GetCountyDetailsByWardName(string wardName)
        {
            LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            return lookupCountyRepository.GetCountyDetailsByWardName(wardName);
        }

    }
}
