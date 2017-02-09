using DataAccess.Base;
using Interface.CCC.Lookup;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using DataAccess.CCC.Repository.Lookup;
using System;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());

        public List<LookupItemView> GetGenderOptions()
        {
            LookupRepository repo = new LookupRepository();
           return repo.GetLookupItemViews("Gender");
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            LookupRepository repo = new LookupRepository();
            return repo.GetLookupItemViews(groupname);
        }

        public List<LookupCounty> GetLookupCounties()
        {
           LookupCountyRepository lookupCountyRepository =new  LookupCountyRepository();
            return lookupCountyRepository.GetCounties();
        }

        public List<LookupItemView> GetLookUpItemViewByMasterName(string masterName)
        {
            List<LookupItemView> person = _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName).OrderBy(l => l.OrdRank).ToList();
            return person;
        }

        public int GetLookUpMasterId(string masterName)
        {
            return _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName).FirstOrDefault().MasterId;
        }

        public List<LookupCounty> GetLookupSubcounty(string county)
        {
            LookupCountyRepository lookupCountyRepository=new LookupCountyRepository();
            return lookupCountyRepository.GetSubCounties(county);
        }

        public List<LookupCounty> GetLookupWards(string subcounty)
        {
            LookupCountyRepository lookupCountyRepository= new LookupCountyRepository();
            return lookupCountyRepository.GetWardsList(subcounty);
        }
        /* pw GetLookupLabs implementation   */
        public List<LookupLabs> GetLookupLabs()
              
        {
            LookupLabsRepository lookupLabRepository = new LookupLabsRepository();
            return lookupLabRepository.GetLabs();

            //LookupLabs declared in entities/lookup
            //GetLookupLabs declared in Interface/Ilookupmanager
            //LookupLabsRepository declared in Dataaccess/Repository/lookup
            //GetLabs() a method in Dataaccess/Repository/lookup ==> List<LookupLabs> GetLabs()


        }
        /* pw .GetLookupLabs implementation   */
        /* pw GetLookupLabs implementation   */
        public List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId)

        {
            LookupPreviousLabsRepository lookupLabprevRepository = new LookupPreviousLabsRepository();
            return lookupLabprevRepository.GetPreviousLabs(patientId);


        }

        public string GetLookupNameFromId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
