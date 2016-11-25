using System;

namespace Entities.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderedService
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public int OrderId { get; set; }
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public Service Service { get; set; }
        /// <summary>
        /// Gets the service identifier.
        /// </summary>
        /// <value>
        /// The service identifier.
        /// </value>
        public int ServiceId
        {
            get
            {
                return Service.Id;
            }
        }
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName
        {
            get
            {
                return Service.Name;
            }
        }
        /// <summary>
        /// Gets or sets the request notes.
        /// </summary>
        /// <value>
        /// The request notes.
        /// </value>
        public string RequestNotes { get; set; }
        /// <summary>
        /// Gets or sets the result notes.
        /// </summary>
        /// <value>
        /// The result notes.
        /// </value>
        public string ResultNotes { get; set; }
        /// <summary>
        /// Gets the service status.
        /// </summary>
        /// <value>
        /// The service status.
        /// </value>
        public string ServiceStatus
        {
            get
            {
                return ResultBy.HasValue ? "Complete" : "Pending";
            }
        }
        public int Quantity
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the result date.
        /// </summary>
        /// <value>
        /// The result date.
        /// </value>
        public DateTime? ResultDate { get; set; }
        /// <summary>
        /// Gets or sets the result by.
        /// </summary>
        /// <value>
        /// The result by.
        /// </value>
        public int? ResultBy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }


    }

}
