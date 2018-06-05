using System;
using Entities.CCC.PSmart;
namespace DataAccess.WebApi.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IApiInboxRepository ApiInboxRepository { get; }
        IApiOutboxRepository ApiOutboxRepository { get; }
        IApiInteropSystemsRepository ApiInteropSystemsRepository { get; }
        ISmartCardPatientListRepository SmartCardPatientListRepository { get; }
        IPSmartAuthRepository PSmartAuthRepository { get; }
        IShrRepository ShrRepository { get; }
        IPatientIdentificationRepository PatientIdentificationRepository { get; }
        IExternalPatientIdRepository ExternalPatientIdRepository { get; }
        IInternalPatientIdRepository InternalPatientIdRepository { get; }
        IPatientNameRepository PatientNameRepository { get; }
        IPhysicalAddressRepository PhysicalAddressRepository { get; }
        IPatientAddressRepository PatientAddressRepository { get; }
        IMotherNameRepository MotherNameRepository { get; }
        IMotherIdentifierRepository MotherIdentifierRepository { get; }
        IMotherDetailsRepository MotherDetailsRepository { get; }
        INokNameRepository NokNameRepository { get; }
        INextOfKinRepository NextOfKinRepository { get; }
        IProviderDetailsRepository ProviderDetailsRepository { get; }
        IHivTestRepository HivTestRepository { get; }
        IImmunizationRepository ImmunizationRepository { get; }
        ICardDetailsRepository CardDetailsRepository { get; }

        IFamilyInfoRepository FamilyInfoRepository { get; }
        IHivTestTrackerRepository HivTestTrackerRepository { get; }
        IImmunizationTrackerRepository ImmunizationTrackerRepository { get; }
        IMstPatientReposiroty MstPatientReposiroty { get; }
        IPatientProgramStartRepository PatientProgramStartRepository { get; }
        IMotherDetailsViewRepository MotherDetailsViewRepository { get; }
    }
}