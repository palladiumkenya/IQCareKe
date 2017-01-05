using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Entities.Common;

namespace Entities.PatientCore
{
    [Serializable]
    [Table("PatientView")]
    public class Patient: IAuditEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("Ptn_Pk")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the unique facility identifier.
        /// </summary>
        /// <value>
        /// The unique facility identifier.
        /// </value>
        [Column("PatientFacilityID")]
        public string UniqueFacilityId { get; set; }
        /// <summary>
        /// Gets or sets the iq number.
        /// </summary>
        /// <value>
        /// The iq number.
        /// </value>
        public string IQNumber { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
       [Column("DOB")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the date of registration.
        /// </summary>
        /// <value>
        /// The date of registration.
        /// </value>
        [Column("RegistrationDate")]
        public DateTime DateOfRegistration { get; set; }
        /// <summary>
        /// Gets or sets the date of death.
        /// </summary>
        /// <value>
        /// The date of death.
        /// </value>
        public DateTime? DateOfDeath { get; set; }
        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>
        /// The sex.
        /// </value>
        public string Sex { get; set; }
        [NotMapped]
        public bool DeleteFlag { get; set; }
        public int LocationId { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
               return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
            }
        }
        
        public List<PatientConsent> Consent { get; set; }
       
        [NotMapped]
        public double Age
        {
            get
            {
                DateTime today = DateTime.Today;
                if (DateOfDeath.HasValue)
                {
                    today = DateOfDeath.Value;
                   
                }
                // get the last birthday
                int years = today.Year - DateOfBirth.Year;
                DateTime last = DateOfBirth.AddYears(years);
                if (last > today)
                {
                    last = last.AddYears(-1);
                    years--;
                }
                // get the next birthday
                DateTime next = last.AddYears(1);
                // calculate the number of days between them
                double yearDays = (next - last).Days;
                // calcluate the number of days since last birthday
                double days = (today - last).Days;
                // calculate exaxt age
                double exactAge = (double)years + (days / yearDays);
                return exactAge;
            }
        }
        public virtual List<PatientVisit> PatientVisit { get; set; }
        public int CreatedBy
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public string AuditData
        {
            get; set;
        }
    }
}
