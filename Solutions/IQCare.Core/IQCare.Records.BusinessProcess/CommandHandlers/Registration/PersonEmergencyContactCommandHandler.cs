using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Command;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.BusinessProcess.Services;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonEmergencyContactCommandHandler : IRequestHandler<PersonEmergencyContactCommand, Result<AddPersonEmergencyContactResponse>>
    {
        public string msg;

        int personEmergencyContactId;
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonEmergencyContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddPersonEmergencyContactResponse>> Handle(PersonEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                if (request.emergencycontacts != null || request.emergencycontacts.Count > 0)
                {
                    foreach (EmergencyContact em in request.emergencycontacts)
                    {
                        if (em.PersonId > 0)
                        {
                            if (em.EmergencyContactPersonId > 0)
                            {
                                PersonEmergencyContact pme = new PersonEmergencyContact();
                                PersonEmergencyContact pmt = new PersonEmergencyContact();
                                if (em.emgEmergencyContactType > 0)
                                {
                                    pme = await rs.GetSpecificEmergencyContacts(Convert.ToInt32(em.EmergencyContactPersonId), em.PersonId);
                                    //pmt = await rs.GetNextofkinEmergencyContacts(Convert.ToInt32(em.EmergencyContactPersonId), em.PersonId);
                                    if (pme != null)
                                    {




                                        pme.PersonId = em.PersonId;
                                        pme.EmergencyContactPersonId = em.EmergencyContactPersonId;
                                        pme.MobileContact = em.MobileContact;
                                        pme.CreatedBy = em.CreatedBy;
                                        pme.DeleteFlag = em.DeleteFlag;
                                        pme.ContactType = em.emgEmergencyContactType;

                                        pme.RegisteredToClinic = em.RegisteredToClinic;

                                        int res = await rs.UpdatePersonEmergencyContact(pme);
                                        if (res > 0)
                                        {
                                            msg += "Person Emergency Mobile Contact Updated Successfully";
                                            personEmergencyContactId = em.EmergencyContactPersonId;

                                        }
                                    }
                                    else
                                    {

                                        PersonEmergencyContact pmm = new PersonEmergencyContact()
                                        {
                                            PersonId = em.PersonId,
                                            EmergencyContactPersonId = em.EmergencyContactPersonId,
                                            MobileContact = em.MobileContact,
                                            CreatedBy = em.CreatedBy,
                                            DeleteFlag = em.DeleteFlag,
                                            ContactType = em.emgEmergencyContactType,
                                            RegisteredToClinic = em.RegisteredToClinic
                                        };

                                        PersonEmergencyContact pmeid = new PersonEmergencyContact();
                                        pmeid = await rs.AddPersonEmergencyContact(pmm);

                                        if (pmeid != null)
                                        {
                                            msg += "New Person Emergencycontact Added successfully";

                                        }
                                    }
                                        var personconsent = await rs.GetCurrentPersonConsent(em.EmergencyContactPersonId, Convert.ToInt32(em.PersonId));
                                        if (personconsent != null)
                                        {
                                            personconsent.ConsentType = em.ConsentType;
                                            personconsent.ConsentValue = em.ConsentValue;
                                            personconsent.ConsentDate = DateTime.Now;
                                            personconsent.ConsentReason = em.ConsentReason;

                                            int consent = await rs.UpdatePersonConsent(personconsent);
                                            if (consent > 0)
                                            {
                                                msg += "Person Consent has been updated successfully";
                                            }

                                        }
                                        else
                                        {
                                            PersonConsent pcs = new PersonConsent();
                                            pcs.PersonId = em.PersonId;
                                            pcs.EmergencyContactId = em.EmergencyContactPersonId;
                                            pcs.ConsentType = em.ConsentType;
                                            pcs.ConsentValue = em.ConsentValue;
                                            pcs.ConsentDate = DateTime.Now;
                                            pcs.ConsentReason = em.ConsentReason;

                                            var perc = await rs.AddPersonConsent(pcs);

                                            if (perc != null)
                                            {
                                                msg += "Person Consent Has been added successfully";
                                            }

                                        }



                                        Person per = new Person();

                                        per.FirstName = em.firstname;
                                        per.MidName = em.middlename;
                                        per.LastName = em.lastname;
                                        per.Sex = em.gender;
                                        per.Id = em.EmergencyContactPersonId;


                                       var resupd = await rs.UpdateEmergencyPerson(per);
                                        if (resupd!=null)
                                        {
                                            msg += "PersonEmergencyContact updated successfully";
                                        }
                                    
                                }
                                if (em.emgNextofKinContactType > 0)
                                {
                                    pmt = await rs.GetNextofkinEmergencyContacts(Convert.ToInt32(em.EmergencyContactPersonId), em.PersonId);

                                    if (pmt != null)
                                    {




                                        pmt.PersonId = em.PersonId;
                                        pmt.EmergencyContactPersonId = em.EmergencyContactPersonId;
                                        pmt.MobileContact = em.MobileContact;
                                        pmt.CreatedBy = em.CreatedBy;
                                        pmt.DeleteFlag = em.DeleteFlag;
                                        pmt.ContactType = em.emgNextofKinContactType;

                                        pmt.RegisteredToClinic = em.RegisteredToClinic;

                                        int res = await rs.UpdatePersonEmergencyContact(pmt);
                                        if (res > 0)
                                        {
                                            msg += "Person Emergency Mobile Contact Updated Successfully";
                                            personEmergencyContactId = em.EmergencyContactPersonId;

                                        }
                                    }
                                    else
                                    {

                                        PersonEmergencyContact pmm = new PersonEmergencyContact()
                                        {
                                            PersonId = em.PersonId,
                                            EmergencyContactPersonId = em.EmergencyContactPersonId,
                                            MobileContact = em.MobileContact,
                                            CreatedBy = em.CreatedBy,
                                            DeleteFlag = em.DeleteFlag,
                                            ContactType = em.emgEmergencyContactType,
                                            RegisteredToClinic = em.RegisteredToClinic
                                        };

                                        PersonEmergencyContact pmeid = new PersonEmergencyContact();
                                        pmeid = await rs.AddPersonEmergencyContact(pmm);

                                        if (pmeid != null)
                                        {
                                            msg += "New Person Emergencycontact Added successfully";

                                        }
                                    }
                                    var personconsent = await rs.GetCurrentPersonConsent(em.EmergencyContactPersonId, Convert.ToInt32(em.PersonId));
                                            if (personconsent != null)
                                            {
                                                personconsent.ConsentType = em.ConsentType;
                                                personconsent.ConsentValue = em.ConsentValue;
                                                personconsent.ConsentDate = DateTime.Now;
                                                personconsent.ConsentReason = em.ConsentReason;

                                                int consent = await rs.UpdatePersonConsent(personconsent);
                                                if (consent > 0)
                                                {
                                                    msg += "Person Consent has been updated successfully";
                                                }

                                            }
                                            else
                                            {
                                            if (em.ConsentValue > 0)
                                            {
                                                PersonConsent pcs = new PersonConsent();
                                                pcs.PersonId = em.PersonId;
                                                pcs.EmergencyContactId = pme.Id;
                                                pcs.ConsentType = em.ConsentType;
                                                pcs.ConsentValue = em.ConsentValue;
                                                pcs.ConsentDate = DateTime.Now;
                                                pcs.ConsentReason = em.ConsentReason;

                                                var perc = await rs.AddPersonConsent(pcs);

                                                if (perc != null)
                                                {
                                                    msg += "Person Consent Has been added successfully";
                                                }
                                            }
                                            }



                                            Person per = new Person();

                                            per.FirstName = em.firstname;
                                            per.MidName = em.middlename;
                                            per.LastName = em.lastname;
                                            per.Sex = em.gender;
                                            per.Id = em.EmergencyContactPersonId;


                                           var resupda = await rs.UpdateEmergencyPerson(per);
                                            if (resupda !=null)
                                            {
                                                msg += "PersonEmergencyContact updated successfully";
                                            }
                                        
                                    

                                }
                            }
                            else
                            {
                               
                                var personEmerg = await rs.InsertPerson(em.firstname, em.middlename, em.lastname, em.gender, em.CreatedBy);

                                if (personEmerg != null)
                                {
                                    personEmergencyContactId = personEmerg.Id;
                                    if (em.emgEmergencyContactType > 0)
                                    {
                                        PersonEmergencyContact pmm = new PersonEmergencyContact()
                                        {
                                            PersonId = em.PersonId,
                                            EmergencyContactPersonId = personEmerg.Id,
                                            MobileContact = em.MobileContact,
                                            CreatedBy = em.CreatedBy,
                                            DeleteFlag = em.DeleteFlag,
                                            ContactType = em.emgEmergencyContactType,
                                            RegisteredToClinic = em.RegisteredToClinic
                                        };

                                        PersonEmergencyContact pmeid = new PersonEmergencyContact();
                                        pmeid = await rs.AddPersonEmergencyContact(pmm);

                                        if (pmeid != null)
                                        {
                                            msg += "New Person Emergencycontact Added successfully";

                                        }
                                    }
                                    if(em.emgNextofKinContactType > 0)
                                    {
                                        PersonEmergencyContact pmm = new PersonEmergencyContact()
                                        {
                                            PersonId = em.PersonId,
                                            EmergencyContactPersonId = personEmerg.Id,
                                            MobileContact = em.MobileContact,
                                            CreatedBy = em.CreatedBy,
                                            DeleteFlag = em.DeleteFlag,
                                            ContactType = em.emgNextofKinContactType,
                                            RegisteredToClinic = em.RegisteredToClinic
                                        };

                                        PersonEmergencyContact pmeid = new PersonEmergencyContact();
                                        pmeid = await rs.AddPersonEmergencyContact(pmm);

                                        if (pmeid != null)
                                        {
                                            msg += "New Person Emergencycontact Added successfully";

                                        }
                                    }
                                    if (em.ConsentValue > 0)
                                    {
                                        PersonConsent pcs = new PersonConsent();
                                        pcs.PersonId = em.PersonId;
                                        pcs.EmergencyContactId = personEmergencyContactId;
                                        pcs.ConsentType = em.ConsentType;
                                        pcs.ConsentValue = em.ConsentValue;
                                        pcs.ConsentDate = DateTime.Now;
                                        pcs.ConsentReason = em.ConsentReason;

                                        var perc = await rs.AddPersonConsent(pcs);

                                        if (perc != null)
                                        {
                                            msg += "Person Consent Has been added successfully";
                                        }

                                    }
                                    PersonRelationship pl = new PersonRelationship();
                                    var personrel = await rs.AddPersonRelationship(em.PersonId, personEmerg.Id, em.RelationshipType, em.CreatedBy);

                                    if (personrel != null)
                                    {
                                        msg += "Person EmergencyContact relationship added successfully";
                                    }
                                }
                            }

                        }


                    }

                }





                            return Result<AddPersonEmergencyContactResponse>.Valid(new AddPersonEmergencyContactResponse()
                            {
                                Message = msg,
                                PersonEmergencyContactId = personEmergencyContactId

                            });
                        
                    
                
            }
            catch (Exception e)
            {
                return Result<AddPersonEmergencyContactResponse>.Invalid(e.Message);
            }
        }
    }
}
