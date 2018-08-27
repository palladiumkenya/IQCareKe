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
using Entities.CCC.Appointment;
using System.Web.Script.Serialization;
using System.IO;
using System.Diagnostics;

namespace IQCare.Web.CCC.UC.EnhanceAdherenceCounselling
{
    public partial class session1 : System.Web.UI.Page
    {
        public int PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
        public TextBox pillAdherenceTb;
        public TextBox dateFilledTb;
        public TextBox mmas4TbScore;
        public TextBox mmas4TbRating;
        public TextBox mmas8TbScore;
        public TextBox mmas8TbRating;
        public TextBox mmas8TbRecommendation;
        public RadioButtonList rbList;
        public TextBox notesTb;
        public string serviceAreaId;
        public string reasonId;
        public string differentiatedCareId;
        public string followupStatusId;
        public int appointmentId;
        public TextBox appointmentDateTb;
        public PatientClinicalNotes[] notesList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            serviceAreaId = LookupLogic.GetLookupItemId("MoH 257 GREENCARD");
            reasonId = LookupLogic.GetLookupItemId("Follow Up");
            differentiatedCareId = LookupLogic.GetLookupItemId("Standard Care");
            followupStatusId = LookupLogic.GetLookupItemId("Pending");

            if (!IsPostBack)
            {
                getAdherenceCtrls();
                populateMMAS();
                getScoreCtrls();
                //populateSocioEconomicBarriers();
                //populateReferrals();
                //notes / Radio buttons
                populateNotesRadio("SocioEconomicBarriers", PHSocioEconomicBarriers);
                populateNotesRadio("SessionReferralsNetworks", PHReferralsandNetworks);
                //notes ctrls
                populateNotesCtrls("UnderstandingViralLoad", PHUnderstandingViralLoad);
                populateNotesCtrls("CognitiveBarriers", PHCognitiveBarriers);
                populateNotesCtrls("BehaviouralBarriers", PHBahaviouralBarriers);
                populateNotesCtrls("EmotionalBarriers", PHEmotionalBarriers);
                //getSessionData(PatientId, PatientMasterVisitId);
                notesList = (PatientClinicalNotes[])Session["PatientNotesData"];
            }
        }
        protected void populateNotesCtrls(string screeningType, PlaceHolder typePlaceholder)
        {
            LookupLogic lookUp = new LookupLogic();
            //LookupItemView[] questionsList = lookUp.getQuestions("UnderstandingViralLoad").ToArray();
            if (Cache[screeningType] == null)
            {

                List<LookupItemView> questionsCacheList = lookUp.getQuestions(screeningType);
                HttpRuntime.Cache.Insert(screeningType, questionsCacheList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            int i = 0;
            List<LookupItemView> questionsList = (List<LookupItemView>)Cache[screeningType];
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                typePlaceholder.Controls.Add(new LiteralControl("<div class='row'>"));
                typePlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                typePlaceholder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                typePlaceholder.Controls.Add(new LiteralControl("</div>"));
                typePlaceholder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "session1tb" + value.ItemId.ToString();
                notesTb.Rows = 3;
                typePlaceholder.Controls.Add(notesTb);
                typePlaceholder.Controls.Add(new LiteralControl("</div>"));
                typePlaceholder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    typePlaceholder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateNotesRadio(string screeningType, PlaceHolder typePlaceHolder)
        {
            LookupLogic lookUp = new LookupLogic();
            //LookupItemView[] questionsList = lookUp.getQuestions("SessionReferralsNetworks").ToArray();
            //int i = 0;
            if (Cache[screeningType] == null)
            {

                List<LookupItemView> questionsCacheList = lookUp.getQuestions(screeningType);
                HttpRuntime.Cache.Insert(screeningType, questionsCacheList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            int i = 0;
            List<LookupItemView> questionsList = (List<LookupItemView>)Cache[screeningType];
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
                typePlaceHolder.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    typePlaceHolder.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    typePlaceHolder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    typePlaceHolder.Controls.Add(new LiteralControl("</div>"));
                    typePlaceHolder.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "session1rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    typePlaceHolder.Controls.Add(rbList);
                    typePlaceHolder.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    typePlaceHolder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    typePlaceHolder.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    typePlaceHolder.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        typePlaceHolder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left session1notessection'>"));
                    }
                    else
                    {
                        typePlaceHolder.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "session1tb" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    typePlaceHolder.Controls.Add(notesTb);
                    typePlaceHolder.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                typePlaceHolder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    typePlaceHolder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void getAdherenceCtrls()
        {
            //MMAS4
            PHPillCount.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHPillCount.Controls.Add(new LiteralControl("<span class='input-group-addon'>Adherence % (from pill count)</span>"));
            pillAdherenceTb = new TextBox();
            pillAdherenceTb.CssClass = "form-control input-sm";
            pillAdherenceTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            pillAdherenceTb.ID = "session1tb" + LookupLogic.GetLookupItemId("PillAdherenceQ1");
            PHPillCount.Controls.Add(pillAdherenceTb);
            PHPillCount.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHDateFilled.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHDateFilled.Controls.Add(new LiteralControl("<span class='input-group-addon'>Date filled in</span>"));
            dateFilledTb = new TextBox();
            dateFilledTb.CssClass = "form-control input-sm filldate";
            dateFilledTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            dateFilledTb.ID = "session1tb" + LookupLogic.GetLookupItemId("PillAdherenceQ2");
            PHDateFilled.Controls.Add(dateFilledTb);
            PHDateFilled.Controls.Add(new LiteralControl("</div>"));
            //Follow up date
            appointmentDateTb = new TextBox();
            appointmentDateTb.CssClass = "form-control input-sm sessiononefollowdate";
            appointmentDateTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            appointmentDateTb.ID = "session1tb" + LookupLogic.GetLookupItemId("Session1FollowupDate");
            PHFollowupDate.Controls.Add(appointmentDateTb);
        }
        private void populateMMAS()
        {
            LookupLogic lookUp = new LookupLogic();
            //LookupItemView[] questionsArray = lookUp.getQuestions("MMAS4").ToArray();
            //LookupItemView[] questionsArray;
            ////string jsondata = new JavaScriptSerializer().Serialize(questionsList);
            ////string path = Server.MapPath("~/CCC/Data/");
            ////System.IO.File.WriteAllText(path + "output.json", jsondata);
            //string path = Server.MapPath("~/CCC/Data/output.json");
            int i = 0;
            //using (StreamReader r = new StreamReader(path))
            //{
            //    string json = r.ReadToEnd();
            //    JavaScriptSerializer ser = new JavaScriptSerializer();
            //    questionsArray = ser.Deserialize<LookupItemView[]>(json);
            //    //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            //}
            if (Cache["MMAS4"] == null)
            {

                List<LookupItemView> questionsCacheList = lookUp.getQuestions("MMAS4");
                HttpRuntime.Cache.Insert("MMAS4", questionsCacheList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            //LookupItemView[] questionsArray = (LookupItemView[])Cache["MMAS4"];
            foreach (var value in (List<LookupItemView>)Cache["MMAS4"])
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                string radioItems = "";
                LookupItemView[] itemList = lookUp.getQuestions(value.ItemName).ToArray();
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        radioItems = items.ItemName;
                    }
                }
                PHMMAS4.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                if (radioItems != "")
                {
                    PHMMAS4.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHMMAS4.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    PHMMAS4.Controls.Add(new LiteralControl("</div>"));
                    PHMMAS4.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "session1rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHMMAS4.Controls.Add(rbList);
                    PHMMAS4.Controls.Add(new LiteralControl("</div>"));
                }
                PHMMAS4.Controls.Add(new LiteralControl("</div>"));
                //var lastItem = questionsArray.Last();
                //if (!value.Equals(lastItem))
                //{
                    PHMMAS4.Controls.Add(new LiteralControl("<hr />"));
                //}

            }

            if (Cache["MMAS8"] == null)
            {

                List<LookupItemView> questionsCacheList = lookUp.getQuestions("MMAS8");
                HttpRuntime.Cache.Insert("MMAS8", questionsCacheList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            //LookupItemView[] mmas8QuestionsList = lookUp.getQuestions("MMAS8").ToArray();
            List<LookupItemView> mmas8QuestionsList = (List<LookupItemView>)Cache["MMAS8"];
            foreach (var value in mmas8QuestionsList)
            {
                i = i + 1;
                var lastItem = mmas8QuestionsList.Last();
                screenTypeId = value.MasterId;
                string radioItems = "";
                LookupItemView[] itemList = lookUp.getQuestions(value.ItemName).ToArray();
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        radioItems = items.ItemName;
                    }
                }
                PHMMAS8.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                if (radioItems != "")
                {

                    if (value.Equals(lastItem))
                    {
                        PHMMAS8.Controls.Add(new LiteralControl("<div class='col-md-4 text-left'>"));
                        PHMMAS8.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                        PHMMAS8.Controls.Add(new LiteralControl("</div>"));
                        PHMMAS8.Controls.Add(new LiteralControl("<div class='col-md-8 text-right'>"));
                        rbList = new RadioButtonList();
                        rbList.ID = "session1rb" + value.ItemId.ToString();
                        rbList.RepeatColumns = 5;
                        rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        rbList.CssClass = "mmrbList";
                        lookUp.populateRBL(rbList, radioItems);
                        PHMMAS8.Controls.Add(rbList);
                        PHMMAS8.Controls.Add(new LiteralControl("</div>"));
                    }
                    else
                    {
                        PHMMAS8.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                        PHMMAS8.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                        PHMMAS8.Controls.Add(new LiteralControl("</div>"));
                        PHMMAS8.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                        rbList = new RadioButtonList();
                        rbList.ID = "session1rb" + value.ItemId.ToString();
                        rbList.RepeatColumns = 2;
                        rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        rbList.CssClass = "mmrbList";
                        lookUp.populateRBL(rbList, radioItems);
                        PHMMAS8.Controls.Add(rbList);
                        PHMMAS8.Controls.Add(new LiteralControl("</div>"));
                    }
                }
                PHMMAS8.Controls.Add(new LiteralControl("</div>"));

                if (!value.Equals(lastItem))
                {
                    PHMMAS8.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        public void getScoreCtrls()
        {
            //MMAS4
            PHMMAS4Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS4Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-4) Score</span>"));
            mmas4TbScore = new TextBox();
            mmas4TbScore.CssClass = "form-control input-sm";
            mmas4TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas4TbScore.ID = "session1tb" + LookupLogic.GetLookupItemId("mmas4Score");
            mmas4TbScore.Enabled = false;
            PHMMAS4Scores.Controls.Add(mmas4TbScore);
            PHMMAS4Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>/ 4</span>"));
            PHMMAS4Scores.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHMMAS4Rating.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS4Rating.Controls.Add(new LiteralControl("<span class='input-group-addon'>Adherence Rating</span>"));
            mmas4TbRating = new TextBox();
            mmas4TbRating.CssClass = "form-control input-sm";
            mmas4TbRating.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas4TbRating.ID = "session1tb" + LookupLogic.GetLookupItemId("mmas4Adherence");
            mmas4TbRating.Enabled = false;
            PHMMAS4Rating.Controls.Add(mmas4TbRating);
            PHMMAS4Rating.Controls.Add(new LiteralControl("</div>"));

            //MMAS8
            PHMMAS8Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS8Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-8) Score</span>"));
            mmas8TbScore = new TextBox();
            mmas8TbScore.CssClass = "form-control input-sm";
            mmas8TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbScore.ID = "session1tb" + LookupLogic.GetLookupItemId("mmas8Score");
            mmas8TbScore.Enabled = false;
            PHMMAS8Scores.Controls.Add(mmas8TbScore);
            PHMMAS8Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>/ 8</span>"));
            PHMMAS8Scores.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHMMAS8Rating.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS8Rating.Controls.Add(new LiteralControl("<span class='input-group-addon'>Adherence Rating</span>"));
            mmas8TbRating = new TextBox();
            mmas8TbRating.CssClass = "form-control input-sm";
            mmas8TbRating.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbRating.ID = "session1tb" + LookupLogic.GetLookupItemId("mmas8Adherence");
            mmas8TbRating.Enabled = false;
            PHMMAS8Rating.Controls.Add(mmas8TbRating);
            PHMMAS8Rating.Controls.Add(new LiteralControl("</div>"));
            //Recommendation
            PHMMASRecommendation.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMASRecommendation.Controls.Add(new LiteralControl("<span class='input-group-addon'>Recommendation</span>"));
            mmas8TbRecommendation = new TextBox();
            mmas8TbRecommendation.CssClass = "form-control input-sm";
            mmas8TbRecommendation.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbRecommendation.ID = "session1tb" + LookupLogic.GetLookupItemId("mmas8Recommendation");
            mmas8TbRecommendation.Enabled = false;
            PHMMASRecommendation.Controls.Add(mmas8TbRecommendation);
            PHMMASRecommendation.Controls.Add(new LiteralControl("</div>"));
        }
    }
}