using DataAccess.Base;
using Interface.CCC.Lookup;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using DataAccess.CCC.Repository.Lookup;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;
using DataAccess.CCC.Repository.Encounter;
using Entities.CCC.Encounter;
using System;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
       
        public List<LookupItemView> GetGenderOptions()
        {
            return _unitOfWork.LookupRepository.GetLookupItemViews("Gender");
            //LookupRepository repo = new LookupRepository();
            //return repo.GetLookupItemViews("Gender");
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            return _unitOfWork.LookupRepository.GetLookupItemViews(groupname);
            //LookupRepository repo = new LookupRepository();
            //return repo.GetLookupItemViews(groupname);
        }

        public List<LookupCounty> GetLookupCounties()
        {
           return  _unitOfWork.LookupCountyRepository.GetCounties();
            //LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            //return lookupCountyRepository.GetCounties();
        }

        public List<LookupItemView> GetLookUpItemViewByMasterName(string masterName)
        {
            //List<LookupItemView> person = _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName).OrderBy(l => l.OrdRank).ToList();
            //return person;
            return _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName).OrderBy(l => l.OrdRank).ToList();
        }

        public int GetLookUpMasterId(string masterName)
        {
            return
                _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName)
                    .Select(x => x.MasterId)
                    .FirstOrDefault();
        }

        public List<LookupCounty> GetLookupSubcounty(string county)
        {
           return  _unitOfWork.LookupCountyRepository.GetSubCounties(county);
            //LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            //return lookupCountyRepository.GetSubCounties(county);
        }

        public List<LookupCounty> GetLookupWards(string subcounty)
        {
           return  _unitOfWork.LookupCountyRepository.GetWardsList(subcounty);
            //LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            //return lookupCountyRepository.GetWardsList(subcounty);
        }

        /* pw GetLookupLabs implementation   */
        public List<LookupLabs> GetLookupLabs()

        {
           return  _unitOfWork.LookupLabsRepository.GetLabs();
            //LookupLabsRepository lookupLabRepository = new LookupLabsRepository();
            //return lookupLabRepository.GetLabs();
        }

        public List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId)

        {
            return _unitOfWork.LookupPreviousLabsRepository.GetPreviousLabs(patientId);
            //LookupPreviousLabsRepository lookupLabprevRepository = new LookupPreviousLabsRepository();
            //return lookupLabprevRepository.GetPreviousLabs(patientId);


        }
        public List<LookupPreviousLabs> GetLookupPendingLabs(int patientId)

        {
           return  _unitOfWork.LookupPreviousLabsRepository.GetPendingLabs(patientId);
            //LookupPreviousLabsRepository lookupPendingLabsRepository = new LookupPreviousLabsRepository();
            //return lookupPendingLabsRepository.GetPendingLabs(patientId);


        }
        public List<LookupPreviousLabs> GetLookupVllabs(int patientId)

        {
          return  _unitOfWork.LookupPreviousLabsRepository.GetVlLabs(patientId);
            //LookupPreviousLabsRepository lookupVllabsRepository = new LookupPreviousLabsRepository();
            //return lookupVllabsRepository.GetVlLabs(patientId);
        }
        public List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId)

        {
            return _unitOfWork.LookupPreviousLabsRepository.GetPendingVlLabs(patientId);
            //LookupPreviousLabsRepository lookupPendingVllabsRepository = new LookupPreviousLabsRepository();
            //return lookupPendingVllabsRepository.GetPendingVlLabs(patientId);
        }

        public string GetLookupNameFromId(int id)
        {
            return _unitOfWork.LookupRepository.FindBy(x => x.ItemId == id).Select(x => x.ItemName).SingleOrDefault();
            //LookupRepository lookupRepository = new LookupRepository();
            //return  lookupRepository.FindBy(x => x.ItemId == id).Select(x => x.ItemName).SingleOrDefault();
            //return itemName.ToString();
        }

        public List<LookupItemView> GetLookUpItemViewByMasterId(int id)
        {
            return  _unitOfWork.LookupRepository.FindBy(x => x.MasterId == id).OrderBy(l => l.OrdRank).ToList();
           // return lookupItem;
        }

        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            return
                _unitOfWork.LookupRepository.FindBy(x => x.MasterName == groupName && x.ItemName == itemName).ToList();
        }
        public LookupFacility GetFacility()

        {
            return _unitOfWork.LookupFacilityRepository.GetFacility();
            //LookupFacilityRepository lookupFacilityRepository = new LookupFacilityRepository();
            //return lookupFacilityRepository.GetFacility();
        }
        public LookupItemView GetPatientGender(int genderId)

        {
            return _unitOfWork.LookupRepository.GetPatientGender(genderId);
            //LookupRepository lookupGender = new LookupRepository();
            //return lookupGender.GetPatientGender(genderId);
        }
        public PatientLookup GetPatientPtn_pk(int patientId)

        {
            return _unitOfWork.PatientLookupRepository.GetPatientPtn_pk(patientId);
            //PatientLookupRepository lookupPtn = new PatientLookupRepository();
            //return lookupPtn.GetPatientPtn_pk(patientId);
        }
        public LookupLabs GetLabTestId(string labType)

        {
           return _unitOfWork.LookupLabsRepository.GetLabTestId(labType);
            //LookupLabsRepository lookupTestId = new LookupLabsRepository();
            //return lookupTestId.GetLabTestId(labType);
        }

        public int GetRegimenCategory(int regimenId)
        {

            string masterName = _unitOfWork.LookupRepository.FindBy(x => x.ItemId == regimenId).Select(x => x.MasterName).FirstOrDefault();
            return
                _unitOfWork.LookupRepository.FindBy(x => x.ItemName == masterName)
                    .Select(x => x.ItemId)
                    .FirstOrDefault();
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
    }
}
