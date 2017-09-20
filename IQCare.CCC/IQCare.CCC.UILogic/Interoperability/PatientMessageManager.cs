using System;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class PatientMessageManager
    {
        IPatientMessageManager _mgr = (IPatientMessageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BPatientMessage, BusinessProcess.CCC");

        public PatientMessage GetPatientMessageByEntityId(int entityId)
        {
            try
            {
                return _mgr.GetPatientMessageByEntityId(entityId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
