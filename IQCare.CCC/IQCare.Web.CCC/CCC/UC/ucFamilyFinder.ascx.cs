using IQCare.Web.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucFamilyFinder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            PopulateFacilityList();
        }


        void PopulateFacilityList()
        {
            try
            {
                //SystemSetting.CurrentSystem.Facilities.Where(f => !f.DeleteFlag);

                Facility.DataSource = SystemSetting.CurrentSystem.Facilities.Where(g => g.DeleteFlag == false).OrderBy(f => f.Id);
                Facility.DataTextField = "Name";
                Facility.DataValueField = "Id";
                Facility.DataBind();
                Facility.Items.Insert(0, new ListItem("select", "0"));
                //  Facility.SelectedValue = Convert.ToString(Session["AppLocationId"]);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}