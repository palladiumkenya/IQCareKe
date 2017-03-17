using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Lab
{
    /// <summary>
    /// Lab Test master
    /// </summary>
    [Serializable]
    [Table("mst_LabTestMaster", Schema="dbo")]
    public class LabTest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        //[ForeignKey("LabOrderId")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is group.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is group; otherwise, <c>false</c>.
        /// </value>
        public bool IsGroup { get; set; }
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        [ForeignKey("Department")]
        public int DepartmentId
        {
            get
            {       
                return null == Department? -1 : Department.Id;
            }
        }
           [NotMapped]
        public string LoincCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
       [NotMapped]
        public string DepartmentName
        {
            get
            {
                return null == Department? "" : Department.Name;
            }
        }
        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public virtual TestDepartment Department { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
       [NotMapped]
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LabTest" /> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }

        public bool Active { get; set; }
        
        /// <summary>
        /// Gets or sets the test parameter.
        /// </summary>
        /// <value>
        /// The test parameter.
        /// </value>
        public virtual List<TestParameter> TestParameter { get; set; }
        /// <summary>
        /// Gets or sets the parameter count.
        /// </summary>
        /// <value>
        /// The parameter count.
        /// </value>
        public int ParameterCount { get; set; }
    }
   
}
