using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Interface.CCC.Lookup;
using Application.Presentation;
using Entities.CCC.Lookup;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientFinder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ILookupRepository l=new LookupRepository();
            //l.GetDropdownValue(Sex,"Gender");
            ILookupManager mgr =
                (ILookupManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetGenderOptions();
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    Sex.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                }

                //
               // vw.ForEach(item=> { Sex.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString())); });
            }
        }
    }
}
