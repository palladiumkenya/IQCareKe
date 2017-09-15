using System;
using System.Data;
using System.Web.UI;
using System.Linq;
using Application.Common;
using Application.Presentation;
using System.Collections.Generic;
using Entities.Lab;
using Interface.Laboratory;
using System.Reflection;
namespace IQCare.Web.Laboratory
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LabSelector : System.Web.UI.Page
    {
        #region "User Functions"
        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            //BindList();
            PopulateList();
        }
       
        void PopulateList()
        {
           lstSelectedLab.DataTextField = "Name";
            lstSelectedLab.DataValueField = "Id";
            lstSelectedLab.DataSource = SelectedLabTestData;
            lstSelectedLab.DataBind();

            List<LabTest> allTests = this.LabTestData;            
            lstLabList.DataTextField = "Name";
            lstLabList.DataValueField = "Id";
            if (null == this.SelectedLabTestData)
            {
                lstLabList.DataSource = allTests.Where(lt=> txtSearch.Text=="" || lt.Name.Contains(txtSearch.Text));
            }
            else
            {
                lstLabList.DataSource = allTests.Except(this.SelectedLabTestData)
                    .Where(lt => txtSearch.Text == "" || lt.Name.Contains(txtSearch.Text));
            }
            lstLabList.DataBind();
        }
        /// <summary>
        /// Binds the list.
        /// </summary>
        private void BindList()
        {
            DataTable theDT = new DataTable();
            DataTable theTmpDT = new DataTable();
            
            DataTable DTSelected = new DataTable();
            if (ViewState["SelectedLab"] == null)
            {
                DTSelected.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
                DTSelected.Columns.Add("LabName", System.Type.GetType("System.String"));
                DTSelected.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
                DTSelected.Columns.Add("SubTestName", System.Type.GetType("System.String"));
                DTSelected.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
                DTSelected.Columns.Add("Flag", System.Type.GetType("System.Int32"));
                DTSelected.Constraints.Add("Con1", DTSelected.Columns["SubTestID"], true);
                ViewState["SelectedLab"] = DTSelected;
            }
            DTSelected = (DataTable)ViewState["SelectedLab"];
            IQCareUtils theUtils = new IQCareUtils();

            if (ViewState["LabData"] != null)
            {
                DataView theDV = new DataView((DataTable)ViewState["LabData"]);
                #region "14-jun-07 - 1"
                // theDV.RowFilter = "LabTypeId=1";
                if (Request.QueryString["Mode"] == "Add")
                { theDV.RowFilter = "LabTypeId=1 and DeleteFlag = 0"; }
                else if (Request.QueryString["Mode"] == "All")
                { theDV.RowFilter = "DeleteFlag = 0"; }
                else
                { theDV.RowFilter = "LabTypeId=1"; }
                #endregion

                theTmpDT = theUtils.CreateTableFromDataView(theDV);
                theDT = theTmpDT;
            }
            DataView theDV1 = new DataView(theDT);
            theDV1.Sort = "SubTestName Asc";
            theDT = theUtils.CreateTableFromDataView(theDV1);

            BindFunctions theBind = new BindFunctions();
            theBind.BindList(lstSelectedLab, DTSelected, "SubTestName", "SubTestID");
            //ajay changes begin
            for (int j = 0; j < lstSelectedLab.Items.Count; j++)
            {
                DataRow[] theDR = theDT.Select("SubTestID=" + lstSelectedLab.Items[j].Value);
                if (theDR.Length != 0)
                    theDT.Rows.Remove(theDR[0]);
            }
            //ajay changes end
            ViewState["LabData"] = theDT;
            theBind.BindList(lstLabList, theDT, "SubTestName", "SubTestId");

        }
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack != true)
            {
                ViewState["SelectedLab"] = (DataTable)Session["SelectedData"];
               // ViewState["LabData"] = (DataTable)Session["MasterData"];
                txtSearch.Attributes.Add("onKeyUp", ClientScript.GetPostBackEventReference(txtSearch, "txtSearch_TextChanged"));
                Init_Form();
                if (Convert.ToString(Session["CustomfrmLab"]) != "CustomfrmLab")
                {
                    Session.Remove("MasterData");
                    Session.Remove("SelectedData");
                }
            }
            btnAdd.Attributes.Add("onClick", "return listBox_selected('lstLabList')");
            btnRemove.Attributes.Add("onClick", "return listBox_selected('lstSelectedLab')");
        }
        ILabManager rMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
        private List<LabTest> SelectedLabTestData
        {
            get
            {
                if (Session["SelectedLabTestData"] == null)
                {
                    return  null;

                }
                return (Session["SelectedLabTestData"]) as List<LabTest>;
            }
            set
            {
                Session["SelectedLabTestData"] = value;
            }
        }
        private List<LabTest> LabTestData
        {
            get
            {
                if (Session["LabTestData"] == null)
                {
                    Session["LabTestData"] = rMgr.GetLabTests().Where(l=> l.Active==true);
                  
                }
                return (Session["LabTestData"]) as List<LabTest>;
            }
            set
            {
                Session["LabTestData"] = value;
            }
        }
        /// <summary>
        /// Makes the names table.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeNamesTable()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Names");

            // Add three column objects to the table.
            DataColumn idColumn = new DataColumn();
            idColumn.DataType = System.Type.GetType("System.Int32");
            idColumn.ColumnName = "id";
            namesTable.Columns.Add(idColumn);

            // Return the new DataTable.
            return namesTable;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //DataView theDV = new DataView((DataTable)ViewState["LabData"]);
            //theDV.RowFilter = "SubTestName like '" + txtSearch.Text + "%'";
            //IQCareUtils theUtil = new IQCareUtils();
            //BindFunctions theBind = new BindFunctions();
            //theBind.BindList(lstLabList, theUtil.CreateTableFromDataView(theDV), "SubTestName", "SubTestID");
            lstLabList.DataTextField = "Name";
            lstLabList.DataValueField = "Id";
            lstLabList.DataSource = this.LabTestData
                .Where(lt => lt.Active == true && lt.Name.Contains(txtSearch.Text))
                .Except(this.SelectedLabTestData);
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
          /*
           * DataSet theDS = new DataSet();
            DataTable DT1 = ((DataTable)ViewState["LabData"]).Copy();
            DataTable DT2 = ((DataTable)ViewState["SelectedLab"]).Copy();
            DT1.TableName = "DT1";
            DT2.TableName = "DT2";
            theDS.Tables.Add(DT1);
            theDS.Tables.Add(DT2);

            Session["AddLab"] = theDS;*/
            Session["AddLab"] = this.SelectedLabTestData;
            Session["LabTestData"] = Session["SelectedLabTestData"] = null;
            string theScript;
            theScript = "<script language='javascript' id='LabPopup'>\n";
            theScript += "window.opener.GetControl();\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(),"Done", theScript);
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int testId = Convert.ToInt32(lstLabList.SelectedValue);

              LabTest thisTest =  this.LabTestData.DefaultIfEmpty(null).FirstOrDefault(lt => lt.Id == testId);
                List<LabTest> _selected;
                if (null == this.SelectedLabTestData)
                {
                    _selected = new List<LabTest>();
                    _selected.Add(thisTest);
                }
                else
                {
                    _selected = this.SelectedLabTestData;
                    if (!_selected.Exists(lt => lt.Id == testId))
                    {
                        _selected.Add(thisTest);
                    }
                }
                this.SelectedLabTestData = _selected;
                PopulateList();
                /*DataTable theDT = (DataTable)ViewState["SelectedLab"];
                DataView theDV = new DataView((DataTable)ViewState["LabData"]);
                theDV.RowFilter = "SubTestId = " + lstLabList.SelectedValue;
                DataRow theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);
                theDR[1] = theDV[0][1].ToString();
                theDR[2] = Convert.ToInt32(theDV[0][2]);
                theDR[3] = theDV[0][3].ToString();
                theDR[4] = Convert.ToInt32(theDV[0][4]);

                theDT.Rows.Add(theDR);
                lstSelectedLab.DataSource = theDT;
                lstSelectedLab.DataBind();
                ViewState["SelectedLab"] = theDT;

                DataTable theDT1 = (DataTable)ViewState["LabData"];
                DataRow[] theDR1 = theDT1.Select("SubTestId=" + lstLabList.SelectedValue);
                theDT1.Rows.Remove(theDR1[0]);
                lstLabList.DataSource = theDT1;
                lstLabList.DataBind();
                ViewState["LabData"] = theDT1;
                txtSearch.Focus();
                txtSearch_TextChanged(sender, e);*/
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int testId = Convert.ToInt32(lstSelectedLab.SelectedValue);
                List<LabTest> _selected = this.SelectedLabTestData;
                LabTest thisTest = _selected.DefaultIfEmpty(null).FirstOrDefault(lt => lt.Id == testId);
                _selected.Remove(thisTest);
                this.SelectedLabTestData = _selected;
                PopulateList();
                /*DataTable theDT = (DataTable)ViewState["LabData"];
                DataView theDV = new DataView((DataTable)ViewState["SelectedLab"]);
                theDV.RowFilter = "SubTestId = " + lstSelectedLab.SelectedValue;

                DataRow theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);
                theDR[1] = theDV[0][1].ToString();
                theDR[2] = Convert.ToInt32(theDV[0][2]);
                theDR[3] = theDV[0][3].ToString();
                theDR[4] = Convert.ToInt32(theDV[0][4]);


                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                theDV = theUtils.GridSort(theDT, "SubTestName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                lstLabList.DataSource = theDT;
                lstLabList.DataBind();
                ViewState["LabData"] = theDT;

                DataTable theDT1 = (DataTable)ViewState["SelectedLab"];
                DataRow[] theDR1 = theDT1.Select("SubTestID=" + lstSelectedLab.SelectedValue);
                theDT1.Rows.Remove(theDR1[0]);
                lstSelectedLab.DataSource = theDT1;
                lstSelectedLab.DataBind();
                ViewState["SelectedLab"] = theDT1;*/
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='LabPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "LabPopup", theScript);
        }
    }
}