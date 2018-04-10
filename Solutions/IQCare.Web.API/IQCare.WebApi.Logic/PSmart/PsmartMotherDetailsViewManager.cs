using Application.Presentation;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class PsmartMotherDetailsViewManager
    {
        private readonly IMotherDetailsViewManager motherDetailsViewManager = (IMotherDetailsViewManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BMotherDetailsViewManager, BusinessProcess.WebApi");

        public string FindExistingMother(MotherDetailsView motherDetailsView)
        {
            return motherDetailsViewManager.FindExistingMother(motherDetailsView);
        }
    }
}