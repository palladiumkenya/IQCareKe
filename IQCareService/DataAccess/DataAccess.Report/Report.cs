using System;
using System.Xml;

namespace DataAccess.Report
{
    [Serializable]
   public class Report
    {
        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>
        /// The report identifier.
        /// </value>
        public int? ReportID { get; protected set; }
        /// <summary>
        /// Sets the report identifier.
        /// </summary>
        /// <param name="reportID">The report identifier.</param>
        public void SetReportID(int reportID)
        {
            this.ReportID = reportID;
        }
        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        public string ReportName { get; set; }
        /// <summary>
        /// Gets or sets the report style sheet.
        /// </summary>
        /// <value>
        /// The report style sheet.
        /// </value>
        public string ReportStyleSheet { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [has report style sheet].
        /// </summary>
        /// <value>
        /// <c>true</c> if [has report style sheet]; otherwise, <c>false</c>.
        /// </value>
        public bool HasReportStyleSheet { get; set; }
        /// <summary>
        /// Gets or sets the size of the report template.
        /// </summary>
        /// <value>
        /// The size of the report template.
        /// </value>
        public int ReportTemplateSize { get; set; }
        /// <summary>
        /// Gets or sets the name of the report template.
        /// </summary>
        /// <value>
        /// The name of the report template.
        /// </value>
        public string ReportTemplateName { get; set; }
        /// <summary>
        /// Gets or sets the name of the report template.
        /// </summary>
        /// <value>
        /// The name of the report template.
        /// </value>
        public string ReportTemplateContentType { get; set; }

        /// <summary>
        /// Gets or sets the report template binary.
        /// </summary>
        /// <value>
        /// The report template binary.
        /// </value>
        public byte[] ReportTemplateBinary
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the report data.
        /// </summary>
        /// <value>
        /// The report data.
        /// </value>
        public XmlDocument ReportData { get; set; }
        /// <summary>
        /// Gets or sets the period from.
        /// </summary>
        /// <value>
        /// The period from.
        /// </value>
        public DateTime? PeriodFrom { get; set; }
        /// <summary>
        /// Gets or sets the period to.
        /// </summary>
        /// <value>
        /// The period to.
        /// </value>
        public DateTime? PeriodTo { get; set; }
        /// <summary>
        /// Gets or sets the name of the facility.
        /// </summary>
        /// <value>
        /// The name of the facility.
        /// </value>
        public string FacilityName { get; set; }
        /// <summary>
        /// Gets or sets the facilty code.
        /// </summary>
        /// <value>
        /// The facilty code.
        /// </value>
        public string FaciltyCode { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        
        
    }
    
}
