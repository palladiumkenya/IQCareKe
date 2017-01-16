using DataAccess.Context.ModuleMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Patient
{
    public partial class ServiceArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IModuleRepository mr = new ModuleRepository();
            //List<Entities.Administration.ServiceArea> _serviceList = mr.GetModuleServiceArea((mr.GetById(1)).Id);
            //DropDownList1.DataSource = _serviceList;
            //DropDownList1.DataTextField = "DisplayName";
            //DropDownList1.DataValueField = "Id";
            //DropDownList1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}