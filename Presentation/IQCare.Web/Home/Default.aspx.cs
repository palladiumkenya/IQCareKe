using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using IQCare.Web.UILogic;
using System.Data;
using System.Web;
using Application.Presentation;
namespace IQCare.Web.Home
{
    public partial class Default : System.Web.UI.Page
    {
        CurrentSession ThisSession;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ThisSession = CurrentSession.Current;
            if (ThisSession == null)
            {
                CurrentSession.Logout();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect("~/frmLogin.aspx", false);

            }
            if (!IsPostBack)
                Init_page();
        }
        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void Init_page()
        {
            
            Session["PatientId"] = 0;
            Session["IQCareAppVersionName"] = GblIQCare.VersionName;
            Session["IQCareAppReleaseDate"] = GblIQCare.ReleaseDate;
            base.Session["TechIdentifier"] = null;
            //createserviceButtons();
            AddServiceButtons();
            ThisSession = CurrentSession.Current.ResetCurrentPatient();
        }
        private DataTable CreateServiceTiles()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ResourceName", typeof(String));
            theDT.Columns.Add("ResourceUrl", typeof(String));
            theDT.Columns.Add("ResourceDescription", typeof(String));
            theDT.Columns.Add("ResourceColor", typeof(String));
            theDT.Columns.Add("IconFont", typeof(String));
            theDT.Columns.Add("ModuleId", typeof(Int32));
            String[] colors = { "firebrick", "blueviolet", "darkcyan", "chocolate", "darkslateblue", "darkmagenta", "cornflowerblue" };
            List<TableCell> cells = new List<TableCell>();
            int colorIndex = 0;


            foreach (HomePageLandScape landScape in ThisSession.CurrentLandScape)
            {

                DataRow row = theDT.NewRow();
                row["ResourceName"] = landScape.ServiceAreaName;
                row["ResourceDescription"] = landScape.MenuName;
                row["ModuleId"] = landScape.MenuId;
                colorIndex = (colorIndex > 6 ? 0 : colorIndex);
                row["ResourceColor"] = colors[colorIndex].ToString();
                if (landScape.MenuName == "Records")
                {
                    row["IconFont"] = "iq iq-records";
                }
                else if (landScape.ServiceAreaName.Contains("257"))
                {
                    row["IconFont"] = "iq iq-awareness-ribbon";
                }
                else if (landScape.ServiceAreaName == "Billing")
                {
                    row["IconFont"] = "iq iq-billing";
                }
                else if (landScape.ServiceAreaName == "Eye Clinic")
                {
                    row["IconFont"] = "fa fa-low-vision";
                }
                else if (landScape.ServiceAreaName == "Outpatient")
                {
                    row["IconFont"] = "fa fa-user-md";
                }
                else if (landScape.ServiceAreaName == "Laboratory")
                {
                    row["IconFont"] = "iq iq-lab";
                }
                else if (landScape.ServiceAreaName == "Imaging")
                {
                    row["IconFont"] = "fa fa-heartbeat";
                }
                else if (landScape.ServiceAreaName == "TB")
                {
                    row["IconFont"] = "iq iq-lungs";
                }
                else if (landScape.ServiceAreaName == "Ward Admission")
                {
                    row["IconFont"] = "fa fa-bed";
                }
                else if (landScape.ServiceAreaName == "Green card")
                {
                    row["IconFont"] = "iq iq-awareness-ribbon";
                }
                else if (landScape.ServiceAreaName == "CCC")
                {
                    row["IconFont"] = "iq iq-awareness-ribbon";
                }
                else row["IconFont"] ="iq iq-default";

                if (landScape.ClickAction == RedirectAction.FindAddPatient)
                {
                    row["ResourceUrl"] = string.Format("../Patient/FindAdd.aspx?srvNm={0}&mod={1}", landScape.MenuName, landScape.MenuId);
                }
                else if (landScape.ClickAction == RedirectAction.ModuleAction)
                {
                    string folderName = landScape.ServiceAreaName.Replace(" ", string.Empty).Replace("/", string.Empty);
                    if(folderName == "HTS")
                    {
                        row["ResourceUrl"] = string.Format("../frontend");
                    }
                    else if (folderName == "CCC")
                    {
                        row["ResourceUrl"] = string.Format("/frontend/dashboard/facilityDashboard");
                    }
                    else if(System.IO.File.Exists(Server.MapPath(string.Format("~/{0}/frmPharmacy_Dashboard.aspx", folderName))))
                    {
                        Guid g = Guid.NewGuid();
                        row["ResourceUrl"] = string.Format("../{1}/frmPharmacy_Dashboard.aspx?key={0}", g.ToString(), folderName);
                    }
                    else { row["ResourceUrl"] = ""; }
                }
                theDT.Rows.Add(row);

                colorIndex++;


            }



            return theDT;
        }

        /// <summary>
        /// Formats the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected string formatUrl(object url)
        {
            return String.Format(@"./{0}", url.ToString());
        }
        private void AddServiceButtons()
        {
            DataTable theDT = this.CreateServiceTiles();
            RepeaterseriveAreas.DataSource = theDT;
            RepeaterseriveAreas.DataBind();

        }
        protected string showIcon(string iconfont)
        {
            return String.Format(@"<i class='{0} iq-tile'></i>", iconfont);
        }
        protected void LoadServiceCommand_OnCommand(object sender, CommandEventArgs e)
        {
            int moduleid;
            if (int.TryParse(e.CommandArgument.ToString(), out moduleid))
            {
                ThisSession.SetCurrentModule(moduleid);

                LinkButton button = (sender as LinkButton);
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                string url = (item.FindControl("linkUrl") as Label).Text;
                if (url != "")
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(url, false);
                }

              /*  HomePageLandScape home = ThisSession.CurrentLandScape.Where(m => m.MenuId == moduleid).FirstOrDefault();
                if (home != null)
                {
                  ///  string url = "";
                    if (home.ClickAction == RedirectAction.FindAddPatient)
                    {
                        url = String.Format("~/Patient/FindAdd.aspx?srvNm={0}&mod={1}", home.MenuName, home.MenuId);
                    }
                    else if (home.ClickAction == RedirectAction.ModuleAction)
                    {
                        string folderName = home.ServiceAreaName.Replace(" ", String.Empty);
                        if (System.IO.File.Exists(Server.MapPath(string.Format("~/{0}/Home.aspx", folderName))))
                        {
                            Guid g = Guid.NewGuid();
                            url = string.Format("~/{1}/Home.aspx?key={0}", g.ToString(), folderName);

                        }
                        
                    }
                    if (url != "")
                    {
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.Redirect(url, false);
                    }
                }*/
            }
            
        }
        
    }
}

