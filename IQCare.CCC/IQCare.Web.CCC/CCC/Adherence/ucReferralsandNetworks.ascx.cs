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
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucReferralsandNetworks : System.Web.UI.UserControl
    {
        public int RefId, PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
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
                getReferrals(PatientId, PatientMasterVisitId);
            }
        }
        protected void populateQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("ReferralsNetworks");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
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
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
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
                    notesTb.ID = value.ItemId.ToString();
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
        protected void getReferrals(int patientId, int patientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    RefId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)QuestionsPlaceholder.FindControl(value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }

            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreeningByVisitId(patientId, patientMasterVisitId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    RefId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rbl = (RadioButtonList)QuestionsPlaceholder.FindControl(value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
        }
    }
}