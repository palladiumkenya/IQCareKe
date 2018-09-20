using Interface.CCC.Lookup;
using IQCare.Web.UILogic;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientFinder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //reset encounterstatus;
            Session["EncounterStatusId"] = 0;
            Session["PatientEditId"] = 0;
            Session["PatientPK"] = 0;

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

        [System.Web.Services.WebMethod(EnableSession =true)]
        public static string SetSelectedPatient(int patientId,int personId)
        {
                      HttpContext.Current.Session["PatientPK"] = patientId;
            HttpContext.Current.Session["PersonId"] = personId;
            HttpContext.Current.Session["PatientInformation"] = null;
            return "success";
        }

            

        void PopulateFacilityList()
        {
            try
            {
                //SystemSetting.CurrentSystem.Facilities.Where(f => !f.DeleteFlag);

                Facility.DataSource = SystemSetting.CurrentSystem.Facilities.Where(g=> g.DeleteFlag==false).OrderBy(f => f.Id);
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
