using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using IQCare.CCC.UILogic;
using System.Web.UI.WebControls;
using Entities.CCC.Enrollment;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.Enrollment
{
    public partial class ServiceEnrollment : System.Web.UI.Page
    {
        public string patType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientType"] !=null && Session["PatientType"].ToString() != null)
            {
                var x = Session["PatientType"].ToString();
                var patientType = int.Parse(Session["PatientType"].ToString());
                patType = LookupLogic.GetLookupNameById(patientType);
            }

            var identifierManager = new IdentifierManager();
            List<Identifier> identifiers = identifierManager.GetAllIdentifiers();

            if (identifiers != null && identifiers.Count > 0)
            {
                IdentifierTypeId.Items.Add(new ListItem("select", "0"));
                foreach (var k in identifiers)
                {
                    IdentifierTypeId.Items.Add(new ListItem(k.Name, k.Id.ToString()));
                    IdentifierTypeId.SelectedIndex = 1;
                    IdentifierTypeId.Enabled = false;
                }
            }

            CreateDynamicControls();
        }

        public void CreateDynamicControls()
        {
            DataTable dataTable = new DataTable();
            dataTable = CustomFields();

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //HtmlGenericControl tr = new HtmlGenericControl("tr");
                    //HtmlGenericControl td = new HtmlGenericControl("td");

                    HtmlGenericControl prefixDiv = new HtmlGenericControl("div");
                    prefixDiv.Attributes.Add("class", "col-md-5");

                    HtmlGenericControl dataDiv = new HtmlGenericControl("div");
                    dataDiv.Attributes.Add("class", "col-md-5");

                    String FieldName = Convert.ToString(dataTable.Rows[i]["FieldName"]);
                    String FieldType = Convert.ToString(dataTable.Rows[i]["FieldType"]);
                    String Code = Convert.ToString(dataTable.Rows[i]["Code"]);
                    String PrefixType = Convert.ToString(dataTable.Rows[i]["PrefixType"]);
                    String Required = Convert.ToString(dataTable.Rows[i]["Required"]);

                    if (FieldType.ToLower().Trim() == "numeric")
                    {
                        placeholder.Controls.Add(new LiteralControl("<table width='100%'>"));
                        placeholder.Controls.Add(new LiteralControl("<tr>"));

                        if (!String.IsNullOrWhiteSpace(PrefixType) && PrefixType=="mfl_code")
                        {
                            placeholder.Controls.Add(new LiteralControl("<td style='width:10%' align='left'>"));
                            placeholder.Controls.Add(new LiteralControl("<label align='center'>MFL CODE :</label>"));
                            placeholder.Controls.Add(new LiteralControl("</td>"));

                            placeholder.Controls.Add(new LiteralControl("<td style='width:20%' align='left'>"));
                            DropDownList thePrefixNumberText = new DropDownList();
                            thePrefixNumberText.ID = "txt" + PrefixType;
                            thePrefixNumberText.CssClass = "chosen-select form-control input-sm";
                            thePrefixNumberText.Width = 180;
                            //thePrefixNumberText.MaxLength = 50;
                            thePrefixNumberText.Attributes.Add("data-parsley-min", "1");
                            thePrefixNumberText.Attributes.Add("data-parsley-min-message", "Please select a facility");

                            var facilityListManager = new FacilityListManager();
                            var result = facilityListManager.GetFacilitiesList();
                            thePrefixNumberText.Items.Add(new ListItem("select", "0"));
                            foreach (var k in result)
                            {
                                thePrefixNumberText.Items.Add(new ListItem(k.Name, k.MFLCode.ToString()));
                            }

                            placeholder.Controls.Add(thePrefixNumberText);
                            //BindTextboxes(FieldName);
                            //theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
                            //thePrefixNumberText.Attributes.Add("data-parsley-type", "digits");
                            placeholder.Controls.Add(new LiteralControl("</td>"));

                            placeholder.Controls.Add(new LiteralControl("<td>"));
                            placeholder.Controls.Add(new LiteralControl("&nbsp;"));
                            placeholder.Controls.Add(new LiteralControl("</td>"));

                        }
                        placeholder.Controls.Add(new LiteralControl("<td style='width:20%' align='left'>"));
                        placeholder.Controls.Add(new LiteralControl("<label align='center'>" + FieldName + " :</label>"));
                        placeholder.Controls.Add(new LiteralControl("</td>"));

                        placeholder.Controls.Add(new LiteralControl("<td style='width:40%' align='left'>"));
                        TextBox theNumberText = new TextBox();
                        theNumberText.ID = "txt" + Code;
                        theNumberText.CssClass = "form-control";
                        theNumberText.Width = 180;
                        theNumberText.MaxLength = 50;
                        placeholder.Controls.Add(theNumberText);
                        //BindTextboxes(FieldName);
                        //theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
                        if (bool.Parse(Required))
                            theNumberText.Attributes.Add("data-parsley-required", "true");
                        theNumberText.Attributes.Add("data-parsley-type", "digits");
                        placeholder.Controls.Add(new LiteralControl("</td>"));
                        placeholder.Controls.Add(new LiteralControl("</tr>"));
                        placeholder.Controls.Add(new LiteralControl("</table>"));

                        //if (!String.IsNullOrWhiteSpace(PrefixType))
                        //{
                        //    HtmlGenericControl labelDiv = new HtmlGenericControl("div");
                        //    labelDiv.Attributes.Add("class", "col-md-12");

                        //    Label lbprefixname = new Label();
                        //    lbprefixname.ID = "lb" + PrefixType;
                        //    lbprefixname.Text = PrefixType;
                        //    lbprefixname.CssClass = "pull-left";

                        //    labelDiv.Controls.Add(lbprefixname);
                        //    prefixDiv.Controls.Add(labelDiv);


                        //    HtmlGenericControl labelPrefixDataDiv = new HtmlGenericControl("div");
                        //    labelPrefixDataDiv.Attributes.Add("class", "col-md-12 form-group");

                        //    TextBox txtprefixbox = new TextBox();
                        //    txtprefixbox.ID = "txt" + PrefixType;
                        //    txtprefixbox.Attributes.Add("data-parsley-required", "true");
                        //    txtprefixbox.CssClass = "form-control input-sm";
                        //    labelPrefixDataDiv.Controls.Add(txtprefixbox);
                        //    prefixDiv.Controls.Add(labelPrefixDataDiv);

                        //    placeholder.Controls.Add(prefixDiv);
                        //}

                        //HtmlGenericControl labelDataDiv = new HtmlGenericControl("div");
                        //labelDataDiv.Attributes.Add("class", "col-md-12");

                        //Label lbcustomename = new Label();
                        //lbcustomename.ID = "lb" + FieldName;
                        //lbcustomename.Text = FieldName;
                        //lbcustomename.CssClass = "pull-left";

                        //labelDataDiv.Controls.Add(lbcustomename);
                        //dataDiv.Controls.Add(labelDataDiv);

                        //HtmlGenericControl labelNumericDataDiv = new HtmlGenericControl("div");
                        //labelNumericDataDiv.Attributes.Add("class", "col-md-12 form-group");

                        //TextBox txtcustombox = new TextBox();
                        //txtcustombox.ID = "txt" + FieldName;
                        //txtcustombox.Text = FieldValue;
                        //txtcustombox.CssClass = "form-control input-sm";
                        //txtcustombox.Attributes.Add("data-parsley-type", "digits");
                        //if (bool.Parse(Required))
                        //{
                        //    txtcustombox.Attributes.Add("data-parsley-required", "true");
                        //}

                        //labelNumericDataDiv.Controls.Add(txtcustombox);
                        //dataDiv.Controls.Add(labelNumericDataDiv);
                        //placeholder.Controls.Add(dataDiv);
                    }
                }
            }
        }
        public DataTable CustomFields()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldName", typeof(String));
            dataTable.Columns.Add("FieldType", typeof(String));
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("PrefixType", typeof(String));
            dataTable.Columns.Add("Required", typeof(String));

            var serviceareIdentifiersManager = new ServiceAreaIdentifiersManager();
            var identifierManager = new IdentifierManager();

            var identifiers = serviceareIdentifiersManager.GetIdentifiersByServiceArea(1);
            if (identifiers.Count > 0)
            {
                for (int i = 0; i < identifiers.Count; i++)
                {
                    var resultIdentifiers = identifierManager.GetIdentifiersById(identifiers[i].IdentifierId);
                    if (resultIdentifiers.Count > 0)
                    {
                        for (int j = 0; j < resultIdentifiers.Count; j++)
                        {
                            dataTable.Rows.Add(resultIdentifiers[i].DisplayName, resultIdentifiers[i].DataType, resultIdentifiers[j].Code, resultIdentifiers[i].PrefixType, identifiers[i].RequiredFlag);

                            //listidentifiers.Add(resultIdentifiers[j]);
                        }
                    }
                }
            }
            //dataTable.Rows.Add("FirstName", "TextBox", String.Empty);
            return dataTable;
        }
    }
}