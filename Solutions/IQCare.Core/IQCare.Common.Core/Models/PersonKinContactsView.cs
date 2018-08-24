namespace IQCare.Common.Core.Models
{
    public class PersonKinContactsView
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int SupporterId { get; set; }

        public int ContactCategory { get; set; }

        public int? ContactRelationship { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string MobileNo { get; set; }
        public bool DeleteFlag { get; set; }
    }
}