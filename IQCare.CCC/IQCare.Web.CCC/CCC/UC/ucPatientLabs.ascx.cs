using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.Web.Laboratory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientLabs : System.Web.UI.UserControl
    {
        public int PatientId;
        public int VisitId;
        public int PatientMasterVisitId;
        public int UserId;
        public int Ptn_pk;
        public int patientId;
        public int AppLocationId;
        public int ModuleId;
        public int locationId;
        public string Msg { get; set; }
        public int Result { get; set; }

        private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            UserId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
            AppLocationId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);

            PatientLookup thisPatient = _lookupManager.GetPatientById(PatientId);
            if (thisPatient.ptn_pk != null)
            {
                Ptn_pk = thisPatient.ptn_pk.Value;
                List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
                list.Add(new KeyValuePair<string, object>("PatientID", Ptn_pk));
                list.Add(new KeyValuePair<string, object>("LocationID", AppLocationId));
                list.Add(new KeyValuePair<string, object>("FacilityID", thisPatient.FacilityId));
                list.Add(new KeyValuePair<string, object>("FirstName", thisPatient.FirstName));
                list.Add(new KeyValuePair<string, object>("MiddleName", thisPatient.MiddleName));
                list.Add(new KeyValuePair<string, object>("LastName", thisPatient.LastName));
                list.Add(new KeyValuePair<string, object>("DOB", thisPatient.DateOfBirth));
                list.Add(new KeyValuePair<string, object>("Gender", thisPatient.Sex));
                list.Add(new KeyValuePair<string, object>("RegistrationDate", thisPatient.RegistrationDate));
                base.Session[SessionKey.LabClient] = list;
            }


            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(orderReason, "LabOrderReason");

            }

        }
    }
}