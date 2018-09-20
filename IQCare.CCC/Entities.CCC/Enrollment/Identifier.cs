using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("Identifiers")]
    public class Identifier : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string PrefixType { get; set; }
        public string SuffixType { get; set; }
        public string AssigningAuthority { get; set; }
        public int IdentifierType { get; set; }
        public string IdentifierValueSeparator { get; set; }
        public string ValidatorRegex { get; set; }
        public string FailedValidationMessage { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        //public virtual ICollection<ServiceAreaIdentifiers> ServiceAreaIdentifierses { get; set; }
        //public virtual ICollection<PatientEntityIdentifier> PatientEntityIdentifiers { get; set; }
    }
}
