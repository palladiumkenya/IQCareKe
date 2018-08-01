using System;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class InteropPlacerValuesManager
    {
        IInteropPlacerValuesManager _mgr = (IInteropPlacerValuesManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BInteropPlacerValues, BusinessProcess.CCC");


        public int AddInteropPlacerValue(InteropPlacerValues interopPlacerValues)
        {
            return _mgr.AddInteropPlacerValue(interopPlacerValues);
        }

        public InteropPlacerValues GetInteropPlacerValues(int interopPlacerTypeId, int identifierType, string placerValue)
        {
            try
            {
                return _mgr.GetInteropPlacerValues(interopPlacerTypeId, identifierType, placerValue);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
