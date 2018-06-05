using System;
using Application.Presentation;
using Interface.CCC.Interoperability;
using Entities.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class InteropPlacerTypeManager
    {
        IInteropPlacerTypeManager _mgr = (IInteropPlacerTypeManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BInteropPlacerType, BusinessProcess.CCC");

        public InteropPlacerType GetInteropPlacerTypeByName(string name)
        {
            try
            {
                return _mgr.GetInteropPlacerTypeByName(name);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
