using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Application.Presentation;
using Interface.Security;
using Application.Common;
using IQCare.Web.UILogic;
namespace IQCare.Web.MasterPage
{
    public partial class levelOneNavigationUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            AuthenticateRights();
        }
        public string CurrentModule
        {
            get
            {
                return labelModule.Text;

            }
            set
            {
                labelModule.Text = value;
            }
        }
        #region "Hide menu item by value"
        public void RemoveMenuItemByValue(MenuItemCollection items, String value)
        {
            List<MenuItem> rmvMenuItem = new List<MenuItem>();

            //Breadth first, look in the collection
            foreach (MenuItem item in items)
            {
                if (item.Value == value)
                {
                    rmvMenuItem.Add(item);
                }
            }

            if (rmvMenuItem.ToArray().Length != 0)
            {
                for (int j = 0; j < rmvMenuItem.ToArray().Length; j++)
                {
                    items.Remove(rmvMenuItem[j]);
                }
            }

            //Search children
            foreach (MenuItem item in items)
            {
                RemoveMenuItemByValue(item.ChildItems, value);
            }
        }
        #endregion

        #region "Assign URL by value"
        public void AssignUrl(MenuItemCollection items, String value, String url)
        {
            foreach (MenuItem item in items)
            {
                if (item.Value == value)
                {
                    item.NavigateUrl = url;
                }
            }

            foreach (MenuItem item in items)
            {
                AssignUrl(item.ChildItems, value, url);
            }
        }
        #endregion

        #region "User Functions ReportHeader footer master"
        private void AuthenticateRights()
        {
            //AuthenticationManager Authentication = new AuthenticationManager();
            CurrentSession thisSession = CurrentSession.Current;
            if (Session["UserRight"].ToString() != "")
            {
                if (thisSession.HasFeaturePermission(ApplicationAccess.FacilitySetup) == false)
                {
                    //mnuAdminFacility.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Facility Setup");
                }

                if (thisSession.HasFeaturePermission(ApplicationAccess.UserAdministration) == false)
                {
                    //mnuAdminUser.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "User Administration");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.UserGroupAdministration) == false)
                {
                    //mnuAdminUserGroup.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "User Group Administration");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.DeletePatient) == false)
                {
                    RemoveMenuItemByValue(mainMenu.Items, "Delete Patient");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.CustomReports) == false)
                {
                    RemoveMenuItemByValue(mainMenu.Items, "Custom Reports");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.FacilityReports) == false)
                {
                    //mnuAdminFacilityReport.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Facility Reports");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.DonorReports) == false)
                {
                    //mnuAdminDonorReport.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Donor Reports");
                }
                if (thisSession.HasFeaturePermission(ApplicationAccess.Schedular) == false)
                {
                    //mnuSchedular.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Scheduler");
                }
               // if (Authentication.HasFeatureRight(ApplicationAccess.ConfigureCustomFields, (DataTable)Session["UserRight"]) == false)
                if (thisSession.HasFeaturePermission("CONFIG_CUSTOM_FIELD")== false)
                {
                    //mnuAdminCustomConfig.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Configure Custom Fields");
                }

                //VY added for custom lists 2015-May-07

                if (!thisSession.HasFeaturePermission("CONFIG_CUSTOM_LIST"))                    
                {
                    //mnuSchedular.Visible = false;
                    RemoveMenuItemByValue(mainMenu.Items, "Customize Lists");
                }
            }
        }
        #endregion

        protected void mainMenu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
    }
}