using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using IQCare.CCC.UILogic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.Enrollment
{
    public partial class ServiceEnrollment : System.Web.UI.Page
    {
        public string patType { get; set; }
        public int PersonId { get; set; }
        public int PatientExists { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientType"] !=null && Session["PatientType"].ToString() != null)
            {
                var patientType = int.Parse(Session["PatientType"].ToString());
                patType = LookupLogic.GetLookupNameById(patientType);
            }

            var patientLookManager = new PatientLookupManager();
            int person = int.Parse(Session["PersonId"].ToString());
            List<PatientLookup> patient = patientLookManager.GetPatientByPersonId(person);
            if (patient.Count > 0)
            {
                PatientExists = patient[0].Id;
            }

            if (!IsPostBack)
            {
                ILookupManager mgr =
                    (ILookupManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

                List<LookupItemView> ms = mgr.GetLookItemByGroup("YesNo");
                if (ms != null && ms.Count > 0)
                {
                    ReconfirmatoryTest.Items.Add(new ListItem("select", "0"));
                    foreach (var k in ms)
                    {
                        ReconfirmatoryTest.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> ReConfirmatoryTest = mgr.GetLookItemByGroup("ReConfirmatoryTest");
                if (ReConfirmatoryTest != null && ReConfirmatoryTest.Count > 0)
                {
                    ResultReConfirmatoryTest.Items.Add(new ListItem("select", "0"));
                    foreach (var k in ReConfirmatoryTest)
                    {
                        ResultReConfirmatoryTest.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> testTypes = mgr.GetLookItemByGroup("HivTestTypes");
                if (testTypes != null && testTypes.Count > 0)
                {
                    TypeOfReConfirmatoryTest.Items.Add(new ListItem("select", ""));
                    foreach (var item in testTypes)
                    {
                        TypeOfReConfirmatoryTest.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }

                //ReconfirmatoryTest.Items.Add();
            }

            //var identifierManager = new IdentifierManager();
            //List<Identifier> identifiers = identifierManager.GetAllIdentifiers();

            //if (identifiers != null && identifiers.Count > 0)
            //{
            //    IdentifierTypeId.Items.Add(new ListItem("select", "0"));
            //    foreach (var k in identifiers)
            //    {
            //        IdentifierTypeId.Items.Add(new ListItem(k.Name, k.Id.ToString()));
            //        IdentifierTypeId.SelectedIndex = 1;
            //        IdentifierTypeId.Enabled = false;
            //    }
            //}

            //CreateDynamicControls();

            //PersonId = int.Parse(Session["PersonId"].ToString());
        }

        //public void CreateDynamicControls()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = CustomFields();

        //    if (dataTable.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            HtmlGenericControl prefixDiv = new HtmlGenericControl("div");
        //            prefixDiv.Attributes.Add("class", "col-md-5");

        //            HtmlGenericControl dataDiv = new HtmlGenericControl("div");
        //            dataDiv.Attributes.Add("class", "col-md-5");

        //            String FieldName = Convert.ToString(dataTable.Rows[i]["FieldName"]);
        //            String FieldType = Convert.ToString(dataTable.Rows[i]["FieldType"]);
        //            String Code = Convert.ToString(dataTable.Rows[i]["Code"]);
        //            String PrefixType = Convert.ToString(dataTable.Rows[i]["PrefixType"]);
        //            String Required = Convert.ToString(dataTable.Rows[i]["Required"]);


        //            placeholder.Controls.Add(new LiteralControl("<table width='100%'>"));
        //            placeholder.Controls.Add(new LiteralControl("<tr>"));

        //            if (FieldType.ToLower().Trim() == "numeric")
        //            {
        //                if (!String.IsNullOrWhiteSpace(PrefixType) && PrefixType == "mfl_code")
        //                {
        //                    placeholder.Controls.Add(new LiteralControl("<td style='width:10%' align='left'>"));
        //                    placeholder.Controls.Add(new LiteralControl("<label align='center'>MFL CODE :</label>"));
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));

        //                    placeholder.Controls.Add(new LiteralControl("<td style='width:20%' align='left'>"));
        //                    DropDownList thePrefixNumberText = new DropDownList();
        //                    thePrefixNumberText.ID = PrefixType;
        //                    thePrefixNumberText.CssClass = "chosen-select form-control input-sm";
        //                    thePrefixNumberText.Width = 180;

        //                    thePrefixNumberText.Attributes.Add("data-parsley-min", "1");
        //                    thePrefixNumberText.Attributes.Add("data-parsley-min-message", "Please select a facility");

        //                    var facilityListManager = new FacilityListManager();
        //                    var result = facilityListManager.GetFacilitiesList();
        //                    thePrefixNumberText.Items.Add(new ListItem("select", "0"));
        //                    foreach (var k in result)
        //                    {
        //                        thePrefixNumberText.Items.Add(new ListItem(k.Name, k.MFLCode.ToString()));
        //                    }
        //                    thePrefixNumberText.SelectedValue = Session["AppPosID"].ToString();
        //                    placeholder.Controls.Add(thePrefixNumberText);
        //                    //BindTextboxes(FieldName);
        //                    //theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
        //                    //thePrefixNumberText.Attributes.Add("data-parsley-type", "digits");
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));

        //                    placeholder.Controls.Add(new LiteralControl("<td>"));
        //                    placeholder.Controls.Add(new LiteralControl("&nbsp;"));
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));

        //                }
        //                else
        //                {
        //                    placeholder.Controls.Add(new LiteralControl("<td style='width:10%' align='left'>"));
        //                    placeholder.Controls.Add(new LiteralControl("<label align='center'>MFL CODE :</label>"));
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));

        //                    placeholder.Controls.Add(new LiteralControl("<td style='width:20%' align='left'>"));
        //                    TextBox thePrefixNumberText = new TextBox();
        //                    thePrefixNumberText.ID = PrefixType;
        //                    thePrefixNumberText.CssClass = "chosen-select form-control input-sm";
        //                    thePrefixNumberText.Width = 180;

        //                    thePrefixNumberText.Attributes.Add("data-parsley-min", "1");
        //                    thePrefixNumberText.Attributes.Add("data-parsley-min-message", "Please select a facility");
        //                    placeholder.Controls.Add(thePrefixNumberText);
        //                    //BindTextboxes(FieldName);
        //                    //theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
        //                    thePrefixNumberText.Attributes.Add("data-parsley-type", "digits");
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));

        //                    placeholder.Controls.Add(new LiteralControl("<td>"));
        //                    placeholder.Controls.Add(new LiteralControl("&nbsp;"));
        //                    placeholder.Controls.Add(new LiteralControl("</td>"));
        //                }
        //                placeholder.Controls.Add(new LiteralControl("<td style='width:20%' align='left'>"));
        //                placeholder.Controls.Add(new LiteralControl("<label align='center'>" + FieldName + " :</label>"));
        //                placeholder.Controls.Add(new LiteralControl("</td>"));

        //                placeholder.Controls.Add(new LiteralControl("<td style='width:40%' align='left'>"));
        //                TextBox theNumberText = new TextBox();
        //                theNumberText.ID = Code;
        //                theNumberText.CssClass = "form-control";
        //                theNumberText.Width = 180;
        //                theNumberText.MaxLength = 50;
        //                placeholder.Controls.Add(theNumberText);
        //                //BindTextboxes(FieldName);
        //                //theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
        //                if (bool.Parse(Required))
        //                    theNumberText.Attributes.Add("data-parsley-required", "true");
        //                theNumberText.Attributes.Add("data-parsley-type", "digits");
        //                placeholder.Controls.Add(new LiteralControl("</td>"));
        //                placeholder.Controls.Add(new LiteralControl("</tr>"));
        //                placeholder.Controls.Add(new LiteralControl("</table>"));
        //            }
        //        }
        //    }
        //}
        //public DataTable CustomFields()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("FieldName", typeof(String));
        //    dataTable.Columns.Add("FieldType", typeof(String));
        //    dataTable.Columns.Add("Code", typeof(String));
        //    dataTable.Columns.Add("PrefixType", typeof(String));
        //    dataTable.Columns.Add("Required", typeof(String));

        //    var serviceareIdentifiersManager = new ServiceAreaIdentifiersManager();
        //    var identifierManager = new IdentifierManager();

        //    var identifiers = serviceareIdentifiersManager.GetIdentifiersByServiceArea(1);
        //    if (identifiers.Count > 0)
        //    {
        //        for (int i = 0; i < identifiers.Count; i++)
        //        {
        //            var resultIdentifiers = identifierManager.GetIdentifiersById(identifiers[i].IdentifierId);
        //            if (resultIdentifiers.Count > 0)
        //            {
        //                for (int j = 0; j < resultIdentifiers.Count; j++)
        //                {
        //                    dataTable.Rows.Add(resultIdentifiers[i].DisplayName, resultIdentifiers[i].DataType, resultIdentifiers[j].Code, resultIdentifiers[i].PrefixType, identifiers[i].RequiredFlag);

        //                    //listidentifiers.Add(resultIdentifiers[j]);
        //                }
        //            }
        //        }
        //    }
        //    //dataTable.Rows.Add("FirstName", "TextBox", String.Empty);
        //    return dataTable;
        //}
    }
}