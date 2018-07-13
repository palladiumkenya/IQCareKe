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
    public partial class ucCRAFFT : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        public int screenTypeId = 0, recordId = 0;
        public RadioButtonList rbList;
        public int NotesId;
        public TextBox notesTb;
        public TextBox tbCrafftScore;
        public TextBox tbCrafftRisk;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                //Alcohol Frequency
                getScreeningCTRLs();
                getScreeningData(PatientId);
                //Alcohol Screening
                getScoreCTRLs();
                getScoreData(PatientId);
                //crafft Notes
                getNotesCTRLs();
                getNotesData(PatientId);
            }
        }

        public void getScreeningCTRLs()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("CRAFFTScreeningQuestions");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("<div class='row'>"));
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("</div>"));
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "crafft" + value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "crafftrbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                PHCRAFFTFrequency.Controls.Add(rbList);
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("</div>"));
                PHCRAFFTFrequency.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHCRAFFTFrequency.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        public void getScreeningData(int PatientId)
        {

        }
        public void getScoreCTRLs()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("CRAFFTScoreQuestions");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<div class='row'>"));
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "cagesm" + value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "crafftrbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                PHCRAFFTAlcoholScreening.Controls.Add(rbList);
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<hr />"));
                }
            }
            //var SHL = new SocialHistoryLogic();
            //LookupLogic lookUp = new LookupLogic();
            //List<LookupItemView> questionsList = lookUp.getQuestions("CRAFFTScoreQuestions");
            //foreach (var value in questionsList)
            //{
            //    screenTypeId = value.MasterId;
            //    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<div class='col-md-4 text-left'>"));
            //    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<label><span class='text-primary'>" + value.ItemDisplayName + "" + "</span></label>"));
            //    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("<div class=''>"));
            //    RadioButtonList rbList = new RadioButtonList();
            //    rbList.ID = "crafft" + value.ItemId.ToString();
            //    rbList.RepeatColumns = 1;
            //    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            //    rbList.CssClass = "crafftrbList";
            //    lookUp.populateRBL(rbList, value.ItemName);
            //    PHCRAFFTAlcoholScreening.Controls.Add(rbList);
            //    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
            //    PHCRAFFTAlcoholScreening.Controls.Add(new LiteralControl("</div>"));
            //}
            PHCRAFFTScore.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHCRAFFTScore.Controls.Add(new LiteralControl("<span class='input-group-addon'>CRAFFT SCORE</span>"));
            tbCrafftScore = new TextBox();
            tbCrafftScore.CssClass = "form-control input-sm";
            tbCrafftScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            tbCrafftScore.ID = LookupLogic.GetLookupItemId("AlcoholScore");
            tbCrafftScore.Enabled = false;
            PHCRAFFTScore.Controls.Add(tbCrafftScore);
            PHCRAFFTScore.Controls.Add(new LiteralControl("<span class='input-group-addon'>/ 6</span>"));
            PHCRAFFTScore.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHCrafftRisk.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHCrafftRisk.Controls.Add(new LiteralControl("<span class='input-group-addon'>Risk</span>"));
            tbCrafftRisk = new TextBox();
            tbCrafftRisk.CssClass = "form-control input-sm";
            tbCrafftRisk.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            tbCrafftRisk.ID = LookupLogic.GetLookupItemId("AlcoholRiskLevel");
            tbCrafftRisk.Enabled = false;
            PHCrafftRisk.Controls.Add(tbCrafftRisk);
            PHCrafftRisk.Controls.Add(new LiteralControl("</div>"));
        }
        public void getScoreData(int PatientId)
        {

        }
        public void getNotesCTRLs()
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
                    notesTb.ID = "crafft" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHCRAFFTNotes.Controls.Add(notesTb);
                }
            }
        }
        public void getNotesData(int PatientId)
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> socialNotesList = PCN.getPatientClinicalNotesById(PatientId, Convert.ToInt32(LookupLogic.GetLookupItemId("SocialNotes")));
            if (socialNotesList.Any())
            {
                foreach (var value in socialNotesList)
                {
                    TextBox ntb = (TextBox)PHCRAFFTNotes.FindControl("crafft" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }
    }
}