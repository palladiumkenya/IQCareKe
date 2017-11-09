using Application.Presentation;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class InteropPlacerTypeManager
    {
        IInteropPlacerTypeManager _mgr = (IInteropPlacerTypeManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BInteropPlacerType, BusinessProcess.CCC");
    }
}
