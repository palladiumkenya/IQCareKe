
using Entities.CCC.PSmart;

namespace Interface.WebApi
{
    public interface IMotherDetailsViewManager
    {
        string FindExistingMother(MotherDetailsView motherdetailsView);
    }
}