using Entities.CCC.Encounter;
using Entities.CCC.Lookup;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucEvaluation : System.Web.UI.UserControl
    {
        public int PCId, evaluationId, PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
        public TextBox notesTb;
        public RadioButtonList rbList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                populateQuestions();
                populateARTHistoryQuestions();
               getEvaluation(PatientId, PatientMasterVisitId);
            }

        }
        protected void populateQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("ClinicalEvaluation");// ("PsychosocialCircumstances");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                evaluationId = value.MasterId;
                string radioItems = "";
                int notesValue = 0;
                List<LookupItemView> itemList = lookUp.getQuestions(value.ItemName);
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        if (items.ItemName == "Notes")
                        {
                            notesValue = items.ItemId;
                        }
                        else
                        {
                            radioItems = items.ItemName;
                        }
                    }
                }
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = value.ItemId.ToString();
                    rbList.RepeatColumns = 4;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "rbList";
                    lookUp.populateRBL(rbList, radioItems);
                    QuestionsPlaceholder.Controls.Add(rbList);
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left notessection'>"));
                    }
                    else
                    {
                        QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "notes" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    QuestionsPlaceholder.Controls.Add(notesTb);
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateARTHistoryQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("OtherRelevantARTHistory");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='row'>"));
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "notes" + value.ItemId.ToString();
                notesTb.Rows = 3;
                ARTHistoryQuestionsPlaceholder.Controls.Add(notesTb);
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    ARTHistoryQuestionsPlaceholder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void getEvaluation(int PatientId, int PatientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    PCId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)QuestionsPlaceholder.FindControl("notes" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }

                }
            }

            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    PCId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rbl = (RadioButtonList)QuestionsPlaceholder.FindControl(value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }

            //var PCNs = new PatientClinicalNotesLogic();
            //List<PatientClinicalNotes> notesLists = PCNs.getPatientClinicalNotes(PatientId);
            //if (notesLists.Any())
            //{
            //    foreach (var value in notesList)
            //    {
            //        PCId = Convert.ToInt32(value.NotesCategoryId);
            //        TextBox ntbs = (TextBox)QuestionsPlaceholder.FindControl(value.NotesCategoryId.ToString());
            //        if (ntbs != null)
            //        {
            //            ntbs.Text = value.ClinicalNotes;
            //        }
            //    }
            //}


        }
    }
}