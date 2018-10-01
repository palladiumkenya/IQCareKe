using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Records.Interface;
namespace DataAccess.Records.Interface
{
    public interface IUnitOfWork
    {
        int Complete();

        IPersonRepository PersonRepository { get; }
        IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }

        IPersonContactLookUpRepository PersonContactLookUpRepository { get; }

        IServiceAreaIndicatorRepository ServiceAreaIndicatorRepository { get; }
        IPersonRelationshipRepository PersonRelationshipRepository { get; }

        IPersonLocationRepository PersonLocationRepository { get; }
        ILookupRepository LookupRepository { get; }

        IPatientLookupRepository PatientLookupRepository { get; }

        IPersonContactRepository PersonContactRepository { get; }

        IPatientRepository PatientRepository { get; }

        IPatientRegistrationLookupRepository PatientRegistrationLookupRepository { get; }

        IPatientVisitRepository PatientVisitRepository { get; }

        IPatientMasterVisitRepository PatientMasterVisitRepository { get; }

        IPatientEnrollmentRepository PatientEnrollmentRepository { get; }

        IPatientIdentifierRepository PatientIdentifierRepository { get; }

        IPatientEntryPointRepository PatientEntryPointRepository { get; }

        IPersonLookUpRepository PersonLookUpRepository { get; }

        IIdentifierRepository IdentifierRepository { get; }
        IPatientReEnrollmentRepository  PatientReEnrollmentRepository {get;}

        IPersonIdentifierRepository PersonIdentifierRepository { get; }
         
        IServiceAreaIdentifiersRepository ServiceAreaIdentifiersRepository { get; }

        IPatientEncounterRepository PatientEncounterRepository { get; }

        IPersonEmergencyContactRepository PersonEmergencyContactRepository { get; }

        IPersonOccupationRepository PersonOccupationRepository { get; }
        IPersonEducationRepository  PersonEducationRepository { get; }

        IPatientConsentRepository PatientConsentRepository { get; }

    }
}
