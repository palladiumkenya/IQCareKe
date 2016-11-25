using System.Configuration;

namespace IQCare.CustomConfig
{
    public class ReportInstanceElement : ConfigurationElement
    {

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [ConfigurationProperty("code", IsKey = true, IsRequired = true)]
        public string Code
        {
            get { return (string)base["code"]; }
            set { base["code"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the connection.
        /// </summary>
        /// <value>
        /// The name of the connection.
        /// </value>
        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return (string)base["description"]; }
            set { base["description"] = value; }
        }

        /// <summary>
        /// Gets or sets the file dir.
        /// </summary>
        /// <value>
        /// The file dir.
        /// </value>
        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get { return (string)base["fileName"]; }
            set { base["fileName"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        [ConfigurationProperty("procedureName", IsRequired = true)]
        public string ProcedureName
        {
            get { return (string)base["procedureName"]; }
            set { base["procedureName"] = value; }
        }
        /// <summary>
        /// Gets or sets the table names.
        /// </summary>
        /// <value>
        /// The table names.
        /// </value>
        [ConfigurationProperty("tableName", IsRequired = true)]
        public string TableNames
        {
            get { return (string)base["tableName"]; }
            set { base["tableName"] = value; }
        }
        [ConfigurationProperty("hasPatientData", IsRequired = true, DefaultValue=true)]
        public bool HasPatientData
        {
            get { return (bool)base["hasPatientData"]; }
            set { base["hasPatientData"] = value; }
        }
        /// <summary>
        /// Gets or sets the allowed roles.
        /// </summary>
        /// <value>
        /// The allowed roles.
        /// </value>
        [ConfigurationProperty("allowedRoles", IsRequired = true, DefaultValue = "All")]
        [SettingsDescription("Roles with permission to view the report. e,g cashier,accountats. should be comma separated with no white space in between. All = all roles, None= No role is allowed")]
        public string AllowedRoles
        {
            get { return (string)base["allowedRoles"]; }
            set { base["allowedRoles"] = value; }
        }

    }
}