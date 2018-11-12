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

                if (request.PersonId > 0)
                {
                    if (request.EmergencyContactPersonId > 0)
                    {
                        PersonEmergencyContact pme = new PersonEmergencyContact();
                        pme = await Task.Run(() => rs.GetSpecificEmergencyContacts(Convert.ToInt32(request.EmergencyContactPersonId), request.PersonId));
                        if (pme != null)
                        {




                            pme.PersonId = request.PersonId;
                            pme.EmergencyContactPersonId = request.EmergencyContactPersonId;
                            pme.MobileContact = request.MobileContact;
                            pme.CreatedBy = request.CreatedBy;
                            pme.DeleteFlag = request.DeleteFlag;


                            int res = await Task.Run(() => rs.UpdatePersonEmergencyContact(pme));
                            if (res > 0)
                            {
                                msg += "Person Emergency Mobile Contact Updated Successfully";
                                personEmergencyContactId = request.EmergencyContactPersonId;

                            }
                            var personconsent = await Task.Run(() => rs.GetCurrentPersonConsent(pme.Id, Convert.ToInt32(request.PersonId)));
                            if (personconsent != null)
                            {
                                personconsent.ConsentType = request.ConsentType;
                                personconsent.ConsentValue = request.ConsentValue;
                                personconsent.ConsentDate = DateTime.Now;
                                personconsent.ConsentReason = request.ConsentReason;

                                int consent = await Task.Run(() => rs.UpdatePersonConsent(personconsent));
                                if (consent > 0)
                                {
                                    msg += "Person Consent has been updated successfully";
                                }

                            }
                            else
                            {
                                PersonConsent pcs = new PersonConsent();
                                pcs.PersonId = request.PersonId;
                                pcs.EmergencyContactId = pme.Id;
                               pcs.ConsentType = request.ConsentType;
                               pcs.ConsentValue = request.ConsentValue;
                                pcs.ConsentDate = DateTime.Now;
                                pcs.ConsentReason = request.ConsentReason;

                                var perc = await Task.Run(() => rs.AddPersonConsent(pcs));

                                if(perc !=null)
                                {
                                    msg += "Person Consent Has been added successfully";
                                }

                            }



                            Person per = new Person();

                            per.FirstName = request.firstname;
                            per.MidName = request.middlename;
                            per.LastName = request.lastname;
                            per.Sex = request.gender;
                            per.Id = request.EmergencyContactPersonId;


                            int resupda = await Task.Run(() => rs.UpdatePerson(per, request.EmergencyContactPersonId));
                            if (resupda > 0)
                            {
                                msg += "PersonEmergencyContact updated successfully";
                            }
                        }
                    }
                }


                else
                {
                    var personEmerg = await Task.Run(() => rs.InsertPerson(request.firstname, request.middlename, request.lastname, request.gender, request.CreatedBy));

                    if (personEmerg != null)
                    {
                        personEmergencyContactId = personEmerg.Id;
                        PersonEmergencyContact pmm = new PersonEmergencyContact()
                        {
                            PersonId = request.PersonId,
                            EmergencyContactPersonId = personEmerg.Id,
                            MobileContact = request.MobileContact,
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = request.DeleteFlag
                        };

                        int pmeid = await Task.Run(() => rs.AddPersonEmergencyContact(pmm));

                        if (pmeid > 0)
                        {
                            msg += "New Person Emergencycontact Added successfully";

                        }

                        PersonConsent pcs = new PersonConsent();
                        pcs.PersonId = request.PersonId;
                        pcs.EmergencyContactId = pmeid;
                        pcs.ConsentType = request.ConsentType;
                        pcs.ConsentValue = request.ConsentValue;
                        pcs.ConsentDate = DateTime.Now;
                        pcs.ConsentReason = request.ConsentReason;

                        var perc = await Task.Run(() => rs.AddPersonConsent(pcs));

                        if (perc != null)
                        {
                            msg += "Person Consent Has been added successfully";
                        }


                        PersonRelation pl = new PersonRelation();
                        var personrel = await Task.Run(() => rs.AddPersonRelationship(request.PersonId, personEmerg.Id, request.RelationshipType, request.CreatedBy));

                        if (personrel != null)
                        {
                            msg += "Person EmergencyContact relationship added successfully";
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
