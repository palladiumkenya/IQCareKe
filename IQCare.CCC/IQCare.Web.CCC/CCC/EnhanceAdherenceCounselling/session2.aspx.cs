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

namespace IQCare.Web.CCC.UC.EnhanceAdherenceCounselling
{
    public partial class session2 : System.Web.UI.Page
    {
        public int PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
        public RadioButtonList rbList;
        public TextBox notesTb;
        public TextBox pillAdherenceTb;
        public TextBox dateFilledTb;
        public TextBox mmas4TbScore;
        public TextBox mmas4TbRating;
        public TextBox mmas8TbScore;
        public TextBox mmas8TbRating;
        public TextBox mmas8TbRecommendation;
        public int appointmentId;
        public string serviceAreaId;
        public string reasonId;
        public string differentiatedCareId;
        public string followupStatusId;
        public TextBox appointmentDateTb;
        public string Session2Refferal1ItemId;
        public string Session2Refferal3ItemId;
        public string Session2Refferal2ItemId;
        public string ItemYes;
        public string ItemNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
            serviceAreaId = LookupLogic.GetLookupItemId("MoH 257 GREENCARD");
            reasonId = LookupLogic.GetLookupItemId("Follow Up");
            differentiatedCareId = LookupLogic.GetLookupItemId("Standard Care");
            Session2Refferal1ItemId = LookupLogic.GetLookupItemId("Session2ReferralsNetworksQ1");
            Session2Refferal3ItemId = LookupLogic.GetLookupItemId("Session2ReferralsNetworksQ3");
            Session2Refferal2ItemId= LookupLogic.GetLookupItemId("Session2ReferralsNetworksQ2");
            ItemYes = LookupLogic.GetLookupItemId("Yes");
            ItemNo = LookupLogic.GetLookupItemId("No");
            followupStatusId = LookupLogic.GetLookupItemId("Pending");
            if (!IsPostBack)
            {
                populateMMAS();
                getAdherenceCtrls();
                getScoreCtrls();
                populateNotesRadio("Session2AdherenceReviews", PHAdherenceReview);
                populateNotesRadio("Session2ReferralsNetworks", PHReferralsandNetworks);
            }
        }
        public void populateMMAS()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("Session2MMAS4").ToArray();
            //List<LookupItemView> questionsList = lookUp.getQuestions("Session2MMAS4").ToList();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                string radioItems = "";
                List<LookupItemView> itemList = lookUp.getQuestions(value.ItemName);
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
                    rbList.ID = "session2rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHMMAS4.Controls.Add(rbList);
                    PHMMAS4.Controls.Add(new LiteralControl("</div>"));
                }
                PHMMAS4.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHMMAS4.Controls.Add(new LiteralControl("<hr />"));
                }

            }

            LookupItemView[] mmas8QuestionsList = lookUp.getQuestions("Session2MMAS8").ToArray();
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
                        rbList.ID = "session2rb" + value.ItemId.ToString();
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
                        rbList.ID = "session2rb" + value.ItemId.ToString();
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
        protected void populateNotesRadio(string screeningType, PlaceHolder typePlaceHolder)
        {
            LookupLogic lookUp = new LookupLogic();
            //if (Cache[screeningType] == null)
            //{

            //    List<LookupItemView> questionsCacheList = lookUp.getQuestions(screeningType);
            //    HttpRuntime.Cache.Insert(screeningType, questionsCacheList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            //}
            int i = 0;
           // List<LookupItemView> questionsList = (List<LookupItemView>)Cache[screeningType];
            List<LookupItemView> questionsList = lookUp.getQuestions(screeningType);
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
                    rbList.ID = "session2rb" + value.ItemId.ToString();
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
                    notesTb.ID = "session2tb" + value.ItemId.ToString();
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
            pillAdherenceTb.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2PillAdherenceQ1");
            PHPillCount.Controls.Add(pillAdherenceTb);
            PHPillCount.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHDateFilled.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHDateFilled.Controls.Add(new LiteralControl("<span class='input-group-addon'>Date filled in</span>"));
            dateFilledTb = new TextBox();
            dateFilledTb.CssClass = "form-control input-sm filldate";
            dateFilledTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            dateFilledTb.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2PillAdherenceQ2");
            PHDateFilled.Controls.Add(dateFilledTb);
            PHDateFilled.Controls.Add(new LiteralControl("</div>"));
            //Follow up date
            appointmentDateTb = new TextBox();
            appointmentDateTb.CssClass = "form-control input-sm s2followupdateinput";
            appointmentDateTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            appointmentDateTb.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2FollowupDate");
            PHFollowupDate.Controls.Add(appointmentDateTb);
        }
        public void getScoreCtrls()
        {
            //MMAS4
            PHMMAS4Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS4Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-4) Score</span>"));
            mmas4TbScore = new TextBox();
            mmas4TbScore.CssClass = "form-control input-sm";
            mmas4TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas4TbScore.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2mmas4Score");
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
            mmas4TbRating.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2mmas4Adherence");
            mmas4TbRating.Enabled = false;
            PHMMAS4Rating.Controls.Add(mmas4TbRating);
            PHMMAS4Rating.Controls.Add(new LiteralControl("</div>"));

            //MMAS8
            PHMMAS8Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS8Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-8) Score</span>"));
            mmas8TbScore = new TextBox();
            mmas8TbScore.CssClass = "form-control input-sm";
            mmas8TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbScore.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2mmas8Score");
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
            mmas8TbRating.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2mmas8Adherence");
            mmas8TbRating.Enabled = false;
            PHMMAS8Rating.Controls.Add(mmas8TbRating);
            PHMMAS8Rating.Controls.Add(new LiteralControl("</div>"));
            //Recommendation
            PHMMASRecommendation.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMASRecommendation.Controls.Add(new LiteralControl("<span class='input-group-addon'>Recommendation</span>"));
            mmas8TbRecommendation = new TextBox();
            mmas8TbRecommendation.CssClass = "form-control input-sm";
            mmas8TbRecommendation.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbRecommendation.ID = "session2tb" + LookupLogic.GetLookupItemId("Session2mmas8Recommendation");
            mmas8TbRecommendation.Enabled = false;
            PHMMASRecommendation.Controls.Add(mmas8TbRecommendation);
            PHMMASRecommendation.Controls.Add(new LiteralControl("</div>"));
        }
    }
}