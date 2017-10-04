using System;
using Entities.CCC.Lookup;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class PatientRegistrationValidation
    {
        public string ValidateInterOperabilityRegistration(Registration registration)
        {
            try
            {
                LookupLogic lookupLogic = new LookupLogic();
                string cccNumber = null;
                PatientLookup patient = new PatientLookup();
                PatientLookupManager patientLookup = new PatientLookupManager();

                if (!string.IsNullOrWhiteSpace(registration.Patient.DateOfBirth))
                {
                    Exception exception = new Exception("Patient Date of Birth not found. Please include date of birth");
                    throw exception;
                }

                if (!string.IsNullOrWhiteSpace(registration.DateOfEnrollment))
                {
                    Exception exception = new Exception("Patient Date of Enrollment not found. Please include date of enrollment");
                    throw exception;
                }

                if (string.IsNullOrWhiteSpace(registration.EntryPoint))
                {
                    Exception exception = new Exception("Patient Entry Point is null. Please include entry point");
                    throw exception;
                }

                var lookupEntryPoints = lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint", registration.EntryPoint);
                if (lookupEntryPoints.Count == 0)
                {
                    Exception exception = new Exception("Invalid Patient Entry Point supplied");
                    throw exception;
                }

                foreach (var item in registration.InternalPatientIdentifiers)
                {
                    if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority == "CCC")
                    {
                        cccNumber = item.IdentifierValue;
                    }
                }

                if (string.IsNullOrWhiteSpace(cccNumber))
                {
                    var exception = new Exception("CCC Number is not part of the identifiers");
                    throw exception;
                }

                patient = patientLookup.GetPatientByCccNumber(cccNumber);
                if (patient == null)
                {
                    Exception exception = new Exception("Patient does not exist in IQCare");
                    throw exception;
                }

                return "Patient registration validation successful";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
