using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Commands;
using Entities.Records;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Records.UILogic;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
    public class PersonRegistrationCommandHandler : IRequestHandler<PersonRegistrationCommand, Result<PersonRegistrationResponse>>
    {
        public int PId;
        public string msg;
        public int res;
        public async Task<Result<PersonRegistrationResponse>> Handle(PersonRegistrationCommand request, CancellationToken cancellationToken)
        {
            try

            {
                Client c = new Client();
                c.FirstName = request.Person.FirstName;
                c.LastName = request.Person.LastName;
                c.MiddleName = request.Person.MiddleName;
                c.LastName = request.Person.LastName;
                c.MaritalStatus = request.Person.MaritalStatus;
                c.Sex = request.Person.Sex;
                c.PersonId = request.Person.PersonId;
                c.CreatedBy = request.Person.CreatedBy;
                c.DateOfBirth = request.Person.DateOfBirth;
                c.DobPrecision = request.Person.DobPrecision;

                int PerId;

                if (!String.IsNullOrEmpty(c.PersonId.ToString()))
                {
                    PerId = Convert.ToInt32(c.PersonId.ToString());
                    if (PerId > 0)
                    {
                        var personManager = new PersonManager();
                        //UpdatePerson(string firstname, string middlename, string lastname, int gender, int userId, int id, DateTime dateOfBirth, bool dobPrecision)
                        await Task.Run(() => personManager.UpdatePerson(c.FirstName, c.MiddleName, c.LastName, c.Sex, c.CreatedBy, Convert.ToInt32(c.PersonId.ToString()), c.DateOfBirth, c.DobPrecision));
                        PId = Convert.ToInt32(c.PersonId.ToString());
                        msg = string.Format("Person with the PersonId: {0} updated successfully", PId);
                    }

                    var maritalstatus = new PersonMaritalStatusManager();
                    var _marStatus = maritalstatus.GetInitialPatientMaritalStatus(PerId);
                    if (_marStatus != null && c.MaritalStatus > 0)
                    {
                        _marStatus.MaritalStatusId = c.MaritalStatus;
                        _marStatus.CreatedBy = c.CreatedBy;
                        res = await Task.Run(()=>maritalstatus.UpdatePatientMaritalStatus(_marStatus));
                        if (res > 0)
                        {
                            msg += "<p>Person Marital Status Updated Successfully!</p>";
                        }

                    }
                    else if (_marStatus != null && c.MaritalStatus== 0)
                    {
                        _marStatus.DeleteFlag = true;

                        res = await Task.Run(()=>maritalstatus.UpdatePatientMaritalStatus(_marStatus));
                        if (res > 0)
                        {
                            msg += "<p>Person Marital Status Updated Successfully!</p>";
                        }
                    }
                    else
                    {
                        if(c.MaritalStatus > 0)
                        {
                            res = await Task.Run(()=>maritalstatus.AddPatientMaritalStatus(Convert.ToInt32(c.PersonId.ToString()), c.MaritalStatus, c.CreatedBy));
                            {
                                if(res> 0)
                                {
                                    msg += "PersonMarital Status Added Successfully!";
                                }
                            }
                        }
                    }
                }
                else
                {
                    var personLogic = new PersonManager();
                    PId = personLogic.AddPersonUiLogic(c.FirstName, c.MiddleName, c.LastName, c.Sex, Convert.ToInt32(c.CreatedBy), c.DateOfBirth, Convert.ToBoolean(c.DobPrecision));
                    if(PId > 0)
                    {
                        msg = "New Person Added successfully:PersonId=>" + PId + "</p>";
                        var MaritalStatus = new PersonMaritalStatusManager();
                        if(c.MaritalStatus > 0)
                        {
                            res = MaritalStatus.AddPatientMaritalStatus(PId, c.MaritalStatus, c.CreatedBy);
                            if(res > 0)
                            {
                                msg += "Person Marital Status Added Successfully!";

                            }
                        }
                        else
                        {

                        }
                           
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;

            }

            return Result<PersonRegistrationResponse>.Valid(new PersonRegistrationResponse()
            {
                PersonId = PId,
                Message=msg

            });

        }
    }
}
