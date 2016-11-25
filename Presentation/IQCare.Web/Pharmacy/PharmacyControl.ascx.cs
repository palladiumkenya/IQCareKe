using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Pharmacy;
using Application.Presentation;
using System.Data;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using Telerik.Web.UI;

namespace IQCare.Web.Pharmacy
{
    public partial class PharmacyControl : System.Web.UI.UserControl
    {
        #region ClientScripts
        /// <summary>
        /// Gets the drug selected script.
        /// </summary>
        /// <value>
        /// The drug selected script.
        /// </value>
        string DrugSelectedScript
        {
            get
            {
                return @"function ace1_itemSelected(source, e) {
            var results = eval('(' + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {               
                var hdCustID = $get('" + hdCustID.ClientID + @"');
                hdCustID.value = results.DrugId;
            }
        }";
            }
        }
        /// <summary>
        /// Gets the drug populated script.
        /// </summary>
        /// <value>
        /// The drug populated script.
        /// </value>
        string DrugPopulatedScript
        {
            get
            {
                return @"function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                var div = document.createElement(""span"");
                var results = eval('(' + propertyPeople[i]._value + ')');
                div.innerHTML = ""<span style=' float:right; font-weight:bold;margin-right: 5px;'> "" + results.AvlQty + ""</span>"";
                //div.innerHTML = results.AvlQty;
                propertyPeople[i].appendChild(div);""
            }  }";
            }
        }

        /// <summary>
        /// Gets the calculate total dose script.
        /// </summary>
        /// <value>
        /// The calculate total dose script.
        /// </value>
        string CalculateTotalDoseScript
        {
            get
            {
                return @" function CalculateTotalDailyDose(){  
                var varduration = $find("+textDuration.ClientID+ @").value;
                var vardose = $find("+textDose.ClientID +@").value;var cb = $find("+ ddlFrequency.ClientID + @"); var item = cb.get_selectedItem(); 
                var multiplier = item.get_attributes().getAttribute('multiplier')
                if (multiplier != ""0"" && vardose != """" && varduration != """") {
                var totalDose = vardose * varduration * result;   $find("+textQuantityPrescribed.ClientID+@").value = totalDose;}";
            }
        }

        #endregion
        
        #region PageEvents
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(aceSearchDrugs, aceSearchDrugs.GetType(), "DrugControl", this.DrugSelectedScript + this.DrugPopulatedScript, true);
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
        } 
        #endregion

        #region EventHandlers
        /// <summary>
        /// Handles the Click event of the buttonAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemCommand event of the rptDrugs control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void rptDrugs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemDataBound event of the rptDrugs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptDrugs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemDataBound event of the ddlFrequency control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void ddlFrequency_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            try
            {
               
                e.Item.Attributes.Clear();
                e.Item.Text = ((DataRowView)e.Item.DataItem)["FrequencyName"].ToString();
                e.Item.Attributes.Add("multiplier", ((DataRowView)e.Item.DataItem)["Multiplier"].ToString());               
                e.Item.Value = ((DataRowView)e.Item.DataItem)["FrequencyId"].ToString();
            }
            catch 
            {
              
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtautoDrugName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtautoDrugName_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlTreatment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlTreatment_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 
        #endregion

        #region "Drug search"
     
        /// <summary>
        /// Searches the drugs.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchDrugs(string prefixText, int count)
        {
            List<string> Drugsdetail = new List<string>();
            List<Drugs> lstDrugsDetail = GetDrugs(prefixText, count);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (Drugs c in lstDrugsDetail)
            {
                Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.DrugName, serializer.Serialize(c)));
            }

            return Drugsdetail;

        }
/// <summary>
/// Internal class
/// </summary>
        public class Drugs
        {
        
/// <summary>
/// Gets or sets the drug identifier.
/// </summary>
/// <value>
/// The drug identifier.
/// </value>
            public int DrugId
            {
                get ;
                set ;
            }           

/// <summary>
/// Gets or sets the available quantity.
/// </summary>
/// <value>
/// The available quantity.
/// </value>
            public int AvailableQuantity
            {
                get;set;
            }
            /// <summary>
            /// Gets or sets the name of the drug.
            /// </summary>
            /// <value>
            /// The name of the drug.
            /// </value>
            public string DrugName
            {
                get;
                set;
            }


        }
        /// <summary>
        /// Gets the drugs.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static List<Drugs> GetDrugs(string prefixText, int count)
        {
            List<Drugs> items = new List<Drugs>();
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            string sqlQuery;
            //creating Sql Query
            if (HttpContext.Current.Session["Paperless"].ToString() == "1" && HttpContext.Current.Session["SCMModule"] != null)
            {
                if (HttpContext.Current.Session["ARTEndedStatus"].ToString() == "ART Stopped")
                {
                    sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                    sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where dbo.fn_GetDrugTypeId_futures (md.Drug_pk) <>37 and DrugName LIKE '%" + prefixText + "%' Group by md.Drug_pk,md.Drugname";
                }
                else
                {
                    sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                    sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' And md.DeleteFlag = 0 Group by md.Drug_pk,md.Drugname;";

                }
            }
            else
            {
                if (HttpContext.Current.Session["ARTEndedStatus"].ToString() == "ART Stopped")
                {
                    sqlQuery = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0 and dbo.fn_GetDrugTypeId_futures (Drug_pk) <>37 and  DrugName LIKE '%{0}%'", prefixText);
                }
                else
                {
                    if (HttpContext.Current.Session["TreatmentProg"] != null)
                    {
                        if (HttpContext.Current.Session["TreatmentProg"].ToString() == "225")
                        {

                            sqlQuery = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0 and dbo.fn_GetDrugTypeId_futures (Drug_pk) <>37 and DrugName LIKE '%{0}%'", prefixText);
                        }
                        else
                        {
                            sqlQuery = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0 and DrugName LIKE '%{0}%'", prefixText);
                        }
                    }
                    else
                    {
                        sqlQuery = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0 and DrugName LIKE '%{0}%'", prefixText);
                    }
                }

            }
            //filling data from database
            DataTable dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);

            if (HttpContext.Current.Session["Paperless"].ToString() == "1" && HttpContext.Current.Session["SCMModule"] != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Drugs item = new Drugs();
                            item.DrugId = (int)row["Drug_pk"];
                            item.DrugName = (string)row["DrugName"];
                            item.AvailableQuantity = Convert.ToInt32(row["QTY"].ToString());
                            items.Add(item);
                        }
                        catch 
                        {

                        }
                    }

                }
            }
            else
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Drugs item = new Drugs();
                            item.DrugId = (int)row["Drug_pk"];
                            item.DrugName = (string)row["DrugName"];
                            //item.AvlQty = Convert.ToInt32(row["QTY"].ToString());
                            items.Add(item);
                        }
                        catch 
                        {

                        }
                    }

                }
            }

            return items;
        }

        #endregion

        #region Populate Data

        /// <summary>
        /// Gets the treatment program.
        /// </summary>
        void PopulateTreatmentProgram()
        {
            DataTable dtTreatmentPrograms = new DataTable("TreatmentProgram");
            BindFunctions BindManager = new BindFunctions();
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            string Query = "exec dbo.pr_Admin_SelectTreatmentProgram_Constella;   ";
            dtTreatmentPrograms = objRptFields.ReturnDatatableQuery(Query);
             
            objRptFields=null;
            BindManager.BindCombo(ddlTreatment, dtTreatmentPrograms,"Name", "ID");
        }
        /// <summary>
        /// Populates the period taken.
        /// </summary>
        void PopulatePeriodTaken()
        {
            BindFunctions BindManager = new BindFunctions();
            string query="Select ID,Name From dbo.mst_decode where codeID=(select CodeID from dbo.mst_code where Name='Pharmacy Period Taken') and (DeleteFlag=0 or Deleteflag is null);";
             IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");          
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);
          
            objRptFields=null;
            BindManager.BindCombo(ddlPeriodTaken, dataTable, "Name", "ID");
        }

        /// <summary>
        /// Populates the providers.
        /// </summary>
        void PopulateProviders()
        {
            BindFunctions BindManager = new BindFunctions();
            string query = "SELECT ID,name,DeleteFlag from mst_Provider where DeleteFlag=0 order by SRNO asc;";
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);

            objRptFields = null;
            BindManager.BindCombo(ddlProvider, dataTable, "Name", "ID");
        }
        /// <summary>
        /// Populates the regimen line.
        /// </summary>
        void PopulateRegimenLine()
        {
            BindFunctions BindManager = new BindFunctions();
            string query = "Select ID,Name From mst_RegimenLine Where DeleteFlag=0 order by SRNO asc;";
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);

            objRptFields = null;
            BindManager.BindCombo(ddlregimenLine, dataTable, "Name", "ID");
        }

        /// <summary>
        /// Populates the appointment reason.
        /// </summary>
        void PopulateAppointmentReason()
        {
            BindFunctions BindManager = new BindFunctions();
            string query = "Select ID,Name From mst_Decode  where CodeID=26 And (DeleteFlag=0 or DeleteFlag IS NULL) Order By SRNO  asc;";
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);

            objRptFields = null;
            BindManager.BindCombo(ddlAppntReason, dataTable, "Name", "ID");
        }
        /// <summary>
        /// Populates the employee list.
        /// </summary>
        void PopulateEmployeeList(bool PaperLess=false)
        {
            BindFunctions BindManager = new BindFunctions();
            string query = "Select EmployeeName, FirstName + ' '+ LastName EmployeeName, DeleteFlag From mst_Employee Where DeleteFlag= 0 Order	By FirstName, LastName ;";
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);

            objRptFields = null;
            BindManager.BindCombo(ddlPharmOrderedbyName, dataTable, "EmployeeName", "EmployeeID");
            BindManager.BindCombo(ddlPharmReportedbyName, dataTable, "EmployeeName", "EmployeeID");
            BindManager.BindCombo(ddlPharmSignature, dataTable, "EmployeeName", "EmployeeID");
        }
        /// <summary>
        /// Populates the drug strength.
        /// </summary>
        void PopulateDrugStrength()
        {
            string query = "Select Id [FrequencyId],Name [FrequencyName],Multiplier  From mst_frequency Where DeleteFlag = 0 ; ";
            BindFunctions BindManager = new BindFunctions();
            IDrug objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(query);
            objRptFields = null;

            ddlFrequency.Items.Clear();
            ddlFrequency.Items.Add(new RadComboBoxItem("Select.....", ""));
            ddlFrequency.DataSource = dataTable;
            ddlFrequency.DataBind();

        }
        #endregion

       
    }
}