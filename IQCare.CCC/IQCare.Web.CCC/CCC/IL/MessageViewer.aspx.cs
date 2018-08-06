using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.IL
{
    public partial class MessageViewer : System.Web.UI.Page
    {
        public string MessageType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageType = Request.QueryString["messageType"];
        }
    }
}