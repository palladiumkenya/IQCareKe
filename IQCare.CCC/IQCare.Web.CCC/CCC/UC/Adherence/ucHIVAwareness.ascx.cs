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

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucHIVAwareness : System.Web.UI.UserControl
    {
        public int HIVStatusId, PatientId, PatientMasterVisitId, userId, screenTypeId;
        public RadioButtonList rbList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                displayQuestions();
                //getHIVAwareness(PatientId, PatientMasterVisitId);
            }
        }

        protected void displayQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("AwarenessofHIVStatus");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='row'>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<label>" + i+". " + value.ItemDisplayName + "" + "</label>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "awareness"+value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "rbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                QuestionsPlaceholder.Controls.Add(rbList);
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                QuestionsPlaceholder.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    QuestionsPlaceholder.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        protected void getHIVAwareness(int PatientId, int PatientMasterVisitId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    HIVStatusId = Convert.ToInt32(value.ScreeningTypeId);
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