using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic.Adherence;
using Entities.CCC.Adherence;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucScreening : System.Web.UI.UserControl
    {
        public int ScreenId, PatientId, PatientMasterVisitId, userId;
        public RadioButtonList scrbList;
        public TextBox scdepressionTotalTb;
        public TextBox scdepressionSeverityTb;
        public TextBox scdepressionReccommendationTb;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                displayQuestions();
                getScreening(PatientId, PatientMasterVisitId);
            }
        }
        protected void displayQuestions()
        {
            //Depression Total
            PHDepressionTotal.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHDepressionTotal.Controls.Add(new LiteralControl("<span class='input-group-addon'>Total</span>"));
            scdepressionTotalTb = new TextBox();
            scdepressionTotalTb.CssClass = "form-control input-sm";
            scdepressionTotalTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            scdepressionTotalTb.ID = "sc"+ LookupLogic.GetLookupItemId("DepressionTotal");
            scdepressionTotalTb.Enabled = false;
            PHDepressionTotal.Controls.Add(scdepressionTotalTb);
            PHDepressionTotal.Controls.Add(new LiteralControl("<span class='input-group-addon'>/ 30</span>"));
            PHDepressionTotal.Controls.Add(new LiteralControl("</div>"));
            //Depression Severity
            PHDepressionSeverity.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHDepressionSeverity.Controls.Add(new LiteralControl("<span class='input-group-addon'>Depression Severity</span>"));
            scdepressionSeverityTb = new TextBox();
            scdepressionSeverityTb.CssClass = "form-control input-sm";
            scdepressionSeverityTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            scdepressionSeverityTb.ID = "sc" + LookupLogic.GetLookupItemId("DepressionSeverity");
            scdepressionSeverityTb.Enabled = false;
            PHDepressionSeverity.Controls.Add(scdepressionSeverityTb);
            PHDepressionSeverity.Controls.Add(new LiteralControl("</div>"));
            //Reccommended Management
            PHRecommendedManagement.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHRecommendedManagement.Controls.Add(new LiteralControl("<span class='input-group-addon'>Reccommended Management</span>"));
            scdepressionReccommendationTb = new TextBox();
            scdepressionReccommendationTb.CssClass = "form-control input-sm";
            scdepressionReccommendationTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            scdepressionReccommendationTb.ID = "sc" + LookupLogic.GetLookupItemId("ReccommendedManagement");
            scdepressionReccommendationTb.Enabled = false;
            PHRecommendedManagement.Controls.Add(scdepressionReccommendationTb);
            PHRecommendedManagement.Controls.Add(new LiteralControl("</div>"));
        }
        protected void getScreening(int patientId, int patientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    TextBox ntb = (TextBox)PHDepressionTotal.FindControl("sc" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
    }
}