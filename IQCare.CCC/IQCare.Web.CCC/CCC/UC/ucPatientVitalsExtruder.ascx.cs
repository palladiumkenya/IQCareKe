using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;
using Entities.CCC.Triage;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientVitalsExtruder : System.Web.UI.UserControl
    {
        protected int PatientId
        {
            get { return Convert.ToInt32(Session["patientId"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            var patientVitals = new PatientVitalsManager();
            List<PatientVital> PatientTriage = new List<PatientVital> {patientVitals.GetPatientVitals(PatientId)};

            foreach (var item in PatientTriage)
            {
                lblVitalsDate.Text = "Vital Signs as at :" + item.CreateDate;
                vitalHeight.Text =Convert.ToString(item.Height);
                vitalsWeight.Text = item.Weight.ToString();
                vitalsCircumference.Text = item.HeadCircumference.ToString();
                vitalsMUAC.Text = item.Muac.ToString();
                vitalBloodPressure.Text = item.Bpdiastolic.ToString() + '/' + item.BpSystolic.ToString();
                vitalTemperature.Text = item.Temperature.ToString();
                vitalRespiratoryRate.Text = item.RespiratoryRate.ToString();
                vitalBloodPressure.Text = item.SpO2.ToString();

            }
        }
    }
}