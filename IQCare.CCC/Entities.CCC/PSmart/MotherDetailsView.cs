using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.PSmart
{
    [Serializable][Table("psmart_FamilyInformation")]
   public class MotherDetailsView
    {
       public int Id { get; set; }
       public int  Ptn_pk { get; set; }
       public int  Sex { get; set; }
       public int AgeYear { get; set; }
       public DateTime? RelationshipDate { get; set; }
       public int? RelationshipType { get; set; }
       public int? HivStatus { get; set; }
       public int? HivCareStatus { get; set; }
       public string RegistrationNo { get; set; }
       public string FileNo { get; set; }
       public int ReferenceId { get; set; }
      public int  UserId { get; set; }
       public DateTime? CreateDate { get; set; }
       public DateTime UpdateDate { get; set; }
       public int MovedToFamilyTestingTable { get; set; }
       public string firstName { get; set; }
       public string middleName { get; set; }
       public string lastName { get; set; }
    }
}
