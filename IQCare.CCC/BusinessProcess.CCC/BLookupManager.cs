using DataAccess.Base;
using Interface.CCC.Lookup;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using DataAccess.CCC.Repository.Lookup;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;
using Entities.CCC.Visit;
using System;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        //  private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());

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

        public int GetLookUpMasterId(string masterName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName)
                    .Select(x => x.MasterId)
                    .FirstOrDefault();
                unitOfWork.Dispose();

                return item;
            }
        }

        public string GetLookUpMasterNameFromId(int  masterId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupRepository.FindBy(x => x.MasterId == masterId)
                    .Select(x => x.MasterName)
                    .FirstOrDefault();
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

        /* pw GetLookupLabs implementation   */

        public List<LookupLabs> GetLookupLabs()

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupLabsRepository.GetLabs();
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetPreviousLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetExtruderCompleteLabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetExtruderCompleteLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetExtruderPendingLabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetExtruderPendingLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPendingLabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetPendingLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupVllabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetVlLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupPreviousLabsRepository.GetPendingVlLabs(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

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

        public List<LookupItemView> GetLookUpItemViewByMasterId(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupRepository.FindBy(x => x.MasterId == id).OrderBy(l => l.OrdRank).ToList();
                unitOfWork.Dispose();
                return item;
            }
        }

       

        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    unitOfWork.LookupRepository.FindBy(x => x.MasterName == groupName && x.ItemName == itemName)
                        .ToList();
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

        public LookupItemView GetPatientGender(int genderId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupRepository.GetPatientGender(genderId);
                unitOfWork.Dispose();
                return item;
            }
        }
        public PatientLookup GetPatientById(int patientId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.PatientLookupRepository.GetPatientById(patientId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupTestParameter> GetTestParameter(int labTestId)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupParameterRepository.GetTestParameter(labTestId);
                unitOfWork.Dispose();
                return item;
            }
        }

        public LookupLabs GetLabTestId(string labType)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = unitOfWork.LookupLabsRepository.GetLabTestId(labType);
                unitOfWork.Dispose();
                return item;
            }
        }

        public int GetRegimenCategory(int regimenId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                string masterName =
                    unitOfWork.LookupRepository.FindBy(x => x.ItemId == regimenId)
                        .Select(x => x.MasterName)
                        .FirstOrDefault();
                int regmineId = unitOfWork.LookupRepository.FindBy(x => x.ItemName == masterName)
                    .Select(x => x.ItemId)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return regmineId;
            }

        }

        public string GetRegimenCategoryByRegimenName(string regimenNaame)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                string masterName =
                    unitOfWork.LookupRepository.FindBy(x => x.ItemName == regimenNaame)
                        .Select(x => x.MasterId)
                        .FirstOrDefault().ToString();
                //int regmineId = unitOfWork.LookupRepository.FindBy(x => x.ItemName == masterName)
                //    .Select(x => x.ItemId)
                //    .FirstOrDefault();
                unitOfWork.Dispose();
                return masterName;
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

        public List<PatientLabTracker> GetVlPendingCount(int facilityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientLabTracker> facilityVlPending =
                    unitOfWork.PatientLabTrackerRepository.GetVlPendingCount(facilityId);
                unitOfWork.Dispose();
                return facilityVlPending;


            }
        }
        public List<LookupFacilityViralLoad> GetFacilityVLSuppressed(int facilityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<LookupFacilityViralLoad> facilityVl =
                    unitOfWork.LookupFacilityViralLoadRepository.GetFacilityVLSuppressed(facilityId);
                unitOfWork.Dispose();
                return facilityVl;


            }
        }
        public List<LookupFacilityViralLoad> GetFacilityVLUnSuppressed(int facilityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<LookupFacilityViralLoad> facilityVl =
                    unitOfWork.LookupFacilityViralLoadRepository.GetFacilityVLUnSuppressed(facilityId);
                   unitOfWork.Dispose();
                return facilityVl;


            }
        }
        public List<PatientLabTracker> GetVlCompleteCount(int facilityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))

            {
                List<PatientLabTracker> facilityVlComplete =
                    unitOfWork.PatientLabTrackerRepository.GetVlCompleteCount(facilityId);
                unitOfWork.Dispose();
                return facilityVlComplete;
            }


        }

        public PatientRegimenLookup GetCurentPatientRegimen(int patientId)
        {
            LookupPatientRegimenMapRepository lookupPatientRegimen = new LookupPatientRegimenMapRepository();
            return lookupPatientRegimen.GetPatientCurrentRegimen(patientId);
        }

        public List<PatientRegimenLookup> GetPatientRegimenList(int patientId)
        {
            LookupPatientRegimenMapRepository lookupPatientRegimen = new LookupPatientRegimenMapRepository();
            return lookupPatientRegimen.GetPatientRegimenList(patientId);
        }

        public LookupPatientAdherence GetPatientAdherence(int patientId)
        {
            PatientLookupAdhereenceRepository patientLookupAdhereence=new PatientLookupAdhereenceRepository();
            return patientLookupAdhereence.GetPatientAdherenceStatus(patientId);
        }

        public List<LookupFacilityStatistics>  GetLookupFacilityStatistics()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
               var facilityStats= unitOfWork.LookupFacilityStatisticsRepository.GetFacilityStatistics();
                unitOfWork.Dispose();
                return facilityStats;
            }
        }

        public string GetLookupMasterNameByMasterIdDisplayName(int itemId, string displayName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var masterName = unitOfWork.LookupRepository.FindBy(x => x.ItemId == itemId && x.DisplayName==displayName).FirstOrDefault()
                    .MasterName;
                unitOfWork.Dispose();
                return masterName;
            }
        }

        public List<LookupItemView> GetItemIdByGroupAndDisplayName(string groupName, string displayName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var items = unitOfWork.LookupRepository.FindBy(x => x.MasterName == groupName && x.DisplayName==displayName);
                unitOfWork.Dispose();
                return items;
            }
        }


        public List<LookupItemView> GetRegimenCategoryListByRegimenName(string regimenName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<LookupItemView> masterName =
                    unitOfWork.LookupRepository.FindBy(x => x.ItemName == regimenName).ToList();

                unitOfWork.Dispose();
                return masterName;
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


        //    public LookupPatientAdherence GetPatientAdherence(int patientId)
        //    {
        //        PatientLookupAdherenceRepository patientAdherence = new PatientLookupAdherenceRepository();



        //    }
        //}
    }
}
