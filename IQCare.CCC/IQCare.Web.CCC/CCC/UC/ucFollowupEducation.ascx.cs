using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucFollowupEducation : System.Web.UI.UserControl
    {
        public int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
            }
            else
            {
                Session["ExistingRecordPatientMasterVisitID"] = "0";
            }

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                var dataTable = lookUp.GetCouncellingTypes();
                ddlCounsellingType.Items.Add(new ListItem("Select", "0"));
                foreach (DataRow row in dataTable.Rows)
                {
                    var ID = row["ID"].ToString();
                    var Name = row["Name"].ToString();

                    ddlCounsellingType.Items.Add(new ListItem(Name, ID));
                }
            }
        }

        protected void ddlCounsellingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupLogic lookUp = new LookupLogic();
            var dataTable = lookUp.GetCouncellingTopics();
            ddlCounsellingTopic.Items.Add(new ListItem("Select", "0"));
            foreach (DataRow row in dataTable.Rows)
            {
                var ID = row["ID"].ToString();
                var Name = row["Name"].ToString();

                ddlCounsellingTopic.Items.Add(new ListItem(Name, ID));
            }
        }
    }
}