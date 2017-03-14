using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientHome : System.Web.UI.Page
    {
        public int patientId;
        public int PatientMasterVisitId;
        public decimal march_height;

        IPatientVitals _vitals =
            (IPatientVitals) ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientVitals, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            
            patientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);


            List<PatientVital> vitals = _vitals.GetCurrentPatientVital(patientId);
            if (vitals != null || vitals.Count > 0)
            {
                //vitals.FirstOrDefault(l => l.CreateDate.Month == )
                foreach (var item in vitals)
                {
                    march_height = item.CreateDate.Month;
                   // var height = item.Height;
                }

                //march_height;
                //feb_height;
            }
        }
    }
}