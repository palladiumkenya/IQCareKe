using Application.Presentation;
using Entities.CCC.psmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class FamilyInfoManager
    {
        private readonly IFamilyInfoManager  familyInfoManager = (IFamilyInfoManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BFamilyInfoManager, BusinessProcess.WebApi");

        public int AddMotherDetails(FamilyInfo familyInfo)
        {
            return familyInfoManager.AddMotherDetails(familyInfo);
        }

        public int EditMotherDetails(FamilyInfo familyInfo)
        {
            return familyInfoManager.EditMotherDetails(familyInfo);
        }
    }
}