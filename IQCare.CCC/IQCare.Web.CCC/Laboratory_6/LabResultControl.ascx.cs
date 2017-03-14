using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
using Interface.Laboratory;
using Application.Presentation;
using Entities.Lab;

namespace IQCare.Web.Laboratory
{
    public partial class LabResultControl : System.Web.UI.UserControl
    {
        int defaultId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindTestsParameter();
            }
        }
        public bool ShowModal
        {
            get
            {
                return hShowModal.Value.ToUpper().Trim() == "TRUE";
            }
            set
            {
                hShowModal.Value = value.ToString();
                mpeResultPopup.Enabled = value;
            }
        }
        public bool IsGroup
        {
            get
            {
                return HIsGroup.Value.ToUpper().Trim() == "TRUE";
            }
            set
            {
                HIsGroup.Value = value.ToString();
            }
        }

        protected string svModal
        {
            get
            {
                return ShowModal ? "" : "none";
            }
        }
        protected string svGroup
        {
            get
            {
                return IsGroup ? "" : "none";
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.BindTestsParameter();
        }

        public int LabOrderId
        {
            get
            {
                int val = defaultId;
                int.TryParse(HLabOrderId.Value, out val);
                return val;
            }
            set
            {
                HLabOrderId.Value = value.ToString();
            }
        }
        public int LabTestOrderId
        {
            get
            {
                int val = defaultId;
                int.TryParse(HLabTestOrderId.Value, out val);
                return val;
            }
            set
            {
                HLabTestOrderId.Value = value.ToString();
            }
        }
        public int LabTestId
        {
            get
            {
                int val = defaultId;
                int.TryParse(HLabTestId.Value, out val);
                return val;
            }
            set
            {
                HLabTestId.Value = value.ToString();
            }
        }
        public string OpenMode
        {
            get
            {
                return hOpenMode.Value;
            }
            set
            {
                hOpenMode.Value = value;
            }
        }
        public void EnableModelDialog(bool visibility)
        {
            if (ShowModal)
            {
                if (visibility && OpenMode != "VIEW")
                {
                    mpeResultPopup.Show();
                }
                else
                {

                    mpeResultPopup.Hide();
                }
            }
        }

        void InjectScript(ref CheckBox cBox, ref TextBox txtBox)
        {
            string checkUndectable = cBox.ClientID;
            string detectionLimit = txtBox.ClientID;
            string script = @"$(function () {$(""#" + checkUndectable + @""").click(function () {
             $(""#" + detectionLimit + @""").val("");
            if ($(this).is("":checked"")) {
                $(""#" + detectionLimit + @""").removeAttr(""disabled"");
                $(""#" + detectionLimit + @""").focus();
            } else {
                $(""#" + detectionLimit + @""").attr(""disabled"", ""disabled"");                
            }
        }); });";
            ScriptManager.RegisterStartupScript(cBox, cBox.GetType(), checkUndectable, script, true);
        }
        public void SaveResult()
        {

        }
        public void Rebind()
        {
            this.PopulateTestParameter();
        }
        BindFunctions theBindManager = new BindFunctions();
        string ShowTextDiv(object datatype, object resultText)
        {
            return (datatype.ToString().ToUpper() == "TEXT" && resultText.ToString().Trim() == "") ? "" : "none";
        }
        string ShowSelectDiv(object datatype, object resultText)
        {
            return (datatype.ToString().ToUpper() == "SELECT LIST" && resultText.ToString().Trim() == "") ? "" : "none";
        }
        string ShowNumDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "NUMERIC") ? "" : "none";
        }
        string ShowTextResult(object datatype, object resultText)
        {
            return ((datatype.ToString().ToUpper() == "TEXT" || datatype.ToString().ToUpper() == "SELECT LIST") && resultText.ToString().Trim() != "") ? "" : "none";
        }
        /// <summary>
        /// Gets or sets the test parameter.
        /// </summary>
        /// <value>
        /// The test parameter.
        /// </value>
        DataTable TestParameter
        {
            get
            {
                if (base.Session["LAB_PARAM"] == null)
                {
                    DataTable dtResult = new DataTable("Parameters");

                    dtResult.Columns.Add("TestOrderId", System.Type.GetType("System.Int32"));
                    dtResult.Columns["TestOrderId"].DefaultValue = LabTestOrderId;

                    dtResult.Columns.Add("ResultId", System.Type.GetType("System.Int32"));


                    dtResult.Columns.Add("LabOrderId", System.Type.GetType("System.Int32"));
                    dtResult.Columns["LabOrderId"].DefaultValue = LabOrderId;
                    dtResult.Columns.Add("TestId", System.Type.GetType("System.Int32"));
                    dtResult.Columns["TestId"].DefaultValue = LabTestId;


                    dtResult.Columns.Add("ParameterId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("UserId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("ResultDate", System.Type.GetType("System.DateTime"));
                    dtResult.Columns.Add("ParameterName", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("TestName", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("DataType", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultValue", System.Type.GetType("System.Decimal"));
                    dtResult.Columns.Add("UnDetectable", System.Type.GetType("System.Boolean"));
                    dtResult.Columns.Add("DetectionLimit", System.Type.GetType("System.Decimal"));
                    dtResult.Columns.Add("ResultText", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultOption", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultUnit", System.Type.GetType("System.String"));


                    dtResult.Columns.Add("Persisted", System.Type.GetType("System.Boolean"));
                    dtResult.Columns["Persisted"].DefaultValue = false;

                    dtResult.Columns.Add("DeleteFlag", System.Type.GetType("System.Boolean"));
                    dtResult.Columns["DeleteFlag"].DefaultValue = false;


                    return dtResult;
                }
                else
                {
                    return (DataTable)base.Session["LAB_PARAM?" + LabTestOrderId.ToString()];
                }
            }
            set
            {
                base.Session["LAB_PARAM?" + LabTestOrderId.ToString()] = value;
            }
        }
        void PopulateTestParameter()
        {
            ILabRequest lrMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");

            if (this.LabOrderId > 0)
            {
               // TestParameter = lrMgr.GetLabTestParameterResult(LabTestId, LabOrderId);
            }
            //else
            //{
            //    TestParameter = lrMgr.GetTestParameters(LabTestId);
            //}

        }
        void CatchPostBack()
        {
            foreach (RepeaterItem dataItem in repeaterResult.Items)
            {

                string _dataType = "";

                string paramId = "";
                DataTable dt = this.TestParameter;
                HiddenField hdataType = dataItem.FindControl("HResultDataType") as HiddenField;
                _dataType = hdataType.Value.ToUpper();
                HiddenField hParameterId = dataItem.FindControl("hParameterId") as HiddenField;

                DropDownList ddlResultUnit = dataItem.FindControl("ddlResultUnit") as DropDownList;
                DropDownList ddlResultList = dataItem.FindControl("ddlResultList") as DropDownList;
                TextBox txtResultText = dataItem.FindControl("textResultText") as TextBox;
                Label labelResultText = dataItem.FindControl("labelResultText") as Label;
                CheckBox cBox = dataItem.FindControl("checkUndetectable") as CheckBox;
                TextBox txtLimit = dataItem.FindControl("textDetectionLimit") as TextBox;
                TextBox txtResultValue = dataItem.FindControl("textResultValue") as TextBox;

                paramId = hParameterId.Value;

                DataRow thisRow = dt.AsEnumerable().FirstOrDefault(r => r["ParameterId"].ToString() == paramId);
                if (thisRow == null)
                {
                    return;
                }
                if (_dataType == "NUMERIC")
                {


                    if (!txtResultValue.ReadOnly)
                    {
                        thisRow["ResultValue"] = txtResultValue.Text;
                        thisRow["UnDetectable"] = cBox.Checked;
                        thisRow["DetectionLimit"] = txtLimit.Text;
                        thisRow["ResultUnitId"] = ddlResultUnit.SelectedValue;

                    }
                }
                else if (_dataType == "TEXT")
                {
                    if (!txtResultValue.ReadOnly) thisRow["ResultText"] = txtResultText.Text;
                }
                else if (_dataType == "SELECTLIST")
                {
                    if (ddlResultList.Visible) { thisRow["ResultOption"] = ddlResultList.SelectedItem.Text; }
                }
                dt.AcceptChanges();
                this.TestParameter = dt;
            }
        }
        void BindTestsParameter()
        {

            DataView dv = this.TestParameter.DefaultView;
            dv.RowFilter = "DeleteFlag  ='False'";
            DataTable theDT = dv.ToTable();

            repeaterResult.DataSource = this.TestParameter;
            repeaterResult.DataBind();

        }
        void PopulateUnits(ref DropDownList ddlControl, int parameterid)
        {
            ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
            List<ParameterResultConfig> config = labMgr.GetParameterConfig(parameterid);
            //  DataTable theDT = labMgr.GetTestParameterResultUnit(parameterid);
            if (config != null)
            {
                ddlControl.Items.Clear();
                ddlControl.ClearSelection();
                ddlControl.Items.Add(new ListItem("Select...", "-1"));
                //  theDT.DefaultView.Sort = "UnitName Asc";
                //  DataTable dt = theDT.DefaultView.ToTable();
                string strDefaultId = "";
                foreach (ParameterResultConfig row in config)
                {
                    ListItem item = (new ListItem(row.UnitName, row.UnitId.ToString()));
                    if (Convert.ToBoolean(row.IsDefault))
                    {
                        strDefaultId = row.UnitId.ToString();
                    }
                    item.Attributes.Add("min", row.MinBoundary.ToString());
                    item.Attributes.Add("max", row.MaxBoundary.ToString());
                    item.Attributes.Add("min_normal", row.MinNormalRange.ToString());
                    item.Attributes.Add("max_normal", row.MaxNormalRange.ToString());
                    item.Attributes.Add("detection_limit", row.DetectionLimit.ToString());
                    item.Attributes.Add("config_id", row.Id.ToString());
                    ddlControl.Items.Add(item);
                }
                if (strDefaultId != "")
                {
                    ListItem item = ddlControl.Items.FindByValue(strDefaultId);
                    if (null != item) item.Selected = true;
                }
            }
        }
        void PopulateSelectList(ref DropDownList ddlControl, int parameterId)
        {
            ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");

            List<ParameterResultOption> options = labMgr.GetParameterResultOption(parameterId);
            if (options != null)
            {
                options.OrderBy(o => o.Text);
                ddlControl.Items.Clear();
                ddlControl.ClearSelection();
                ddlControl.Items.Add(new ListItem("Select...", "-1"));
                // theDT.DefaultView.Sort = "Value Asc";
                // DataTable dt = theDT.DefaultView.ToTable();
                foreach (ParameterResultOption option in options)
                {
                    ListItem item = (new ListItem(option.Text, option.Id.ToString()));
                    ddlControl.Items.Add(item);
                }
            }
        }
        protected void repeaterResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DataRow rowView = (DataRow)e.Item.DataItem;
                string strDataType = rowView["DataType"].ToString().ToUpper();
                string strResultValue = rowView["ResultValue"].ToString();
                string strResultText = rowView["ResultText"].ToString();
                string strLimit = rowView["DetectionLimit"].ToString();
                string strParameterId = rowView["ParameterId"].ToString();

                bool hasResult = (strResultText != "" || strResultValue != "");

                FilteredTextBoxExtender fteLimit = e.Item.FindControl("fteLimit") as FilteredTextBoxExtender;
                FilteredTextBoxExtender fteValue = e.Item.FindControl("fteValue") as FilteredTextBoxExtender;
                DropDownList ddlResultUnit = e.Item.FindControl("ddlResultUnit") as DropDownList;
                DropDownList ddlResultList = e.Item.FindControl("ddlResultList") as DropDownList;
                TextBox txtResultText = e.Item.FindControl("textResultText") as TextBox;
                Label labelResultText = e.Item.FindControl("labelResultText") as Label;
                CheckBox cBox = e.Item.FindControl("checkUndetectable") as CheckBox;
                TextBox txtLimit = e.Item.FindControl("textDetectionLimit") as TextBox;
                TextBox txtResultValue = e.Item.FindControl("textResultValue") as TextBox;


                fteLimit.Enabled = fteValue.Enabled =
                    cBox.Enabled = ddlResultUnit.Enabled =
                        ddlResultList.Enabled = txtResultText.Enabled =
                            txtResultValue.Enabled = hasResult;
                ddlResultList.Visible = txtResultText.Visible = ddlResultUnit.Visible = !hasResult;

                labelResultText.Visible = false;

                if (strDataType == "NUMERIC")
                {

                    if (hasResult)
                    {
                        txtResultValue.Enabled = false;
                    }
                    else
                    {
                        if (null != cBox)
                        {
                            this.InjectScript(ref cBox, ref txtLimit);
                        }
                        //load dropdown;
                        this.PopulateUnits(ref ddlResultUnit, int.Parse(strParameterId));
                    }
                }
                else if (strDataType == "TEXT")
                {
                    labelResultText.Visible = hasResult;
                }
                else if (strDataType == "SELECTLIST")
                {
                    labelResultText.Visible = hasResult;
                    if (!hasResult)
                    {
                        this.PopulateSelectList(ref ddlResultList, int.Parse(strParameterId));
                    }
                }
            }
        }

        protected void btnSaveResult_Click(object sender, EventArgs e)
        {

        }
    }
}