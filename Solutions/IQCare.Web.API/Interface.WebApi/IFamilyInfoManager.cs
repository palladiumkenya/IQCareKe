using Entities.CCC.psmart;

namespace Interface.WebApi
{
    public interface IFamilyInfoManager
    {
        int AddMotherDetails(FamilyInfo familyInfo);
        int EditMotherDetails(FamilyInfo familyInfo);
    }
}