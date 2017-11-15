using Entities.CCC.Lookup;
using Interface.CCC;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Application.Presentation;
using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.CCC.UILogic
{
    public class LookupLogic
    {
        public static string GeSubCountyListJson(string county)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(county);

            if (lookupCounties != null && lookupCounties.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupCounties);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public static string GetLookupWardListJson(string subcounty)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
           List<LookupCounty> lookupwardsList = lookupManager.GetLookupWards(subcounty);

            if (lookupwardsList != null && lookupwardsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupwardsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

       
        public static string GetLookupLabsListJson()
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupLabs> lookuplabsList = lookupManager.GetLookupLabs();  

            if (lookuplabsList != null && lookuplabsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookuplabsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
      
        public static string GetLookupPreviousLabsListJson(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupPreviousLabs> lookupprevlabsList = lookupManager.GetLookupPreviousLabs(patientId);  
            if (lookupprevlabsList != null && lookupprevlabsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupprevlabsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
        public static string LookupExtruderCompleteLabs(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupPreviousLabs> lookupprevlabsList = lookupManager.GetExtruderCompleteLabs(patientId);
            if (lookupprevlabsList != null && lookupprevlabsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupprevlabsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
        public static string LookupExtruderPendingLabs(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupPreviousLabs> lookupprevlabsList = lookupManager.GetExtruderPendingLabs(patientId);
            if (lookupprevlabsList != null && lookupprevlabsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupprevlabsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
        public static string GetLookupPendingLabsListJson(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupPreviousLabs> lookuppendinglabsList = lookupManager.GetLookupPendingLabs(patientId);
            if (lookuppendinglabsList != null && lookuppendinglabsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookuppendinglabsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
        public static string GetvlTestsJson(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupPreviousLabs> lookupVllabs = lookupManager.GetLookupVllabs(patientId);
            if (lookupVllabs != null && lookupVllabs.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupVllabs);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
        public static string GetPendingvlTestsJson(int patientId)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupPreviousLabs> lookupPendingVllabs = lookupManager.GetLookupPendingVllabs(patientId);
            if (lookupPendingVllabs != null && lookupPendingVllabs.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupPendingVllabs);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public void populateDDL(DropDownList ddl, string groupName)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetLookItemByGroup(groupName);
            ddl.Items.Add(new ListItem("Select", "0"));
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    ddl.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                }
            }
        }

        public void populateDDL(DropDownList ddl, string groupName, string anotherGroupName)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetLookItemByGroup(groupName, anotherGroupName);
            ddl.Items.Add(new ListItem("Select", "0"));
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    ddl.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                }
            }
        }

        public void PopulateListBox(ListBox lb, string groupName)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetLookItemByGroup(groupName);
            //lb.Items.Add(new ListItem("Select", "0"));
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    lb.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                }
            }
        }

        public void populateCBL(CheckBoxList cbl, string groupName)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetLookItemByGroup(groupName);
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    cbl.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                }
            }
        }

        public void getPharmacyDrugFrequency(DropDownList ddl)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            List<DrugFrequency> drg = patientEncounter.getPharmacyDrugFrequency();
            ddl.Items.Add(new ListItem("Select", "0"));
            if (drg != null && drg.Count > 0)
            {
                foreach (var item in drg)
                {
                    ddl.Items.Add(new ListItem(item.frequency, item.id));
                }
            }
        }

        public static string GetLookupItemByName(string itemName)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager =
                (ILookupManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            List<LookupItemView> lookupItem = lookupManager.GetLookItemByGroup(itemName);
            if (lookupItem !=null && lookupItem.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupItem);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public static string GetLookUpItemViewByMasterName(string masterName)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            List<LookupItemView> lookupItem = lookupManager.GetLookUpItemViewByMasterName(masterName);
            if (lookupItem != null && lookupItem.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupItem);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public static string GetLookUpItemViewByMasterId(int id)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            List<LookupItemView> lookupItem = lookupManager.GetLookUpItemViewByMasterId(id);
            if (lookupItem != null && lookupItem.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupItem);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }



        public static int GetLookUpMasterId(string masterName)
        {
            int masterId;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            masterId = lookupManager.GetLookUpMasterId(masterName);

            return masterId;
        }

        public static string GetLookupItemId(string lookupItemName)
        {
            
            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            string masterId = lookupManager.GetLookupItemId(lookupItemName).ToString();

            return masterId;
        }

        public static string GetLookupMasterNameByMasterIdDisplayName(int itemId, string displayName)
        {
            string masterName = "";
            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            masterName = lookupManager.GetLookupMasterNameByMasterIdDisplayName(itemId, displayName);
            return masterName;

        }

        public List<LookupItemView> GetItemIdByGroupAndDisplayName(string groupName, string displayName)
        {
            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            var items = lookupManager.GetItemIdByGroupAndDisplayName(groupName, displayName);
            return items;
        }

        public string GetLookupItemNameById(int id)
        {
            string lookupName = null;
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                lookupName = lookupManager.GetLookupNameFromId(id);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return lookupName;
        }

        public static string GetLookupNameById(int id)
        {
            string lookupName = null;
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                lookupName = lookupManager.GetLookupNameFromId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return lookupName;
        }

        public static int GetRegimenCategory(int regimenId)
        {
            int id = 0;
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                id = lookupManager.GetRegimenCategory(regimenId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return id;
        }

        public string GetRegimenCategoryByRegimenName(string regimenName)
        {
            string masterName;
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                masterName = lookupManager.GetRegimenCategoryByRegimenName(regimenName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return masterName;
        }

        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                return lookupManager.GetItemIdByGroupAndItemName(groupName, itemName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public LookupFacility GetFacility()
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                return lookupManager.GetFacility();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetCountyByCountyId(int countyId)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                return lookupManager.GetCountyByCountyId(countyId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetCountyNameBySubCountyId(int subCountyId)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                return lookupManager.GetCountyNameBySubCountyId(subCountyId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetWardNameByWardId(int wardId)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                return lookupManager.GetWardNameByWardId(wardId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PatientLookup GetPatientById(int patientId)

        {
            ILookupManager lookuplist = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            return lookuplist.GetPatientById(patientId);
        }

        public static string GetPatientCurrentRegimen(int patientId)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            var regimen = lookupManager.GetCurentPatientRegimen(patientId);

            return jsonObject = (regimen != null) ? new JavaScriptSerializer().Serialize(regimen) : jsonObject = "[]";
        }

        public static string GetPatientRegimenList(int patientId)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            var regimen = lookupManager.GetPatientRegimenList(patientId);

            return jsonObject = (regimen != null) ? new JavaScriptSerializer().Serialize(regimen) : jsonObject = "[]";
        }

        public List<LookupItemView> GetRegimenCategoryListByRegimenName(string regimenName)
        {

            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
                var displayMasterName = lookupManager.GetRegimenCategoryListByRegimenName(regimenName);
                return displayMasterName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public List<LookupFacilityStatistics> GetLookupFacilityStatistics()
        {
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            var facilityStats = lookupManager.GetLookupFacilityStatistics();
            return facilityStats;
        }

        public static string GetLookUpMasterNameFromId(int masterId)
        {

            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            string masterName = lookupManager.GetLookUpMasterNameFromId(masterId);

            return masterName;
        }
    }
}
