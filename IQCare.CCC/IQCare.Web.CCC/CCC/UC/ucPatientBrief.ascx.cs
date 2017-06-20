using Application.Common;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic;
using System;
using System.Web;
using Application.Presentation;
using Interface.CCC;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientBrief : System.Web.UI.UserControl
    {
        Utility _utility = new Utility();

        //readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected IPatientMaritalStatusManager PatientMaritalStatusManager = (IPatientMaritalStatusManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");
        protected void Page_Load(object sender, EventArgs e)
        {
            var myDate = DateTime.Now.Year;
            var myDateMonth = DateTime.Now.Month;

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            
            //if (Request.QueryString["patient"] != null)
            //{
            //    patientId = Convert.ToInt32(Request.QueryString["patient"]);
            //    Session["patientId"] = patientId;
            //}

            DateTime DoB;
            PatientLookupManager pMgr = new PatientLookupManager();
            //IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

           // List<PatientLookup> patientLookups = patientLookupmanager.GetPatientDetailsLookup(patientId);
            PatientLookup thisPatient = pMgr.GetPatientDetailSummary(patientId);
            pMgr = null;
            if (null != thisPatient)
            {
                DoB = Convert.ToDateTime(thisPatient.DateOfBirth);
                Session["PatientId"] = thisPatient.ptn_pk;
                Session["SystemId"] = 1;
                Session["DateOfBirth"] = thisPatient.DateOfBirth.ToString("dd-MMM-yyyy");
                //Don't decrypt at this level. the use Logic project for this

                //lblPatientNames.Text = _utility.Decrypt(thisPatient.LastName) + ", " + _utility.Decrypt(x.FirstName) + " " +
                //                       _utility.Decrypt(thisPatient.MiddleName)+" ";

                lblPatientNames.Text= (thisPatient.LastName) + ", " + (thisPatient.FirstName) + " " +  (thisPatient.MiddleName)+" ";

                //    lblLastName.Text = "<strong><i>" + _utility.Decrypt(x.LastName) + "</i></strong>";
                if (thisPatient.PatientStatus.Equals("Active"))
                {
                    lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-info fa-1x'>Patient Active</strong>";
                }
                else
                {
                    lblPatientStatus.Text = "<i class='fa fa-user-o text-danger' aria-hidden='true'></i><strong class='label label-danger fa-1x'>" + thisPatient.PatientStatus + "</strong>";
                }

                // string femaleIcon = "<i class='fa fa-female' aria-hidden='true'></i>";
                // string maleIcon = "<i class='fa fa-male' aria-hidden='true'></i>";

                //todo patientManagershould have the lookups resolved
                //if (x.Sex == 62)
                //{
                Session["Gender"] = lblGender.Text = LookupLogic.GetLookupNameById(thisPatient.Sex);
                //_lookupManager.GetLookupNameFromId(thisPatient.Sex);
                //    Session["Gender"] = _lookupManager.GetLookupNameFromId(x.Sex).ToLower();
                //}
                //if (x.Sex == 61)
                //{
                //    lblGender.Text =_lookupManager.GetLookupNameFromId(x.Sex);
                //    Session["Gender"] = _lookupManager.GetLookupNameFromId(x.Sex).ToLower();
                //}
                //todo patientManagershould have the lookups resolved
                lblPatientType.Text = LookupLogic.GetLookupNameById(thisPatient.PatientType).ToUpper();
                // _lookupManager.GetLookupNameFromId(thisPatient.PatientType).ToUpper();

                //lblDOB.Text = thisPatient.DateOfBirth.ToString("dd-MMM-yyyy");
                var ptnMaritalStatus = PatientMaritalStatusManager.GetCurrentPatientMaritalStatus(thisPatient.PersonId);
                if (ptnMaritalStatus != null)
                {
                    lblmstatus.Text =
                        LookupLogic.GetLookupNameById(ptnMaritalStatus.MaritalStatusId)
                            .ToString()
                            .ToUpper();
                }
                else
                {
                    lblmstatus.Text = "<span class='label label-danger'> N/A </span>";
                }
            

                //    lblOtherNames.Text = "<strong></i>" + _utility.Decrypt(x.FirstName) + ' ' + _utility.Decrypt(x.MiddleName) + "</i></strong>";

                var age = PatientManager.CalculateYourAge(DoB);

                //lblAge.Text ="<strong><i>"+ Convert.ToString(myDate - DoB.Year)+" Years " + Convert.ToString(myDateMonth-DoB.Month) + " Months </i></strong>";
                lblAge.Text = "<strong><i>" + age.Replace("Age:","") + "</i></strong>";
                Session["Age"] = Convert.ToString(myDate - DoB.Year);
                // lblCCCReg.Text = x.EnrollmentNumber;
                lblCCCRegNo.Text = " (" + thisPatient.EnrollmentNumber+") ";
                lblEnrollmentDate.Text = "" + thisPatient.EnrollmentDate.ToString("dd-MMM-yyyy");

            }

        }
    }
}