using System;
using System.Web;
using Entities.CCC.Triage;
using IQCare.CCC.UILogic;
using Entities.CCC.Appointment;
using Entities.CCC.Lookup;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientTriageSummary : System.Web.UI.UserControl
    {
        protected int bpDiastolic = 0;
        protected int bpSystloic = 0;

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
            double bmi = 0.0;
            string bmiZ;
            decimal diastolic = 0;
            decimal systolic = 0;
            string bmiAnalysis = "";
            string bpAnalysis = "";
            var patientVitals = new PatientVitalsManager();
            PatientVital patientTriage;

            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
                patientTriage = patientVitals.GetByPatientVisitId(Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"]));
            }
            else
            {
                Session["ExistingRecordPatientMasterVisitID"] = "0";
                patientTriage = patientVitals.GetByPatientId(PatientId);
            }

            PatientLookupManager pMgr = new PatientLookupManager();
            //PatientVital patientTriage = patientVitals.GetByPatientId(PatientId);
            PatientLookup thisPatient = pMgr.GetPatientDetailSummary(PatientId);
            int age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            DateTime DoB = Convert.ToDateTime(thisPatient.DateOfBirth);
            var patientAge = PatientManager.CalculateYourAge(DoB);
            lblAge.Text = "<strong><i>" + patientAge.Replace("Age:", "") + "</i></strong>";
            string notTaken = "<span class='label label-danger'>Not Taken!</span>";
            if (patientTriage != null)
            {
                lblDatetaken.Text = Convert.ToDateTime(patientTriage.VisitDate).ToString("dd-MMM-yyyy");
                bmi = Convert.ToDouble(patientTriage.BMI);
                bmiZ = Convert.ToString(patientTriage.BMIZ);
                diastolic = Convert.ToDecimal(patientTriage.Bpdiastolic);
                systolic = Convert.ToDecimal(patientTriage.BpSystolic);

                bpDiastolic = Convert.ToInt32(diastolic);
                bpSystloic = Convert.ToInt32(systolic);

                bgSystolicT.Text = bpSystloic.ToString() + " (Systolic)";
                pgDiastolicT.Text = bpDiastolic.ToString() + " (Diastolic)";

                if (patientTriage.Temperature > 0)
                {

                    if (patientTriage.Temperature > 40)
                    {
                        lblTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) +
                                              "°C | Possible Hyperpyrexia‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) >= 36.0 &&
                             Convert.ToDouble(patientTriage.Temperature) <= 38)
                    {
                        lblTemperature.Text = "<span class='label label-success'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Normal‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) > 38 &&
                             Convert.ToDouble(patientTriage.Temperature) <= 40.0)
                    {
                        lblTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) +
                                              "°C | Possible Hyperpyrexia‎";
                    }
                    else if (patientTriage.Temperature < 32)
                    {
                        lblTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Medical Emergency‎";

                    }
                    else if (patientTriage.Temperature >= 32 && patientTriage.Temperature < 36)
                    {
                        lblTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Hypothermia ‎";

                    }
                }
                else
                {
                    lblTemperature.Text = "<span class='label label-danger'>" +
                                          Convert.ToString(patientTriage.Temperature) + "NO Temperature Readings ‎";


                }



                if (diastolic < 1 & systolic < 1)
                {
                    lblbloodpressure.Text = "<span class='label label-danger'> NOT TAKEN </span>";

                }
                else
                {


                    if (systolic<120  & diastolic<80)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg] | Normal </span>";
                    }
                    else if (systolic >=120 | systolic<129 & diastolic <80)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg]  | Elavated </span>";
                    }
                    else if (systolic>130 ||systolic<139 && diastolic>80 || diastolic <89)
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg]  | HYPERTENSION STAGE 1</span>";
                    }
                    else if (systolic>140 && diastolic>90)
                    {
                        bpAnalysis = "<span class='label label-danger'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + " |mm[Hg] | HYPERTENSION STAGE 2</span>";
                    }
                    else if(systolic>180 && diastolic>120)
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + " |mm[Hg] | HYPERTENSIVE CRISIS </span>";
                    }

                    lblbloodpressure.Text = bpAnalysis;
                }

                if (age > 15 & patientTriage.Muac > 0)
                {
                    lblMuac.Text = Convert.ToString(patientTriage.Muac)+ "cms";
                    lblMuac.Text= "<span class='label label-warning'> " + Convert.ToString(patientTriage.Muac) +" | CMs "+ "</span>";
                }
                else
                {
                    lblMuac.Text = "<span class='label label-danger'>  0 | Cms </span>";
                }
                if (age > 15)
                {
                    if (bmi < 18.5)
                    {
                        bmiAnalysis = "<span class='label label-danger'>" + Convert.ToString(patientTriage.BMI) +
                                      "Kg/M2" +
                                      " | Under weight</span>";
                    }
                    else if (bmi >= 18.5 & bmi < 25.0)
                    {
                        bmiAnalysis = "<span class='label label-success'> " + Convert.ToString(patientTriage.BMI) +
                                      "Kg/M2" +
                                      " | Normal weight</span>";
                    }
                    else if (bmi >= 25 & bmi < 30.0)
                    {
                        bmiAnalysis = "<span class='label label-warning'> " + Convert.ToString(patientTriage.BMI) +
                                      "Kg/M2" +
                                      " | Over weight<span>";
                    }
                    else
                    {
                        bmiAnalysis = "<span class='label label-danger'> " + Convert.ToString(patientTriage.BMI) +
                                      "Kg/M2" +
                                      " | Obese<span>";
                    }
                }
                else
                {
                    bmiAnalysis = bmiZ;
                }
                


                lblBMI.Text = bmiAnalysis;
                if((int)patientTriage.HeartRate==0)
                    lblPulseRate.Text = notTaken;
                else
                    lblPulseRate.Text = "<span class='label label-info'>" + patientTriage.HeartRate + " beats/min</span>";
                if ((int)patientTriage.SpO2 == 0)
                    lblOxygenSaturation.Text = notTaken;
                else
                    lblOxygenSaturation.Text = "<span class='label label-info'>" + patientTriage.SpO2 + " %</span>";
                if ((int)patientTriage.RespiratoryRate == 0)
                     lblRespiratoryRate.Text = notTaken;
                else
                    lblRespiratoryRate.Text = "<span class='label label-info'>" + patientTriage.RespiratoryRate + " breaths/min</span>";

                lblNurseComments.Text = "<span class='label label-info'>" + patientTriage.NursesComments + "</span>";
            }
            else
            {
                lblTemperature.Text = notTaken;
                lblbloodpressure.Text = notTaken;
                lblBMI.Text = notTaken;
                lblPulseRate.Text = notTaken;
                lblOxygenSaturation.Text = notTaken;
                lblRespiratoryRate.Text = notTaken;

            }

            var patientAppointment = new PatientAppointmentManager();
            var pa = patientAppointment.GetByPatientId(PatientId);
            if (pa!=null)
            {
                foreach (var item in pa)
                {
                    lblappointmentDate.Text = "<span class='pull-right'>"+Convert.ToDateTime(item.AppointmentDate).ToString("dd-MMM-yyyy")+"</span>";
                    lblAppointmentReason.Text = "<span class='pull-right'>" + LookupLogic.GetLookupNameById(item.ReasonId) + "</span>";
                    lblappointmentStatus.Text = "<span class='pull-right'>" + LookupLogic.GetLookupNameById(item.StatusId) + "</span>";
                    lblcareStatus.Text = "<span class='pull-right'>" + LookupLogic.GetLookupNameById(item.DifferentiatedCareId) + "</span>";
                }

            }



        }
    }
}