using System;
using System.Collections;
using System.Data;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Administration;

namespace BusinessProcess.Administration
{
    /// <summary>
    /// 
    /// </summary>
    public class BFacility : ProcessBase, IFacilitySetup
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BFacility"/> class.
        /// </summary>
        public BFacility()
        {
        }

        #endregion "Constructor"

        /// <summary>
        /// Gets the backup setup.
        /// </summary>
        /// <returns></returns>
        public DataTable GetBackupSetup()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject BackupManager = new ClsObject();
                return (DataTable)BackupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetBackupSetup_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the facility.
        /// </summary>
        /// <returns></returns>
        public DataSet GetFacility()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the facility list.
        /// </summary>
        /// <param name="SystemId">The system identifier.</param>
        /// <param name="FeatureId">The feature identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetFacilityList(int SystemId, int FeatureId, int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFacilityList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleName()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetModuleName_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the system based labels.
        /// </summary>
        /// <param name="SystemId">The system identifier.</param>
        /// <param name="FeatureId">The feature identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetSystemBasedLabels(int SystemId, int FeatureId, int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_GetSystemBasedLabels_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the backup setup.
        /// </summary>
        /// <param name="theDrive">The drive.</param>
        /// <param name="theTime">The time.</param>
        /// <returns></returns>
        public int SaveBackupSetup(string theDrive, DateTime theTime)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BackupDrive", SqlDbType.VarChar, theDrive.ToString());
                ClsUtility.AddParameters("@BackUpTime", SqlDbType.DateTime, theTime.ToString());
                ClsObject BackupManager = new ClsObject();
                return (Int32)BackupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateBackupSetup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                // BackupManager = null;
            }
        }

        /// <summary>
        /// Saves the new facility.
        /// </summary>
        /// <param name="FacilityName">Name of the facility.</param>
        /// <param name="CountryID">The country identifier.</param>
        /// <param name="PosID">The position identifier.</param>
        /// <param name="SatelliteID">The satellite identifier.</param>
        /// <param name="NationalID">The national identifier.</param>
        /// <param name="ProvinceId">The province identifier.</param>
        /// <param name="DistrictId">The district identifier.</param>
        /// <param name="image">The image.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="AppGracePeriod">The application grace period.</param>
        /// <param name="dateformat">The dateformat.</param>
        /// <param name="PepFarStartDate">The pep far start date.</param>
        /// <param name="SystemId">The system identifier.</param>
        /// <param name="thePreferred">The preferred.</param>
        /// <param name="Paperless">The paperless.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="dtModule">The dt module.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public int SaveNewFacility(string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int SystemId, int thePreferred, int Paperless, int UserID,int Frequency, DataTable dtModule, Hashtable ht)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                ClsUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                ClsUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                ClsUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                ClsUtility.AddParameters("@image", SqlDbType.VarChar, image);
                ClsUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                ClsUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                ClsUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                ClsUtility.AddParameters("@PepFarStartDate", SqlDbType.DateTime, PepFarStartDate.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                ClsUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                ClsUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                ClsUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                ClsUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                ClsUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                ClsUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                ClsUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                ClsUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                ClsUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                ClsUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());
                ClsUtility.AddParameters("@DosageFrequency", SqlDbType.Int, Frequency.ToString());

                Int32 RowsAffected = (Int32)FacilityManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertFacility_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected <= 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    //Exception ex = AppException.Create("#C1", theBL);
                    //throw ex;
                    AppException.Create("#C1", theBL);
                }

                if (RowsAffected > 0)
                {
                    for (int i = 0; i < dtModule.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, "99999");
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dtModule.Rows[i]["ModuleId"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                        int retval = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveModule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffected < 0)
                        {
                            MsgBuilder theBL = new MsgBuilder();
                            theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                            //Exception ex = AppException.Create("#C1", theBL);
                            //throw ex;
                            AppException.Create("#C1", theBL);
                        }
                    }
                }
                FacilityManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Updates the facility.
        /// </summary>
        /// <param name="FacilityId">The facility identifier.</param>
        /// <param name="FacilityName">Name of the facility.</param>
        /// <param name="CountryID">The country identifier.</param>
        /// <param name="PosID">The position identifier.</param>
        /// <param name="SatelliteID">The satellite identifier.</param>
        /// <param name="NationalID">The national identifier.</param>
        /// <param name="ProvinceId">The province identifier.</param>
        /// <param name="DistrictId">The district identifier.</param>
        /// <param name="image">The image.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="AppGracePeriod">The application grace period.</param>
        /// <param name="dateformat">The dateformat.</param>
        /// <param name="PepFarStartDate">The pep far start date.</param>
        /// <param name="Status">The status.</param>
        /// <param name="SystemId">The system identifier.</param>
        /// <param name="thePreferred">The preferred.</param>
        /// <param name="Paperless">The paperless.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="dtModule">The dt module.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public int UpdateFacility(int FacilityId, string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int Status, int SystemId, int thePreferred, int Paperless, int UserID, int Frequency, DataTable dtModule, Hashtable ht)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                ClsUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                ClsUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                ClsUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                ClsUtility.AddParameters("@image", SqlDbType.VarChar, image.ToString());
                ClsUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                ClsUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                ClsUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                ClsUtility.AddParameters("@PepFarStartDate", SqlDbType.DateTime, PepFarStartDate.ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                ClsUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                ClsUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                ClsUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                ClsUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                ClsUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                ClsUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                ClsUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                ClsUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                ClsUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                ClsUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                ClsUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());
                ClsUtility.AddParameters("@DosageFrequency", SqlDbType.Int, Frequency.ToString());
                int RowsAffected = (Int32)FacilityManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateFacility_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                int DeleteFlag = 0;

                if (DeleteFlag == 0)
                {
                    string theSQL = string.Format("delete from lnk_FacilityModule where FacilityId = {0}", FacilityId);
                    ClsUtility.Init_Hashtable();
                    int Rows = (int)FacilityManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DeleteFlag = 1;
                }
                for (int i = 0; i < dtModule.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dtModule.Rows[i]["ModuleID"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    int RowsAffModule = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveModule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (RowsAffModule == 0)
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                        AppException.Create("#C1", theBL);
                    }
                }

                FacilityManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}