using IQCare.CCC.UILogic;
using System;

namespace IQCare.Web.CCC
{
    public partial class OneTimeEventsTracker : System.Web.UI.Page
    {
        private string Msg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    PatientDisclosureManager disclosure = new PatientDisclosureManager();

                    string Stage1DateValue = Stage1Date.Value;
                    string Stage2DateValue = Stage2Date.Value;
                    string Stage3DateValue = Stage3Date.Value;
                    string SexPartnerDateValue = SexPartnerDate.Value;
                    string INHStartDateValue = INHStartDate.Text;

                    int patientid = 15;
                    disclosure.AddPatientDisclosure(patientid, 10, "Stage1", "Stage1", DateTime.Parse(Stage1DateValue));
                }
                catch (Exception ex)
                {
                    Msg = ex.Message + ' ' + ex.InnerException;
                }
                //return Msg;
            }
        }
    }
}