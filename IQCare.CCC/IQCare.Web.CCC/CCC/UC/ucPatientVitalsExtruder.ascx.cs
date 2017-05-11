using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;
using Entities.CCC.Triage;
using IQCare.CCC.UILogic.Triage;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientVitalsExtruder : System.Web.UI.UserControl
    {
        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected string PatientGender
        {
            get { return Convert.ToString(Session["Gender"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var patientVitals = new PatientVitalsManager();
            var pregnancyStatus=new PatientPregnancyManager();

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

            string gender = PatientGender;
            if (gender == "Female")
            {
                int pgStatus = pregnancyStatus.CheckIfPatientPregnancyExisists(PatientId);
                var pregnancyList = pregnancyStatus.GetPatientPregnancy(PatientId);
                if (pgStatus > 0)
                {
                    lblPregnancyStatus.Text = "<span class='label label-info'> Pregnant </span> ";
                    if (pregnancyList!=null)
                    {
                        foreach (var item in pregnancyList)
                        {
                            lblLMP.Text = "<span class='label label-info'>LMP : " + item.LMP.ToString("dd-MMM-yyyy") + "</span>";
                           
                            lblEDD.Text = "<span class='label label-info'>EDD : " + Convert.ToDateTime(item.EDD).ToString("dd-MMM-yyyy") +"</span>";

                        }
                    }
                }
                else
                {
                    lblLMP.Text = "N/A";
                    lblEDD.Text = "N/A";
                    lblPregnancyStatus.Text = "Not Pregnannt";
                }
                
            }
            else
            {
                
            }
        }
    }
}