using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IQCare.Web.UILogic
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SessionManager
    {
        /// <summary>
        /// The session manager
        /// </summary>
        private const string SESSION_MANAGER = "IQ.Session.Manager";

        /// <summary>
        /// Prevents a default instance of the <see cref="SessionManager"/> class from being created.
        /// </summary>
        private SessionManager() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        static SessionManager Instance
        {
            get
            {
                HttpContext context = HttpContext.Current;

                SessionManager manager = context.Session[SESSION_MANAGER] as SessionManager;

                if (manager == null)
                {
                    manager = new SessionManager();
                    context.Session[SESSION_MANAGER] = manager;
                }
                return manager;
            }
        }
  
        /// <summary>
        /// Gets or sets the patient identifier.  assgined ptn_pk from mst_Patient
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public static int PatientId { get { return Instance._patientId; } set { Instance._patientId = value; } }
        /// <summary>
        /// Gets or sets the patient pk. Assigned PatientId from Patient table
        /// </summary>
        /// <value>
        /// The patient pk.
        /// </value>
        public static int PatientPk { get { return Instance._patientPk; } set { Instance._patientPk = value; } }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier. current user logged in
        /// </value>
        public static int UserId { get { return Instance._userId; } set { Instance._userId = value; } }
        /// <summary>
        /// Gets or sets the location identifier. Facility user id logged in to
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public static int FacilityId { get { return Instance._facilityId; } set { Instance._facilityId = value; } }
        /// <summary>
        /// Gets or sets the service area identifier. Module user is accessing
        /// </summary>
        /// <value>
        /// The service area identifier.
        /// </value>
        public static int ServiceAreaId { get { return Instance._serviceAreaId; } set { Instance._serviceAreaId = value; } }
        /// <summary>
        /// Gets or sets the module identifier.  Module user is accessing
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public static int ModuleId { get { return Instance._moduleId; } set { Instance._moduleId = value; } }

        public static int SystemId { get { return Instance._systemId; } set { Instance._systemId = value; } }
        public static int VisitId { get { return Instance._visitId; } set { Instance._visitId = value; } }
        public static CurrentSession CurrentUser { get { return Instance._cUser; } set { Instance._cUser = value; } }

        private int _patientId;
        private int _patientPk;
        private int _userId;
        private int _visitId;
        private int _facilityId;
        private int _serviceAreaId;
        private int _moduleId;
        private int _systemId;
        private CurrentSession _cUser;
        public static void Dispose()
        {
            //Cleanup this object so that GC can reclaim space
           HttpContext.Current.Session.Remove(SESSION_MANAGER);
        }
    }
}
