using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Neonatal;

namespace IQCare.CCC.UILogic.Encounter
{
    public class ImmunizationLogic
    {
        INeonatalHistory neonatatalHtx = (INeonatalHistory)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BNeonatalHistory, BusinessProcess.CCC");
        public List<PatientImmunizationHistory> getPatientImmunization(int patientId)
        {
            List<PatientImmunizationHistory> immunizationList = new List<PatientImmunizationHistory>();
            try
            {
                immunizationList = neonatatalHtx.getPatientImmunization(patientId);
            }
            catch
            {
                throw;
            }
            return immunizationList;
        }
    }
}
