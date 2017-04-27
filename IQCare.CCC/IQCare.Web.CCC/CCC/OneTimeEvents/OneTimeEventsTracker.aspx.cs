using System;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.OneTimeEvents
{
    public partial class OneTimeEventsTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int patientId = int.Parse(Session["patientId"].ToString());

                var patientLookUpManager = new PatientLookupManager();
                var patientlookup = patientLookUpManager.GetPatientDetailSummary(patientId);
                DateTime dob = patientlookup.DateOfBirth;
                int age = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;

                Age.Value = age.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}