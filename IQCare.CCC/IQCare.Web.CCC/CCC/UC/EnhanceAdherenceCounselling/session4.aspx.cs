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
    public partial class session4 : System.Web.UI.Page
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
                populateMMAS();
                populateReferrals();
                populateAdherenceReviews();
                getAdherenceCtrls();
                getScoreCtrls();
            }
        }
        protected void populateReferrals()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("Session4ReferralsNetworks");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                string radioItems = "";
                int notesValue = 0;
                List<LookupItemView> itemList = lookUp.getQuestions(value.ItemName);
                LookupItemView[] itemArray = itemList.ToArray();
                if (itemArray.Any())
                {
                    foreach (var items in itemArray)
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
                    rbList.ID = "session4rb" + value.ItemId.ToString();
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
                        PHReferralsandNetworks.Controls.Add(new LiteralControl("<div class='col-md-12 text-left notessection'>"));
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
                    notesTb.ID = "session4tb" + value.ItemId.ToString();
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
        public void populateMMAS()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("Session4MMAS4");
            LookupItemView[] questionArray = questionsList.ToArray();
            int i = 0;
            foreach (var value in questionArray)
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
                    rbList.ID = "session4rb" + value.ItemId.ToString();
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

            List<LookupItemView> mmas8QuestionsList = lookUp.getQuestions("Session4MMAS8");
            LookupItemView[] mmas8QuestionArray = mmas8QuestionsList.ToArray();
            foreach (var value in mmas8QuestionArray)
            {
                i = i + 1;
                var lastItem = mmas8QuestionArray.Last();
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
                        rbList.ID = "session4rb" + value.ItemId.ToString();
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
                        rbList.ID = "session4rb" + value.ItemId.ToString();
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
        protected void populateAdherenceReviews()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("Session4AdherenceReviews");
            LookupItemView[] questionsArray = questionsList.ToArray();
            int i = 0;
            foreach (var value in questionsArray)
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
                PHAdherenceReview.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    PHAdherenceReview.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHAdherenceReview.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    PHAdherenceReview.Controls.Add(new LiteralControl("</div>"));
                    PHAdherenceReview.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "session4rb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "mmrbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHAdherenceReview.Controls.Add(rbList);
                    PHAdherenceReview.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    PHAdherenceReview.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    PHAdherenceReview.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    PHAdherenceReview.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        PHAdherenceReview.Controls.Add(new LiteralControl("<div class='col-md-12 text-left notessection'>"));
                    }
                    else
                    {
                        PHAdherenceReview.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "session4tb" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHAdherenceReview.Controls.Add(notesTb);
                    PHAdherenceReview.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                PHAdherenceReview.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHAdherenceReview.Controls.Add(new LiteralControl("<hr />"));
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
            pillAdherenceTb.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4PillAdherenceQ1");
            PHPillCount.Controls.Add(pillAdherenceTb);
            PHPillCount.Controls.Add(new LiteralControl("</div>"));
            //RISK
            PHDateFilled.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHDateFilled.Controls.Add(new LiteralControl("<span class='input-group-addon'>Date filled in</span>"));
            dateFilledTb = new TextBox();
            dateFilledTb.CssClass = "form-control input-sm filldate";
            dateFilledTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            dateFilledTb.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4PillAdherenceQ2");
            PHDateFilled.Controls.Add(dateFilledTb);
            PHDateFilled.Controls.Add(new LiteralControl("</div>"));
            //Follow up date
            appointmentDateTb = new TextBox();
            appointmentDateTb.CssClass = "form-control input-sm";
            appointmentDateTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            appointmentDateTb.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4FollowupDate");
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
            mmas4TbScore.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4mmas4Score");
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
            mmas4TbRating.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4mmas4Adherence");
            mmas4TbRating.Enabled = false;
            PHMMAS4Rating.Controls.Add(mmas4TbRating);
            PHMMAS4Rating.Controls.Add(new LiteralControl("</div>"));

            //MMAS8
            PHMMAS8Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS8Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-8) Score</span>"));
            mmas8TbScore = new TextBox();
            mmas8TbScore.CssClass = "form-control input-sm";
            mmas8TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbScore.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4mmas8Score");
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
            mmas8TbRating.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4mmas8Adherence");
            mmas8TbRating.Enabled = false;
            PHMMAS8Rating.Controls.Add(mmas8TbRating);
            PHMMAS8Rating.Controls.Add(new LiteralControl("</div>"));
            //Recommendation
            PHMMASRecommendation.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMASRecommendation.Controls.Add(new LiteralControl("<span class='input-group-addon'>Recommendation</span>"));
            mmas8TbRecommendation = new TextBox();
            mmas8TbRecommendation.CssClass = "form-control input-sm";
            mmas8TbRecommendation.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbRecommendation.ID = "session4tb" + LookupLogic.GetLookupItemId("Session4mmas8Recommendation");
            mmas8TbRecommendation.Enabled = false;
            PHMMASRecommendation.Controls.Add(mmas8TbRecommendation);
            PHMMASRecommendation.Controls.Add(new LiteralControl("</div>"));
        }
    }
}