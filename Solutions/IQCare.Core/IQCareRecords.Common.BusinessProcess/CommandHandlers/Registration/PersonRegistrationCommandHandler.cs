using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class PersonRegistrationCommandHandler:IRequestHandler<PersonRegistrationCommand, Result<PersonRegistrationResponse>>
    {
        public int PId;
        public string msg;
        public int res;


        private readonly ICommonUnitOfWork _unitOfWork;
    
        public PersonRegistrationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

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
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                int PerId;


                if (!String.IsNullOrEmpty(c.PersonId.ToString()))
                {
                    PerId = Convert.ToInt32(c.PersonId.ToString());

                    if (PerId > 0)
                    {

                        var result = await Task.Run(() => rs.UpdatePerson(PerId, c.FirstName, c.MiddleName, c.LastName, c.Sex, c.CreatedBy, c.DateOfBirth, c.DobPrecision));
                        PId = result.Id;
                        msg = String.Format("Person with the PersonId: {0} updated successfully", PId);
                        var _marStatus = await Task.Run(() => rs.GetFirstPatientMaritalStatus(PerId));
                        if (_marStatus != null && c.MaritalStatus > 0)
                        {


                            //_marStatus.DeleteFlag = true;
                            var maritalStatus = await Task.Run(() => rs.UpdateMaritalStatus(_marStatus));
                           // var finalupdatestatus = await Task.Run(() => rs.AddMaritalStatus(PerId, c.MaritalStatus, c.CreatedBy));
                            if (maritalStatus!=null)
                            {
                                msg += "PersonMaritalStatus Updated Successfully";

                            }
                        }
                        else if (_marStatus != null && c.MaritalStatus == 0)
                        {
                            _marStatus.DeleteFlag = true;
                            var maritalStatus = await Task.Run(() => rs.UpdateMaritalStatus(_marStatus));
                            if (maritalStatus.DeleteFlag == true)
                            {
                                msg += "Person MaritalStatus Updated Successfully";
                            }
                        }
                        else
                        {
                            if (c.MaritalStatus > 0)
                            {
                                var finalupdatestatus = await Task.Run(() => rs.AddMaritalStatus(PerId, c.MaritalStatus, c.CreatedBy));
                                if (finalupdatestatus != null)
                                {
                                    msg += "PersonMaritalStatus Added Successfully!";
                                }
                            }
                        }
                    }
                }
                else
                {
                    var reg = rs.RegisterPerson(c.FirstName, c.MiddleName, c.LastName, c.Sex, c.DateOfBirth, c.CreatedBy, c.DobPrecision);
                    if (reg != null && reg.Id > 0)
                    {
                        int perId = reg.Id;
                        PId = reg.Id;
                        msg += String.Format("New Person Added Successsfully:PersonId=>,{0}", reg.Id);
                        if (c.MaritalStatus > 0)
                        {
                            var mar = rs.AddMaritalStatus(perId, c.MaritalStatus, c.CreatedBy);
                            if (mar != null)
                            {
                                msg += "Person Marital Status added successfully";
                            }
                        }
                    }
                }



                _unitOfWork.Dispose();

                return Result<PersonRegistrationResponse>.Valid(new PersonRegistrationResponse { PersonId = PId,Message=msg });
            }
            catch (Exception e)
            {
               // msg = e.Message;
                return Result<PersonRegistrationResponse>.Invalid(e.Message);
            }
        }
    }
}
