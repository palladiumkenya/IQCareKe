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
            PatientVital patientTriage = patientVitals.GetByPatientId(PatientId);
            if (patientTriage != null)
            {

                lblVitalsDate.Text = "<span class='label label-primary'>Date Taken :" + patientTriage.CreateDate.ToString("dd-MMM-yyyy")+"</span>";
                vitalHeight.Text = Convert.ToString(patientTriage.Height);
                vitalsWeight.Text = patientTriage.Weight.ToString();
                vitalsCircumference.Text = patientTriage.HeadCircumference.ToString();
                vitalsMUAC.Text = patientTriage.Muac.ToString();
                vitalBloodPressure.Text = patientTriage.Bpdiastolic.ToString() + '/' +
                                          patientTriage.BpSystolic.ToString();
                vitalTemperature.Text = patientTriage.Temperature.ToString();
                vitalRespiratoryRate.Text = patientTriage.RespiratoryRate.ToString();
                lblOxygenSaturation.Text = patientTriage.SpO2.ToString();
            }
            else
            {
                lblVitalsDate.Text = "<span class='label label-danger'> VITAL SIGNS NOT TAKEN </span>";
            }
        }
    }
}