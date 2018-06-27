using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic;


namespace IQCare.Web.CCC.UC
{
    public partial class ucSocialHistory : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                populateCtrls();
                //Populate Radio Button Lists
                getSocialHistory(PatientId, PatientMasterVisitId);
            }
        }

        private void getSocialHistory(int patientId, int patientMasterVisitId)
        {
            var SHL = new SocialHistoryLogic();

            List<PatientSocialHistory> socialHistoryList = SHL.getSocialHistory(patientId, patientMasterVisitId);
            
            //rbDrinkAlcohol.SelectedValue = socialHistoryList;
            foreach (var value in socialHistoryList)
            {
                rbRecordSocialHistory.SelectedValue = value.RecordSocialHistory.ToString();
                rbDrinkAlcohol.SelectedValue = value.DrinkAlcoholId.ToString();
                rbSmoke.SelectedValue = value.SmokeId.ToString();
                rbUseDrugs.SelectedValue = value.UseDrugsId.ToString();
                tbSocialHistoryNotes.Text = value.SocialHistoryNotes.ToString();
                SocialHistoryId = value.Id;
            }
        }

        private void populateCtrls()
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateRBL(rbDrinkAlcohol, "DrinkAlcohol");
            lookUp.populateRBL(rbSmoke, "Smoke");
            lookUp.populateRBL(rbUseDrugs, "UseDrugs");
            lookUp.populateRBL(rbRecordSocialHistory, "GeneralYesNo");
        }
    }
}