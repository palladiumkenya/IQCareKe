using Entities.CCC.Encounter;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucCaseSummary : System.Web.UI.UserControl
    {

        public int caseSummaryId, PatientId, PatientMasterVisitId, userId, NotesId;
        public TextBox notesTb;
        protected void Page_Load(object sender, EventArgs e)
        {

            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                populateQuestions();
                getCaseSummaries(PatientId, PatientMasterVisitId);
               // getVitals(PatientId);
            }

        }
        protected void populateQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("CaseSummary");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='row'>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = value.ItemId.ToString();
                notesTb.Rows = 3;
                QuestionsPlaceholder.Controls.Add(notesTb);
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void getCaseSummaries(int PatientId, int PatientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    caseSummaryId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)QuestionsPlaceholder.FindControl(value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
        protected void getVitals(int PatientId)
        {
            var PVM = new PatientVitalsManager();
            var Vitals = PVM.GetByPatientId(PatientId);
        }
    }
}