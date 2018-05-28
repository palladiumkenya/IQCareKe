using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using IQCare.CCC.UILogic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.Enrollment
{
    public partial class ServiceEnrollment : System.Web.UI.Page
    {
        public string patType { get; set; }
        public int PersonId { get; set; }
        public int PatientExists { get; set; }
        public string AppLocation
        {
            get { return Session["AppLocation"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientType"] !=null && Session["PatientType"].ToString() != null)
            {
                var patientType = int.Parse(Session["PatientType"].ToString());
                patType = LookupLogic.GetLookupNameById(patientType);
            }

            var patientLookManager = new PatientLookupManager();
            int person = int.Parse(Session["PersonId"].ToString());
            PatientLookup patient = patientLookManager.GetPatientByPersonId(person);
            if (patient !=null)
            {
                PatientExists = patient.Id;
            }
            else
            {
                PatientExists = 0;
            }

            if (!IsPostBack)
            {
                ILookupManager mgr =
                    (ILookupManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

                List<LookupItemView> ms = mgr.GetLookItemByGroup("YesNo");
                if (ms != null && ms.Count > 0)
                {
                    ReconfirmatoryTest.Items.Add(new ListItem("select", "0"));
                    foreach (var k in ms)
                    {
                        ReconfirmatoryTest.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> ReConfirmatoryTest = mgr.GetLookItemByGroup("ReConfirmatoryTest");
                if (ReConfirmatoryTest != null && ReConfirmatoryTest.Count > 0)
                {
                    ResultReConfirmatoryTest.Items.Add(new ListItem("select", ""));
                    foreach (var k in ReConfirmatoryTest)
                    {
                        ResultReConfirmatoryTest.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> testTypes = mgr.GetLookItemByGroup("HivTestTypes");
                if (testTypes != null && testTypes.Count > 0)
                {
                    TypeOfReConfirmatoryTest.Items.Add(new ListItem("select", ""));
                    foreach (var item in testTypes)
                    {
                        TypeOfReConfirmatoryTest.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }
            }
        }
    }
}