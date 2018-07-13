using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Screening;
using Entities.CCC.Screening;
using System.Web.Script.Serialization;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucDailyRoutine : System.Web.UI.UserControl
    {
        public int dailyRoutineId, PatientId, PatientMasterVisitId, userId, NotesId;
        public TextBox notesTb;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                populateQuestions();
                getDailyRoutine(PatientId, PatientMasterVisitId);
            }
        }
        protected void populateQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("DailyRoutine");
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
        protected void getDailyRoutine(int PatientId, int PatientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    dailyRoutineId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)QuestionsPlaceholder.FindControl(value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
    }
}