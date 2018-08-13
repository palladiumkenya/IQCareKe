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

namespace IQCare.Web.CCC.UC.EnhanceAdherenceCounselling
{
    public partial class ucMMAS4 : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
        public TextBox mmas4TbScore;
        public TextBox mmas4TbRating;
        public TextBox mmas8TbScore;
        public TextBox mmas8TbRating;
        public RadioButtonList rbList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                populateQuestions();
                getScoreCtrls();
            }
        }
        public void populateQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("MMAS4");
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
                    rbList.ID = value.ItemId.ToString();
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

            List<LookupItemView> mmas8QuestionsList = lookUp.getQuestions("MMAS8");
            foreach (var value in mmas8QuestionsList)
            {
                i = i + 1;
                var lastItem = mmas8QuestionsList.Last();
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
                        rbList.ID = value.ItemId.ToString();
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
                        rbList.ID = value.ItemId.ToString();
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
            mmas4TbScore.ID = "mmas4" + LookupLogic.GetLookupItemId("AlcoholScore");
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
            mmas4TbRating.ID = "mmas4" + LookupLogic.GetLookupItemId("AlcoholRiskLevel");
            mmas4TbRating.Enabled = false;
            PHMMAS4Rating.Controls.Add(mmas4TbRating);
            PHMMAS4Rating.Controls.Add(new LiteralControl("</div>"));

            //MMAS8
            PHMMAS8Scores.Controls.Add(new LiteralControl("<div class='input-group'>"));
            PHMMAS8Scores.Controls.Add(new LiteralControl("<span class='input-group-addon'>(MMAS-8) Score</span>"));
            mmas8TbScore = new TextBox();
            mmas8TbScore.CssClass = "form-control input-sm";
            mmas8TbScore.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            mmas8TbScore.ID = "mmas8" + LookupLogic.GetLookupItemId("AlcoholScore");
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
            mmas8TbRating.ID = "mmas8" + LookupLogic.GetLookupItemId("AlcoholRiskLevel");
            mmas8TbRating.Enabled = false;
            PHMMAS8Rating.Controls.Add(mmas8TbRating);
            PHMMAS8Rating.Controls.Add(new LiteralControl("</div>"));
        }
    }
}