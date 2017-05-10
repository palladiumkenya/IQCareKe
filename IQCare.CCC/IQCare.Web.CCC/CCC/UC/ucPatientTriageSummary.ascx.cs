﻿using System;
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
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected string PatientGender
        {
            get { return Convert.ToString(Session["Gender"]); }
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
            lblAge.Text = Session["Age"].ToString() + "Years";
            if (patientTriage != null)
            {
                bmi = Convert.ToDouble(patientTriage.BMI);
                diastolic = Convert.ToDecimal(patientTriage.Bpdiastolic);
                systolic = Convert.ToDecimal(patientTriage.BpSystolic);

                bpDiastolic = Convert.ToInt32(diastolic);
                bpSystloic = Convert.ToInt32(systolic);




                if (patientTriage.Temperature > 0)
                {

                    if (patientTriage.Temperature > 40)
                    {
                        lblTemperature.Text = "<span class='label label-danger'>" +
                                              Convert.ToString(patientTriage.Temperature) +
                                              "°C | Possible Hyperpyrexia‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) >= 36.0 &
                             Convert.ToDouble(patientTriage.Temperature) < 37.5)
                    {
                        lblTemperature.Text = "<span class='label label-success'>" +
                                              Convert.ToString(patientTriage.Temperature) + "°C | Normal‎";

                    }
                    else if (Convert.ToDouble(patientTriage.Temperature) > 37.5 &
                             Convert.ToDouble(patientTriage.Temperature) < 40.0)
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
                    else if (patientTriage.Temperature > 32 & patientTriage.Temperature < 36)
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


                    if (diastolic <= 80 & systolic <= 120)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(diastolic) + "/" +
                                     Convert.ToString(systolic) + "mm[Hg] |Normal </span>";
                    }
                    else if (diastolic > 85 & systolic > 140)
                    {
                        bpAnalysis = "<span class='label label-success'>" + Convert.ToString(diastolic) + "/" +
                                     Convert.ToString(systolic) + "mm[Hg]  | Normal </span>";
                    }
                    else if (diastolic > 90 & systolic > 140)
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(diastolic) + "/" +
                                     Convert.ToString(systolic) + "mm[Hg]  | Border Line</span>";
                    }
                    else if (diastolic > 90 & systolic > 160)
                    {
                        bpAnalysis = "<span class='label label-danger'> " + Convert.ToString(diastolic) + "/" +
                                     Convert.ToString(systolic) + " |mm[Hg]  Suspect Hypertension</span>";
                    }
                    else
                    {
                        bpAnalysis = "<span class='label label-warning'> " + Convert.ToString(diastolic) + "/" +
                                     Convert.ToString(systolic) + " |mm[Hg]  </span>";
                    }

                    lblbloodpressure.Text = bpAnalysis;
                }

                if (bmi < 18.5)
                {
                    bmiAnalysis = "<span class='label label-danger'>" + Convert.ToString(patientTriage.BMI) + "Kg/M2" +
                                  " | Under weight</span>";
                }
                else if (bmi >= 18.5 & bmi < 25.0)
                {
                    bmiAnalysis = "<span class='label label-success'> " + Convert.ToString(patientTriage.BMI) + "Kg/M2" +
                                  " | Normal weight</span>";
                }
                else if (bmi >= 25 & bmi < 30.0)
                {
                    bmiAnalysis = "<span class='label label-warning'> " + Convert.ToString(patientTriage.BMI) + "Kg/M2" +
                                  " | Over weight<span>";
                }
                else
                {
                    bmiAnalysis = "<span class='label label-danger'> " + Convert.ToString(patientTriage.BMI) + "Kg/M2" +
                                  " | Obese<span>";
                }


                lblBMI.Text = bmiAnalysis;
                lblPulseRate.Text = "<span class='label label-info'>"+patientTriage.HeartRate+" beats/min</span>";
                lblOxygenSaturation.Text = "<span class='label label-info'>"+patientTriage.SpO2+" %</span>";
                lblRespiratoryRate.Text = "<span class='label label-info'>"+patientTriage.RespiratoryRate+" breaths/min</span>";
            }
            else
            {
                string notTaken = "<span class='label label-danger'>Not Taken!</span>";
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