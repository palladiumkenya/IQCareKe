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
        private int retval;

        public int AddPatientDisclosure(int PatientId, int PatientMasterVisitId, string Category, string DisclosureStage, DateTime DisclosureDate)
        {
            PatientDisclosure patientDisclosure = new PatientDisclosure
            {
                PatientId = PatientId,
                PatientMasterVisitId = PatientMasterVisitId,
                Category = Category,
                DisclosureStage = DisclosureStage,
                DisclosureDate = DisclosureDate
            };

            return retval = _mgr.AddPatientDisclosure(patientDisclosure);
        }
    }
}
