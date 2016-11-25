using System;
using System.Collections.Generic;
using System.Text;

    namespace Application.Common
    {
    [Serializable()]
    public class LoggedInUser
    {
        private const string SESSION_SINGLETON = "SINGLETON";
        private int _PatientID;
        private int _PatientStatus;
        private int _PatientVisitId;
        private int _PatientPharmacyId;
        private int _LocId;
        private string _Prog = string.Empty;

        private LoggedInUser()
        {
        }

        public  int PatientID
        {

            get { return _PatientID; }
            set { _PatientID = value; }

        }
        public  int PatientStatus
        {

            get { return _PatientStatus; }
            set { _PatientStatus = value; }

        }
        public  int PatientVisitId
        {
            get { return _PatientVisitId; }
            set {_PatientVisitId = value;}
        }
        public  int PatientPharmacyId
        {
            get { return _PatientPharmacyId; }
            set { _PatientPharmacyId = value; }

        }
        public  string Program
        {
            get { return _Prog; }
            set { _Prog = value; }
        }
        public Int32 LocationId
        {
            get { return _LocId; }
            set { _LocId = value; }
        }
      public static LoggedInUser GetCurrentSingleton()
      {
        LoggedInUser oSingleton;

        if (null == System.Web.HttpContext.Current.Session[SESSION_SINGLETON])
        {
            oSingleton = new LoggedInUser();
            System.Web.HttpContext.Current.Session[SESSION_SINGLETON] = oSingleton;
        }
        else
        {
            oSingleton = (LoggedInUser)System.Web.HttpContext.Current.Session[SESSION_SINGLETON];
        }
        return oSingleton;
      }
    }



    }
