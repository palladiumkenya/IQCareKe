using IQCare.CCC.UILogic;
using System;

namespace IQCare.Web.CCC.Encounter
{
    public partial class EncounterHistory : System.Web.UI.Page
    {
        PatientEncounterLogic PEL = new PatientEncounterLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PEL.EncounterHistory(TreeViewEncounterHistory, Session["PatientPK"].ToString());
            }
        }
    }
}