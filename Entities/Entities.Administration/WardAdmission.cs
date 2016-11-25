using System;

namespace Entities.Administration
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WardAdmission
    {

        /// <summary>
        /// The admission identifier AdmissionID
        /// </summary>
        public int? AdmissionID { get; set; }
        /// <summary>
        /// The ward identifier
        /// </summary>
        public int WardID{get;set;}
        /// <summary>
        /// The ward name
        /// </summary>
        public string WardName { get; set; }
        /// <summary>
        /// The patient identifier
        /// </summary>
        public int PatientID { get; set; }
        /// <summary>
        /// The patient names
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the patient number.
        /// </summary>
        /// <value>
        /// The patient number.
        /// </value>
        public string PatientNumber { get; set; }
                /// <summary>
        /// The admission date
        /// </summary>
        public DateTime AdmissionDate { get; set; }
        /// <summary>
        /// The bed number
        /// </summary>
        public string BedNumber { get; set; }
        /// <summary>
        /// The admission number
        /// </summary>
        public string AdmissionNumber { get; set; }
        /// <summary>
        /// The referred from
        /// </summary>
        public string ReferredFrom { get; set; }
        /// <summary>
        /// The expected discharge
        /// </summary>
        public DateTime? ExpectedDischarge { get; set; }
        /// <summary>
        /// The discharge date
        /// </summary>
        public DateTime? DischargeDate { get; set; }
        /// <summary>
        /// The discharged by
        /// </summary>
        public int? DischargedBy { get; set; }
        /// <summary>
        /// The user identifier
        /// </summary>
        public int AdmittedBy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WardAdmission"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WardAdmission"/> is discharged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if discharged; otherwise, <c>false</c>.
        /// </value>
        public bool Discharged { get; set; }

    }
}
