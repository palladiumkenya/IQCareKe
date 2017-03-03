using Application.Presentation;
using DataAccess.Base;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
    public class PatientDisclosureManager
    {
        private IPatientDisclosureManager _mgr = (IPatientDisclosureManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientDisclosure, BusinessProcess.CCC");
        private int _retval;

        public int AddPatientDisclosure(int patientId, int patientMasterVisitId, string category, string disclosureStage, DateTime disclosureDate)
        {
            PatientDisclosure patientDisclosure = new PatientDisclosure
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Category = category,
                DisclosureStage = disclosureStage,
                DisclosureDate = disclosureDate
            };

            return _retval = _mgr.AddPatientDisclosure(patientDisclosure);
        }

        public int UpdatePatientDisclosure(PatientDisclosure patientDisclosure)
        {
            return _mgr.UpdatePatientDisclosure(patientDisclosure);
        }

        public List<PatientDisclosure> GetPatientDisclosure(int patientId, string category, string disclosureStage)
        {
            return _mgr.GetPatientDisclosures(patientId, category, disclosureStage);
        }
    }
}
