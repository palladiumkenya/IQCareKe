using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

    namespace IQCare.FormBuilder
    {
    public partial class frmBusinessRule : Form
    {
        DataSet dsBusinessRule = new DataSet(); //Dataset to store all business rule from MSt_BusinessRule

        public frmBusinessRule()
        {
            InitializeComponent();
        }

        private void frmBusinessRule_Load(object sender, EventArgs e)
        {
            //disable all controls on form when form mode is 1 i.e. readonly
            if (GblIQCare.iFormMode.ToString() == "1")
            {
                pnlBusinessRule.Enabled = false;
            }

            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
            
            try
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                dsBusinessRule = objFieldDetail.GetBusinessRule();
                DataTable dtBusinessRule = new DataTable();
                /// <summary>
                /// This switch case is used to checked the checklistbox item(if any for same field)
                /// in case of update and show all the business rule on the basis of display type ("GblIQCare.strDisplayType")
                BindRegimenType();
                BindFunctions theBindMgr = new BindFunctions();
                DataView theBRuleDV = new DataView(dsBusinessRule.Tables[1]);

                if (GblIQCare.iManageCareEnded == 2)
                {
                    if ((GblIQCare.strDisplayType.ToString() == "Numeric") || (GblIQCare.strDisplayType.ToString() == "DecimalTextBox"))
                    {
                        theBRuleDV.RowFilter = "Id IN(1,2,3,16) and ControlName ='" + GblIQCare.strDisplayType + "'";
                        theBRuleDV.Sort = "Name asc";
                    }
                    else
                    {
                        theBRuleDV.RowFilter = "Id IN(1,16) and ControlName ='" + GblIQCare.strDisplayType + "'";
                        theBRuleDV.Sort = "Name asc";
                    }
                }
                else
                {
                    theBRuleDV.RowFilter = "ControlName ='" + GblIQCare.strDisplayType + "'";
                    theBRuleDV.Sort = "Name asc";
                }
                DataTable BusinessRulesDT = theBRuleDV.ToTable();
                if (BusinessRulesDT.Rows.Count > 0)
                {
                    BusinessRulesDT.Columns.Add("SNo", typeof(int));
                    for (int i = 0; i < BusinessRulesDT.Rows.Count; i++)
                    {
                        BusinessRulesDT.Rows[i]["SNo"] = i + 1;
                    }
                    var query = (from alias in BusinessRulesDT.AsEnumerable()
                                where (alias.Field<string>("name") == "Max Value")
                                select new
                                {
                                    name = alias["name"]
                                    
                                }).SingleOrDefault();
                    if (query != null)
                    {
                        DataRow dr = BusinessRulesDT.AsEnumerable().Where(r => ((string)r["name"]).Equals("Max Value")).First();
                        if (dr != null)
                        {
                            dr["SNo"] = "6";
                            DataRow dr2 = BusinessRulesDT.AsEnumerable().Where(r => ((string)r["name"]).Equals("Max Normal")).First();
                            if (dr2 != null)
                                dr2["SNo"] = "7";
                            BusinessRulesDT.AcceptChanges();
                        }
                    }

                    BusinessRulesDT.DefaultView.Sort = "SNo ASC";
                }
                if (GblIQCare.iManageCareEnded == 0)
                {
                    if ((BusinessRulesDT.Rows.Count > 0) && (Convert.ToString(BusinessRulesDT.Rows[0][2]) != ""))
                    {

                        if (BusinessRulesDT.Rows[0][2].ToString() == "14" || BusinessRulesDT.Rows[0][2].ToString() == "6" || BusinessRulesDT.Rows[0][2].ToString() == "7" || BusinessRulesDT.Rows[0][2].ToString() == "9" || BusinessRulesDT.Rows[0][2].ToString() == "10" || BusinessRulesDT.Rows[0][2].ToString() == "11" || BusinessRulesDT.Rows[0][2].ToString() == "12" || BusinessRulesDT.Rows[0][2].ToString() == "13")
                        {
                            theBRuleDV.RowFilter = "ControlName ='" + GblIQCare.strDisplayType + "' and Id <> 17";
                            //theBRuleDV.RowFilter = string.Format("Id <> 17");
                            theBRuleDV.Sort = "Name asc";
                            BusinessRulesDT = theBRuleDV.ToTable();
                        }
                        
                    }
                }

                if (GblIQCare.iManageCareEnded == 1)
                {

                    theBRuleDV.RowFilter = "ControlName ='" + GblIQCare.strDisplayType + "' and Id <> 17 and Id <> 26 and Id <> 27";
                        //theBRuleDV.RowFilter = string.Format("Id <> 17");
                        theBRuleDV.Sort = "Name asc";
                        BusinessRulesDT = theBRuleDV.ToTable();
                }

                if (theBRuleDV.Count > 0)
                {
                    chkLstBox.Items.Clear();
                    theBindMgr.Win_BindCheckListBox(chkLstBox,BusinessRulesDT , "Name", "Id");  
                }

                if (GblIQCare.objhashBusinessRule.Contains(GblIQCare.gblRowIndex))
                {
                    if (GblIQCare.objhashBusinessRule[GblIQCare.gblRowIndex].ToString() != "" || ((DataTable)GblIQCare.objhashBusinessRule[GblIQCare.gblRowIndex]).Rows.Count!=0)
                    {
                        dtBusinessRule = (DataTable)(GblIQCare.objhashBusinessRule[GblIQCare.gblRowIndex]);
                        if (dtBusinessRule.Rows.Count > 0)
                        {
                            foreach (DataRow theDRBr in dtBusinessRule.Rows)
                            {
                                for (int i = 0; i < chkLstBox.Items.Count; i++)
                                {

                                    DataRowView theDV = (DataRowView)chkLstBox.Items[i];
                                    if (theDV["Id"].ToString() == theDRBr["BusRuleId"].ToString())
                                    {
                                        chkLstBox.SetItemChecked(i, true);
                                        if (GblIQCare.iManageCareEnded == 2)
                                        {
                                            if (theDRBr["BusRuleId"].ToString() == "2")
                                            {
                                                txtMaxVal.Location = new Point(150, i * 20);
                                            }
                                            else if (theDRBr["BusRuleId"].ToString() == "3")
                                            {
                                                txtMinVal.Location = new Point(150, i * 20);
                                            }
                                            else if (theDRBr["BusRuleId"].ToString() == "26")
                                            {
                                                txtMaxNormalVal.Location = new Point(150, i * 20);
                                            }
                                            else if (theDRBr["BusRuleId"].ToString() == "27")
                                            {
                                                txtMinNormalVal.Location = new Point(150, i * 20);
                                            }
                                        }
                                        if (theDRBr["Value"].ToString() != "")
                                        {
                                            if (theDRBr["BusRuleId"].ToString() == "2")
                                                txtMaxVal.Text = theDRBr["Value"].ToString();
                                            else if (theDRBr["BusRuleId"].ToString() == "3")
                                                txtMinVal.Text = theDRBr["Value"].ToString();
                                            else if (theDRBr["BusRuleId"].ToString() == "26")
                                                txtMaxNormalVal.Text = theDRBr["Value"].ToString();
                                            else if (theDRBr["BusRuleId"].ToString() == "27")
                                                txtMinNormalVal.Text = theDRBr["Value"].ToString();
                                            else if (theDRBr["BusRuleId"].ToString() == "10" || theDRBr["BusRuleId"].ToString() == "11")
                                                ddlRegimenType.SelectedValue = theDRBr["Value"].ToString();
                                           //12may2011
                                            else if (theDRBr["BusRuleId"].ToString() == "16")
                                            {
                                                txtMinAgeRange.Text = theDRBr["Value"].ToString();
                                                txtMaxAgeRange.Text = theDRBr["Value1"].ToString();
                                            }

                                            else if (theDRBr["BusRuleId"].ToString() == "18")
                                                txtDate1.Text = theDRBr["Value"].ToString();
                                            else if (theDRBr["BusRuleId"].ToString() == "19")
                                            {
                                                string[] strValue = theDRBr["Value"].ToString().Split('_');
                                                if (strValue.Length > 1)
                                                {
                                                    if (strValue[0] != " ")
                                                        txtDate2.Text = strValue[0];
                                                    if (strValue[1] != " ")
                                                        txtDate1.Text = strValue[1];
                                                }
                                                else 
                                                {
                                                    txtDate2.Text = theDRBr["Value"].ToString();
                                                }
                                                
                                            }
                                            else if (theDRBr["BusRuleId"].ToString() == "20")
                                                txtNumeric.Text = theDRBr["Value"].ToString();
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
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void BindRegimenType()
        {
            BindFunctions theBind = new BindFunctions();
            IFieldDetail objFetchDrugTypes;
            objFetchDrugTypes = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet objDSDrugType = objFetchDrugTypes.GetDrugType();

            DataView theBRuleDV1 = new DataView(objDSDrugType.Tables[0]);
            theBRuleDV1.Sort = "DrugTypeName asc";
            DataTable BusinessRulesDT1 = theBRuleDV1.ToTable();

            theBind.Win_BindCombo(ddlRegimenType, BusinessRulesDT1, "DrugTypeName", "DrugTypeId");
       }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (FieldValidation() == false)
            {
                return;
            }

            //<local variable>
           // bool blnItemChecked;
            // This is used to check whether value from the checklist box is checked or not.
            //<local Variable declaration end>

            /// <summary>
            /// This loop is used add/update checked item into datatabletable and before that remove 
            /// the previous business rule(if any) from the datatable 


            /// <summary>

            try
            {
                ClearTable();

                DataTable btable = new DataTable();
                btable.Columns.Add("FieldName", typeof(string));
                btable.Columns.Add("BusRuleId", typeof(string));
                btable.Columns.Add("Value", typeof(string));
                btable.Columns.Add("Value1", typeof(string));
                foreach (object obj in chkLstBox.CheckedItems)
                {
                    DataRowView theDV = (DataRowView)obj;
                    DataRow theDR = btable.NewRow();
                    theDR["FieldName"] = GblIQCare.strFieldName;
                    theDR["BusRuleId"] = theDV["BusinessRuleId"];
                    if (theDV["BusinessRuleId"].ToString() == "2")
                        theDR["Value"] = txtMaxVal.Text;
                    else if (theDV["BusinessRuleId"].ToString() == "3")
                        theDR["Value"] = txtMinVal.Text;
                    else if (theDV["BusinessRuleId"].ToString() == "26")
                        theDR["Value"] = txtMaxNormalVal.Text;
                    else if (theDV["BusinessRuleId"].ToString() == "27")
                        theDR["Value"] = txtMinNormalVal.Text;
                    else if (theDV["BusinessRuleId"].ToString() == "10" || theDV["BusinessRuleId"].ToString() == "11")
                        theDR["Value"] = ddlRegimenType.SelectedValue;
                    else if (theDV["BusinessRuleId"].ToString() == "16")
                    {
                        theDR["Value"] = txtMinAgeRange.Text;
                        theDR["Value1"] = txtMaxAgeRange.Text;
                    }
                    else if (theDV["BusinessRuleId"].ToString() == "18")
                    {
                        theDR["Value"] = txtDate1.Text;
                    }
                    else if (theDV["BusinessRuleId"].ToString() == "19")
                    {
                        theDR["Value"] = txtDate2.Text +"_"+txtDate1.Text;
                    }
                    else if (theDV["BusinessRuleId"].ToString() == "20")
                        theDR["Value"] = txtNumeric.Text;

                    btable.Rows.Add(theDR);
                }
                if (GblIQCare.objhashBusinessRule.Contains(GblIQCare.gblRowIndex))
                    GblIQCare.objhashBusinessRule[GblIQCare.gblRowIndex] = btable;
                else
                    GblIQCare.objhashBusinessRule.Add(GblIQCare.gblRowIndex, btable);

                GblIQCare.blnBusinessRuleChange = true;
                GblIQCare.dtTempValue = btable.Copy();
                if (GblIQCare.dtTempValue.Rows.Count == 0)
                {
                    GblIQCare.strCount = "Zero";
                }
                this.Close();

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Whenever user check/uncheck the items in the checklistbox this fuction call.
        /// If adding the value (max or min in case of integer) it will check whether it is checked or not if 
        /// it is check text box is visibleto user else not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                //if (e.NewValue == CheckState.Checked)
                //{
                    if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "2")
                    {
                        txtMaxVal.Visible = true;
                        if (GblIQCare.iManageCareEnded == 2)
                        {
                            txtMaxVal.Location = new Point(150, e.Index * 19);
                        }
                        else 
                        {
                            txtMaxVal.Location = new Point(150, e.Index * 17);

                        }
                    }
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "3")
                    {
                        txtMinVal.Visible = true;
                        if (GblIQCare.iManageCareEnded == 2)
                        {
                            txtMinVal.Location = new Point(150, e.Index * 19);
                        }
                        else
                        {
                            txtMinVal.Location = new Point(150, e.Index * 17);

                        }
                    }
                        //todo
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "26")
                    {
                        txtMaxNormalVal.Visible = true;
                        if (GblIQCare.iManageCareEnded == 2)
                        {
                            txtMaxNormalVal.Location = new Point(150, e.Index * 19);
                        }
                        else
                        {
                            txtMaxNormalVal.Location = new Point(150, e.Index * 17);

                        }
                    }
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "27")
                    {
                        txtMinNormalVal.Visible = true;
                        if (GblIQCare.iManageCareEnded == 2)
                        {
                            txtMinNormalVal.Location = new Point(150, e.Index * 19);
                        }
                        else
                        {
                            txtMinNormalVal.Location = new Point(150, e.Index * 17);

                        }
                    }

                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "10" || ((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "11")
                    {
                        ddlRegimenType.Visible = true;
                    }
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "16")
                    {
                        txtMaxAgeRange.Visible = true;
                        txtMinAgeRange.Visible = true;
                       
                    }

                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "20")// This rule for numeric data label
                    {
                        if (e.NewValue == CheckState.Checked)
                        {
                            pnlNumeric.Visible = true;
                            pnlNumeric.BackColor = Color.FromArgb(0, 0, 0, 0);
                            pnlNumeric.Location = new Point(233, e.Index * 14);
                        }
                        else
                        {
                            pnlNumeric.Visible = false;
                        }
                    }
                    
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "18")// This rule for data1 label
                    {
                        if (e.NewValue == CheckState.Checked)
                        {
                            if (!pnlDate1.Visible)
                            {
                                pnlDate1.Visible = true;
                                pnlDate1.BackColor = Color.FromArgb(0, 0, 0, 0);
                                pnlDate1.Location = new Point(232, e.Index * 16);
                            }
                            //if (pnlDate2.Visible)
                            //    pnlDate2.Visible = false;    
                        }
                        else
                        {
                            if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "19")
                                pnlDate1.Visible = true;
                            else
                                pnlDate1.Visible = false;
                            
                        }
                    }
                    else if (((DataRowView)chkLstBox.Items[e.Index]).Row["BusinessRuleId"].ToString() == "19")// This rule for data2 label
                    {
                        if (e.NewValue == CheckState.Checked)
                        {
                            if (pnlDate1.Visible != true)
                            {
                                pnlDate1.Visible = true;
                                pnlDate1.BackColor = Color.FromArgb(0, 0, 0, 0);
                                pnlDate1.Location = new Point(232, e.Index * 13);
                            }
                           
                            pnlDate2.Visible = true;
                            pnlDate2.BackColor = Color.FromArgb(0, 0, 0, 0);
                            pnlDate2.Location = new Point(232, e.Index * 18);
                        }
                        else
                        {
                            pnlDate2.Visible = false;
                            pnlDate1.Visible = false;
                        }
                    }
                //}
        
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        /// <summary>
        /// This function is used to clear the table whenever we edit/add new business rule.
        /// </summary>
        private void ClearTable()
        {
            GblIQCare.dtBusinessValues.Clear();
            GblIQCare.dtBusinessValues.Columns.Clear();
            GblIQCare.dtBusinessValues.Rows.Clear();

        }
        /// <summary>
        /// This function is used to put the validation.
        /// </summary>
        private bool FieldValidation()
        {

            if (GblIQCare.iManageCareEnded == 1)
            {
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(4) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(4) == true))
                {
                    if (txtMaxVal.Text.ToString() == null || txtMaxVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(2) == true))
                //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(5) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(5) == true))
                {
                    if (txtMinVal.Text.ToString() == null || txtMinVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(4) == true) && (chkLstBox.GetItemChecked(5) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(4) == true) && (chkLstBox.GetItemChecked(5) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }

                //below code added on 11Aug2011
                //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(4) == true) && (chkLstBox.GetItemChecked(5) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }

            }

            else if (GblIQCare.iManageCareEnded == 2)
            {
                 if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(1) == true))
                {
                    if (txtMaxVal.Text.ToString() == null || txtMaxVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(2) == true))
                {
                    if (txtMinVal.Text.ToString() == null || txtMinVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(1) == true) && (chkLstBox.GetItemChecked(2) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }

                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(1) == true) && (chkLstBox.GetItemChecked(2) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }

            }
            else 
            {
                //Validation to choose  for Hour Format (either 24 or 12)
                int hrformat = 0;
                for (int i = 0; i < chkLstBox.Items.Count; i++)
                {
                    if (chkLstBox.GetItemChecked(i) && Convert.ToInt32(((System.Data.DataRowView)(chkLstBox.Items[i])).Row.ItemArray[0]) == 22)
                    {
                        hrformat++;
                    }
                    else if (chkLstBox.GetItemChecked(i) && Convert.ToInt32(((System.Data.DataRowView)(chkLstBox.Items[i])).Row.ItemArray[0]) == 23) 
                    {
                        hrformat++;
                    }
                }
                if (hrformat == 2)
                {
                    IQCareWindowMsgBox.ShowWindow("TimeFormat", this);
                    return false;
                }
                 

                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(4) == true))
                
                //todo
                //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(5) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(6) == true))
                {
                    if (txtMaxVal.Text.ToString() == null || txtMaxVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                

                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(2) == true))
                //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(5) == true))
              
                //todo
                //  if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(6) == true))

                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(8) == true))
                {
                    if (txtMinVal.Text.ToString() == null || txtMinVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(4) == true) && (chkLstBox.GetItemChecked(5) == true))
                
                //todo
                //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))

                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(6) == true) && (chkLstBox.GetItemChecked(8) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }

                //below code added on 11Aug2011

                //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
                // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
                //todo
                //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(6) == true) && (chkLstBox.GetItemChecked(8) == true))
                {
                    if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }


                // For Max Normal
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[25]["Id"].ToString() == "26") && (chkLstBox.GetItemChecked(5) == true))
                {
                    if (txtMaxNormalVal.Text.ToString() == null || txtMaxNormalVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                // For Min Normal
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[26]["Id"].ToString() == "27") && (chkLstBox.GetItemChecked(7) == true))
                {
                    if (txtMinNormalVal.Text.ToString() == null || txtMinNormalVal.Text.ToString() == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return false;
                    }

                }
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(7) == true))
                {
                    if (Convert.ToDecimal(txtMinNormalVal.Text) >= Convert.ToDecimal(txtMaxNormalVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                        return false;
                    }

                }
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
                {
                    if (Convert.ToDecimal(txtMaxNormalVal.Text) > Convert.ToDecimal(txtMaxVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMaxNormal", this);
                        return false;
                    }

                }
                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(7) == true) && (chkLstBox.GetItemChecked(8) == true))
                {
                    if (Convert.ToDecimal(txtMinNormalVal.Text) < Convert.ToDecimal(txtMinVal.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMinNormal", this);
                        return false;
                    }

                }

                //  when Max Normal enter and do not enter Min normal and viceversa

                if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox"))
                {
                    if ((chkLstBox.GetItemChecked(6) == true) && (chkLstBox.GetItemChecked(8) == false))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMinMaxSelect", this);
                        return false;
                    }
                    else if ((chkLstBox.GetItemChecked(6) == false) && (chkLstBox.GetItemChecked(8) == true))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMinMaxSelect", this);
                        return false;
                    }
                   else if ((chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(7) == false))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMinMaxNormalSelect", this);
                        return false;
                    }
                    else if ((chkLstBox.GetItemChecked(5) == false) && (chkLstBox.GetItemChecked(7) == true))
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterCorrectMinMaxNormalSelect", this);
                        return false;
                    }
                    else if ((chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(7) == true))
                    {
                        if ((chkLstBox.GetItemChecked(6) == false) || (chkLstBox.GetItemChecked(8) == false))
                        {
                            IQCareWindowMsgBox.ShowWindow("EnterCorrectMinMaxSelect", this);
                            return false;
                        }
                    }                 

                }


            
            }
        //  // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(1) == true))
        //   // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(4) == true))
        //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[1]["Id"].ToString() == "2") && (chkLstBox.GetItemChecked(5) == true))
        //    {
        //        if (txtMaxVal.Text.ToString() == null || txtMaxVal.Text.ToString() == "")
        //        {
        //            IQCareWindowMsgBox.ShowWindow("EnterValue", this);
        //            return false;
        //        }

        //    }
        //   // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(2) == true))
        //    //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(5) == true))
        //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (dsBusinessRule.Tables[0].Rows[2]["Id"].ToString() == "3") && (chkLstBox.GetItemChecked(6) == true))
        //    {
        //        if (txtMinVal.Text.ToString() == null || txtMinVal.Text.ToString() == "")
        //        {
        //            IQCareWindowMsgBox.ShowWindow("EnterValue", this);
        //            return false;
        //        }

        //    }
        ////    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
        //       // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(4) == true) && (chkLstBox.GetItemChecked(5) == true))
        //     if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
        //    {
        //        if (Convert.ToDecimal(txtMinVal.Text) >= Convert.ToDecimal(txtMaxVal.Text))
        //        {
        //            IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
        //            return false;
        //        }

        //    }

                //below code added on 11Aug2011
            //    if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(2) == true) && (chkLstBox.GetItemChecked(1) == true))
           // if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
            //if ((GblIQCare.strDisplayType.ToString() == "Numeric" || GblIQCare.strDisplayType.ToString() == "DecimalTextBox") && (chkLstBox.GetItemChecked(5) == true) && (chkLstBox.GetItemChecked(6) == true))
            //{
            //    if (Convert.ToInt32(txtMinVal.Text) >= Convert.ToInt32(txtMaxVal.Text))
            //    {
            //        IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
            //        return false;
            //    }

            //}






            //below code added on 12may2011
            //if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" || GblIQCare.strDisplayType.ToString() != "Lab Selection") && (dsBusinessRule.Tables[0].Rows[15]["Id"].ToString() == "16") && (chkLstBox.GetItemChecked(0) == true))
            if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" && GblIQCare.strDisplayType.ToString() != "Lab Selection" && GblIQCare.strDisplayType.ToString() != "Regimen") && (chkLstBox.GetItemChecked(0) == true) && GblIQCare.strDisplayType.ToString() != "Time")
            {
                if (txtMaxAgeRange.Text.ToString() == null || txtMaxAgeRange.Text.ToString() == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return false;
                }

            }
            //if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" || GblIQCare.strDisplayType.ToString() != "Lab Selection") && (dsBusinessRule.Tables[0].Rows[15]["Id"].ToString() == "16") && (chkLstBox.GetItemChecked(0) == true))
            if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" && GblIQCare.strDisplayType.ToString() != "Lab Selection" && GblIQCare.strDisplayType.ToString() != "Regimen") && (chkLstBox.GetItemChecked(0) == true) && GblIQCare.strDisplayType.ToString() != "Time")
            {
                if (txtMinAgeRange.Text.ToString() == null || txtMinAgeRange.Text.ToString() == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return false;
                }

            }
            //if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" || GblIQCare.strDisplayType.ToString() != "Lab Selection") && (chkLstBox.GetItemChecked(0) == true))
            if ((GblIQCare.strDisplayType.ToString() != "Drug Selection" && GblIQCare.strDisplayType.ToString() != "Lab Selection" && GblIQCare.strDisplayType.ToString() != "Regimen") && (chkLstBox.GetItemChecked(0) == true) && GblIQCare.strDisplayType.ToString() != "Time")
            {
                if (Convert.ToDecimal(txtMinAgeRange.Text) >= Convert.ToDecimal(txtMaxAgeRange.Text))
                {
                    IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                    return false;
                }

            }

            return true;

        }

        private void frmBusinessRule_FormClosed(object sender, FormClosedEventArgs e)
        {
            GblIQCare.objHashtbl.Clear();
          
        }

        private void txtMaxVal_Validating(object sender, CancelEventArgs e)
        {
           
                try
                {
                    
                    if (txtMaxVal.Text == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return;
                    }


                    float iNumberEntered = float.Parse(txtMaxVal.Text);
                    if (iNumberEntered < 0.0)
                    {
                        e.Cancel = true;
                        IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                        return;
                    }
                }
                catch (Exception err)
                {

                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
         }

        private void txtMinVal_Validating(object sender, CancelEventArgs e)
        {
          
                try
                {
                    if (txtMinVal.Text == "")
                    {
                        IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                        return;
                    }
                    float iNumberEntered = float.Parse(txtMinVal.Text);
                    if (iNumberEntered < 0.0)
                    {
                        e.Cancel = true;
                        IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                        return;
                    }
                }
                catch (Exception err)
                {

                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
         }

        private void txtMinAgeRange_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtMinAgeRange.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtMinAgeRange.Text);
                if (iNumberEntered < 0.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtMaxAgeRange_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtMaxAgeRange.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtMaxAgeRange.Text);
                if (iNumberEntered < 1.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void txtMinNormalVal_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtMinNormalVal.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtMinNormalVal.Text);
                if (iNumberEntered < 0.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtMaxNormalVal_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                if (txtMaxNormalVal.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }


                float iNumberEntered = float.Parse(txtMaxNormalVal.Text);
                if (iNumberEntered < 0.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

    }

    }
