using System;
using Entities.Administration;

namespace Entities.PatientCore
{
    /// <summary>
    /// 
    /// </summary>
     [Serializable]
    public class PatientIdentifier
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
         public int PatientId { get; set; }
         /// <summary>
         /// Gets or sets the patient.
         /// </summary>
         /// <value>
         /// The patient.
         /// </value>
         public Patient Patient { get; set; }
         /// <summary>
         /// Gets or sets the identifier.
         /// </summary>
         /// <value>
         /// The identifier.
         /// </value>
         public Identifier Identifier { get; set; }
         /// <summary>
         /// Gets or sets the value.
         /// </summary>
         /// <value>
         /// The value.
         /// </value>
         public string Value { get; set; }
    }
}
