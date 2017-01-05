
using System;
using IQCare.CCC.UILogic;
using System.Collections.Generic;
using Entities.CCC;

namespace IQCare.Web.CCC.Encounter
{
    public partial class TriageTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageVital mc = new ManageVital();
              List<CCCVital> vitals =   mc.GetVitals();
                grdSearchResult.DataSource = vitals;
                grdSearchResult.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ManageVital mc = new ManageVital();
            
   
            //get values from the data entry form
           mc.SaveVital(140.5M, 68.7M);
        }
    }
}