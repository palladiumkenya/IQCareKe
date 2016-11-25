using System;

namespace Application.Common
{
    public class ApplicationAccess
    {
        public struct BillingFeature
        {
            public static string BillingModule = "BILLING_MODULE";
            public static string ManageBillable = "BILLABLES_MANAGE";
            public static string Report = "BILLING_REPORTS";
            public static string ApproveReversals = "BILL_REVERSAL_APPROVAL";
            public static string PaymentType = "BILLING_TYPE";
            public static string Configuration = "BILLING_CONFIGURATION";
            public static string QuickPanel = "BILL_QUICK_PANEL";
            public static string ChequePayment = "BILL_CHEQUE_PAYMENT";
            public static string InsurancePayment = "BILL_INSURANCE_PAYMENT";
            public static string ReceivePayment = "BILL_RECEIVE_PAYMENT";
            public static string WriteOffDebt = "BILL_WRITE_OFF";
            public static string ClientBilling = "CLIENT_BILLING";
            public static string CreditKnockOff = "CREDIT_PAYMENT_KNOCKOFF";

        };
        public static int Enrollment = 1;
        public static int InitialEvaluation = 2;
        public static int AdultPharmacy = 3;
        public static int PaediatricPharmacy = 4;
        public static int Laboratory = 5;
        public static int NonARTFollowup = 6;
        public static int ARTFollowup = 7;
        public static int CareTracking = 8;
        public static int HomeVisit = 9;
        public static int FacilitySetup = 10;
        public static int UserAdministration = 11;
        public static int UserGroupAdministration = 12;
        //public static int CustomiseDropDown = 13;
        public static int HIVCarePatientProfile = 14;
        public static int PatientARVPickup = 15;
        public static int FacilityReports = 16;
        public static int DonorReports = 17;
        public static int DeleteForm = 46;
        public static int DeletePatient = 47;
        public static int CustomReports = 48;
        public static int ConfigureCustomFields = 49;
        public static int Schedular = 50;
        public static int SchedularAppointment = 45;
        public static int Transfer = 56;
        public static int FamilyInfo = 59;
        public static int OrderLabTest = 83;
        public static int PMTCTEnrollment = 117;
        public static int ChildEnrollment = 118;



        //form builder
        public static int FormBuilder = 119;
        public static int Upsize = 120;
        public static int DatabaseMigration = 121;
        public static int DatabaseMerge = 122;
        public static int ManageForms = 123;
        public static int ManageFields = 124;
        public static int PatientRegistration = 126;
        public static int ConfigureHomePages = 127;
        public static int ConfigureCareTermination = 128;
        public static int SpecialFormLinking = 129;
        public static int ManageTechnicalArea = 130;
        //form Project Management Module
        public static int Program = 131;
        public static int DonorName = 132;
        public static int Supplier = 133;
        public static int CostAllocationCategory = 134;
        public static int ItemType = 135;
        public static int LabTestLocation = 136;
        public static int ManufacturerDetail = 137;
        public static int PurchasingDispensingUnit = 138;
        public static int ReturnReason = 139;
        public static int AdjustmentReason = 140;
        public static int DonorProgramLinking = 141;
        public static int DrugType = 142;
        public static int Store = 143;
        public static int SupplierItem = 144;
        public static int ProgramItem = 145;
        public static int ItemTypeSubTypeLinking = 146;
        public static int StoreSourceDestinationLinking = 147;
        public static int StoreUserLinking = 148;
        public static int StoreItem = 149;
        public static int PatientVisitConfiguration = 151;
        public static int BudgetConfiguration = 152;
        public static int DebitNote = 153;
        public static int OpeningStock = 154;
        public static int AdjustStocklevel = 155;
        public static int PurchaseOrder = 156;
        public static int GoodReceiveNotes = 157;
        public static int DrugDispense = 158;
        public static int DisposeItem = 159;
        public static int BatchSummary = 160;
        public static int StockSummary = 161;
        public static int ExpiryReport = 162;
        public static int PriorARTHIVCare = 163;
        public static int ARTCare = 164;
        public static int HIVCareARTEncounter = 165;
        public static int InitialFollowupVisits = 167;
        public static int ARTHistory = 168;
        //**************//
        //John Macharia start
        public static int ARVTherapy = 169;
        //John End
        public static int InterStoreTransfer = 170;
        public static int IssueVoucher = 171;
        //VY for billing 
      //  public static int Billing = 175;
     //   public static int BillingConfiguration = 176;
     //   public static int BillingReports = 178;
     //   public static int BillingReversal = 179;
      //  public static int BillingQuickPanel = 180; //renamed from Consumables;
        /// <summary>
        /// 
        /// </summary>
       // public static int BillingType = 174;
        /// <summary>
        /// The billing cheque payment
        /// </summary>
     //   public static int BillingChequePayment = 181;
        /// <summary>
        /// The billing insurance payment
        /// </summary>
      //  public static int BillingInsurancePayment = 182;
        /// <summary>
        /// The billing payment type
        /// </summary>
       // public static int BillingReceivePayment = 183;
        /// <summary>
        /// The billing payment type
        /// </summary>
       // public static int BillingWriteOffBill = 184;

        /// <summary>
        /// The client billing{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// Direct client billing , generate invoice
        /// </summary>
       // public static int ClientBilling = 187;

        //Added by  vy for customize list 2015-mau-07
      //  public static int ConfigureCustomLists = 186;

      //  public static int Consumables = 180;
        //patients Ward

       // public static int NewWardAdmission = 250;
       //public static int UpdateWardAdmission = 251;
       // public static int DischargePatient = 252;

        public static int GetUserFullName = 253;

        #region "CTC"

        public static int FollowupEducation = 71;
        public static int Pharmacy = 72;
        public static int PatientClassification = 67;
        public static int PatientRecord = 73;


        #endregion

        #region "CustomizeList"
        public class CustomizeMasterList
        {
            public static int AdherenceCodes = 18;
            public static int ARVSideEffects = 19;
            public static int BarrierToCare = 20;
            public static int District = 21;
            public static int Drugs = 22;
            public static int EducationLevel = 23;
            public static int EmergencyContactRelationship = 24;
            public static int Designation = 25;
            public static int Employee = 26;
            public static int EmploymentStatus = 27;
            public static int Laboratory = 28;
            public static int MartialStatus = 29;
            public static int Occupation = 30;
            public static int OIAIDSDefiningIllness = 31;
            public static int PresentingComplaints = 32;
            public static int Province = 33;
            public static int TherapyChangeCode = 34;
            public static int Village = 35;
            public static int PatientReferred = 36;
            //public static int PharmacyUnits = 37;
            public static int DrugFrequency = 37;
            public static int LaboratoryUnit = 53;
            public static int PatientRelationType = 57;
            public static int ARVAdherenceReasonPoor = 81;

            #region CTC
            public static int Region = 60;
            public static int Divison = 62;
            public static int Ward = 64;
            public static int CTC_PatientClassification = 67;
            public static int CTC_ARVAdherenceReason = 65;
            public static int CTC_AIDSDefiningEvents = 68;
            public static int CTC_PatientReferredTo = 82;
            public static int CTC_Regimen = 70;
            #endregion

        }
        #endregion

        public static string DBSecurity = "'ttwbvXWpqb5WOLfLrBgisw=='";
    }

    public class FunctionAccess
    {
        public static int View = 1;
        public static int Update = 2;
        public static int Delete = 3;
        public static int Add = 4;
        public static int Print = 5;
    }
}
