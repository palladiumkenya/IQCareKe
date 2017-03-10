using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Application.Presentation;
using Interface.Clinical;

namespace IQCare.Web.Clinical
{
    public partial class JsonDebitNoteSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = GetIdParameter();
            string json;  // this will be the data returned

            // change the content type from HTML to JSON and restart the response.
            Response.Clear();
            Response.ContentType = "application/json";

            DataTable table = GetDataTable(id); // get data using remoting
            json = GetJson(table);  // convert datatable to json

            Response.Write(json);
            Response.End();
        }

        private int GetIdParameter()
        {
            string param = Request.QueryString["PatientId"];
            int id;
            return Int32.TryParse(param, out id) ? id : 0;
        }

        /// <summary>
        /// regular way to get data using remoting in IQCare
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DataTable GetDataTable(int id)
        {
            IPatientHome PatientManager;
            PatientManager =
                (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            DataTable dataTable = PatientManager.GetPatientDebitNoteSummary(id);
            return dataTable;
        }

        /// <summary>
        /// returns json from a datatable using Serialization.
        /// this could be a utility method and made more generic.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetJson(DataTable table)
        {
            // first get the datatable into a more useable format
            List<DebitNoteSummary> list = GetSummaryList(table);
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<DebitNoteSummary>));
            ser.WriteObject(stream, list);
            string json = Encoding.Default.GetString(stream.ToArray());
            return json;
        }

        /// <summary>
        /// convert the data table to a list of typed elements.
        /// this helps the serializer produce cleaner json
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<DebitNoteSummary> GetSummaryList(DataTable table)
        {
            // this would be simpler using LINQ but we are using the 2.0 framework today.

            List<DebitNoteSummary> list = new List<DebitNoteSummary>();
            foreach (DataRow row in table.Rows)
            {
                DebitNoteSummary note = new DebitNoteSummary();
                note.VisitDate = Convert.ToDateTime(row["VisitDate"]);
                note.TotalCost = Convert.ToDecimal(row["TotalCost"]);
                note.TotalAmount = Convert.ToDecimal(row["TotalAmount"]);
                list.Add(note);
            }
            return list;
        }

        /// <summary>
        /// this is a strongly typed debitnote datarow.
        /// converting it this simpler type help with serialization.
        /// </summary>
        [Serializable]
        public class DebitNoteSummary
        {
            public DateTime VisitDate;
            public decimal TotalCost;
            public decimal TotalAmount;
        }
    }
}