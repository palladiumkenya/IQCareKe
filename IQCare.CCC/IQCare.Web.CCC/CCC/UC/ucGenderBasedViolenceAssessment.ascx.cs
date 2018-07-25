using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucGenderBasedViolenceAssessment : System.Web.UI.UserControl
    {
        protected ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return Convert.ToInt32(Session["patientMasterVisitId"]); }
        }

        protected int ScreeningCategoryId
        {
            get {
                var gbvAssessmentId = Convert.ToInt32(lookupManager.GetLookupItemId("GBVAssessment"));
                return gbvAssessmentId;
            }
        }
        
        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserID"]); }
        }

        protected int yesId
        {
            get {
                return Convert.ToInt32(lookupManager.GetLookupItemId("Yes"));
            }
        }

        protected int noId
        {
            get
            {
                return Convert.ToInt32(lookupManager.GetLookupItemId("No"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<LookupItemView> screeningQuestions = lookupManager.GetLookItemByGroup("GBVAssessment");

            if (screeningQuestions != null && screeningQuestions.Count > 0)
            {

                foreach (var screeningQuestion in screeningQuestions)
                {
                    HtmlTableRow row = new HtmlTableRow();
                    HtmlTableCell cell1 = new HtmlTableCell();
                    HtmlTableCell cell2 = new HtmlTableCell();

                    cell1.Align = "left";
                    cell2.Align = "left";

                    List<LookupItemView> responses = lookupManager.GetLookItemByGroup("YesNo");

                    foreach (var k in responses)
                    {
                        HtmlInputRadioButton radioButton = new HtmlInputRadioButton
                        {
                            ID = string.Format("|{0}|{1}", screeningQuestion.ItemId.ToString(), k.ItemId.ToString()),
                            Name = screeningQuestion.ItemName,
                            Value = k.ItemId.ToString(),
                        };

                        radioButton.Attributes.Add("data-parsley-required", "True");
                        radioButton.Attributes.Add("itemid", screeningQuestion.ItemId.ToString());

                        HtmlGenericControl label = new HtmlGenericControl("label");

                        label.InnerHtml = string.Format("{0}", k.ItemName);
                        cell2.Controls.Add(label);
                        cell2.Controls.Add(radioButton);
                    }

                    cell1.InnerHtml = screeningQuestion.ItemDisplayName;

                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    tblGbvScreeningQuestions.Rows.Add(row);

                }

            }
        }
    }
}