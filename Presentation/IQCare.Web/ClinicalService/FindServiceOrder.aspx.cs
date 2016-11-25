using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.ClinicalService
{
    public partial class FindServiceOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        protected void grdPatientOrder_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdPatientOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdPatientOrder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ckbTodaysOrders_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbtlst_findOrder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}