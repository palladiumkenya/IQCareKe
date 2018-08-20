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
    public partial class ucEndSessionViralLoad : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, NotesId, screenTypeId;
        public RadioButtonList rbList;
        public TextBox notesTb;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                populateCtrls();
            }
        }
        protected void populateCtrls()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("EndSessionViralLoad");
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
                PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='row' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("</div>"));
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "rbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHEndSessionViralLoad.Controls.Add(rbList);
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + radioItems + "</label>"));
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='col-md-12 text-left notessection'>"));
                    }
                    else
                    {
                        PHEndSessionViralLoad.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHEndSessionViralLoad.Controls.Add(notesTb);
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                PHEndSessionViralLoad.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHEndSessionViralLoad.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
    }
}