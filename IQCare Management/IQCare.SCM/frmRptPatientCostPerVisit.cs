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
    public partial class frmRptPatientCostPerVisit : Form
    {

        // local instance of the data access object for this form.
        // protected getter that will just get one and keep using it. see singleton pattern.
        private IBudgetConfigDetail objDataAccess = null;
        protected IBudgetConfigDetail ObjDataAccess
        {
            get
            {
                if (objDataAccess == null)
                {
                    objDataAccess = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
                }
                return objDataAccess;
            }
        }

        public frmRptPatientCostPerVisit()
        {
            InitializeComponent();
            
        }

        private void frmConfigureBudget_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Location = new Point(1,1);
            BindDropdown();
            //Init_Form();
            //SetRights();  // not sure if we are checking rights at this level.
        }

        private void BindDropdown()
        {
            BindFunctions objBindControls = new BindFunctions();
            DataTable dtYears = objBindControls.GetYears(DateTime.Now.Year, "Name", "Id");
            ddlProgramYear.Items.Clear();
            objBindControls.Win_BindCombo(ddlProgramYear, dtYears, "Name", "Id");
            ddlProgramYear.SelectedValue = DateTime.Now.Year;

        }

        private void Init_Form()
        {
            // nothing to do here.  included because other forms have it.
            // the datagrid is filled when the year dropdown is selected the first time in BindDropDown()
        }





       

        /// <summary>
        /// load the data for the given year form the DataAccess class
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DataTable LoadData(int year)
        {
            DataTable dtVisits = ObjDataAccess.GetPatientVisitConfigForYear(year, GblIQCare.AppUserId);
            dtVisits.AcceptChanges(); // helps detecting changes later
            return dtVisits;
        }

      
       
       

       


        private void buttonClose_Click(object sender, EventArgs e)
        {
            
            Close();
        }

       

    }
}
