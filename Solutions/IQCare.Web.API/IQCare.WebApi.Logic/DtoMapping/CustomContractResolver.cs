using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace IQCare.WebApi.Logic.DtoMapping
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public CustomContractResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
            {
                {"FirstName", "FIRST_NAME"},
                {"MiddleName", "MIDDLE_NAME" },
                {"LastName","LAST_NAME" },
                {"DateOfBirth","DATE_OF_BIRTH" },
                {"Sex","SEX" },
                {"Village","VILLAGE" },
                {"Ward","WARD" },
                {"SubCounty","SUB_COUNTY" },
                {"County","COUNTY" },
                {"PhysicalAddress","POSTAL_ADDRESS" },
                {"MobileNumber","PHONE_NUMBER" },
                {"MaritalStatus","MARITAL_STATUS" },
                {"DateOfDeath","DEATH_DATE" },
                {"DeathIndicator","DEATH_INDICATOR" },
                {"TSRelationshipType","RELATIONSHIP" },
                {"MotherMaidenName","MOTHER_MAIDEN_NAME" }
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;

            var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
