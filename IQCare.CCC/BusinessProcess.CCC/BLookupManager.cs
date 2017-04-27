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
using Entities.CCC.Visit;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        //  private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());

        public List<LookupItemView> GetGenderOptions()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var gender = _unitOfWork.LookupRepository.GetLookupItemViews("Gender");
                _unitOfWork.Dispose();
                return gender;
            }
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupRepository.GetLookupItemViews(groupname);
                ;
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupCounty> GetLookupCounties()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupCountyRepository.GetCounties();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupItemView> GetLookUpItemViewByMasterName(string masterName)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName)
                        .OrderBy(l => l.OrdRank)
                        .ToList();
                _unitOfWork.Dispose();
                return item;

            }
        }

        public int GetLookUpMasterId(string masterName)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupRepository.FindBy(x => x.MasterName == masterName)
                    .Select(x => x.MasterId)
                    .FirstOrDefault();
                _unitOfWork.Dispose();

                return item;
            }
        }

        public List<LookupCounty> GetLookupSubcounty(string county)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupCountyRepository.GetSubCounties(county);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupCounty> GetLookupWards(string subcounty)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupCountyRepository.GetWardsList(subcounty);
                _unitOfWork.Dispose();
                return item;
            }
        }

        /* pw GetLookupLabs implementation   */

        public List<LookupLabs> GetLookupLabs()

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupLabsRepository.GetLabs();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetPreviousLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetExtruderCompleteLabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetExtruderCompleteLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetExtruderPendingLabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetExtruderPendingLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPendingLabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetPendingLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupVllabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetVlLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupPreviousLabsRepository.GetPendingVlLabs(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public string GetLookupNameFromId(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    _unitOfWork.LookupRepository.FindBy(x => x.ItemId == id).Select(x => x.ItemName).FirstOrDefault();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupItemView> GetLookUpItemViewByMasterId(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupRepository.FindBy(x => x.MasterId == id).OrderBy(l => l.OrdRank).ToList();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item =
                    _unitOfWork.LookupRepository.FindBy(x => x.MasterName == groupName && x.ItemName == itemName)
                        .ToList();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public LookupFacility GetFacility()

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupFacilityRepository.GetFacility();
                _unitOfWork.Dispose();
                return item;
            }
        }

        public LookupItemView GetPatientGender(int genderId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupRepository.GetPatientGender(genderId);
                _unitOfWork.Dispose();
                return item;
            }
        }
        public PatientLookup GetPatientById(int patientId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.PatientLookupRepository.GetPatientById(patientId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public LookupTestParameter GetTestParameter(int LabTestId)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupParameterRepository.GetTestParameter(LabTestId);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public LookupLabs GetLabTestId(string labType)

        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var item = _unitOfWork.LookupLabsRepository.GetLabTestId(labType);
                _unitOfWork.Dispose();
                return item;
            }
        }

        public int GetRegimenCategory(int regimenId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                string masterName =
                    _unitOfWork.LookupRepository.FindBy(x => x.ItemId == regimenId)
                        .Select(x => x.MasterName)
                        .FirstOrDefault();
                int RegmineId = _unitOfWork.LookupRepository.FindBy(x => x.ItemName == masterName)
                    .Select(x => x.ItemId)
                    .FirstOrDefault();
                _unitOfWork.Dispose();
                return RegmineId;
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
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientLabTracker> facilityVlPending =
                    _unitOfWork.PatientLabTrackerRepository.GetVlPendingCount(facilityId);
                _unitOfWork.Dispose();
                return facilityVlPending;


            }
        }

        public List<PatientLabTracker> GetVlCompleteCount(int facilityId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))

            {
                List<PatientLabTracker> facilityVlComplete =
                    _unitOfWork.PatientLabTrackerRepository.GetVlCompleteCount(facilityId);
                _unitOfWork.Dispose();
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

        //    public LookupPatientAdherence GetPatientAdherence(int patientId)
        //    {
        //        PatientLookupAdherenceRepository patientAdherence = new PatientLookupAdherenceRepository();



        //    }
        //}
    }
}
