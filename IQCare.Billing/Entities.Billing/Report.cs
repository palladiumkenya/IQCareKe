using System;

namespace Entities.Billing
{
    [Serializable]
    public class Report
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public string ProcedureName { get; set; }
        /// <summary>
        /// Gets or sets the table names.
        /// </summary>
        /// <value>
        /// The table names.
        /// </value>
        public string TableNames { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has patient data.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has patient data; otherwise, <c>false</c>.
        /// </value>
        public bool HasPatientData { get; set; }
    }
}
