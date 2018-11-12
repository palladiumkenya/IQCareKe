using AjaxControlToolkit;
using Application.Common;
using Application.Presentation;
using Entities.Lab;
using Entities.Lookup;
using Interface.Clinical;
using Interface.Laboratory;
using Interface.Lookup;
using IQCare.IQControl;
using IQCare.Web.UILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BasePage" />
    /// <seealso cref="System.Web.UI.ICallbackEventHandler" />
    public partial class CustomForm_v2 : BasePage, ICallbackEventHandler
    {
        /// <summary>
        /// The htcontrolstatus
        /// </summary>
        public static Hashtable htcontrolstatus = new Hashtable();
        List<RequiredField> requiredField = new List<RequiredField>();
        class RequiredField
        {
            public string FieldId { get; set; }
            public string SectionId { get; set; }
            public bool IsGrid { get; set; }
        }
        /// <summary>
        /// The current reg dt
        /// </summary>
        public DataTable theCurrentRegDT;
        /// <summary>
        /// The regimen
        /// </summary>
        public DataTable theRegimen;
        /// <summary>
        /// The _table name
        /// </summary>
        private string _tableName="";
        /// <summary>
        /// The arl header
        /// </summary>
        private ArrayList ARLHeader = new ArrayList();
        /// <summary>
        /// The arl multi select
        /// </summary>
        private ArrayList ARLMultiSelect = new ArrayList();
        /// <summary>
        /// The automatic dt
        /// </summary>
        private DataTable customFieldData = new DataTable();
        /// <summary>
        /// The automatic dt pre
        /// </summary>
        private DataTable visitDetail = new DataTable();
        /// <summary>
        /// The bmi identifier
        /// </summary>
        private string BmiID;
        /// <summary>
        /// The div custom item
        /// </summary>
        private Panel DIVCustomItem = new Panel();
        /// <summary>
        /// The drug type
        /// </summary>
        private int DrugType, RegimenType;
        /// <summary>
        /// The GBL dt grid view controls
        /// </summary>
        private DataTable gblDTGridViewControls = new DataTable();
        /// <summary>
        /// The is height avail
        /// </summary>
        private bool IsHeightAvail = false;

        //todo
        /// <summary>
        /// The is s ingle visit
        /// </summary>
        private bool IsSingleVisit = false;

        /// <summary>
        /// The is weight avail
        /// </summary>
        private bool IsWeightAvail = false;
        /// <summary>
        /// The object factory parameter
        /// </summary>
        private string objFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
        /// <summary>
        /// </summary>
        private string str, strCallback;
        /// <summary>
        /// The tabcontainer
        /// </summary>
        private TabContainer tabContainer = new TabContainer();
        /// <summary>
        /// The tb child panel
        /// </summary>
        private TabPanel tbChildPanel;
        /// <summary>
        /// The conditional
        /// </summary>
        private bool theConditional;
        /// <summary>
        /// The ds labs
        /// </summary>
        private DataSet theDSLabs;
        /// <summary>
        /// The DSXML
        /// </summary>
        private DataSet theDSXML = new DataSet();
        /// <summary>
        /// The second label conditional
        /// </summary>
        private bool theSecondLabelConditional;

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Removecontrolstatuses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static void removecontrolstatus(string id)
        {
            if (htcontrolstatus.ContainsKey(id))
            {
                htcontrolstatus.Remove(id);
            }
        }

        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Sets the contrl status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static void SetContrlStatus(string id)
        {
            if (!(htcontrolstatus.ContainsKey(id)))
            {
                htcontrolstatus.Add(id, id);
            }
        }

        /// <summary>
        /// Adds the contol staus in hast table.
        /// </summary>
        /// <param name="controlid">The controlid.</param>
        public void AddContolStausInHastTable(string controlid)
        {
            if (!(htcontrolstatus.ContainsKey(controlid)))
            {
                htcontrolstatus.Add(controlid, controlid);
            }
        }

        /// <summary>
        /// Dtgridviews the specified dt.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public DataTable dtgridview(DataTable dt)
        {
            DataTable btable = new DataTable();
            btable.Columns.Add("FeatureID", typeof(string));
            btable.Columns.Add("FieldName", typeof(string));
            btable.Columns.Add("IsGridView", typeof(string));
            btable.Columns.Add("SectionID", typeof(string));
            btable.Columns.Add("Fieldlabel", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                DataRow theDR = btable.NewRow();
                theDR["FeatureID"] = r["FeatureID"].ToString();
                theDR["FieldName"] = r["FieldName"].ToString();
                theDR["IsGridView"] = r["IsGridView"].ToString();
                theDR["SectionID"] = r["SectionID"].ToString();
                theDR["Fieldlabel"] = r["Fieldlabel"].ToString();

                btable.Rows.Add(theDR);
            }
            return btable;
        }

        /// <summary>
        /// Findcolumnfieldnames the specified dt.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="fieldlabel">The fieldlable.</param>
        /// <returns></returns>
        public string findcolumnfieldname(DataTable dt, string fieldlabel)
        {
            string strcolumname = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (fieldlabel.ToUpper() == dt.Rows[i]["Fieldlabel"].ToString().ToUpper())
                {
                    strcolumname = "[" + dt.Rows[i]["FieldName"].ToString() + "]";
                }
            }
            return strcolumname;
        }

        /// <summary>
        /// Gets the grid table.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public DataTable GetGridTable(DataTable dt)
        {
            DataTable btable = new DataTable();
            btable.Columns.Add("FeatureID", typeof(string));
            btable.Columns.Add("FeatureName", typeof(string));
            btable.Columns.Add("IsGridView", typeof(string));
            btable.Columns.Add("SectionID", typeof(string));
            btable.Columns.Add("SectionName", typeof(string));
            btable.Columns.Add("SectionInfo", typeof(string));
            foreach (DataRow r in dt.Select("IsGridView = 1"))
            {
                DataRow theDR = btable.NewRow();
                theDR["FeatureID"] = r["FeatureID"].ToString();
                theDR["FeatureName"] = r["FeatureName"].ToString();
                theDR["IsGridView"] = r["IsGridView"].ToString();
                theDR["SectionID"] = r["SectionID"].ToString();
                theDR["SectionName"] = r["SectionName"].ToString();
                theDR["SectionInfo"] = r["SectionInfo"].ToString();
                btable.Rows.Add(theDR);
            }
            return btable;
        }

        /// <summary>
        /// HTMLs the CheckBox select.
        /// </summary>
        /// <param name="theObj">The object.</param>
        public void HtmlCheckBoxSelect(object theObj)
        {
            TabContainer container = tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            CheckBox theButton = ((CheckBox)theObj);
            string[] theControlId = theButton.ID.ToString().Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Checked == true)
                theValue = 1;
            else
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[17].Rows)
            {
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in c.Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if (theIdent.Length > 2)
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    //((TextBox)x).Enabled = true;
                                                    //ApplyBusinessRules(x, "1", true);
                                                    //ApplyBusinessRules(x, "2", true);
                                                    //ApplyBusinessRules(x, "3", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    //((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }
                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    //((DropDownList)x).Enabled = true;
                                                    //ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "AM";
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Button)x).Enabled = true;
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theValue.ToString() == "0")
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((Button)x).Enabled = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// HTMLs the RadioButton select.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void HtmlRadioButtonSelect(object sender)
        {
            TabContainer container = tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
            string[] theControlId = theButton.ID.Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Value == "Yes" && theButton.Checked == true)
                theValue = 1;
            else if (theButton.Value == "Yes" && theButton.Checked == false)
                theValue = 0;
            if (theButton.Value == "No" && theButton.Checked == true)
                theValue = 2;
            else if (theButton.Value == "No" && theButton.Checked == false)
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[17].Rows)
            {
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in c.Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if (theIdent.Length > 2)
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    // ((TextBox)x).Enabled = true;
                                                    // ApplyBusinessRules(x, "1", true);
                                                    // ApplyBusinessRules(x, "2", true);
                                                    // ApplyBusinessRules(x, "3", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    //((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }
                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    if (((DropDownList)x).ID.Contains("AMPM") == false)
                                                    {
                                                        ((DropDownList)x).SelectedValue = "0";
                                                    }
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                            }
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] + theIdent[1] + theIdent[2] == "Pnl_" + theDR["PdfTableName"].ToString() + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Button)x).Enabled = true;
                                                    ViewState["btnlabisEnable"] = "1";
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ViewState["btnlabisEnable"] = "2";
                                                    //ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the contol staus in hast table.
        /// </summary>
        /// <param name="controlid">The controlid.</param>
        public void RemoveContolStausInHastTable(string controlid)
        {
            if (htcontrolstatus.ContainsKey(controlid))
            {
                htcontrolstatus.Remove(controlid);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Click event of the btncomplete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btncomplete_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            int tabId = Convert.ToInt32(ID[1]);
            string PrevTabId = hdnPrevTabId.Value;
            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            string SaveTabData = hdnSaveTabData.Value;
            TabContainer container = tabContainer;
            ConFieldEnableDisable(container);
            Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, tabId));
            theDS.Tables.Add(ReadARVMedicationTable(container, tabId));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, tabId));

            if (FieldValidation() == false)
            { return; }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                //int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(PatientId, theDS, 0, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                SaveCancel();
            }
            else if (Request.QueryString["Name"] == "Delete" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
              //  int PatientID = Convert.ToInt32(Session["PatientId"]);
                int visitId = Convert.ToInt32(Session["PatientVisitId"]);
                DeleteForm(PatientId, visitId);
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int featureId = Convert.ToInt32(Session["FeatureID"]);
              //  int PatientID = Convert.ToInt32(Session["PatientId"]);
                int visitId = Convert.ToInt32(Session["PatientVisitId"]);
             //   int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(PatientId, featureId, visitId, LocationId, theDS, 0, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                UpdateCancel();
            }
        }

        /// <summary>
        /// Creates the bound column.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        protected BoundField CreateBoundColumn(DataColumn c)
        {
            BoundField column = new BoundField();

            column.DataField = c.ColumnName;
            column.HeaderText = c.ColumnName.Replace("_", " ");
            column.DataFormatString = setFormating(c);

            column.ItemStyle.CssClass = "textstyle";
            return column;
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //So that when the user clicks on the row - the corresponding row is edited
            GridView gv = sender as GridView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] secname = (sender as GridView).ID.ToString().Split('_'); ;

                string secid = secname[1];
                if (ViewState["GridCache_" + secid] != null)
                {
                    int columncount = ((DataTable)ViewState["GridCache_" + secid]).Columns.Count;

                    for (int i = 0; i < gv.Columns.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex.ToString()));
                    }
                }
                else
                {
                }
            }
        }

        /// <summary>
        /// Handles the RowDeleted event of the grdView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void grdView_RowDeleted(object sender, GridViewDeleteEventArgs e)
        {
            string[] secname = (sender as GridView).ID.ToString().Split('_'); ;
            string secid = secname[1];

            DataTable dtviewstate = (DataTable)ViewState["GridCache_" + secid];
            dtviewstate.Rows.RemoveAt(e.RowIndex);
            TabContainer container = tabContainer;
            BindGridView(secid, container, (DataTable)ViewState["GridCache_" + secid]);
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the objdView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void objdView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TabContainer container = tabContainer;
            string[] secname = (sender as GridView).ID.ToString().Split('_'); ;
            string secid = secname[1];

            int thePage = (sender as GridView).PageIndex;
            int thePageSize = (sender as GridView).PageSize;

            GridViewRow theRow = (sender as GridView).Rows[e.NewSelectedIndex];
            theRow.BackColor = System.Drawing.Color.AliceBlue;
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            DataTable theDT = new DataTable();
            theDT = (DataTable)ViewState["GridCache_" + secid];

            foreach (object obj in container.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object ctl in c.Controls)
                            {
                                string btnname = "BtnAdd-" + secid.ToString();
                                if (ctl != null)
                                {
                                    if (ctl is Button)
                                    {
                                        if (((Button)ctl).ID.ToString() == btnname)
                                        {
                                            ((Button)ctl).Text = "Update";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //foreach (Control ctl in DIVCustomItem.Controls)
            //{
            //}

            int r = theIndex;
            //Fill data in Textboxes from grid
            //Edit the selected row
            DataTable theGVSectionControl = ((DataTable)(ViewState["gblDTGridViewControls"])).DefaultView.ToTable(true, "FieldName", "FieldLabel", "SectionId").Copy();
            DataView theDVSectionControl = new DataView(theGVSectionControl);
            theDVSectionControl.RowFilter = "SectionId=" + secid;
            if (theDT.Rows.Count > 0)
            {
                for (int i = 0; i < theDVSectionControl.Count; i++)
                {
                    setGridViewSectionControl(container, theDT, theIndex, theDVSectionControl[i]["FieldName"].ToString(), theDVSectionControl[i]["FieldLabel"].ToString());
                }
                ViewState["SaveFlag_" + secid] = "Edit";
                ViewState["SelectedRow_" + secid] = theIndex;
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
        }
        /// <summary>
        /// Gets the visit identifier.
        /// </summary>
        /// <value>
        /// The visit identifier.
        /// </value>
        private int VisitId
        {
            get
            {
                return (Convert.ToInt32(Session["PatientVisitId"]) > 0) ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            }
        }
        /// <summary>
        /// Gets the feature identifier.
        /// </summary>
        /// <value>
        /// The feature identifier.
        /// </value>
        private int FeatureId
        {
            get
            {
                return Convert.ToInt32(Session["FeatureID"]);
            }
        }
      
        private int ModuleId
        {
            get
            {
                return Convert.ToInt32(Session["TechnicalAreaId"]);
            }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                LoadPredefinedLabel_Field(this.FeatureId, this.PatientId);
                GetICallBackFunction();
                DataSet dsvisit = new DataSet();
                dsvisit = (DataSet)Session["AllData"];
                if (Convert.ToInt32(dsvisit.Tables[14].Rows[0]["MultiVisit"]) == 1)
                {
                    OnBlur();
                }

                if (IsPostBack != true)
                {
                    this.tabContainer.OnClientActiveTabChanged = "ValidateSave";
                    hdnCurrentTabId.Value = this.tabContainer.ActiveTab.ID;
                    hdnPrevTabId.Value = this.tabContainer.ActiveTab.ID;
                    hdnCurrenTabName.Value = this.tabContainer.ActiveTab.HeaderText;
                    hdnPrevTabName.Value = this.tabContainer.ActiveTab.HeaderText;
                    ViewState["ActiveTabIndex"] = this.tabContainer.ActiveTabIndex;
                    hdnPrevTabIndex.Value = Convert.ToString(this.tabContainer.ActiveTabIndex);
                    hdnCurrenTabIndex.Value = Convert.ToString(this.tabContainer.ActiveTabIndex);
                    Session["SaveFlag"] = "Add";
                    /* if (ViewState["LabRanges"] == null)
                     {
                         ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                         theDSLabs = LabResultManager.GetLabValues();
                         ViewState["LabRanges"] = theDSLabs;
                         ViewState["LabMaster"] = theDSLabs.Tables[2];
                     }   */
                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {
                        ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
                        DataSet theDS = MgrValidate.Validate(lblFeatureName.Text, "01-01-1900", this.PatientId,this.ModuleId);
                        AuthenticationRight(FeatureId, "Add");
                        hdnVisitId.Value = "0";
                    }
                    else if (Request.QueryString["Name"] == "Delete" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        btnsave.Visible = true;
                        btnsave.Text = "Delete";
                        foreach (DataRow theDRBindValue in dsvisit.Tables[23].Rows)
                        {

                            BindValue(this.PatientId, this.VisitId, this.LocationId, this.tabContainer, dsvisit.Tables[23]);
                        }
                        AuthenticationRight(this.FeatureId, "Delete");
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["FormName"] = "This";
                        IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnsave);
                    }
                    else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        hdnVisitId.Value = this.VisitId.ToString();
                        BindValue(this.PatientId, this.VisitId, this.LocationId, this.tabContainer, dsvisit.Tables[23]);

                        AuthenticationRight(this.FeatureId, "Edit");
                    }

                    txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                    txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); SendCodeName('" + txtvisitDate.ClientID + "');");
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical >>";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = formDescription.InnerText;
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = formDescription.InnerText;

                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                    Session["PtnRegCTC"] = "";
                    //Drug Data
                    Session["CustomfrmDrug"] = "CustomfrmDrug";
                    Session["CustomfrmLab"] = "CustomfrmLab";
                    //  BindDropdown(ddSignature);
                    BindUserDropdown(ref ddSignature, "");
                    ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();// +err.StackTrace + err.Data.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            TabContainer container = tabContainer;
            /////HTML Controls PostBack//////
            ConFieldEnableDisable(container);
        }

        /// <summary>
        /// Apostrophes the handler.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string apostropheHandler(string input)
        {
            return input.Replace("'", "''");
        }

        /// <summary>
        /// Applies the business rules.
        /// </summary>
        /// <param name="theControl">The control.</param>
        /// <param name="controlId">The control identifier.</param>
        /// <param name="theConField">if set to <c>true</c> [the con field].</param>
        private void ApplyBusinessRules(object theControl, string controlId, bool theConField)
        {
            try
            {
                //bool isDateSet = false;
                tabContainer.ID = "TAB";
                DataTable theDT = (DataTable)ViewState["BusRule"];
                string max = "", min = "", column = "";//, AgeFrom = "", AgeTo = "";
                string maxNormalval = "", minNormalVal = "";
                bool theEnable = theConField;
                string[] field, wihifield;
                if (controlId == "9")
                {
                    field = ((Control)theControl).ID.Split('_');
                }
                else
                {
                    field = ((Control)theControl).ID.Split('-');
                }
                foreach (DataRow DR in theDT.Rows)
                {
                    if (field[0] == "Pnl")
                    {
                        string[] PnlBus;
                        int splitlength = ((Control)theControl).ID.Split('_').Length - 1;
                        PnlBus = field[splitlength].Split('-');

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "14"
                            && Session["PatientSex"].ToString() != "Male")
                            theEnable = false;

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "15"
                            && Session["PatientSex"].ToString() != "Female")
                            theEnable = false;

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != DBNull.Value) && (DR["Value1"] != DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                    theEnable = false;
                            }
                        }
                    }
                    else if (field[0] == "BtnLab")
                    {
                        if (field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() != "Male")
                        {
                            theEnable = false;
                        }

                        if (field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() != "Female")
                        {
                            theEnable = false;
                        }

                        if (field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != DBNull.Value) && (DR["Value1"] != DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                {
                                    theEnable = false;
                                }
                            }
                        }
                    }
                    else if (field[0].ToUpper() == "TXTDT")
                    {
                        // Date format for like "MMM-yyyy"
                        //if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "21")
                        //{
                        //    //isDateSet = true;
                        //    TextBox theDateText = (TextBox)theControl;
                        //    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
                        //    theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4')");
                        //    DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(MMM-YYYY)</span>"));
                        //}
                    }
                    else
                    {
                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "2")
                        {
                            max = Convert.ToString(DR["Value"]);
                            column = Convert.ToString(DR["FieldLabel"]);
                        }
                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "3")
                        {
                            min = Convert.ToString(DR["Value"]);
                        }
                        // todo
                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "26")
                        {
                            maxNormalval = Convert.ToString(DR["Value"]);
                        }
                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "27")
                        {
                            minNormalVal = Convert.ToString(DR["Value"]);
                        }

                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"])
                            && Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() != "Male")
                            theEnable = false;

                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"])
                        && Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() != "Female")
                            theEnable = false;

                        if (field[1] == Convert.ToString(DR["FieldName"]) && field[2] == Convert.ToString(DR["TableName"]) && field[3] == Convert.ToString(DR["FieldId"])
                        && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                            {
                            }
                            else
                                theEnable = false;
                        }
                    }
                }

                if (theControl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    field = ((Control)theControl).ID.Split('_');
                    wihifield = field[0].Split('-');
                    TextBox tbox = (TextBox)theControl;
                    //tbox.Enabled = theEnable;
                    if (controlId == "1")
                    { }
                    else if (controlId == "2" && field[0] == "TXT")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "')");
                        }
                    }
                    else if (controlId == "3" && field[0] == "TXTNUM")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "')");
                        }
                    }
                    else if (controlId == "5" && field[0] == "TXTDT")
                    {
                        tbox.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                        tbox.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                    }
                    if (max != "" && min != "" && maxNormalval != "" && minNormalVal != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            if (wihifield[1].ToUpper() == "HEIGHT" || wihifield[1].ToUpper() == "WEIGHT")
                            {
                                tbox.Attributes.Add("onblur", "CalcualteBMIGet(); isCheckNormal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + column + "', '" + min + "', '" + max + "', '" + minNormalVal + "', '" + maxNormalval + "')");
                            }
                            else
                                tbox.Attributes.Add("onblur", "isCheckNormal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + column + "', '" + min + "', '" + max + "', '" + minNormalVal + "', '" + maxNormalval + "')");
                        }
                    }
                    else if (max != "" && min != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "isBetween('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + column + "', '" + min + "', '" + max + "')");
                        }
                    }
                    else if (max != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "checkMax('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + column + "', '" + max + "')");
                        }
                    }
                    else if (min != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "checkMin('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + column + "', '" + min + "')");
                        }
                    }

                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    //DropDownList ddList = (DropDownList)theControl;
                    //ddList.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    CheckBox Multichk = (CheckBox)theControl;
                    Multichk.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    HtmlInputRadioButton Rdobtn = (HtmlInputRadioButton)theControl;
                    //Rdobtn.Visible = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Image")
                {
                    Image img = (Image)theControl;
                    img.Visible = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                {
                    Panel pnl = (Panel)theControl;
                    pnl.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Button")
                {
                    Button tbtn = (Button)theControl;
                    tbtn.Enabled = theEnable;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Arvs the drug.
        /// </summary>
        /// <returns></returns>
        private DataTable ARVDrug()
        {
            DataTable dtARVDrug = new DataTable();
            dtARVDrug.Columns.Add("DrugId", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("GenericID", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("Dose", Type.GetType("System.String"));
            dtARVDrug.Columns.Add("FrequencyID", Type.GetType("System.String"));
            dtARVDrug.Columns.Add("Duration", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("QtyPrescribed", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("QtyDispensed", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("ARFinance", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
            return dtARVDrug;
        }

        /// <summary>
        /// Authentications the right.
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="mode">The mode.</param>
        private void AuthenticationRight(int featureId, string mode)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(featureId, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (mode == "Add")
            {
                if (Authentication.HasFunctionRight(featureId, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (mode == "Edit")
            {
                if (Authentication.HasFunctionRight(featureId, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (mode == "Delete")
            {
                if (Authentication.HasFunctionRight(featureId, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Autopopulates the hiddenvalue.
        /// </summary>
        /// <param name="str">The string.</param>
        private void AutopopulateHiddenvalue(string str)
        {
            if (str != "")
            {
                DataView AutoDV = new DataView(customFieldData);
                AutoDV.RowFilter = "visitdate < " + Convert.ToDateTime(str);
            }
        }
        /// <summary>
        /// The request MGR
        /// </summary>
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
        /// <summary>
        /// The lab MGR
        /// </summary>
        private ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");

        /// <summary>
        /// Binds the custom controls.
        /// </summary>
        /// <param name="test">The test.</param>
        void BindCustomControls(LabTest test)
        {
            LabTestGroup group = labMgr.GetGroupLabTest(test.Id);

            List<TestParameter> testParameters = labMgr.GetLabTestParameters(test.Id);
            if (null != group && null != group.ComponentTest)
            {
                foreach (LabTest _t in group.ComponentTest)
                {
                    List<TestParameter> _p = labMgr.GetLabTestParameters(_t.Id);
                    if (null != _p)
                    {
                        testParameters.AddRange(_p);
                    }
                }
            }
            foreach (TestParameter param in testParameters)
            {
                this.AddLabControls(param);
            }

        }
        /// <summary>
        /// Adds the lab controls.
        /// </summary>
        /// <param name="param">The parameter.</param>
        void AddLabControls(TestParameter param)
        {
            Panel thePnl = new Panel();
            thePnl.ID = "pnl-Lab-" + param.Id.ToString();
            thePnl.Height = 20;
            thePnl.Width = 850;
            thePnl.Controls.Clear();

            Label theSpace = new Label();
            theSpace.ID = "theSpaceLab" + param.Id.ToString();
            theSpace.Width = 5;
            theSpace.Text = "";
            thePnl.Controls.Add(theSpace);

            Label theParamName = new Label();
            theParamName.ID = "theNameLab" + param.Id.ToString();
            theParamName.Width = 400; //140;
            theParamName.Text = param.Name;
            thePnl.Controls.Add(theParamName);

            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2Lab" + param.Id.ToString();
            theSpace2.Width = 20;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);
            if (param.DataType == "TEXT")
            {
                TextBox theLabResult = new TextBox();
                theLabResult.ID = "LabResult" + param.Id.ToString();
                theLabResult.Width = 320;
                thePnl.Controls.Add(theLabResult);
            }
            else if (param.DataType == "NUMERIC")
            {
                TextBox theLabResult = new TextBox();
                theLabResult.ID = "LabResult" + param.Id.ToString();
                theLabResult.Width = 120;
                thePnl.Controls.Add(theLabResult);

                List<ParameterResultConfig> config = labMgr.GetParameterConfig(param.Id);

                theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "');");


            }
            else if (param.DataType == "SELECTLIST")
            {
                List<ParameterResultOption> options = labMgr.GetParameterResultOption(param.Id);

                DropDownList theddlLabResult = new DropDownList();
                theddlLabResult.ID = "ddlLabResult" + param.Id.ToString();
                theddlLabResult.Width = 120;
                theddlLabResult.DataValueField = "Id";
                theddlLabResult.DataTextField = "Text";
                theddlLabResult.DataSource = options;
                theddlLabResult.DataBind();
                theddlLabResult.Items.Insert(0, "Select");
                thePnl.Controls.Add(theddlLabResult);
            }
            else
            {
                return;
            }
            Label theSpace3 = new Label();
            theSpace3.ID = "theSpace3Lab" + param.Id.ToString();
            theSpace3.Width = 20;
            theSpace3.Text = "";
            thePnl.Controls.Add(theSpace3);

            Label theTestId = new Label();
            theTestId.ID = "lblTestIdLab" + param.Id.ToString() + "=" + param.LabTestId.ToString();
            theTestId.Text = "";
            thePnl.Controls.Add(theTestId);
            DIVCustomItem.Controls.Add(thePnl);

        }
        /// <summary>
        /// Binds the custom controls.
        /// </summary>
        /// <param name="theDR">The dr.</param>
        private void BindCustomControls(DataRow theDR)
        {
            try
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl-Lab-" + theDR["SubTestId"].ToString();
                thePnl.Height = 20;
                thePnl.Width = 850;
                thePnl.Controls.Clear();

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpaceLab" + theDR["SubTestId"].ToString();
                theSpace.Width = 5;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);

                ////////////////////
                Label theTestName = new Label();
                theTestName.ID = "theNameLab" + theDR["SubTestId"].ToString();
                theTestName.Width = 400; //140;
                theTestName.Text = theDR["SubTestName"].ToString();
                thePnl.Controls.Add(theTestName);

                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2Lab" + theDR["SubTestId"].ToString();
                theSpace2.Width = 20;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);

                if (ViewState["LabRanges"] == null)
                    ViewState["LabRanges"] = theDSLabs;

                DataSet theDSselectList = (DataSet)ViewState["LabRanges"];
                DataView theDVselectList = new DataView(theDSselectList.Tables[1]);
                theDVselectList.RowFilter = "SubTestId = " + theDR["SubTestId"].ToString();
                if (theDVselectList.Count != 0)
                {
                    DropDownList theddlLabResult = new DropDownList();
                    theddlLabResult.ID = "ddlLabResult" + theDR["SubTestId"].ToString();
                    theddlLabResult.Width = 120;
                    for (int i = 0; i < theDVselectList.Count; i++)
                    {
                        theddlLabResult.Items.Add(theDVselectList[i].Row["Result"].ToString());
                    }
                    theddlLabResult.Items.Insert(0, "Select");
                    thePnl.Controls.Add(theddlLabResult);
                }
                else
                {
                    TextBox theLabResult = new TextBox();
                    theLabResult.ID = "LabResult" + theDR["SubTestId"].ToString();
                    theLabResult.Width = 120;
                    thePnl.Controls.Add(theLabResult);

                    DataView theDV = new DataView(theDSselectList.Tables[0]);

                    //  theDV.RowFilter = "SubTestNameLab = '" + theDR["SubTestName"].ToString() + "'";
                    theDV.RowFilter = "SubTestName = '" + theDR["SubTestName"].ToString() + "'";
                    if (theDV.Count != 0)
                    {
                        theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "'); AddBoundary('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                        theLabResult.Attributes.Add("onblur", "CheckValue('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    }
                }

                ////////////Space////////////////////////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3Lab" + theDR["SubTestId"].ToString();
                theSpace3.Width = 20;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);

                Label theTestId = new Label();
                theTestId.ID = "lblTestIdLab" + theDR["SubTestId"].ToString() + "=" + theDR["LabTestId"].ToString();
                theTestId.Text = "";
                thePnl.Controls.Add(theTestId);
                DIVCustomItem.Controls.Add(thePnl);
            }
            catch { throw; }
            finally { }
        }

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <value>
        /// The employee list.
        /// </value>
        private DataTable EmployeeList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Mst_Employee");
                if (theDS.Tables["Mst_Employee"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                    if (theDV.Table != null)
                    {
                        dt = theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <value>
        /// The user list.
        /// </value>
        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Users");
                if (theDS.Tables["Users"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Users"]);
                    if (theDV.Table != null)
                    {
                        dt = theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }
        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }
        /// <summary>
        /// Binds the user dropdown.
        /// </summary>
        /// <param name="dropDown">The drop down.</param>
        /// <param name="userId">The user identifier.</param>
        private void BindUserDropdown(ref DropDownList dropDown, string userId = "")
        {
            //DataSet theDS = new DataSet();
            userId = userId == "0" ? "" : userId;
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{

            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            if (userId != "")
            {

                rowFilter = "UserId = " + userId;
            }
            else if (this.EmployeeId > 0)
            {
                userId = this.UserId.ToString();
                rowFilter = "UserId = " + userId;
            }
            //}
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(dropDown, theDT, "Name", "UserId", "", userId);
                ListItem item = dropDown.Items.FindByValue(userId);
                if (item == null)
                {
                    item = dropDown.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;

                }
            }
        }

        /// <summary>
        /// Binds the drug controls.
        /// </summary>
        /// <param name="drugId">The drug identifier.</param>
        /// <param name="generic">The generic.</param>
        /// <param name="drugType">Type of the drug.</param>
        /// <param name="flag">The flag.</param>
        private void BindDrugControls(int drugId, int generic, int drugType, int flag)
        {
            #region "ARV Drugs"

            if ((drugType == 37 || drugType == 36) && flag == 0) //// DrugType-36 OI Med,37 ARV Med////
            {
                Panel thePnl = new Panel();
                if (generic == 0)
                {
                    thePnl.ID = "pnlDrugARV_" + drugId;
                }
                else
                {
                    thePnl.ID = "pnlGenericARV_" + generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "" + generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)Session["AllData"];
                DataTable DT = new DataTable();
                if (generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    if (drugId.ToString().LastIndexOf("8888") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "Drug_Pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (drugId.ToString().LastIndexOf("9999") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + generic;
                }

                Label theDrugNm = new Label();
                if (generic == 0)
                {
                    theDrugNm.ID = "ARVdrgNm" + drugId;
                }
                else
                {
                    theDrugNm.ID = "ARVGenericNm" + generic;
                }
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 400;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace_" + drugId + "" + generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                ////////////////////

                thePnl.Controls.Add(theSpace);

                BindFunctions theBindMgr = new BindFunctions();
                DropDownList theDrugFrequency = new DropDownList();
                if (generic == 0)
                {
                    theDrugFrequency.ID = "ARVdrgFrequency" + drugId;
                }
                else { theDrugFrequency.ID = "ARVGenericFrequency" + generic; }
                theDrugFrequency.Width = 80;

                #region "BindCombo"

                DataTable theDTF = new DataTable();
                DataView theDVFrequency = new DataView(theDS.Tables[21]);
                DataTable theDTFrequency = new DataTable();
                if (theDVFrequency.Count > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                    theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                }

                #endregion "BindCombo"

                thePnl.Controls.Add(theDrugFrequency);

                ////////////Space////////////////////////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2" + drugId + "" + generic;
                theSpace2.Width = 15;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////////////////////////

                TextBox theQtyPrescribed = new TextBox();
                if (generic == 0)
                {
                    theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + drugId;
                }
                else
                {
                    theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + generic;
                }
                theQtyPrescribed.Width = 100;
                thePnl.Controls.Add(theQtyPrescribed);
                theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4" + drugId + "" + generic;
                theSpace4.Width = 20;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (generic == 0)
                {
                    theQtyDispensed.ID = "ARVdrgQtyDispensed" + drugId;
                }
                else
                {
                    theQtyDispensed.ID = "ARVGenericQtyDispensed" + generic;
                }
                theQtyDispensed.Width = 100;
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);
                theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5" + drugId + "" + generic;
                theSpace5.Width = 20;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////
                CheckBox theFinChk = new CheckBox();
                if (generic == 0)
                {
                    theFinChk.ID = "ARVDrugFinChk-" + drugId;
                }
                else { theFinChk.ID = "ARVGenericFinChk-" + generic; }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);
                ////////////Space///////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6" + drugId + "" + generic;
                theSpace6.Width = 20;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                DIVCustomItem.Controls.Add(thePnl);
            }
            else if ((drugType == 37 || drugType == 36) && flag == 1)
            {
                Panel thePnl = new Panel();
                if (generic == 0)
                {
                    thePnl.ID = "pnlDrugARV_" + drugId;
                }
                else
                {
                    thePnl.ID = "pnlGenericARV_" + generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "" + generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)Session["AllData"];
                DataTable DT = new DataTable();
                if (generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    if (drugId.ToString().LastIndexOf("8888") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "Drug_Pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (drugId.ToString().LastIndexOf("9999") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + generic;
                }

                Label theDrugNm = new Label();
                if (generic == 0)
                {
                    theDrugNm.ID = "ARVdrgNm" + drugId;
                }
                else
                {
                    theDrugNm.ID = "ARVGenericNm" + generic;
                }
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 400;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace_" + drugId + "" + generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                ////////////////////

                thePnl.Controls.Add(theSpace);

                BindFunctions theBindMgr = new BindFunctions();
                DropDownList theDrugFrequency = new DropDownList();
                if (generic == 0)
                {
                    theDrugFrequency.ID = "ARVdrgFrequency" + drugId;
                }
                else { theDrugFrequency.ID = "ARVGenericFrequency" + generic; }
                theDrugFrequency.Width = 80;

                #region "BindCombo"

                DataTable theDTF = new DataTable();
                DataView theDVFrequency = new DataView(theDS.Tables[21]);

                DataTable theDTFrequency = new DataTable();
                if (theDVFrequency.Count > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                    theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                }

                #endregion "BindCombo"

                thePnl.Controls.Add(theDrugFrequency);

                ////////////Space////////////////////////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2" + drugId + "" + generic; ;
                theSpace2.Width = 15;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////////////////////////
                TextBox theQtyPrescribed = new TextBox();
                if (generic == 0)
                {
                    theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + drugId;
                }
                else
                {
                    theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + generic;
                }
                theQtyPrescribed.Width = 100;
                //theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);
                theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4" + drugId + "" + generic;
                theSpace4.Width = 20;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (generic == 0)
                {
                    theQtyDispensed.ID = "ARVdrgQtyDispensed" + drugId;
                }
                else
                {
                    theQtyDispensed.ID = "ARVGenericQtyDispensed" + generic;
                }
                theQtyDispensed.Width = 100;
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);
                theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5" + drugId + "" + generic;
                theSpace5.Width = 20;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////
                CheckBox theFinChk = new CheckBox();
                if (generic == 0)
                {
                    theFinChk.ID = "ARVDrugFinChk" + drugId;
                }
                else { theFinChk.ID = "ARVGenericFinChk" + generic; }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);
                ////////////Space///////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6" + drugId + "" + generic;
                theSpace6.Width = 20;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                DIVCustomItem.Controls.Add(thePnl);
            }

            #endregion "ARV Drugs"

            #region "Non ARV Drugs"

            else
            {
                Panel thePnl = new Panel();
                thePnl.Controls.Clear();
                if (generic == 0)
                {
                    thePnl.ID = "pnlDrug" + drugId;
                }
                else
                {
                    thePnl.ID = "pnlGeneric" + generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "^" + generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)Session["AllData"];
                if (generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    theDV.RowFilter = "Drug_Pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (drugId.ToString().LastIndexOf("9999") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + generic;
                }

                Label theDrugNm = new Label();
                if (generic == 0)
                {
                    theDrugNm.ID = "DrugNm" + drugId;
                }
                else
                {
                    theDrugNm.ID = "GenericNm" + generic;
                }

                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 350;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace" + drugId + "^" + generic;
                theSpace.Width = 20;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);
                ////////////////////

                BindFunctions theBindMgr = new BindFunctions();

                DropDownList theFrequency = new DropDownList();
                if (generic == 0)
                {
                    theFrequency.ID = "drugFrequency" + drugId;
                }
                else
                {
                    theFrequency.ID = "GenericFrequency" + generic;
                }
                theFrequency.Width = 80;
                DataTable DTFreq = new DataTable();
                DTFreq = theDS.Tables[12];
                theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
                thePnl.Controls.Add(theFrequency);

                /////// Space//////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3*" + drugId + "^" + generic;
                theSpace3.Width = 10;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
                ////////////////////

                TextBox theQtyPrescribed = new TextBox();
                if (generic == 0)
                {
                    theQtyPrescribed.ID = "drugQtyPrescribed" + drugId;
                }
                else
                {
                    theQtyPrescribed.ID = "genericQtyPrescribed" + generic;
                }
                theQtyPrescribed.Width = 90;
                theQtyPrescribed.Text = "";
                tabContainer.ID = "TAB";
                theQtyPrescribed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theQtyPrescribed.ClientID + "')");
                //theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5*" + drugId + "^" + generic;
                theSpace5.Width = 10;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (generic == 0)
                {
                    theQtyDispensed.ID = "drugQtyDispensed" + drugId;
                }
                else { theQtyDispensed.ID = "genericQtyDispensed" + generic; }
                theQtyDispensed.Width = 90;
                theQtyDispensed.Text = "";
                tabContainer.ID = "TAB";
                theQtyDispensed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theQtyDispensed.ClientID + "')");
                //theQtyDispensed.Load += new EventHandler(Control_Load);
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);

                ////////////Space////////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6*" + drugId + "^" + generic;
                theSpace6.Width = 25;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                ////////////////////////////////////////

                CheckBox theFinChk = new CheckBox();
                if (generic == 0)
                {
                    theFinChk.ID = "FinChkDrug" + drugId;
                }
                else
                {
                    theFinChk.ID = "FinChkGeneric" + generic;
                }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);

                ////////////Space////////////////////////
                Label theSpace7 = new Label();
                theSpace7.ID = "theSpace7*" + drugId + "^" + generic;
                theSpace7.Width = 15;
                theSpace7.Text = "";
                thePnl.Controls.Add(theSpace7);
                ////////////////////////////////////////
                DIVCustomItem.Controls.Add(thePnl);
            }

            #endregion "Non ARV Drugs"
        }

        /// <summary>
        /// Binds the grid view.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="theControl">The control.</param>
        /// <param name="dt">The dt.</param>
        private void BindGridView(string section, Control theControl, DataTable dt)
        {
            foreach (object obj in theControl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(GridView))
                                {
                                    if (((GridView)x).ID.Contains("Dview_" + section))
                                    {
                                        ((GridView)x).DataSource = dt;
                                        ((GridView)x).DataBind();
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        /// <summary>
        /// Binds the time12 control value.
        /// </summary>
        /// <param name="timeMinute">The time minute.</param>
        /// <param name="id">The identifier.</param>
        private void BindTime12ControlValue(string[] timeMinute, string id)
        {
            string[] AMPM = new string[1]; ;
            if (timeMinute[1].Contains("AM"))
            {
                AMPM = timeMinute[1].Replace(' ', ':').Split(':');
            }
            if (timeMinute[1].Contains("PM"))
            {
                AMPM = timeMinute[1].Replace(' ', ':').Split(':');
            }

            foreach (object obj in tabContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (((DropDownList)x).ID == id && ((DropDownList)x).ID.Contains("AM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[1]);
                                    }
                                    else if (((DropDownList)x).ID == id && ((DropDownList)x).ID.Contains("PM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[1]);
                                    }
                                    else if (((DropDownList)x).ID == id && ((DropDownList)x).ID.Contains("Min"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Binds the time24 control value.
        /// </summary>
        /// <param name="TimeMinute">The time minute.</param>
        /// <param name="ID">The identifier.</param>
        private void BindTime24ControlValue(string[] timeMinute, string id)
        {
            foreach (object obj in tabContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (((DropDownList)x).ID == id)
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(timeMinute[1]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private StringBuilder PrepareGetValueQuery(string strtableName, int patientId, int visitId, int locationId)
        {
            StringBuilder SBGetValue = new StringBuilder();

            if (strtableName == "DTL_CUSTOMFIELD")
            {
                string TableName = "DTL_FBCUSTOMFIELD_" + lblFeatureName.Text.Replace(' ', '_');
                SBGetValue.Append("Select * from [" + TableName + "] where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + ";");
            }
            else if (strtableName == "DTL_CUSTOMFORM")
            {
            }
            else if (strtableName == "REGIMEN")
            {
            }
            else
            {
                if (strtableName == "dtl_PatientCareEnded".ToUpper())
                {
                    SBGetValue.Append("Select * from dtl_PatientCareEnded where Ptn_pk=" + patientId + " and LocationId=" + locationId + ";");
                }
                else if (strtableName == "dtl_PatientARVInfo".ToUpper() || strtableName == "dtl_PatientContacts".ToUpper())
                {
                    SBGetValue.Append("Select * from [" + strtableName + "] where Ptn_pk=" + patientId + " and Visitid=" + patientId + " and LocationId=" + locationId + ";");
                }
                else if (strtableName == "mst_patient".ToUpper())
                {
                    SBGetValue.Append("Select * from mst_patient where Ptn_pk=" + patientId + " and LocationId=" + locationId + ";");
                }
                else if (strtableName == "dtl_ICD10Field".ToUpper())
                {
                    SBGetValue.Append("Select +'%'+Convert(Varchar,ISNULL(BlockId,0)) +'%'+ Convert(Varchar,ISNULL(SubBlockId,0))+'%'+Convert(Varchar,ISNULL(ICDCodeId,0))+'%'+convert(varchar, Predefined)[CodeId],");
                    SBGetValue.Append("Case When Predefined = 0 then '8888'+Convert(Varchar,FieldId) When Predefined = 1 then '9999'+Convert(Varchar,FieldId)end[Field], * from [" + strtableName + "]");
                    SBGetValue.Append("where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + ";");
                }
                else if (strtableName == "LNK_FORMTABORDVISIT")
                {
                    SBGetValue.Append("Select * from LNK_FORMTABORDVISIT  where Visit_Pk=" + visitId + ";");
                }
                else if (strtableName == "dtl_PatientClinicalNotes".ToUpper())
                {
                    SBGetValue.Append("Select * from dtl_PatientClinicalNotes where Visit_Pk=" + visitId + " and modifiedflag=0;");
                }
                else
                {
                    SBGetValue.Append("Select * from [" + strtableName + "] where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + ";");
                }
            }
            return SBGetValue;
        }
        private void BindICD110Values(string strTableName, ref TabContainer container, ref DataSet valuesDataset)
        {
            foreach (DataRow theDRICD10 in valuesDataset.Tables[0].Rows)
            {
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object x in c.Controls)
                                {
                                    if (x.GetType() == typeof(CheckBox))
                                    {
                                        string[] remStr = ((CheckBox)x).ID.Split('-');
                                        string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                        if ("CHKMULTI-" + theDRICD10["Field"] + theDRICD10["CodeId"] + "-" + strTableName == str)//((CheckBox)x).ID.Substring(0, ((CheckBox)x).ID.LastIndexOf('-')))
                                        {
                                            ((CheckBox)x).Checked = true;
                                        }
                                    }
                                    if (x.GetType() == typeof(TextBox))
                                    {
                                        string[] remStr = ((TextBox)x).ID.Split('-');
                                        string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                        if ("TXTDT-" + theDRICD10["Field"] + theDRICD10["CodeId"].ToString().Replace('%', '^') + "OnSetDate" + "-" + strTableName == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                        {
                                            string strdateformat = string.Format("{0:dd-MMM-yyyy}", theDRICD10["DateOnSet"]);
                                            if (strdateformat.Trim() != "01-Jan-1900")
                                            {
                                                ((TextBox)x).Text = string.Format("{0:dd-MMM-yyyy}", theDRICD10["DateOnSet"]);
                                            }
                                        }

                                        if ("TXTComment-" + theDRICD10["Field"] + theDRICD10["CodeId"].ToString().Replace('%', '~') + "ICDComment" + "-" + strTableName == str)// ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                        {
                                            ((TextBox)x).Text = Convert.ToString(theDRICD10["Comments"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void BindTextBoxValue(ref string strTableName, TextBox textBox, ref DataSet valuesDataset, ref int tableCount, ref int columnCount, ref DataTable businessRuleDataTable)
        {
            string[] remStr = textBox.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];


            if ("TXTMulti-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    textBox.Text = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                }
            }
            if ("TXTSingle-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    textBox.Text = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                }
            }

            if ("TXT-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    textBox.Text = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);

                    if (textBox.Text != "")
                    {
                        bool FlagVal = CheckAbNormalStatus(textBox.ID, textBox.Text);
                        if (FlagVal == true)
                        {
                            textBox.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                        }
                    }


                }
            }

            if ("TXTNUM-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    textBox.Text = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);

                    if (textBox.Text != "")
                    {
                        bool FlagVal = CheckAbNormalStatus(textBox.ID, textBox.Text);
                        if (FlagVal == true)
                        {
                            textBox.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                        }
                    }


                }
            }

            if ("TXTDT-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    // Date formate for like "MMM-yyyy"
                    DataView dvBusDtl = businessRuleDataTable.DefaultView;
                    dvBusDtl.RowFilter = "BusRuleId = 21";
                    DataTable dtFilter = dvBusDtl.ToTable();

                    bool isDateValidate = true;
                    if (dtFilter.Rows.Count > 0)
                    {
                        for (int rowcount = 0; rowcount < dtFilter.Rows.Count; rowcount++)
                        {
                            if (dtFilter.Rows[rowcount]["FieldName"].ToString().Trim() == valuesDataset.Tables[tableCount].Columns[columnCount].ToString().Trim() && dtFilter.Rows[rowcount]["TableName"].ToString().Trim() == strTableName.ToString().Trim())
                            {
                                isDateValidate = false;
                                textBox.Text = string.Format("{0:MMM-yyyy}", valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                            }
                            else
                            {
                                if (isDateValidate == true)
                                {
                                    textBox.Text = string.Format("{0:dd-MMM-yyyy}", valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                                }
                            }
                        }
                    }
                    else
                    {
                        textBox.Text = string.Format("{0:dd-MMM-yyyy}", valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                    }
                }
            }
            if ("TXTReg-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    textBox.Text = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                    string[] regimen = textBox.ID.Split('=');
                    string[] controlid = regimen[0].Split('-');
                    RegimenSessionSetting(Convert.ToInt32(regimen[1].Remove(regimen[1].IndexOf("-"), regimen[1].Length - regimen[1].IndexOf("-"))), controlid[3].ToString(), textBox.Text);
                }
            }
        }

        private void BindDropDownListValue(ref string strTableName, DropDownList dropDown, ref DataSet valuesDataset, ref int tableCount, ref int columnCount, ref DataTable tabsTable)
        {
            string[] remStr = dropDown.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
            string strTabSig = remStr[0] + "-" + remStr[1] + "-" + remStr[2] + "-" + remStr[4];
            if ("SELECTLIST-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str && dropDown.ID.Contains("24Hr"))
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    if (valuesDataset.Tables[tableCount].Rows[0][columnCount] != DBNull.Value)
                    {
                        string[] TimeMinute = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]).Split(':');
                        dropDown.SelectedValue = Convert.ToString(TimeMinute[0]);
                        string ID = dropDown.ID.Replace("24Hr", "Min");
                        BindTime24ControlValue(TimeMinute, ID);
                    }
                }
            }
            else if ("SELECTLIST-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str && dropDown.ID.Contains("12Hr"))
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    string[] timeMinute = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]).Split(':');
                    dropDown.SelectedValue = Convert.ToString(timeMinute[0]);
                    string ID = dropDown.ID.Replace("12Hr", "Min");
                    BindTime12ControlValue(timeMinute, ID);
                }
            }
            else if ("SELECTLIST-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str && dropDown.ID.Contains("AMPM"))
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    string[] TimeMinute = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]).Split(':');
                    dropDown.SelectedValue = Convert.ToString(TimeMinute[0]);
                    string ID = dropDown.ID.Replace("12Hr", "Min");
                    BindTime12ControlValue(TimeMinute, ID);
                }
            }
            else if ("SELECTLIST-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str && dropDown.ID.Contains("AMPM") == false && dropDown.ID.Contains("12Hr") == false && dropDown.ID.Contains("24Hr") == false && dropDown.ID.Contains("Min") == false)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    dropDown.SelectedValue = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                    string[] theId = dropDown.ID.Split('-');
                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                    if (theDVConditionalField.Count > 0)
                    {
                        EventArgs s = new EventArgs();
                        ddlSelectList_SelectedIndexChanged(dropDown, s);
                    }
                }
            }
            else if ("SELECTLIST-TAB" + valuesDataset.Tables[tableCount].Columns[1].ToString() + "-" + strTableName == str && dropDown.ID.Contains("LNK_FORMTABORDVISIT") == true)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    foreach (DataRow theDRTabId in tabsTable.Rows)
                    {
                        if ("SELECTLIST-TAB" + valuesDataset.Tables[tableCount].Columns[1].ToString() + "-" + strTableName + "-" + Convert.ToInt32(theDRTabId["TabId"]) == strTabSig && dropDown.ID.Contains("LNK_FORMTABORDVISIT") == true)
                        {
                            foreach (DataRow theDRSig in valuesDataset.Tables[tableCount].Rows)
                            {
                                if (Convert.ToInt32(theDRTabId["TabId"]) == Convert.ToInt32(theDRSig["TabId"]))
                                {
                                    dropDown.SelectedValue = Convert.ToString(theDRSig["Signature"]);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void BindHtmlInputRadioValue(ref string strTableName, HtmlInputRadioButton htmlInputRadioButton, ref int tableCount, ref int columnCount, ref DataSet valuesDataset)
        {
            string[] remStr = htmlInputRadioButton.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
            if (valuesDataset.Tables[tableCount].Columns[columnCount].ToString() == htmlInputRadioButton.Name)
            {
                for (int k = 0; k < valuesDataset.Tables[tableCount].Rows.Count; k++)
                {
                    if (valuesDataset.Tables[tableCount].Rows[k][valuesDataset.Tables[tableCount].Columns[columnCount]].ToString() == "True" || valuesDataset.Tables[tableCount].Rows[k][valuesDataset.Tables[tableCount].Columns[columnCount]].ToString() == "1")
                    {
                        if ("RADIO1-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
                        {
                            htmlInputRadioButton.Checked = true;
                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                            string[] theId = htmlInputRadioButton.ID.Split('-');
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.HtmlRadioButtonSelect(htmlInputRadioButton);
                            }
                        }
                    }
                    else if (valuesDataset.Tables[tableCount].Rows[k][valuesDataset.Tables[tableCount].Columns[columnCount]].ToString() == "False" || valuesDataset.Tables[tableCount].Rows[k][valuesDataset.Tables[tableCount].Columns[columnCount]].ToString() == "0")
                    {
                        if ("RADIO2-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
                        {
                            htmlInputRadioButton.Checked = true;
                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                            string[] theId = htmlInputRadioButton.ID.Split('-');
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.HtmlRadioButtonSelect(htmlInputRadioButton);
                            }
                        }
                    }
                }
            };
        }
        private void BindHtmlInputCheckBoxValue(ref string strTableName, HtmlInputCheckBox htmlInputCheckBox, ref int tableCount, ref int columnCount, ref DataSet valuesDataset)
        {
            string[] remStr = htmlInputCheckBox.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
            if ("Chk-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName == str)
            {
                for (int k = 0; k < valuesDataset.Tables[tableCount].Rows.Count; k++)
                {
                    if (valuesDataset.Tables[tableCount].Rows[k][valuesDataset.Tables[tableCount].Columns[columnCount]].ToString() == "True")
                    {
                        htmlInputCheckBox.Checked = true;
                    }
                    else { htmlInputCheckBox.Checked = false; }
                }
            }
        }
        private void BindIQLookupTextBoxValue(ref string strTableName, IQLookupTextBox iqTextBox, ref int tableCount, ref int columnCount, ref DataSet valuesDataset)
        {

            string[] remStr = iqTextBox.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
            string strTabSig = remStr[0] + "-" + remStr[1] + "-" + remStr[2] + "-" + remStr[4];

            string strName = "TXTLookup-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName;

            if (strName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    iqTextBox.SelectedValue = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                    ILookupService lkMgr = (ILookupService)ObjectFactory.CreateInstance("BusinessProcess.Lookup.BLookup, BusinessProcess.Lookup");
                    LookupItem item = lkMgr.GetLookUpItem(Convert.ToInt32(iqTextBox.SelectedValue), iqTextBox.LookupName, iqTextBox.LookupCategory);
                    iqTextBox.ValueText = item.Name;


                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                    string[] theId = iqTextBox.ID.Split('-');
                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                    if (theDVConditionalField.Count > 0)
                    {
                        //EventArgs s = new EventArgs();
                        //ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                    }
                }
            }
        }
        /// <summary>
        /// Binds the value.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="theControl">The control.</param>
        /// <param name="tabsTable">The tabs table.</param>
        private void BindValue(int patientId, int visitId, int locationId, Control theControl, DataTable tabsTable)
        {
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            TabContainer container = tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataTable TempDT = theDT.DefaultView.ToTable(true, "TableName").Copy();
            string strQuery = "Select VisitDate, Signature,DataQuality from ord_visit where Ptn_Pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "";

            DataSet dsVisitDetails = MgrBindValue.Common_GetSaveUpdate(strQuery);
            try
            {
                if (!IsPostBack)
                {
                    txtvisitDate.Text = string.Format("{0:dd-MMM-yyyy}", dsVisitDetails.Tables[0].Rows[0]["VisitDate"]);
                    ViewState["VisitDate"] = txtvisitDate.Text;
                    if (Convert.ToInt32(dsVisitDetails.Tables[0].Rows[0]["DataQuality"]) == 1)
                    {
                        btncomplete.CssClass = "greenbutton";
                    }

                    if (dsVisitDetails.Tables[0].Rows[0]["Signature"].ToString() != "")
                    {
                        this.BindUserDropdown(ref ddSignature, dsVisitDetails.Tables[0].Rows[0]["Signature"].ToString());
                        // BindDropdown(ddSignature, TmpDS.Tables[0].Rows[0]["Signature"].ToString());
                        // ddSignature.SelectedValue = TmpDS.Tables[0].Rows[0]["Signature"].ToString();
                    }

                    DataTable dtgGetDataView = GetGridTable((DataTable)ViewState["LnkTable"]);
                    //DataTable dtgGetDataView = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView", "FeatureName").Copy();
                    DataView dvGetDataView = new DataView(dtgGetDataView);
                    dvGetDataView.RowFilter = "IsGridView = 1";
                    #region gridView
                    if (dvGetDataView.Count > 0)
                    {
                        foreach (DataRow TempDR in dvGetDataView.ToTable().Rows)
                        {
                            string GetValue = "";
                            string TableName = "DTL_CUSTOMFORM_" + TempDR["SectionName"].ToString() + "_" + TempDR["FeatureName"].ToString().Trim().Replace(' ', '_');
                            GetValue = "Select * from [" + TableName + "] where FormID=" + TempDR["FeatureID"].ToString() + "and   SectionID=" + TempDR["SectionID"].ToString() + " and Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + "";
                            DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);

                            foreach (object obj in container.Controls)
                            {
                                if (obj is TabPanel)
                                {
                                    TabPanel tabPanel = (TabPanel)obj;
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object y in c.Controls)
                                            {
                                                if (y.GetType() == typeof(GridView))
                                                {
                                                    if (((GridView)y).ID.Contains("Dview_" + TempDR["SectionID"].ToString()))
                                                    {
                                                        if (((GridView)y).DataSource == null)
                                                        {
                                                            TempDS.Tables[0].Columns.Remove("ID");
                                                            TempDS.Tables[0].Columns.Remove("Ptn_pk");
                                                            TempDS.Tables[0].Columns.Remove("Visit_Pk");
                                                            TempDS.Tables[0].Columns.Remove("LocationId");
                                                            TempDS.Tables[0].Columns.Remove("UserId");
                                                            TempDS.Tables[0].Columns.Remove("SectionId");
                                                            TempDS.Tables[0].Columns.Remove("FormID");
                                                            TempDS.Tables[0].Columns.Remove("CreateDate");
                                                            TempDS.Tables[0].Columns.Remove("UpdateDate");

                                                            for (int x = 0; x < TempDS.Tables[0].Columns.Count; x++)
                                                            {
                                                                foreach (DataRow row in ((DataTable)ViewState["gblDTGridViewControls"]).Rows)
                                                                {
                                                                    if (TempDS.Tables[0].Columns[x].ColumnName == row["FieldName"].ToString())
                                                                    {
                                                                        TempDS.Tables[0].Columns[x].ColumnName = row["FieldLabel"].ToString();
                                                                    }
                                                                    if (TempDS.Tables[0].Columns[x].DataType.ToString() == "System.DateTime")
                                                                    {
                                                                        //TempDS.Tables[0].Columns[x]
                                                                    }
                                                                }
                                                            }
                                                            DataTable dtGetRecord = new DataTable();
                                                            dtGetRecord = TempDS.Tables[0].Clone();
                                                            for (int q = 0; q < dtGetRecord.Columns.Count; q++)
                                                            {
                                                                if (dtGetRecord.Columns[q].DataType.ToString() == "System.Int32")
                                                                {
                                                                    dtGetRecord.Columns[q].DataType = Type.GetType("System.String");
                                                                }
                                                            }
                                                            string SectionID = TempDR["SectionID"].ToString();
                                                            DataTable dtlnktable = ((DataTable)ViewState["LnkTable"]).Copy();

                                                            for (int i = 0; i < TempDS.Tables[0].Rows.Count; i++)
                                                            {
                                                                DataRow dr = dtGetRecord.NewRow();
                                                                for (int x = 0; x < TempDS.Tables[0].Columns.Count; x++)
                                                                {
                                                                    bool isddl = false;

                                                                    DataView dvGridViewDDL = new DataView(dtlnktable);
                                                                    string COLNAME = TempDS.Tables[0].Columns[x].ColumnName.ToString();
                                                                    dvGridViewDDL.RowFilter = "FieldLabel= '" + COLNAME + "' AND IsGridView = 1 and  ControlId = 4";
                                                                    if (dvGridViewDDL.Count > 0)
                                                                    {
                                                                        if (ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()] != null)
                                                                        {
                                                                            DataTable dtDDL = new DataTable();
                                                                            dtDDL = (DataTable)ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()];
                                                                            DataView dvddl = new DataView(dtDDL);
                                                                            string DDLVALUE = TempDS.Tables[0].Rows[i][x].ToString();
                                                                            if (!String.IsNullOrEmpty(DDLVALUE))
                                                                            {
                                                                                dvddl.RowFilter = "ID  ='" + DDLVALUE + "'";
                                                                                DataTable dtdlnew = dvddl.ToTable();
                                                                                if (dtdlnew.Rows.Count > 0)
                                                                                {
                                                                                    dr[x] = dvddl[0]["Name"];
                                                                                }
                                                                                else
                                                                                {
                                                                                    dr[x] = DBNull.Value;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                dr[x] = DBNull.Value;
                                                                            }
                                                                            isddl = true;
                                                                        }
                                                                    }
                                                                    if (!isddl)
                                                                    {
                                                                        if (TempDS.Tables[0].Columns[x].DataType.ToString() == "System.DateTime")
                                                                        {
                                                                            if (Convert.ToString(TempDS.Tables[0].Rows[i][x]).Contains("1/1/1900"))
                                                                            {
                                                                                dr[x] = DBNull.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                dr[x] = TempDS.Tables[0].Rows[i][x];
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            dr[x] = TempDS.Tables[0].Rows[i][x];
                                                                        }
                                                                    }
                                                                }
                                                                dtGetRecord.Rows.Add(dr);
                                                            }
                                                            ViewState["GridCache_" + TempDR["SectionID"].ToString()] = dtGetRecord;
                                                            BindGridView(TempDR["SectionID"].ToString(), container, dtGetRecord);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                }
                StringBuilder SBGetValue = new StringBuilder();

                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string _tableName = TempDR["TableName"].ToString().ToUpper();
                    SBGetValue.Append(this.PrepareGetValueQuery(_tableName, PatientId, VisitId, LocationId));

                }

                DataSet tempDSValue = new DataSet();
                if (!string.IsNullOrEmpty(SBGetValue.ToString()))
                {
                    tempDSValue = MgrBindValue.Common_GetSaveUpdate(SBGetValue.ToString());
                }
                DataTable theBUssDT = (DataTable)ViewState["BusRule"];
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string strTableName = TempDR["TableName"].ToString();
                    if (strTableName == "dtl_ICD10Field")
                    {
                        this.BindICD110Values(_tableName, ref tabContainer, ref tempDSValue);

                    }
                    else
                    {
                        if (tempDSValue.Tables.Count > 0)
                        {
                            for (int n = 0; n < tempDSValue.Tables.Count; n++)
                            {
                                for (int i = 0; i <= tempDSValue.Tables[n].Columns.Count - 1; i++)
                                {
                                    foreach (TabPanel tabPanel in container.Tabs)
                                    {

                                        foreach (Control ctrl in tabPanel.Controls)
                                        {
                                            Control c = ctrl;
                                            foreach (object x in c.Controls)
                                            {
                                                if (x.GetType() == typeof(TextBox))
                                                {
                                                    this.BindTextBoxValue(ref strTableName, (TextBox)x, ref tempDSValue, ref n, ref i, ref theBUssDT);
                                                }
                                                else if (x.GetType() == typeof(DropDownList))
                                                {
                                                    this.BindDropDownListValue(ref strTableName, (DropDownList)x, ref tempDSValue, ref n, ref i, ref tabsTable);
                                                }
                                                else if (x.GetType() == typeof(HtmlInputRadioButton))
                                                {
                                                    this.BindHtmlInputRadioValue(ref strTableName, (HtmlInputRadioButton)x, ref n, ref i, ref tempDSValue);

                                                }
                                                else if (x.GetType() == typeof(HtmlInputCheckBox))
                                                {
                                                    this.BindHtmlInputCheckBoxValue(ref strTableName, (HtmlInputCheckBox)x, ref n, ref i, ref tempDSValue);
                                                }
                                                else if (x.GetType() == typeof(IQCare.IQControl.IQLookupTextBox))
                                                {
                                                    IQLookupTextBox iq = (IQLookupTextBox)x;
                                                    this.BindIQLookupTextBoxValue(ref strTableName, iq, ref n, ref i, ref tempDSValue);


                                                }
                                            }
                                            //}
                                        }
                                        // }
                                    }
                                }
                            }
                        }
                    }
                }

                //Multiselect
                DataTable theMultiDT = new DataTable();
                theMultiDT.Columns.Add(new DataColumn("TableName", typeof(string)));
                DataSet TmpDSMulti = new DataSet();
                StringBuilder SBGetValueMultiselect = new StringBuilder();
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object multiselect in c.Controls)
                                {
                                    if (multiselect.GetType() == typeof(Panel))
                                    {
                                        string Id = string.Empty;
                                        foreach (Control z in ((Control)multiselect).Controls)
                                        {
                                            if (z.GetType() == typeof(CheckBox) && ((CheckBox)z).ID.Contains("FinChkDrug") == false)
                                            {
                                                string[] Table = ((CheckBox)z).ID.Split('-');
                                                string TableName = Table[3];
                                                Id = Table[1];
                                                if (TableName == "DTL_CUSTOMFIELD")
                                                {
                                                    DataRow theMultiDR = theMultiDT.NewRow();
                                                    TableName = "DTL_FB_" + Table[2] + "";
                                                    TableName = TableName.Trim().Replace(' ', '_');
                                                    theMultiDR["TableName"] = TableName;
                                                    theMultiDT.Rows.Add(theMultiDR);
                                                }
                                                else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                                {
                                                }
                                                else
                                                {
                                                    DataRow theMultiDR = theMultiDT.NewRow();
                                                    theMultiDR["TableName"] = TableName;
                                                    theMultiDT.Rows.Add(theMultiDR);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DataTable theLocalDT = theMultiDT.DefaultView.ToTable(true, "TableName").Copy();
                for (int i = 0; i < theLocalDT.Rows.Count; i++)
                {
                    SBGetValueMultiselect.Append("Select * from " + theLocalDT.Rows[i]["TableName"].ToString() + " where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + ";");
                }

                TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(SBGetValueMultiselect.ToString());
                for (int m = 0; m < TmpDSMulti.Tables.Count; m++)
                {
                    if (TmpDSMulti.Tables[m].Rows.Count > 0)
                    {
                        foreach (object obj in container.Controls)
                        {
                            if (obj is TabPanel)
                            {
                                TabPanel tabPanel = (TabPanel)obj;
                                foreach (object ctrl in tabPanel.Controls)
                                {
                                    if (ctrl is Control)
                                    {
                                        Control c = (Control)ctrl;
                                        foreach (object multiselect in c.Controls)
                                        {
                                            if (multiselect.GetType() == typeof(Panel))
                                            {
                                                string Id = string.Empty;
                                                foreach (Control z in ((Control)multiselect).Controls)
                                                {
                                                    if (z.GetType() == typeof(CheckBox))
                                                    {
                                                        string[] Table = ((CheckBox)z).ID.Split('-');
                                                        string TableName = Table[3];
                                                        string str = Table[0] + "-" + Table[1] + "-" + Table[2] + "-" + Table[3];
                                                        if (Table.Length == 5)
                                                        {
                                                            if (Table[3] == "DTL_CUSTOMFIELD")
                                                            {
                                                                foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + "DTL_CUSTOMFIELD" == str)
                                                                        {
                                                                            ((CheckBox)z).Checked = true;
                                                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                            string[] theId = ((CheckBox)z).ID.Split('-');
                                                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                            if (theDVConditionalField.Count > 0)
                                                                            {
                                                                                EventArgs s = new EventArgs();
                                                                                this.HtmlCheckBoxSelect(z);
                                                                            }
                                                                            if (((CheckBox)z).Text == "Other" && "CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + "DTL_CUSTOMFIELD" == str)
                                                                            {
                                                                                string script = "";
                                                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                                                script += "show('" + ((CheckBox)z).ID + "-" + Table[2] + "');\n";
                                                                                script += "</script>\n";
                                                                                //ClientScript.RegisterStartupScript(this.GetType(),"Other" + Id + "", script);
                                                                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Other" + Id + "", script);
                                                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                                                ViewState["Othertxt"] = theDR[6];
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                                            {
                                                            }
                                                            else
                                                            {
                                                                foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + Table[3].ToString() == str)
                                                                        {
                                                                            if (((CheckBox)z).Text == "Other")
                                                                            {
                                                                                ((CheckBox)z).Checked = true;
                                                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                                if (theDVConditionalField.Count > 0)
                                                                                {
                                                                                    EventArgs s = new EventArgs();
                                                                                    this.HtmlCheckBoxSelect(z);
                                                                                }

                                                                                string script = "";
                                                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                                                script += "show('" + Table[2] + "');\n";
                                                                                script += "</script>\n";
                                                                                ClientScript.RegisterStartupScript(this.GetType(), "Other" + Id + "", script);
                                                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                                                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                                                DataSet dsMultiSelectList = new DataSet();
                                                                                dsMultiSelectList.ReadXml(filePath);
                                                                                DataTable DT = dsMultiSelectList.Tables[0];
                                                                                foreach (DataRow DR in DT.Rows)
                                                                                {
                                                                                    if (DR[0].ToString().ToUpper() == TableName)
                                                                                    {
                                                                                        ViewState["Othertxt"] = theDR["" + DR[2].ToString() + ""];
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ((CheckBox)z).Checked = true;
                                                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                                if (theDVConditionalField.Count > 0)
                                                                                {
                                                                                    EventArgs s = new EventArgs();
                                                                                    this.HtmlCheckBoxSelect(z);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (z.GetType() == typeof(HtmlInputText))
                                                    {
                                                        if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                                        {
                                                            ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                                        }
                                                    }

                                                    if (z.GetType() == typeof(TextBox))
                                                    {
                                                        string[] TableName = ((TextBox)z).ID.Split('-');
                                                        string str = TableName[0] + "-" + TableName[1] + "-" + TableName[2] + "-" + TableName[3];
                                                        if (TableName.Length == 6)
                                                        {
                                                            foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                            {
                                                                if (TmpDSMulti.Tables[m].Rows.Count > 0)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("TXTDT1-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["DateField1"].ToString()) && Convert.ToDateTime(theDR["DateField1"]) != DateTime.Parse("1/1/1900"))
                                                                                ((TextBox)z).Text = Convert.ToDateTime(theDR["DateField1"]).ToString("dd-MMM-yyyy");
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                        else if ("TXTDT2-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["DateField2"].ToString()) && Convert.ToDateTime(theDR["DateField2"]) != DateTime.Parse("1/1/1900"))
                                                                                ((TextBox)z).Text = Convert.ToDateTime(theDR["DateField2"]).ToString("dd-MMM-yyyy");
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                        else if ("TXTNUM-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["NumericField"].ToString()) && theDR["NumericField"].ToString() != "0")
                                                                                ((TextBox)z).Text = theDR["NumericField"].ToString();
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (z.GetType() == typeof(HtmlInputText))
                                                    {
                                                        if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                                        {
                                                            ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }






        /// <summary>
        /// Handles the Click event of the btnDynDQ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDynDQ_Click(object sender, EventArgs e)
        {
            string[] id = ((Button)sender).ID.Split('-');
            int tabId = Convert.ToInt32(id[1]);
            ViewState["TabId"] = tabId;
            TabContainer container = tabContainer;
            //ConFieldEnableDisable(container);
            //Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, tabId));
            theDS.Tables.Add(ReadARVMedicationTable(container, tabId));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, tabId));

            if (FieldValidation() == false)
            {
                hdnPrevTabId.Value = tabId.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                return;
            }
            string msg = ValidationMessage(theDS, tabId);
            if (msg.Length > 51)
            {
                hdnPrevTabId.Value = tabId.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(PatientID, theDS, 1, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                DQCheck(PatientID, Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["ServiceLocationId"]));
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                SaveCancel();
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int FeatureID = Convert.ToInt32(Session["FeatureID"]);
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS, 1, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                DQCheck(PatientID, Convert.ToInt32(Session["PatientVisitId"]), LocationID);
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                UpdateCancel();
            }
            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue1", "StringASCII(" + hdnPrevTabId.Value + ");", true);
            hdnPrevTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
        }

        ///////////////////////////////////////////////////////////////////
        /// <summary>
        /// Handles the Click event of the btnDynSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDynSave_Click(object sender, EventArgs e)
        {
            string[] Id = ((Button)sender).ID.Split('-');
            int tabId = Convert.ToInt32(Id[1]);
            ViewState["TabId"] = tabId;
            TabContainer container = tabContainer;
            //ConFieldEnableDisable(container);
            //Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, tabId));
            theDS.Tables.Add(ReadARVMedicationTable(container, tabId));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, tabId));
            int a = container.ActiveTabIndex;
            if (FieldValidation() == false)
            {
                hdnPrevTabId.Value = tabId.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                return;
            }
            string msg = ValidationMessage(theDS, tabId);
            if (msg.Length > 51)
            {
                hdnPrevTabId.Value = tabId.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                int patientId = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(patientId, theDS, 0, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                SaveCancel();
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int featureId = Convert.ToInt32(Session["FeatureID"]);
                int patientId = Convert.ToInt32(Session["PatientId"]);
                int visitId = Convert.ToInt32(Session["PatientVisitId"]);
                int locationId = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(patientId, featureId, visitId, locationId, theDS, 0, tabId);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, tabId);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                UpdateCancel();
            }

            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue1", "StringASCII(" + hdnPrevTabId.Value + ");", true);
            hdnPrevTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
        }

        /// <summary>
        /// Checks the ab normal status.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        private bool CheckAbNormalStatus(string id, string Value)
        {
            bool status = false;
            string ar = id;
            string[] arVal = ar.Split('-');
            DataTable theDTNew = (DataTable)ViewState["BusRule"];
            DataView FilterAbVal = theDTNew.DefaultView;
            FilterAbVal.RowFilter = "FieldID=" + arVal[3].ToString();
            DataTable dtNewVal = FilterAbVal.ToTable();
            if (dtNewVal.Rows.Count == 4)
            {
                string MaxValue = "", MinValue = "", MaxNormal = "", MinNormal = "", TextValue = "";
                int MaxValue1 = 0, MinValue1 = 0, MaxNormal1 = 0, MinNormal1 = 0;
                double TextValue1 = 0;
                foreach (DataRow dr in dtNewVal.Rows)
                {
                    if (dr["BusRuleID"].ToString() == "2")
                    {
                        MaxValue = dr["Value"].ToString();
                        MaxValue1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "3")
                    {
                        MinValue = dr["Value"].ToString();
                        MinValue1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "26")
                    {
                        MaxNormal = dr["Value"].ToString();
                        MaxNormal1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "27")
                    {
                        MinNormal = dr["Value"].ToString();
                        MinNormal1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    TextValue = Convert.ToString(Value);
                    TextValue1 = Convert.ToDouble(Value);
                }

                if (MaxValue != "" && MinValue != "" && MaxNormal != "" && MinNormal != "")
                {
                    if ((TextValue1 <= MaxValue1) && (TextValue1 > MaxNormal1))
                    {
                        status = true;
                        // ((TextBox)y).ForeColor = System.Drawing.Color.Red;
                    }
                    else if ((TextValue1 < MinNormal1) && (TextValue1 >= MinValue1))
                    {
                        status = true;
                        //((TextBox)y).ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        status = false;
                        // ((TextBox)y).ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            return status;
        }

        /// <summary>
        /// Checks the control.
        /// </summary>
        /// <param name="theCntrl">The CNTRL.</param>
        /// <param name="theId">The identifier.</param>
        private void CheckControl(Control theCntrl, string[] theId)
        {
            string theCntrlType = theId[0];
            //Calling for HTML check box Event
            foreach (object obj in theCntrl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object y in c.Controls)
                            {
                                foreach (Control x in ((Control)y).Controls)
                                {
                                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                        CheckControl(x, theId);
                                    else
                                    {
                                        if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.WebControls.CheckBox")
                                        {
                                            HtmlCheckBoxSelect(x);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Calling for HTML Radio Button Event
            foreach (object obj in theCntrl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (Control x in c.Controls)
                            {
                                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                    CheckControl(x, theId);
                                else
                                {
                                    if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                                    {
                                        HtmlRadioButtonSelect(x);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cons the field enable disable.
        /// </summary>
        /// <param name="theControl">The control.</param>
        private void ConFieldEnableDisable(Control theControl)
        {
            foreach (object obj in theControl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object y in c.Controls)
                            {
                                foreach (Control x in ((Control)y).Controls)
                                {
                                    //if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                    //{
                                    //    ConFieldEnableDisable(x);
                                    //}
                                    //else
                                    //{
                                    if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                            string[] theId = ((CheckBox)x).ID.Split('-');
                                            if (theId.Length == 5)
                                            {
                                                theDVConditionalField.RowFilter = "ConditionalFieldSectionId=" + theId.GetValue(1);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                        RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                                    }
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(x);
                                                }
                                            }
                                        }
                                    }
                                    // }
                                }

                                if (y.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                                {
                                    if (((HtmlInputRadioButton)y).Checked == true && ((HtmlInputRadioButton)y).Value == "Yes")
                                    {
                                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                        string[] theId = ((HtmlInputRadioButton)y).ID.Split('-');
                                        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                        if (theDVConditionalField.Count > 0)
                                        {
                                            foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                            {
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                            }
                                            EventArgs s = new EventArgs();
                                            this.HtmlRadioButtonSelect(y);
                                        }
                                    }

                                }
                                else if (y.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                                {
                                    if (((DropDownList)y).SelectedValue != "0")
                                    {
                                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                        string[] theId = ((DropDownList)y).ID.Split('-');
                                        if (!((DropDownList)y).SelectedValue.Contains("AM"))
                                        {
                                            if (!((DropDownList)y).SelectedValue.Contains("PM"))
                                            {
                                                theDVConditionalField.RowFilter = "ConditionalFieldSectionId=" + ((DropDownList)y).SelectedValue;
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                        RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (y.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)y).Text != "")
                                    {
                                        bool FlagVal = CheckAbNormalStatus(((TextBox)y).ID, ((TextBox)y).Text);
                                        if (FlagVal == true)
                                        {
                                            ((TextBox)y).ForeColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            ((TextBox)y).ForeColor = System.Drawing.Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates the columnthe dtic D10.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateColumntheDTICD10()
        {
            DataTable theDTICD10 = new DataTable();
            theDTICD10.Columns.Add("FieldId", typeof(int));
            theDTICD10.Columns.Add("BlockId", typeof(int));
            theDTICD10.Columns.Add("SubBlockId", typeof(int));
            theDTICD10.Columns.Add("Id", typeof(int));
            theDTICD10.Columns.Add("CodeId", typeof(string));
            theDTICD10.Columns.Add("Name", typeof(string));
            return theDTICD10;
        }

        /// <summary>
        /// Creates the date image.
        /// </summary>
        /// <param name="theControl">The control.</param>
        /// <param name="controlId">The control identifier.</param>
        /// <param name="theConField">if set to <c>true</c> [the con field].</param>
        /// <param name="MMMYYYY">if set to <c>true</c> [mmmyyyy].</param>
        private void CreateDateImage(object theControl, string controlId, bool theConField, bool MMMYYYY)
        {
            string[] field = ((Control)theControl).ID.Split('-');
            DataTable dtBusinessRules = (DataTable)ViewState["BusRule"];
            TextBox theDateText = (TextBox)theControl;
            foreach (DataRow rule in dtBusinessRules.Rows)
            {
                if (field[1] == Convert.ToString(rule["FieldName"]) && Convert.ToString(rule["BusRuleId"]) == "21" && MMMYYYY == false)
                {
                    theDateText = (TextBox)theControl;
                    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
                    theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4')");
                    DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(MMM-YYYY)</span>"));
                    MMMYYYY = true;
                }
            }
            if (MMMYYYY == false)
            {
                theDateText = (TextBox)theControl;
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                Image theDateImage = new Image();
                theDateImage.ID = "img" + theDateText.ID;
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                //theDateImage.Visible = theEnable;
                theDateImage.ToolTip = "Date Helper";
                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                tabContainer.ID = "TAB";
                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)theControl).ClientID + "');");
                DIVCustomItem.Controls.Add(new LiteralControl("&nbsp;"));
                DIVCustomItem.Controls.Add(theDateImage);
                // ApplyBusinessRules(theDateImage, ControlID, theEnable);
                DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
            }
        }

        /// <summary>
        /// Creates the selected table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", Type.GetType("System.String"));
            theDT.Columns.Add("Generic", Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }

        /// <summary>
        /// Creates the tab.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void CreateTab(DataTable theDT)
        {
            tabContainer = new TabContainer();
            tabContainer.CssClass = "ajax__tab_technorati-theme";
            //tabcontainer.Height = Unit.Pixel(200);
            foreach (DataRow theDR in theDT.Rows)
            {
                tbChildPanel = new TabPanel();
                tbChildPanel.HeaderText = theDR["TabName"].ToString();
                tbChildPanel.ID = theDR["TabId"].ToString();
                tabContainer.Tabs.Add(tbChildPanel);
            }
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private DataTable CreateTable(string[] value)
        {
            DataTable TmpDT = new DataTable();
            DataColumn ID = new DataColumn();
            ID.DataType = Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            TmpDT.Columns.Add(ID);

            DataColumn Name = new DataColumn();
            Name.DataType = Type.GetType("System.String");
            Name.ColumnName = "Name";
            TmpDT.Columns.Add(Name);

            DataRow tmpdr;

            for (int i = 1; i < value.Length + 1; i++)
            {
                tmpdr = TmpDT.NewRow();
                tmpdr[0] = i;
                tmpdr[1] = value[i - 1];
                TmpDT.Rows.Add(tmpdr);
            }
            return TmpDT;
        }

        /// <summary>
        /// Customs the form add regimen.
        /// </summary>
        /// <param name="regTypeId">The reg type identifier.</param>
        /// <param name="strregime">The strregime.</param>
        /// <param name="sessionName">Name of the session.</param>
        /// <param name="controlId">The control identifier.</param>
        private void CustomFormAddRegimen(int regTypeId, string strregime, string sessionName, string controlId)
        {
            try
            {

                DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                DataTable theDTCustomFrmReg = theDV.ToTable();
                // DataTable theDTCustomFrmReg = (DataTable)Session["Reg" + controlID + RegTypeID + ""];
                string[] eachregime = strregime.Split('/');
                DataRow theDR;
                DataTable theDT = PtnCustomformselectedDataTable();
                for (int i = 0; i < eachregime.Length; i++)
                {
                    DataView dv = new DataView(theDTCustomFrmReg);
                    dv.RowFilter = "Abbr ='" + eachregime[i] + "'";
                    if (dv.Count > 0)
                    {
                    }
                    else
                    {
                        dv = new DataView(theDTCustomFrmReg);
                        dv.RowFilter = "DrugName ='" + eachregime[i] + "'";
                    }
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(dv[0][0].ToString());
                    theDR[1] = dv[0][1].ToString();
                    theDR[2] = dv[0][2].ToString();
                    theDR[4] = dv[0][4].ToString();
                    theDT.Rows.Add(theDR);
                }
                Session[sessionName] = theDT;
            }
            catch
            {
            }
            finally { }
        }

        /// <summary>
        /// Customs the form regimen.
        /// </summary>
        /// <param name="regTypeId">The reg type identifier.</param>
        private void CustomFormRegimen(int regTypeId)
        {
            BindFunctions theBind = new BindFunctions();
            DataTable theDTCustomFrmReg = (DataTable)Session["Reg" + ViewState["ControlId"].ToString() + regTypeId + ""];
            DataTable theDTCustomFrmSelectedReg = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + regTypeId + ""];
            foreach (DataRow theDR in theDTCustomFrmReg.Rows)
            {
                if (Convert.ToInt32(theDR["Generic"]) > 0) // if generic
                {
                    if ((theDR["Abbr"].ToString() != "") && (theDR["DrugName"].ToString().LastIndexOf(']')) < 1)
                    {
                        theDR["DrugName"] = theDR["DrugName"].ToString() + "-[" + theDR["Abbr"].ToString() + "]";
                    }
                    else
                    {
                        theDR["DrugName"] = theDR["DrugName"].ToString();
                    }
                }
            }
            foreach (DataRow theSelectedDR in theDTCustomFrmSelectedReg.Rows)
            {
                if (Convert.ToInt32(theSelectedDR["Generic"]) > 0) // if generic
                {
                    if (theSelectedDR["DrugAbbr"].ToString() != "" && (theSelectedDR["DrugName"].ToString().LastIndexOf(']')) < 1)
                    {
                        theSelectedDR["DrugName"] = theSelectedDR["DrugName"].ToString() + "-[" + theSelectedDR["DrugAbbr"].ToString() + "]";
                    }
                    else
                    {
                        theSelectedDR["DrugName"] = theSelectedDR["DrugName"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlSelectList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabContainer container = tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            DropDownList theDList = ((DropDownList)sender);
            DataSet theDS = (DataSet)Session["AllData"];
            string[] theCntrl = theDList.ID.Split('-');

            foreach (DataRow theDR in theDS.Tables[17].Rows) //Dtcon.Rows)
            {
                foreach (object obj in tabContainer.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in c.Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if ((theIdent.Length > 2) || (x.ID.StartsWith("Pnl_")))
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((TextBox)x).Enabled = true;
                                                    ApplyBusinessRules(x, "1", true);
                                                    ApplyBusinessRules(x, "2", true);
                                                    ApplyBusinessRules(x, "3", true);
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }

                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }

                                                if (theIdent[0] == "TXTReg")
                                                {
                                                    ((TextBox)x).Enabled = true;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "-" + theDR["TabId"].ToString())
                                                    {
                                                        if (theIdent[0].ToString() == "SELECTLISTAuto")
                                                        {
                                                            ((DropDownList)x).Enabled = false;
                                                        }
                                                        else
                                                        {
                                                            ((DropDownList)x).Enabled = true;
                                                            ApplyBusinessRules(x, "4", true);
                                                        }
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    if (((DropDownList)x).ID.Contains("AMPM") == false)
                                                    {
                                                        ((DropDownList)x).SelectedValue = "0";
                                                    }
                                                }
                                            }
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] + theIdent[1] + theIdent[2] == "Pnl_" + theDR["PdfTableName"].ToString() + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the form.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        private void DeleteForm(int patientId, int visitId)
        {
            ICustomForm CustomManager = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            int theResultRow = CustomManager.DeleteForm("Custom", visitId, patientId, Convert.ToInt32(Session["AppUserId"].ToString()));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToString(patientId));
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Dqs the cancel.
        /// </summary>
        private void DQCancel()
        {
            ViewState["btcolor"] = '1';
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (base.Session["Redirect"].ToString() == "0")
            {
                script += "window.location.href='frmPatient_Home.aspx';\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';\n";
            }
            script += "}\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        /// <summary>
        /// Dqs the check.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        private void DQCheck(int patientId, int visitId, int locationId)
        {
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            string GetValue = "Select * from LNK_FORMTABORDVISIT where Visit_Pk=" + visitId + "";
            DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            DataTable theDTDQStatus = ((DataSet)Session["AllData"]).Tables[23];
            int TabCount = theDTDQStatus.Rows.Count;
            int SavedTabCount = 0;
            for (int i = 0; i < TempDS.Tables[0].Rows.Count; i++)
            {
                if (TempDS.Tables[0].Rows[i]["DataQuality"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(TempDS.Tables[0].Rows[i]["DataQuality"]) == 1)
                    {
                        SavedTabCount++;
                    }
                }
            }
            if (TabCount == SavedTabCount && TempDS.Tables[0].Rows.Count > 0)
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=1 where ptn_pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
            else if (TrSignatureAll.Visible == true)
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=1 where ptn_pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
            else
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=0 where ptn_pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
        }

        /// <summary>
        /// Dqs the message.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        /// <returns></returns>
        private string DQMessage(DataSet theDS)
        {
         
            DateTime theCurrentDate = SystemSetting.SystemDate;
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            DataTable dtBusinessRule = (DataTable)ViewState["BusRule"];
            string radio1 = "", radio2 = "", multiSelectName = "", multiSelectLabel = "";
            int TotCount = 0, falseCount = 0;
            try
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    string scriptblankvisitdate = "<script language = 'javascript' defer ='defer' id = 'Colorlblvisitdate'>\n";
                    scriptblankvisitdate += "To_Change_Color('lblvisitdate');\n";
                    scriptblankvisitdate += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Colorlblvisitdate", scriptblankvisitdate);
                    strmsg += " Visit Date is Blank";
                    strmsg = strmsg + "\\n";
                }
                TabContainer container = tabContainer;
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                //foreach (Control x in DIVCustomItem.Controls)
                                foreach (object x in c.Controls)
                                {
                                    if (x.GetType() == typeof(TextBox))
                                    {
                                        string[] field = ((TextBox)x).ID.Split('-');

                                        foreach (DataRow rule in dtBusinessRule.Rows)
                                        {
                                            if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                                            {
                                                string[] field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                                if (field10[1] == Convert.ToString(rule["FieldName"]) && field10[2] == Convert.ToString(rule["TableName"]) && field10[3] == Convert.ToString(rule["FieldId"]) && (Convert.ToString(rule["BusRuleId"]) == "13" || Convert.ToString(rule["BusRuleId"]) == "1"))
                                                {
                                                    if (((TextBox)x).Text == "")
                                                    {
                                                        string scriptblankmultitext = "<script language = 'javascript' defer ='defer' id = 'Color" + rule["FieldLabel"] + rule["FieldId"] + "'>\n";
                                                        scriptblankmultitext += "To_Change_Color('lbl" + rule["FieldLabel"] + "-" + rule["FieldId"] + "');\n";
                                                        scriptblankmultitext += "</script>\n";
                                                        ClientScript.RegisterStartupScript(this.GetType(), "Color" + rule["FieldLabel"] + rule["FieldId"], scriptblankmultitext);
                                                        strmsg += rule["FieldLabel"] + " is Blank";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            if (field[1] == Convert.ToString(rule["FieldName"]) && field[2] == Convert.ToString(rule["TableName"]) && field[3] == Convert.ToString(rule["FieldId"]) && (Convert.ToString(rule["BusRuleId"]) == "13" || Convert.ToString(rule["BusRuleId"]) == "1"))
                                            {
                                                if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptblanktext = "<script language = 'javascript' defer ='defer' id = 'Color" + rule["FieldLabel"] + rule["FieldId"] + "'>\n";
                                                    scriptblanktext += "To_Change_Color('lbl" + rule["FieldLabel"] + "-" + rule["FieldId"] + "');\n";
                                                    scriptblanktext += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + rule["FieldLabel"] + rule["FieldId"], scriptblanktext);
                                                    //}
                                                    strmsg += rule["FieldLabel"] + " is Blank";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }

                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputRadioButton))
                                    {
                                        string[] field = ((HtmlInputRadioButton)x).ID.Split('-');
                                        if (field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            radio1 = field[3];
                                        }
                                        if (field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            radio2 = field[3];
                                        }

                                        foreach (DataRow theDR in dtBusinessRule.Rows)
                                        {
                                            if (radio1 == field[3] && radio2 == field[3])
                                            {
                                                if (field[1] == Convert.ToString(theDR["FieldName"]) && field[2] == Convert.ToString(theDR["TableName"]) && field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptradio = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptradio += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptradio += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptradio);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(DropDownList))
                                    {
                                        string[] field = ((DropDownList)x).ID.Split('-');
                                        foreach (DataRow theDR in dtBusinessRule.Rows)
                                        {
                                            if (field[1] == Convert.ToString(theDR["FieldName"]) && field[2] == Convert.ToString(theDR["TableName"]) && field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && (field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptdropdown = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptdropdown += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptdropdown += "</script>\n";
                                                    // ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptdropdown);
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptdropdown);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputCheckBox))
                                    {
                                        string[] field = ((HtmlInputCheckBox)x).ID.Split('-');
                                        foreach (DataRow theDR in dtBusinessRule.Rows)
                                        {
                                            if (field[1] == Convert.ToString(theDR["FieldName"]) && field[2] == Convert.ToString(theDR["TableName"]) && field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                if (((HtmlInputCheckBox)x).Checked == false)
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptHtmlchkbox = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptHtmlchkbox += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptHtmlchkbox += "</script>\n";
                                                    //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptHtmlchkbox);

                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptHtmlchkbox);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    //if (x.GetType() == typeof(System.Web.UI.WebControls.Panel) && x.ID.StartsWith("Pnl_") == true)
                                    if (x.GetType() == typeof(Panel) && ((Panel)x).ID.StartsWith("Pnl_") == true)
                                    {
                                        string[] field = ((Panel)x).ID.Split('_');
                                        foreach (DataRow rule in dtBusinessRule.Rows)
                                        {
                                            if (field[1] == rule["FieldId"].ToString() && ((Panel)x).ToolTip.ToString() == rule["FieldLabel"].ToString() && (Convert.ToString(rule["BusRuleId"]) == "13" || Convert.ToString(rule["BusRuleId"]) == "1"))
                                            {
                                                int noChecks = 0;
                                                foreach (Control theCntrl in ((Panel)x).Controls)
                                                {
                                                    if (theCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                                    {
                                                        if (((CheckBox)theCntrl).Checked == true)
                                                            noChecks = noChecks + 1;
                                                    }
                                                }

                                                if (noChecks == 0)
                                                {
                                                    string scriptMultiSelect = "<script language = 'javascript' defer ='defer' id = 'Color" + rule["FieldLabel"].ToString() + rule["FieldId"].ToString() + "'>\n";
                                                    scriptMultiSelect += "To_Change_Color('lbl" + rule["FieldLabel"].ToString() + "-" + rule["FieldId"].ToString() + "');\n";
                                                    scriptMultiSelect += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + rule["FieldLabel"].ToString() + rule["FieldId"].ToString(), scriptMultiSelect);
                                                    //}
                                                    strmsg += rule["FieldLabel"].ToString() + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HiddenField))
                                    {
                                        string[] field = ((HiddenField)x).ID.Split('-');

                                        if (field.Length == 4)
                                        {
                                            foreach (DataRow theDR in dtBusinessRule.Rows)
                                            {
                                                if (field[3] == Convert.ToString(theDR["FieldName"]) && field[2] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                                {
                                                    if (theDS.Tables[0].Rows.Count == 0)
                                                    {
                                                        //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                        //{
                                                        string scripthiddenfields = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                        scripthiddenfields += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                        scripthiddenfields += "</script>\n";
                                                        //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scripthiddenfields);
                                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scripthiddenfields);
                                                        //}
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }

                                        if (field.Length == 5)
                                        {
                                            foreach (DataRow rule in dtBusinessRule.Rows)
                                            {
                                                if (field[3] == Convert.ToString(rule["FieldName"]) && field[2] == Convert.ToString(rule["FieldId"]) && Convert.ToString(rule["BusRuleId"]) == "13" && Convert.ToString(rule["Value"]) == "37")
                                                {
                                                    if (theDS.Tables[1].Rows.Count == 0)
                                                    {
                                                        if (rule["Value"].ToString() != "")
                                                        {
                                                            DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                            theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(rule["Value"]).ToString();
                                                            DataTable theDrugNameDT = theDV.ToTable();
                                                            strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                            strmsg = strmsg + "\\n";
                                                        }
                                                    }
                                                }
                                                else if (field[3] == Convert.ToString(rule["FieldName"]) && field[2] == Convert.ToString(rule["FieldId"]) && (Convert.ToString(rule["BusRuleId"]) == "13" || Convert.ToString(rule["BusRuleId"]) == "1") && Convert.ToString(rule["Value"]) != "37")
                                                {
                                                    if (theDS.Tables[2].Rows.Count == 0)
                                                    {
                                                        if (rule["Value"].ToString() != "")
                                                        {
                                                            DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                            theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(rule["Value"].ToString()).ToString();
                                                            DataTable theDrugNameDT = theDV.ToTable();
                                                            strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                            strmsg = strmsg + "\\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }//
                            }
                        }
                    }
                }
                //MultiSelect Validation
                foreach (object obj in container.Controls)
                {
                    if (obj is TabPanel)
                    {
                        TabPanel tabPanel = (TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control y in ((Control)ctrl).Controls)
                                {
                                    if (y.GetType() == typeof(Panel) && ((Panel)y).ID.StartsWith("Pnl_") == true)
                                    {
                                        string[] field = ((Panel)y).ID.Split('-');
                                        foreach (Control z in y.Controls)
                                        {
                                            if (z.GetType() == typeof(CheckBox))
                                            {
                                                TotCount++;
                                                if (((CheckBox)z).Checked == false)
                                                {
                                                    falseCount++;
                                                }
                                            }
                                        }
                                        foreach (DataRow theMultiDR in dtBusinessRule.Rows)
                                        {
                                            if (Convert.ToString(theMultiDR["ControlId"]) == "9" && field[2] == Convert.ToString(theMultiDR["FieldID"]) && (Convert.ToInt32(theMultiDR["BusRuleId"]) == 13 || Convert.ToInt32(theMultiDR["BusRuleId"]) == 1))
                                            {
                                                multiSelectName = Convert.ToString(theMultiDR["Name"]);
                                                multiSelectLabel = Convert.ToString(theMultiDR["FieldLabel"]);
                                                if (TotCount == falseCount)
                                                {
                                                    string scriptMultiSelect = "<script language = 'javascript' defer ='defer' id = 'Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"] + "'>\n";
                                                    scriptMultiSelect += "To_Change_Color('lbl" + theMultiDR["FieldLabel"] + "-" + theMultiDR["FieldId"] + "');\n";
                                                    scriptMultiSelect += "</script>\n";
                                                    //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"], scriptMultiSelect);

                                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"], scriptMultiSelect);
                                                    strmsg += multiSelectLabel + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }

                                        TotCount = 0; falseCount = 0;
                                        multiSelectName = ""; multiSelectLabel = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally { }

            return strmsg;
        }

        /// <summary>
        /// Drugs the data binding.
        /// </summary>
        /// <param name="BtnId">The BTN identifier.</param>
        /// <param name="drugTypeId">The drug type identifier.</param>
        private void DrugDataBinding(string BtnId, int drugTypeId)
        {
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);
            //int PatientID = Convert.ToInt32(Session["PatientId"]);
            DataSet theDSDrug = new DataSet();
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            StringBuilder StrDrug = new StringBuilder();
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" Inner join Vw_Drug c on b.Drug_Pk = c.Drug_Pk");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + visitId + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientId + "')");
            StrDrug.Append(" UNION ");
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" Inner join Vw_Generic c on b.GenericId = c.GenericId");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + visitId + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientId + "')");
            StrDrug.Append(" Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk ");
            StrDrug.Append(" inner join lnk_drugtypegeneric c on c.GenericId=b.GenericId");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + visitId + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientId + "')");
            StrDrug.Append(" UNION ");
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" inner join vw_drug c on b.Drug_Pk=c.Drug_pk");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + visitId + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientId + "')");
            theDSDrug = MgrBindValue.Common_GetSaveUpdate(StrDrug.ToString());

            DataTable dtARVDrug = ARVDrug();
            foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
            {
                DataRow tmpDR = dtARVDrug.NewRow();
                tmpDR[0] = thedr["Drug_Pk"];
                tmpDR[1] = thedr["GenericID"];
                tmpDR[2] = thedr["SingleDose"];
                tmpDR[3] = thedr["FrequencyID"];
                tmpDR[4] = thedr["Duration"];
                tmpDR[5] = thedr["OrderedQuantity"];
                tmpDR[6] = thedr["DispensedQuantity"];
                tmpDR[7] = thedr["Financed"];
                tmpDR[8] = thedr["DrugTypeId"];
                dtARVDrug.Rows.Add(tmpDR);
            }
            DataView theDVARV = new DataView(dtARVDrug);
            //theDVARV.RowFilter = "DrugTypeId=" + DrugTypeId;
            theDVARV.RowFilter = "DrugTypeId=37";
            DataTable theARVDT = theDVARV.ToTable();

            //Setting Session
            if (!IsPostBack)
            {
                if (drugTypeId == 37)
                {
                    DataTable theDTARVDrug = PtnCustomformselectedDataTableDrug(theARVDT, drugTypeId);
                    Session["Selected" + DrugType + ""] = theDTARVDrug;
                }
            }

            foreach (DataRow drgdr in theARVDT.Rows)
            {
                int drugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
                if ((DataTable)Session["Selected" + DrugType + ""] != null)
                {
                    foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                    {
                        if (drugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                        {
                            BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                        }
                    }
                }
            }

            foreach (DataRow drgdr1 in theARVDT.Rows)
            {
                FillDrugData(DIVCustomItem, drgdr1);
            }

            ///Section for NON ARV Drug
            DataTable dtNonARVDrug = NonARVDrug();
            //foreach (DataRow thedr in theDSDrug.Tables[1].Rows)
            foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
            {
                DataRow theRow = dtNonARVDrug.NewRow();
                theRow[0] = thedr["Drug_pk"];
                theRow[1] = thedr["GenericId"];
                //theRow[2] = thedr["UnitId"];
                theRow[2] = 0;
                theRow[3] = thedr["FrequencyID"];
                theRow[4] = thedr["SingleDose"];
                theRow[5] = thedr["Duration"];
                theRow[6] = thedr["OrderedQuantity"];
                theRow[7] = thedr["DispensedQuantity"];
                theRow[8] = thedr["Financed"];
                theRow[9] = thedr["DrugTypeId"];
                dtNonARVDrug.Rows.Add(theRow);
            }

            DataView theDV = new DataView(dtNonARVDrug);
            theDV.RowFilter = "DrugTypeId<>37";
            DataTable theNonARVDT = theDV.ToTable();

            if (!IsPostBack)
            {
                //Setting Session
                //if (DrugTypeId != 37 && DrugTypeId!=36)
                if (drugTypeId != 37)
                {
                    DataTable theDTNonARVDrug = PtnCustomformselectedDataTableDrug(theNonARVDT, drugTypeId);
                    Session["Selected" + DrugType + ""] = theDTNonARVDrug;
                }
            }

            foreach (DataRow drgdr in theNonARVDT.Rows)
            {
                int drugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
                if ((DataTable)Session["Selected" + DrugType + ""] != null)
                {
                    foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                    {
                        if (drugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                        {
                            BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                        }
                    }
                }
            }
            foreach (DataRow drgdr1 in theNonARVDT.Rows)
            {
                FillDrugData(DIVCustomItem, drgdr1);
            }
        }

        /// <summary>
        /// Drugses the heading.
        /// </summary>
        /// <param name="drugType">Type of the drug.</param>
        private void DrugsHeading(int drugType)
        {
            Panel thelblPnl = new Panel();

            #region "ARV Medication"

            if (thelblPnl.Controls.Count < 1 && (drugType == 37 || drugType == 36))
            {
                Panel PnlHeading = new Panel();
                PnlHeading.ID = "pnlARV" + drugType;
                PnlHeading.Height = 20;
                PnlHeading.Width = 840;
                PnlHeading.Font.Bold = true;
                thelblPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + drugType;
                theSP.Width = 5;
                theSP.Text = "";
                PnlHeading.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + drugType;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 410;
                PnlHeading.Controls.Add(theLabel1);

                //Label theLabel2 = new Label();
                //theLabel2.ID = "lblDrgDose" + DrugType;
                //theLabel2.Text = "Dose";
                //theLabel2.Width = 100;
                //PnlHeading.Controls.Add(theLabel2);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + drugType;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 95;
                PnlHeading.Controls.Add(theLabel4);

                //Label theLabel5 = new Label();
                //theLabel5.ID = "lblDrgDuration" + DrugType;
                //theLabel5.Text = "Duration";
                //theLabel5.Width = 120;
                //theLabel5.CssClass = "required";
                //PnlHeading.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + drugType;
                theLabel6.Text = "Qty. Prescribed";
                theLabel6.Width = 120;
                PnlHeading.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + drugType;
                theLabel7.Text = "Qty. Dispensed";
                theLabel7.Width = 110;
                PnlHeading.Controls.Add(theLabel7);

                Label theFinLbl = new Label();
                theFinLbl.ID = "lblAddARVFin" + drugType;
                theFinLbl.Text = "Prophylaxis";
                PnlHeading.Controls.Add(theFinLbl);
                DIVCustomItem.Controls.Add(PnlHeading);
            }

            #endregion "ARV Medication"

            #region "Non-ARV Medication"

            else if (thelblPnl.Controls.Count < 1 && (drugType != 37 && drugType != 36))
            {
                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderOtherDrug" + drugType; ;
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 840;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + drugType; ;
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + drugType; ;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 360;
                theheaderPnl.Controls.Add(theLabel1);

                Label theSP1 = new Label();
                theSP1.ID = "lblDrgSp1" + drugType; ;
                theSP1.Width = 10;
                theSP1.Text = "";
                theheaderPnl.Controls.Add(theSP1);

                //Label theLabel2 = new Label();
                //theLabel2.ID = "lblDrgDose" + DrugType; ;
                //theLabel2.Text = "Dose";
                //theLabel2.Width = 62;
                //theheaderPnl.Controls.Add(theLabel2);

                //Label theSP2 = new Label();
                //theSP2.ID = "lblDrgSp2" + DrugType; ;
                //theSP2.Width = 30;
                //theSP2.Text = "";
                //theheaderPnl.Controls.Add(theSP2);

                //Label theLabel3 = new Label();
                //theLabel3.ID = "lblDrgUnits" + DrugType; ;
                //theLabel3.Text = "Unit";
                //theLabel3.Width = 88;
                //theheaderPnl.Controls.Add(theLabel3);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + drugType; ;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 90;
                theheaderPnl.Controls.Add(theLabel4);

                //Label theLabel5 = new Label();
                //theLabel5.ID = "lblDrgDuration" + DrugType; ;
                //theLabel5.Text = "Duration";
                //theLabel5.Width = 100;
                //theLabel5.CssClass = "required";
                //theheaderPnl.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + drugType; ;
                theLabel6.Text = "Qty. Prescribed";
                theLabel6.Width = 100;
                theheaderPnl.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + drugType; ;
                theLabel7.Text = "Qty. Dispensed";
                theLabel7.Width = 100;
                theheaderPnl.Controls.Add(theLabel7);

                Label theLabel8 = new Label();
                theLabel8.ID = "lblDrgFinanced" + drugType; ;
                theLabel8.Text = "Prophylaxis";
                theLabel8.Width = 10;
                theheaderPnl.Controls.Add(theLabel8);
                DIVCustomItem.Controls.Add(theheaderPnl);
            }

            #endregion "Non-ARV Medication"
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private bool FieldValidation()
        {
            DateTime theCurrentDate = SystemSetting.SystemDate;           
            TabContainer container = tabContainer;
            DataTable dtCustomitems = this.SetControlIDs(container);
            // todo
            if (this.IsSingleVisit == false)
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Visit Date";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
            else
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Date encounter";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (Session["RegDate"] != null && txtvisitDate.Text != "")
                {
                    if (Convert.ToDateTime(txtvisitDate.Text) < Convert.ToDateTime(Session["RegDate"]))
                    {
                        txtvisitDate.Focus();
                        MsgBuilder totalMsgBuilder = new MsgBuilder();
                        totalMsgBuilder.DataElements["MessageText"] = "Encounter Date should not be less then registration date";
                        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                        return false;
                    }
                }
            }

            ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataSet theDS = MgrValidate.Validate(lblFeatureName.Text, txtvisitDate.Text, this.PatientId,this.ModuleId);

            // todo
            //if (txtvisitDate.Text != "01-01-1900")
            if (IsSingleVisit == false)
            {
                DateTime VisitDate = Convert.ToDateTime((txtvisitDate.Text));
                DateTime RegisDate = Convert.ToDateTime(theDS.Tables[1].Rows[0]["StartDate"]);
                if (VisitDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("CompareDate5", this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (VisitDate < RegisDate)
                {
                    IQCareMsgBox.Show("PMTCTCustomRegDateValidate", this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    if (txtvisitDate.Text != "")
                    {
                        if (txtvisitDate.Text != Convert.ToString(ViewState["VisitDate"]))
                        {
                            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                            {
                                IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (txtvisitDate.Text != "")
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                        {
                            IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                            return false;
                        }
                    }
                }
            }
          
            return true;
        }

        /// <summary>
        /// Fills the drug data.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        /// <param name="theDR">The dr.</param>
        private void FillDrugData(Control Cntrl, DataRow theDR)
        {
            foreach (Control z in Cntrl.Controls)
            {
                if (z.GetType() == typeof(Panel))
                {
                    foreach (Control x in z.Controls)
                    {
                        if (x.GetType() == typeof(DropDownList))
                        {
                            if (x.ID.StartsWith("theUnitDrug" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["UnitId"].ToString();
                            }

                            if (x.ID.StartsWith("theUnitGeneric" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["UnitId"].ToString();
                            }

                            if (x.ID.StartsWith("drugFrequency" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyID"].ToString();
                            }
                            if (x.ID.StartsWith("GenericFrequency" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }

                            if (x.ID.StartsWith("ARVdrgStrength" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]);
                            }
                            if (x.ID.StartsWith("ARVGenericStrength" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]);
                            }

                            if (x.ID.StartsWith("ARVdrgFrequency" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }
                            if (x.ID.StartsWith("ARVGenericFrequency" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }
                        }
                        else if (x.GetType() == typeof(TextBox))
                        {
                            if (x.ID.StartsWith("DrugDuration" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("GenericDuration" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("drugQtyPrescribed" + theDR["DrugId"] + ""))
                            {
                                //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                                }
                                else
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                }
                            }

                            if (x.ID.StartsWith("genericQtyPrescribed" + theDR["GenericId"] + ""))
                            {
                                //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                                }
                                else
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                }
                            }

                            if (x.ID.StartsWith("drugQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                            }

                            if (x.ID.StartsWith("genericQtyDispensed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                            }
                            if (x.ID.StartsWith("theDoseDrug" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                            }
                            if (x.ID.StartsWith("theDoseGeneric" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                            }

                            if (x.ID.StartsWith("theDoseGeneric" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }
                            if (x.ID.StartsWith("ARVdrgDuration" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericDuration" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("ARVdrgQtyPrescribed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericQtyPrescribed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }

                            if (x.ID.StartsWith("drgQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }
                            if (x.ID.StartsWith("ARVdrgQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericQtyDispensed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }
                        }
                        else if (x.GetType() == typeof(CheckBox))
                        {
                            if (x.ID.StartsWith("FinChkDrug" + theDR["DrugId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                            if (x.ID.StartsWith("FinChkGeneric" + theDR["GenericId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                            if (x.ID.StartsWith("ARVDrugFinChk" + theDR["DrugId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }

                            if (x.ID.StartsWith("ARVGenericFinChk" + theDR["GenericId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the lab data.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        /// <param name="theDR">The dr.</param>
        private void FillLabData(Control Cntrl, DataRow theDR)
        {
            int y = 0;
            foreach (Control z in Cntrl.Controls)
            {
                if (z.GetType() == typeof(Panel))
                {
                    foreach (Control x in z.Controls)
                    {
                        if (x.GetType() == typeof(TextBox))
                        {
                            if (x.ID.StartsWith("LabResult"))
                                y = Convert.ToInt32(x.ID.Substring(9, x.ID.Length - 9));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((TextBox)x).Text = theDR["TestResults1"].ToString();
                            }
                        }
                        else if (x.GetType() == typeof(DropDownList))
                        {
                            if (x.ID.StartsWith("ddlLabResult"))
                                y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((DropDownList)x).SelectedValue = theDR["TestResults1"].ToString();
                            }
                        }

                        //else if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        //{
                        //    if (x.ID.ToUpper().StartsWith("FinChkLab"))
                        //        y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                        //    if (y == Convert.ToInt32(theDR["SubTestId"]))
                        //    {
                        //        ((CheckBox)x).Checked = Convert.ToBoolean(theDR["Financed"]);
                        //    }
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// Fills the regimen.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <returns></returns>
        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToString(theDR["DrugAbbr"]) != "")
                {
                    if (theRegimen == "")
                    {
                        theRegimen = Convert.ToString(theDR["DrugAbbr"]);
                    }
                    else
                    {
                        theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugAbbr"]);
                    }
                }
                else
                {
                    if (theRegimen == "")
                    {
                        theRegimen = Convert.ToString(theDR["DrugName"]);
                    }
                    else
                    {
                        theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugName"]);
                    }
                }
            }
            return theRegimen;
        }

        /// <summary>
        /// Gets the type of the control data.
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        private Type GetControlDataType(int typeid)
        {
            Type typeofdata = typeof(string);
            if (typeid == 3 || typeid == 4 || typeid == 14 || typeid == 15)
            {
                typeofdata = typeof(int);
            }
            if (typeid == 2)
            {
                typeofdata = typeof(decimal);
            }
            if (typeid == 5)
            {
                typeofdata = typeof(DateTime);
            }
            if (typeid == 7 || typeid == 6)
            {
                typeofdata = typeof(bool);
            }
            return typeofdata;
        }

        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <param name="fieldId">The field identifier.</param>
        /// <param name="fieldLabel">The field label.</param>
        /// <returns></returns>
        private int GetFilterId(string fieldId, string fieldLabel)
        {
            int drugTypeId = 0;
            DataTable theDT = (DataTable)ViewState["BusRule"];
            foreach (DataRow DR in theDT.Rows)
            {
                if (Convert.ToString(DR["FieldID"]) == fieldId && Convert.ToString(DR["FieldName"]) == fieldLabel && (Convert.ToString(DR["BusRuleId"]) == "11" || Convert.ToString(DR["BusRuleId"]) == "10"))
                {
                    drugTypeId = Convert.ToInt32(DR["Value"]);
                }
            }
            return drugTypeId;
        }

        /// <summary>
        /// Gets the grid view control value.
        /// </summary>
        /// <param name="theControl">The control.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private string GetGridViewControlValue(Control theControl, string columnName, DataTable dt)
        {
            string ret = string.Empty;

            string[] regimen;
            string[] controlid;

            foreach (object obj in theControl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (x.GetType() == typeof(TextBox))
                                    {
                                        if (((TextBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                            {
                                                if (((TextBox)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                                {
                                                    ret = ((TextBox)x).Text;

                                                    ((TextBox)x).Text = "";

                                                    if (((TextBox)x).ID.Contains("TXTReg-"))
                                                    {
                                                        regimen = ((TextBox)x).ID.Split('=');
                                                        controlid = regimen[0].Split('-');
                                                        if ((regimen.Length > 1) && (controlid.Length > 2))
                                                        {
                                                            if (Session["SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + ""] != null)
                                                            {
                                                                Session["SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + ""] = null;
                                                            }
                                                        }
                                                    }
                                                    return ret;
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(HtmlInputRadioButton))
                                    {
                                        if (((HtmlInputRadioButton)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (((HtmlInputRadioButton)x).ID.Contains("RADIO1-"))
                                                {
                                                    if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                    {
                                                        if (((HtmlInputRadioButton)x).Checked == true)
                                                        {
                                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                                ret = "true";
                                                            else
                                                                ret = "";

                                                            ((HtmlInputRadioButton)x).Checked = false;

                                                            return ret;
                                                        }
                                                    }
                                                }
                                            }
                                            if (((HtmlInputRadioButton)x).ID.Contains("RADIO2-"))
                                            {
                                                if (((HtmlInputRadioButton)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                                {
                                                    if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                    {
                                                        if (((HtmlInputRadioButton)x).Checked == true)
                                                        {
                                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                                ret = "false";
                                                            else
                                                                ret = "";

                                                            ((HtmlInputRadioButton)x).Checked = false;

                                                            return ret;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(DropDownList))
                                    {
                                        if (((DropDownList)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((DropDownList)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                {
                                                    if (((DropDownList)x).Enabled == true)
                                                    {
                                                        if (((DropDownList)x).SelectedIndex == 0)
                                                        {
                                                            if (ret == "")
                                                            {
                                                                ret = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (ret == "")
                                                            {
                                                                ret = ((DropDownList)x).SelectedItem.Text;
                                                            }
                                                            else
                                                            {
                                                                ret = ret + "-" + ((DropDownList)x).SelectedItem.Text;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ret = "";
                                                    }

                                                    ((DropDownList)x).SelectedIndex = 0;

                                                    //return ret;
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputCheckBox))
                                    {
                                        if (((HtmlInputCheckBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((HtmlInputCheckBox)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                {
                                                    if (((HtmlInputCheckBox)x).Visible == true)
                                                    {
                                                        if (((HtmlInputCheckBox)x).Checked == true)
                                                        {
                                                            ret = "true";
                                                        }
                                                        else
                                                        {
                                                            ret = "false";
                                                        }

                                                        ((HtmlInputCheckBox)x).Checked = false;

                                                        return ret;
                                                    }
                                                    else
                                                    {
                                                        ret = "";

                                                        return ret;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Gets the i call back function.
        /// </summary>
        private void GetICallBackFunction()
        {
            str = "";
            ClientScriptManager m = Page.ClientScript;
            str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
            strCallback = "function CallServer(args,context){" + str + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
        }

        /// <summary>
        /// Ics the D10 heading.
        /// </summary>
        /// <param name="fieldId">The field identifier.</param>
        private void ICD10Heading(string fieldId)
        {
            Panel thelblPnl = new Panel();

            #region "ICD10"

            if (thelblPnl.Controls.Count < 1)
            {
                Panel PnlHeading = new Panel();
                PnlHeading.ID = "pnl-ICD10-" + fieldId;
                PnlHeading.Height = 20;
                PnlHeading.Width = 840;
                PnlHeading.Font.Bold = true;
                thelblPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblICD10" + fieldId;
                theSP.Width = 5;
                theSP.Text = "";
                PnlHeading.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblICD10Nm" + fieldId;
                theLabel1.Text = "ICD10";
                theLabel1.Width = 400;
                PnlHeading.Controls.Add(theLabel1);

                Label theLabel2 = new Label();
                theLabel2.ID = "lblDateOnset" + fieldId;
                theLabel2.Text = " Date of Onset";
                theLabel2.Width = 250;
                PnlHeading.Controls.Add(theLabel2);

                Label theLabel3 = new Label();
                theLabel3.ID = "lblComments" + fieldId;
                theLabel3.Text = "Comments";
                theLabel3.Width = 120;
                PnlHeading.Controls.Add(theLabel3);
                DIVCustomItem.Controls.Add(PnlHeading);
            }

            #endregion "ICD10"
        }

        /// <summary>
        /// Inserts the grid view.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="sectionId">The section identifier.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <returns></returns>
        private StringBuilder InsertGridView(int patientId, int featureId, int sectionId, string sectionName, int visitId, string featureName)
        {
            StringBuilder Sbinsert = new StringBuilder();
            DataTable dtlnktable = ((DataTable)ViewState["LnkTable"]).Copy();
            DataTable lnkSectionFieldName = dtgridview(dtlnktable);
            //DataTable lnkSectionFieldName = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "FieldName", "IsGridView", "SectionID","Fieldlabel").Copy();
            DataView dvSectionFieldName = new DataView(lnkSectionFieldName);
            dvSectionFieldName.RowFilter = "SectionId=" + sectionId + " and IsGridView = 1";
            TabContainer container = tabContainer;
            foreach (object obj in container.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object z in c.Controls)
                            {
                                if (z.GetType() == typeof(GridView))
                                {
                                    StringBuilder sbColumns = new StringBuilder(); ;

                                    if (((GridView)z).ID.Contains("Dview_" + sectionId))
                                    {
                                        string Table = "DTL_CUSTOMFORM_" + sectionName + "_" + featureName.ToString().Trim().Replace(' ', '_'); ;
                                        if (visitId > 0)
                                        {
                                            Sbinsert.Append(" Delete  from [" + Table + "] where FormID = " + featureId + " and  Ptn_Pk=" + patientId + " and Visit_pk=" + visitId + " and LocationID=" + Session["AppLocationId"].ToString() + "; ");
                                        }
                                        //for (int i = 0; i < dvSectionFieldName.ToTable().Rows.Count; i++)
                                        //{
                                        //    sbColumns.Append(",[" + dvSectionFieldName.ToTable().Rows[i]["FieldName"].ToString() + "]");
                                        //}
                                        if (ViewState["GridCache_" + sectionId] != null)
                                        {
                                            for (int y = 0; y < ((DataTable)ViewState["GridCache_" + sectionId]).Columns.Count; y++)
                                            {
                                                string COLNAME = ((DataTable)ViewState["GridCache_" + sectionId]).Columns[y].ColumnName.ToString();
                                                string strfieldname = findcolumnfieldname(dvSectionFieldName.ToTable(), COLNAME);
                                                sbColumns.Append("," + strfieldname + "");
                                            }

                                            for (int j = 0; j < ((DataTable)ViewState["GridCache_" + sectionId]).Rows.Count; j++)
                                            {
                                                StringBuilder sbSelect = new StringBuilder();
                                                StringBuilder sbRows = new StringBuilder();
                                                Sbinsert.Append(" Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID],[SectionId],[FormID]");
                                                Sbinsert.Append(sbColumns);
                                                Sbinsert.Append(", [UserID], [CreateDate])");

                                                for (int y = 0; y < ((DataTable)ViewState["GridCache_" + sectionId]).Columns.Count; y++)
                                                {
                                                    bool isddl = false;
                                                    DataView dvGridViewDDL = new DataView(dtlnktable);
                                                    string COLNAME = ((DataTable)ViewState["GridCache_" + sectionId]).Columns[y].ColumnName.ToString();
                                                    dvGridViewDDL.RowFilter = "FieldLabel= '" + COLNAME + "' AND IsGridView = 1 and ControlId =4";
                                                    if (dvGridViewDDL.Count > 0)
                                                    {
                                                        if (ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()] != null)
                                                        {
                                                            DataTable dtDDL = new DataTable();
                                                            dtDDL = (DataTable)ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()];
                                                            DataView dvddl = new DataView(dtDDL);
                                                            string DDLVALUE = Convert.ToString(((DataTable)ViewState["GridCache_" + sectionId]).Rows[j][y]);
                                                            dvddl.RowFilter = "Name  ='" + DDLVALUE + "'";
                                                            if (dvddl.Count == 0)
                                                            {
                                                                sbRows.Append(",null");
                                                            }
                                                            else
                                                            {
                                                                sbRows.Append(",'" + dvddl[0]["ID"] + "'");
                                                            }
                                                            isddl = true;
                                                            dtDDL.Dispose();
                                                        }
                                                    }
                                                    if (!isddl)
                                                    {
                                                        if (string.IsNullOrEmpty(Convert.ToString(((DataTable)ViewState["GridCache_" + sectionId]).Rows[j][y])))
                                                        {
                                                            sbRows.Append(",null");
                                                        }
                                                        else
                                                        {
                                                            sbRows.Append(",'" + apostropheHandler(Convert.ToString(((DataTable)ViewState["GridCache_" + sectionId]).Rows[j][y])) + "'");
                                                        }
                                                    }
                                                }

                                                if (visitId == 0)
                                                {
                                                    sbSelect.Append(" select " + patientId + ", @thisVisitId , " + Session["AppLocationId"].ToString() + " , " + sectionId.ToString() + "," + featureId.ToString());
                                                }
                                                else
                                                {
                                                    sbSelect.Append(" select " + patientId + ", " + visitId + " , " + Session["AppLocationId"].ToString() + " , " + sectionId.ToString() + "," + featureId.ToString());
                                                }
                                                sbSelect.Append(sbRows);
                                                sbSelect.Append(" , " + Session["AppUserId"].ToString() + " , Getdate() ");
                                                Sbinsert.Append(sbSelect);
                                                Sbinsert.Append(" ; ");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Sbinsert;
        }

        /// <summary>
        /// Inserts the multi select list.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="multi_SelectTable">The multi_ select table.</param>
        /// <param name="theControlId">The control identifier.</param>
        /// <param name="theFieldId">The field identifier.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private StringBuilder InsertMultiSelectList(int patientId, string fieldName, int featureId, string multi_SelectTable, int theControlId, int theFieldId, int tabId)
        {
            StringBuilder Insertcbl = new StringBuilder();
            TabContainer container = tabContainer;
            foreach (object obj in container.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    if (Convert.ToInt32(tabPanel.ID) == tabId)
                    {
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(Panel))
                                    {
                                        string strCheckBox = string.Empty;
                                        string strDate1 = string.Empty;
                                        string strDate2 = string.Empty;
                                        string strNumeric = string.Empty;
                                        string[] TableName1 = null;
                                        string Table1 = string.Empty;

                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(CheckBox))
                                            {
                                                string[] TableName = ((CheckBox)x).ID.Split('-');
                                                TableName1 = ((CheckBox)x).ID.Split('-');
                                                if (TableName.Length == 5)
                                                {
                                                    Table1 = TableName[3];

                                                    if (Table1 == multi_SelectTable)
                                                    {
                                                        if (Table1 == "DTL_CUSTOMFIELD")
                                                        {
                                                            Table1 = "DTL_FB_" + TableName[2] + "";
                                                            Table1 = Table1.Replace(' ', '_');
                                                        }
                                                        if (Table1 != "DTL_CUSTOMFIELD" && Convert.ToInt32(TableName[4]) == theFieldId)
                                                        {
                                                            if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                                            {
                                                                if (strCheckBox == string.Empty)
                                                                {
                                                                    strCheckBox = TableName[1];
                                                                }
                                                                else
                                                                {
                                                                    strCheckBox = strCheckBox + ", " + TableName[1];
                                                                }
                                                                //Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                //Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                                //Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                            }
                                                            else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                                            {
                                                                if (strCheckBox == string.Empty)
                                                                {
                                                                    strCheckBox = TableName[1];
                                                                }
                                                                else
                                                                {
                                                                    strCheckBox = strCheckBox + ", " + TableName[1];
                                                                }
                                                                ViewState["OtherNote"] = ((CheckBox)x).Text;
                                                            }
                                                        }
                                                        //For DtlCustom Field Table
                                                        else if (Convert.ToInt32(TableName[4]) == theFieldId)
                                                        {
                                                            if (((CheckBox)x).Checked == true)
                                                            {
                                                                Insertcbl.Append("Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                Insertcbl.Append("values (" + patientId + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                                Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(TextBox))
                                            {
                                                string[] TableName = ((TextBox)x).ID.Split('-');
                                                if (TableName[0] == "TXTDT1" || TableName[0] == "TXTDT2" || TableName[0] == "TXTNUM")
                                                {
                                                    if (TableName[0] == "TXTDT1")
                                                    {
                                                        if (strDate1 == string.Empty)
                                                            strDate1 = ((TextBox)x).Text;
                                                        else
                                                            strDate1 = strDate1 + ", " + ((TextBox)x).Text;
                                                    }
                                                    else if (TableName[0] == "TXTDT2")
                                                    {
                                                        if (strDate2 == string.Empty)
                                                            strDate2 = ((TextBox)x).Text;
                                                        else
                                                            strDate2 = strDate2 + ", " + ((TextBox)x).Text;
                                                    }
                                                    else if (TableName[0] == "TXTNUM")
                                                    {
                                                        if (strNumeric == string.Empty)
                                                            strNumeric = ((TextBox)x).Text;
                                                        else
                                                            strNumeric = strNumeric + ", " + ((TextBox)x).Text;
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(HtmlInputText))
                                            {
                                                string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                                string Table = TableName[3];
                                                string Other = "";
                                                if (Table == multi_SelectTable)
                                                {
                                                    if (Table == "DTL_CUSTOMFIELD")
                                                    {
                                                        Table = "DTL_FB_" + TableName[2] + "";
                                                        Table = Table.Replace(' ', '_');
                                                    }
                                                    if (Table != "DTL_CUSTOMFIELD")
                                                    {
                                                        string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                        DataSet dsMultiSelectList = new DataSet();
                                                        dsMultiSelectList.ReadXml(filePath);
                                                        DataTable DT = dsMultiSelectList.Tables[0];
                                                        foreach (DataRow DR in DT.Rows)
                                                        {
                                                            if (DR[0].ToString().ToUpper() == Table)
                                                            {
                                                                Other = DR[2].ToString();
                                                            }
                                                        }
                                                        if (theControlId == 15)
                                                        {
                                                            Other = "Other";
                                                        }
                                                        if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "" && Other != "")
                                                        {
                                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate],TabId)");
                                                            Insertcbl.Append("values (" + patientId + ", @thisVisitId ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                            Insertcbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate()," + tabId + ")");
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // code here
                                        if (!string.IsNullOrEmpty(strCheckBox))
                                        {
                                            string[] arrDate1 = null;
                                            string[] arrDate2 = null;
                                            string[] arrNumeric = null;
                                            string[] arrCheckBox = null;

                                            if (strCheckBox != string.Empty)
                                                arrCheckBox = strCheckBox.Split(',');
                                            if (strDate1 != string.Empty)
                                                arrDate1 = strDate1.Split(',');
                                            if (strDate2 != string.Empty)
                                                arrDate2 = strDate2.Split(',');
                                            if (strNumeric != string.Empty)
                                                arrNumeric = strNumeric.Split(',');

                                            DateTime dtDate1 = new DateTime();
                                            DateTime dtDate2 = new DateTime();
                                            dtDate1 = DateTime.Now;
                                            dtDate2 = DateTime.Now;
                                            int intNumeric = 0;
                                            for (int i = 0; i < arrCheckBox.Length; i++)
                                            {
                                                if (arrDate1 != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrDate1[i]))
                                                        if (arrDate1[i].Trim() != "")
                                                        {
                                                            dtDate1 = Convert.ToDateTime(arrDate1[i]);
                                                        }
                                                }
                                                if (arrDate2 != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrDate2[i]))
                                                        if (arrDate2[i].Trim() != "")
                                                        {
                                                            dtDate2 = Convert.ToDateTime(arrDate2[i]);
                                                        }
                                                }
                                                if (arrNumeric != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrNumeric[i]))
                                                        if (arrNumeric[i].Trim() != "")
                                                        {
                                                            intNumeric = Convert.ToInt16(arrNumeric[i]);
                                                        }
                                                        else { intNumeric = 0; }
                                                }
                                                bool tabIdexist = false;
                                                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                DataSet dsMultiSelectList = new DataSet();
                                                dsMultiSelectList.ReadXml(filePath);
                                                DataTable DT = dsMultiSelectList.Tables[0];
                                                foreach (DataRow DR in DT.Rows)
                                                {
                                                    if (DR[0].ToString().ToUpper() == Table1)
                                                    {
                                                        tabIdexist = true;
                                                    }
                                                }
                                                //Creating column for old database where Column Name 'Datefield1', 'Datefield2' and Numericfield doesnot exist
                                                StringBuilder createColumn = new StringBuilder();
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField1')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField1 datetime End ");
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField2')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField2 datetime End ");
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='NumericField')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add NumericField int End ");
                                                createColumn.Append("Select 1[Saved]");
                                                ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
                                                DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(createColumn.ToString());

                                                if (tabIdexist == true)
                                                {
                                                    Insertcbl.Append(" Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField,TabId)");
                                                    Insertcbl.Append("values (" + patientId + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + arrCheckBox[i] + ",");
                                                    Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ", " + tabId + ")");
                                                }
                                                else
                                                {
                                                    Insertcbl.Append(" Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField)");
                                                    Insertcbl.Append("values (" + patientId + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + arrCheckBox[i] + ",");
                                                    Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ")");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Insertcbl;
        }

        /// <summary>
        /// Labs the data binding.
        /// </summary>
        private void LabDataBinding()
        {
            //Lab Order
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);
            int patientId = Convert.ToInt32(Session["PatientId"]);
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            StringBuilder StrLab = new StringBuilder();
          
            StrLab.Append("select a.LabID,a.LocationID,a.OrderedByName,a.OrderedByDate,a.ReportedByName,");
            StrLab.Append("a.ReportedByDate,a.CheckedByName,a.CheckedByDate,a.PreClinicLabDate, a.LabPeriod,");
            StrLab.Append("b.LabTestID,b.ParameterID[SubTestId],b.TestResults,b.TestResults1,b.TestResultId,b.Financed,");
            StrLab.Append("c.subtestname[SubTestName],d.LabTypeID[LabTypeID],d.LabName,b.Units,e.name as UnitName,");
            StrLab.Append("f.MinBoundaryValue,f.MaxBoundaryValue from ord_PatientlabOrder a,dtl_PatientLabResults b");
            StrLab.Append(" left outer join mst_Decode e on e.Id=b.Units");
            StrLab.Append(" left outer join lnk_labValue f  on  f.UnitId=b.units and f.SubTestId=b.ParameterId,");
            StrLab.Append("lnk_testParameter c, mst_labtest d where a.labid = b.labid and a.labid=");
            StrLab.Append("(Select LabID from Ord_PatientLabOrder where VisitId='" + visitId + "')");
            StrLab.Append(" and b.parameterid = c.subtestid and c.testid=d.labtestid   ");

            DataSet theDSLab = MgrBindValue.Common_GetSaveUpdate(StrLab.ToString());

            DataTable dtLabs = new DataTable();
            dtLabs.Columns.Add("LabTestID", Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabName", Type.GetType("System.String"));
            dtLabs.Columns.Add("SubTestID", Type.GetType("System.Int32"));
            dtLabs.Columns.Add("SubTestName", Type.GetType("System.String"));
            dtLabs.Columns.Add("LabTypeId", Type.GetType("System.Int32"));
            dtLabs.Columns.Add("Flag", Type.GetType("System.Int32"));

            foreach (DataRow thedr in theDSLab.Tables[0].Rows)
            {
                //if (Convert.ToInt32(thedr["LabTypeId"]) == 1)
                //{
                DataRow tmpDR = dtLabs.NewRow();
                tmpDR[0] = thedr["LabTestId"];
                tmpDR[1] = thedr["LabName"];
                tmpDR[2] = thedr["SubTestId"];
                tmpDR[3] = thedr["SubTestName"];
                tmpDR[4] = thedr["LabTypeId"];
                tmpDR[5] = 1;
                dtLabs.Rows.Add(tmpDR);
                //BindCustomControls(thedr);
                //}
            }

            if (!IsPostBack)
            {
                if (ViewState["LabRanges"] == null)
                {
                    ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                    theDSLabs = LabResultManager.GetLabValues(); //pr_Laboratory_GetLabValues_constella
                    ViewState["LabRanges"] = theDSLabs;
                    ViewState["LabMaster"] = theDSLabs.Tables[2];
                }
                //Setting Session
                DataTable theDTLab = PtnCustomformselectedDataTableLab(dtLabs);
                Session["SelectedData"] = theDTLab;
            }

            foreach (DataRow labdr in dtLabs.Rows)
            {
                if ((DataTable)Session["SelectedData"] != null)
                {
                    foreach (DataRow labdrII in ((DataTable)Session["SelectedData"]).Rows)
                    {
                        int Flag = labdrII["Flag"] == DBNull.Value ? 0 : 1;
                        if (Convert.ToInt32(labdr["SubTestId"]) == Convert.ToInt32(labdrII["SubTestId"]) && Flag == 1)
                        {
                            BindCustomControls(labdr);
                        }
                    }
                }
            }

            foreach (DataRow thedrdata in theDSLab.Tables[0].Rows)
            {
                FillLabData(DIVCustomItem, thedrdata);
            }
        }

        /// <summary>
        /// Loads the additional labs.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <param name="thePanel">The panel.</param>
        private void LoadAdditionalLabs(DataTable theDT, Panel thePanel)
        {
            if (theDT != null)
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                    BindCustomControls(theDR);
                }
            }
        }

        void CreateSingleLineField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theSingleTextAuto = new TextBox();
                theSingleTextAuto.ID = "TXTAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                //hidautoSingleLineID.Value = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                //theSingleTextAuto.Width = 180;
                theSingleTextAuto.MaxLength = 250;
                theSingleTextAuto.CssClass = "form-control";
                //theSingleTextAuto.Text = AutoDt.Rows[0][Column].ToString();
                theSingleTextAuto.Enabled = false;
                DIVCustomItem.Controls.Add(theSingleTextAuto);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleTextAuto.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }

            if (this.IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "' >" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            TextBox theSingleText = new TextBox();
            theSingleText.ID = "TXT-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            // theSingleText.Width = 180;
            theSingleText.MaxLength = 250;
            theSingleText.CssClass = "form-control";
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + this.tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleText.ClientID + "";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "uniqueId" + Guid.NewGuid(), "EnableControlFalse('" + str + "');", true);
                if (!IsPostBack)
                {
                    AddContolStausInHastTable(str);
                }
            }
            //theSingleText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theSingleText);
            tabContainer.ID = "TAB";
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleText.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            ApplyBusinessRules(theSingleText, controlId.ToString(), theEnable);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        void CreateMultiLineField(int controlId, string referenceId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            bool spanfield = false;
            DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
            DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
            theBusinessRuleDV.RowFilter = "BusRuleId=25 and FieldId = " + fieldId.ToString();
            if (theBusinessRuleDV.Count > 0 || referenceId == "TXT_MULTILINE_LARGE")
            {
                spanfield = true;
            }
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

            if (theAutoPopulate == true)
            {
                if (spanfield == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                }
                else
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                if (spanfield == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%'>"));
                }
                else
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                TextBox theMultiTextAuto = new TextBox();
                theMultiTextAuto.ID = "TXTMultiAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;

                theMultiTextAuto.CssClass = "form-control";
                //if (spanfield == true)
                //{
                theMultiTextAuto.Width = Unit.Percentage(100);
                //}
                //else
                //    theMultiTextAuto.Width = 100;
                theMultiTextAuto.MaxLength = 9;
                theMultiTextAuto.Enabled = false;
                DIVCustomItem.Controls.Add(theMultiTextAuto);
                TextBox thehiddenText = new TextBox();
                thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theMultiTextAuto.ClientID + "";
                thehiddenText.Width = 0;
                divhidden.Controls.Add(thehiddenText);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                //DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }
            if (spanfield == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
            }
            else
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            if (spanfield == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%'>"));
            }
            else
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            TextBox theMultiText = new TextBox();
            theMultiText.ID = "TXTMulti-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theMultiText.CssClass = "form-control";


            //if (spanfield == true)
            //{
            theMultiText.Width = Unit.Percentage(100);
            theMultiText.Rows = 6;
            //}
            //else
            //    theMultiText.Width = 200;*/
            theMultiText.TextMode = TextBoxMode.MultiLine;
            theMultiText.MaxLength = 500;
            theMultiText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theMultiText);
            TextBox thehiddenText1 = new TextBox();
            thehiddenText1.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theMultiText.ClientID + "";
            thehiddenText1.Width = 0;
            divhidden.Controls.Add(thehiddenText1);
            ApplyBusinessRules(theMultiText, controlId.ToString(), theEnable);
            //theMultiText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        void CreateDecimalField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theSingleDecimalAuto = new TextBox();
                theSingleDecimalAuto.ID = "TXTAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;

                // theSingleDecimalAuto.Width = 180;
                theSingleDecimalAuto.MaxLength = 10;
                theSingleDecimalAuto.CssClass = "form-control";
                theSingleDecimalAuto.Enabled = false;
                DIVCustomItem.Controls.Add(theSingleDecimalAuto);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalAuto.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }

            if (this.IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            TextBox theSingleDecimalText = new TextBox();
            theSingleDecimalText.ID = "TXT-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theSingleDecimalText.CssClass = "form-control";
            //theSingleDecimalText.Width = 180;
            theSingleDecimalText.MaxLength = 10;
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "');", true);
                if (!this.IsPostBack)
                {
                    this.AddContolStausInHastTable(str);
                }
            }
            //theSingleDecimalText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theSingleDecimalText);
            ApplyBusinessRules(theSingleDecimalText, controlId.ToString(), theEnable);
            tabContainer.ID = "TAB";
            theSingleDecimalText.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "')");
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);

            //theSingleDecimalText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            if (fieldName.ToUpper() == "HEIGHT")
            {
                //theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
                hdnHeight.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
            }
            if (fieldName.ToUpper() == "WEIGHT")
            {
                //theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
                hdnWeight.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
            }
        }

        void CreateNumericField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theNumberAuto = new TextBox();
                theNumberAuto.ID = "TXTNUMAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                //theNumberAuto.Width = 100;
                theNumberAuto.MaxLength = 9;
                theNumberAuto.CssClass = "form-control";
                theNumberAuto.Enabled = false;
                DIVCustomItem.Controls.Add(theNumberAuto);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberAuto.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }

            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
            TextBox theNumberText = new TextBox();
            theNumberText.ID = "TXTNUM-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            //  theNumberText.Width = 100;
            theNumberText.MaxLength = 9;
            theNumberText.CssClass = "form-control";
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "');", true);
                if (!this.IsPostBack)
                {
                    this.AddContolStausInHastTable(str);
                }
            }
            //theNumberText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theNumberText);
            tabContainer.ID = "TAB";
            theNumberText.Attributes.Add("onkeyup", "chkInteger('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "')");
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            ApplyBusinessRules(theNumberText, controlId.ToString(), theEnable);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        void CreateDateField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                TextBox theDateTextAuto = new TextBox();
                theDateTextAuto.ID = "TXTDTAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
               // theDateTextAuto.Width = 100;
                theDateTextAuto.CssClass = "form-control";
                theDateTextAuto.MaxLength = 11;
                theDateTextAuto.Enabled = false;
                DIVCustomItem.Controls.Add(theDateTextAuto);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateTextAuto.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl(@"<td style='width:60%;white-space:nowrap' align='left'><table style=""width:100%""><tr><td style='width:60%;white-space:nowrap;text-align:left'> "));
            TextBox theDateText = new TextBox();
            theDateText.CssClass = "form-control";
            theDateText.ID = "TXTDT-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            Control ctl = theDateText;
            //theDateText.Width = 83;
            theDateText.MaxLength = 11;
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "');", true);
                if (!IsPostBack)
                {
                    AddContolStausInHastTable(str);
                }
            }
            //theDateText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theDateText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td><td align='left'>"));
            CreateDateImage(theDateText, controlId.ToString(), theEnable, false);
            DIVCustomItem.Controls.Add(new LiteralControl("</td></tr></table>"));
            ApplyBusinessRules(theDateText, controlId.ToString(), theEnable);
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        void CreateCheckboxField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            HtmlInputCheckBox theChk = new HtmlInputCheckBox();
            theChk.ID = "Chk-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            // theChk.Attributes.Add("class", "form-control");
            //theChk.ID = "Chk-" + Column + "-" + Table;
            //theChk.Visible = theEnable;
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "');", true);
                if (!IsPostBack)
                {
                    AddContolStausInHastTable(str);
                }
            }
            //DIVCustomItem.Controls.Add(new LiteralControl("<div class=\"checkbox\"> <label>"));
            DIVCustomItem.Controls.Add(theChk);
            // DIVCustomItem.Controls.Add(new LiteralControl("</label></div> "));



            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        void CreateYesNoField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
            theYesNoRadio1.ID = "RADIO1-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theYesNoRadio1.Value = "Yes";
            theYesNoRadio1.Name = "" + fieldName + "";
            if (theConditional == true && theEnable == true)
            {
                DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                DataView theDVC = new DataView(thDTC);
                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                StringBuilder EnableValueRadio = new StringBuilder();
                if (theDVC.ToTable().Rows.Count > 0)
                {
                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                    {
                        if (Convert.ToString(theDRC["ConControlId"]) != "")
                        {
                            EnableValueRadio.Append("EnableValueYes('" + theDRC["ConControlId"] + "');");
                        }
                    }
                    theYesNoRadio1.Attributes.Add("onclick", "down(this); " + EnableValueRadio + "");
                }
                else { theYesNoRadio1.Attributes.Add("onclick", "down(this);"); }
            }
            else
                theYesNoRadio1.Attributes.Add("onclick", "down(this);");
            theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
            DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + fieldId + "'>Yes</label>"));
            // DIVCustomItem.Controls.Add(new LiteralControl(@"<label class=""radio-inline"">"));
            DIVCustomItem.Controls.Add(theYesNoRadio1);
           // DIVCustomItem.Controls.Add(new LiteralControl("</label>"));
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "_Yes" + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "');", true);
                if (!IsPostBack)
                {
                    AddContolStausInHastTable(str);
                }
            }
            //theYesNoRadio1.Visible = theEnable;
            ApplyBusinessRules(theYesNoRadio1, controlId.ToString(), theEnable);
            //theYesNoRadio1.Visible = theEnable;
          
            HtmlInputRadioButton thehiddenRd = new HtmlInputRadioButton();
            thehiddenRd.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "";
            divhidden.Controls.Add(thehiddenRd);
            HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
            theYesNoRadio2.ID = "RADIO2-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theYesNoRadio2.Value = "No";
            theYesNoRadio2.Name = "" + fieldName + "";
            if (theConditional == true && theEnable == true)
            {
                DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                DataView theDVC = new DataView(thDTC);
                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                StringBuilder EnableValueRadio = new StringBuilder();
                if (theDVC.ToTable().Rows.Count > 0)
                {
                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                    {
                        if (Convert.ToString(theDRC["ConControlId"]) != "")
                        {
                            EnableValueRadio.Append("EnableValueNo('" + theDRC["ConControlId"] + "');");
                        }
                    }
                    theYesNoRadio2.Attributes.Add("onclick", "down(this); " + EnableValueRadio + "");
                }
                else { theYesNoRadio2.Attributes.Add("onclick", "down(this);"); }
            }
            else
                theYesNoRadio2.Attributes.Add("onclick", "down(this);");
            theYesNoRadio2.Attributes.Add("onfocus", "up(this)");
            DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + fieldId + "'>No</label>"));
            //DIVCustomItem.Controls.Add(new LiteralControl(@"<label class=""radio-inline"">"));
            DIVCustomItem.Controls.Add(theYesNoRadio2);
           // DIVCustomItem.Controls.Add(new LiteralControl("</label>"));
            HtmlInputRadioButton thehiddenRd1 = new HtmlInputRadioButton();
            thehiddenRd1.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "";
            divhidden.Controls.Add(thehiddenRd1);
            if (theEnable == false)
            {
                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "_No" + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "');", true);
                if (!IsPostBack)
                {
                    AddContolStausInHastTable(str);
                }
            }
            ApplyBusinessRules(theYesNoRadio2, controlId.ToString(), theEnable);
        
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        private void CreateAutoCompleteField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate, string bindCategory, string bindSource)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            tabContainer.ID = "TAB";
            IQLookupTextBox selectListTextBox = new IQLookupTextBox();
            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                IQLookupTextBox autoCompleteTextBox = new IQLookupTextBox();
                autoCompleteTextBox.ID = "TXTLookupAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                autoCompleteTextBox.Width = 280;
                autoCompleteTextBox.ShowLabel = false;
                //autoCompleteTextBox.TextBoxCssClass = "";
                // autoCompleteTextBox.LabelCssClass = "";

                autoCompleteTextBox.Enabled = false;
                DIVCustomItem.Controls.Add(autoCompleteTextBox);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + autoCompleteTextBox.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }
            // selectListTextBox.LabelCssClass = "";
            // selectListTextBox.TextBoxCssClass = "";
            selectListTextBox.ShowLabel = false;
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));

            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            //selectListTextBox.LabelText = fieldLabel + " : ";
            selectListTextBox.LookupCategory = bindSource;
            selectListTextBox.LookupName = bindCategory;
            selectListTextBox.ID = "TXTLookup-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;

            // selectListTextBox.Width = 280;
            selectListTextBox.ToolTip = fieldName;
            selectListTextBox.ServiceMethod = "GetLookupValue";
            selectListTextBox.ServicePath = "~/WebService/IQLookupWS.asmx";

            DIVCustomItem.Controls.Add(selectListTextBox);
            ApplyBusinessRules(selectListTextBox, controlId.ToString(), theEnable);
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + selectListTextBox.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

        }
        void CreateRequiredFieldValidator(string controlName, string fieldName, string fieldLabel, string tableName, string fieldId, string tabId)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ID = "req_field" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            validator.ErrorMessage = string.Format("{0} is required", fieldLabel);
            validator.Display = ValidatorDisplay.Dynamic;
            validator.ValidationGroup = "vgroup" + tabId;
            validator.ControlToValidate = controlName;
            DIVCustomItem.Controls.Add(validator);
        }

        void CreateSelectListField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate, string codeId, string bindSource)
        {
            bool theCntrlPresent = false;
            if (theCntrlPresent != true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                DropDownList ddlSelectListAuto = new DropDownList();
                tabContainer.ID = "TAB";
                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    ddlSelectListAuto.ID = "SELECTLISTAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                    // ddlSelectListAuto.Width = 100;
                    ddlSelectListAuto.CssClass = "form-control";
                    //ddlSelectListAuto.MaxLength = 9;
                    ddlSelectListAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(ddlSelectListAuto);
                    TextBox thehiddenTextAuto = new TextBox();
                    thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectListAuto.ClientID + "";
                    thehiddenTextAuto.Width = 0;
                    divhidden.Controls.Add(thehiddenTextAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                }
                string controlFieldId = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                if (IsRequiredField(fieldId, fieldName) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));

                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label  align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                DropDownList ddlSelectList = new DropDownList();
                ddlSelectList.ID = controlFieldId;
                ddlSelectList.CssClass = "form-control";
                if (codeId == "")
                {
                    codeId = "0";
                }
                if (bindSource == "")
                {
                    bindSource = "MST_MODDECODE";
                    codeId = "-1";
                }
                DataView theDV = new DataView(theDSXML.Tables[bindSource]);
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    if (bindSource.ToUpper() == "MST_SYMPTOM" || bindSource.ToUpper() == "MST_REASON")
                    {
                        theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + codeId + "";
                    }
                    else if (bindSource.ToUpper() == "MST_HIVDISEASE")
                    {
                        theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + codeId + "";
                    }
                    else if (bindSource.ToUpper() == "MST_STOPPEDREASON")
                    {
                        theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                    }
                    else if (bindSource.ToUpper() == "MST_DECODE" || bindSource.ToUpper() == "MST_PMTCTDECODE" || bindSource.ToUpper() == "MST_MODDECODE")
                    {
                        if (bindSource.ToUpper() == "MST_DECODE")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                        }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + "";
                        }
                    }
                    else
                    {
                        theDV.RowFilter = "DeleteFlag=0";
                    }
                }
                else
                {
                    if (bindSource.ToUpper() == "MST_SYMPTOM" || bindSource.ToUpper() == "MST_REASON")
                    {
                        theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + codeId + "";
                    }
                    else if (bindSource.ToUpper() == "MST_HIVDISEASE")
                    {
                        theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + codeId + "";
                    }
                    else if (bindSource.ToUpper() == "MST_STOPPEDREASON")
                    {
                        theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                    }
                    else if (bindSource.ToUpper() == "MST_DECODE" || bindSource.ToUpper() == "MST_PMTCTDECODE" || bindSource.ToUpper() == "MST_MODDECODE")
                    {
                        if (bindSource.ToUpper() == "MST_DECODE")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                        }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + "";
                        }
                    }
                }

                if (theDV.Table != null)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    BindFunctions BindManager = new BindFunctions();
                    try { theDV.Sort = "SRNO Asc"; }
                    catch { }
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlSelectListAuto, theDT, "Name", "ID");
                    BindManager.BindCombo(ddlSelectList, theDT, "Name", "ID");
                    ViewState["GridViewDDL-" + fieldName] = (DataTable)ddlSelectList.DataSource;
                    if (theDT.Rows.Count == 0)
                    {
                        ListItem theItem = new ListItem();
                        theItem.Text = "Select";
                        theItem.Value = "0";
                        ddlSelectList.Items.Add(theItem);
                    }
                }
                else
                {
                    ListItem theItem1 = new ListItem();
                    theItem1.Text = "Select";
                    theItem1.Value = "0";
                    ddlSelectList.Items.Add(theItem1);
                }
                //ddlSelectList.Width = 180;
                if (theEnable == false)
                {
                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "";
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "');", true);
                    if (!this.IsPostBack)
                    {
                        this.AddContolStausInHastTable(str);
                    }
                }
                if (theConditional == true && theEnable == true)
                {
                    DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                    DataView theDVC = new DataView(thDTC);
                    theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                    StringBuilder EnableDisableControl = new StringBuilder();
                    if (theDVC.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow theDRC in theDVC.ToTable().Rows)
                        {
                            EnableDisableControl.Append("EnableValueDropdown('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "');");
                        }
                        ddlSelectList.Attributes.Add("onchange", "" + EnableDisableControl + "");
                    }

                }

                if (theSecondLabelConditional == true && theEnable == false)
                {
                    ddlSelectList.AutoPostBack = false;
                    ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                }
                DIVCustomItem.Controls.Add(ddlSelectList);
                ApplyBusinessRules(ddlSelectList, controlId.ToString(), theEnable);
                TextBox thehiddenText = new TextBox();
                thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "";
                thehiddenText.Width = 0;
                divhidden.Controls.Add(thehiddenText);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }
        }

        void LoadFieldTypeControl(int controlId,
            string controlReferenceId,
            string fieldName,
            string fieldId,
            string fieldLabel,
            string codeId,
            string bindCategory,
            string bindSource,
            string tableName,
            string tabId,
            bool theEnable)
        {
            try
            {
                bool theAutoPopulate = false;
                DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
                DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
                DataView theAutoDV = new DataView();
                theBusinessRuleDV.RowFilter = "BusRuleId=17 and FieldId = " + fieldId.ToString();
                if (theBusinessRuleDV.Count > 0)
                    theAutoPopulate = true;
                if (controlReferenceId == "TXT_SINGLE_LINE" ) ///SingleLine Text Box
                {
                    this.CreateSingleLineField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);
                }
                else if (controlReferenceId == "DECIMAL_TXT")
                {
                    this.CreateDecimalField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);

                }
                else if (controlReferenceId == "NUMERIC_TXT")
                {
                    this.CreateNumericField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);
                }
                else if (controlReferenceId == "SELECT_LIST" && bindSource != "")
                {
                    this.CreateSelectListField
                        (controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate, codeId, bindSource);
                }
                else if (controlReferenceId == "DATE_TXT")
                {
                    this.CreateDateField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);
                }
                else if (controlReferenceId == "YES_NO")
                {
                    this.CreateYesNoField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);

                }
                else if (controlReferenceId == "CHECK_BOX")
                {
                    this.CreateCheckboxField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "TXT_MULTILINE" || controlReferenceId == "TXT_MULTILINE_LARGE") ///SingleLine Text Box
                {
                    this.CreateMultiLineField(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate);
                }
                else if (controlReferenceId == "SELECT_LIST_MULTI")
                {
                    if (String.IsNullOrEmpty(bindSource) && String.IsNullOrEmpty(bindCategory))
                    {

                        this.CreateMissingFieldAlert(fieldId, fieldName, tabId, "");
                    }
                    else
                    {
                        this.CreateMultiSelectListField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate, codeId, bindSource);
                    }
                }
                else if (controlReferenceId == "PLACE_HOLDER")
                {
                    this.CreatePlaceholderField(fieldId, fieldName, tabId);
                }
                else if (controlReferenceId == "REGIMEN")
                {
                    this.CreateRegimenField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "DRUG_FIELD")
                {
                    this.CreateDrugSelectionField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "BMI_FIELD")
                {
                    this.CreateBMIField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "DISEASE_SYMPTOM")
                {
                    if (bindSource == "")
                    {

                        this.CreateMissingFieldAlert(fieldId, fieldName, tabId, "");
                    }
                    else
                    {
                        this.CreateDiseaseSymptomField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, codeId, bindSource);
                    }
                }
                else if(controlReferenceId=="BUTTON")
                {
                    this.CreateButtonField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "ICD10_FIELD")
                {
                    this.CreateICD10Field(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "TIME_FIELD")
                {
                    this.CreateTimeField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "LAB_FIELD")
                {
                    this.CreateLabSelectionField(fieldName, fieldId, fieldLabel, tableName, tabId, theEnable);
                }
                else if (controlReferenceId == "SELECTLIST_TEXTBOX")
                {
                    if (bindSource == "" || bindCategory == "")
                    {

                        this.CreateMissingFieldAlert(fieldId, fieldName, tabId, "");
                    }
                    else
                    {
                        this.CreateAutoCompleteField(controlId, fieldName, fieldId, fieldLabel, tableName, tabId, theEnable, theAutoPopulate, bindCategory, bindSource);
                    }
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }


        private void CreateLabSelectionField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
            HiddenField theHF = new HiddenField();
            Label theLabel = new Label();
            theLabel.ID = "lblLab-" + fieldName;
            theHF.ID = "hfLab-12-" + fieldId + "-" + fieldName;
            theLabel.Text = fieldLabel;
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + theLabel.Text + ":</label>"));
                ARLHeader.Add(theLabel.Text);
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + theLabel.Text + " :</label>"));
            }
            theHF.Value = theLabel.Text;
            DIVCustomItem.Controls.Add(theHF);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
            Button theBtn = new Button();
            theBtn.Width = 100;
            theBtn.ID = "BtnLab-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theBtn.Text = "Lab Test";
            theBtn.Enabled = theEnable;
            theBtn.Attributes.Add("onclick", "javascript:AdditionalLab(); return false");
            DIVCustomItem.Controls.Add(theBtn);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
            ApplyBusinessRules(theBtn, theBtn.ID, theEnable);
            if ((DataSet)Session["AddLab"] != null)
            {

                ViewState["AddLab"] = Session["AddLab"];
                Session.Remove("AddLab");
            }
            if (Convert.ToString(ViewState["btnlabisEnable"]) == "2")
            {
                ViewState["AddLab"] = null;
            }

            if ((DataTable)ViewState["AddLab"] != null)
            {
                foreach (DataRow theDR in ((DataTable)ViewState["AddLab"]).Rows)
                {
                    if (theDR["Flag"] == System.DBNull.Value)
                    {
                        BindCustomControls(theDR);
                    }
                }
                Session["SelectedData"] = ViewState["AddLab"];
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                LabDataBinding();
            }

         
        }

        private void CreateButtonField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {

            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + ":</label>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
            
            Button theBtn = new Button();
            theBtn.Width = 100;
            theBtn.ID = "BtnFrm-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theBtn.Text = "" + fieldName + "";
            theBtn.CssClass = "btn btn-primary";
            theBtn.Style.Add("height", "30px");
            theBtn.Style.Add("width", "13%");
            theBtn.Style.Add("text-align", "left");
            theBtn.Enabled = theEnable;
            DataView theDV = new DataView((DataTable)ViewState["BusRule"]);
            theDV.RowFilter = "FieldId=" + fieldId + "";
            DataView theDVPublish = new DataView((DataTable)ViewState["BusRulePublish"]);
            theDVPublish.RowFilter = "FeatureID=" + Convert.ToInt32(theDV.ToTable().Rows[0]["Value"]) + "";
            if (Convert.ToInt32(theDVPublish.ToTable().Rows[0]["Published"]) == 1)
            {
                theBtn.Attributes.Add("onclick", "javascript:alert('Please publish this form in order to open'); return false");
            }
            else
            {
                theBtn.Attributes.Add("onclick", "javascript:OpenDynamicForm('" + Convert.ToInt32(theDV.ToTable().Rows[0]["Value"]) + "', '" + Convert.ToInt32(Session["PatientId"]) + "'); return false");
            }
            DIVCustomItem.Controls.Add(theBtn);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

        }
        private void CreateTimeField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
            IQCareUtils theUtil = new IQCareUtils();
            DataView theDV = new DataView((DataTable)ViewState["BusRule"]);
            theDV.RowFilter = "ControlId=14 and FieldId=" + fieldId + "";
            DataTable theBDV = theDV.ToTable();
            int Format = 0;
            Label lblFormat = new Label();
            foreach (DataRow theDR in theBDV.Rows)
            {
                if (Convert.ToString(theDR["BusRuleId"]) == "22")
                {
                    //24 Hour
                    DropDownList ddlSelectList24Hr = new DropDownList();
                    ddlSelectList24Hr.ID = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "24Hr" + "-" + tabId;
                    ddlSelectList24Hr.Width = 60;
                    DataTable theDT24 = theUtil.CreateHourFormat_24();
                    DataRow theDR24 = theDT24.NewRow();
                    theDR24[0] = "0";
                    theDR24[1] = "Select";
                    theDT24.Rows.InsertAt(theDR24, 0);
                    ddlSelectList24Hr.DataSource = theDT24;
                    ddlSelectList24Hr.DataTextField = "Time";
                    ddlSelectList24Hr.DataValueField = "Id";
                    ddlSelectList24Hr.DataBind();
                    ddlSelectList24Hr.SelectedValue = "Select";
                    ddlSelectList24Hr.Enabled = theEnable;
                    ddlSelectList24Hr.CssClass = "form-control";
                    DIVCustomItem.Controls.Add(ddlSelectList24Hr);
                    //60 Minutes
                    DropDownList ddlSelectList60Min = new DropDownList();
                    ddlSelectList60Min.ID = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "Min" + "-" + tabId;
                    ddlSelectList60Min.Width = 60;
                    DataTable theDT60 = theUtil.CreateMinuteFormat();
                    DataRow theDR60 = theDT60.NewRow();
                    theDR60[0] = "0";
                    theDR60[1] = "Select";
                    theDT60.Rows.InsertAt(theDR60, 0);
                    ddlSelectList60Min.DataSource = theDT60;
                    ddlSelectList60Min.DataTextField = "Time";
                    ddlSelectList60Min.DataValueField = "Id";
                    ddlSelectList60Min.DataBind();
                    ddlSelectList60Min.SelectedValue = "Select";
                    ddlSelectList60Min.Enabled = theEnable;
                    ddlSelectList60Min.CssClass = "form-control";
                    DIVCustomItem.Controls.Add(ddlSelectList60Min);
                    Format = 24;
                    lblFormat.Text = "24Hr";
                    lblFormat.CssClass = "smalllabel";
                    DIVCustomItem.Controls.Add(lblFormat);
                }
                else if (Convert.ToString(theDR["BusRuleId"]) == "23")
                {
                    //12 Hour
                    DropDownList ddlSelectList12Hr = new DropDownList();
                    ddlSelectList12Hr.ID = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "12Hr" + "-" + tabId;
                    ddlSelectList12Hr.Width = 60;
                    DataTable theDT12 = theUtil.CreateHourFormat_12();
                    DataRow theDR12 = theDT12.NewRow();
                    theDR12[0] = "0";
                    theDR12[1] = "Select";
                    theDT12.Rows.InsertAt(theDR12, 0);
                    ddlSelectList12Hr.DataSource = theDT12;
                    ddlSelectList12Hr.DataTextField = "Time";
                    ddlSelectList12Hr.DataValueField = "Id";
                    ddlSelectList12Hr.DataBind();
                    ddlSelectList12Hr.SelectedValue = "Select";
                    ddlSelectList12Hr.CssClass = "form-control";
                    ddlSelectList12Hr.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(ddlSelectList12Hr);
                    //60 Minutes
                    DropDownList ddlSelectList60Min = new DropDownList();
                    ddlSelectList60Min.ID = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "Min" + "-" + tabId;
                    ddlSelectList60Min.Width = 60;
                    DataTable theDT60 = theUtil.CreateMinuteFormat();
                    DataRow theDR60 = theDT60.NewRow();
                    theDR60[0] = "0";
                    theDR60[1] = "Select";
                    theDT60.Rows.InsertAt(theDR60, 0);
                    ddlSelectList60Min.DataSource = theDT60;
                    ddlSelectList60Min.DataTextField = "Time";
                    ddlSelectList60Min.DataValueField = "Id";
                    ddlSelectList60Min.DataBind();
                    ddlSelectList60Min.SelectedValue = "Select";
                    ddlSelectList60Min.CssClass = "form-control";
                    ddlSelectList60Min.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(ddlSelectList60Min);
                    //AM/PM
                    DropDownList ddlSelectListAMPM = new DropDownList();
                    ddlSelectListAMPM.ID = "SELECTLIST-" + fieldName + "-" + tableName + "-" + fieldId + "AMPM" + "-" + tabId;
                    ddlSelectListAMPM.Width = 40;
                    DataTable theDTAMPM = theUtil.CreateAMPM();
                    ddlSelectListAMPM.DataSource = theDTAMPM;
                    ddlSelectListAMPM.DataTextField = "Format";
                    ddlSelectListAMPM.DataValueField = "Id";
                    ddlSelectListAMPM.DataBind();
                    ddlSelectListAMPM.SelectedValue = "0";
                    ddlSelectListAMPM.CssClass = "form-control";
                    ddlSelectListAMPM.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(ddlSelectListAMPM);
                    Format = 12;
                    lblFormat.Text = "12Hr";
                    lblFormat.CssClass = "smalllabel";
                    DIVCustomItem.Controls.Add(lblFormat);
                }
                else if (Convert.ToString(theDR["BusRuleId"]) == "24")
                {
                    DIVCustomItem.Controls.Remove(lblFormat);
                    Button theButton = new Button();
                    if (Format == 12)
                    {
                        theButton.ID = "theBtn12AMPM-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        lblFormat.Text = "12Hr";
                        lblFormat.CssClass = "smalllabel";
                        theButton.Text = "Set Time";
                        theButton.Click += new EventHandler(theButton_Click);
                        theButton.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(theButton);
                    }
                    else if (Format == 24)
                    {
                        theButton.ID = "theBtn-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        lblFormat.Text = "24Hr";
                        lblFormat.CssClass = "smalllabel";
                        theButton.Text = "Set Time";
                        theButton.Click += new EventHandler(theButton_Click);
                        theButton.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(theButton);
                    }
                    DIVCustomItem.Controls.Add(lblFormat);
                }
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }

        private void CreateICD10Field(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));

            Label theLabel = new Label();
            theLabel.ID = "lblICD10-" + fieldName;
            theLabel.Text = fieldLabel;
            theLabel.Font.Bold = true;
            DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " </label>"));
            ICD10Heading(fieldId);
            DIVCustomItem.Controls.Add(new LiteralControl("<div class = 'customdivbordericd'  runat='server' nowrap='nowrap'>"));
            string bindSource = "VW_ICDList";
            DataView theDV = new DataView(theDSXML.Tables[bindSource]);
            theDV.RowFilter = "FieldId=" + fieldId + "and DeleteFlag=0";

            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            if (Session["SelectedICD" + Convert.ToInt32(fieldId) + ""] == null)
            {
                Session["SelectedICD" + Convert.ToInt32(fieldId) + ""] = CreateColumntheDTICD10();
                theDV = new DataView((DataTable)ViewState["ICD10"]);
                theDV.RowFilter = "FieldId=" + fieldId + " and Visit_pk=" + Convert.ToInt32(Session["PatientVisitId"]) + " and LocationID =" + Convert.ToInt32(Session["ServiceLocationId"]) + "";
                DataTable theDTTemp = theDV.ToTable();
                foreach (DataRow theDRICD1 in theDTTemp.Rows)
                {
                    DataRow[] rows = theDT.Select("Name='" + theDRICD1["Name"].ToString().Replace("'", "") + "'");
                    if (rows.Length == 0)
                    {
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(fieldId) + ""]).Rows.Add(theDRICD1["FieldId"], theDRICD1["BlockId"], theDRICD1["SubBlockId"], theDRICD1["Id"], theDRICD1["CodeID"], theDRICD1["Name"]);
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(fieldId) + ""]).AcceptChanges();
                    }
                }
            }
            if (((DataTable)Session["SelectedICD" + Convert.ToInt32(fieldId) + ""]) != null)
            {
                theDT.Merge(((DataTable)Session["SelectedICD" + Convert.ToInt32(fieldId) + ""]));
            }
            if (theDT != null)
            {
                Hashtable ht = new Hashtable();
                ht.Clear();
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    if (!(ht.ContainsKey(Convert.ToString("CHKMULTI-" + fieldId + theDT.Rows[i][4] + "-" + "dtl_ICD10Field" + "-" + fieldName))))
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='50%'>"));
                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + fieldId + theDT.Rows[i][4] + "-" + "dtl_ICD10Field" + "-" + fieldName + "-" + tabId);
                        ht.Add(chkbox.ID, chkbox.ID);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        DIVCustomItem.Controls.Add(chkbox);
                        //chkbox.Width = 400;
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='30%'>"));
                        TextBox theDateText = new TextBox();
                        theDateText.ID = "TXTDT-" + fieldId + theDT.Rows[i][4].ToString().Replace('%', '^') + "OnSetDate" + "-" + "dtl_ICD10Field" + "-" + fieldName + "-" + tabId;
                        Control ctl = theDateText;
                        theDateText.Width = 83;
                        theDateText.MaxLength = 11;
                        DIVCustomItem.Controls.Add(theDateText);
                        theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                        theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                        DIVCustomItem.Controls.Add(new LiteralControl("&nbsp;"));
                        tabContainer.ID = "TAB";
                        Image theDateImage = new Image();
                        theDateImage.ID = "img" + theDateText.ID;
                        theDateImage.Height = 22;
                        theDateImage.Width = 22;
                        theDateImage.Visible = theEnable;
                        theDateImage.ToolTip = "Date Helper";
                        theDateImage.ImageUrl = "~/images/cal_icon.gif";
                        theDateImage.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "');");
                        DIVCustomItem.Controls.Add(theDateImage);
                        DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("&nbsp&nbsp;"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='20%'>"));
                        TextBox theCommentText = new TextBox();
                        theCommentText.ID = "TXTComment-" + fieldId + theDT.Rows[i][4].ToString().Replace('%', '~') + "ICDComment" + "-" + "dtl_ICD10Field" + "-" + fieldName + "-" + tabId;
                        theCommentText.Width = 250;
                        theCommentText.MaxLength = 200;
                        DIVCustomItem.Controls.Add(theCommentText);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                        //DIVCustomItem.Controls.Add(new LiteralControl("<br>"));
                    }
                }
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
            Button theBtn = new Button();
            theBtn.Width = 180;
            theBtn.ID = "BtnICD10-" + fieldId + "-" + "dtl_ICD10Field" + "-" + fieldName;
            theBtn.Text = "Other ICD10 Disease/Symptom";
            theBtn.Enabled = theEnable;
            theBtn.Attributes.Add("onclick", "javascript:ICD10PopUp('" + fieldId + "'," + Convert.ToInt32(Session["PatientVisitId"]) + "); return false");
            DIVCustomItem.Controls.Add(theBtn);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }

        private void CreateDiseaseSymptomField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, string codeId, string bindSource)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

            //WithPanel

            Panel PnlMulti = new Panel();
            PnlMulti.ID = "Pnl_" + fieldId;
            PnlMulti.ToolTip = fieldLabel;
            PnlMulti.Enabled = theEnable;
            PnlMulti.Controls.Add(new LiteralControl("<div class = 'diseasesymptomdivborder'  runat='server' nowrap='nowrap'>"));

            if (codeId == "")
            {
                codeId = "0";
            }
            DataView theDV = new DataView(theDSXML.Tables[bindSource]);
            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            if (theDT != null)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    CheckBox chkbox = new CheckBox();
                    chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId);
                    chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                    if (chkbox.Text == "Other")
                    {
                        PnlMulti.Controls.Add(chkbox);
                        PnlMulti.Controls.Add(new LiteralControl("<div  class='pad10' id='" + chkbox.ID + '-' + fieldName + "' style='display:none'>Specify: "));
                        HtmlInputText HTextother = new HtmlInputText();
                        HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        HTextother.Size = 20;
                        PnlMulti.Controls.Add(HTextother);
                        PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));

                        HtmlInputText HTextICDCode = new HtmlInputText();
                        HTextICDCode.ID = "TXTMULTIICDCode-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        HTextICDCode.Size = 10;
                        HTextICDCode.Visible = false;
                        PnlMulti.Controls.Add(HTextICDCode);
                        PnlMulti.Controls.Add(new LiteralControl(HTextICDCode.Value));

                        Button theBtn = new Button();
                        theBtn.Width = 100;
                        theBtn.ID = "Btn-" + fieldName + "-" + i + "-" + fieldId;
                        theBtn.Text = "ICDCode";
                        theBtn.Attributes.Add("onclick", "javascript:OpenTreeViewPopup('" + HTextother.ID + "', '" + HTextICDCode.ID + "'); return false");
                        PnlMulti.Controls.Add(theBtn);

                        PnlMulti.Controls.Add(new LiteralControl("</div>"));
                        if (theConditional == true && theEnable == true)
                            chkbox.Attributes.Add("onclick", "toggle('" + fieldName + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                        else
                            chkbox.Attributes.Add("onclick", "toggle('" + chkbox.ID + "-" + fieldName + "');");
                        PnlMulti.Controls.Add(new LiteralControl("<br>"));
                    }
                    else
                    {
                        if (theConditional == true && theEnable == true)
                            chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                        //chkbox.Load += new EventHandler(MultiSelect_Load);

                        PnlMulti.Controls.Add(chkbox);
                        //ApplyBusinessRules(chkbox, ControlID);
                        chkbox.Width = 300;
                        PnlMulti.Controls.Add(new LiteralControl("<br>"));
                    }
                }
            }
            PnlMulti.Controls.Add(new LiteralControl("</div>"));

            DIVCustomItem.Controls.Add(PnlMulti);
            ApplyBusinessRules(PnlMulti, controlId.ToString(), theEnable);
            //PnlMulti.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }


        private void CreateBMIField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
            TextBox theSingleDecimalText = new TextBox();
            theSingleDecimalText.ID = "TXT-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            // theSingleDecimalText.ID = "TXT-BMI-DTL_PATIENTVITALS-" + FieldId + "-" + TabID;
            //theSingleDecimalText.Text="00";
            //  theSingleDecimalText.Width = 180;
            theSingleDecimalText.CssClass = "form-control";
            theSingleDecimalText.MaxLength = 50;
            theSingleDecimalText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(theSingleDecimalText);
            ApplyBusinessRules(theSingleDecimalText, controlId.ToString(), theEnable);
            tabContainer.ID = "TAB";
            theSingleDecimalText.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "')");

            BmiID = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;

            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);

            //theSingleDecimalText.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            Int32 PatientID = Convert.ToInt32(Session["PatientId"]);
            Int32 VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            Int32 LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

            Int32 statusHW = 0;

            if (IsHeightAvail == true && IsWeightAvail == true)
            {
                statusHW = 1;
            }

            DataTable dt = LabResultManager.GetBmiValue(PatientID, LocationID, VisitID, statusHW);
            if (dt.Rows.Count > 0)
            {
                theSingleDecimalText.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dt.Rows[0]["BMI"].ToString()), 1));
            }

            theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
            hdnBNI.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
        }

        private void CreateDrugSelectionField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
            IQCareUtils theUtils = new IQCareUtils();
            DrugType = GetFilterId(fieldId, fieldName);
            DataView theDVName = new DataView((DataTable)Session["DrugTypeName"]);
            if (DrugType > 0)
            {
                theDVName.RowFilter = "DrugTypeId=" + DrugType + "";
                HiddenField theHF = new HiddenField();
                Label theLabel = new Label();
                theLabel.ID = "lblDrg-" + fieldName + "-" + DrugType;
                theHF.ID = "hfDrg-11-" + fieldId + "-" + fieldName + "-" + DrugType;
                theLabel.Text = fieldLabel + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                theLabel.Font.Bold = true;
                if (IsRequiredField(fieldId, fieldName) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                    ARLHeader.Add(theLabel.Text);
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                }
                theHF.Value = fieldLabel + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                DIVCustomItem.Controls.Add(theHF);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                Button theBtn = new Button();
                theBtn.Width = 100;
                theBtn.ID = "BtnDrg-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                theBtn.Text = "Drug Selection";

                if (Session["Selected" + DrugType + ""] == null)
                {
                    DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                    theDV.RowFilter = "DrugTypeId=" + DrugType + " and Generic=0";
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    Session["" + DrugType + ""] = theDT;
                }
                theBtn.Enabled = theEnable;
                theBtn.Attributes.Add("onclick", "javascript:OpenPharmacyDialog('" + DrugType + "'); return false");
                DIVCustomItem.Controls.Add(theBtn);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                DrugsHeading(DrugType);
                ApplyBusinessRules(theBtn, theBtn.ID, theEnable);
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    DrugDataBinding(theBtn.ID, DrugType);
                }

                if ((DataTable)Session["Selected" + DrugType + ""] != null)
                {
                    DataTable theDT = (DataTable)Session["Selected" + DrugType + ""];
                    LoadNewDrugs(theDT);
                }

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }
        }

        private void CreateRegimenField(string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            Label theLabel = new Label();
            HiddenField theHF = new HiddenField();
            if (IsRequiredField(fieldId, fieldName) == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
                ARLHeader.Add(fieldLabel);
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
            RegimenType = GetFilterId(fieldId, fieldName);
            theHF.ID = "hfReg-10-" + fieldId + "-" + tableName + "-" + fieldName + "-" + RegimenType;
            theHF.Value = fieldLabel;
            DIVCustomItem.Controls.Add(theHF);
            TextBox theRegText = new TextBox();
            theRegText.ID = "TXTReg-" + fieldName + "-" + tableName + "-" + fieldId + "=" + RegimenType + "-" + tabId;
            theRegText.Attributes.Add("readonly", "readonly");
            //theRegText.Enabled = theEnable;
            theRegText.Width = 100;
            theRegText.MaxLength = 200;
            DIVCustomItem.Controls.Add(theRegText);
            tabContainer.ID = "TAB";
            Control ctl = theRegText;
            IQCareUtils theUtils = new IQCareUtils();
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    Session.Remove("SelectedReg" + fieldId + RegimenType + "");
                }
            }
            if (Session["SelectedReg" + fieldId + RegimenType + ""] == null)
            {
                DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                Session["Reg" + fieldId + RegimenType + ""] = theDT;
            }

            HtmlInputButton theBtn = new HtmlInputButton();
            theBtn.ID = "BtnRegimen-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
            theBtn.Visible = theEnable;
            theBtn.Value = "...";
            theBtn.Attributes.Add("onclick", "javascript:OpenRegimenDialog('" + RegimenType + "','ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "'); return false");
            DIVCustomItem.Controls.Add(theBtn);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }

        private void CreateMultiSelectListField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate, string codeId, string bindSource)
        {
            DataTable dtBusinessRules = (DataTable)ViewState["BusRule"];
            if (Convert.ToInt32(Session["busRulChk"]) == 1)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<div class='customdivborder leftallign' runat='server' nowrap='nowrap'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<table width=100%>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));

                if (IsRequiredField(fieldId, fieldName) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " </label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " </label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                Int32 theColCount = 1;
                if (dtBusinessRules.Rows.Count > 0)
                {
                    foreach (DataRow DR in dtBusinessRules.Rows)
                    {
                        if ((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18")
                            || (fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19")
                            || (fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                        {
                            if ((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                            {
                                DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + DR["Value"] + "-" + fieldId + "'>" + DR["Value"] + " </label>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                theColCount = theColCount + 1;
                            }

                            if (fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19")
                            {
                                string[] arrValue = DR["Value"].ToString().Split('_');
                                for (int i = 0; i < arrValue.Length; i++)
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + arrValue[i] + "-" + fieldId + "'>" + arrValue[i] + " </label>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    theColCount = theColCount + 1;
                                }
                            }
                            else if (((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18")))
                            {
                                DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + DR["Value"] + "-" + fieldId + "'>" + DR["Value"] + " </label>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            }
                        }
                    }
                }
                while (theColCount <= 3)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    theColCount = theColCount + 1;
                }

                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td colspan='4' class='border'>"));

                //WithPanel
                Panel PnlMulti = new Panel();
                PnlMulti.ID = "Pnl_-" + tableName + "-" + fieldId;
                PnlMulti.ToolTip = fieldLabel;
                PnlMulti.Enabled = theEnable;
                PnlMulti.Controls.Add(new LiteralControl("<div class='customdivborder1 leftallign' runat='server' nowrap='nowrap'>"));


                if (codeId == "")
                {
                    codeId = "0";
                }
                if (bindSource == "")
                {
                    bindSource = "MST_MODDECODE";
                    codeId = "-1";
                }
                string theCodeFldName = "";
                DataTable theBindTbl = theDSXML.Tables[bindSource];
                if (theBindTbl.Columns.Contains("CategoryId") == true)
                    theCodeFldName = "CategoryId";
                else if (theBindTbl.Columns.Contains("SectionId") == true)
                    theCodeFldName = "SectionId";
                else
                    theCodeFldName = "CodeId";
                DataView theDV = new DataView(theDSXML.Tables[bindSource]);
                if (bindSource.ToUpper() == "MST_DECODE")
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                }
                else
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + codeId + "";
                }

                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                if (theDT != null)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        // Dates Control creation for multi Select list
                        //Date 1 Control
                        tabContainer.ID = "TAB";
                        TextBox theDate1 = new TextBox();
                        theDate1.ID = "TXTDT1-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        Control ctl = theDate1;
                        theDate1.Width = 83;
                        theDate1.MaxLength = 11;
                        theDate1.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

                        tabContainer.ID = "TAB";
                        string thDTVar = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDate1.ClientID;
                        theDate1.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVar + "', '" + thDTVar + "')");

                        Image theDateImage1 = new Image();
                        theDateImage1.ID = "img" + theDate1.ID;
                        theDateImage1.Height = 22;
                        theDateImage1.Width = 22;
                        theDateImage1.Visible = theEnable;
                        theDateImage1.ToolTip = "Date Helper";
                        theDateImage1.ImageUrl = "~/images/cal_icon.gif";
                        theDateImage1.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "');");
                        theDate1.Visible = false;
                        theDateImage1.Visible = false;
                        //Date 2 Control
                        TextBox theDate2 = new TextBox();
                        theDate2.ID = "TXTDT2-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        Control ctl2 = theDate2;
                        theDate2.Width = 83;
                        theDate2.MaxLength = 11;
                        theDate2.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                        theDate2.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

                        tabContainer.ID = "TAB";
                        string thDTVarOth = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDate2.ClientID;
                        theDate2.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVarOth + "', '" + thDTVarOth + "')");

                        Image theDateImage2 = new Image();
                        theDateImage2.ID = "img" + theDate2.ID;
                        theDateImage2.Height = 22;
                        theDateImage2.Width = 22;
                        theDateImage2.Visible = theEnable;
                        theDateImage2.ToolTip = "Date Helper";
                        theDateImage2.ImageUrl = "~/images/cal_icon.gif";
                        theDateImage2.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl2).ClientID + "');");

                        // Text control for numeric field
                        TextBox theNumeric = new TextBox();
                        theNumeric.ID = "TXTNUM-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                        theNumeric.Width = 83;
                        theNumeric.MaxLength = 3;
                        theNumeric.Attributes.Add("onkeypress", "return isNumberKey(event);");

                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        CheckBox thehiddenchkbox = new CheckBox();
                        thehiddenchkbox.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ClientID + "";
                        thehiddenchkbox.Width = 0;
                        divhidden.Controls.Add(thehiddenchkbox);
                        if (chkbox.Text == "Other")
                        {
                            PnlMulti.Controls.Add(chkbox);
                            PnlMulti.Controls.Add(new LiteralControl("<div  id='" + fieldName + "' style='display:block'>Specify: "));
                            HtmlInputText HTextother = new HtmlInputText();
                            HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                            HTextother.Size = 10;
                            PnlMulti.Controls.Add(HTextother);
                            PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                            PnlMulti.Controls.Add(new LiteralControl("</div>"));
                            if (theConditional == true && theEnable == true)
                            {
                                DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                                DataView theDVC = new DataView(thDTC);
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        //EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                        EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");

                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                }
                            }
                            //chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                            else
                                chkbox.Attributes.Add("onclick", "toggle('" + fieldName + "');");
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));
                        }
                        else
                        {
                            if (theConditional == true && theEnable == true)
                            {
                                DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                                DataView theDVC = new DataView(thDTC);
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        //  EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                        EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                }
                                //chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                            }
                            //chkbox.Load += new EventHandler(MultiSelect_Load);

                            PnlMulti.Controls.Add(chkbox);
                            if (dtBusinessRules.Rows.Count > 0)
                            {
                                foreach (DataRow DR in dtBusinessRules.Rows)
                                {
                                    if ((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18"))
                                    {
                                        PnlMulti.Controls.Add(theDate1);
                                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                        PnlMulti.Controls.Add(theDateImage1);
                                        PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                        theDate1.Visible = true;
                                        theDateImage1.Visible = true;
                                    }
                                    else if ((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19"))
                                    {
                                        Label theSpace2 = new Label();
                                        if (theDate1.Visible != true && theDateImage1.Visible != true)
                                        {
                                            theDate1.Visible = true;
                                            theSpace2.ID = "theSpace2" + chkbox.ID;
                                            theSpace2.Text = "";
                                            theSpace2.Width = 25;
                                            PnlMulti.Controls.Add(theSpace2);
                                            theDateImage1.Visible = true;
                                            PnlMulti.Controls.Add(theDate1);
                                            PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                            PnlMulti.Controls.Add(theDateImage1);
                                            PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                        }
                                        theSpace2.ID = "theSpace2_2" + chkbox.ID;
                                        theSpace2.Text = "";
                                        theSpace2.Width = 25;
                                        PnlMulti.Controls.Add(theSpace2);
                                        PnlMulti.Controls.Add(theDate2);
                                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                        PnlMulti.Controls.Add(theDateImage2);
                                        PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                    }
                                    else if ((fieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                                    {
                                        Label theSpace3 = new Label();
                                        theSpace3.ID = "theSpace3" + chkbox.ID;
                                        theSpace3.Text = "";
                                        theSpace3.Width = 25;
                                        PnlMulti.Controls.Add(theSpace3);
                                        PnlMulti.Controls.Add(theNumeric);
                                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                    }
                                }
                            }
                            chkbox.Width = 210;
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));
                        }
                    }
                }
                PnlMulti.Controls.Add(new LiteralControl("</div>"));

                DIVCustomItem.Controls.Add(PnlMulti);
                ApplyBusinessRules(PnlMulti, controlId.ToString(), theEnable);
                //PnlMulti.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (IsRequiredField(fieldId, fieldName) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + fieldLabel + "-" + fieldId + "'>" + fieldLabel + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                //WithPanel
                Panel PnlMulti = new Panel();
                PnlMulti.ID = "Pnl_-" + tableName + "-" + fieldId;
                PnlMulti.ToolTip = fieldLabel;
                PnlMulti.Enabled = theEnable;
                PnlMulti.Controls.Add(new LiteralControl("<DIV class ='customdivbordermultiselect'  runat='server' nowrap='nowrap'>"));
                tabContainer.ID = "TAB";
                if (codeId == "")
                {
                    codeId = "0";
                }
                string theCodeFldName = "";
                if (bindSource == "")
                {
                    bindSource = "MST_MODDECODE";
                    codeId = "-1";
                }
                DataTable theBindTbl = theDSXML.Tables[bindSource];
                if (theBindTbl.Columns.Contains("CategoryId") == true)
                    theCodeFldName = "CategoryId";
                else if (theBindTbl.Columns.Contains("SectionId") == true)
                    theCodeFldName = "SectionId";
                else
                    theCodeFldName = "CodeId";
                DataView theDV = new DataView(theDSXML.Tables[bindSource]);
                if (bindSource.ToUpper() == "MST_DECODE")
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                }
                else
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + codeId + "";
                }
                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                DataView theDVC = new DataView(thDTC);
                if (theDT != null)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        CheckBox thehiddenchkbox = new CheckBox();
                        thehiddenchkbox.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ClientID + "";
                        thehiddenchkbox.Width = 0;
                        divhidden.Controls.Add(thehiddenchkbox);
                        if (chkbox.Text == "Other")
                        {
                            PnlMulti.Controls.Add(chkbox);
                            PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + fieldName + "' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextother = new HtmlInputText();
                            HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                            HTextother.Size = 10;
                            PnlMulti.Controls.Add(HTextother);
                            PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                            PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                            if (theConditional == true && theEnable == true)
                            {
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        // string pref = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_"; 
                                        //EnableDisableControl.Append(string.Format("EnableValuechkbox('{0}{1}','{0}{2}','{3}','{4}','{5}')"
                                        //    , pref, chkbox.ClientID,
                                        //    HTextother.ClientID,theDRC["ConditionalFieldSectionId"].ToString(),theDT.Rows[i][0].ToString(),fieldName
                                        //    ));
                                        EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                }
                            }
                            else
                            {
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl_1 = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        EnableDisableControl_1.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl_1 + "");
                                }
                                PnlMulti.Controls.Add(new LiteralControl("<br>"));
                            }
                        }
                        else
                        {
                            if (theConditional == true && theEnable == true)
                            {
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                }
                            }
                            else
                            {
                                theDVC.RowFilter = "ConFieldId=" + fieldId + "";
                                StringBuilder EnableDisableControl_1 = new StringBuilder();
                                if (theDVC.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                    {
                                        EnableDisableControl_1.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                    }
                                    chkbox.Attributes.Add("onclick", "" + EnableDisableControl_1 + "");
                                }
                            }
                            // PnlMulti.Controls.Add(new LiteralControl("<div class=\"checkbox\"> <label>"));
                            PnlMulti.Controls.Add(chkbox);
                            // PnlMulti.Controls.Add(new LiteralControl("</label></div> "));
                            chkbox.Width = 300;
                            PnlMulti.Controls.Add(new LiteralControl("<br />"));
                        }
                    }
                }
                PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                DIVCustomItem.Controls.Add(PnlMulti);
                ApplyBusinessRules(PnlMulti, controlId.ToString(), theEnable);
                //PnlMulti.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }

        }
        private void CreateMissingFieldAlert(string fieldId, string fieldName, string tabId, string erroMessage)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right' class=\"alert alert-danger\">"));


            DIVCustomItem.Controls.Add(new LiteralControl(string.Format("<strong>Error!</strong> The field {0} is not well formed.{1}", fieldName, erroMessage)));


            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        private void CreatePlaceholderField(string fieldId, string fieldName, string tabId)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right'>"));
            HtmlGenericControl div1 = new HtmlGenericControl("div");
            div1.ID = "DIVPLC-" + fieldName + "-" + fieldId;
            PlaceHolder thePlchlderText = new PlaceHolder();
            thePlchlderText.ID = "plchlder-" + fieldName + "-" + fieldId + "-" + tabId;
            thePlchlderText.Controls.Add(div1);
            DIVCustomItem.Controls.Add(thePlchlderText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }


        /// <summary>
        /// Loads the new drugs.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void LoadNewDrugs(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToInt32(theDR["Flag"]) == 0)
                {
                    BindDrugControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), DrugType, Convert.ToInt32(theDR["Flag"]));
                }
            }
        }

        /// <summary>
        /// Loads the predefined label_ field.
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="pateintId">The patient identifier.</param>
        private void LoadPredefinedLabel_Field(int featureId, int pateintId)
        {
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            ICustomForm custFormMgr = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataSet mainDataset = custFormMgr.GetFieldName_and_Label(featureId, pateintId);
            ViewState["ICD10"] = mainDataset.Tables[22];
            DIVCustomItem.Controls.Clear();

            if (mainDataset.Tables[19].Rows.Count > 0)
            {
                this.customFieldData = mainDataset.Tables[19];
            }

            if (mainDataset.Tables[20].Rows.Count > 0)
            {
                this.visitDetail = mainDataset.Tables[20];
            }

            if (mainDataset.Tables[2].Rows.Count > 0)
            {
                DataView theDV = new DataView(mainDataset.Tables[2]);
                theDV.RowFilter = "BusRuleId = 13";
                if (theDV.Count > 0)
                {
                    //btncomplete.Visible = true;
                }
            }
            //try
            //{
            //    IsSIngleVisit = false;
            //For Single Visit or MultiVisit
            if (Convert.ToInt32(mainDataset.Tables[14].Rows[0]["MultiVisit"]) == 0)
            {
                // todo
                this.IsSingleVisit = true;
                Label1.InnerHtml = "First Encounter Date:";
                if (mainDataset.Tables[15].Rows.Count > 0)
                {
                    txtvisitDate.Text = string.Format("{0:dd-MMM-yyyy}", mainDataset.Tables[15].Rows[0]["VisitDate"]);
                    hdnVisitData.Value = string.Format("{0:dd-MMM-yyyy}", mainDataset.Tables[15].Rows[0]["VisitDate"]);
                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "DataEncounter();", true);

                    Session["PatientVisitId"] = mainDataset.Tables[15].Rows[0]["Visit_Id"];
                    Session["ServiceLocationId"] = mainDataset.Tables[0].Rows[0]["LocationId"];
                }
            }

            //All tables are put in Session in order to bind strength, UnitID, Frequency etc for Drug.
            Session["AllData"] = mainDataset;
            //Drug and Regimen Master Data
            if (Session["SelectedCustomfrmRegimen"] == null)
            {
                Session["MasterCustomfrmReg"] = mainDataset.Tables[4];
                Session["DrugTypeName"] = mainDataset.Tables[13];
            }
            //LabMasterData
            Session["MasterData"] = mainDataset.Tables[6];

            //----- Clearing dynamic Drug and ICD10 Session.
            if (!IsPostBack)
            {
                foreach (DataRow r in ((DataTable)Session["MasterCustomfrmReg"]).Rows)
                {
                    if (Session["Selected" + r["DrugTypeId"].ToString() + ""] != null)
                    {
                        Session.Remove("Selected" + r["DrugTypeId"].ToString() + "");
                    }
                }
                Session.Remove("SelectedData");
                foreach (DataRow theDRICDRemoveSession in mainDataset.Tables[1].Rows)
                {
                    if (Session["SelectedICD" + Convert.ToInt32(theDRICDRemoveSession["FieldId"]) + ""] != null && Convert.ToInt32(theDRICDRemoveSession["ControlId"]) == 16)
                    {
                        Session.Remove("SelectedICD" + Convert.ToInt32(theDRICDRemoveSession["FieldId"]) + "");
                    }
                }
            }
            if (mainDataset.Tables[1].Rows.Count > 0)
            {
                formDescription.InnerHtml = mainDataset.Tables[1].Rows[0]["FormDescription"].ToString();
                lblFeatureName.Text = mainDataset.Tables[1].Rows[0]["FeatureName"].ToString();
            }

            ViewState["BusRule"] = mainDataset.Tables[2];

            if (mainDataset.Tables[0].Rows.Count > 0)
            {
                hdfldDOB.Value = string.Format("{0:dd-MMM-yyyy}", mainDataset.Tables[0].Rows[0]["DOB"]);
            }
            //For Loading Controls in the form
            if (mainDataset.Tables[1].Rows.Count > 0)
            {
                ViewState["LnkTable"] = mainDataset.Tables[1];
            }
            DataTable tableSection = mainDataset.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName", "SectionInfo", "IsGridView", "TabId").Copy();
            int Numtds = 2, td = 1;
            DIVCustomItem.Controls.Clear();

            DataTable theConditionalFields = mainDataset.Tables[17].Copy();
            theConditionalFields.Columns.Add("Load", typeof(string));
            theConditionalFields.Columns["Load"].DefaultValue = "T";

            foreach (DataRow theMDR in mainDataset.Tables[17].Rows)
            {
                int theFieldId = Convert.ToInt32(theMDR["FieldId"]);
                bool theRecFnd = false;
                foreach (DataRow theDR in theConditionalFields.Rows)
                {
                    if (Convert.ToInt32(theDR["FieldId"]) == theFieldId && theRecFnd == true)
                        theDR["Load"] = "F";
                    else if (Convert.ToInt32(theDR["FieldId"]) == theFieldId)
                    {
                        theDR["Load"] = "T";
                        theRecFnd = true;
                    }
                }
                theRecFnd = false;
            }
            theConditionalFields.AcceptChanges();
            this.CreateTab(mainDataset.Tables[23]);
            int z = 0, SignatureFlag = 0;
            foreach (DataRow tabRow in mainDataset.Tables[23].Rows)
            {
                DIVCustomItem = new Panel();
                DIVCustomItem.CssClass = "border center formbg";
                DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
                foreach (DataRow sectionRow in tableSection.Rows)
                {
                    bool spanfieldflag = false;
                    string sectionId = Convert.ToString(sectionRow["SectionID"]);
                    bool isGridviewSection = (sectionRow["IsGridView"].ToString() == "1");
                    if (Convert.ToInt32(tabRow["TabId"]) == Convert.ToInt32(sectionRow["TabId"]))
                    {
                        tbChildPanel.ID = sectionRow["TabId"].ToString();
                        SectionHeading(sectionRow["SectionName"].ToString(), sectionRow["SectionInfo"].ToString());
                        DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));

                        //bool isHeight = false;
                        //bool isWeight = false;

                        foreach (DataRow fieldRow in mainDataset.Tables[1].Rows)
                        {
                            int controlId = Convert.ToInt32(fieldRow["ControlId"]);
                            string fieldName = fieldRow["FieldName"].ToString();
                            string fieldId = fieldRow["FieldID"].ToString();
                            string codeId = fieldRow["CodeID"].ToString();
                            string fieldLabel = fieldRow["FieldLabel"].ToString();
                            string tableName = fieldRow["PDFTableName"].ToString();
                            string tabId = fieldRow["TabId"].ToString();
                            string bindSource = fieldRow["BindSource"].ToString();
                            string bindCategory = fieldRow["BindCategory"].ToString();
                            string controlReferenceId = fieldRow["ReferenceId"].ToString();

                            DataView dvchkHeight = mainDataset.Tables[1].DefaultView;
                            dvchkHeight.RowFilter = "FieldName = 'Height'   and Predefined=1";
                            if (dvchkHeight.Count > 0)
                            {
                                this.IsHeightAvail = true;
                            }
                            DataView dvchkWeight = mainDataset.Tables[1].DefaultView;
                            dvchkWeight.RowFilter = "FieldName = 'Weight'   and Predefined=1";
                            if (dvchkWeight.Count > 0)
                            {
                                this.IsWeightAvail = true;
                            }
                            if (sectionRow["SectionID"].ToString() == fieldRow["SectionID"].ToString())
                            {
                                #region "CheckConditionalFields"

                                DataView theDVConditionalField = new DataView(theConditionalFields);
                                theDVConditionalField.RowFilter = "ConFieldId=" + fieldRow["FieldID"].ToString() + " and ConFieldPredefined=" + fieldRow["Predefined"].ToString() + " and Load = 'T'";
                                theDVConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                                if (theDVConditionalField.Count > 0)
                                    theConditional = true;
                                else
                                    theConditional = false;

                                #endregion "CheckConditionalFields"

                                #region "CheckSpanFields"

                                DataView theDVspanField = new DataView(mainDataset.Tables[2]);
                                theDVspanField.RowFilter = "BusRuleId=25 and FieldId = " + fieldRow["FieldID"].ToString();

                                if ((theDVspanField.Count > 0) || Convert.ToString(fieldRow["ReferenceId"]) == "TXT_MULTILINE_LARGE")
                                    spanfieldflag = true;
                                else
                                    spanfieldflag = false;

                                #endregion "CheckSpanFields"

                                #region "Check if Multi select has business rules 18 19 20"

                                DataView theDVMultiSelect = new DataView(mainDataset.Tables[2]);
                                theDVMultiSelect.RowFilter = "(BusRuleId=18 or BusRuleId=19 or BusRuleId=20) and FieldId = " + fieldRow["FieldID"].ToString();

                                if (theDVMultiSelect.Count > 0)
                                    Session["busRulChk"] = 1;
                                else
                                    Session["busRulChk"] = 0;

                                #endregion "Check if Multi select has business rules 18 19 20"
                                if (IsRequiredField(fieldId, fieldName))
                                {
                                    //requiredField.ContainsValue(fieldId)
                                    if (!requiredField.Exists(rf => rf.FieldId == fieldId))
                                    {
                                        //requiredField.Add(Convert.ToInt32(sectionId), fieldId);
                                        requiredField.Add(new RequiredField() { FieldId = fieldId, SectionId = sectionId, IsGrid = isGridviewSection });
                                    }
                                }
                                if (td <= Numtds)
                                {
                                    if (td == 1)
                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                 
                                    if (((Convert.ToInt32(fieldRow["Controlid"]) == 9) && (Convert.ToInt32(Session["busRulChk"]) == 1))
                                        || (Convert.ToInt32(fieldRow["Controlid"]) == 11)
                                        || (Convert.ToInt32(fieldRow["Controlid"]) == 12)
                                        || (Convert.ToInt32(fieldRow["Controlid"]) == 16)

                                        )
                                    {
                                        if (td == 1)
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                            this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            td = 1;
                                        }
                                        else
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                            //  LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                            this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            td = 1;
                                        }
                                    }
                                    else
                                    {
                                        if ((Convert.ToString(fieldRow["ReferenceId"]) == "TXT_MULTILINE" && (spanfieldflag == true)) || (Convert.ToString(fieldRow["ReferenceId"]) == "TXT_MULTILINE_LARGE"))
                                        {
                                            if (td == 1)
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                //  LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                td = 1;
                                            }
                                            else
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                // LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                td = 1;
                                            }
                                        }
                                        else if ((Convert.ToInt32(fieldRow["ControlId"]) == 8) && (spanfieldflag == true))
                                        {
                                            if (td == 1)
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                //  LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                td = 1;
                                            }
                                            else
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                // LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                td = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (td == 1)
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                //  LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                td++;
                                            }
                                            else
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                // LoadFieldTypeControl(fieldRow["Controlid"].ToString(), fieldRow["FieldName"].ToString(), fieldRow["FieldID"].ToString(), fieldRow["CodeID"].ToString(), fieldRow["FieldLabel"].ToString(), fieldRow["PDFTableName"].ToString(), fieldRow["TabId"].ToString(), fieldRow["BindSource"].ToString(), true);
                                                this.LoadFieldTypeControl(controlId, controlReferenceId, fieldName, fieldId, fieldLabel, codeId, bindCategory, bindSource, tableName, tabId, true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                td = 1;
                                            }
                                        }
                                    }
                                }

                                #region "Create Conditional Fields"

                                if (theConditional == true)
                                {
                                    for (int i = 0; i < theDVConditionalField.Count; i++)
                                    {
                                        int con_controlId = Convert.ToInt32(theDVConditionalField[i]["Controlid"]);
                                        string con_fieldName = theDVConditionalField[i]["FieldName"].ToString();
                                        string con_fieldId = theDVConditionalField[i]["FieldID"].ToString();
                                        string con_codeId = theDVConditionalField[i]["CodeID"].ToString();
                                        string con_fieldLabel = theDVConditionalField[i]["FieldLabel"].ToString();
                                        string con_tableName = theDVConditionalField[i]["PDFTableName"].ToString();
                                        string con_tabId = theDVConditionalField[i]["TabId"].ToString();
                                        string con_bindSource = theDVConditionalField[i]["BindSource"].ToString();
                                        string con_bindCategory = theDVConditionalField[i]["BindCategory"].ToString();
                                        string con_controlReferenceId = theDVConditionalField[i]["ReferenceId"].ToString();
                                        if (IsRequiredField(con_fieldId, con_fieldName))
                                        {
                                            //requiredField.ContainsValue(fieldId)
                                            if (!requiredField.Exists(rf => rf.FieldId == con_fieldId))
                                            {
                                                //requiredField.Add(Convert.ToInt32(sectionId), fieldId);
                                                requiredField.Add(new RequiredField() { FieldId = con_fieldId, SectionId = sectionId, IsGrid = isGridviewSection });
                                            }
                                        }
                                        if (td <= Numtds)
                                        {
                                            theSecondLabelConditional = false;
                                            if (td == 1)
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                            if ((Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 11) || (Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 12))
                                            {
                                                if (td == 1)
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    //LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                    //    theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                    //    theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);

                                                    this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false);
                                                    td = 1;
                                                }
                                                else
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false);
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    td = 1;
                                                }
                                            }
                                            else
                                            {
                                                if ((Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 8 && spanfieldflag == true) || Convert.ToInt32(fieldRow["Controlid"]) == 19)
                                                {
                                                    if (td == 1)
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                        this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false);
                                                        td = 1;
                                                    }
                                                    else
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                        this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false);
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        td = 1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (td == 1)
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false); ;
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        td++;
                                                    }
                                                    else
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        this.LoadFieldTypeControl(con_controlId, con_controlReferenceId, con_fieldName, con_fieldId, con_fieldLabel, con_codeId, con_bindCategory, con_bindSource, con_tableName, con_tabId, false);
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        td = 1;
                                                    }
                                                }
                                            }
                                        }
                                        ///////////////////////////////////////////////////////////////////////

                                        #region "CheckSecondLabelConditionalFields"

                                        DataView theDVSecondLabelConditionalField = new DataView(theConditionalFields);

                                        theDVSecondLabelConditionalField.RowFilter = "ConFieldId=" + theDVConditionalField[i]["FieldID"].ToString() + " and Load='T'";
                                        theDVSecondLabelConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                                        if (theDVSecondLabelConditionalField.Count > 0)
                                            theSecondLabelConditional = true;
                                        else
                                            theSecondLabelConditional = false;

                                        #endregion "CheckSecondLabelConditionalFields"

                                        #region "Create Second Label Conditional Fields"

                                        if (theSecondLabelConditional == true)
                                        {
                                            for (int j = 0; j < theDVSecondLabelConditionalField.Count; j++)
                                            {
                                                int sec_con_controlId = Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]);
                                                string sec_con_fieldName = theDVSecondLabelConditionalField[j]["FieldName"].ToString();
                                                string sec_con_fieldId = theDVSecondLabelConditionalField[j]["FieldID"].ToString();
                                                string sec_con_codeId = theDVSecondLabelConditionalField[j]["CodeID"].ToString();
                                                string sec_con_fieldLabel = theDVSecondLabelConditionalField[j]["FieldLabel"].ToString();
                                                string sec_con_tableName = theDVSecondLabelConditionalField[j]["PDFTableName"].ToString();
                                                string sec_con_tabId = theDVSecondLabelConditionalField[j]["TabId"].ToString();
                                                string sec_con_bindSource = theDVSecondLabelConditionalField[j]["BindSource"].ToString();
                                                string sec_con_bindCategory = theDVSecondLabelConditionalField[j]["BindCategory"].ToString();
                                                string sec_con_controlReferenceId = theDVSecondLabelConditionalField[j]["ReferenceId"].ToString();
                                                if (IsRequiredField(sec_con_fieldId, sec_con_fieldName))
                                                {
                                                    //requiredField.ContainsValue(fieldId)
                                                    if (!requiredField.Exists(rf => rf.FieldId == sec_con_fieldId))
                                                    {
                                                        //requiredField.Add(Convert.ToInt32(sectionId), fieldId);
                                                        requiredField.Add(new RequiredField() { FieldId = sec_con_fieldId, SectionId = sectionId, IsGrid = isGridviewSection });
                                                    }
                                                }
                                                if (td <= Numtds)
                                                {
                                                    if (td == 1)
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                                    if ((Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 11) || (Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 12))
                                                    {
                                                        if (td == 1)
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                            this.LoadFieldTypeControl(sec_con_controlId, sec_con_controlReferenceId, sec_con_fieldName, sec_con_fieldId, sec_con_fieldLabel, sec_con_codeId, sec_con_bindCategory, sec_con_bindSource, sec_con_tableName, sec_con_tabId, false);
                                                            td = 1;
                                                        }
                                                        else
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));

                                                            this.LoadFieldTypeControl(sec_con_controlId, sec_con_controlReferenceId, sec_con_fieldName, sec_con_fieldId, sec_con_fieldLabel, sec_con_codeId, sec_con_bindCategory, sec_con_bindSource, sec_con_tableName, sec_con_tabId, false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            td = 1;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (td == 1)
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            this.LoadFieldTypeControl(sec_con_controlId, sec_con_controlReferenceId, sec_con_fieldName, sec_con_fieldId, sec_con_fieldLabel, sec_con_codeId, sec_con_bindCategory, sec_con_bindSource, sec_con_tableName, sec_con_tabId, false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            td++;
                                                        }
                                                        else
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            this.LoadFieldTypeControl(sec_con_controlId, sec_con_controlReferenceId, sec_con_fieldName, sec_con_fieldId, sec_con_fieldLabel, sec_con_codeId, sec_con_bindCategory, sec_con_bindSource, sec_con_tableName, sec_con_tabId, false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            td = 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #endregion "Create Second Label Conditional Fields"


                                    }
                                }

                                #endregion "Create Conditional Fields"
                            }
                        }
                        if (td == 2)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        }
                        td = 1;

                        #region "Grid Section"

                        //if (sectionRow["IsGridView"].ToString() == "1")
                        if (isGridviewSection)
                        {
                            DataTable theDT = new DataTable();

                            DataColumn dtDataColumn;
                            DataTable thedtGridViewField = new DataTable();
                            thedtGridViewField = mainDataset.Tables[1].Copy();
                            DataView theDVGridView = new DataView(thedtGridViewField);
                            theDVGridView.RowFilter = "SectionID =" + sectionId;
                            if (!IsPostBack)
                            {
                                if (gblDTGridViewControls.Rows.Count > 0)
                                {
                                    gblDTGridViewControls.Merge(theDVGridView.ToTable());
                                }
                                else
                                {
                                    gblDTGridViewControls = theDVGridView.ToTable();
                                }
                                ViewState["gblDTGridViewControls"] = gblDTGridViewControls;
                            }
                            for (int i = 0; i < theDVGridView.Count; i++)
                            {
                                dtDataColumn = new DataColumn();
                                dtDataColumn.DataType = GetControlDataType(Convert.ToInt32(theDVGridView[i]["ControlID"]));// Type.GetType("System.String");
                                dtDataColumn.ColumnName = theDVGridView[i]["FieldLabel"].ToString();
                                theDT.Columns.Add(dtDataColumn);
                            }

                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                            GridView objdView = new GridView();

                            objdView.ID = "Dview_" + sectionId;
                            objdView.AutoGenerateColumns = false;

                            objdView.CssClass = "datatable  table-striped table-responsive";
                            objdView.SelectedIndexChanging += new GridViewSelectEventHandler(objdView_SelectedIndexChanging);
                            objdView.RowDeleting += new GridViewDeleteEventHandler(grdView_RowDeleted);
                            objdView.RowDataBound += new GridViewRowEventHandler(grdView_RowDataBound);
                            objdView.AllowSorting = true;
                            objdView.BorderWidth = 1;
                            objdView.GridLines = GridLines.None;
                            objdView.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                            objdView.RowStyle.CssClass = "gridrow";
                            objdView.Width = Unit.Percentage(100);

                            foreach (DataColumn c in theDT.Columns)
                            {
                                if (c.DataType.ToString() == "System.Int32")
                                {
                                    c.DataType = Type.GetType("System.String");
                                }
                                objdView.Columns.Add(CreateBoundColumn(c));
                            }

                            CommandField objfield = new CommandField();
                            objfield.ButtonType = ButtonType.Link;
                            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
                            objfield.ShowDeleteButton = true;
                            objdView.Columns.Add(objfield);

                            Button theBtn = new Button();
                            theBtn.Width = 100;
                            theBtn.ID = "BtnAdd-" + sectionId;
                            theBtn.Text = "Add";
                            theBtn.Enabled = true;
                            theBtn.Click += delegate(object sender, EventArgs e)
                            {
                                TabContainer container = tabContainer;
                                DataRow row = null;
                                row = theDT.NewRow();
                                string btnName = (sender as Button).ID;
                                string[] strsection = btnName.Split('-');
                                if (ViewState["SaveFlag_" + strsection[1]] == null)
                                {
                                    ViewState["SaveFlag_" + strsection[1]] = "Add";
                                }

                                if (ViewState["SaveFlag_" + strsection[1]].ToString() == "Add")
                                {
                                    int gridrecord = 0;
                                    if (ViewState["GridCache_" + strsection[1]] != null)
                                    {
                                        DataTable dtviewstateAdd = (DataTable)ViewState["GridCache_" + strsection[1]];
                                        gridrecord = dtviewstateAdd.Rows.Count + 1;
                                    }

                                    bool isRecord = false;
                                    for (int i = 0; i < theDT.Columns.Count; i++)
                                    {
                                        string ctlvalue = GetGridViewControlValue(container, theDT.Columns[i].ColumnName, theDVGridView.ToTable());// thedtGridViewField);
                                        if (ctlvalue == "")
                                        {
                                            row[i] = DBNull.Value;
                                        }
                                        else
                                        {
                                            row[i] = ctlvalue;
                                            isRecord = true;
                                        }
                                    }
                                    if (isRecord)
                                        theDT.Rows.Add(row);
                                }
                                else if (ViewState["SaveFlag_" + strsection[1]].ToString() == "Edit")
                                {
                                    int r = Convert.ToInt32(ViewState["SelectedRow_" + strsection[1]]);

                                    bool isRecord = false;
                                    DataTable dtviewstate = (DataTable)ViewState["GridCache_" + strsection[1]];
                                    if (dtviewstate.Rows.Count > 0)
                                    {
                                        if (r > -1)
                                        {
                                            dtviewstate.Rows.RemoveAt(r);
                                        }
                                    }
                                    for (int i = 0; i < theDT.Columns.Count; i++)
                                    {
                                        Type typeofdata = typeof(string);

                                        string ctlvalue = GetGridViewControlValue(container, theDT.Columns[i].ColumnName, theDVGridView.ToTable());// thedtGridViewField);
                                        if (ctlvalue == "")
                                        {
                                            row[i] = DBNull.Value;
                                        }
                                        else
                                        {
                                            row[i] = ctlvalue;
                                            isRecord = true;
                                        }
                                    }

                                    if (isRecord)
                                        theDT.Rows.Add(row);

                                    ViewState["SaveFlag_" + strsection[1]] = "Add";
                                    (sender as Button).Text = "Add";
                                }
                                if (ViewState["GridCache_" + strsection[1]] != null)
                                {
                                    DataTable dtviewstate = (DataTable)ViewState["GridCache_" + strsection[1]];
                                    dtviewstate.Merge(theDT);
                                    ViewState["GridCache_" + strsection[1]] = dtviewstate;
                                    BindGridView(strsection[1], container, (DataTable)ViewState["GridCache_" + strsection[1]]);
                                }
                                else
                                {
                                    ViewState["GridCache_" + strsection[1]] = theDT;

                                    BindGridView(strsection[1], container, theDT);
                                }
                            };

                            DIVCustomItem.Controls.Add(theBtn);
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'; height:'25px'; align='center'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<div class='grid'>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<div id = 'div-gridview' class='gridview whitebg'>"));
                            DIVCustomItem.Controls.Add(objdView);

                            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

                            objdView.Visible = true;
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
                    }
                }

                        #endregion "Grid Section"

                #region "Signature Section"

                if (Convert.ToInt32(tabRow["Signature"]) == 1)
                {
                    SignatureFlag = 1;
                    DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl(@"<div class=""form-group"" style=""white-space:nowrap; display:inline"">"));
                    DIVCustomItem.Controls.Add(new LiteralControl(@"<div class=""col-md-2"" style=""white-space:nowrap; text-align:right""></div>"));
                    DIVCustomItem.Controls.Add(new LiteralControl(@"<div class=""col-md-2"" style=""white-space:nowrap; text-align:right"">"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class=''  id='lbl" + tabRow["Signature"] + "' >Signature:</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
                    DropDownList theDropDown = new DropDownList();
                    theDropDown.ID = "SELECTLIST-TABSignature-LNK_FORMTABORDVISIT-'00'-" + tabRow["TabId"];
                    theDropDown.CssClass = "form-control";

                    BindUserDropdown(ref theDropDown, "");
                    DIVCustomItem.Controls.Add(new LiteralControl(@"<div class=""col-md-4"" style=""white-space:nowrap; text-align:left"">"));
                    DIVCustomItem.Controls.Add(theDropDown);
                    DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
                    DIVCustomItem.Controls.Add(new LiteralControl(@"<div class=""col-md-2"" style=""white-space:nowrap; text-align:right""></div>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }

                #endregion "Signature Section"

                DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                Button btnDynSave = new Button();
                btnDynSave.ID = "btnSave-" + tabRow["TabId"];
                btnDynSave.Text = "Save";
                btnDynSave.CssClass = "btn btn-primary";
                btnDynSave.Click += new EventHandler(btnDynSave_Click);
                DIVCustomItem.Controls.Add(btnDynSave);
                Button btnDynDQ = new Button();
                btnDynDQ.ID = "btnDQ-" + tabRow["TabId"];
                btnDynDQ.Click += new EventHandler(btnDynDQ_Click);
                btnDynDQ.Text = "Data Quality Check";
                btnDynDQ.CssClass = "btn btn-success";
                DIVCustomItem.Controls.Add(btnDynDQ);
                Button btnDynPrint = new Button();
                btnDynPrint.ID = "btnPrint-" + tabRow["TabId"];
                btnDynPrint.Text = "Print";
                btnDynPrint.Font.Bold = true;
                btnDynPrint.CssClass = "btn btn-default";
                btnDynPrint.Attributes.Add("OnClick", "WindowPrint()");
                DIVCustomItem.Controls.Add(btnDynPrint);
                ///john 28th june 2013
                UserRights(btnDynSave, btnDynDQ, btnDynPrint, featureId);
                ///////////////////////////////////////////////////////////////////
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                tabContainer.Tabs[z].Controls.Add(DIVCustomItem);
                PnlforTab.Controls.Add(tabContainer);
                z = z + 1;
            }
            Session["RequiredField"] = requiredField;
            if (SignatureFlag == 0) { TrSignatureAll.Visible = true; }
            //For Saving/Updating Controls in the form Except MultiSelect Items
            ViewState["NoMulti"] = mainDataset.Tables[3];

        }

        /// <summary>
        /// Nons the arv drug.
        /// </summary>
        /// <returns></returns>
        private DataTable NonARVDrug()
        {
            DataTable dtNonARV = new DataTable();
            dtNonARV.Columns.Add("DrugId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("GenericId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("UnitId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("FrequencyID", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("SingleDose", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("Duration", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyOrdered", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyDispensed", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("ARFinance", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
            return dtNonARV;
        }

        /// <summary>
        /// Olds the regimen list.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="theDV">The dv.</param>
        /// <returns></returns>
        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];

                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;
        }

        /// <summary>
        /// Called when [blur].
        /// </summary>
        private void OnBlur()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirmonblur'>\n";
            script += "SendCodeName('" + txtvisitDate.ClientID + "')\n";
            script += "</script>\n";
            // ClientScript.RegisterStartupScript(this.GetType(),"confirmonblur", script);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirmonblur", script);
        }

        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs" /> instance containing the event data.</param>
        private void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        /// <summary>
        /// PTNs the customformselected data table.
        /// </summary>
        /// <returns></returns>
        private DataTable PtnCustomformselectedDataTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            theDT.Columns.Add("Flag", System.Type.GetType("System.Int32"));
            return theDT;
        }

        /// <summary>
        /// PTNs the customformselected data table drug.
        /// </summary>
        /// <param name="dT">The dt.</param>
        /// <param name="drugTypeId">The drug type identifier.</param>
        /// <returns></returns>
        private DataTable PtnCustomformselectedDataTableDrug(DataTable dT, int drugTypeId)
        {
            DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            theMstDV.RowFilter = "DrugTypeId=" + drugTypeId;
            DataTable theMSTDT = theMstDV.ToTable();

            DataTable theDTDrug = new DataTable();
            theDTDrug.Columns.Add("DrugId", Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugName", Type.GetType("System.String"));
            theDTDrug.Columns.Add("Generic", Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugTypeID", Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugAbbr", Type.GetType("System.String"));
            theDTDrug.Columns.Add("Flag", Type.GetType("System.Int32"));

            foreach (DataRow thedrI in dT.Rows)
            {
                foreach (DataRow thedrII in theMSTDT.Rows)
                {
                    int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
                    if (DrugId == Convert.ToInt32(thedrII["DrugId"]))
                    {
                        DataRow TmpDR = theDTDrug.NewRow();
                        TmpDR[0] = thedrII["DrugId"];
                        TmpDR[1] = thedrII["DrugName"];
                        TmpDR[2] = thedrII["Generic"];
                        TmpDR[3] = thedrII["DrugTypeID"];
                        TmpDR[4] = thedrII["Abbr"];
                        TmpDR[5] = 1;
                        theDTDrug.Rows.Add(TmpDR);
                    }
                }
            }
            //DataTable theDT1 = theMSTARVDT;
            foreach (DataRow thedrI in dT.Rows)
            {
                int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
                DataRow[] theDR1 = theMSTDT.Select("DrugId=" + DrugId);
                theMSTDT.Rows.Remove(theDR1[0]);
            }
            Session["" + DrugType + ""] = theMSTDT;

            return theDTDrug;
        }

        /// <summary>
        /// PTNs the customformselected data table lab.
        /// </summary>
        /// <param name="dT">The dt.</param>
        /// <returns></returns>
        private DataTable PtnCustomformselectedDataTableLab(DataTable dT)
        {
            DataTable DTMstLab = (DataTable)Session["MasterData"];
            DataTable theDTLab = new DataTable();
            theDTLab.Columns.Add("LabTestID", Type.GetType("System.Int32"));
            theDTLab.Columns.Add("LabName", Type.GetType("System.String"));
            theDTLab.Columns.Add("SubTestID", Type.GetType("System.Int32"));
            theDTLab.Columns.Add("SubTestName", Type.GetType("System.String"));
            theDTLab.Columns.Add("LabTypeId", Type.GetType("System.Int32"));
            theDTLab.Columns.Add("Flag", Type.GetType("System.Int32"));

            foreach (DataRow thedrI in dT.Rows)
            {
                foreach (DataRow thedrII in DTMstLab.Rows)
                {
                    if (Convert.ToInt32(thedrI["SubTestID"]) == Convert.ToInt32(thedrII["SubTestID"]))
                    {
                        DataRow TmpDr = theDTLab.NewRow();
                        TmpDr[0] = thedrII["LabTestID"];
                        TmpDr[1] = thedrII["LabName"];
                        TmpDr[2] = thedrII["SubTestID"];
                        TmpDr[3] = thedrII["SubTestName"];
                        TmpDr[4] = thedrII["LabTypeId"];
                        TmpDr[5] = 1;
                        theDTLab.Rows.Add(TmpDr);
                    }
                }
            }
            foreach (DataRow thedrI in dT.Rows)
            {
                DataRow[] theDR1 = DTMstLab.Select("SubTestId=" + thedrI["SubTestID"]);
                DTMstLab.Rows.Remove(theDR1[0]);
            }
            Session["MasterData"] = DTMstLab;
            return theDTLab;
        }

        /// <summary>
        /// Reads the arv medication table.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private DataTable ReadARVMedicationTable(Control theContainer, int tabId)
        {
            DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            theMstDV.RowFilter = "DrugTypeId in (37,36)";
            DataTable theMSTDT = theMstDV.ToTable();

            DataTable dtARV = new DataTable();
            dtARV.Columns.Add("DrugId", Type.GetType("System.Int32"));
            dtARV.Columns.Add("GenericId", Type.GetType("System.Int32"));
            dtARV.Columns.Add("Dose", Type.GetType("System.String"));
            dtARV.Columns.Add("FrequencyId", Type.GetType("System.String"));
            dtARV.Columns.Add("Duration", Type.GetType("System.Decimal"));
            dtARV.Columns.Add("QtyPrescribed", Type.GetType("System.Decimal"));
            dtARV.Columns.Add("QtyDispensed", Type.GetType("System.Decimal"));
            dtARV.Columns.Add("ARFinance", Type.GetType("System.Int32"));
            dtARV.Columns.Add("DrugType", Type.GetType("System.Int32"));
            dtARV.Columns.Add("DrugAbbr", Type.GetType("System.String"));
            int drugId = 0;
            int drugIdforAbbr = 0;
            int genericId = 0;
            int dose = 0;
            int frequency = 0;
            decimal duration = 0;
            decimal qtyPrescribed = 0;
            decimal qtyDispensed = 0;
            int ARFinanced = 2;
            //string Abbr = "";
            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == tabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(Label))
                                            {
                                                if (x.ID.StartsWith("ARVdrgNm"))
                                                {
                                                    drugId = Convert.ToInt32(x.ID.Substring(8));
                                                    genericId = 0;
                                                }
                                                else if (x.ID.StartsWith("ARVGenericNm"))
                                                {
                                                    genericId = Convert.ToInt32(x.ID.Substring(12));
                                                    drugId = 0;
                                                }
                                            }
                                            if (x.GetType() == typeof(DropDownList))
                                            {
                                                if (x.ID.StartsWith("ARVdrgStrength"))
                                                {
                                                    dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericStrength"))
                                                {
                                                    dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }

                                                if (x.ID.StartsWith("ARVdrgFrequency"))
                                                {
                                                    frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericFrequency"))
                                                {
                                                    frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                            }

                                            if (x.GetType() == typeof(TextBox))
                                            {
                                                if (x.ID.Contains("ARVdrgDuration"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericDuration"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }

                                                if (x.ID.StartsWith("ARVdrgQtyPrescribed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        qtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericQtyPrescribed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        qtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }

                                                if (x.ID.StartsWith("ARVdrgQtyDispensed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        qtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericQtyDispensed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        qtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                if (x.ID.StartsWith("ARVDrugFinChk"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericFinChk"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }
                                        }
                                        if ((drugId != 0 || genericId != 0) && ARFinanced != 2)
                                        {
                                            if (dose != 0 || frequency != 0 || duration != 0 || qtyPrescribed != 0 || qtyDispensed != 0)
                                            {
                                                drugIdforAbbr = drugId == 0 ? genericId : drugId;
                                                theMSTDT.Select("DrugId=" + drugIdforAbbr + "");
                                                DataRow[] filterRows = theMSTDT.Select("DrugId=" + drugIdforAbbr + "");
                                                theRow = dtARV.NewRow();
                                                theRow["DrugId"] = drugId;
                                                theRow["GenericId"] = genericId;
                                                theRow["Dose"] = dose;
                                                theRow["FrequencyId"] = frequency;
                                                theRow["Duration"] = duration;
                                                theRow["QtyPrescribed"] = qtyPrescribed;
                                                theRow["QtyDispensed"] = qtyDispensed;
                                                theRow["ARFinance"] = ARFinanced;
                                                theRow["DrugAbbr"] = filterRows;
                                                dtARV.Rows.Add(theRow);
                                                drugId = 0;
                                                genericId = 0;
                                                dose = 0;
                                                frequency = 0;
                                                duration = 0;
                                                qtyPrescribed = 0;
                                                qtyDispensed = 0;
                                                ARFinanced = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dtARV;
        }

        /// <summary>
        /// Reads the lab table.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private DataTable ReadLabTable(Control theContainer, int tabId)
        {
            // This procedure reads the the additional labs on the panel labs  into a datatable
            DataTable dtLabs = new DataTable();
            dtLabs.Columns.Add("LabTestId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabParameterId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabResult", System.Type.GetType("System.Decimal"));
            dtLabs.Columns.Add("LabResult1", System.Type.GetType("System.String"));
            dtLabs.Columns.Add("LabResultId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("Financed", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("UnitId", System.Type.GetType("System.Int32"));

            int theSubTestId = 0;
            int theLabTestId = 0;
            string theResultId = string.Empty;
            int theFinanced = 2;

            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == tabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(Label))
                                            {
                                                if (x.ID.StartsWith("theNameLab"))
                                                {
                                                    theSubTestId = Convert.ToInt32(x.ID.Substring(10));
                                                }
                                            }
                                            if (x.GetType() == typeof(TextBox))
                                            {
                                                if (x.ID.StartsWith("LabResult"))
                                                {
                                                    theResultId = ((TextBox)x).Text; //Convert.ToInt32(((TextBox)x).Text);
                                                }
                                            }
                                            else if (x.GetType() == typeof(DropDownList))
                                            {
                                                if (x.ID.StartsWith("ddlLabResult"))
                                                {
                                                    theResultId = ((DropDownList)x).SelectedValue;
                                                }
                                            }
                                            if (x.GetType() == typeof(CheckBox))
                                            {
                                                if (x.ID.StartsWith("FinChkLab"))
                                                {
                                                    theFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }

                                            if (x.GetType() == typeof(Label))
                                            {
                                                if (x.ID.Contains("=") == true)
                                                {
                                                    string[] LabTestId = ((Label)x).ID.Split('=');
                                                    theLabTestId = Convert.ToInt32(LabTestId[1]);
                                                }
                                            }
                                            if (theLabTestId != 0 && theSubTestId != 0 && theResultId != "")
                                            {
                                                theRow = dtLabs.NewRow();
                                                theRow["LabTestId"] = theLabTestId;
                                                theRow["LabParameterId"] = theSubTestId;
                                                //theRow["LabResult"] = 0;
                                                theRow["LabResult"] = 99998888;
                                                theRow["LabResult1"] = theResultId;
                                                theRow["LabResultId"] = 0;
                                                theRow["Financed"] = 0;
                                                dtLabs.Rows.Add(theRow);
                                                theSubTestId = 0;
                                                theLabTestId = 0;
                                                theResultId = string.Empty;
                                                theFinanced = 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /*  foreach (Control y in theContainer.Controls)
              {
                  if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                  {
                      foreach (Control x in y.Controls)
                      {
                      }
                  }
              }*/
            return dtLabs;
        }

        /// <summary>
        /// Reads the non arv medication table.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <param name="TabId">The tab identifier.</param>
        /// <returns></returns>
        private DataTable ReadNonARVMedicationTable(Control theContainer, int TabId)
        {
            DataTable dtNonARV = new DataTable();
            dtNonARV.Columns.Add("DrugId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("GenericId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("UnitId", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("FrequencyID", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("SingleDose", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("Duration", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyOrdered", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyDispensed", Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("ARFinance", Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("DrugType", Type.GetType("System.Int32"));
            int drugId = 0;
            decimal singleDose = 0;
            int genericId = 0;
            int unitId = 0;
            decimal FrequencyId = 0;
            decimal duration = 0;
            decimal qtyOrdered = 0;
            decimal qtyDispensed = 0;
            int ARFinanced = 2;
            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == TabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(Label))
                                            {
                                                if (x.ID.StartsWith("DrugNm"))
                                                {
                                                    drugId = Convert.ToInt32(x.ID.Substring(6));
                                                    genericId = 0;
                                                }
                                                else if (x.ID.StartsWith("GenericNm"))
                                                {
                                                    genericId = Convert.ToInt32(x.ID.Substring(9));
                                                    drugId = 0;
                                                }
                                            }
                                            if (x.GetType() == typeof(TextBox))
                                            {
                                                if (x.ID.StartsWith("theDoseDrug"))
                                                {
                                                    singleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("theDoseGeneric"))
                                                {
                                                    singleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("DrugDuration"))
                                                {
                                                    duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("GenericDuration"))
                                                {
                                                    duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugQtyPrescribed"))
                                                {
                                                    qtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("genericQtyPrescribed"))
                                                {
                                                    qtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugQtyDispensed"))
                                                {
                                                    qtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("genericQtyDispensed"))
                                                {
                                                    qtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                            }
                                            if (x.GetType() == typeof(DropDownList))
                                            {
                                                if (x.ID.StartsWith("theUnitDrug"))
                                                {
                                                    unitId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("theUnitGeneric"))
                                                {
                                                    unitId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugFrequency"))
                                                {
                                                    FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("GenericFrequency"))
                                                {
                                                    FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                            }
                                            if (x.GetType() == typeof(CheckBox))
                                            {
                                                if (x.ID.StartsWith("FinChkDrug"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                                else if (x.ID.StartsWith("FinChkGeneric"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }
                                        }

                                        if ((drugId != 0 || genericId != 0) && ARFinanced != 2)
                                        {
                                            if (unitId != 0 || FrequencyId != 0 || singleDose != 0 || duration != 0 || qtyOrdered != 0 || qtyDispensed != 0)
                                            {
                                                theRow = dtNonARV.NewRow();
                                                theRow["DrugId"] = drugId;
                                                theRow["GenericId"] = genericId;
                                                theRow["UnitId"] = unitId;
                                                theRow["FrequencyID"] = FrequencyId;
                                                theRow["SingleDose"] = singleDose;
                                                theRow["Duration"] = duration;
                                                theRow["QtyOrdered"] = qtyOrdered;
                                                theRow["QtyDispensed"] = qtyDispensed;
                                                theRow["ARFinance"] = ARFinanced;
                                                theRow["DrugType"] = System.DBNull.Value;
                                                dtNonARV.Rows.Add(theRow);
                                                drugId = 0;
                                                genericId = 0;
                                                unitId = 0;
                                                FrequencyId = 0;
                                                singleDose = 0;
                                                duration = 0;
                                                qtyOrdered = 0;
                                                qtyDispensed = 0;
                                                ARFinanced = 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //foreach (Control y in theContainer.Controls)
            //{
            //    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            //    {
            //        foreach (Control x in y.Controls)
            //        {
            //        }
            //     }
            //}
            return dtNonARV;
        }

        /// <summary>
        /// Regimens the session setting.
        /// </summary>
        /// <param name="regimenTypeId">Type of the regimen.</param>
        /// <param name="controlId">The control identifier.</param>
        /// <param name="regimen">The regimen.</param>
        private void RegimenSessionSetting(int regimenTypeId, string controlId, string regimen)
        {
            IQCareUtils theUtils = new IQCareUtils();
            if (Session["Reg" + controlId.ToString() + regimenTypeId + ""] == null)
            {
                DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                theDV.RowFilter = "DrugTypeId=" + regimenTypeId + " and Generic<>0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                Session["Reg" + controlId.ToString() + regimenTypeId + ""] = theDT;
            }
            if (Session["SelectedReg" + controlId.ToString() + regimenTypeId + ""] == null)
            {
                //DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                //theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                //DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                //Session["Reg" + controlId.ToString() + RegimenType + ""] = theDT;
                //Table for Selected Drugs
                DataTable theSelectedDT = new DataTable();
                theSelectedDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugName", Type.GetType("System.String"));
                theSelectedDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugTypeID", Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugAbbr", Type.GetType("System.String"));
                Session["SelectedReg" + controlId.ToString() + regimenTypeId + ""] = theSelectedDT;
            }
            DataTable theTmpDT = ((DataTable)Session["Reg" + controlId.ToString() + regimenTypeId + ""]).Copy();
            string[] ArrRegimen = regimen.Split('/');
            int colvalue;
            if (regimenTypeId == 37)
            {
                colvalue = 4;
            }
            else
                colvalue = 1;

            DataTable theDTSelected = (DataTable)Session["SelectedReg" + controlId.ToString() + regimenTypeId + ""];
            for (int i = 0; i < ArrRegimen.Length; i++)
            {
                foreach (DataRow theDR in theTmpDT.Rows)
                {
                    if (Convert.ToString(theDR[colvalue]) == ArrRegimen[i])
                    {
                        DataRow theTmpDR = theDTSelected.NewRow();
                        theTmpDR[0] = theDR["DrugId"];
                        theTmpDR[1] = theDR["DrugName"];
                        theTmpDR[2] = theDR["Generic"];
                        theTmpDR[3] = theDR["DrugTypeID"];
                        theTmpDR[4] = theDR["Abbr"];
                        theDTSelected.Rows.Add(theTmpDR);
                    }
                }
            }
            Session["SelectedReg" + controlId.ToString() + regimenTypeId + ""] = theDTSelected;

            //For setting Master Regimen Session
            foreach (DataRow theDR in ((DataTable)Session["Reg" + controlId.ToString() + regimenTypeId + ""]).Rows)
            {
                foreach (DataRow theDRI in theDTSelected.Rows)
                {
                    if (Convert.ToString(theDR[1]) == Convert.ToString(theDRI[1]))
                    {
                        DataRow[] theDR1 = theTmpDT.Select("DrugId=" + Convert.ToInt32(theDR[0]));
                        theTmpDT.Rows.Remove(theDR1[0]);
                    }
                }
            }
            Session["Reg" + controlId.ToString() + regimenTypeId + ""] = theTmpDT;
        }

        /// <summary>
        /// Returns the regimen.
        /// </summary>
        /// <param name="regtypeId">The regtype identifier.</param>
        /// <returns></returns>
        private string ReturnRegimen(int regtypeId)
        {
            string theStr = "";
            if (Session["SelectedReg" + regtypeId + ""] != null)
            {
                DataTable theDT = (DataTable)Session["SelectedReg" + regtypeId + ""];
                theStr = FillRegimen(theDT);
            }
            return theStr;
        }

        /// <summary>
        /// Saves the cancel.
        /// </summary>
        private void SaveCancel()
        {
            TabContainer container = tabContainer;
            hdnPrevTabIndex.Value = hdnCurrenTabIndex.Value;
            container.ActiveTabIndex = Convert.ToInt32(hdnCurrenTabIndex.Value);
            //--- For Cancel event, on saving the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "alert('Data on " + hdnPrevTabName.Value + " saved successfully.');\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            hdnPrevTabName.Value = hdnCurrenTabName.Value;
        }

        /// <summary>
        /// Saves the custom form data.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="dataSet">The ds.</param>
        /// <param name="DQSaveChk">The dq save CHK.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private StringBuilder SaveCustomFormData(int patientId, DataSet dataSet, int DQSaveChk, int tabId)
        {
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            TabContainer container = tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataView theViewDT = new DataView(theDT);
            theViewDT.RowFilter = "TabId=" + tabId + "";
            theDT = theViewDT.ToTable();
            StringBuilder SbInsert = new StringBuilder();
            string str = "";
            int ICD10Count = 0;
            StringBuilder SbUpdateColMstPatient = new StringBuilder();
            StringBuilder SbUpdateValMstPatient = new StringBuilder();

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                DataView theViewMulti = new DataView(((DataTable)ViewState["LnkTable"]));
                theViewMulti.RowFilter = "TabId=" + tabId + "";
                DataTable theDTMulti = theViewMulti.ToTable();
                DataView theViewother = new DataView(((DataTable)ViewState["NoMulti"]));
                theViewother.RowFilter = "TabId=" + tabId + "";
                DataTable theDTother = theViewother.ToTable();

                #region "Conditional Field Inclusion"

                DataView theConditionalView = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                theConditionalView.RowFilter = "TabId=" + tabId + "";
                DataTable theConditionalDT = theConditionalView.ToTable();

                foreach (DataRow theConDR in theConditionalDT.Rows)
                {
                    DataRow theDTMultiDR = theDTMulti.NewRow();
                    theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
                    theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
                    theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
                    theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
                    theDTMultiDR["FieldId"] = theConDR["FieldId"];
                    theDTMultiDR["FieldName"] = theConDR["FieldName"];
                    theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
                    theDTMultiDR["Predefined"] = theConDR["Predefined"];
                    theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
                    theDTMultiDR["ControlId"] = theConDR["ControlId"];
                    theDTMultiDR["BindSource"] = theConDR["BindSource"];
                    theDTMultiDR["CodeId"] = theConDR["CodeId"];
                    theDTMultiDR["Seq"] = theConDR["Seq"];
                    theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
                    theDTMultiDR["TabId"] = theConDR["TabId"];
                    theDTMultiDR["TabName"] = theConDR["TabName"];
                    theDTMulti.Rows.Add(theDTMultiDR);

                    if (theConDR["ControlId"].ToString() != "9")
                    {
                        DataRow theDTOtherDR = theDTother.NewRow();
                        theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                        theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                        theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                        theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                        theDTOtherDR["FieldId"] = theConDR["FieldId"];
                        theDTOtherDR["FieldName"] = theConDR["FieldName"];
                        theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                        theDTOtherDR["Predefined"] = theConDR["Predefined"];
                        theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                        theDTOtherDR["ControlId"] = theConDR["ControlId"];
                        theDTOtherDR["BindSource"] = theConDR["BindSource"];
                        theDTOtherDR["CodeId"] = theConDR["CodeId"];
                        theDTOtherDR["Seq"] = theConDR["Seq"];
                        theDTOtherDR["TabId"] = theConDR["TabId"];
                        theDTOtherDR["TabName"] = theConDR["TabName"];
                        theDTother.Rows.Add(theDTOtherDR);
                    }
                }

                #endregion "Conditional Field Inclusion"

                DataTable LnkDTUnique = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
                //To Add Signature Dropdown for Each tab
                DataTable theDTORDVisit = ((DataSet)Session["AllData"]).Tables[23];
                String Signature = "0";
                if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 1)
                {
                    LnkDTUnique.Rows.Add("LNK_FORMTABORDVISIT", "" + lblFeatureName.Text + "");
                }
                if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 0)
                {
                    Signature = ddSignature.SelectedValue;
                }

                string GetValue = "";
                GetValue = "Select VisitTypeID from mst_visittype where (DeleteFlag = 0 or DeleteFlag is null) and VisitTypeId>12 and VisitName='" + lblFeatureName.Text + "'";
                DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(GetValue);

                SbInsert.Append("Insert into [ord_visit](ptn_pk, LocationID, VisitDate, VisitType, DataQuality, UserID, Signature, CreateDate,ModuleId)");

                // todo
                if (IsSingleVisit == true)
                {
                    string theRegDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                    SbInsert.Append("Values(" + patientId + "," + Session["AppLocationId"] + ",'" + txtvisitDate.Text + "'," + TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + Signature + ", GetDate(), " + Convert.ToString(Session["TechnicalAreaId"]) + ")");
                }
                else
                {
                    SbInsert.Append("Values(" + patientId + "," + Session["AppLocationId"] + ", '" + txtvisitDate.Text + "', " + TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + Signature + ", GetDate() ," + Convert.ToString(Session["TechnicalAreaId"]) + ")");
                }
                SbInsert.Append("declare @thisVisitId int; Select @thisVisitId = SCOPE_IDENTITY();");
                string theRegVisitDate;

                // todo
                if (IsSingleVisit == true)
                {
                    theRegVisitDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                }
                else
                {
                    theRegVisitDate = txtvisitDate.Text;
                }
                SbInsert.Append("INSERT INTO Dtl_PatientBillTransaction(BillId,Ptn_Pk,VisitId,LocationId,TransactionDate,LabId,PharmacyId,");
                SbInsert.Append("ItemId,BatchId,DispensingUnit,Quantity,SellingPrice,CostPrice,Margin,ConsultancyFee,AdminFee,BillAmount,DoctorId,UserId,CreateDate)");
                SbInsert.Append("VALUES(0," + patientId + ",@thisVisitId ," + Session["AppLocationId"] + ",'" + theRegVisitDate + "',0,0,0,0,0,1,0,0,0,dbo.fn_GetConsultationPerVisit_Futures('" + theRegVisitDate + "'),");
                SbInsert.Append("dbo.fn_GetOverHeadPerVisit_Futures('" + theRegVisitDate + "'),dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + theRegVisitDate + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + theRegVisitDate + "'),");
                SbInsert.Append("" + Signature + "," + Session["AppUserId"] + ", getdate())");
                //Generating Query for MultiSelect
                foreach (DataRow DRMultiSelect in theDTMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9" || DRMultiSelect["ControlID"].ToString() == "15")
                    {
                        StringBuilder InsertMultiselect = InsertMultiSelectList(patientId, DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                            DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]), tabId);
                        if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                            SbInsert.Append(InsertMultiselect);
                    }
                    else if (DRMultiSelect["ControlID"].ToString() == "16" && ICD10Count == 0)
                    {
                        int Setflag = 0;
                        StringBuilder SbInsertICD10 = new StringBuilder();
                        StringBuilder SbValuesICD10 = new StringBuilder();
                        foreach (DataRow theICD10Row in theDT.Rows)
                        {
                            if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("%") && theICD10Row[2].ToString().Contains("1") && Setflag == 0)
                            {
                                string[] strICD10 = theICD10Row[0].ToString().Split('%');
                                string FieldId = strICD10[0].StartsWith("8888") ? strICD10[0].Replace("8888", "") : strICD10[0].Replace("9999", "");
                                SbInsertICD10.Append("Insert into [dtl_ICD10Field](ptn_pk, Visit_Pk, LocationID, FieldId, BlockId,SubBlockId,ICDCodeId, Predefined, UserID,CreateDate,TabId,");
                                SbValuesICD10.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + FieldId + ", " + strICD10[1] + ", " + strICD10[2] + ", " + strICD10[3] + "," + strICD10[4] + "," + Session["AppUserId"] + ", GetDate()," + tabId + ",");
                                Setflag = 1;
                            }
                            else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("OnSetDate") && Setflag == 1)
                            {
                                SbInsertICD10.Append("DateOnSet,");
                                SbValuesICD10.Append("'" + theICD10Row[2] + "',");
                            }
                            else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("Comment") && Setflag == 1)
                            {
                                SbInsertICD10.Append("Comments)");
                                SbValuesICD10.Append("'" + theICD10Row[2] + "')");
                                Setflag = 0;
                                SbInsertICD10.Append(SbValuesICD10);
                                SbValuesICD10 = new StringBuilder();
                            }
                        }
                        SbInsert.Append(SbInsertICD10);
                        ICD10Count = 1;
                    }
                }
                //
                foreach (DataRow DR in LnkDTUnique.Rows)
                {
                    string quotes = "''''";
                    StringBuilder SbValues = new StringBuilder();
                    if (DR[0].ToString() == "DTL_CUSTOMFIELD")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_');
                        SbInsert.Append("Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                        SbValues.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                    }
                    else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                    {
                    }
                    else if (DR[0].ToString().ToUpper() == "BMI")
                    {
                    }
                    else if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper())
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + patientId + "," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper())
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visitid,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "mst_patient".ToUpper())
                        {
                            str = "mst_patient";
                            SbUpdateColMstPatient.Append("Update [" + DR[0] + "] Set ");
                        }
                        else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT")
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Visit_pk, DataQuality, TabId, UserID,CreateDate,Signature,");
                            SbValues.Append("Values(@thisVisitId , " + DQSaveChk + ", " + tabId + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "dtl_ICD10Field".ToUpper())
                        {
                        }
                        else
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                    }
                    //Generating Query to Insert values other than MultiSelect
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DR["PDFTableName"].ToString().ToUpper() == DRlnk["TableName"].ToString().ToUpper() && Convert.ToInt32(DRlnk["TabId"]) == tabId)
                        {
                            if (Convert.ToString(DR["PDFTableName"]) == "mst_patient".ToUpper())
                            {
                                SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                SbUpdateColMstPatient.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString().ToUpper()) == "DTL_CUSTOMFORM")
                            {
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "dtl_ICD10Field")
                            {
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "LNK_FORMTABORDVISIT")
                            {
                                SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                Signature = DRlnk["Value"].ToString();
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "dtl_Adherence_Missed_Reason".ToUpper())
                            {
                                if (DRlnk["Value"].ToString() == "")
                                {
                                }
                                else
                                {
                                    SbInsert.Append("[" + DRlnk["Column"] + "],");
                                    SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                }
                            }
                            else
                            {
                                if (DRlnk["Value"].ToString() != "")
                                {
                                    SbInsert.Append("[" + DRlnk["Column"] + "],");
                                    if (DRlnk["Value"].ToString() == "")
                                    {
                                        SbValues.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                    }
                                    else
                                    {
                                        SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                    }
                                }
                            }
                        }
                    }
                    SbInsert.Remove(SbInsert.Length - 1, 1);
                    if (Convert.ToString(DR[0]) != "mst_patient".ToUpper())
                    {
                        SbInsert.Append(" )");
                    }
                    if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) != "mst_patient".ToUpper())
                        {
                            if (SbValues.Length > 0)
                            {
                                SbValues.Remove(SbValues.Length - 1, 1);
                                SbValues.Append(" )");
                            }
                        }
                        else
                        {
                            SbValues.Append(" )");
                            SbUpdateColMstPatient.Remove(SbUpdateColMstPatient.Length - 1, 1);
                        }
                    }
                    SbInsert.Append(SbValues);
                    TempDS.Dispose();
                }

                if (str == "mst_patient".ToUpper())
                {
                    SbUpdateValMstPatient.Append(" where Ptn_pk=" + patientId + " and LocationID=" + Session["AppLocationId"] + " ");
                }

                SbInsert.Append(SbUpdateColMstPatient);
                SbInsert.Append(SbUpdateValMstPatient);
                SbInsert.Append("Select LocationID, @thisVisitId [VisitID] from ord_visit where Visit_ID=@thisVisitId ");
                if (dataSet.Tables[1].Rows.Count > 0 || dataSet.Tables[2].Rows.Count > 0)
                {
                    string orderstatus = string.Empty;
                    if (Session["SCMModule"] != null)
                    {
                        orderstatus = "1";
                    }
                    SbInsert.Append("Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate,orderstatus)");
                    SbInsert.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + Session["AppUserId"] + "," + Signature + ", getdate(),'" + orderstatus + "')");
                    SbInsert.Append("Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=@thisVisitId ");
                }
                else { SbInsert.Append("Select '00000'[PharmacyID]"); };

                #region Insert GridView

                //DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true,"FeatureID", "SectionID", "SectionName", "IsGridView","FeatureName").Copy();
                DataTable lnkSection = theDTMulti.DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "SectionInfo", "IsGridView", "FeatureName").Copy();
                DataView theDVSectionGridView = new DataView(lnkSection);
                theDVSectionGridView.RowFilter = "IsGridView= 1";
                if (theDVSectionGridView.Count > 0)
                {
                    StringBuilder sbInsertGridView = new StringBuilder();
                    foreach (DataRow DRGridView in theDVSectionGridView.ToTable().Rows)
                    {
                        sbInsertGridView.Append(InsertGridView(patientId, Convert.ToInt32(DRGridView["FeatureID"].ToString()), Convert.ToInt32(DRGridView["SectionID"]), DRGridView["SectionName"].ToString(), 0, DRGridView["FeatureName"].ToString()));
                        sbInsertGridView.Append(";");
                    }
                    SbInsert.Append(sbInsertGridView);
                }
                # endregion

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    SbInsert.Append("Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                    SbInsert.Append("Values(" + patientId + ",@thisVisitId ," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + Signature + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                    SbInsert.Append("Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=@thisVisitId ");
                }
                else { SbInsert.Append("Select '00000'[LabID]"); }
            }
            return SbInsert;
        }

        /// <summary>
        /// Sections the heading.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="sectionInfo">The section information.</param>
        private void SectionHeading(string sectionName, string sectionInfo = "")
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + sectionName + "</h2>"));
            if ("" != sectionInfo)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<div class='forms' align='left' style=\"font-style:italic;margin-left:8px;\">" + sectionInfo + "</div>"));
            }
        }

        /// <summary>
        /// Sections the heading.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="i">The i.</param>
        /// <param name="sectionInfo">The section information.</param>
        private void SectionHeading(string sectionName, int i, string sectionInfo = "")
        {
            //DIVCustomItem.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + H2 + "</h2>"));
            this.SectionHeading(sectionName, sectionInfo);
            tabContainer.Tabs[i].Controls.Add(DIVCustomItem);
        }

        /// <summary>
        /// Sets the businessrule.
        /// </summary>
        /// <param name="fieldId">The field identifier.</param>
        /// <param name="fieldLabel">The field label.</param>
        /// <returns></returns>
        private bool IsRequiredField(string fieldId, string fieldLabel)
        {
            DataTable theDT = (DataTable)ViewState["BusRule"];
            foreach (DataRow DR in theDT.Rows)
            {
                if (Convert.ToString(DR["FieldID"]) == fieldId && Convert.ToString(DR["FieldName"]) == fieldLabel && Convert.ToString(DR["BusRuleId"]) == "1")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the control i ds.
        /// </summary>
        /// <param name="theControl">The control.</param>
        /// <returns></returns>
        private DataTable SetControlIDs(TabContainer container)
        {
            DataTable TempDT = new DataTable();
            TempDT.Columns.Add("Column", Type.GetType("System.String"));
            TempDT.Columns.Add("FieldId", Type.GetType("System.String"));
            TempDT.Columns.Add("Value", Type.GetType("System.String"));
            TempDT.Columns.Add("ValueText", Type.GetType("System.String"));
            TempDT.Columns.Add("TableName", Type.GetType("System.String"));
            TempDT.Columns.Add("TabId", Type.GetType("System.String"));
            DataRow DRTemp;
            DRTemp = TempDT.NewRow();
            string Time24 = "", Time12 = "", TimeAMPM = "";
            foreach (TabPanel tabPanel in container.Tabs)
            {
                foreach (Control ctrl in tabPanel.Controls)
                {
                    //if (ctrl is Control)
                    //{
                    Control c = ctrl;
                    foreach (object x in c.Controls)
                    {
                        if (x.GetType() == typeof(TextBox))
                        {
                            string[] str = ((TextBox)x).ID.Split('-');
                            if (str[2] != "BMI")
                            {
                                DRTemp = TempDT.NewRow();

                                DRTemp["Column"] = str[1];

                                if (((TextBox)x).Enabled == true)
                                {
                                    if (str[0].ToString() == "TXTDT")
                                    {
                                        if (((TextBox)x).Text.Length > 8)
                                        {
                                            DRTemp["Value"] = ((TextBox)x).Text;
                                        }
                                        else
                                        {
                                            if (((TextBox)x).Text != "")
                                            {
                                                DRTemp["Value"] = "01-" + ((TextBox)x).Text;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            DRTemp["Value"] = ((TextBox)x).Text;
                                        }
                                    }
                                }
                                else
                                {
                                    DRTemp["Value"] = "";
                                }

                                DRTemp["TableName"] = str[2];
                                DRTemp["FieldID"] = str[3];
                                DRTemp["TabId"] = str[4];
                                TempDT.Rows.Add(DRTemp);
                            }
                        }
                        if (x.GetType() == typeof(HtmlInputRadioButton))
                        {
                            DRTemp = TempDT.NewRow();
                            string[] str = ((HtmlInputRadioButton)x).ID.Split('-');
                            if (((HtmlInputRadioButton)x).ID == "RADIO1-" + str[1] + "-" + str[2] + "-" + str[3] + "-" + str[4])
                            {
                                if (((HtmlInputRadioButton)x).Checked == true)
                                {
                                    DRTemp["Column"] = str[1];
                                    if (((HtmlInputRadioButton)x).Visible == true)
                                        DRTemp["Value"] = "1";
                                    else
                                        DRTemp["Value"] = "";
                                }
                            }
                            else if (((HtmlInputRadioButton)x).ID == "RADIO2-" + str[1] + "-" + str[2] + "-" + str[3] + "-" + str[4])
                            {
                                if (((HtmlInputRadioButton)x).Checked == true)
                                {
                                    DRTemp["Column"] = str[1];
                                    if (((HtmlInputRadioButton)x).Visible == true)
                                        DRTemp["Value"] = "0";
                                    else
                                        DRTemp["Value"] = "";
                                }
                            }

                            DRTemp["TableName"] = str[2];
                            DRTemp["FieldID"] = str[3];
                            DRTemp["TabId"] = str[4];
                            TempDT.Rows.Add(DRTemp);
                        }
                        if (x.GetType() == typeof(DropDownList))
                        {
                            DRTemp = TempDT.NewRow();
                            string[] str = ((DropDownList)x).ID.Split('-');
                            if (str[3].Contains("12Hr"))
                            {
                                Time12 = ((DropDownList)x).SelectedValue;
                            }
                            else if (str[3].Contains("24Hr"))
                            {
                                Time24 = ((DropDownList)x).SelectedValue;
                            }
                            else if (str[3].Contains("Min") && Time12 != "")
                            {
                                TimeAMPM = Time12 + ":" + ((DropDownList)x).SelectedValue;
                                Time12 = "";
                            }
                            else if (str[3].Contains("Min") && Time24 != "")
                            {
                                Time24 = Time24 + ":" + ((DropDownList)x).SelectedValue;
                                DRTemp["Column"] = str[1];
                                DRTemp["Value"] = Time24;
                                DRTemp["TableName"] = str[2];
                                DRTemp["FieldID"] = str[3];
                                DRTemp["TabId"] = str[4];
                                TempDT.Rows.Add(DRTemp);
                                Time24 = "";
                            }
                            else if (str[3].Contains("AMPM"))
                            {
                                TimeAMPM = TimeAMPM + " " + ((DropDownList)x).SelectedValue;
                                DRTemp["Column"] = str[1];
                                DRTemp["Value"] = TimeAMPM;
                                DRTemp["TableName"] = str[2];
                                DRTemp["FieldID"] = str[3];
                                DRTemp["TabId"] = str[4];
                                TempDT.Rows.Add(DRTemp);
                                TimeAMPM = "";
                            }
                            else
                            {
                                if (str[0] != "SELECTLISTAuto")
                                {
                                    DRTemp["Column"] = str[1];
                                    if (((DropDownList)x).Enabled == true)
                                        DRTemp["Value"] = ((DropDownList)x).SelectedValue;
                                    else
                                        DRTemp["Value"] = "";
                                    DRTemp["TableName"] = str[2];
                                    DRTemp["FieldID"] = str[3];
                                    DRTemp["TabId"] = str[4];
                                    TempDT.Rows.Add(DRTemp);
                                }
                            }
                        }
                        if (x.GetType() == typeof(CheckBox))
                        {
                            DRTemp = TempDT.NewRow();
                            string[] str = ((CheckBox)x).ID.Split('-');
                            DRTemp["Column"] = str[1];
                            if (((CheckBox)x).Visible == true)
                            {
                                if (((CheckBox)x).Checked == true)
                                {
                                    DRTemp["Value"] = 1;
                                }
                                else
                                {
                                    DRTemp["Value"] = "";
                                }
                            }
                            else
                            {
                                DRTemp["Value"] = "";
                            }
                            DRTemp["TableName"] = str[2];
                            DRTemp["FieldID"] = str[3];
                            DRTemp["TabId"] = str[4];
                            TempDT.Rows.Add(DRTemp);
                        }

                        if (x.GetType() == typeof(HtmlInputCheckBox))
                        {
                            DRTemp = TempDT.NewRow();
                            string[] str = ((HtmlInputCheckBox)x).ID.Split('-');
                            DRTemp["Column"] = str[1];
                            if (((HtmlInputCheckBox)x).Visible == true)
                            {
                                if (((HtmlInputCheckBox)x).Checked == true)
                                {
                                    DRTemp["Value"] = 1;
                                }
                                else
                                {
                                    DRTemp["Value"] = 0;
                                }
                            }
                            else
                            {
                                DRTemp["Value"] = "";
                            }
                            DRTemp["TableName"] = str[2];
                            DRTemp["FieldID"] = str[3];
                            DRTemp["TabId"] = str[4];
                            TempDT.Rows.Add(DRTemp);
                        }
                        if (x.GetType() == typeof(IQLookupTextBox))
                        {
                            DRTemp = TempDT.NewRow();
                            IQLookupTextBox iq = (IQLookupTextBox)x;
                            string[] str = iq.ID.Split('-');
                            string strValueName = iq.ValueText;
                            string strValueId = iq.SelectedValue;
                            DRTemp["Column"] = str[1];
                            DRTemp["Value"] = strValueId;
                            DRTemp["ValueText"] = strValueName;
                            //if (((HtmlInputCheckBox)x).Visible == true)
                            //{
                            //    if (((HtmlInputCheckBox)x).Checked == true)
                            //    {
                            //        DRTemp["Value"] = 1;
                            //    }
                            //    else
                            //    {
                            //        DRTemp["Value"] = 0;
                            //    }
                            //}
                            //else
                            //{
                            //    DRTemp["Value"] = "";
                            //}
                            DRTemp["TableName"] = str[2];
                            DRTemp["FieldID"] = str[3];
                            DRTemp["TabId"] = str[4];
                            TempDT.Rows.Add(DRTemp);
                        }
                    }
                    //}
                }
            }

            return TempDT;
        }

        /// <summary>
        /// Sets the formating.
        /// </summary>
        /// <param name="bc">The bc.</param>
        /// <returns></returns>
        private string setFormating(DataColumn bc)
        {
            string dataType = null;
            switch (bc.DataType.ToString())
            {
                case "System.Int32":
                    dataType = "";
                    break;

                case "System.Decimal":
                    dataType = "{0:#.##}";
                    break;

                case "System.DateTime":
                    dataType = "{0:dd-MMM-yyyy}";
                    break;

                case "System.String":
                    dataType = "";
                    break;

                default:
                    dataType = "";
                    break;
            }
            return dataType;
        }

        /// <summary>
        /// Sets the grid view section control.
        /// </summary>
        /// <param name="theControl">The control.</param>
        /// <param name="dt">The dt.</param>
        /// <param name="index">The index.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="dtcol">The dtcol.</param>
        private void setGridViewSectionControl(Control theControl, DataTable dt, int index, string columnName, string dtcol)
        {
            // string ret = string.Empty;
            foreach (object obj in theControl.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        {
                                            if (((TextBox)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Columns[dtcol].DataType.ToString() == "System.DateTime")
                                                {
                                                    if (dt.Rows[index][dtcol] != DBNull.Value)
                                                        ((TextBox)x).Text = Convert.ToDateTime(dt.Rows[index][dtcol]).ToString("dd-MMM-yyyy");
                                                }
                                                else
                                                {
                                                    ((TextBox)x).Text = dt.Rows[index][dtcol].ToString();
                                                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[index][dtcol])))
                                                    {
                                                        string[] regimen;
                                                        string[] controlid;
                                                        if (((TextBox)x).ID.Contains("TXTReg-"))
                                                        {
                                                            regimen = ((TextBox)x).ID.Split('=');
                                                            controlid = regimen[0].Split('-');
                                                            if ((regimen.Length > 1) && (controlid.Length > 2))
                                                            {
                                                                string sessionname = "SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + "";
                                                                CustomFormAddRegimen(Convert.ToInt32(regimen[1].ToString()), dt.Rows[index][dtcol].ToString(), sessionname, controlid[3].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                {
                                    if (((HtmlInputRadioButton)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO1-"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Rows[index][dtcol].ToString() != "")
                                                {
                                                    if (dt.Rows[index][dtcol].ToString().ToUpper() == "TRUE")
                                                    {
                                                        ((HtmlInputRadioButton)x).Checked = true;
                                                    }
                                                }
                                            }
                                        }

                                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO2-"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Rows[index][dtcol].ToString() != "")
                                                {
                                                    if (dt.Rows[index][dtcol].ToString().ToUpper() == "FALSE")
                                                    {
                                                        ((HtmlInputRadioButton)x).Checked = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                {
                                    if (((DropDownList)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((DropDownList)x).ID.Contains("-" + columnName + "-"))
                                        {
                                            ((DropDownList)x).SelectedIndex = ((DropDownList)x).Items.IndexOf(((DropDownList)x).Items.FindByText(dt.Rows[index][dtcol].ToString()));
                                        }
                                    }
                                }

                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    if (((HtmlInputCheckBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((HtmlInputCheckBox)x).ID.Contains("-" + columnName + "-"))
                                        {
                                            if (dt.Rows[index][dtcol].ToString() != "")
                                            {
                                                ((HtmlInputCheckBox)x).Checked = Convert.ToBoolean(dt.Rows[index][dtcol].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the theBtnAdditionalLab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void theBtnAdditionalLab_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Click event of the theBtnDrugSelection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void theBtnDrugSelection_Click(object sender, EventArgs e)
        {
            string theScript;

            Application.Add("MasterData", Session["MasterDrugTable"]);
            Application.Add("SelectedDrug", (DataTable)Session["AddARV"]);
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('frmDrugSelector.aspx?DrugType=37','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(),"DrgPopup", theScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Click event of the theBtnRegimen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theBtnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Session.Add("MasterData", ViewState["MasterData"]);
            Session.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btncustomReg' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(),"DrgPopup", theScript);
            Page.ClientScript.RegisterStartupScript(GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Click event of the theButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theButton_Click(object sender, EventArgs e)
        {
            Button theButton = ((Button)sender);

            string[] ID = theButton.ID.Split('-');
            string HrID = "", Min = "", AMPMID = "";
            if (theButton.ID.Contains("12AMPM"))
            {
                HrID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "12Hr").Replace("theBtn12AMPM", "SELECTLIST");
                Min = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "Min").Replace("theBtn12AMPM", "SELECTLIST");
                AMPMID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "AMPM").Replace("theBtn12AMPM", "SELECTLIST");
            }
            else
            {
                HrID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "24Hr").Replace("theBtn", "SELECTLIST");
                Min = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "Min").Replace("theBtn", "SELECTLIST");
            }
            ICustomForm MgrTime = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            String[] Time;
            if (theButton.ID.Contains("12AMPM"))
            {
                Time = MgrTime.GetSystemTime(12).Replace(" ", ":").Split(':');
            }
            else
            {
                Time = MgrTime.GetSystemTime(24).Split(':');
            }
            foreach (object obj in tabContainer.Controls)
            {
                if (obj is TabPanel)
                {
                    TabPanel tabPanel = (TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (((DropDownList)x).ID == HrID && ((DropDownList)x).ID.Contains("12Hr"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[0]);
                                    }
                                    else if (((DropDownList)x).ID == HrID && ((DropDownList)x).ID.Contains("24Hr"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[0]);
                                    }
                                    else if (((DropDownList)x).ID == Min && ((DropDownList)x).ID.Contains("Min"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[1]);
                                    }
                                    else if (((DropDownList)x).ID == AMPMID && ((DropDownList)x).ID.Contains("AMPM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[2]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /******** Fill Regimen *********/

        /// <summary>
        /// Handles the Load event of the theDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theDuration_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
        }

        /// <summary>
        /// Updates the cancel.
        /// </summary>
        private void UpdateCancel()
        {
            TabContainer container = tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnCurrenTabIndex.Value);
            hdnPrevTabIndex.Value = hdnCurrenTabIndex.Value;
            //--- For Cancel event, on updating the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "alert('Data on " + hdnPrevTabName.Value + " updated successfully.');\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            hdnPrevTabName.Value = hdnCurrenTabName.Value;
        }

        /// <summary>
        /// Updates the custom form data.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="ds">The ds.</param>
        /// <param name="DQChk">The dq CHK.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private StringBuilder UpdateCustomFormData(int patientId, int featureId, int visitId, int locationId, DataSet ds, int DQChk, int tabId)
        {
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            TabContainer container = tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataView theViewDT = new DataView(theDT);
            theViewDT.RowFilter = "TabId=" + tabId + "";
            theDT = theViewDT.ToTable();

            StringBuilder SbUpdateParam = new StringBuilder();
            StringBuilder SbUpdateColMstPatient = new StringBuilder();
            StringBuilder SbUpdateValMstPatient = new StringBuilder();
            //  string str = "";
            int ICD10Count = 0;
            DataView theViewMulti = new DataView(((DataTable)ViewState["LnkTable"]));
            theViewMulti.RowFilter = "TabId=" + tabId + "";
            DataTable theDTMulti = theViewMulti.ToTable();
            DataView theViewother = new DataView(((DataTable)ViewState["NoMulti"]));
            theViewother.RowFilter = "TabId=" + tabId + "";
            DataTable theDTother = theViewother.ToTable();

            #region "Conditional Field Inclusion"

            DataView theConditionalView = new DataView(((DataSet)Session["AllData"]).Tables[17]);
            theConditionalView.RowFilter = "TabId=" + tabId + "";
            DataTable theConditionalDT = theConditionalView.ToTable();

            foreach (DataRow theConDR in theConditionalDT.Rows)
            {
                DataRow theDTMultiDR = theDTMulti.NewRow();
                theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
                theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
                theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
                theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
                theDTMultiDR["FieldId"] = theConDR["FieldId"];
                theDTMultiDR["FieldName"] = theConDR["FieldName"];
                theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
                theDTMultiDR["Predefined"] = theConDR["Predefined"];
                theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
                theDTMultiDR["ControlId"] = theConDR["ControlId"];
                theDTMultiDR["BindSource"] = theConDR["BindSource"];
                theDTMultiDR["CodeId"] = theConDR["CodeId"];
                theDTMultiDR["Seq"] = theConDR["Seq"];
                theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
                theDTMultiDR["TabId"] = theConDR["TabId"];
                theDTMultiDR["TabName"] = theConDR["TabName"];
                theDTMulti.Rows.Add(theDTMultiDR);

                if (theConDR["ControlId"].ToString() != "9")
                {
                    DataRow theDTOtherDR = theDTother.NewRow();
                    theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                    theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                    theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                    theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                    theDTOtherDR["FieldId"] = theConDR["FieldId"];
                    theDTOtherDR["FieldName"] = theConDR["FieldName"];
                    theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                    theDTOtherDR["Predefined"] = theConDR["Predefined"];
                    theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                    theDTOtherDR["ControlId"] = theConDR["ControlId"];
                    theDTOtherDR["BindSource"] = theConDR["BindSource"];
                    theDTOtherDR["CodeId"] = theConDR["CodeId"];
                    theDTOtherDR["Seq"] = theConDR["Seq"];
                    theDTOtherDR["TabId"] = theConDR["TabId"];
                    theDTOtherDR["TabName"] = theConDR["TabName"];
                    theDTother.Rows.Add(theDTOtherDR);
                }
            }

            #endregion "Conditional Field Inclusion"

            DataTable LnkDT = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();

            //Generating Query for MultiSelect List
            foreach (DataRow DRMultiSelect in theDTMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9" || DRMultiSelect["ControlID"].ToString() == "15")
                {
                    StringBuilder DeleteInsertMultiselect = UpdateMultiSelectList(patientId, featureId, visitId, locationId, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]), tabId);
                    SbUpdateParam.Append(DeleteInsertMultiselect);

                }
                else if (DRMultiSelect["ControlID"].ToString() == "16" && ICD10Count == 0)
                {
                    int Setflag = 0;
                    StringBuilder SbUpdateICD10 = new StringBuilder();
                    StringBuilder SbValuesICD10 = new StringBuilder();
                    string FieldId = DRMultiSelect["FieldID"].ToString().StartsWith("8888") ? DRMultiSelect["FieldID"].ToString().Replace("8888", "") : DRMultiSelect["FieldID"].ToString().Replace("9999", "");
                    SbUpdateICD10.Append("Delete from dtl_ICD10Field where Ptn_pk=" + patientId + " and LocationId=" + locationId + " and Visit_pk=" + visitId + " and FieldId='" + FieldId + "' and TabId=" + tabId + "");
                    foreach (DataRow theICD10Row in theDT.Rows)
                    {
                        if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("%") && theICD10Row[2].ToString().Contains("1") && Setflag == 0)
                        {
                            string[] strICD10 = theICD10Row[0].ToString().Split('%');
                            FieldId = strICD10[0].StartsWith("8888") ? strICD10[0].Replace("8888", "") : strICD10[0].Replace("9999", "");
                            SbUpdateICD10.Append("Insert into [dtl_ICD10Field](ptn_pk, Visit_Pk, LocationID, FieldId, BlockId,SubBlockId,ICDCodeId, Predefined, UserID,CreateDate,TabId,");
                            SbValuesICD10.Append("Values(" + patientId + "," + visitId + "," + locationId + "," + FieldId + ", " + strICD10[1] + ", " + strICD10[2] + ", " + strICD10[3] + "," + strICD10[4] + "," + Session["AppUserId"] + ", GetDate()," + tabId + ",");
                            Setflag = 1;
                        }
                        else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("OnSetDate") && Setflag == 1)
                        {
                            SbUpdateICD10.Append("DateOnSet,");
                            SbValuesICD10.Append("'" + theICD10Row[2] + "',");
                        }
                        else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("Comment") && Setflag == 1)
                        {
                            SbUpdateICD10.Append("Comments)");
                            SbValuesICD10.Append("'" + theICD10Row[2] + "')");
                            Setflag = 0;
                            SbUpdateICD10.Append(SbValuesICD10);
                            SbValuesICD10 = new StringBuilder();
                        }
                    }
                    SbUpdateParam.Append(SbUpdateICD10);
                    ICD10Count = 1;
                }
            }

            //To Add Signature Dropdown for Each tab
            DataTable theDTORDVisit = ((DataSet)Session["AllData"]).Tables[23];
            String Signature = "0";
            if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 1)
            {
                LnkDT.Rows.Add("LNK_FORMTABORDVISIT", "" + lblFeatureName.Text + "");
            }
            if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 0)
            {
                Signature = ddSignature.SelectedValue;
            }
            //Update statement if already exists
            foreach (DataRow DR in LnkDT.Rows)
            {
                Boolean Valuethere = false;
                foreach (DataRow DRlnk in theDT.Rows)
                {
                    if (DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString() && Convert.ToString(DRlnk["Value"]) != "")
                    {
                        Valuethere = true;
                    }
                }

                StringBuilder builder = new StringBuilder();
                if (DR[0].ToString() == "DTL_CUSTOMFIELD" && Valuethere == true)
                {
                    builder = new StringBuilder(" if exists(Select * from [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] where ptn_pk=" + patientId + "");
                    builder.Append(" and Visit_pk=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] SET ");
                }
                else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                }
                else if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                {
                }
                else if (DR[0].ToString() == "BMI".ToUpper())
                {
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and Visitid=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT" && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where Visit_pk=" + visitId + " and TabId=" + tabId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "MST_PATIENT".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and Visit_pk=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                if (builder.ToString().Trim().Length > 0)
                {
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString() && Convert.ToString(DRlnk["value"]) != "")
                        {
                            if (DR["PDFTableName"].ToString() == "LNK_FORMTABORDVISIT")
                            {
                                builder.Append("Signature").Append(" = ").Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                            }
                            else
                            {
                                builder.Append("[" + DRlnk["column"] + "]").Append(" = ").Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                            }
                        }
                    }
                    if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                    {
                    }
                    else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper() && Valuethere == true)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        builder.Append(" where Ptn_Pk=" + patientId + " and LocationID=" + locationId + "");
                        builder.Append(" End ");
                        SbUpdateParam.Append(builder);
                    }
                    else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper() && Valuethere == true)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        builder.Append(" where Ptn_Pk=" + patientId + " and Visitid=" + visitId + " and LocationID=" + locationId + "");
                        builder.Append(" End ");
                        SbUpdateParam.Append(builder);
                    }
                    else if (Convert.ToString(DR[0]) == "MST_PATIENT" && Valuethere == true)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        builder.Append(" where Ptn_Pk=" + patientId + " and LocationID=" + locationId + "");
                        builder.Append(" End ");
                        SbUpdateParam.Append(builder);
                    }
                    else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT".ToUpper() && Valuethere == true)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        builder.Append(" where  Visit_pk=" + visitId + " and TabId=" + tabId + "");
                        builder.Append(" End ");
                        SbUpdateParam.Append(builder);
                    }
                    else if (Valuethere == true)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        builder.Append(" where Ptn_Pk=" + patientId + " and Visit_pk=" + visitId + " and LocationID=" + locationId + "");
                        builder.Append(" End ");
                        SbUpdateParam.Append(builder);
                    }
                }
            }

            //Insert Statement
            foreach (DataRow DR in LnkDT.Rows)
            {
                StringBuilder builder = new StringBuilder();
                if (DR[0].ToString() == "DTL_CUSTOMFIELD")
                {
                    builder = new StringBuilder(" if not exists(Select * from [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] where ptn_pk=" + patientId + "");
                    builder.Append(" and Visit_pk=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "](ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("[" + DRlnk["column"] + "]").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                }
                else if (DR[0].ToString().ToUpper() == "BMI")
                {
                }
                else if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                {
                }
                else if (DR[0].ToString() == "mst_Patient".ToUpper())
                {
                }
                else if (DR[0].ToString() == "")
                {
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper())
                {
                    builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + patientId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper())
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and Visitid=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, Visitid, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT")
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where Visit_pk=" + visitId + " and TabId=" + tabId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](Visit_pk, UserID,CreateDate,TabId,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("Signature").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + visitId + ", " + Session["AppUserId"] + ", GetDate()," + tabId + ",");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + patientId + "");
                    builder.Append(" and Visit_pk=" + visitId + " and LocationID=" + locationId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
            }


            // todo
            if (IsSingleVisit == false)
            {
                SbUpdateParam.Append(" Update Ord_visit Set VisitDate='" + txtvisitDate.Text + "', Signature='" + Signature + "',DataQuality='" + DQChk.ToString() + "'where Ptn_Pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "");// and UserId=" + Session["AppUserId"] + "");
            }
            else
            {
                SbUpdateParam.Append(" Update Ord_visit Set Signature='" + Signature + "', DataQuality ='" + DQChk.ToString() + "' where Ptn_Pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "");// and UserId=" + Session["AppUserId"] + "");
            }
            string theRegVisitDate;
            // todo
            if (IsSingleVisit == true)
            {
                theRegVisitDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                theRegVisitDate = txtvisitDate.Text;
            }

            SbUpdateParam.Append(" Select Visit_Id[VisitID] from ord_visit where Ptn_Pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrder where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk=" + patientId + " and VisitID=" + visitId + " and LocationID=" + locationId + ") and TabId=" + tabId + "");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrderNonARV where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk=" + patientId + " and VisitID=" + visitId + " and LocationID=" + locationId + ")  and TabId=" + tabId + "");

            //////////////////////////////SCM Section////////////////////////////////////////////////

            SbUpdateParam.Append(" UPDATE Dtl_PatientBillTransaction SET TransactionDate='" + theRegVisitDate + "',ConsultancyFee = dbo.fn_GetConsultationPerVisit_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" AdminFee = dbo.fn_GetOverHeadPerVisit_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" BillAmount = dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + theRegVisitDate + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" DoctorId = '" + Signature + "',UserId = " + Session["AppUserId"] + ",UpdateDate = getdate()");
            SbUpdateParam.Append(" where VisitID=" + visitId + "");
            ////////////////////////////////////////////////////////////////////////////////////////
            if (ds.Tables[1].Rows.Count > 0 || ds.Tables[2].Rows.Count > 0)
            {
                SbUpdateParam.Append(" if not exists(Select * from [ord_patientpharmacyorder] where ptn_pk=" + patientId + "");
                SbUpdateParam.Append(" and VisitID=" + visitId + " and LocationID=" + locationId + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate)");
                SbUpdateParam.Append(" Values(" + patientId + "," + visitId + "," + locationId + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append(" " + Session["AppUserId"] + "," + Signature + ", getdate())");
                SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=" + visitId + "");
            }
            else { SbUpdateParam.Append(" Select '00000'[PharmacyID]"); };

            #region Insert GridView

            //DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView","FeatureName").Copy();
            DataTable lnkSection = theDTMulti.DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "SectionInfo", "IsGridView", "FeatureName").Copy();
            DataView theDVSectionGridView = new DataView(lnkSection);
            theDVSectionGridView.RowFilter = "IsGridView= 1";
            if (theDVSectionGridView.Count > 0)
            {
                StringBuilder sbInsertGridView = new StringBuilder();
                foreach (DataRow DRGridView in theDVSectionGridView.ToTable().Rows)
                {
                    sbInsertGridView.Append(InsertGridView(patientId, Convert.ToInt32(DRGridView["FeatureID"].ToString()), Convert.ToInt32(DRGridView["SectionID"]), DRGridView["SectionName"].ToString(), visitId, DRGridView["FeatureName"].ToString()));
                    sbInsertGridView.Append(";");
                }
                SbUpdateParam.Append(sbInsertGridView);
            }
            #endregion

            SbUpdateParam.Append(" Delete from dbo.dtl_LabOrderTestResult where LabOrderId = (Select plo.LabID from dbo.ord_PatientLabOrder plo inner join  dtl_PatientLabResults plr on plr.LabID =plo.LabID  ");
            SbUpdateParam.Append(" where plo.ptn_pk=" + patientId + " and plo.VisitID=" + visitId + " and plo.LocationID=" + locationId + " and plr.TabId=" + tabId + ")");
           // SbUpdateParam.Append(" Delete from dbo.dtl_PatientLabResults where LabID = (Select LabID from dbo.ord_PatientLabOrder");
           // SbUpdateParam.Append(" where ptn_pk=" + patientId + " and VisitID=" + visitId + " and LocationID=" + locationId + ") and TabId=" + tabId + "");
            SbUpdateParam.Append(" Delete from dbo.Dtl_PatientBillTransaction where LabID = (Select LabID from dbo.ord_PatientLabOrder");
            SbUpdateParam.Append(" where ptn_pk=" + patientId + " and VisitID=" + visitId + " and LocationID=" + locationId + ");   ");
            if (ds.Tables[0].Rows.Count > 0)
            {


                SbUpdateParam.Append(" if not exists(Select * from [ord_PatientLabOrder] where ptn_pk=" + patientId + "");
                SbUpdateParam.Append(" and VisitID=" + visitId + " and LocationID=" + locationId + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_LabOrder](ptn_pk, VisitID, LocationID, Orderedby, OrderedDate,  UserID, CreateDate)");
                SbUpdateParam.Append(" Values(" + patientId + "," + visitId + "," + locationId + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append("" + Session["AppUserId"] + ", getdate())");
                SbUpdateParam.Append(" End");
                //SbUpdateParam.Append(" Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=" + visitId + "");
                //SbUpdateParam.Append(" if not exists(Select * from [ord_PatientLabOrder] where ptn_pk=" + patientId + "");
                //SbUpdateParam.Append(" and VisitID=" + visitId + " and LocationID=" + locationId + ")");
                //SbUpdateParam.Append(" Begin");
                //SbUpdateParam.Append(" Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                //SbUpdateParam.Append(" Values(" + patientId + "," + visitId + "," + locationId + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                //SbUpdateParam.Append("" + Signature + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                //SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=" + visitId + "");
             
            }
            else { SbUpdateParam.Append(" Select '00000'[LabID]"); }

            return SbUpdateParam;
        }

        /// <summary>
        /// Updates the multi select list.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="multiSelectTable">The multi_ select table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="deleteFlag">The delete flag.</param>
        /// <param name="theControlId">The control identifier.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private StringBuilder UpdateMultiSelectList(int patientId, int featureId, int visitId, int locationId, string multiSelectTable, string columnName, int deleteFlag, int theControlId, int tabId)
        {
            StringBuilder Updatecbl = new StringBuilder();
            if (deleteFlag == 0)
            {
                if (multiSelectTable == "DTL_CUSTOMFIELD")
                {
                    multiSelectTable = "dtl_FB_" + columnName + "";
                    multiSelectTable = multiSelectTable.Trim().Replace(' ', '_');
                }
                bool tabExists = false;
                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                DataSet dsMultiSelectList = new DataSet();
                dsMultiSelectList.ReadXml(filePath);
                DataTable DT = dsMultiSelectList.Tables[0];
                foreach (DataRow DR in DT.Rows)
                {
                    if (DR[0].ToString().ToUpper() == multiSelectTable.ToUpper())
                    {
                        tabExists = true;
                    }
                }
                if (Updatecbl.ToString().Contains(multiSelectTable.ToString()) == false)
                {
                    if (tabExists)
                    {
                        Updatecbl.Append("Delete from [" + multiSelectTable + "] where [ptn_pk]=" + patientId + " and [Visit_Pk]=" + visitId + " and [LocationID]=" + locationId + " and [TabId]=" + tabId + "");
                    }
                    else
                    {
                        Updatecbl.Append("Delete from [" + multiSelectTable + "] where [ptn_pk]=" + patientId + " and [Visit_Pk]=" + visitId + " and [LocationID]=" + locationId + "");
                        TabContainer container = tabContainer;
                        foreach (object obj in container.Controls)
                        {
                            if (obj is TabPanel)
                            {
                                TabPanel tabPanel = (TabPanel)obj;

                                if (Convert.ToInt32(tabPanel.ID) == tabId)
                                {
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object y in c.Controls)
                                            {
                                                if (y.GetType() == typeof(Panel))
                                                {
                                                    string strCheckBox = string.Empty;
                                                    string strTableName = string.Empty;
                                                    string strDate1 = string.Empty;
                                                    string strDate2 = string.Empty;
                                                    string strNumeric = string.Empty;
                                                    string[] TableName1 = null;
                                                    string Table1 = string.Empty;

                                                    foreach (Control x in ((Control)y).Controls)
                                                    {
                                                        if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                                                            return Updatecbl;
                                                        if (x.GetType() == typeof(CheckBox))
                                                        {
                                                            string[] TableName = ((CheckBox)x).ID.Split('-');
                                                            TableName1 = ((CheckBox)x).ID.Split('-');
                                                            if (TableName.Length == 5)
                                                            {
                                                                string Table = TableName[3];
                                                                Table1 = TableName[3];
                                                                if (Table1 == "DTL_CUSTOMFIELD")
                                                                {
                                                                    Table1 = "DTL_FB_" + TableName[2] + "";
                                                                    Table1 = Table1.Replace(' ', '_');
                                                                }
                                                                if (Table1 != "DTL_CUSTOMFIELD")
                                                                {
                                                                    if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                                                    {
                                                                        if (strCheckBox == string.Empty)
                                                                        {
                                                                            strCheckBox = TableName[1];
                                                                            strTableName = Table1;
                                                                        }
                                                                        else
                                                                        {
                                                                            strCheckBox = strCheckBox + ", " + TableName[1];
                                                                            strTableName = strTableName + "," + Table1;
                                                                        }
                                                                    }
                                                                    else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                                                    {
                                                                        if (strCheckBox == string.Empty)
                                                                        {
                                                                            strCheckBox = TableName[1];
                                                                            strTableName = Table1;
                                                                        }
                                                                        else
                                                                        {
                                                                            strCheckBox = strCheckBox + ", " + TableName[1];
                                                                            strTableName = strTableName + "," + Table1;
                                                                        }
                                                                        ViewState["OtherNote"] = ((CheckBox)x).Text;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (((CheckBox)x).Checked == true)
                                                                    {
                                                                        Updatecbl.Append("Insert into [" + multiSelectTable + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                        Updatecbl.Append("values (" + patientId + ",  " + visitId + ", " + locationId + "," + TableName[1] + ",");
                                                                        Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (x.GetType() == typeof(TextBox))
                                                        {
                                                            string[] TableName = ((TextBox)x).ID.Split('-');
                                                            if (TableName[0] == "TXTDT1" || TableName[0] == "TXTDT2" || TableName[0] == "TXTNUM")
                                                            {
                                                                if (TableName[0] == "TXTDT1")
                                                                {
                                                                    if (strDate1 == string.Empty)
                                                                    {
                                                                        if (((TextBox)x).Text != "")
                                                                        {
                                                                            strDate1 = ((TextBox)x).Text;
                                                                        }
                                                                        else
                                                                        {
                                                                            strDate1 = "1/1/1900";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        strDate1 = strDate1 + ", " + ((TextBox)x).Text;
                                                                    }
                                                                }
                                                                else if (TableName[0] == "TXTDT2")
                                                                {
                                                                    if (strDate2 == string.Empty)
                                                                    {
                                                                        if (((TextBox)x).Text != "")
                                                                        {
                                                                            strDate2 = ((TextBox)x).Text;
                                                                        }
                                                                        else
                                                                        {
                                                                            strDate2 = "1/1/1900";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        strDate2 = strDate2 + ", " + ((TextBox)x).Text;
                                                                    }
                                                                }
                                                                else if (TableName[0] == "TXTNUM")
                                                                {
                                                                    if (strNumeric == string.Empty)
                                                                        strNumeric = ((TextBox)x).Text;
                                                                    else
                                                                        strNumeric = strNumeric + ", " + ((TextBox)x).Text;
                                                                }
                                                            }
                                                        }
                                                        if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                                        {
                                                            string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                                            string Table = TableName[3];
                                                            string Other = "";
                                                            if (Table == "DTL_CUSTOMFIELD")
                                                            {
                                                                Table = "DTL_FB_" + TableName[2] + "";
                                                                Table = Table.Replace(' ', '_');
                                                            }
                                                            if (Table != "DTL_CUSTOMFIELD")
                                                            {
                                                                foreach (DataRow DR in DT.Rows)
                                                                {
                                                                    if (DR[0].ToString().ToUpper() == Table)
                                                                    {
                                                                        Other = DR[2].ToString();
                                                                    }
                                                                }
                                                                if (theControlId == 15)
                                                                {
                                                                    Other = "Other";
                                                                }

                                                                if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "" && Other != "")
                                                                {
                                                                    Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate], DateField1, DateField2, NumericField,TabId)");
                                                                    Updatecbl.Append("values (" + patientId + ", " + visitId + ", " + locationId + "," + TableName[1] + ",");
                                                                    Updatecbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate(), '" + Convert.ToDateTime("1/1/1900") + "',  '" + Convert.ToDateTime("1/1/1900") + "', 0, " + tabId + ")");
                                                                    ViewState["OtherNote"] = null;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    // code here
                                                    if (!string.IsNullOrEmpty(strCheckBox))
                                                    {
                                                        string[] arrDate1 = null;
                                                        string[] arrDate2 = null;
                                                        string[] arrNumeric = null;
                                                        string[] arrCheckBox = null;
                                                        string[] arrTableName = null;

                                                        if (strCheckBox != string.Empty)
                                                            arrCheckBox = strCheckBox.Split(',');
                                                        if (strDate1 != string.Empty)
                                                            arrDate1 = strDate1.Split(',');
                                                        if (strDate2 != string.Empty)
                                                            arrDate2 = strDate2.Split(',');
                                                        if (strNumeric != string.Empty)
                                                            arrNumeric = strNumeric.Split(',');
                                                        if (strCheckBox != string.Empty)
                                                            arrTableName = strTableName.Split(',');

                                                        DateTime dtDate1 = new DateTime();
                                                        DateTime dtDate2 = new DateTime();

                                                        int intNumeric = 0;
                                                        for (int i = 0; i < arrCheckBox.Length; i++)
                                                        {
                                                            dtDate1 = Convert.ToDateTime("1/1/1900");
                                                            dtDate2 = Convert.ToDateTime("1/1/1900");
                                                            if (arrDate1 != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrDate1[i]) && arrDate1[i] != " ")
                                                                    dtDate1 = Convert.ToDateTime(arrDate1[i]);
                                                            }

                                                            if (arrDate2 != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrDate2[i]) && arrDate2[i] != " ")
                                                                    dtDate2 = Convert.ToDateTime(arrDate2[i]);
                                                            }
                                                            if (arrNumeric != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrNumeric[i]) && arrNumeric[i] != " ")
                                                                    intNumeric = Convert.ToInt16(arrNumeric[i]);
                                                            }

                                                            Boolean tabIdexist = false;
                                                            foreach (DataRow DR in DT.Rows)
                                                            {
                                                                if (DR[0].ToString().ToUpper() == Table1)
                                                                {
                                                                    tabIdexist = true;
                                                                }
                                                            }

                                                            //Creating column for old database where Column Name 'Datefield1', 'Datefield2' and Numericfield doesnot exist
                                                            StringBuilder createColumn = new StringBuilder();
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField1')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField1 datetime End ");
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField2')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField2 datetime End ");
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='NumericField')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add NumericField int End ");
                                                            createColumn.Append("Select 1[Saved]");
                                                            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
                                                            DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(createColumn.ToString());

                                                            if (tabIdexist)
                                                            {
                                                                //    Updatecbl.Append("Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField, TabId)");
                                                                //    Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + ", " + arrCheckBox[i] + ",");
                                                                //    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ", " + TabId + ")");
                                                            }
                                                            else
                                                            {
                                                                string[] spltbl = strTableName.Split(',');
                                                                if (spltbl[0].ToLower() == multiSelectTable.ToLower())
                                                                {
                                                                    Updatecbl.Append("Insert into [" + multiSelectTable + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField)");
                                                                    Updatecbl.Append("values (" + patientId + ",  " + visitId + ", " + locationId + ", " + arrCheckBox[i] + ",");
                                                                    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ")");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Updatecbl;
        }

        //john 28th june 2013
        /// <summary>
        /// Users the rights.
        /// </summary>
        /// <param name="save">The save.</param>
        /// <param name="DQ">The dq.</param>
        /// <param name="print">The print.</param>
        /// <param name="featureId">The feature identifier.</param>
        private void UserRights(Button save, Button DQ, Button print, int featureId)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(featureId, FunctionAccess.View, (DataTable)Session["UserRight"]) == true)
            {
                save.Enabled = false;
                DQ.Enabled = false;
            }
            else
            {
                save.Enabled = true;
                DQ.Enabled = true;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentication.HasFunctionRight(featureId, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    save.Enabled = false;
                    DQ.Enabled = false;
                }
                else
                {
                    save.Enabled = true;
                    DQ.Enabled = true;
                }
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (Authentication.HasFunctionRight(featureId, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    save.Enabled = false;
                    DQ.Enabled = false;
                }
                else
                {
                    save.Enabled = true;
                    DQ.Enabled = true;
                }
            }

            if (Authentication.HasFunctionRight(featureId, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                print.Enabled = false;
            }
        }

        /// <summary>
        /// Validations the message.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <returns></returns>
        private string ValidationMessage(DataSet theDS, int tabId)
        {
            DateTime theCurrentDate = SystemSetting.SystemDate;
            string strmsg = "Following values are required to complete this:\\n\\n";
            DataView theViewValidation = new DataView(((DataTable)ViewState["BusRule"]));
            theViewValidation.RowFilter = "TabId=" + tabId + "";
            DataTable theDT = theViewValidation.ToTable();

            string radio1 = "", radio2 = "", multiSelectName = "", multiSelectLabel = "";
            int TotCount = 0, FalseCount = 0, TextBoxDate1FalseCount = 0, TextBoxDate2FalseCount = 0, TextBoxNumericFalseCount = 0;
            try
            {
                //TabContainer container = (TabContainer)tabcontainer;
                foreach (TabPanel tabPanel in this.tabContainer.Tabs)
                {
                    //if (obj is TabPanel)
                    // {
                    //  TabPanel tabPanel = (TabPanel)obj;
                    foreach (Control ctrl in tabPanel.Controls)
                    {
                        // if (ctrl is Control)
                        //{
                        Control c = ctrl;
                        foreach (object x in c.Controls)
                        {
                            if (x.GetType() == typeof(TextBox))
                            {
                                string[] Field = ((TextBox)x).ID.Split('-');

                                foreach (DataRow theDR in theDT.Rows)
                                {
                                    if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                                    {
                                        string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                        if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2] == Convert.ToString(theDR["TableName"]) && Field10[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                        {
                                            if (((TextBox)x).Text == "")
                                            {
                                                string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((TextBox)x).ID + "";
                                                if (!(htcontrolstatus.ContainsKey(str)))
                                                {
                                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                    {
                                        if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((TextBox)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }

                                    //Date Greater than Today's Date
                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "7")
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                            if (GetDate < Convert.ToDateTime(theCurrentDate.ToShortDateString()))
                                            {
                                                strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                    //Date Less than Today's Date
                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "8")
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                                            if (GetDate >= Convert.ToDateTime(theCurrentDate.ToShortDateString()))
                                            {
                                                strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                    //Date greater than Date of Birth
                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "9")
                                    {
                                        DateTime GetDOB = Convert.ToDateTime(hdfldDOB.Value);
                                        if (((TextBox)x).Text != "")
                                        {
                                            DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                            if (GetDate <= GetDOB)
                                            {
                                                strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                }
                            }

                            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                            {
                                string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                                if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                                {
                                    radio1 = Field[3];
                                }
                                if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                                {
                                    radio2 = Field[3];
                                }

                                foreach (DataRow theDR in theDT.Rows)
                                {
                                    if (radio1 == Field[3] && radio2 == Field[3])
                                    {
                                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                        {
                                            strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                            strmsg = strmsg + "\\n";
                                        }
                                    }
                                }
                            }
                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                string[] Field = ((DropDownList)x).ID.Split('-');
                                foreach (DataRow theDR in theDT.Rows)
                                {
                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[0].ToString() != "SELECTLISTAuto" && Field[3].Contains("12Hr") == false && Field[3].Contains("24Hr") == false && Field[3].Contains("Min") == false && Field[3].Contains("AMPM") == false)
                                    {
                                        if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                                //ScriptManager.RegisterClientScriptBlock(, this.GetType(), "test1", "alert('test 1');", true);
                                                //ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue2", "fncontrolstatus('ctl00_IQCareContentPlaceHolder_" + tabcontainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "');", true);
                                            }
                                        }
                                    }
                                    else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("12Hr"))
                                    {
                                        if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                    else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("24Hr"))
                                    {
                                        if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                    else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("Min"))
                                    {
                                        if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                    else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("AMPM"))
                                    {
                                        if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                        {
                                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                            if (!(htcontrolstatus.ContainsKey(str)))
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                }
                            }

                            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                            {
                                string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                                foreach (DataRow theDR in theDT.Rows)
                                {
                                    if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                    {
                                        if (((HtmlInputCheckBox)x).Checked == false)
                                        {
                                            strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                            strmsg = strmsg + "\\n";
                                        }
                                    }
                                }
                            }

                            if (x.GetType() == typeof(System.Web.UI.WebControls.HiddenField))
                            {
                                string[] Field = ((HiddenField)x).ID.Split('-');

                                if (Field.Length == 4)
                                {
                                    foreach (DataRow theDR in theDT.Rows)
                                    {
                                        if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                        {
                                            if (theDS.Tables[0].Rows.Count == 0)
                                            {
                                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                }
                                if (Field.Length == 5)
                                {
                                    foreach (DataRow theDR in theDT.Rows)
                                    {
                                        if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1"
                                            && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                                        {
                                            if (theDS.Tables[1].Rows.Count == 0)
                                            {
                                                DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                                DataTable theDrugNameDT = theDV.ToTable();
                                                strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                        else if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1"
                                            && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                                        {
                                            if (theDS.Tables[2].Rows.Count == 0)
                                            {
                                                DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                                DataTable theDrugNameDT = theDV.ToTable();
                                                strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // }
                    }
                    // }
                }

                //MultiSelect Validation
                int DrugsCount = 0;
                foreach (TabPanel tabPanel in this.tabContainer.Tabs)
                {
                    //if (obj is TabPanel)
                    //{
                    //  TabPanel tabPanel = (TabPanel)obj;
                    foreach (Control ctrl in tabPanel.Controls)
                    {
                        //  if (ctrl is Control)
                        //  {
                        Control c = ctrl;
                        foreach (Control y in ctrl.Controls)
                        {
                            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                            {
                                foreach (Control z in y.Controls)
                                {
                                    if (z.GetType() == typeof(System.Web.UI.WebControls.Label))
                                    {
                                        if (z.ID.Contains("ARVdrgNm") == true || z.ID.Contains("ARVGenericNm") == true || z.ID.Contains("DrugNm") == true || z.ID.Contains("GenericNm") == true)
                                        {
                                            DrugsCount++;
                                        }
                                    }
                                    if (z.GetType() == typeof(TextBox))
                                    {
                                        if (z.ID.Contains("ARVdrgDuration") == true || z.ID.Contains("ARVGenericDuration") == true || z.ID.Contains("DrugDuration") == true || z.ID.Contains("GenericDuration") == true)
                                        {
                                            if (((TextBox)z).Text == "")
                                            {
                                                strmsg += "Drug Duration Cannot be Blank";
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                        else if (z.ID.Contains("GenericFrequency") == true || z.ID.Contains("genericQtyPrescribed") == true || z.ID.Contains("genericQtyDispensed") == true || z.ID.Contains("FinChkGeneric") == true)
                                        {
                                            if (((TextBox)z).Text == "")
                                            {
                                                strmsg += "Drug Duration Cannot be Blank";
                                                strmsg = strmsg + "\\n";
                                            }
                                        }
                                        else if (z.ID.Contains("TXTDT1") == true || z.ID.Contains("TXTDT2") == true || z.ID.Contains("TXTNUM") == true)
                                        {
                                            if (z.ID.Contains("TXTDT1"))
                                            {
                                                if (((TextBox)z).Text == string.Empty)
                                                {
                                                    TextBoxDate1FalseCount++;
                                                }
                                            }
                                            else if (z.ID.Contains("TXTDT2"))
                                            {
                                                if (((TextBox)z).Text == string.Empty)
                                                {
                                                    TextBoxDate2FalseCount++;
                                                }
                                            }
                                            else
                                            {
                                                if (((TextBox)z).Text == string.Empty)
                                                {
                                                    TextBoxNumericFalseCount++;
                                                }
                                            }
                                        }
                                    }

                                    if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                    {
                                        TotCount++;
                                        if (((CheckBox)z).Checked == false && ((CheckBox)z).Text.ToUpper().Trim() != "OTHER")
                                        {
                                            FalseCount++;
                                        }
                                    }
                                }
                                if (TextBoxDate1FalseCount > 0 || TextBoxDate2FalseCount > 0 || TextBoxNumericFalseCount > 0)
                                {
                                    if (FalseCount != 0)
                                    {
                                        //if (TextBoxDate1FalseCount >= FalseCount && TextBoxDate1FalseCount!=0)
                                        //{
                                        //    strmsg += "Please select Checkbox.";
                                        //    strmsg = strmsg + "\\n";
                                        //}
                                        //else if (TextBoxDate2FalseCount >= FalseCount && TextBoxDate2FalseCount !=0)
                                        //{
                                        //    strmsg += "Please select Checkbox.";
                                        //    strmsg = strmsg + "\\n";
                                        //}
                                        //else if (TextBoxNumericFalseCount >= FalseCount && TextBoxNumericFalseCount !=0)
                                        //{
                                        //    strmsg += "Please select Checkbox.";
                                        //    strmsg = strmsg + "\\n";
                                        //}
                                    }
                                }
                                foreach (DataRow theDR in theDT.Rows)
                                {
                                    string[] arrID = ((Panel)y).ID.Split('-');
                                    if (Convert.ToString(theDR["ControlId"]) == "9" && arrID[2] == Convert.ToString(theDR["FieldID"]) && Convert.ToInt32(theDR["BusRuleId"]) <= 13)
                                    {
                                        multiSelectName = Convert.ToString(theDR["Name"]);
                                        multiSelectLabel = Convert.ToString(theDR["FieldLabel"]);
                                        if (TotCount == FalseCount)
                                        {
                                            strmsg += multiSelectLabel + " is " + multiSelectName;
                                            strmsg = strmsg + "\\n";
                                        }
                                        if (TextBoxDate1FalseCount > 0 || TextBoxDate2FalseCount > 0 || TextBoxNumericFalseCount > 0)
                                        {
                                            strmsg += multiSelectLabel + " is " + multiSelectName;
                                            strmsg = strmsg + "\\n";
                                        }
                                    }
                                }
                                TotCount = 0; FalseCount = 0; TextBoxDate1FalseCount = 0; TextBoxDate2FalseCount = 0; TextBoxNumericFalseCount = 0;
                                multiSelectName = ""; multiSelectLabel = "";
                            }
                        }
                        //}
                    }
                    //}
                }

                foreach (DataRow DR in theDT.Rows)
                {
                    if (Convert.ToString(DR["BusRuleId"]) == "1" && Convert.ToString(DR["ControlId"]) == "11")
                    {
                        if (DrugsCount == 0)
                        {
                            strmsg = strmsg + "Please Select Drug Selection.";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally { }

            return strmsg;
        }

        #region ICallbackEventHandler Members

        /// <summary>
        /// Returns the results of a callback event that targets a control.
        /// </summary>
        /// <returns>
        /// The result of the callback.
        /// </returns>
        public string GetCallbackResult()
        {
            string thestr = str;
            return thestr;
        }

        /// <summary>
        /// Gets the raise event value.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="VisitID">The visit identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="theControl">The control.</param>
        /// <returns></returns>
        public DataSet GetRaiseEventValue(int PatientID, int VisitID, int LocationID, Control theControl)
        {
            TabContainer container = tabContainer;
            DataSet theDSAuto = new DataSet();
            DataTable theDTAuto = new DataTable("theDTAuto");
            theDTAuto.Columns.Add(new DataColumn("ID", typeof(String)));
            theDTAuto.Columns.Add(new DataColumn("Value", typeof(String)));
            theDTAuto.Columns.Add(new DataColumn("Ctrl", typeof(String)));
            DataRow theDR;
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(objFactoryParameter);
            DataTable theDT_I = SetControlIDs(tabContainer);
            DataTable TempDT = theDT_I.DefaultView.ToTable(true, "TableName").Copy();
            DataSet theDS = new DataSet();
            DataSet TempDS = new DataSet();
            try
            {
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string GetValue = "";
                    if (TempDR["TableName"].ToString() == "DTL_CUSTOMFIELD")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + lblFeatureName.Text.Replace(' ', '_');
                        GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                    else
                    {
                        if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(TempDR["TableName"]) == "dtl_PatientContacts".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + "";
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "mst_patient".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                        }
                        else if (TempDR["TableName"].ToString().ToUpper() == "DTL_CUSTOMFORM".ToUpper())
                        {
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientCareEnded".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientClinicalNotes".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + " and modifiedflag = 0 ";
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "lnk_FormTabOrdVisit".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Visit_pk=" + VisitID + "";
                        }
                        else
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                        }
                    }
                    if (GetValue.Length > 2)
                    {
                        TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                    }
                    if (TempDS.Tables.Count > 0)
                    {
                        for (int i = 0; i <= TempDS.Tables[0].Columns.Count - 1; i++)
                        {
                            foreach (object obj in container.Controls)
                            {
                                if (obj is TabPanel)
                                {
                                    TabPanel tabPanel = (TabPanel)obj;
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object x in c.Controls)
                                            {
                                                if (x.GetType() == typeof(TextBox))
                                                {
                                                    string[] remStr = ((TextBox)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("TXTMultiAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTSingleAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;

                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }

                                                    if ("TXTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTNUMAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }

                                                    if ("TXTDTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                // theDR["Value"] = string.Format("ddMMyyyy", TempDS.Tables[0].Rows[0][i].ToString());
                                                                theDR["Value"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(TempDS.Tables[0].Rows[0][i]));

                                                                //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]).ToString();
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTRegAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                }
                                                else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                                {
                                                    string[] remStr = ((DropDownList)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("SELECTLISTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            //((DropDownList)x).Enabled = true;
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((DropDownList)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "DropDown";
                                                            theDTAuto.Rows.Add(theDR);
                                                            ((DropDownList)x).Enabled = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //foreach (Control x in DIVCustomItem.Controls)
                            //{
                            //}
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
            }

            theDSAuto.Tables.Add(theDTAuto);
            return theDSAuto;
        }

        /// <summary>
        /// Processes a callback event that targets a control.
        /// </summary>
        /// <param name="eventArgument">A string that represents an event argument to pass to the event handler.</param>
        public void RaiseCallbackEvent(string eventArgument)
        {
            try
            {
                //if (IsPostBack != true)
                //{
                if (customFieldData.TableName == "")
                {
                    return;
                }

                DateTime theDT = Convert.ToDateTime(eventArgument.Trim().ToString());
                if ((!String.IsNullOrEmpty(eventArgument.Trim().ToString())) && (customFieldData.Rows.Count > 0) && (Convert.ToInt32(customFieldData.Rows[0][0]) > 0))
                {
                    DataRow[] theDR = customFieldData.Select("VisitDate < '" + theDT + "'");
                    DataView AutoDV = new DataView(customFieldData);

                    //AutoDV.RowFilter = "VisitDate < '" + theDT + "'";//Getting value Max Date
                    AutoDV.RowFilter = "VisitDate < " + "#" + theDT.ToString("MM/dd/yyyy") + "#";//Getting value Max Date
                    //dataView.RowFilter = "Date>=" + "#" + theDT + "#"
                    AutoDV.Sort = "VisitDate DESC";

                    //DataView AutoDV1 = new DataView(AutoDV.Table);
                    //AutoDV1.RowFilter = "VisitDate=Max(VisitDate)";
                    IQCareUtils theUtils = new IQCareUtils();
                    DataTable dt = new DataTable();
                    if (AutoDV.Table != null)
                    {
                        dt = theUtils.CreateTableFromDataView(AutoDV);
                    }
                    //if ((dt.Rows.Count > 0) &&  (Convert.ToInt32(Session["PatientVisitId"]) == 0))
                    if (dt.Rows.Count > 0)
                    {
                        DataSet theDSAuto = GetRaiseEventValue(Convert.ToInt32(dt.Rows[0]["ptn_pk"]), Convert.ToInt32(dt.Rows[0]["visit_pk"]), Convert.ToInt32(dt.Rows[0]["LocationID"]), DIVCustomItem);
                        str = theDSAuto.GetXml();
                        ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                    }
                }
                else
                {
                    int FeatureID1 = Convert.ToInt32(Session["FeatureID"]);
                    int PatientId1 = Convert.ToInt32(Session["PatientId"]);
                    int VisitID1 = Convert.ToInt32(Session["PatientVisitId"]);

                    int LocationID1 = Convert.ToInt32(Session["AppLocationId"]);
                    DataView AutoDVpre = new DataView(visitDetail);

                    if (AutoDVpre.Count > 0)
                    {
                        AutoDVpre.RowFilter = "VisitDate < " + "#" + theDT + "#";//Getting value Max Date
                        AutoDVpre.Sort = "VisitDate DESC";
                        AutoDVpre.Sort = "VisitID DESC";

                        IQCareUtils theUtils = new IQCareUtils();
                        DataTable dtpre = new DataTable();
                        if (AutoDVpre.Table != null)
                        {
                            dtpre = theUtils.CreateTableFromDataView(AutoDVpre);
                        }

                        if (dtpre.Rows.Count > 0)
                        {
                            DataSet theDSAutopre = GetRaiseEventValue(Convert.ToInt32(PatientId1), Convert.ToInt32(dtpre.Rows[0]["VisitID"]), Convert.ToInt32(LocationID1), DIVCustomItem);
                            str = theDSAutopre.GetXml();
                            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                        }
                    }
                }
            }
            finally
            {
                //CallBackmgr = null;
            }
        }

        #endregion ICallbackEventHandler Members
    }
}