using System;

namespace IQCare.Web.CCC.Encounter
{
    public partial class VitalSigns : System.Web.UI.Page
    {
        protected string Gender
        {
            get { return Convert.ToString(Session["Gender"]); }
        }
        protected int PatientAge
        {
            get { return Convert.ToInt32(Session["Age"]); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}