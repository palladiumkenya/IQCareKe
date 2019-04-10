using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Administration;
using System.Data;
using System.Web;
using System.Web.UI;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Entities.PatientCore;

namespace IQCare.Web.UILogic
{
    /// <summary>
    /// 
    /// </summary>
    public enum LoginResponseCode
    {
        /// <summary>
        /// The success
        /// </summary>
        Success = 100,
        /// <summary>
        /// The password not match
        /// </summary>
        PasswordNotMatch = 200,
        /// <summary>
        /// The invalid login
        /// </summary>
        InvalidLogin = 300,
        /// <summary>
        /// The password expired
        /// </summary>
        PasswordExpired = 400

    }
    /// <summary>
    /// 
    /// </summary>
    public class HomePageLandScape
    {
        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>
        /// The name of the menu.
        /// </value>
        public string MenuName { get; set; }
        /// <summary>
        /// Gets or sets the name of the service area.
        /// </summary>
        /// <value>
        /// The name of the service area.
        /// </value>
        public string ServiceAreaName { get; set; }
        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>
        /// The menu identifier.
        /// </value>
        public int MenuId { get; set; }
        /// <summary>
        /// Gets or sets the click action.
        /// </summary>
        /// <value>
        /// The click action.
        /// </value>
        public RedirectAction ClickAction { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public enum RedirectAction
    {
        /// <summary>
        /// The module action
        /// </summary>
        ModuleAction = 1,
        /// <summary>
        /// The find add patient
        /// </summary>
        FindAddPatient = 2
    }
    /// <summary>
    /// 
    /// </summary>
    public class CurrentSession
    {


        /// <summary>
        /// The user
        /// </summary>
        public readonly User User;
        /// <summary>
        /// The facility
        /// </summary>
        public readonly Facility Facility;
        /// <summary>
        /// The user rights
        /// </summary>
        public readonly DataTable UserRights;
        /// <summary>
        /// The user detail
        /// </summary>
        public readonly DataSet UserDetail;
        /// <summary>
        /// Gets the current patient.
        /// </summary>
        /// <value>
        /// The current patient.
        /// </value>
        public Patient CurrentPatient { get; private set; }

        public List<PatientEnrollment> PatientEnrollment { get; private set; }
        public PatientEnrollment CurrentEnrollment
        {
            get
            {
                try
                {

                    return this.PatientEnrollment.FirstOrDefault(en => en.PatientId == CurrentPatient.Id && en.ServiceAreaId == CurrentServiceArea.Id);
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Gets the current service area.
        /// </summary>
        /// <value>
        /// The current service area.
        /// </value>
        public ServiceArea CurrentServiceArea { get; private set; }
       
        /// <summary>
        /// Gets the current form set.
        /// </summary>
        /// <value>
        /// The current form set.
        /// </value>
        public DataTable CurrentFormSet { get; private set; }
        /// <summary>
        /// The has billing
        /// </summary>
        public readonly bool HasBilling;
        /// <summary>
        /// The has PMSCM
        /// </summary>
        public readonly bool HasPMSCM;
        /// <summary>
        /// The has ward admission
        /// </summary>
        public readonly bool HasWardAdmission;
        /// <summary>
        /// The has lab module
        /// </summary>
        public readonly bool HasLabModule;
        /// <summary>
        /// The current land scape
        /// </summary>
        public readonly List<HomePageLandScape> CurrentLandScape;
        /// <summary>
        /// The is valid
        /// </summary>
        protected bool isValid;
        /// <summary>
        /// The has patient
        /// </summary>
        protected bool hasPatient = false;
        /// <summary>
        /// The _response code
        /// </summary>
        private LoginResponseCode _responseCode;
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
        }
        /// <summary>
        /// Gets a value indicating whether [module selected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [module selected]; otherwise, <c>false</c>.
        /// </value>
        public bool ModuleSelected
        {
            get
            {
                return CurrentServiceArea != null;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has patient.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has patient; otherwise, <c>false</c>.
        /// </value>
        public bool HasPatient
        {
            get
            {
                return this.hasPatient;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSession" /> class.
        /// </summary>
        /// <param name="_user">The _user.</param>
        /// <param name="_facility">The _facility.</param>
        /// <param name="_details">The _details.</param>
        CurrentSession(User _user, Facility _facility, DataSet _details)
        {
            User = _user;
            Facility = _facility;
            this.UserDetail = _details;
            this.UserRights = _details.Tables[1];
            CurrentLandScape = this.GetLandScape();
            this.isValid = true;
            this.hasPatient = false;
        }
        /// <summary>
        /// Gets the land scape.
        /// </summary>
        /// <returns></returns>
        List<HomePageLandScape> GetLandScape()
        {
            var result = new List<HomePageLandScape>();
            if (this.Facility.PaperLess)
            {
                result.Add(new HomePageLandScape()
                {
                    MenuName = "Records",
                    MenuId = 0,
                    ClickAction = RedirectAction.FindAddPatient,
                    ServiceAreaName = "Records"
                });
                if (this.Facility.Integrated)
                {
                    result.Add(new HomePageLandScape()
                    {
                        MenuName = "Consultation",
                        MenuId = -1,
                        ClickAction = RedirectAction.FindAddPatient,
                        ServiceAreaName = "Consultation"
                    });
                }
            }
            //Manually add greencard menu
            //result.Add(new HomePageLandScape()
            //{
            //    MenuName = "Green Card (2016)",
            //    MenuId = -1,
            //    ClickAction = RedirectAction.ModuleAction,
            //    ServiceAreaName = "CCC"
            //});
            Facility.Modules.Where(m => m.PublishFlag == true).OrderBy(n => n.Name).ToList().ForEach(
            s =>
            {
                //if (this.HasModuleRight(s.Id, s.Clinical))
                //{

                    result.Add(new HomePageLandScape()
                    {
                        MenuName = s.DisplayName,
                        MenuId = s.Id,
                        ClickAction = !s.ModuleFlag ? RedirectAction.FindAddPatient : RedirectAction.ModuleAction,
                        ServiceAreaName = s.Name
                    });
                //}
            });


            return result;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSession" /> class.
        /// </summary>
        /// <param name="userUname">The user uname.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="systemId">The system identifier.</param>
        /// <param name="strPassword">The string password.</param>
        public CurrentSession(string userUname, int locationId, int systemId, string strPassword)
        {
            IUser LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataSet ds = LoginManager.GetUserCredentials(userUname, locationId, systemId);
            this.isValid = false;
            this.hasPatient = false;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
            {
                Utility theUtil = new Utility();
                if (theUtil.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["Password"])) != strPassword)
                {
                    _responseCode = LoginResponseCode.PasswordNotMatch;
                }
                else if (ds.Tables[3].Rows[0]["ExpPwdFlag"] != null && Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]) != 1 && Convert.ToInt32(ds.Tables[3].Rows[0]["ExpPwdFlag"]) == 1)
                {
                    DateTime lastcontDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PwdDate"]);
                    TimeSpan t = Convert.ToDateTime(ds.Tables[4].Rows[0]["CurrentDate"]) - lastcontDate;
                    double NrOfDaysdiffernce = t.TotalDays;
                    if (NrOfDaysdiffernce > Convert.ToInt32(ds.Tables[3].Rows[0]["ExpPwdDays"]))
                    {
                        _responseCode = LoginResponseCode.PasswordExpired;
                    }
                    else
                    {
                        _responseCode = LoginResponseCode.Success;
                    }
                }
                else
                {
                    _responseCode = LoginResponseCode.Success;
                    DataTable theDT = ds.Tables[2];
                    Facility facility = new Facility()
                    {
                        Currency = theDT.Rows[0]["Currency"].ToString(),
                        MasterIndex = theDT.Rows[0]["PosID"].ToString(),
                        PaperLess = Convert.ToBoolean(theDT.Rows[0]["Paperless"]),
                        Id = Convert.ToInt32(theDT.Rows[0]["FacilityID"]),
                        Name = Convert.ToString(theDT.Rows[0]["FacilityName"]),
                        GracePeriod = Convert.ToInt32(theDT.Rows[0]["AppGracePeriod"]),
                        BackupDrive = theDT.Rows[0]["BackupDrive"].ToString(),
                        Active = true,
                        SatelliteId = Convert.ToInt32(theDT.Rows[0]["SatelliteID"]),
                        DeleteFlag = false,
                        DateFormat = (theDT.Rows[0]["DateFormat"] == DBNull.Value || theDT.Rows[0]["DateFormat"].ToString() == "") ? "dd-MMM-yyyy" : theDT.Rows[0]["DateFormat"].ToString(),
                        Integrated = Convert.ToBoolean(theDT.Rows[0]["Integrated"])

                    };
                    facility.Modules = (from row in ds.Tables[3].AsEnumerable()
                                        select new ServiceArea()
                                        {
                                            Id = Convert.ToInt32(row["ModuleId"]),
                                            Name = Convert.ToString(row["ModuleName"]),
                                            DisplayName = Convert.ToString(row["DisplayName"]),
                                            EnrolFlag = Convert.ToBoolean(row["CanEnroll"]),
                                            ModuleFlag = Convert.ToBoolean(row["ModuleFlag"]),
                                            Clinical = Convert.ToBoolean(row["CanEnroll"]),
                                            //PublishFlag = (Convert.ToString(row["ModuleName"]) == "PM/SCM") ? false : true,
                                            PublishFlag = true,
                                            DeleteFlag = false,
                                            Active = true,
                                            BusinessRules = null,
                                            Identifiers = null

                                        }).OrderBy(m => m.DisplayName).ToList();
                    //Set pmscm to not published so that it does not show on the page  .. NEEDS improvement                  
                    if (!facility.PaperLess)
                    {

                        facility.Modules.RemoveAll(m => m.Name.ToUpper() == "LABORATORY" || m.Name.ToUpper() == "PHARMACY" 
                       // || 
                      //  m.Name.ToUpper() == "PM/SCM"
                        );
                    }
                    this.HasBilling = facility.Modules.Exists(m => m.Name.ToUpper() == "BILLING");
                    this.HasPMSCM = facility.Modules.Exists(m => m.Name.ToUpper() == "PM/SCM");                       //&& (facility.PaperLess == true);
                    this.HasWardAdmission = facility.Modules.Exists(m => m.Name.ToUpper() == "WARD ADMISSION");
                    this.HasLabModule = facility.Modules.Exists(m => m.Name.ToUpper() == "LABORATORY") && (facility.PaperLess == true);
                    DataTable dtUser = ds.Tables[0];
                    User user = new User()
                    {
                        Id = Convert.ToInt32(dtUser.Rows[0]["UserId"]),
                        LoginName = userUname,
                        FirstName = Convert.ToString(dtUser.Rows[0]["UserFirstName"]),
                        LastName = Convert.ToString(dtUser.Rows[0]["UserLastName"]),
                        DeleteFlag = false,
                        Active = true,
                        Employee = null
                    };
                    if (dtUser.Rows[0]["EmployeeId"] != DBNull.Value && Convert.ToInt32(dtUser.Rows[0]["EmployeeId"]) > 0)
                    {
                        user.Employee = new Employee()
                        {
                            Id = Convert.ToInt32(dtUser.Rows[0]["EmployeeId"]),
                            Active = Convert.ToInt32(dtUser.Rows[0]["EmployeeDeleteFlag"]) == 0,
                            Designation = Convert.ToString(dtUser.Rows[0]["Designation"]),
                            DesignationId = Convert.ToInt32(dtUser.Rows[0]["DesignationID"])
                        };
                    }
                    this.isValid = true;
                    this.User = user;
                    Facility = facility;
                    this.UserDetail = ds;
                    this.UserRights = ds.Tables[1];
                    CurrentLandScape = this.GetLandScape();
                    SessionManager.UserId = user.Id;
                    SessionManager.FacilityId = facility.Id;
                    SessionManager.SystemId = facility.SystemId;

                }
            }
            else
            {
                _responseCode = LoginResponseCode.InvalidLogin;
                SessionManager.Dispose();
            }
        }
        public static DateTime SystemDate()
        {
            IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = SystemManager.SystemDate();
            SystemManager = null;
            return theCurrentDate;
        }
        public static string DateFormat
        {
            get
            {
                if (Current != null)
                {
                    return Current.Facility.DateFormat;
                }
                return "dd-MMM-yyyy";
            }
        }
        /// <summary>
        /// Resets the current module.
        /// </summary>
        /// <returns></returns>
        public CurrentSession ResetCurrentModule()
        {
            this.CurrentServiceArea = null;
            return this.ResetCurrentPatient();
        }
        /// <summary>
        /// Resets the current patient.
        /// </summary>
        /// <returns></returns>
        public CurrentSession ResetCurrentPatient()
        {
            CurrentSession session = this;
            session.CurrentPatient = null;          
            session.CurrentFormSet = null;
            session.PatientEnrollment = null;
            session.hasPatient = false;
            HttpContext.Current.Session["CurrentUser"] = session;
            return session;
        }
        /// <summary>
        /// Sets the current module.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public CurrentSession SetCurrentModule(int moduleId)
        {
            this.hasPatient = false;
            this.CurrentPatient = null;
            this.CurrentServiceArea = null;
            this.CurrentFormSet = null;
        
            CurrentSession session = this;
            CurrentServiceArea = session.Facility.Modules.Where(m => m.Id == moduleId).FirstOrDefault();
            
            HttpContext.Current.Session["CurrentUser"] = session;

            return session;
        }
        /// <summary>
        /// Sets the current patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public CurrentSession SetCurrentPatient(int patientId)
        {
            int moduleId = CurrentServiceArea.Id;
            CurrentSession session = this;
            SessionManager.ModuleId = moduleId;
            SessionManager.CurrentUser = session;
            return SetCurrentPatient(patientId, moduleId);
        }
        /// <summary>
        /// Sets the current patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public CurrentSession SetCurrentPatient(int patientId, int moduleId)
        {
            CurrentSession session = this.SetCurrentModule(moduleId);
            PatientService service = PatientService.SetCurrentPatient(session, patientId, moduleId);
            session.PatientEnrollment = new EnrollmentService(patientId).GetPatientEnrollment(session);             
            session.CurrentPatient = service.CurrentPatient;
            session.CurrentServiceArea = service.CurrentServiceArea;
            session.CurrentFormSet = service.Formset;
            session.hasPatient = true;
            SessionManager.PatientId = patientId;
            SessionManager.ModuleId = moduleId;
            SessionManager.CurrentUser = session;
            HttpContext.Current.Session["CurrentUser"] = session;
            return session;
        }
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static CurrentSession Current
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Session != null))
                {
                    return (CurrentSession)HttpContext.Current.Session["CurrentUser"];
                }
                return null;
            }
        }
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            SessionManager.Dispose();
            if (Current != null)
            {
                // pre loggout 
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
           
        }
        /// <summary>
        /// Logins the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="userUname">The user uname.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="systemId">The system identifier.</param>
        /// <param name="strPassword">The string password.</param>
        /// <returns></returns>
        public static LoginResponseCode Login(Page sender, string userUname, int locationId, int systemId, string strPassword)
        {
            CurrentSession cUser = new CurrentSession(userUname, locationId, systemId, strPassword);
            if (cUser.IsValid)
            {
                HttpContext.Current.Session["CurrentUser"] = cUser;
            }
            return cUser._responseCode;
        }
        /// <summary>
        /// Determines whether [has feature permission] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <returns></returns>
        public bool HasFeaturePermission(string featureReferenceId)
        {
            return GblIQCare.HasFeaturePermission(featureReferenceId, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //theDV.RowFilter = "ReferenceId = '" + featureReferenceId + "'";
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// Determines whether [has feature permission] [the specified feature identifier].
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <returns></returns>
        public bool HasFeaturePermission(int featureId)
        {
            return GblIQCare.HasFeaturePermission(featureId, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //theDV.RowFilter = "FeatureId = " + featureId.ToString();
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// Determines whether [has function right] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <param name="functionId">The function identifier.</param>
        /// <returns></returns>
        public bool HasFunctionRight(string featureReferenceId, int functionId)
        {
            return GblIQCare.HasFunctionRight(featureReferenceId, functionId, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //theDV.RowFilter = "ReferenceId = '" + featureReferenceId.ToString() + "' and FunctionId = " + functionId.ToString();
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// Determines whether [has function right] [the specified feature identifier].
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="functionId">The function identifier.</param>
        /// <returns></returns>
        public bool HasFunctionRight(int featureId, int functionId)
        {
            return GblIQCare.HasFunctionRight(featureId, functionId, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //theDV.RowFilter = "FeatureId = " + featureId.ToString() + " and FunctionId = " + functionId.ToString();
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// Determines whether [has function right] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <param name="access">The access.</param>
        /// <returns></returns>
        public bool HasFunctionRight(string featureReferenceId, FunctionAccess access)
        {
            return GblIQCare.HasFunctionRight(featureReferenceId, access, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //theDV.RowFilter = "ReferenceId = '" + featureReferenceId.ToString() + "' and FunctionId = " + access.ToString();
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        
        /// <summary>
        /// Determines whether [has module right] [the specified module identifier].
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="clinical">if set to <c>true</c> [clinical].</param>
        /// <returns></returns>
        public bool HasModuleRight(int moduleId, bool clinical)
        {
            return GblIQCare.HasModuleRight(moduleId, clinical, this.UserRights);
            //DataView theDV = new DataView(this.UserRights);
            //if (clinical)
            //{
            //    theDV.RowFilter = string.Format("ModuleId = {0}", moduleId);
            //}
            //else
            //{
            //    theDV.RowFilter = string.Format("ModuleId = {0} And FeatureTypeName='{1}'", moduleId, "MODULE_ACTION");
            //}
            //if (theDV.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// Determines whether [has current module right].
        /// </summary>
        /// <returns></returns>
        public bool HasCurrentModuleRight()
        {
            DataView theDV = new DataView(this.UserRights);
            int moduleId = CurrentServiceArea.Id;
            bool clinical = CurrentServiceArea.Clinical;
            return this.HasModuleRight(moduleId, clinical);
        }
    }
}
