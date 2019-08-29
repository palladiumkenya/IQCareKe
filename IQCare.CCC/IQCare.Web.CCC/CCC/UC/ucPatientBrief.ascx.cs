using Application.Common;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic;
using System;
using System.Web;
using Application.Presentation;
using Interface.CCC;
using System.Data;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientBrief : System.Web.UI.UserControl
    {
        Utility _utility = new Utility();
        public string PatientTrace;
        //readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected IPatientMaritalStatusManager PatientMaritalStatusManager = (IPatientMaritalStatusManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");
        protected void Page_Load(object sender, EventArgs e)
        {
            var myDate = DateTime.Now.Year;
            var myDateMonth = DateTime.Now.Month;
            string TBStatus="unknown";
            string NutritionStatus = "unknown";
            string categorization = "Not done";

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            int personId = Convert.ToInt32(HttpContext.Current.Session["personId"]);
            PatientTrace = HttpContext.Current.Session["PatientTrace"].ToString();

            DateTime DoB;
            PatientLookupManager pMgr = new PatientLookupManager();
            PatientLookup thisPatient = pMgr.GetPatientDetailSummaryBrief(patientId,personId);
            if (null != thisPatient)
            {
                DoB = Convert.ToDateTime(thisPatient.DateOfBirth);
                Session["PatientId"] = thisPatient.ptn_pk;
                Session["SystemId"] = 1;
                Session["DateOfBirth"] = thisPatient.DateOfBirth.ToString("dd-MMM-yyyy");
                Session["PersonId"] = thisPatient.PersonId;
                Session["PatientType"] = thisPatient.PatientType;
                Session["PatientStatus"] = thisPatient.PatientStatus;

                lblPatientNames.Text= (thisPatient.LastName) + ", " + (thisPatient.FirstName) + " " +  (thisPatient.MiddleName)+" ";
                if (thisPatient.PatientStatus.Equals("Active"))
                {
                    int ptn_pk = thisPatient.ptn_pk.HasValue ? thisPatient.ptn_pk.Value : 0;
                    DataTable patientStatuses = pMgr.GetPatientStatus(ptn_pk);
                    if (patientStatuses.Rows.Count == 0)
                    {
                        lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-danger fa-1x'>no ART pharmacy forms dispensed</strong>";
                    }
                    else
                    {
                        foreach (DataRow patientStatus in patientStatuses.Rows)
                        {
                            var expectedReturnDate = patientStatus.Field<DateTime>("ExpectedReturnDate");
                            var lostToFollowDate = patientStatus.Field<DateTime>("LostToFollowDate");

                            if (lostToFollowDate < DateTime.Now)
                            {
                                lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-danger fa-1x'>LTFU (>90days) since " + lostToFollowDate.ToString("dd-MMM-yyyy") + "</strong>";
                            }
                            else if (expectedReturnDate < DateTime.Now)
                            {
                                lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-danger fa-1x'>Defaulter (>0days) since - " + expectedReturnDate.ToString("dd-MMM-yyyy") + " </strong>";
                            }
                            else
                            {
                                lblPatientStatus.Text = "<i class=fa fa-user-o text-success' aria-hidden='true'></i><strong class='label label-info fa-1x'>Patient Active</strong>";
                            }
                        }
                    }
                }
                else
                {
                    lblPatientStatus.Text = "<i class='fa fa-user-o text-danger' aria-hidden='true'></i><strong class='label label-danger fa-1x'>" + thisPatient.PatientStatus + "</strong>";
                }
                Session["Gender"] = Session["PatientSex"] = lblGender.Text = LookupLogic.GetLookupNameById(thisPatient.Sex);
                //todo patientManagershould have the lookups resolved
                var patType = LookupLogic.GetLookupNameById(thisPatient.PatientType);
                if (patType != null)
                {
                    lblPatientType.Text = patType.ToUpper();
                }
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
                lblCCCRegNo.Text = thisPatient.EnrollmentNumber;
                lblEnrollmentDate.Text = "" + thisPatient.EnrollmentDate.ToString("dd-MMM-yyyy");
                Session["DateOfEnrollment"] = thisPatient.EnrollmentDate.ToString("dd-MMM-yyyy");

                //SET TB STATUS
                if (thisPatient.TBStatus<1)
                {
                    lbltbstatus.Text = "<span class='fa fa-info-circle text-danger'> " + TBStatus + "<span>";
                }
                else
                {
                    TBStatus= LookupLogic.GetLookupNameById(thisPatient.TBStatus).ToString().ToUpper();
                    switch(TBStatus)
                    {
                        case "TBRX":
                            lbltbstatus.Text = "<span class='label label-danger'>"+TBStatus+"</span>";
                            break;
                        case "INH":
                            lbltbstatus.Text = "<span class='label label-warning'>" + TBStatus + "</span>";
                            break;
                        case "PRTB":
                            lbltbstatus.Text = "<span class='label label-warning'>" + TBStatus + "</span>";
                            break;
                        default:
                            lbltbstatus.Text = "<span class='label label-success'>" + TBStatus + "</span>";
                            break;
                    }
                }

                // SET NUTRITION STATUS
                if (thisPatient.NutritionStatus < 1)
                {
                    lblnutritionstatus.Text = "<span class='fa fa-info-circle text-danger'> " + NutritionStatus+"<span>";
                }
                else
                {
                    string nutrition = LookupLogic.GetLookupNameById(thisPatient.NutritionStatus);
                    if (!string.IsNullOrWhiteSpace(nutrition))
                    {
                        NutritionStatus = nutrition.ToUpper();
                    }
                    
                    //lblnutritionstatus.Text = "<span class='label label-success'>" + NutritionStatus + "</span>";
                    switch (NutritionStatus)
                    {
                        case "OBESE":
                            lblnutritionstatus.Text = "<span class='label label-warning'>" + NutritionStatus + "</span>";
                            break;
                        case "MAM":
                            lblnutritionstatus.Text = "<span class='label label-warning'>" + NutritionStatus + "</span>";
                            break;
                        case "SAM":
                            lblnutritionstatus.Text = "<span class='label label-danger'>" + NutritionStatus + "</span>";
                            break;
                        default:
                            lblnutritionstatus.Text = "<span class='label label-success'>" + NutritionStatus + "</span>";
                            break;
                    }
                }

                // SET categorization:
                if ((DateTime.Now - thisPatient.EnrollmentDate).TotalDays < 365 )
                {
                    IPatientEncounter patientCat = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
                    DataTable theDT = patientCat.patientCategorizationAtEnrollment(HttpContext.Current.Session["PatientPK"].ToString());
                    if(theDT.Rows.Count > 0)
                        lblcategorization.Text = "<span class='label " + theDT.Rows[0][1].ToString() + "'>" + theDT.Rows[0][0].ToString() + "</span>";
                    else
                        lblcategorization.Text = "<span class='label label-danger'>Unstable</span>";
                }
                else
                {
                    if (thisPatient.categorization < 1)
                    {
                        lblcategorization.Text = "<span class='label label-danger'>Unstable</span>";
                    }
                    else
                    {
                        //categorization= LookupLogic.GetLookupNameById(thisPatient.categorization).ToString().ToUpper();
                        categorization = thisPatient.categorization.ToString();
                        switch (categorization)
                        {
                            case "1":
                                lblcategorization.Text = "<span class='label label-success'>Stable</span>";
                                break;
                            case "2":
                                lblcategorization.Text = "<span class='label label-danger'>Unstable</span>";
                                break;
                        }
                    }
                }

            }

        }
    }
}