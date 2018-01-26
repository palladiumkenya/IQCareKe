using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability.Observation
{
    public class PatientVitalsMessageManager
    {
        IPatientVitalsMessageManager _mgr = (IPatientVitalsMessageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BPatientVitalsMessage, BusinessProcess.CCC");

        public PatientVitalsMessage GetPatientVitalsMessageByPatientIdPatientMasterVisitId(int patientId,
            int patientMasterVisitId)
        {
            try
            {
                return _mgr.GetPatientVitalsMessageByPatientIdPatientMasterVisitId(patientId, patientMasterVisitId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
