using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientIdentifierManager
    {
        IPatientIdentifierManager _mgr = (IPatientIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientIdentifier, BusinessProcess.CCC");

        public int addPatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            int returnValue;
            returnValue = _mgr.AddPatientIdentifier(patientIdentifier);
            return returnValue;
        }

        public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId,
            int identifierTypeId)
        {
            return _mgr.GetPatientEntityIdentifiers(patientId, patientEnrollmentId, identifierTypeId);
        }
    }
}
