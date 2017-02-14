using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using System.Web;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientDetails : System.Web.UI.UserControl
    {
        Utility _utility = new Utility();

        readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            var myDate = DateTime.Now.Year;
            var myDateMonth= DateTime.Now.Month;

            DateTime DoB;

            IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

            List<PatientLookup> patientLookups = patientLookupmanager.GetPatientDetailsLookup(int.Parse(HttpContext.Current.Session["PatientId"].ToString()));

            foreach (var x in patientLookups)
            {
                DoB = Convert.ToDateTime(x.DateOfBirth);

                lblLastName.Text = _utility.Decrypt(x.FirstName);
                if (x.Active)
                {
                    lblStatus.Text = "<i class='fa fa-check - square - o text-success' aria-hidden='true'></i> Active";
                }
                else
                {
                    lblStatus.Text = "Inactive";
                }
                string femaleIcon= "<i class='fa fa-female' aria-hidden='true'></i>";
                string maleIcon = "<i class='fa fa-male' aria-hidden='true'></i>";

                if (x.Sex == 62)
                {
                    lblGender.Text= femaleIcon + _lookupManager.GetLookupNameFromId(x.Sex);
                }
                if (x.Sex==61)
                {
                    lblGender.Text = maleIcon + _lookupManager.GetLookupNameFromId(x.Sex);
                }
                

                lblOtherNames.Text = _utility.Decrypt(x.FirstName) + ' ' + _utility.Decrypt(x.MiddleName);

                lblAge.Text = Convert.ToString(myDate - DoB.Year)+" Years " + Convert.ToString(myDateMonth-DoB.Month) + "Months ";
                lblCCCReg.Text = x.EnrollmentNumber;
            }


        }
    }
}