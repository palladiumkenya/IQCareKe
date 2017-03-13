using System;
//using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Clinical;
namespace IQCare.Web.Clinical
{
    public partial class DebitNoteControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSummaryGridView();
        }

        private void LoadSummaryGridView()
        {
            IPatientHome PatientManager;
            PatientManager =
                (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            DataTable dataTable = PatientManager.GetPatientDebitNoteSummary(Convert.ToInt32(Session["PatientId"]));
            GridViewSummary.DataSource = dataTable;
            AddFormattedDateColumn(dataTable);
            GridViewSummary.DataBind();
            GridViewTran.DataSource = null;
            GridViewTran.DataBind();
        }
        protected void GridViewSummary_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // this should be the print button.
        }
        protected void GridViewSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int billid = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Select":
                    ShowDetails(billid);
                    break;
                case "Print":
                    PrintDebitNote(billid);
                    break;
            }

        }

        private void PrintDebitNote(int billid)
        {
            //throw new NotImplementedException();
            string theUrl = string.Format("{0}&PatientId={1}&SatelliteID={2}&CountryID={3}&PosID={4}&sts={5}", "../Reports/frmReport_PatientARVPickup.aspx?name=Add", Session["PatientId"], Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], "1");
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            //theScript += "window.open('frmDrugSelector.aspx?DrugType=37','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "window.open('" + theUrl + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=800,height=700,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        private void ShowDetails(int billid)
        {
            IPatientHome PatientManager;
            PatientManager =
                (IPatientHome)
                ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            //DataSe dataTable = PatientManager.GetPatientDebitNoteDetails(billid);
            //GridViewTran.DataSource = dataTable;

            GridViewTran.DataBind();
        }

        private void AddFormattedDateColumn(DataTable dataTable)
        {
            dataTable.Columns.Add("VisitDateFmt", typeof(string));
            foreach (DataRow row in dataTable.Rows)
            {
                row["VisitDateFmt"] = Convert.ToDateTime(row["VisitDate"]).ToString(this.DateFmt);
            }
        }

        public string DateFmt
        {
            get { return Session["AppDateFormat"].ToString(); }
        }

        protected void GridViewSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            

        }
    }
}