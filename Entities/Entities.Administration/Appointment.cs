using System;
using System.Collections.Generic;

namespace Entities.Administration
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class Appointment
    {
        /// <summary>
        /// Gets or sets the appointment date.
        /// </summary>
        /// <value>
        /// The appointment date.
        /// </value>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        /// <value>
        /// The appointment identifier.
        /// </value>
        public int? AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the booked by.
        /// </summary>
        /// <value>
        /// The booked by.
        /// </value>
        public KeyValuePair<int, string> BookedBy { get; set; }

        /// <summary>
        /// Gets or sets the date met.
        /// </summary>
        /// <value>
        /// The date met.
        /// </value>
        public DateTime? DateMet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Appointment"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public Boolean Deleted { get; set; }

        public KeyValuePair<int, string> Location { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the patient enrollment identifier.
        /// </summary>
        /// <value>
        /// The patient enrollment identifier.
        /// </value>
        public string PatientEnrollmentId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the patient status.
        /// </summary>
        /// <value>
        /// The patient status.
        /// </value>
        public string PatientStatus { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public KeyValuePair<int, string> Provider { get; set; }

        /// <summary>
        /// Gets or sets the purpose.
        /// </summary>
        /// <value>
        /// The purpose.
        /// </value>
        public KeyValuePair<int, string> Purpose { get; set; }

        /// <summary>
        /// Gets or sets the service area.
        /// </summary>
        /// <value>
        /// The service area.
        /// </value>
        public KeyValuePair<int, string> ServiceArea { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public KeyValuePair<int, string> Status { get; set; }

        /// <summary>
        /// Gets or sets the status date.
        /// </summary>
        /// <value>
        /// The status date.
        /// </value>
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Gets or sets the visit identifier.
        /// </summary>
        /// <value>
        /// The visit identifier.
        /// </value>
        public int? VisitId { get; set; }
    }
}