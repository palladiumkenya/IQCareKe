using System;
using System.Web;
using Entities.CCC.Triage;
using IQCare.CCC.UILogic;
using Entities.CCC.Appointment;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientTriageSummary : System.Web.UI.UserControl
    {
        protected int bpDiastolic = 0;
        protected int bpSystloic = 0;

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["patientId"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            double bmi = 0.0;
            decimal diastolic = 0;
            decimal systolic = 0;
            string bmiAnalysis = "";
            string bpAnalysis = "";

            var patientVitals = new PatientVitalsManager();
            PatientVital patientTriage = patientVitals.GetByPatientId(PatientId);
            if (patientTriage != null)
            {
                bmi =Convert.ToDouble(patientTriage.BMI);
                diastolic = Convert.ToDecimal(patientTriage.Bpdiastolic);
                systolic = Convert.ToDecimal(patientTriage.BpSystolic);

                bpDiastolic = Convert.ToInt32(diastolic);
                bpSystloic = Convert.ToInt32(systolic);


                lblAge.Text = Session["Age"].ToString() + "Years";
                lblWeight.Text = Convert.ToString(patientTriage.Weight) + "Kgs";
                if (diastolic < 1 & systolic < 1)
                {
                    lblbloodpressure.Text ="< span class='label label-danger'> NOT TAKEN </span>";

                }
                else
                {
                    
               
                    if (diastolic <= 80 & systolic <= 120)
                    {
                        bpAnalysis = "<span class='label label-success'> Normal </span>";
                    }
                    else if (diastolic>85 & systolic>140)
                    {
                        bpAnalysis = "<span class='label label-success'> Normal </span>";
                    }else if (diastolic > 90 & systolic > 140)
                    {
                        bpAnalysis = "<span class='label label-warning'>Border Line</span>";
                    }else if (diastolic > 90 & systolic > 160)
                    {
                        bpAnalysis = "<span class='label label-danger'>Suspect Hypertension</span>";
                    }

                    lblbloodpressure.Text = Convert.ToString(diastolic) + "/" + Convert.ToString(systolic)+ "mm[Hg] "+ bpAnalysis;
                }

                if (bmi < 18.5)
                {
                    bmiAnalysis = "<span class='label label-danger'> Under weight</span>";
                }
                else if (bmi >= 18.5 & bmi < 25.0)
                {
                    bmiAnalysis = "<span class='label label-success'> Normal weight</span>";
                }
                else if (bmi >= 25 & bmi < 30.0)
                {
                    bmiAnalysis = "<span class='label label-warning'> Over weight<span>";
                }
                else
                {
                    bmiAnalysis = "<span class='label label-danger'> Obese<span>";
                }


                lblBMI.Text = Convert.ToString(patientTriage.BMI) + "Kg/M2" + " " + bmiAnalysis;
            }

            var _patientAppointment = new PatientAppointmentManager();
            var PA = _patientAppointment.GetByPatientId(PatientId);
            if (PA!=null)
            {
                foreach (var item in PA)
                {
                    lblappointmentDate.Text =Convert.ToDateTime(item.AppointmentDate).ToString("DD-MMM-YYYY");
                    lblAppointmentReason.Text = LookupLogic.GetLookupNameById(item.ReasonId);
                    lblappointmentStatus.Text = LookupLogic.GetLookupNameById(item.StatusId);
                    lblcareStatus.Text = LookupLogic.GetLookupNameById(item.DifferentiatedCareId);
                }

            }



        }
    }
}