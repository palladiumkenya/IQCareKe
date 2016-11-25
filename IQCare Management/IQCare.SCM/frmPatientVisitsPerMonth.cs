using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;

namespace IQCare.SCM
{
    public partial class frmPatientVisitsPerMonth : Form
    {

       

        public frmPatientVisitsPerMonth()
        {
            InitializeComponent();
            
        }

        private void frmConfigureBudget_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Location = new Point(1, 1);
            BindDropdown();
            Init_Form();
            SetRights();  
        }


        private void SetRights()
        {
            if (!GblIQCare.HasFunctionRight(ApplicationAccess.PatientVisitConfiguration, FunctionAccess.Update, GblIQCare.dtUserRight))
            {
                buttonSave.Visible = false;
                dgwVisits.Columns["ColNumber"].ReadOnly = true;
            }
        }

        private int currentYear = 0; //med fix this year. 

        private void BindDropdown()
        {
            BindFunctions objBindControls = new BindFunctions();
            DataTable dtYears = objBindControls.GetYears(DateTime.Now.Year+1, "Name", "Id");
            ddlProgramYear.Items.Clear();
            objBindControls.Win_BindCombo(ddlProgramYear, dtYears, "Name", "Id");
            ddlProgramYear.SelectedValue = DateTime.Now.Year;
            currentYear = Convert.ToInt32(ddlProgramYear.SelectedValue);

        }

        private void Init_Form()
        {
            string XmlFile = GblIQCare.GetXMLPath() + "Currency.xml";
            DataSet theDSCurrencyXML = new DataSet();
            theDSCurrencyXML.ReadXml(XmlFile);

            string CurrencyCode = theDSCurrencyXML.Tables[0].Select("Id = '" + GblIQCare.AppCountryId + "'").SingleOrDefault().ItemArray[1].ToString().Split('(').LastOrDefault().Replace(")", "");
            labelCurrency.Text = "Currency: " + CurrencyCode;
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {

                dgwVisits.AutoGenerateColumns = false;
                dgwVisits.DataSource = theDT;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message;
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// fillin the MonthName coumns with the month name from the bound month number
        /// </summary>
        private void FillMonthNames()
        {
            foreach (DataGridViewRow gridRow in dgwVisits.Rows)
            {
                DataRowView rowView = gridRow.DataBoundItem as DataRowView;
                DataRow dataRow = rowView.Row;
                int month = dataRow.Field<int>("Month");
                gridRow.Cells["ColMonthName"].Value = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + " " + ddlProgramYear.Text;
            }
        }

        /// <summary>
        /// calulate the feel cells
        /// </summary>
        /// <param name="gridRow"></param>
        private void CalcFees(DataGridViewRow gridRow)
        {
            // get the grid cells we nee to work with
            DataGridViewCell dcAdminFee = gridRow.Cells["colAdminFee"];
            DataGridViewCell dcConsultFee = gridRow.Cells["colConsultFee"];

            // get the data row bound to this grid row
            DataRowView rowView = gridRow.DataBoundItem as DataRowView;
            DataRow dataRow = rowView.Row;

            // get the number of visitors bound
            int visits = dataRow.Field<int>("visits");

            // update the cell values with the budgetamt/visits formatted for currency.
            string fmt = "{0:#,###,###,###,###,###.##}";
            if (visits == 0)
            {
                dcAdminFee.Value = string.Format(fmt, 0);
                dcConsultFee.Value = string.Format(fmt, 0);
            }
            else
            {
                decimal budgetOverhead = dataRow.Field<decimal>("BudgetOverhead");
                decimal budgetSalary = dataRow.Field<decimal>("BudgetSalary");

                decimal adminfee = budgetOverhead / visits;
                decimal consultfee = budgetSalary / visits;

                dcAdminFee.Value = string.Format(fmt, adminfee);
                dcConsultFee.Value = string.Format(fmt, consultfee);
            }
        }

        /// <summary>
        /// when the user selects a new year: 
        ///     if the data was updated, prompt for save
        ///         yes = save the data
        ///     load the data for the newly selected year 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlProgramYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check to see if the data has changed
            DataTable dt = dgwVisits.DataSource as DataTable;
            if(dt != null && dt.GetChanges()!=null)
            {
                // prompt the user to save or not.
                DialogResult dr = IQCareWindowMsgBox.ShowWindowConfirm("SCMDataChangedSave", this);
                if(dr.ToString() == "Yes")
                {
                    Save();
                }
            }

            // load the newly selected years data
            DataRowView row = ((ComboBox)sender).SelectedItem as DataRowView;
            currentYear = int.Parse(row.Row.Field<string>("Id"));

            if (currentYear == 0)
            {
                currentYear = DateTime.Now.Year; // default to current year
            }

            DataTable dtVisits = LoadData(currentYear);
            ShowGrid(dtVisits);
        }

        /// <summary>
        /// load the data for the given year form the DataAccess class
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DataTable LoadData(int year)
        {
            IBudgetConfigDetail ObjDataAccess = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
      
            DataTable dtVisits = ObjDataAccess.GetPatientVisitConfigForYear(year, GblIQCare.AppUserId);
            dtVisits.AcceptChanges(); // helps detecting changes later
            return dtVisits;
        }

        /// <summary>
        /// save the currently selected data
        ///     only save the rows that have changed
        ///     inform the user about the updates
        ///     
        /// </summary>
        private void Save()
        {
            // get all the bound data
            DataTable dt = dgwVisits.DataSource as DataTable;

            // we only want to send the updated rows to the DB
            DataTable changes = dt.GetChanges();
            if (changes != null)
            {
                // save to the DB
                IBudgetConfigDetail ObjDataAccess = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
                ObjDataAccess.SavePatientVisitConfigForYear(changes, currentYear, GblIQCare.AppUserId);
            }

            // inform the user and re-load the data
            IQCareWindowMsgBox.ShowWindow("SCMSave", this);
            LoadData(currentYear);
        }

       
        /// <summary>
        /// after the row is validated
        ///     calculate the new fees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwVisits_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgwVisits.Rows[e.RowIndex];
            CalcFees(row);
        }

        private void dgwVisits_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            IQCareWindowMsgBox.ShowWindow("SCMVisitsPerMonthError", this);
         
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DataTable dt = dgwVisits.DataSource as DataTable;
            if (dt != null && dt.GetChanges() != null)
            {
                DialogResult dr = IQCareWindowMsgBox.ShowWindowConfirm("SCMDataChangedSave", this);
                if (dr.ToString() == "Yes")
                {
                    Save();
                }
            }
            Close();
        }

        private void dgwVisits_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FillMonthNames();
            foreach (DataGridViewRow row in dgwVisits.Rows)
            {
                CalcFees(row);
            }
        }

    }
}
