using System;
using System.Collections.Generic;
using System.Web;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientBrief : System.Web.UI.UserControl
    {
        Utility _utility = new Utility();

        readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        protected void Page_Load(object sender, EventArgs e)
        {
            var myDate = DateTime.Now.Year;
            var myDateMonth = DateTime.Now.Month;

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            if (Request.QueryString["patient"] != null)
            {
                patientId = Convert.ToInt32(Request.QueryString["patient"]);
                Session["patientId"] = patientId;
            }

            DateTime DoB;

            IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

            List<PatientLookup> patientLookups = patientLookupmanager.GetPatientDetailsLookup(patientId);
            foreach (var x in patientLookups)
            {
                DoB = Convert.ToDateTime(x.DateOfBirth);
                Session["DateOfBirth"] = x.DateOfBirth.ToString("dd-MMM-yyyy");

                lblPatientNames.Text = _utility.Decrypt(x.LastName) + ", " + _utility.Decrypt(x.FirstName) + " " +
                                       _utility.Decrypt(x.MiddleName)+" ";

                //    lblLastName.Text = "<strong><i>" + _utility.Decrypt(x.LastName) + "</i></strong>";
                if (x.Active)
                {
                    lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-info fa-1x'>Patient Active</strong>";
                }
                else
                {
                    lblPatientStatus.Text = "<i class=fa fa-user-o text-danger' aria-hidden='true'></i><strong> Inactive</strong>";
                }
                // string femaleIcon = "<i class='fa fa-female' aria-hidden='true'></i>";
                // string maleIcon = "<i class='fa fa-male' aria-hidden='true'></i>";

                if (x.Sex == 62)
                {
                    lblGender.Text = _lookupManager.GetLookupNameFromId(x.Sex);
                    Session["Gender"] = _lookupManager.GetLookupNameFromId(x.Sex).ToLower();
                }
                if (x.Sex == 61)
                {
                    lblGender.Text =_lookupManager.GetLookupNameFromId(x.Sex);
                    Session["Gender"] = _lookupManager.GetLookupNameFromId(x.Sex).ToLower();
                }

                lblDOB.Text = x.DateOfBirth.ToString("dd-MMM-yyyy");

                //    lblOtherNames.Text = "<strong></i>" + _utility.Decrypt(x.FirstName) + ' ' + _utility.Decrypt(x.MiddleName) + "</i></strong>";

                var age = PatientManager.CalculateYourAge(DoB);

                //lblAge.Text ="<strong><i>"+ Convert.ToString(myDate - DoB.Year)+" Years " + Convert.ToString(myDateMonth-DoB.Month) + " Months </i></strong>";
                lblAge.Text = "<strong><i>" + age.Replace("Age:","") + "</i></strong>";
                Session["Age"] = Convert.ToString(myDate - DoB.Year);
                // lblCCCReg.Text = x.EnrollmentNumber;
                lblCCCRegNo.Text = " (" + x.EnrollmentNumber+") ";
                lblEnrollmentDate.Text = "" + x.EnrollmentDate.ToString("dd-MMM-yyyy");

            }

        }
    }
}