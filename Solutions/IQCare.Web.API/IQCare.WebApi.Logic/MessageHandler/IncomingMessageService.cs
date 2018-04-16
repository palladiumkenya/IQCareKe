using AutoMapper;
using Entity.WebApi;
using Interface.WebApi;
using IQCare.WebApi.Logic.DtoMapping;
using IQCare.WebApi.Logic.MappingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using IQCare.CCC.UILogic.Interoperability;
using IQCare.CCC.UILogic.Interoperability.Appointment;
using IQCare.CCC.UILogic.Interoperability.Enrollment;
using IQCare.DTO;
using IQCare.DTO.PatientAppointment;
using IQCare.DTO.PatientRegistration;
using IQCare.WebApi.Logic.PSmart;
using Entity.WebApi.PSmart;
using Application.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.PSmart;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO.PSmart;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public class IncomingMessageService : IIncomingMessageService
    {
        private readonly IApiInboxManager _apiInboxmanager;
        private readonly IDtoMapper _dtoMapper;
        private readonly IPsmartStoreManager _psmartStoreManager;
        private readonly IPSmartAuthManager _pSmartAuthManager;
        public IncomingMessageService()
        {
            _apiInboxmanager = new ApiInboxManager();
            _dtoMapper = new DtoMapper();
        }

        public IncomingMessageService(IApiInboxManager apiInboxmanager, IDtoMapper dtoMapper, IPsmartStoreManager psmartStoreManager, IPSmartAuthManager pSmartAuthManager)
        {
            _apiInboxmanager = apiInboxmanager;
            _dtoMapper = dtoMapper;
            // _smartcardPatientListManager = smartcardPatientListManager;
            // _shrApiManager = new ShrApiManager();
            _psmartStoreManager = psmartStoreManager;
            _pSmartAuthManager = pSmartAuthManager;
        }

        public void Handle(string messageType, string message)
        {
            var apiInbox = new ApiInbox()
            {
                DateReceived = DateTime.Now,
                Message = message,
                SenderId = 1
            };

            switch (messageType)
            {
                case "ADT^A04":
                    HandleNewClientRegistration(apiInbox);
                    break;

                case "ADT^A08":
                    HandleUpdatedClientInformation(apiInbox);
                    break;

                case "RDE^001 ":
                    HandleDrugPrescriptionRaised(apiInbox);
                    break;

                case "RDS^O13":
                    HandlePharmacyDispense(apiInbox);
                    break;

                case "SIU^S12":
                    HandleAppointments(apiInbox);
                    break;

                case "ORU^VL":
                    HandleNewViralLoadResults(apiInbox);
                    break;
            }
        }

        private void HandleNewClientRegistration(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                PatientRegistrationEntity entity = new JavaScriptSerializer().Deserialize<PatientRegistrationEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PatientRegistrationDTO, PatientRegistrationEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTIDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NEXTOFKIN, MappingEntities.NEXTOFKIN>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.VISIT, MappingEntities.VISIT>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTADDRESS, MappingEntities.PATIENTADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PHYSICAL_ADDRESS, MappingEntities.PHYSICALADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NOKNAME, MappingEntities.NOKNAME>().ReverseMap();
                });
                var register = Mapper.Map<PatientRegistrationDTO>(entity);

                var processRegistration = new ProcessRegistration();
                processRegistration.Save(register);

                //update message set processed=1, erromsq=null
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                //update message set processed=1, erromsq
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleUpdatedClientInformation(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                PatientRegistrationEntity entity = new JavaScriptSerializer().Deserialize<PatientRegistrationEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PatientRegistrationDTO, PatientRegistrationEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTIDENTIFICATION, MappingEntities.PATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NEXTOFKIN, MappingEntities.NEXTOFKIN>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.VISIT, MappingEntities.VISIT>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTADDRESS, MappingEntities.PATIENTADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PHYSICAL_ADDRESS, MappingEntities.PHYSICALADDRESS>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.NOKNAME, MappingEntities.NOKNAME>().ReverseMap();
                });
                var register = Mapper.Map<PatientRegistrationDTO>(entity);
                var processRegistration = new ProcessRegistration();
                processRegistration.Update(register);

                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleDrugPrescriptionRaised(ApiInbox incomingMessage)
        {
            _apiInboxmanager.AddApiInbox(incomingMessage);
        }

        private void HandlePharmacyDispense(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;
            try
            {
                PharmacyDispenseEntity pharmacyDispenseEntity = new JavaScriptSerializer().Deserialize<PharmacyDispenseEntity>(incomingMessage.Message);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DtoDrugDispensed, PharmacyDispenseEntity>().ReverseMap();
                    cfg.CreateMap<MappingEntities.MESSAGEHEADER, DTO.MESSAGE_HEADER>().ForMember(x => x.MESSAGE_DATETIME, z => z.Ignore())
                    .AfterMap((src, dst) =>
                    {
                        if (!src.MESSAGE_DATETIME.Equals(""))
                            dst.MESSAGE_DATETIME = DateTime.ParseExact(pharmacyDispenseEntity.MESSAGE_HEADER.MESSAGE_DATETIME, "yyyyMMddHHmmss", null);

                    });
                    cfg.CreateMap<DTO.CommonEntities.APPOINTMENTPATIENTIDENTIFICATION, MappingEntities.APPOINTMENTPATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<MappingEntities.drugs.COMMONORDERDETAILS, DTO.CommonOrderDetailsDispenseDto>().ForMember(x => x.TRANSACTION_DATETIME, z => z.Ignore())
                        .AfterMap((src, dst) =>
                        {
                            if (!src.TRANSACTION_DATETIME.Equals(""))
                                dst.TRANSACTION_DATETIME = DateTime.ParseExact(pharmacyDispenseEntity.MESSAGE_HEADER.MESSAGE_DATETIME, "yyyyMMddHHmmss", null);

                        });
                    cfg.CreateMap<PlacerOrderNumberDto, MappingEntities.drugs.PLACERORDERNUMBER>().ReverseMap();
                    cfg.CreateMap<PlacerOrderNumberDto, MappingEntities.drugs.FILLERORDERNUMBER>().ReverseMap();
                    cfg.CreateMap<OrderingPysicianDto, MappingEntities.drugs.ORDERINGPHYSICIAN>().ReverseMap();
                    cfg.CreateMap<DTO.PHARMACY_DISPENSE, MappingEntities.PHARMACYDISPENSE>().ReverseMap();
                    cfg.CreateMap<DTO.PHARMACY_ENCODED_ORDER, MappingEntities.drugs.PHARMACYENCODEDORDER>().ReverseMap();
                });
                var drugDispensed = Mapper.Map<DtoDrugDispensed>(pharmacyDispenseEntity);
                var processPharmacyDispense = new ProcessPharmacyDispense();
                var msg = processPharmacyDispense.Process(drugDispensed);
                incomingMessage.LogMessage = msg;
                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);

                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.AddApiInbox(incomingMessage);
            }
        }

        private void HandleAppointments(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                PatientAppointmentEntity appointmentEntity = new JavaScriptSerializer().Deserialize<PatientAppointmentEntity>(incomingMessage.Message);

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PatientAppointSchedulingDTO, PatientAppointmentEntity>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.MESSAGEHEADER, MappingEntities.MESSAGEHEADER>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.APPOINTMENTPATIENTIDENTIFICATION, MappingEntities.APPOINTMENTPATIENTIDENTIFICATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.EXTERNALPATIENTID, MappingEntities.EXTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.INTERNALPATIENTID, MappingEntities.INTERNALPATIENTID>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PATIENTNAME, MappingEntities.PATIENTNAME>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.APPOINTMENT_INFORMATION, MappingEntities.APPOINTMENT_INFORMATION>().ReverseMap();
                    cfg.CreateMap<DTO.CommonEntities.PLACER_APPOINTMENT_NUMBER, MappingEntities.PLACER_APPOINTMENT_NUMBER>().ReverseMap();
                });
                var appointment = Mapper.Map<PatientAppointSchedulingDTO>(appointmentEntity);
                var processAppoinment = new ProcessPatientAppointmentMessage();

                foreach (var itemAppointment in appointment.APPOINTMENT_INFORMATION)
                {
                    if (itemAppointment.ACTION_CODE == "A")
                    {
                        processAppoinment.Save(appointment);
                    }
                    else if (itemAppointment.ACTION_CODE == "U" || itemAppointment.ACTION_CODE == "D")
                    {
                        processAppoinment.Update(appointment);
                    }
                }

                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        private void HandleNewViralLoadResults(ApiInbox incomingMessage)
        {
            //save to inbox
            int Id = _apiInboxmanager.AddApiInbox(incomingMessage);
            incomingMessage.Id = Id;

            try
            {
                ViralLoadResultEntity entity = new JavaScriptSerializer().Deserialize<ViralLoadResultEntity>(incomingMessage.Message);
                ViralLoadResultsDto vlResultsDto = _dtoMapper.ViralLoadResults(entity);
                var processViralLoadResults = new ProcessViralLoadResults();
                var msg = processViralLoadResults.Save(vlResultsDto);
                // var msg = "could not be processed";

                incomingMessage.LogMessage = msg;
                //update message that it has been processed
                incomingMessage.DateProcessed = DateTime.Now;
                incomingMessage.Processed = true;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
            catch (Exception e)
            {
                incomingMessage.LogMessage = e.Message;
                incomingMessage.Processed = false;
                _apiInboxmanager.EditApiInbox(incomingMessage);
            }
        }

        public List<DtoSmartcardPatientList> FetchSmartcardEligibleList()
        {
            SmartcardPatientListManager smartcardPatientListManager = new SmartcardPatientListManager();
            return smartcardPatientListManager.GetSmartcardPatientList();

            // return _smartcardPatientListManager.GetSmartCardPatientList().ToString();
        }

        public DtoShr FetchClientShr(psmartCard psmartCard)
        {
            PsmartShrManager psmartShrManager = new PsmartShrManager();

            if (psmartCard.PATIENTID is int i)
            {
                var shr = psmartShrManager.GenerateShrForEmr(psmartCard.PATIENTID);

                return shr;
            }
            else
            {
                PsmartShrCardSerialManager psmartShrCardSerialManager = new PsmartShrCardSerialManager();
                var shr = psmartShrCardSerialManager.GenerateShrForEmr(psmartCard.CARD_SERIAL_NO);
                return shr;
            }
        }

        public DtoShr FetchClientShrNew(int id)
        {
            PsmartShrManager psmartShrManager = new PsmartShrManager();

            if (id is int i)
            {
                var shr = psmartShrManager.GenerateShrForEmr(id);

                return shr;
            }
            else
            {
                PsmartShrCardSerialManager psmartShrCardSerialManager = new PsmartShrCardSerialManager();
                var shr = psmartShrCardSerialManager.GenerateShrForEmr("");
                return shr;
            }
        }

        public int SaveShrFromMiddleware(Psmart_Store p)
        {
            return _psmartStoreManager.SaveShr(p);
        }

        public string ProcessCardSerialNumberIdentifier(psmartCard psmartCard)
        {
            PatientEnrollmentManager patientEnrollmentManager = new PatientEnrollmentManager();
            IdentifierManager identifierManager = new IdentifierManager();
            PatientIdentifierManager patientIdentifierManager = new PatientIdentifierManager();
            var patientEnrollment = patientEnrollmentManager.GetPatientEnrollmentByPatientId(psmartCard.PATIENTID);
            int patientEnrollmentId = patientEnrollment[0].Id;
            int results = 0;
            // Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");
            Identifier identifier = identifierManager.GetIdentifierByCode("CARD_SERIAL_NUMBER");
            try
            {
                var patientEntityIdentifier = new PatientEntityIdentifier()
                {
                    IdentifierTypeId = identifier.Id,
                    IdentifierValue = psmartCard.CARD_SERIAL_NO,
                    PatientEnrollmentId = patientEnrollmentId,
                    PatientId = psmartCard.PATIENTID
                };

                string serialNumber = null;
                var identifierLists = patientIdentifierManager.CheckIfIdentifierNumberIsUsed(psmartCard.CARD_SERIAL_NO, patientEntityIdentifier.IdentifierTypeId);

                foreach (var identifierList in identifierLists)
                {
                    serialNumber = identifierList.IdentifierValue;
                }
                if (string.IsNullOrEmpty(serialNumber))
                {
                    results = patientIdentifierManager.addPatientIdentifier(patientEntityIdentifier.PatientId,
                        patientEntityIdentifier.PatientEnrollmentId, patientEntityIdentifier.IdentifierTypeId,
                        patientEntityIdentifier.IdentifierValue, 0, false);
                    return (results) > 0 ? "Card serial Number Linked to a patient as identifier" : "Failed linking card Serial Number to patient";
                }
                else
                {
                    return "Card Serial Number : " + psmartCard.CARD_SERIAL_NO + " is already assigned to a Patient.";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Authentication(string username, string password)
        {
            PsmartAuthUser _user = _pSmartAuthManager.LoginValidate(username);
            string response = @"{""status"":""false"", ""DisplayName"":""""}";

            if (null != _user)
            {
                Utility util = new Utility();

                if (_user.Password == util.Encrypt(password))
                {
                    response = @"{""status"":""true"", ""DisplayName"":""" + _user.DisplayName + "}";
                }
            }
            return response;
        }

        public DtoShr ProcessCardSerialNumberIdentifierBluecard(psmartCard psmartCard)
        {
            MstPatientManager patientManager = new MstPatientManager();
            PsmartShrCardSerialManager psmartShrCardSerialManager = new PsmartShrCardSerialManager();
            DtoShr NewSHR = new DtoShr();

            try
            {
              //  int result = patientManager.UpdatePatientCardSerial(psmartCard);
                string processCardStatus = this.ProcessCardSerialNumberIdentifier(psmartCard);

                if (!string.IsNullOrEmpty(processCardStatus))
                {
                    NewSHR = psmartShrCardSerialManager.GenerateShrForEmr(psmartCard.CARD_SERIAL_NO);
                }
                return NewSHR;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DtoShr LoadFromEmr(string CARD_SERIAL_NO)
        {
            PsmartShrCardSerialManager psmartShrCardSerialManager = new PsmartShrCardSerialManager();
            DtoShr NewSHR = new DtoShr();

            try
            {
                NewSHR = psmartShrCardSerialManager.GenerateShrForEmr(CARD_SERIAL_NO);
                return NewSHR;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}