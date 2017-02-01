using IQCare.CCC.UILogic;
using System;

namespace IQCare.Web.CCC
{
    public partial class OneTimeEventsTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                PatientDisclosureManager disclosure = new PatientDisclosureManager();

                string Stage1DateValue = Stage1Date.Value;
                string Stage2DateValue = Stage2Date.Value;
                string Stage3DateValue = Stage3Date.Value;
                string SexPartnerDateValue = SexPartnerDate.Value;
                string INHStartDateValue = INHStartDate.Text;

                int patientid =  (Int32)Session["PatientID"];
                //disclosure.AddPatientDisclosure(patientid, 0, null, null, DateTime.Parse(Stage1DateValue));

            }
        }
    }
}