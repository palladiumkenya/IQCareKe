using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Interface.CCC.Lookup;
using Application.Presentation;
using Entities.CCC.Lookup;
using IQCare.Web.UILogic;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientFinder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //reset encounterstatus;
            Session["EncounterStatusId"] = 0;
            Session["PatientEditId"] = 0;

            //ILookupRepository l=new LookupRepository();
            //l.GetDropdownValue(Sex,"Gender");
            //ILookupManager mgr =
            //    (ILookupManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //List<LookupItemView> vw = mgr.GetGenderOptions();
            //if (vw != null && vw.Count > 0)
            //{
            //    Sex.Items.Add(new ListItem("select", "0"));
            //    foreach (var item in vw)
            //    {
            //        Sex.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
            //    }

            //    //
            //   // vw.ForEach(item=> { Sex.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString())); });
            //}

            PopulateFacilityList();
        }

        void PopulateFacilityList()
        {
            try
            {
                SystemSetting.CurrentSystem.Facilities.Where(f => !f.DeleteFlag);

                Facility.DataSource = SystemSetting.CurrentSystem.Facilities.OrderBy(f => f.Id);
                Facility.DataTextField = "Name";
                Facility.DataValueField = "Id";
                Facility.DataBind();
                Facility.Items.Insert(0, new ListItem("select", "0"));
              //  Facility.SelectedValue = Convert.ToString(Session["AppLocationId"]);
            }
            catch (Exception ex)
            {
               throw new Exception("",ex);
            }
        }
    }
}
