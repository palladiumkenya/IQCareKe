using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientRegistration : System.Web.UI.Page
    {
        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        public int PersonId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PersonId"] = 0;

            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetGenderOptions();
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    Gender.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    GuardianGender.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    tsGender.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                }

                //
                // vw.ForEach(item=> { Sex.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString())); });
            }

            List<LookupItemView> ms = mgr.GetLookItemByGroup("MaritalStatus");
            if (ms != null && ms.Count > 0)
            {
                foreach (var k in ms)
                {
                    MaritalStatusId.Items.Add(new ListItem(k.ItemName,k.ItemId.ToString()));
                }
            }

            List<LookupItemView> lookItemByGroup = mgr.GetLookItemByGroup("YesNo");
            if (lookItemByGroup != null && lookItemByGroup.Count > 0)
            {
                foreach (var k in lookItemByGroup)
                {
                    Inschool.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    ChildOrphan.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
            }

            List<LookupCounty> ct = mgr.GetLookupCounties();
            
            if (ct != null && ct.Count > 0)
            {
                foreach (var item in ct)
                {
                    countyId.Items.Add(new ListItem(item.CountyName,item.CountyId.ToString()));
                }
            }

        }

        protected void countyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(countyId.SelectedItem.Text);
            //if (lookupCounties != null && lookupCounties.Count > 0)
            //{
            //    foreach (var items in lookupCounties)
            //    {
            //        SubcountyId.Items.Add(new ListItem(items.SubcountyName, items.SubcountyId.ToString()));
            //    }
            //}
        }

        protected void WardId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SubcountyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //List<LookupCounty> lookupCounties = lookupManager.GetLookupWards(SubcountyId.SelectedItem.Text);
            //if (lookupCounties != null && lookupCounties.Count > 0)
            //{
            //    foreach (var items in lookupCounties)
            //    {
            //        WardId.Items.Add(new ListItem(items.WardName, items.WardId.ToString()));
            //    }
            //}
        }
    }
}