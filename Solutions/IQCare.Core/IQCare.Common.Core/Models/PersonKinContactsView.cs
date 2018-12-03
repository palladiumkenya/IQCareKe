using System.Collections.Generic;

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

        public int Sex { get; set; }

        public string MobileNo { get; set; }

        public bool DeleteFlag { get; set; }

        public List<LookupItemView> GenderList;

        public List<LookupItemView> ContactCategoryList;

        public List<LookupItemView> ContactRelationshipList;
    }
}