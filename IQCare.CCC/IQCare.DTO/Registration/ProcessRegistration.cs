using System;
using IQCare.CCC.UILogic;

namespace IQCare.DTO.Registration
{
    public class ProcessRegistration
    {
        public void Save(DTO.Registration registration)
        {
            PersonManager personManager = new PersonManager();
            PatientManager patientManager = new PatientManager();
            LookupLogic lookupLogic = new LookupLogic();
            
            string gender;// = registration.Patient.Sex == "F"? "Female"
            switch (registration.Patient.Sex)
            {
                case "F":
                    gender = "Female";
                    break;
                case "M":
                    gender = "Male";
                    break;
                default:
                    gender = "";
                    break;
            }

            int sex = lookupLogic.GetItemIdByGroupAndItemName("Gender", gender)[0].ItemId;

            DateTime DOB = registration.Patient.DateOfBirth.HasValue? registration.Patient.DateOfBirth.Value: DateTime.MinValue;

            int personId = personManager.AddPersonUiLogic(registration.Patient.FirstName, registration.Patient.MiddleName,
                registration.Patient.LastName, sex, 1, DOB,
                registration.Patient.DobPrecision);

            //patientManager.AddPatient()

        }
    }
}
