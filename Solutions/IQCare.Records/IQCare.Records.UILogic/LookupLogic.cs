using Application.Presentation;
using Entities.Records;
using Entities.Records.Lookup;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    public class LookupLogic
    {

        
        public List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
               return  lookupManager.GetItemIdByGroupAndItemName(groupName, itemName);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static string GetLookupNameById(int id)
        {
            string lookupName = null;
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
                lookupName = lookupManager.GetLookupNameFromId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return lookupName;
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
                return lookupManager.GetLookItemByGroup(groupname);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LookupFacility GetFacility()
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
                return lookupManager.GetFacility();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<LookupItemView> GetListGenderOptions()
        {
            try
            {
                 ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
                return lookupManager.GetGenderOptions();



            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public List<LookupCounty> GetLookupCounties()
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");

                List<LookupCounty> lookupCounties = lookupManager.GetLookupCounties();
                return lookupCounties;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<LookupCounty> GetSubCountyList(string county)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager, BusinessProcess.Records");
                List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(county);
                return lookupCounties;

            }

            catch(Exception e)
            {
                throw new Exception(e.Message);

            }

        }

        public List<LookupCounty> GetLookupWardList(string subcounty)
        {
            try {

                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager, BusinessProcess.Records");
                List<LookupCounty> lookupCounties = lookupManager.GetLookupWards(subcounty);
                return lookupCounties;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<LookupItemView> GetLookupItemByName(string itemName)
        {
            try
            {
                ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager, BusinessProcess.Records");
                List<LookupItemView> lookupItem = lookupManager.GetLookItemByGroup(itemName);
                return lookupItem;
            }

            catch (Exception e)

            {
                throw new Exception(e.Message);
            }
        }


        public static string GetLookupItemId(string lookupItemName)
        {

            ILookupManager lookupManager =
                (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
            string masterId = lookupManager.GetLookupItemId(lookupItemName).ToString();

            return masterId;
        }


        //public List<LookupItemView> GetItemIdByGroupAndDisplayName(string groupName, string displayName)
        //{
        //    ILookupManager lookupManager =
        //        (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
        //    var items = lookupManager.GetItemIdByGroupAndItemName
        //        .GetItemIdByGroupAndDisplayName(groupName, displayName);
        //    return items;
        //}

        //public string GetLookupItemNameById(int id)
        //{
        //    string lookupName = null;
        //    try
        //    {
        //        ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
        //        lookupName = lookupManager.G
        //            GetLookupNameFromId(id);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //    return lookupName;
        //}

        //public static string GetLookupNameById(int id)
        //{
        //    string lookupName = null;
        //    try
        //    {
        //        ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BLookupManager,BusinessProcess.Records");
        //        lookupName = lookupManager. GetLookupNameFromId(id);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //    return lookupName;
        //}



    }

}
