using Common.Core.Model;

namespace Config.Core.Model
{

    public class ServiceArea:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return $"{Code} - {DisplayName}";
        }
    }
}