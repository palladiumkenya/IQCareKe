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
    public partial class ucDisclosure : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        public int screenTypeId = 0, recordId = 0;
        public RadioButtonList rbList;
        public DropDownList ddList;
        public int NotesId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                getDisclosureCtrls();
                getDisclosureData(PatientId);
            }
        }
        public void getDisclosureCtrls()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("Disclosure");
            LookupItemView[] questionsArray = questionsList.ToArray();
            int i = 0;
            PHDisclosureQuestions.Controls.Add(new LiteralControl("<div class='row'>"));
            foreach (var value in questionsArray)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHDisclosureQuestions.Controls.Add(new LiteralControl("<div class='col-md-6 text-left' id='"+value.ItemId+"'>"));
                PHDisclosureQuestions.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                PHDisclosureQuestions.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHDisclosureQuestions.Controls.Add(new LiteralControl("</div>"));
                PHDisclosureQuestions.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                LookupItemView[] itemList = lookUp.getQuestions(value.ItemName).ToArray();
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        if (items.ItemName == "GeneralYesNo")
                        {
                            rbList = new RadioButtonList();
                            rbList.ID = "disclosurerbl" + value.ItemId.ToString();
                            rbList.RepeatColumns = 4;
                            rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            rbList.CssClass = "disclosurerbList";
                            lookUp.populateRBL(rbList, items.ItemName);
                            PHDisclosureQuestions.Controls.Add(rbList);
                        }
                        else
                        {
                            ddList = new DropDownList();
                            ddList.ID = "disclosureddl" + value.ItemId.ToString();
                            ddList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            ddList.CssClass = "disclosureddList disclosedToList";
                            lookUp.populateDDL(ddList, items.ItemName);
                            PHDisclosureQuestions.Controls.Add(ddList);
                        }
                    }
                }
                PHDisclosureQuestions.Controls.Add(new LiteralControl("</div>"));
                PHDisclosureQuestions.Controls.Add(new LiteralControl("</div>"));
            }
            PHDisclosureQuestions.Controls.Add(new LiteralControl("</div>"));
        }
        public void getDisclosureData(int patientId)
        {
            var PSM = new PatientScreeningManager();
            PatientScreening[] screeningList = PSM.GetPatientScreening(PatientId).ToArray();
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    RadioButtonList rbl = (RadioButtonList)FindControl("disclosurerbl" + value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                    DropDownList ddl = (DropDownList)FindControl("disclosureddl" + value.ScreeningCategoryId.ToString());
                    if (ddl != null)
                    {
                        ddl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
        }
    }
}