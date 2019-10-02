using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Presentation;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Interoperability;

namespace IQCare.Web.CCC
{
    public partial class Home : System.Web.UI.Page
    {
        public int AppLocationId;
        ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        protected void Page_Load(object sender, EventArgs e)
        {
            AppLocationId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
            Session["PatientPK"] = 0;
            Session["PatientTrace"] = "";           
        }

        
    }
}