using System;
using System.Data;
using Application.Presentation;
using Interface.Security;
using Entities.Administration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Application.Logger;
using Interface.Clinical;

namespace IQCare.Web.UILogic
{
    /// <summary>
    /// 
    /// </summary>
    public enum SystemSettingResponseCode
    {
        /// <summary>
        /// The success
        /// </summary>
        Success = 100,
        /// <summary>
        /// The password not match
        /// </summary>
        NoFacilityDefined = 200,
        /// <summary>
        /// The invalid login
        /// </summary>
        WrongVersion = 300

    }
    /// <summary>
    /// 
    /// </summary>
    public class SystemSetting
    {
        /// <summary>
        /// The facilities
        /// </summary>
        public readonly List<Facility> Facilities;
        /// <summary>
        /// The version
        /// </summary>
        public readonly SystemVersion Version;
        /// <summary>
        /// The response code
        /// </summary>
        public readonly SystemSettingResponseCode responseCode;

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
        /// The is valid
        /// </summary>
        protected bool isValid;
        /// <summary>
        /// Prevents a default instance of the <see cref="SystemSetting"/> class from being created.
        /// </summary>
        SystemSetting()  
        {
            IUser usrMgr = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataSet theDS = usrMgr.GetFacilitySettings();
            DataRow r = theDS.Tables[1].Rows[0];
            SystemVersion version = new SystemVersion()
            {
                Id = Convert.ToInt32(r["Id"]),
                AppVersion = r["AppVer"].ToString(),
                DBVersion = r["DbVer"].ToString(),
                ReleaseDate = Convert.ToDateTime(r["RelDate"]),
                VersionName = Convert.ToString(r["VersionName"])
            };
            this.isValid = true;
            List<Facility> facilities = null;
            this.responseCode = SystemSettingResponseCode.Success;
            if (theDS.Tables[0].Rows.Count > 0)
            {
                facilities = (from row in theDS.Tables[0].AsEnumerable()
                              select new Facility()
                              {
                                  Currency = row["Currency"].ToString(),
                                  MasterIndex = row["PosID"].ToString(),
                                  PaperLess = Convert.ToBoolean(row["Paperless"]),
                                  Preffered = Convert.ToBoolean(row["Preferred"]),
                                  Id = Convert.ToInt32(row["FacilityID"]),
                                  Name = Convert.ToString(row["FacilityName"]),
                                  GracePeriod = Convert.ToInt32(row["AppGracePeriod"]),
                                  BackupDrive = row["BackupDrive"].ToString(),
                                  Active = true,
                                  LoginImage =  row["Image"].ToString(),
                                  SystemId = Convert.ToInt32(row["SystemId"]),
                                  SatelliteId = Convert.ToInt32(row["SatelliteID"]),
                                  DeleteFlag = false

                              }
                 ).ToList();
            }
            else
            {
                this.responseCode = SystemSettingResponseCode.NoFacilityDefined;
                this.isValid = false;
            }
            if (version.AppVersion != GblIQCare.AppVersion || version.ReleaseDate.ToString("dd-MMM-yyyy") != GblIQCare.ReleaseDate)
            {
                this.responseCode = SystemSettingResponseCode.WrongVersion;
                this.isValid = false;
            } 
            this.Facilities = facilities;
            this.Version = version;
        }
        /// <summary>
        /// Generates the cache.
        /// </summary>
        /// <param name="StateInfo">The state information.</param>
        public static void GenerateCache(object StateInfo)
        {try
            {
                string xmlPath = GblIQCare.GetXMLPath();
                IIQCareSystem DateManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                DateTime theDTime = DateManager.SystemDate();
                System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(xmlPath + "\\AllMasters.con");
                System.IO.FileInfo theFileInfo2 = new System.IO.FileInfo(xmlPath + "\\DrugMasters.con");
                System.IO.FileInfo theFileInfo3 = new System.IO.FileInfo(xmlPath + "\\LabMasters.con");
                if (theFileInfo1.LastWriteTime.Date != theDTime.Date || theFileInfo2.LastWriteTime.Date != theDTime.Date || theFileInfo3.LastWriteTime.Date != theDTime.Date)
                {
                    IIQCareSystem theCacheManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
                    DataSet theMainDS = theCacheManager.GetSystemCache();
                    IQCareUtils.WriteCache(ref theMainDS, theDTime);

                }
            }
            catch { }
        }
        public static void LogError(Exception lastError)
        {
            try
            {
                if (lastError != null)
                {
                    Exception baseException = lastError.GetBaseException();
                    if (baseException != null)
                    {
                        lastError = baseException;
                    }
                }
                EventLogger logger = new EventLogger();
                logger.LogError(lastError);

                if (ConfigurationManager.AppSettings.Get("DEBUG").ToUpper() == "TRUE")
                {
                    HttpContext.Current.Session["IQCARE_ERROR"] = lastError.Message + lastError.StackTrace;
                }
                else
                {
                    HttpContext.Current.Session.Remove("IQCARE_ERROR");
                }
            }
            catch { }
        }
        /// <summary>
        /// Gets the facility settings.
        /// </summary>
        /// <returns></returns>
        public static SystemSettingResponseCode GetFacilitySettings()
        {
           
            SystemSetting cSS = new SystemSetting();
            if(cSS.isValid)
            {
                HttpContext.Current.Session["SystemSettings"] = cSS;
            }
            return cSS.responseCode;  
            
        }
        /// <summary>
        /// Gets the current system.
        /// </summary>
        /// <value>
        /// The current system.
        /// </value>
        public static SystemSetting CurrentSystem
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Session != null) && (HttpContext.Current.Session["SystemSettings"] != null))
                {
                    return (SystemSetting)HttpContext.Current.Session["SystemSettings"];
                }
                else
                {
                    SystemSetting cSS = new SystemSetting();
                    if (cSS.isValid)
                    {
                        HttpContext.Current.Session["SystemSettings"] = cSS;
                    }
                    return cSS;
                }
               // return null;
            }
        }
        /// <summary>
        /// Gets the default facility.
        /// </summary>
        /// <value>
        /// The default facility.
        /// </value>
        public  Facility DefaultFacility
        {
            get
            {
                return CurrentSystem.Facilities.OrderBy(f=> f.Id).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets the system date.
        /// </summary>
        /// <value>
        /// The system date.
        /// </value>
        public static DateTime SystemDate
        {
            get
            {
                IIQCareSystem DateManager;
                DateManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                DateTime theDTime = DateManager.SystemDate();
                return theDTime;
            }
        }
        public static DataTable GetPatientIdentifiers(int serviceAreaId = 0)
        {
            IPatientRegistration ipr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            return ipr.GetIdentifiersByServiceAreaId(serviceAreaId);
        }
    }
}
