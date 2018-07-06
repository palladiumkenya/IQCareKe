using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;

namespace IQCare.Web.CCC.UC.Depression
{
    public partial class ucDepressionScreening : System.Web.UI.UserControl
    {
        public int depressionId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                displayQuestions();
            }
        }
        public void displayQuestions()
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateRBL(rbBotheredbyHopeless, "GeneralYesNo");
            lookUp.populateRBL(rbBotheredByLittleInterest, "GeneralYesNo");
            List<LookupItemView> questionsList = lookUp.getQuestions("PHQ9Questions");
            foreach (var value in questionsList)
            {
                PlaceHolder1.Controls.Add(new LiteralControl("<div class='row'>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<div class='col-md-5 text-left'>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<label>" + value.ItemDisplayName + "" + "</label>"));
                PlaceHolder1.Controls.Add(new LiteralControl("</div>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<div class='col-md-7 text-right'>"));
                RadioButtonList rbList = new RadioButtonList();
                rbList.ID = value.ItemName;
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "rbList";
                lookUp.populateRBL(rbList, "DepressionSeverity");
                PlaceHolder1.Controls.Add(rbList);
                PlaceHolder1.Controls.Add(new LiteralControl("</div>"));
                PlaceHolder1.Controls.Add(new LiteralControl("</div>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<hr />"));
            }
        }
    }
}