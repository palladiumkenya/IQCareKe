using System;
using System.Collections;
using System.Data;
using System.Configuration;
using Application.Common;


namespace Application.Presentation
    {
   public  enum MenuChoice
    {
        Dispense = 1,
        Register,
        CounterRequistion,
        IssueVoucher,
        CRWithIV,
        PurchaseOrder,
        GoodReceived,
        POWithGRN
    }
    public class CustomFormUIManager
    {
        public static string InputCssClass = "form-control";
        public static string LabelCssClass = "control-label pull-left";
        public static string FormWrapperCssClass = "container-fluid";
        public static string FormMarginCssClass = "col-md-1";
        public static string FormColumnSeparatorCssClass = "col-md-2";
        public static string FormColumnCssClass = "col-md-4";
        public static string FormRowCssClass = "form-group  col-md-10";
    }
    /// <summary>
    /// This class is applicable only with windows application of IQCare.
    /// </summary>
    public  class GblIQCare
    {

        public GblIQCare()
        {
            AppVersion = "Ver 2.0.0 Kenya HMIS";
            ReleaseDate = "10-Jan-2019";
            VersionName = "Kenya HMIS Ver 2.0.0 BETA";
            DbVersion = "Ver 2.0.0 Kenya HMIS";
        }
        public static int iFormMode;
        public static MenuChoice CurrentMenu;
        public int iUserId;
        public static string strAppVersion;
        public static DateTime dtmAppVersion;

        #region "Application Parameters"
        public static string AppVersion = "Ver 2.0.0 Kenya HMIS";
        public static string DbVersion = "Ver 2.0.0 Kenya HMIS";
        public static string ReleaseDate = "10-Jan-2019";
        public static string VersionName = "Kenya HMIS Ver 2.0.0 BETA";
        #endregion

        #region "Public Variables"
        /// <summary>
        /// This class is applicable for login form
        /// Meetu Rahul
        /// </summary>
        public static int AppUserId;
        public static string AppUserName;
        public static string AppUName;
        public static int EnrollFlag;
        public static DataTable dtUserRight;
        public static int SystemId;
        public static string AppCountryId;
        public static int AppLocationId;
        public static string AppLocation;
        public static string AppPosID;
        public static string AppSatelliteId;
        public static string AppGracePeriod;
        public static string AppDateFormat;
        public static string BackupDrive;
        public static string pwd;
        public static string CurrentDate;
        public static DataTable dtModules;
        public static DataSet dsTreeView;

        /// <summary>
        /// This class is applicable for Businessrule Screen
        /// </summary>
        public static string strDisplayType ;
        public static string strControlReferenceId;
        public static string strFieldName ;
        public static string strTempName = "";
        public static string strCount = "";
        public static DataTable dtBusinessValues = new DataTable();
        public static DataSet dsBusinessRuleVal = new DataSet();
        public static DataTable dtTempValue = new DataTable();
        /// <summary>
        /// This class is applicable for Select List Screen
        /// </summary>

        public static string strSelectFieldName = "";
        public static Hashtable hashTblSelectList = new Hashtable();
        public static string strSelectListValue = "";
        public static DataTable dtICD10Code = new DataTable();
        /// <summary>
        /// This class is applicable for Field Details Screen
        /// </summary>
        public static Hashtable objHashtbl = new Hashtable();
        public static bool blnFieldDetailsChange = false;
        public static bool blhselectlistChange=false;
        public static bool blnBusinessRuleChange = false;
        public static int intFieldDetaisChange = 0;
        public static string strRetainSelectList = "";
        public static string strRetainSelectField = "";
        public static string strgblPredefined = "";
        public static Hashtable objhashSelectList = new Hashtable();
        public static int gblRowIndex = 0;
        public static Hashtable objhashBusinessRule = new Hashtable();
        /// <summary>
        /// This class is applicable for Form Details Screen
        /// </summary>
        public static int iFormBuilderId;
        public static int ResetSection;
        public static int RefillDdlFields;
        public static int ModuleId;
        public static int iHomePageId;
        public static string ModuleName="";
        public static int UpdateFlag = 0;
        public static int Identifier = 0;
        public static int PharmacyFlag = 0;
        public static string Query = "";
        public static int Scheduler = 0;
        public static int iManageCareEnded;
        public static int iCareEndedId=0;
        public static bool blnIsPatientHomePage = false;

        public static string ModuleDisplayName = "";
        public static bool ModuleCanEnroll = false;

        public static DataTable dtPrintLabel = new DataTable();
        public static string PatientName = "";
        public static string IQNumber = "";
        public static string StoreName = "";
        public static string AppLocTelNo = "";


        /// <summary>
        /// This class is applicable for Associated Field
        /// </summary>
        public static int iFieldId=0;
        public static int iModuleId = 0;
        public static string strMainGrdFldName = "";
        public static DataTable dtConditionalFields = new DataTable();
        public static string strPredefinevalue = "";
        public static int iConditionalbtn = 0;
        public static DataSet DsFieldDetailsCon = new DataSet();
        
        /// <summary>
        /// This class is applicable for CareEndConditional Field
        /// </summary>
        public static DataTable dtCareEndConditional = new DataTable();

        public static string strSelectList = "";
        public static string strSelectListstr = "";
        public static string strCareEndFeatureName = "";
        public static int CareEndconFlag = 0;
        public static string strYesNo = "";
        public static string strCareEndcon = "";
        public static string strfoll = "0";

        /// <summary>
        /// This class is applicable for Lab Configuration Field
        /// </summary>
        public static int LabTestId = 0;
        
        //Item List Common Variable
        public static string ItemLabel = "";
        public static string ItemCategoryId = "";
        public static string ItemTableName = "";
        public static int ItemFeatureId = 0;

        /// <summary>
        /// Variables releated to ItemMaster
        /// </summary>
        public static string theItemId = "";

        /// <summary>
        /// Variables for UserStore Selection
        /// </summary>
        public static string theArea = "";
        public static int intStoreId = 0;
        public static int PurchaseOrderID = 0;
        /// <summary>
        /// if ModePurchaseOrder = 0 then No interstore transfer and no Purchase Order  Selected
        /// if ModePurchaseOrder = 1 then  Purchase Order  Selected
        /// if ModePurchaseOrder = 2 then  Interstore transfer  Selected
        /// </summary>
        public static int ModePurchaseOrder = 0;

        /// <summary>
        /// Variables releated to PatientRegistration
        /// </summary>
        public static string strPatientRegistrationInsert = "I";
        public static int patientID;

       
        public static string strserviceareaname = "";
        public static DataTable dtServiceBusinessValues = new DataTable();
        public static bool blnSingleVisit = false;
        public static bool blnMultivisit = false;
        public static bool blnSignatureOneachpage = false;
        public static bool blncontrolunabledesable = true;
        public static DataTable dtformBusinessValues = new DataTable();

        #endregion
    /*
        public static Boolean HasFeatureRightByReferenceID(string ReferenceID, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ReferenceID = '" + ReferenceID + "'";
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean HasFunctionRightByReferenceID(string ReferenceID, int FunctionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ReferenceID = '" + ReferenceID + "' and FunctionId = " + FunctionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean HasFeatureRight(int FeatureId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + FeatureId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean HasFunctionRight(int FeatureId, int FunctionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + FeatureId.ToString() + " and FunctionId = " + FunctionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
      
        
        /// <summary>
        /// Determines whether [has feature permission] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <returns></returns>
        public static bool HasFeaturePermission(string featureReferenceId, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            theDV.RowFilter = "ReferenceId = '" + featureReferenceId + "'";
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether [has feature permission] [the specified feature identifier].
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <returns></returns>
        public static bool HasFeaturePermission(int featureId, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            theDV.RowFilter = "FeatureId = " + featureId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether [has function right] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <param name="functionId">The function identifier.</param>
        /// <returns></returns>
        public static bool HasFunctionRight(string featureReferenceId, int functionId, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            theDV.RowFilter = "ReferenceId = '" + featureReferenceId.ToString() + "' and FunctionId = " + functionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether [has function right] [the specified feature identifier].
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <param name="functionId">The function identifier.</param>
        /// <returns></returns>
        public static bool HasFunctionRight(int featureId, int functionId, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            theDV.RowFilter = "FeatureId = " + featureId.ToString() + " and FunctionId = " + functionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has function right] [the specified feature reference identifier].
        /// </summary>
        /// <param name="featureReferenceId">The feature reference identifier.</param>
        /// <param name="access">The access.</param>
        /// <returns></returns>
        public static bool HasFunctionRight(string featureReferenceId, FunctionAccess access, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            theDV.RowFilter = "ReferenceId = '" + featureReferenceId.ToString() + "' and FunctionId = " + access.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has module right] [the specified module identifier].
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="clinical">if set to <c>true</c> [clinical].</param>
        /// <returns></returns>
        public static bool HasModuleRight(int moduleId, bool clinical, DataTable UserRights)
        {
            DataView theDV = new DataView(UserRights);
            if (clinical)
            {
                theDV.RowFilter = string.Format("ModuleId = {0}", moduleId);
            }
            else
            {
                theDV.RowFilter = string.Format("ModuleId = {0} And FeatureTypeName='{1}'", moduleId, "MODULE_ACTION");
            }
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        /// <summary>
        /// Gets the base path.
        /// </summary>
        /// <value>
        /// The base path.
        /// </value>
        public static string BasePath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
                    //(ConfigurationManager.AppSettings.Get("ApplicationPath"));
            }
        }

        /// <summary>
        /// Gets the setting paths.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        static string GetSettingPaths(string key)
        {
            return (BasePath + "\\" + ConfigurationManager.AppSettings.Get(key)+"\\");
        }
        /// <summary>
        /// This function is used to get the full path of image folder
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            return GetSettingPaths("ImagePath");
                //(BasePath+"\\"+ ConfigurationManager.AppSettings.Get("ImagePath"));
        }
        /// <summary>
        /// Weburls this instance.
        /// </summary>
        /// <returns></returns>
        public static string weburl()
        {
            return GetSettingPaths("webfindaddpatientUrl");
                //(ConfigurationManager.AppSettings.Get("webfindaddpatientUrl"));
        }
        /// <summary>
        /// Presentations the image path.
        /// </summary>
        /// <returns></returns>
        public static string PresentationImagePath()
        {
            return GetSettingPaths("PresentationImagePath"); 
                //(ConfigurationManager.AppSettings.Get("PresentationImagePath"));
        }
        /// <summary>
        /// This function is used to get the full path of image folder
        /// </summary>
        /// <returns></returns>
        public static string GetXMLPath()
        {
            return GetSettingPaths("XMLFilesPath");  
                //(ConfigurationManager.AppSettings.Get("XMLFilesPath"));
        }

        /// <summary>
        /// Gets the excel path.
        /// </summary>
        /// <returns></returns>
        public static  string GetExcelPath()
        {
            return GetSettingPaths("ExcelFilesPath");
                //(ConfigurationManager.AppSettings.Get("ExcelFilesPath"));
        }
        /// <summary>
        /// Gets the report path.
        /// </summary>
        /// <returns></returns>
        public static  string GetReportPath()
        {
            return GetSettingPaths("ReportsPath");
                //(ConfigurationManager.AppSettings.Get("ReportsPath"));
        }
        /// <summary>
        /// Gets the fieldvalidation report path.
        /// </summary>
        /// <returns></returns>
        public  static string GetFieldvalidationReportPath()
        {
            return GetSettingPaths("Rptfieldvalidation");
                //(ConfigurationManager.AppSettings.Get("Rptfieldvalidation"));
        }

    }
 }
