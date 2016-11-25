using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
namespace IQCare.Web.Pharmacy
{
    public partial class PharmacySelector : System.Web.UI.Page
    {
        DataView theDV = new DataView();
        DataTable DTtestDate = new DataTable();
        //string theCntrlName = "";
        // string[] ControlId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DrugType"] = Request.QueryString["DrugType"].ToString();
                Session["DrugData"] = (DataTable)Application["MasterData"];
                Session["DrugTable"] = (DataTable)Application["MasterData"];
                DataTable ds = (DataTable)Application["SelectedDrug"];
                if ((DataTable)Application["SelectedDrug"] != null && ds.Rows.Count > 0)
                {
                    Session["SelectedData"] = Application["SelectedDrug"];
                }
                else
                {
                    Session["SelectedData"] = CreateSelectedTable();
                }
                Application.Remove("MasterData");
                Application.Remove("SelectedDrug");

                Init_Form();
            }
        }
        private void BindControls()
        {
            BindFunctions theBindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            theDV = new DataView((DataTable)Session["DrugTable"]);
            DataTable DTSelected = (DataTable)Session["SelectedData"];

            if (Convert.ToInt32(Session["DrugType"]) > 0)
            {
                if (Convert.ToInt32(Session["DrugType"]) == 100)
                {
                    theDV.RowFilter = "DrugTypeId NOT IN (37,36,31) and Generic=0";
                }
                else
                {
                    //theDV.RowFilter = "DrugTypeId = " + Session["DrugType"].ToString();
                    theDV.RowFilter = "DrugTypeId = " + Session["DrugType"].ToString() + " and Generic=0 ";
                }
                theDV.Sort = "DrugName Asc";
            }
            else
            {
                //theDV.RowFilter = "DrugTypeId <> 37 ";
                theDV.RowFilter = "DrugTypeId <> 37  and DrugId <> 1  and Generic=0 ";
            }
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);

            //if (theDT != null)
            //{
            //    DataView theDV1 = new DataView(theDT);
            //    theDV1.RowFilter = "DrugId <> 203";
            //    theDV1.Sort = "DrugName Asc";
            //    theDT = theUtils.CreateTableFromDataView(theDV1);
            //}


            if (theDT.Rows.Count > 0)
            {
                theBindManager.BindCheckedList(chkPharmacyselect, theDT, "DrugName", "DrugId");
                theDV.Dispose();
                theDT.Clear();
            }


            if (DTSelected != null && DTSelected.Rows.Count > 0)
            {
                foreach (DataRow dr1 in DTSelected.Rows)
                {
                    foreach (ListItem item in this.chkPharmacyselect.Items)
                    //foreach (DataRow dr in theDT.Rows)
                    {
                        //string drugName = dr["DrugName"].ToString();
                        if (Convert.ToInt32(dr1["DrugId"]) == Convert.ToInt32(item.Value))
                        {
                            //theDT.Rows.Remove(dr);
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }



        #region "User Functions"

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
            //theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
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


        private void Init_Form()
        {
            BindControls();
        }



        /********** Fill IE Regimen **********/
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
        #endregion

        protected void btnsave_Click(object sender, EventArgs e)
        {
            DataTable theDT1 = (DataTable)Session["DrugData"];
            DataTable theDT = (DataTable)Session["SelectedData"];
            foreach (ListItem li in chkPharmacyselect.Items)
            {
                if (li.Selected == true)
                {
                    DataRow theDR;
                    //theDT = (DataTable)Session["SelectedData"];
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(li.Value);
                    theDR[1] = li.Text;
                    theDT1 = (DataTable)Session["DrugData"];
                    DataRow[] theDR1 = theDT1.Select("DrugId=" + li.Value);
                    theDR[2] = theDR1[0][2];
                    theDR[3] = theDR1[0][3];
                    if (((DataTable)Session["SelectedData"]).Rows.Count > 0)
                    {
                        DataRow[] theFilDR = ((DataTable)Session["SelectedData"]).Select("DrugId=" + li.Value);

                        if (theFilDR.Length == 0)
                        {
                            theDT.Rows.Add(theDR);
                        }


                        //theDT1.Rows.Remove(theDR1[0]);


                    }
                    else
                    {
                        theDT.Rows.Add(theDR);
                    }


                }
                else
                {
                    if (((DataTable)Session["SelectedData"]).Rows.Count > 0)
                    {
                        DataRow[] theFilDR = ((DataTable)Session["SelectedData"]).Select("DrugId=" + li.Value);

                        if (theFilDR.Length > 0)
                        {
                            theDT.Rows.Remove(theFilDR[0]);
                        }
                    }
                }
            }
            Session["SelectedData"] = theDT;
            //Session["DrugData"] = theDT1;

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
                else if (Convert.ToInt32(Session["DrugType"]) == 100)
                {
                    Application.Add("OtherDrugs", Session["SelectedData"]);

                }
                else if (Convert.ToInt32(Session["DrugType"]) == 36)
                {
                    Application.Add("OIDrugs", Session["SelectedData"]);

                }
            }

            Application["MasterData"] = Session["DrugData"];
            //Application["SelectedDrug"] = Session["SelectedData"];
            string theScript;
            theScript = "<script language='javascript'id='DrgPopup'>\n";
            theScript += "window.opener.GetControl();\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Done", theScript);

        }
        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }
}