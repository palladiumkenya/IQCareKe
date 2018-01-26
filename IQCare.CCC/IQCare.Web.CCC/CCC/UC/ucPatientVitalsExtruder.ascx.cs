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

            double bmi = 0.0;
            string bmiZ;
            decimal diastolic = 0;
            decimal systolic = 0;
            string bmiAnalysis = "";
            string bpAnalysis = "";
            int bpDiastolic = 0;
            int bpSystloic = 0;

        PatientVital patientTriage = patientVitals.GetByPatientId(PatientId);
            if (patientTriage != null)
            {
                ////////////////////////////////////////////////
                bmi = Convert.ToDouble(patientTriage.BMI);
                bmiZ = Convert.ToString(patientTriage.BMIZ);
                diastolic = Convert.ToDecimal(patientTriage.Bpdiastolic);
                systolic = Convert.ToDecimal(patientTriage.BpSystolic);
                bpDiastolic = Convert.ToInt32(diastolic);
                bpSystloic = Convert.ToInt32(systolic);
                int age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
                /////////////////////////////////////////////////////

                lblVitalsDate.Text = "<span class='label label-primary'>Date Taken :" + Convert.ToDateTime(patientTriage.VisitDate).ToString("dd-MMM-yyyy")+"</span>";
                vitalHeight.Text = "<span class='label label-primary'>" + Convert.ToString(patientTriage.Height)+"</span>";
                vitalsWeight.Text = "<span class='label label-primary'>" + patientTriage.Weight.ToString() + "</span>";
                vitalsCircumference.Text = patientTriage.HeadCircumference.ToString();
                vitalsMUAC.Text = patientTriage.Muac.ToString();
                vitalBloodPressure.Text = patientTriage.BpSystolic.ToString() + '/' +
                                          patientTriage.Bpdiastolic.ToString();
                vitalTemperature.Text = patientTriage.Temperature.ToString();
                vitalRespiratoryRate.Text = patientTriage.RespiratoryRate.ToString();
                lblOxygenSaturation.Text = patientTriage.SpO2.ToString();

                ///////////////////

                if (patientTriage.Temperature > 0)
                {

                    if (patientTriage.Temperature > 40)
                    {
                        vitalTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) +
                                              "°C | Possible Hyperpyrexia‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) >= 36.0 &&
                             Convert.ToDouble(patientTriage.Temperature) <= 38)
                    {
                        vitalTemperature.Text = "<span class='label label-success'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Normal‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) > 38 &&
                             Convert.ToDouble(patientTriage.Temperature) <= 40.0)
                    {
                        vitalTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) +
                                              "°C | Possible Hyperpyrexia‎";
                    }
                    else if (patientTriage.Temperature < 32)
                    {
                        vitalTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Medical Emergency‎";

                    }
                    else if (patientTriage.Temperature >= 32 && patientTriage.Temperature < 36)
                    {
                        vitalTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Hypothermia ‎";

                    }
                }
                else
                {
                    vitalTemperature.Text = "<span class='label label-danger'>" +
                                          Convert.ToString(patientTriage.Temperature) + "NO Temperature Readings ‎";
                }


                if (diastolic < 1 & systolic < 1)
                {
                    vitalBloodPressure.Text = "<span class='label label-danger'> NOT TAKEN </span>";

                }
                else
                {

                    if (systolic < 120 & diastolic < 80)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg] | Normal </span>";
                    }
                    else if (systolic >= 120 | systolic < 129 & diastolic < 80)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg]  | Elavated </span>";
                    }
                    else if (systolic > 130 || systolic < 139 && diastolic > 80 || diastolic < 89)
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + "mm[Hg]  | HYPERTENSION STAGE 1</span>";
                    }
                    else if (systolic > 140 && diastolic > 90)
                    {
                        bpAnalysis = "<span class='label label-danger'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + " |mm[Hg] | HYPERTENSION STAGE 2</span>";
                    }
                    else if (systolic > 180 && diastolic > 120)
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(systolic) + "/" + Convert.ToString(diastolic) + " |mm[Hg] | HYPERTENSIVE CRISIS </span>";
                    }

                    vitalBloodPressure.Text = bpAnalysis;
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

                vitalsBMI.Text = bmiAnalysis;

                //if ((int)patientTriage.HeartRate == 0)
                //    lblPulseRate.Text = notTaken;
                //else
                //    lblPulseRate.Text = "<span class='label label-info'>" + patientTriage.HeartRate + " beats/min</span>";
                if ((int)patientTriage.SpO2 == 0)
                    lblOxygenSaturation.Text = "<span class='label label-danger'>Not Taken!</span>";
                else
                    lblOxygenSaturation.Text = "<span class='label label-info'>" + patientTriage.SpO2 + " %</span>";
                if ((int)patientTriage.RespiratoryRate == 0)
                    vitalRespiratoryRate.Text = "<span class='label label-danger'>Not Taken!</span>";
                else
                    vitalRespiratoryRate.Text = "<span class='label label-info'>" + patientTriage.RespiratoryRate + " breaths/min</span>";
                ///////////////////
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