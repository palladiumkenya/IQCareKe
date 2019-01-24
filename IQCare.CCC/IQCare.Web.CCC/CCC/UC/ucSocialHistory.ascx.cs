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


namespace IQCare.Web.CCC.UC
{
    public partial class ucSocialHistory : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        public int screenTypeId = 0, recordId = 0;
        public RadioButtonList rbList;
        public int NotesId;
        public TextBox notesTb;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                populateCtrls();
                //Populate Radio Button Lists
                getSocialHistory(PatientId);
                getScreeningYesNo(PatientId);
            }
        }

        private void getSocialHistory(int patientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(patientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    RadioButtonList rbl = (RadioButtonList)PlaceHolder1.FindControl(value.ScreeningCategoryId.ToString());
                    if(rbl!=null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
                
        }
        private void getScreeningYesNo(int PatientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    RadioButtonList rbl = (RadioButtonList)PHSocialHistory.FindControl(value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> socialNotesList = PCN.getPatientClinicalNotesById(PatientId, Convert.ToInt32(LookupLogic.GetLookupItemId("SocialNotes")));
            if (socialNotesList.Any())
            {
                foreach (var value in socialNotesList)
                {
                    TextBox ntb = (TextBox)PHSocialHistoryNotes.FindControl(value.NotesCategoryId.ToString());
                    if(ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }
        }

        private void populateCtrls()
        {
            var SHL = new SocialHistoryLogic();
            LookupLogic lookUp = new LookupLogic();
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("SocialHistory");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (value.ItemName == "SocialRecord")
                {
                    recordId = value.MasterId;
                    PHSocialHistory.Controls.Add(new LiteralControl("<label class='control-label  pull-left text-primary'>" + value.ItemDisplayName + "</label>"));
                    rbList = new RadioButtonList();
                    rbList.ID = value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "rbList";
                    lookUp.populateRBL(rbList, "GeneralYesNo");
                    PHSocialHistory.Controls.Add(rbList);
                    RadioButtonList rbl = (RadioButtonList)PHSocialHistory.FindControl(value.ItemId.ToString());
                    rbl.SelectedValue = LookupLogic.GetLookupItemId("No");
                }
                if (value.ItemName == "SocialNotes")
                {
                    PHSocialHistoryNotes.Controls.Add(new LiteralControl("<label class='control-label  pull-left text-primary'>" + value.ItemDisplayName + "</label>"));
                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ID = value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHSocialHistoryNotes.Controls.Add(notesTb);
                }
            }
            List<LookupItemView> questionsList = lookUp.getQuestions("SocialHistoryQuestions");
            foreach (var value in questionsList)
            {
                screenTypeId = value.MasterId;
                PlaceHolder1.Controls.Add(new LiteralControl("<div class='col-md-4 text-left'>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<label><span class='text-primary'>" + value.ItemDisplayName + "" + "</span></label>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<div class=''>"));
                RadioButtonList rbList = new RadioButtonList();
                rbList.ID = value.ItemId.ToString();
                rbList.RepeatColumns = 1;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "rbList";
                lookUp.populateRBL(rbList, value.ItemName);
                PlaceHolder1.Controls.Add(rbList);
                PlaceHolder1.Controls.Add(new LiteralControl("</div>"));
                PlaceHolder1.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }
}