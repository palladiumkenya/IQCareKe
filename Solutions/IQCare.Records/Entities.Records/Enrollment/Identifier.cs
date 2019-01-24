using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records.Enrollment
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

        //public virtual ICollection<ServiceAreaIdentifiers> ServiceAreaIdentifierses { get; set; }
        //public virtual ICollection<PatientEntityIdentifier> PatientEntityIdentifiers { get; set; }
    }
}
