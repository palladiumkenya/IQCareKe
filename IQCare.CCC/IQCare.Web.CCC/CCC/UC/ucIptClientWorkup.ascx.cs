using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucIptClientWorkup : System.Web.UI.UserControl
    {
        public int PatientId;
        public int PatientMasterVisitId;
        public string StartIPTDate;

        public DropDownList IPTurineColour
        {
            get { return this.urineColour; }
            set { urineColour = value; }
        }

        public DropDownList IPTNumbness
        {
            get { return this.numbness; }
            set { numbness = value; }
        }

        public DropDownList IPTYellowEyes
        {
            get { return this.yellowEyes; }
            set { yellowEyes = value; }
        }

        public DropDownList IPTAbdominalTenderness
        {
            get { return this.abdominalTenderness; }
            set { abdominalTenderness = value; }
        }

        public TextBox IPTLiverTest
        {
            get { return this.liverTest; }
            set { liverTest = value; }
        }

        public DropDownList IPTStartIPT
        {
            get { return this.startIpt; }
            set { startIpt = value; }
        }

        public TextBox StartDateIPT
        {
            get { return this.IPTStartDate; }
            set { IPTStartDate = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            StartIPTDate = StartDateIPT.Text;
        }
    }
}