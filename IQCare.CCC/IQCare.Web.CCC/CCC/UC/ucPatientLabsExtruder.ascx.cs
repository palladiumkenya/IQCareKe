using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.Web.CCC.WebService;

namespace IQCare.Web.CCC.UC
{
   

    public partial class ucPatientLabsExtruder : System.Web.UI.UserControl
    {
        protected int patientId
        {
            get { return Convert.ToInt32(Session["patientId"]); }
        }

      
        protected void Page_Load(object sender, EventArgs e)
        {

         

        }
    }
}