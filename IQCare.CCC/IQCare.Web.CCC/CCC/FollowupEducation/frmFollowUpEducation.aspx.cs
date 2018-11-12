using IQCare.CCC.UILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.FollowupEducation
{
    public partial class frmFollowUpEducation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // GetGetCounsellingTopics("ProgressionRX");
        }
        public ArrayList GetGetCounsellingTopics(string counsellingtopics)
        {
            LookupLogic lookUp = new LookupLogic();
            DropDownList dll = new DropDownList();
            lookUp.populateDDL(dll, counsellingtopics);
            // String rows = "Rugute";
            //counsellingtopics = "1";
            //HIVEducationLogic hivEducation = new HIVEducationLogic();

            //DataTable theDT = hivEducation.getCounsellingTopics(counsellingtopics);
            ArrayList rows = new ArrayList();
            int x = dll.Items.Count;
            if (x > 0)
            {
                for (int k = 0; k < x - 1; k++)
                {
                    string[] i = new string[2] { dll.Items[k].Value.ToString(), dll.Items[k].Text.ToString() };
                    rows.Add(i);
                }
            }
            
            return rows;

        }
    }
}