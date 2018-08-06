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

namespace IQCare.Web.CCC.UC.EnhanceAdherenceCounselling
{
    public partial class ucSession1 : System.Web.UI.UserControl
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
                populateUnderstandingViralLoad();
                populateCognitiveBarriers();
                populateBehaviouralBarriers();
                populateEmotionalBarriers();
                populateSocioEconomicBarriers();
                populateReferrals();
                getSessionData(PatientId, PatientMasterVisitId);
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
            appointmentDateTb.CssClass = "form-control input-sm";
            appointmentDateTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            appointmentDateTb.ID = "session1tb" + LookupLogic.GetLookupItemId("Session1FollowupDate");
            PHFollowupDate.Controls.Add(appointmentDateTb);
        }
        private void populateMMAS()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("MMAS4").ToArray();
            LookupItemView[] questionsArray;
            //string jsondata = new JavaScriptSerializer().Serialize(questionsList);
            //string path = Server.MapPath("~/CCC/Data/");
            //System.IO.File.WriteAllText(path + "output.json", jsondata);
            string path = Server.MapPath("~/CCC/Data/output.json");
            int i = 0;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JavaScriptSerializer ser = new JavaScriptSerializer();
                questionsArray = ser.Deserialize<LookupItemView[]>(json);
                //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            foreach (var value in questionsArray)
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
                var lastItem = questionsArray.Last();
                if (!value.Equals(lastItem))
                {
                    PHMMAS4.Controls.Add(new LiteralControl("<hr />"));
                }

            }

            LookupItemView[] mmas8QuestionsList = lookUp.getQuestions("MMAS8").ToArray();
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
        protected void populateUnderstandingViralLoad()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("UnderstandingViralLoad").ToArray();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("<div class='row'>"));
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("</div>"));
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "session1tb" + value.ItemId.ToString();
                notesTb.Rows = 3;
                PHUnderstandingViralLoad.Controls.Add(notesTb);
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("</div>"));
                PHUnderstandingViralLoad.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHUnderstandingViralLoad.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateCognitiveBarriers()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("CognitiveBarriers").ToArray();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                PHCognitiveBarriers.Controls.Add(new LiteralControl("<div class='row'>"));
                PHCognitiveBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                PHCognitiveBarriers.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHCognitiveBarriers.Controls.Add(new LiteralControl("</div>"));
                PHCognitiveBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "session1tb" + value.ItemId.ToString();
                notesTb.Rows = 3;
                PHCognitiveBarriers.Controls.Add(notesTb);
                PHCognitiveBarriers.Controls.Add(new LiteralControl("</div>"));
                PHCognitiveBarriers.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHCognitiveBarriers.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateBehaviouralBarriers()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("BehaviouralBarriers").ToArray();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("<div class='row'>"));
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("</div>"));
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "session1tb" + value.ItemId.ToString();
                notesTb.Rows = 3;
                PHBahaviouralBarriers.Controls.Add(notesTb);
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("</div>"));
                PHBahaviouralBarriers.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHBahaviouralBarriers.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateEmotionalBarriers()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("EmotionalBarriers").ToArray();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                //NotesId = value.MasterId;
                PHEmotionalBarriers.Controls.Add(new LiteralControl("<div class='row'>"));
                PHEmotionalBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                PHEmotionalBarriers.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHEmotionalBarriers.Controls.Add(new LiteralControl("</div>"));
                PHEmotionalBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                NotesId = value.ItemId;
                notesTb = new TextBox();
                notesTb.TextMode = TextBoxMode.MultiLine;
                notesTb.CssClass = "form-control input-sm";
                notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                notesTb.ID = "session1tb" + value.ItemId.ToString();
                notesTb.Rows = 3;
                PHEmotionalBarriers.Controls.Add(notesTb);
                PHEmotionalBarriers.Controls.Add(new LiteralControl("</div>"));
                PHEmotionalBarriers.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHEmotionalBarriers.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateSocioEconomicBarriers()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("SocioEconomicBarriers").ToArray();
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
                PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("</div>"));
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "session1rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHSocioEconomicBarriers.Controls.Add(rbList);
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left session1notessection'>"));
                    }
                    else
                    {
                        PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "session1tb" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHSocioEconomicBarriers.Controls.Add(notesTb);
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                PHSocioEconomicBarriers.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHSocioEconomicBarriers.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void populateReferrals()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("SessionReferralsNetworks").ToArray();
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
                PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("</div>"));
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "session1rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHReferralsandNetworks.Controls.Add(rbList);
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-12 text-left session1notessection'>"));
                    }
                    else
                    {
                        PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "session1tb" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHReferralsandNetworks.Controls.Add(notesTb);
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                PHReferralsandNetworks.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHReferralsandNetworks.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void getSessionData(int patientId, int patientMasterVisitId)
        {
            var PCN = new PatientClinicalNotesLogic();
            //PatientClinicalNotes[] notesList = PCN.getPatientClinicalNotes(PatientId).ToArray();
            PatientClinicalNotes[] notesList = (PatientClinicalNotes[])Session["PatientNotesData"];
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    //PCId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)FindControl("session1tb" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                    if(LookupLogic.GetLookupItemId("Session1FollowupDate") == value.NotesCategoryId.ToString())
                    {
                        PatientAppointmentManager appointmentmgr = new PatientAppointmentManager();
                        if(value.ClinicalNotes != "")
                        {
                            List<PatientAppointment> paList = appointmentmgr.GetByDate(Convert.ToDateTime(value.ClinicalNotes));
                            foreach (var pavalue in paList)
                            {
                                appointmentId = pavalue.Id;
                            }
                        }
                        
                    }
                }
            }

            var PSM = new PatientScreeningManager();
            PatientScreening[] screeningList = (PatientScreening[])Session["PatientScreeningData"];
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    //PCId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rbl = (RadioButtonList)FindControl("session1rb"+value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
            //PatientAppointmentManager appointmentmgr = new PatientAppointmentManager();
            //PatientAppointment pa = appointmentmgr.GetAppointmentSummaryByDate(AppointmentId);
            //AppointmentDate.Text = pa.AppointmentDate.ToString("dd-MMM-yyy");
        }
    }
}