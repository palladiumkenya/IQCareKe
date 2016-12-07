using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Config.Data;
using Config.Core;
using Config.Core.Interfaces;
using Config.Data.Repository;
using Config.Core.Model;

namespace IQCare.Web.CCC.Patient
{
    public partial class AerviceArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IServiceAreaRepository SArea = new ServiceAreaRepository();
            List<ServiceArea> ServiceList = SArea.GetAll().ToList();
            DropDownList1.DataSource = ServiceList;
            DropDownList1.DataTextField = "DisplayName";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}