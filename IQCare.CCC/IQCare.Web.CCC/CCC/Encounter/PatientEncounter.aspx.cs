using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.Web.Laboratory;
using System.Web.Services;
using System.Web.UI;


namespace IQCare.Web.CCC.Encounter
{
    public partial class PatientEncounter : System.Web.UI.Page
    {
        public int PatientId;       
        public int PatientMasterVisitId;
        public int UserId;     
        public int patientId;              
        public string Msg { get; set; }
        public int Result { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
          
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            UserId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);           

        }
    }

}