using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Lab;
using Interface.Laboratory;
using System.Configuration;
using System.Collections;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory.Admin
{
    public partial class ManageTestParameter : System.Web.UI.Page
    {
        protected string svDataType = "";
        protected string svGroup = "none";
        protected string svParam = "none";
        private bool isError = false;
        private ILabManager mGr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
  

        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>

        int UserId
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        protected int LabTestId
        {
            get
            {
                if (hLabTestId.Value == "-1") return -1;
                else { return int.Parse(hLabTestId.Value); }
            }
            set
            {
                hLabTestId.Value = value.ToString();
            }
        }

        protected string OpenMode
        {
            get
            {
                return hMode.Value.ToUpper().Trim();
            }
            set
            {
                hMode.Value = value;
            }
        }

        protected int ParameterId
        {
            get
            {
                if (hParameterId.Value == "-1") return -1;
                else { return int.Parse(hParameterId.Value); }
            }
            set
            {
                hParameterId.Value = value.ToString();
            }
        }

        protected void ExitPage(object sender, EventArgs e)
        {
        }

        protected void gridLabUnits_DataBound(object sender, EventArgs e)
        {
           

        }

        protected void gridLabUnits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddItem")
                {
                    GridViewRow row = gridLabUnits.FooterRow;
                    DropDownList ddlNewUnitName = (DropDownList)row.FindControl("ddlNewUnitName");
                    Double? nullDouble = null;
                    if (!validateDropDown(ddlNewUnitName))
                    {
                        ddlNewUnitName.BorderColor = System.Drawing.Color.Red;
                        IQCareMsgBox.NotifyAction("Please select the unit", "Error: Missing Fields", true,this, "");
                        return;
                    }
                    ResultUnit unit = new ResultUnit() { Id = int.Parse(ddlNewUnitName.SelectedValue), Text = ddlNewUnitName.SelectedItem.Text };

                    TextBox texMinBoundary = row.FindControl("txtNewMinBoundaryValue") as TextBox;

                    TextBox txtNewMaxBoundaryValue = row.FindControl("txtNewMaxBoundaryValue") as TextBox;

                    TextBox txtNewMinNormalRange = row.FindControl("txtNewMinNormalRange") as TextBox;

                    TextBox txtNewMaxNormalRange = row.FindControl("txtNewMaxNormalRange") as TextBox;

                    TextBox txtNewDetectionLimit = row.FindControl("txtNewDetectionLimit") as TextBox;

                    bool defaultunit = false;
                    CheckBox ckbNewDefault = row.FindControl("ckbNewDefault") as CheckBox;
                    defaultunit = ckbNewDefault.Checked;
                    List<ParameterResultConfig> _configs = this.UnitConfig;
                    if (defaultunit)
                    {
                        if (_configs.Count > 0)
                        {
                            _configs.ForEach(u =>
                            {
                                u.IsDefault = false;
                            });
                        }
                    }

                    ParameterResultConfig config = null;
                    if (_configs.Count > 0)
                    {
                        config = _configs.DefaultIfEmpty(null).FirstOrDefault(u => u.UnitId == unit.Id);
                    }
                    if (config != null)
                    {
                        config.DeleteFlag = false;
                        config.IsDefault = defaultunit;
                        config.DetectionLimit = txtNewDetectionLimit.Text == "" ? nullDouble : Convert.ToDouble(txtNewDetectionLimit.Text);
                        config.MaxBoundary = txtNewMaxBoundaryValue.Text == "" ? nullDouble : Convert.ToDouble(txtNewMaxBoundaryValue.Text);
                        config.MinBoundary = texMinBoundary.Text == "" ? nullDouble : Convert.ToDouble(texMinBoundary.Text);
                        config.MaxNormalRange = txtNewMaxNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtNewMaxNormalRange.Text);
                        config.MinNormalRange = txtNewMinNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtNewMinNormalRange.Text);
                    }
                    else
                    {
                        config = new ParameterResultConfig()
                        {
                            ParameterId = this.ParameterId,
                            Id = -1,
                            DeleteFlag = false,
                            IsDefault = defaultunit,
                            DetectionLimit = txtNewDetectionLimit.Text == "" ? nullDouble : Convert.ToDouble(txtNewDetectionLimit.Text),
                            MinBoundary = texMinBoundary.Text == "" ? nullDouble : Convert.ToDouble(texMinBoundary.Text),
                            MaxBoundary = txtNewMaxBoundaryValue.Text == "" ? nullDouble : Convert.ToDouble(txtNewMaxBoundaryValue.Text),
                            MinNormalRange = txtNewMinNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtNewMinNormalRange.Text),
                            MaxNormalRange = txtNewMaxNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtNewMaxNormalRange.Text),
                            ResultUnit = unit
                        };
                        _configs.Add(config);

                    };
                    this.UnitConfig = _configs;
                    this.BindParameterConfig();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
        private bool validateDropDown(DropDownList selectList)
        {
            if (selectList.SelectedIndex == -1 || int.Parse(selectList.SelectedValue) < 1)
            {
                selectList.BorderColor = System.Drawing.Color.Red;
                return false;
            }
            selectList.BorderColor = System.Drawing.Color.White;
            return true;

        }
        protected void gridLabUnits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlNewUnitName = (DropDownList)e.Row.FindControl("ddlNewUnitName");

                ddlNewUnitName.DataSource = (IEnumerable<ResultUnit>)base.Session[SessionKey.ResultUnit];
                ddlNewUnitName.DataTextField = "Text";
                ddlNewUnitName.DataValueField = "Id";
                ddlNewUnitName.DataBind();
                ddlNewUnitName.Items.Insert(0, new ListItem("Select", "-1"));
            }
            else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                ParameterResultConfig rowView = (ParameterResultConfig)e.Row.DataItem;
                DropDownList ddEditUnitName = (DropDownList)e.Row.FindControl("ddEditUnitName");
                ddEditUnitName.DataSource = (IEnumerable<ResultUnit>)base.Session[SessionKey.ResultUnit];
                ddEditUnitName.DataTextField = "Text";
                ddEditUnitName.DataValueField = "Id";
                ddEditUnitName.DataBind();
                ddEditUnitName.Items.Insert(0, new ListItem("Select", "-1"));
                ddEditUnitName.SelectedValue = rowView.UnitId.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Manage Test Parameter";
            if (!IsPostBack)
            {
                Session.Remove("PameterResultOption");
                this.PopulateUnits();
                if (Session[SessionKey.SelectedLab] == null)
                {
                    string theUrl = string.Format("{0}", "~/Laboratory/Admin/LabTestMaster.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                else
                {
                    LabTest selectedLab = (LabTest)Session[SessionKey.SelectedLab];
                    this.LabTestId = selectedLab.Id;
                    this.PopulateLabDetails(selectedLab);
                }
                if (Session[SessionKey.TestParameters] == null)
                {
                    this.OpenMode = "NEW";
                    this.BindConfigPlaceHolder();
                }
                else
                {
                    this.OpenMode = "EDIT";
                    TestParameter selectedParam = (TestParameter)Session[SessionKey.TestParameters];
                    ParameterId = selectedParam.Id;
                    this.GetParameterDetails(selectedParam.LabTestId, selectedParam.Id);
                }

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            btnExit.OnClientClick = "javascript:window.location='./TestParameterMaster.aspx'; return false;";

            textReference.Visible = (OpenMode == "NEW");
            labelReference.Visible = (OpenMode == "EDIT");


        }
        List<ParameterResultOption> ParseResultOptions()
        {
            List<ParameterResultOption> persistedOption = this.ParamOptions;

            string optionValues = Request.Form[hOptionsAvailable.UniqueID];
            optionValues = optionValues.Substring(0, optionValues.Length - 1);

            // string values = Request.Form[lboxOptions.UniqueID];
            string[] options = optionValues.Split(':');
            Hashtable ht = new Hashtable();
            foreach (string strOption in options)
            {
                string[] split = strOption.Split('=');
                ht.Add(split[0], ht[1]);
            }
            //  List<ParameterResultOption> optionsToSave = new List<ParameterResultOption>();
            if (persistedOption.Count > 0)
            {
                //find those that have been removed

                foreach (ParameterResultOption option in persistedOption)
                {
                    //  ht.ContainsKey(option.Text);
                    //int index = Array.IndexOf(options, option.Id.ToString());
                    if (!ht.ContainsKey(option.Text))//(index == -1) // not found. must have been deleted
                    {
                        option.DeleteFlag = true;
                    }

                }
            }
            foreach (DictionaryEntry entry in ht)
            {

                if (persistedOption.Count > 0 && persistedOption.Exists(o => o.Text == entry.Key.ToString()))
                {

                }
                else
                {
                    persistedOption.Add(new ParameterResultOption()
                    {
                        Id = -1,
                        Text = entry.Key.ToString(),
                        ParameterId = this.ParameterId,
                        DeleteFlag = false
                    });
                }
            }
           
            return persistedOption;
        }
        protected void SaveLabParameter(object sender, EventArgs e)
        {
            TestParameter parameter = null;
            if (this.OpenMode == "NEW" && this.ParameterId == -1)
            {
                parameter = new TestParameter()
                {
                    Id = -1,
                    LabTestId = this.LabTestId,
                    DeleteFlag = false,
                    DataType = ddlDataType.SelectedValue,
                    LoincCode = textLoincCode.Text,
                    Rank = textRank.Text!=""? Convert.ToDouble(textRank.Text) : 0.00,
                    Name = textParameterName.Text,
                    ReferenceId = textReference.Text,
                    ResultConfig = null,
                    ResultOption = null
                };
                if (ddlDataType.SelectedValue == "NUMERIC")
                {
                    parameter.ResultConfig = this.UnitConfig;
                }
                else if (ddlDataType.SelectedValue == "SELECTLIST")
                {

                    parameter.ResultOption = this.ParseResultOptions();
                }
            }
            else
            {
                parameter = (TestParameter)Session[SessionKey.TestParameters];
                parameter.DataType = ddlDataType.SelectedValue;
                parameter.LoincCode = textLoincCode.Text;
                // parameter.ReferenceId = textReference.Text;
                parameter.LabTestId = this.LabTestId;
                parameter.Name = textParameterName.Text;
                parameter.DeleteFlag = false;
                if (ddlDataType.SelectedValue == "NUMERIC")
                {
                    parameter.ResultConfig = this.UnitConfig;
                }
                else if (ddlDataType.SelectedValue == "SELECTLIST")
                {
                    
                    parameter.ResultOption = this.ParseResultOptions();
                }

            }
            TestParameter result = mGr.SaveLabTestParameter(parameter, this.UserId);

            this.OpenMode = "EDIT";
            IQCareMsgBox.NotifyAction(result.Name + " has been saved successfully", "Success: Save Parameter", false,this, "javascript:window.location='./TestParameterMaster.aspx'; return true;");
            this.BindParameter(result);
            
        }

        private void BindConfigPlaceHolder()
        {
            ParameterResultConfig param = new ParameterResultConfig() { Id = -1, DeleteFlag = true, ParameterId = -1, ResultUnit = new ResultUnit() { Id = -1, Text = "PlaceHolder" } };
            List<ParameterResultConfig> ie = new List<ParameterResultConfig>();
            ie.Add(param);

            gridLabUnits.DataSource = ie;
            gridLabUnits.DataBind();
            gridLabUnits.Rows[0].Visible = false;
        }

        private void PopulateLabDetails(LabTest selectedLab)
        {
            this.LabTestId = selectedLab.Id;
            labelTestName.Text = selectedLab.Name;
        }
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void ShowErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                SystemSetting.LogError(ex);
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Lab Management");
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{

                //}
            }
        }
        List<ParameterResultOption> ParamOptions
        {
            get
            {
                if (base.Session[SessionKey.ResultOptions] == null)
                {
                    return new List<ParameterResultOption>();

                }
                else
                {
                    return (List<ParameterResultOption>)base.Session[SessionKey.ResultOptions];
                }
            }
            set
            {
                base.Session[SessionKey.ResultOptions] = value;
            }
        }
        List<ParameterResultConfig> UnitConfig
        {
            get
            {
                if (base.Session[SessionKey.UnitConfig] == null)
                {
                    return new List<ParameterResultConfig>();

                }
                else
                {
                    return (List<ParameterResultConfig>)base.Session[SessionKey.UnitConfig];
                }
            }
            set
            {
                base.Session[SessionKey.UnitConfig] = value;
            }
        }
        void BindParameterConfig()
        {
            try
            {
                List<ParameterResultConfig> configs = this.UnitConfig;

                if (configs.Count == 0)
                {
                    ParameterResultConfig param = new ParameterResultConfig() { Id = -1, DeleteFlag = true, ParameterId = this.ParameterId };
                    List<ParameterResultConfig> ie = new List<ParameterResultConfig>();
                    ie.Add(param);

                    gridLabUnits.DataSource = ie;
                    gridLabUnits.DataBind();
                    gridLabUnits.Rows[0].Visible = false;
                }
                else
                {
                    this.gridLabUnits.DataSource = configs.Where(c => c.DeleteFlag == false);
                    this.gridLabUnits.DataBind();
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
        protected string svUnits
        {
            get
            {
                return hShowUnits.Value;
            }
            set
            {
                hShowUnits.Value = value.ToLower().Trim();
            }
        }
        protected string svOptions
        {
            get
            {
                return hShowOptions.Value;
            }
            set
            {
                hShowOptions.Value = value.ToLower().Trim();
            }
        }

        private void BindParameter(TestParameter testParameter)
        {
            labelName.Text = textParameterName.Text = testParameter.Name;
            textReference.Text = labelReference.Text = testParameter.ReferenceId;
            ddlDataType.ClearSelection();
            ddlDataType.SelectedValue = testParameter.DataType;
            labelDataType.Text = ddlDataType.SelectedItem.Text;
            textLoincCode.Text = labelLoincCode.Text = testParameter.LoincCode;
            ParameterId = testParameter.Id;
            if (testParameter.DataType == "SELECTLIST")
            {
                if (null == testParameter.ResultOption || testParameter.ResultOption.Count() == 0)
                {

                }
                else
                {
                    this.ParamOptions = testParameter.ResultOption;
                    foreach (ParameterResultOption option in testParameter.ResultOption)
                    {
                        lboxOptions.Items.Add(new ListItem(option.Text, option.Id.ToString()));
                    }
                }
                this.svUnits = "none";
                this.svOptions = "";
            }
            else if (testParameter.DataType == "NUMERIC")
            {

                if (null == testParameter.ResultConfig || testParameter.ResultConfig.Count() == 0)
                {

                }
                else
                {
                    this.UnitConfig = testParameter.ResultConfig;

                }
                this.svUnits = "";
                this.svOptions = "none";
                this.BindParameterConfig();
            }
        }
        private void GetParameterDetails(int LabTestId, int parameterId)
        {
            TestParameter testParameter = mGr.GetLabTestParameterById(LabTestId, parameterId);
            ParameterId = testParameter.Id;
            if (testParameter.DataType == "SELECTLIST")
            {
                testParameter.ResultOption = mGr.GetParameterResultOption(testParameter.Id);
                this.ParamOptions = testParameter.ResultOption;
                foreach (ParameterResultOption option in testParameter.ResultOption)
                {
                    lboxOptions.Items.Add(new ListItem(option.Text, option.Id.ToString()));
                }
                this.svUnits = "none";
                this.svOptions = "";
            }
            else if (testParameter.DataType == "NUMERIC")
            {
                testParameter.ResultConfig = mGr.GetParameterConfig(testParameter.Id);
                if (null == testParameter.ResultConfig || testParameter.ResultConfig.Count() == 0)
                {

                }
                else
                {
                    this.UnitConfig = testParameter.ResultConfig;

                }
                this.svUnits = "";
                this.svOptions = "none";
                this.BindParameterConfig();
            }


            labelName.Text = textParameterName.Text = testParameter.Name;
            textReference.Text = labelReference.Text = testParameter.ReferenceId;
            ddlDataType.ClearSelection();
            ddlDataType.SelectedValue = testParameter.DataType;
            textRank.Text = testParameter.Rank.ToString();
            labelDataType.Text = ddlDataType.SelectedItem.Text;
            textLoincCode.Text = labelLoincCode.Text = testParameter.LoincCode;
        }

        private void PopulateUnits()
        {

            List<ResultUnit> units = mGr.GetResultUnits();
            base.Session[SessionKey.ResultUnit] = units;

        }

        protected void gridLabUnits_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridLabUnits.EditIndex = e.NewEditIndex;
            this.BindParameterConfig();
        }

        protected void gridLabUnits_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridLabUnits.EditIndex = -1;
            this.BindParameterConfig();
        }

        protected void gridLabUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                List<ParameterResultConfig> configs = this.UnitConfig;

                ParameterResultConfig _config = configs.ElementAt(e.RowIndex);


                if (_config.Id > 0)
                {
                    _config.DeleteFlag = true;

                }
                else
                {
                    configs.RemoveAt(e.RowIndex);
                }
                this.UnitConfig = configs;
                this.BindParameterConfig();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
       
        protected void gridLabUnits_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                List<ParameterResultConfig> configs = this.UnitConfig;
                GridViewRow rowView = gridLabUnits.Rows[e.RowIndex];
                ParameterResultConfig config = configs.ElementAt(e.RowIndex);
                if (config.Id < 1)
                {
                    configs.Remove(config);
                }
                DropDownList ddEditUnitName = rowView.FindControl("ddEditUnitName") as DropDownList;
                if (ddEditUnitName.SelectedValue == "-1")
                {
                    e.Cancel = true;
                    ddEditUnitName.BorderColor = System.Drawing.Color.Red;
                    IQCareMsgBox.NotifyAction("Please select the unit", "Error: Missing Fields", true,this, "");
                    return;
                }
                {
                    var duplicate = configs.DefaultIfEmpty(null).FirstOrDefault(u =>
                        u.UnitId == Convert.ToInt32(ddEditUnitName.SelectedValue) &&
                        u.DeleteFlag == false &&
                        u.Id != config.Id);
                    if (null != duplicate)
                    {
                        e.Cancel = true;
                        ddEditUnitName.BorderColor = System.Drawing.Color.Red;
                        IQCareMsgBox.NotifyAction("Please select the unit", "Error: Duplicate Unit", true, this, "");
                        return;
                    }

                }
                TextBox txtEditMinBoundaryValue = rowView.FindControl("txtEditMinBoundaryValue") as TextBox;
                TextBox txtEditMaxBoundaryValue = rowView.FindControl("txtEditMaxBoundaryValue") as TextBox;
                TextBox txtEditMinNormalRange = rowView.FindControl("txtEditMinNormalRange") as TextBox;
                TextBox txtEditMaxNormalRange = rowView.FindControl("txtEditMaxNormalRange") as TextBox;
                TextBox txtEditDetectionLimit = rowView.FindControl("txtEditDetectionLimit") as TextBox;
                CheckBox ckbDefault = rowView.FindControl("ckbDefault") as CheckBox;

                Double? nullDouble = null;

                if (ckbDefault.Checked)
                {
                    if (configs.Count > 0)
                    {
                        configs.ForEach(u =>
                        {
                            u.IsDefault = false;
                        });
                    }
                }
                // config.DeleteFlag = true;
                configs.Add(new ParameterResultConfig()
                {
                    Id = -1,
                    ParameterId = this.ParameterId,
                    IsDefault = ckbDefault.Checked,
                    DeleteFlag = false,
                    DetectionLimit = txtEditDetectionLimit.Text == "" ? nullDouble : Convert.ToDouble(txtEditDetectionLimit.Text),
                    MinBoundary = txtEditMinBoundaryValue.Text == "" ? nullDouble : Convert.ToDouble(txtEditMinBoundaryValue.Text),
                    MaxBoundary = txtEditMaxBoundaryValue.Text == "" ? nullDouble : Convert.ToDouble(txtEditMaxBoundaryValue.Text),
                    MinNormalRange = txtEditMinNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtEditMinNormalRange.Text),
                    MaxNormalRange = txtEditMaxNormalRange.Text == "" ? nullDouble : Convert.ToDouble(txtEditMaxNormalRange.Text),
                    ResultUnit = new ResultUnit() { Id = Convert.ToInt32(ddEditUnitName.SelectedValue), Text = ddEditUnitName.SelectedItem.Text }
                });
                this.UnitConfig = configs;
                gridLabUnits.EditIndex = -1;
                this.BindParameterConfig();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
    }
}