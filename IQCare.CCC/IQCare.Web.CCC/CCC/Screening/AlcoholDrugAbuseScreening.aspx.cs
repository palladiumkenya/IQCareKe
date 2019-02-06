using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Screening
{
    public partial class AlcoholDrugAbuseScreening : System.Web.UI.Page
    {
        public string visitdateval = "";
        public string LMPval = "";
        public string EDDval = "";
        public string nxtAppDateval = "";
        public int genderID;
        public string gender = "";
        public int PatientId;
        public int PatientMasterVisitId;
        public int age;
        public string Weight = "0";
        public string AppointmentId;
        public Boolean IsEditAppointment = false;
        public int IsEditAppointmentId = 0;
        public int PmVisitId;
        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            gender = HttpContext.Current.Session["Gender"].ToString();
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            PmVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());


            if (age >= 9 && age <= 19)
            {
                Control crafftControl = Page.LoadControl("~/CCC/UC/Depression/ucCRAFFT.ascx");
                PHAlcoholSection.Controls.Add(crafftControl);
            }
            if (age > 20)
            {
                Control cageControl = Page.LoadControl("~/CCC/UC/Depression/ucCAGEAID.ascx");
                PHAlcoholSection.Controls.Add(cageControl);
            }
            

        }
    }
}