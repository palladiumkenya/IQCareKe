using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.psmart;
using Interface.CCC.psmart;

namespace BusinessProcess.CCC.psmart
{
    public class BPsmartShrManager :ProcessBase, IPsmartShrManager
    {
        public EXTERNALPATIENTID GetExternalpatientid(int personId)
        {
            try {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var externalPatientId = unitOfWork.ExternalPatientIdRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return externalPatientId;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<INTERNALPATIENTID> GetInternalpatientids(int personId)
        {
            try {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var internalPatientId = unitOfWork.InternalPatientIdRepository.FindBy(x => x.personId == personId).ToList();
                    unitOfWork.Dispose();
                    return internalPatientId;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTADDRESS GetPatientaddress(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var patientAddress = unitOfWork.PatientAddressRepository.FindBy(x => x.PersonId==personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return patientAddress;
                }                  
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CARDDETAILS GetPatientCarddetails(int personId, int patientId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var cardDetails = unitOfWork.CardDetailsRepository.FindBy(x => x.PatientId == patientId && x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return cardDetails;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public HIVTEST GetPatientHivtest(int personId, int patientId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var hivTest = unitOfWork.HivTestRepository.FindBy(x => x.PatientId == patientId && x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return hivTest;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTIDENTIFICATION GetPatientidentification(int personId, int patientId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var patientIdentification = unitOfWork.PatientIdentificationRepository.FindBy(x => x.PatientId == patientId && x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return patientIdentification;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IMMUNIZATION GetPatientImmunization(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var immunization = unitOfWork.ImmunizationRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return immunization;
                }
                   
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MOTHERDETAILS GetPatientMotherdetails(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherDetails = unitOfWork.MotherDetailsRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return motherDetails;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MOTHERIDENTIFIER> GetPatientMotheridentifier(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherIdentifier = unitOfWork.MotherIdentifierRepository.FindBy(x => x.PersonId == personId).ToList();
                    unitOfWork.Dispose();
                    return motherIdentifier;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MOTHERNAME GetPatientMothername(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherName = unitOfWork.MotherNameRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return motherName;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PATIENTNAME GetPatientname(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var patientName = unitOfWork.PatientNameRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return patientName;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NEXTOFKIN GetPatientNextofkin(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var nextOfKin = unitOfWork.NextOfKinRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return nextOfKin;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NOKNAME GetPatientNokname(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var nokName = unitOfWork.NokNameRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return nokName;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PHYSICALADDRESS GetPatientPhysicaladdress(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var physicalAddress = unitOfWork.PhysicalAddressRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return physicalAddress;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PROVIDERDETAILS GetPatientProviderdetails(int personId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var providerDetails = unitOfWork.ProviderDetailsRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
                    unitOfWork.Dispose();
                    return providerDetails;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}