using System;
using System.Data;
using System.Web.UI;
using Application.Common;
using Application.Presentation;

namespace IQCare.Web.Pharmacy
{
    public partial class DrugSelector : BasePage
    {
        private string[] ControlId;
        private string theCntrlName = "";

        #region "User Functions"

        private void BindList()
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV;

            #region "Filter Drug"

            //if(Request.QueryString["DrugType"].ToString()=="37" && Request.QueryString["btnreg"].ToString()=="btnRegimen")
            //{
            //}
            //else{
            theDV = new DataView((DataTable)Session["DrugData"]);//}

            DataTable DTSelected = (DataTable)Session["SelectedData"];

            if (Convert.ToInt32(Session["DrugType"]) > 0)
            {
                theDV.RowFilter = "DrugTypeId = " + Session["DrugType"].ToString();
                //theDV.RowFilter = "DrugTypeId = " + Session["DrugType"].ToString() + " and DrugId <> 1 " ;
            }
            else
            {
                theDV.RowFilter = "DrugTypeId <> 37 ";
                //theDV.RowFilter = "DrugTypeId <> 37  and DrugId <> 1 ";
            }
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);

            #endregion "Filter Drug"

            if (theDT != null)
            {
                DataView theDV1 = new DataView(theDT);
                theDV1.RowFilter = "DrugId <> 203";
                theDV1.Sort = "DrugName Asc";
                theDT = theUtils.CreateTableFromDataView(theDV1);
            }

            if (DTSelected != null && DTSelected.Rows.Count > 0)
            {
                foreach (DataRow dr1 in DTSelected.Rows)
                {
                    foreach (DataRow dr in theDT.Rows)
                    {
                        //string drugName = dr["DrugName"].ToString();
                        if ((Convert.ToInt32(dr1["Generic"]) == Convert.ToInt32(dr["Generic"])) && (Convert.ToInt32(dr1["DrugId"]) == Convert.ToInt32(dr["DrugId"])))
                        {
                            theDT.Rows.Remove(dr);
                            break;
                        }
                    }
                }
            }

            //----rupesh for seperating generic and drugid
            if (theDT != null)
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                    if (Convert.ToInt32(theDR["Generic"]) == 0) // its drug
                    {
                        if (theDR["DrugId"].ToString().LastIndexOf("8888") == -1)
                        {
                            theDR["DrugId"] = theDR["DrugId"].ToString() + "8888";
                            if (theDR["Abbr"].ToString() != "")
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString() + "-[" + theDR["Abbr"].ToString() + "]";
                            }
                            else
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString();
                            }
                        }
                    }
                    else if (Convert.ToInt32(theDR["Generic"]) > 0)  // if generic
                    {
                        if (theDR["DrugId"].ToString().LastIndexOf("9999") == -1)
                        {
                            theDR["DrugId"] = theDR["DrugId"].ToString() + "9999";
                            if (theDR["Abbr"].ToString() != "")
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString() + "-[" + theDR["Abbr"].ToString() + "]";
                            }
                            else
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString();
                            }
                        }
                    }
                }

                Session["DrugTable"] = theDT;

                foreach (DataRow theDR in DTSelected.Rows)
                {
                    if (Convert.ToInt32(theDR["Generic"]) == 0) // its drug
                    {
                        if (theDR["DrugId"].ToString().LastIndexOf("8888") == -1)
                            theDR["DrugId"] = theDR["DrugId"].ToString() + "8888";
                    }
                    else if (Convert.ToInt32(theDR["Generic"]) > 0) // if generic
                    {
                        if (theDR["DrugId"].ToString().LastIndexOf("9999") == -1)
                        {
                            theDR["DrugId"] = theDR["DrugId"].ToString() + "9999";
                            if (theDR["Abbr"].ToString() != "")
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString() + "-[" + theDR["Abbr"].ToString() + "]";
                            }
                            else
                            {
                                theDR["DrugName"] = theDR["DrugName"].ToString();
                            }
                        }
                    }
                }

                //--------------------------------------------
                BindFunctions theBind = new BindFunctions();

                theBind.BindList(lstDrugList, theDT, "DrugName", "DrugId");
                theBind.BindList(lstSelectedDrug, DTSelected, "DrugName", "DrugId");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Page_Load", "fnClose('lstSelectedDrug');", true);
            }
        }

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
            return theDT;
        }

        private void FillIERegimen()
        {
            if (Request.QueryString["btnreg"].ToString() == "btnRegimen")
            {
                Application.Add("AddRegimen", Session["SelectedData"]);
            }
            else if (Request.QueryString["btnreg"].ToString() == "btnRegimen1")
            {
                Application.Add("AddRegimen1", Session["SelectedData"]);
            }
            else if (Request.QueryString["btnreg"].ToString() == "btnRegimen2")
            {
                Application.Add("AddRegimen2", Session["SelectedData"]);
            }
            else if (Request.QueryString["btnreg"].ToString() == "btnRegimen3")
            {
                Application.Add("AddRegimen3", Session["SelectedData"]);
            }
            else if (Request.QueryString["btnreg"].ToString() == "btnRegimen4")
            {
                Application.Add("AddRegimen4", Session["SelectedData"]);
            }
            else
            {
                Application.Add("AddRegimen", Session["SelectedData"]);
            }
        }

        private void Init_Form()
        {
            BindList();
        }

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

        private DataTable PtnRegCTCselectedDataTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            return theDT;
        }

        /********** Fill IE Regimen **********/

        #endregion "User Functions"

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Jayant start 28/11/2008
            if (Session["PtnRegCTC"] != null && Session["PtnRegCTC"].ToString() == "PtnRegCTC")
            {
                RegistrationCTCAddDrug();
            }
            //Jayant End 28/11/2008

            ////Jayant Start 28/11/2010
            else if (Session["CustomfrmDrug"].ToString() == "CustomfrmDrug")
            {
                if (Request.QueryString["BtnDrg"] == "customfrmDrug")
                {
                    int AddDrgtypeID = Convert.ToInt32(Request.QueryString["DrugType"]);
                    CustomFormAddDrug(AddDrgtypeID);
                }
                else if (Request.QueryString["BtnRegimen"] == "customfrmReg")
                {
                    int AddRegtypeID = Convert.ToInt32(Request.QueryString["RegType"]);
                    CustomFormAddRegimen(AddRegtypeID);
                }
            }
            //Jayant End 28/11/2010
            else
            {
                //try
                //{
                DataRow theDR;
                DataTable theDT = (DataTable)Session["SelectedData"];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
                theDR[1] = lstDrugList.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["DrugTable"];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstDrugList.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDR[3] = theDR1[0][3];

                theDT.Rows.Add(theDR);

                lstSelectedDrug.DataSource = theDT;
                lstSelectedDrug.DataBind();
                Session["SelectedData"] = theDT;

                theDT1.Rows.Remove(theDR1[0]);

                //----rupesh for seperating generic and drugid
                foreach (DataRow theDRC in ((DataTable)Session["DrugData"]).Rows)
                {
                    if (Convert.ToInt32(theDRC["Generic"]) == 0) // its drug
                    {
                        if (theDRC["DrugId"].ToString().LastIndexOf("8888") == -1)
                            theDRC["DrugId"] = theDRC["DrugId"].ToString() + "8888";
                    }
                    else if (Convert.ToInt32(theDRC["Generic"]) > 0)// if generic
                    {
                        if (theDRC["DrugId"].ToString().LastIndexOf("9999") == -1)
                            theDRC["DrugId"] = theDRC["DrugId"].ToString() + "9999";
                    }
                }

                //----------------------------------------------

                DataTable theDT2 = (DataTable)Session["DrugData"];
                DataRow[] theDR2 = theDT2.Select("DrugId=" + lstDrugList.SelectedValue);
                theDT2.Rows.Remove(theDR2[0]);
                Session["DrugData"] = theDT2;

                lstDrugList.DataSource = theDT1;
                lstDrugList.DataBind();
                Session["DrugTable"] = theDT1;
                txtSearch.Focus();
                txtSearch_TextChanged(sender, e);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //Jayant Start 29-11-2008
            if (Session["PtnRegCTC"] != null && Session["PtnRegCTC"].ToString() == "PtnRegCTC")
            {
                RegistrationCTCRemoveDrug();
            }
            //Jayant End 29-11-2008

            //Jayant Start 27-11-2010
            else if (Session["CustomfrmDrug"].ToString() == "CustomfrmDrug")
            {
                if (Request.QueryString["BtnDrg"] == "customfrmDrug")
                {
                    int RemoveDrgtypeID = Convert.ToInt32(Request.QueryString["DrugType"]);

                    CustomFormRemoveDrug(RemoveDrgtypeID);
                }
                else if (Request.QueryString["BtnRegimen"] == "customfrmReg")
                {
                    int RemoveRegtypeID = Convert.ToInt32(Request.QueryString["RegType"]);
                    CustomFormRemoveRegimen(RemoveRegtypeID);
                }
            }
            //Jayant End 27-11-2010
            else
            {
                try
                {
                    DataRow theDR;
                    DataTable theDT = (DataTable)Session["DrugTable"];
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                    theDR[1] = lstSelectedDrug.SelectedItem.Text;
                    DataTable theDT1 = (DataTable)Session["SelectedData"];
                    DataRow[] theDR1 = theDT1.Select("DrugId=" + lstSelectedDrug.SelectedValue);
                    theDR[2] = theDR1[0][2];
                    theDR[3] = theDR1[0][3];
                    theDT.Rows.Add(theDR);

                    DataTable theDT2 = (DataTable)Session["DrugData"];
                    DataRow theDR2 = theDT2.NewRow();
                    theDR2[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                    theDR2[1] = lstSelectedDrug.SelectedItem.Text;
                    theDR2[2] = theDR1[0][2];
                    theDR2[3] = theDR1[0][3];
                    theDT2.Rows.Add(theDR2);
                    Session["DrugData"] = theDT2;

                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    lstDrugList.DataSource = theDT;
                    lstDrugList.DataBind();
                    Session["DrugTable"] = theDT;

                    theDT1.Rows.Remove(theDR1[0]);
                    lstSelectedDrug.DataSource = theDT1;
                    lstSelectedDrug.DataBind();
                    Session["SelectedData"] = theDT1;
                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["PtnRegCTC"] != null && Session["PtnRegCTC"].ToString() == "PtnRegCTC")
            {
                Application.Add("RegimenCTCptnReg", Session["SelectedData"]);
                Session.Remove("PtnRegCTC");
                ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script>window.opener.GetControl();closeMe();</script>");
            }
            if (Session["CustomfrmDrug"]!=null && Session["CustomfrmDrug"].ToString() == "CustomfrmDrug")
            {
                if (Request.QueryString["BtnRegimen"] == "customfrmReg")
                {
                    int RegimenType = Convert.ToInt32(Request.QueryString["RegType"]);
                    if (Request.QueryString["Cntrl"].ToString() != "")
                    {
                        theCntrlName = Request.QueryString["Cntrl"].ToString();
                    }
                    string Reg = ReturnRegimen(RegimenType);
                    ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script language='javascript' type='text/javascript'>fnset('" + theCntrlName + "','" + Reg + "');closeMe();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script>window.opener.GetControl();closeMe();</script>");
                }
            }
            else
            {
                //----rupesh for seperating generic and drugid
                foreach (DataRow theDR in ((DataTable)Session["SelectedData"]).Rows)
                {
                    if (theDR["DrugId"].ToString().LastIndexOf("8888") > 0) //--- if '8888' is added at the end of id - drug
                    {
                        theDR["DrugId"] = theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4);
                    }
                    if (theDR["DrugId"].ToString().LastIndexOf("9999") > 0) //--- if '9999' is added at the end of id  - generic
                    {
                        theDR["DrugId"] = theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4);
                    }
                }

                //--------- rupesh 27Dec07----------
                foreach (DataRow theDR in ((DataTable)Session["DrugData"]).Rows)
                {
                    if (theDR["DrugId"].ToString().LastIndexOf("8888") > 0) //--- if '8888' is added at the end of id - drug
                    {
                        theDR["DrugId"] = theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4);
                    }
                    if (theDR["DrugId"].ToString().LastIndexOf("9999") > 0) //--- if '9999' is added at the end of id  - generic
                    {
                        theDR["DrugId"] = theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4);
                    }
                }

                //----------------------------------------------

                if (Request.QueryString["btnreg"] != null)
                {
                    FillIERegimen();
                }
                else
                {
                    if (Convert.ToInt32(Session["DrugType"]) == 37)
                    {
                        Application.Add("AddARV", Session["SelectedData"]);
                    }
                    else if (Convert.ToInt32(Session["DrugType"]) == 31)
                    {
                        Application.Add("AddTB", Session["SelectedData"]);
                    }
                    else
                    {
                        Application.Add("OtherDrugs", Session["SelectedData"]);
                    }
                }

                Application["MasterData"] = Session["DrugData"];
                string theScript;
                theScript = "<script language='javascript'id='DrgPopup'>\n";
                theScript += "window.opener.GetControl();\n";
                theScript += "window.close();\n";
                theScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Done", theScript);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack != true)
            {
                Session["DrugData"] = (DataTable)Application["MasterData"];

                #region CTC

                //Jayant start 27/11/2008
                if (Session["PtnRegCTC"] != null)
                {
                    if (Session["PtnRegCTC"].ToString() == "PtnRegCTC")
                    {
                        Session["DrugDataPtnReg"] = (DataTable)Session["MasterData"];

                        if ((DataTable)Session["SelectedData"] != null)
                        {
                            ViewState["SelectedDrug"] = Session["SelectedData"];
                        }
                        else
                        {
                            ViewState["SelectedDrug"] = PtnRegCTCselectedDataTable();
                        }
                        RegistrationCTC();
                    }
                }
                //Jayant End 27/11/2008

                #endregion CTC

                //Jayant Start 27/11/2010
                //if (Session["CustomfrmDrug"].ToString() == "CustomfrmDrug")
                //{
                if (Request.QueryString["BtnDrg"] == "customfrmDrug")
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    int DrgtypeID = Convert.ToInt32(Request.QueryString["DrugType"].ToString());
                    //Session["" + DrgtypeID + ""] = Session["" + DrgtypeID + ""];
                    if ((DataTable)Session["Selected" + DrgtypeID + ""] == null)
                    {
                        Session["Selected" + DrgtypeID + ""] = PtnCustomformselectedDataTable();
                    }
                    CustomFormDrug(DrgtypeID);
                }
                else if (Request.QueryString["BtnRegimen"] == "customfrmReg")
                {
                    if (Request.QueryString["Cntrl"].ToString() != "")
                    {
                        theCntrlName = Request.QueryString["Cntrl"].ToString();
                        ControlId = theCntrlName.Split('-');
                        if (ControlId[3] == "DTL_CUSTOMFORM")
                        {
                            ControlId = ControlId[4].ToString().Split('=');
                        }
                        else
                        {
                            ControlId = ControlId[3].ToString().Split('=');
                        }
                        ViewState["ControlId"] = ControlId[0].ToString(); ;
                    }
                    IQCareUtils theUtils = new IQCareUtils();
                    int RegtypeID = Convert.ToInt32(Request.QueryString["RegType"].ToString());
                    //Session["Reg"+ViewState["ControlId"].ToString() + RegtypeID + ""] = Session["Reg"+ViewState["ControlId"].ToString() + RegtypeID + ""];
                    if ((DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + RegtypeID + ""] == null)
                    {
                        Session["SelectedReg" + ViewState["ControlId"].ToString() + RegtypeID + ""] = PtnCustomformselectedDataTable();
                    }
                    CustomFormRegimen(RegtypeID);
                }

            //}

            //Jayant End 27/11/2011
                else
                {
                    DataTable ds = (DataTable)Application["SelectedDrug"];
                    if ((DataTable)Application["SelectedDrug"] != null && ds.Rows.Count > 0)
                    {
                        Session["SelectedData"] = Application["SelectedDrug"];
                    }
                    else
                    {
                        Session["SelectedData"] = CreateSelectedTable();
                    }

                    Session["DrugType"] = Request.QueryString["DrugType"].ToString();
                    Application.Remove("MasterData");
                    Application.Remove("SelectedDrug");
                    txtSearch.Attributes.Add("onKeyUp", ClientScript.GetPostBackEventReference(txtSearch, "txtSearch_TextChanged"));
                    Init_Form();
                }
                btnAdd.Attributes.Add("onClick", "return listBox_selected('lstDrugList')");
                btnRemove.Attributes.Add("onClick", "return listBox_selected('lstSelectedDrug')");

                if (Convert.ToInt32(Session["DrugType"]) == 37)
                {
                    lblHeader.Text = "ARV Medication";
                }
                else if (Convert.ToInt32(Session["DrugType"]) == 31)
                {
                    lblHeader.Text = "TB Medication";
                }
                else
                {
                    lblHeader.Text = "OI Treatment and Other Medications";
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (Session["PtnRegCTC"] != null && Session["PtnRegCTC"].ToString() == "PtnRegCTC")
            {
                DataView theDV = new DataView((DataTable)Session["DrugDataPtnReg"]);
                theDV.RowFilter = "DrugName like '" + txtSearch.Text + "%'";
                IQCareUtils theUtil = new IQCareUtils();
                BindFunctions theBind = new BindFunctions();
                theBind.BindList(lstDrugList, theUtil.CreateTableFromDataView(theDV), "DrugName", "DrugId");
            }
            else
            {
                DataView theDV = new DataView((DataTable)Session["DrugTable"]);
                theDV.RowFilter = "DrugName like '" + txtSearch.Text + "%'";
                IQCareUtils theUtil = new IQCareUtils();
                BindFunctions theBind = new BindFunctions();
                theBind.BindList(lstDrugList, theUtil.CreateTableFromDataView(theDV), "DrugName", "DrugId");
            }
            //txtSearch.Focus();
        }

        //Custom Form Add Drug
        private void CustomFormAddDrug(int AddDrgtypeID)
        {
            BindFunctions theBind = new BindFunctions();
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)Session["Selected" + AddDrgtypeID + ""];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
                theDR[1] = lstDrugList.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["" + AddDrgtypeID + ""];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstDrugList.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDR[3] = AddDrgtypeID;//theDR1[0][3];
                theDR[5] = 0;
                theDT.Rows.Add(theDR);
                theBind.BindList(lstSelectedDrug, theDT, "DrugName", "DrugId");
                Session["Selected" + AddDrgtypeID + ""] = theDT;
                theDT1.Rows.Remove(theDR1[0]);
                theBind.BindList(lstDrugList, theDT1, "DrugName", "DrugId");
                Session["" + AddDrgtypeID + ""] = theDT1;
            }
            catch
            {
            }
            finally { }
        }

        //Custom Form Add Regimen
        private void CustomFormAddRegimen(int AddRegTypeID)
        {
            BindFunctions theBind = new BindFunctions();
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + AddRegTypeID + ""];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
                theDR[1] = lstDrugList.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["Reg" + ViewState["ControlId"].ToString() + AddRegTypeID + ""];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstDrugList.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDR[4] = theDR1[0][4];
                theDT.Rows.Add(theDR);
                theBind.BindList(lstSelectedDrug, theDT, "DrugName", "DrugId");
                Session["SelectedReg" + ViewState["ControlId"].ToString() + AddRegTypeID + ""] = theDT;
                theDT1.Rows.Remove(theDR1[0]);
                theBind.BindList(lstDrugList, theDT1, "DrugName", "DrugId");
                Session["Reg" + ViewState["ControlId"].ToString() + AddRegTypeID + ""] = theDT1;
            }
            catch
            {
            }
            finally { }
        }

        private void CustomFormDrug(int DrugTypeID)
        {
            BindFunctions theBind = new BindFunctions();
            DataTable theDTCustomFrmDrug = (DataTable)Session["" + DrugTypeID + ""];
            DataTable theDTCustomFrmSelectedDrug = (DataTable)Session["Selected" + DrugTypeID + ""];
            theBind.BindList(lstDrugList, theDTCustomFrmDrug, "DrugName", "DrugId");
            theBind.BindList(lstSelectedDrug, theDTCustomFrmSelectedDrug, "DrugName", "DrugId");
        }

        private void CustomFormRegimen(int RegTypeID)
        {
            BindFunctions theBind = new BindFunctions();
            DataTable theDTCustomFrmReg = (DataTable)Session["Reg" + ViewState["ControlId"].ToString() + RegTypeID + ""];
            DataTable theDTCustomFrmSelectedReg = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + RegTypeID + ""];
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
                //////Sanjay-05-07-2012///////
                DataView theSelectedFilter = new DataView(theDTCustomFrmReg);
                theSelectedFilter.RowFilter = "Generic <>" + theSelectedDR["Generic"].ToString();
                theDTCustomFrmReg = theSelectedFilter.ToTable();
                //////////////////////////////
            }
            theBind.BindList(lstDrugList, theDTCustomFrmReg, "DrugName", "DrugId");
            theBind.BindList(lstSelectedDrug, theDTCustomFrmSelectedReg, "DrugName", "DrugId");
        }

        private void CustomFormRemoveDrug(int RemoveDrgtypeID)
        {
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)Session["" + RemoveDrgtypeID + ""]; ;
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                theDR[1] = lstSelectedDrug.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["Selected" + RemoveDrgtypeID + ""];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstSelectedDrug.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                Session.Remove("" + RemoveDrgtypeID + "");
                Session["" + RemoveDrgtypeID + ""] = theDT;
                lstDrugList.DataSource = theDT;
                lstDrugList.DataBind();
                theDT1.Rows.Remove(theDR1[0]);
                if (theDT1.Rows.Count > 0)
                {
                    Session["Selected" + RemoveDrgtypeID + ""] = theDT1;
                }
                lstSelectedDrug.DataSource = theDT1;
                lstSelectedDrug.DataBind();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void CustomFormRemoveRegimen(int RemoveRegTypeID)
        {
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)Session["Reg" + ViewState["ControlId"].ToString() + RemoveRegTypeID + ""];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                theDR[1] = lstSelectedDrug.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + RemoveRegTypeID + ""];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstSelectedDrug.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                Session.Remove("Reg" + RemoveRegTypeID + "");
                Session["Reg" + ViewState["ControlId"].ToString() + RemoveRegTypeID + ""] = theDT;
                lstDrugList.DataSource = theDT;
                lstDrugList.DataBind();

                theDT1.Rows.Remove(theDR1[0]);
                if (theDT1.Rows.Count > 0)
                {
                    Session["SelectedReg" + ViewState["ControlId"].ToString() + RemoveRegTypeID + ""] = theDT1;
                }
                lstSelectedDrug.DataSource = theDT1;
                lstSelectedDrug.DataBind();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        //This function is Specially For Dynamic Form.
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

        private void RegistrationCTC()
        {
            BindFunctions theBind = new BindFunctions();
            DataTable theDTDrug = (DataTable)Session["DrugDataPtnReg"];
            DataTable theDTSelectedDrug = (DataTable)Session["SelectedData"];
            theBind.BindList(lstDrugList, theDTDrug, "DrugAbbName", "DrugId");
            theBind.BindList(lstSelectedDrug, theDTSelectedDrug, "DrugName", "DrugId");
        }

        private void RegistrationCTCAddDrug()
        {
            BindFunctions theBind = new BindFunctions();

            DataRow theDR;
            DataTable theDT = (DataTable)ViewState["SelectedDrug"];
            theDR = theDT.NewRow();
            theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
            theDR[1] = lstDrugList.SelectedItem.Text;
            DataTable theDT1 = (DataTable)Session["DrugDataPtnReg"];
            DataRow[] theDR1 = theDT1.Select("DrugId=" + lstDrugList.SelectedValue);
            theDR[2] = theDR1[0][2];
            theDT.Rows.Add(theDR);
            theBind.BindList(lstSelectedDrug, theDT, "DrugName", "DrugId");
            Session["SelectedData"] = theDT;
            theDT1.Rows.Remove(theDR1[0]);
            theBind.BindList(lstDrugList, theDT1, "DrugAbbName", "DrugId");
            Session["DrugDataPtnReg"] = theDT1;
            // txtSearch_TextChanged(sender, e);
        }

        private void RegistrationCTCRemoveDrug()
        {
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)Session["DrugDataPtnReg"];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                theDR[1] = lstSelectedDrug.SelectedItem.Text;
                DataTable theDT1 = (DataTable)Session["SelectedData"];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstSelectedDrug.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                Session.Remove("DrugDataPtnReg");
                Session["DrugDataPtnReg"] = theDT;
                lstDrugList.DataSource = theDT;
                lstDrugList.DataBind();

                theDT1.Rows.Remove(theDR1[0]);
                Session.Remove("SelectedData");
                Session["SelectedData"] = theDT1;
                ViewState["SelectedDrug"] = Session["SelectedData"];
                lstSelectedDrug.DataSource = theDT1;
                lstSelectedDrug.DataBind();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private string ReturnRegimen(int RegtypeID)
        {
            string theStr = "";
            if (Session["SelectedReg" + ViewState["ControlId"].ToString() + RegtypeID + ""] != null)
            {
                DataTable theDT = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + RegtypeID + ""];
                theStr = FillRegimen(theDT);
            }
            return theStr;
        }
    }
}