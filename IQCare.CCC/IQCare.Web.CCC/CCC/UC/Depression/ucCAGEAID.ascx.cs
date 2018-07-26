using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Screening;
using Entities.CCC.Screening;
using System.Web.Script.Serialization;

namespace IQCare.Web.CCC.UC.Depression
{
    public partial class ucCAGEAID : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        public int screenTypeId = 0, recordId = 0;
        public RadioButtonList rbList;
        public int NotesId;
        public TextBox notesTb;
        public TextBox tbCageScore;
        public TextBox tbCageRisk;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                //Alcohol Frequency
                getFrequencyCTRLs();
                //Alcohol Screening
                getAlcoholCTRLs();
                //Smoking Screening
                getSmokingCTRLs();
                //cageaid Notes
                getNotesCTRLS();
                getCageaidData(PatientId);
            }
        }
        private void getFrequencyData(int patientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(patientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    RadioButtonList rbl = (RadioButtonList)PHCageFrequency.FindControl("cage"+value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }

        }
        private void getFrequencyCTRLs()
        {
            var SHL = new SocialHistoryLogic();
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("SocialHistoryQuestions");
            foreach (var value in questionsList)
            {
                screenTypeId = value.MasterId;
                PHCageFrequency.Controls.Add(new LiteralControl("<div class='col-md-4 text-left'>"));
                PHCageFrequency.Controls.Add(new LiteralControl("<label><span class='text-primary'>" + value.ItemDisplayName + "" + "</span></label>"));
                PHCageFrequency.Controls.Add(new LiteralControl("<div class=''>"));
                RadioButtonList rbList = new RadioButtonList();
                rbList.ID = "cage"+value.ItemId.ToString();
                rbList.RepeatColumns = 1;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                if(value.ItemDisplayName.Contains("smoke"))
                {
                    rbList.CssClass = "cagerbList smokerb";
                }
                else
                {
                    rbList.CssClass = "cagerbList alcoholrb";
                }
                lookUp.populateRBL(rbList, value.ItemName);
                PHCageFrequency.Controls.Add(rbList);
                PHCageFrequency.Controls.Add(new LiteralControl("</div>"));
                PHCageFrequency.Controls.Add(new LiteralControl("</div>"));
            }
        }
        private void getAlcoholCTRLs()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("AlcoholScreeningQuestions");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("<div class='row'>"));
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "cage"+value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "cagerbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                PHCAGEAlcoholScreening.Controls.Add(rbList);
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHCAGEAlcoholScreening.Controls.Add(new LiteralControl("<hr />"));
                }
            }

            //Score and Risks
            //CAGE AID SCORE
            PHCAGEAIDScore.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHCAGEAIDScore.Controls.Add(new LiteralControl("<span class='input-group-addon'>CAGE-AID SCORE</span>"));
            tbCageScore = new TextBox();
            tbCageScore.CssClass = "form-control input-sm";
            tbCageScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            tbCageScore.ID = "cage"+LookupLogic.GetLookupItemId("AlcoholScore");
            tbCageScore.Enabled = false;
            PHCAGEAIDScore.Controls.Add(tbCageScore);
            PHCAGEAIDScore.Controls.Add(new LiteralControl("<span class='input-group-addon'>/ 4</span>"));
            PHCAGEAIDScore.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHRisk.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHRisk.Controls.Add(new LiteralControl("<span class='input-group-addon'>Risk</span>"));
            tbCageRisk = new TextBox();
            tbCageRisk.CssClass = "form-control input-sm";
            tbCageRisk.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            tbCageRisk.ID = "cage"+LookupLogic.GetLookupItemId("AlcoholRiskLevel");
            tbCageRisk.Enabled = false;
            PHRisk.Controls.Add(tbCageRisk);
            PHRisk.Controls.Add(new LiteralControl("</div>"));
        }
        private void getNotesCTRLS()
        {
            var SHL = new SocialHistoryLogic();
            LookupLogic lookUp = new LookupLogic();
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("SocialHistory");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (value.ItemName == "SocialNotes")
                {
                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "cage"+value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHCageNotes.Controls.Add(notesTb);
                }
            }
        }
        private void getNotesData(int patientId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> socialNotesList = PCN.getPatientClinicalNotesById(PatientId, Convert.ToInt32(LookupLogic.GetLookupItemId("SocialNotes")));
            if (socialNotesList.Any())
            {
                foreach (var value in socialNotesList)
                {
                    TextBox ntb = (TextBox)PHCageNotes.FindControl("cage"+value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
        private void getSmokingCTRLs()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("SmokingScreeningQuestions");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHCageSmokingScreening.Controls.Add(new LiteralControl("<div class='row'>"));
                PHCageSmokingScreening.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                PHCageSmokingScreening.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHCageSmokingScreening.Controls.Add(new LiteralControl("</div>"));
                PHCageSmokingScreening.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "cage"+value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "cagerbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                PHCageSmokingScreening.Controls.Add(rbList);
                PHCageSmokingScreening.Controls.Add(new LiteralControl("</div>"));
                PHCageSmokingScreening.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHCageSmokingScreening.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        public void getCageaidData(int PatientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    screenTypeId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rblPC1Qs = (RadioButtonList)PHCageFrequency.FindControl("cage" + value.ScreeningCategoryId.ToString());
                    if (rblPC1Qs != null)
                    {
                        rblPC1Qs.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    TextBox ntb = (TextBox)PHCAGEAIDScore.FindControl("cage" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
    }
}