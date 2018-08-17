using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using IQCare.Records.BusinessProcess.Command;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace IQCare.Records.BusinessProcess.CommandHandlers
{
  public  class GetPersonDetailsCommandHandler:IRequestHandler<GetPersonDetailsCommand,Result<GetPersonDetailsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        Person persondetail = new Person();
        PersonEducation personEducation = new PersonEducation();
        PersonOccupation personocc = new PersonOccupation();
        PersonMaritalStatus personmarital = new PersonMaritalStatus();
        PersonLocation personlocation = new PersonLocation();
        PersonContactView personcontact = new PersonContactView();
        List<PersonEmergencyView> personemerg = new List<PersonEmergencyView>();
        PersonIdentifier pid = new PersonIdentifier();
        Patient pt = new Patient();
        public GetPersonDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetPersonDetailsResponse>> Handle(GetPersonDetailsCommand request,CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                int id = request.PersonId;
                if (request.PersonId > 0)
                {
                    persondetail = await rs.GetPerson(id);
                    personEducation = await rs.GetCurrentPersonEducation(id);
                    personocc = await rs.GetCurrentOccupation(id);
                    personmarital = await rs.GetFirstPatientMaritalStatus(id);
                    personlocation = await rs.GetCurrentPersonLocation(id);
                    personcontact = await rs.GetCurrentPersonContact(id);
                    personemerg = await rs.GetCurrentPersonEmergency(id);
                    pid = await rs.GetCurrentPersonIdentifier(id);
                    pt = await rs.GetPatientByPersonId(id);
                }


                _unitOfWork.Dispose();


                return Result<GetPersonDetailsResponse>.Valid(new GetPersonDetailsResponse()
                {

                    personDetail = persondetail,
                    personEducation = personEducation,
                    personOccupation = personocc,
                    personMaritalStatus = personmarital,
                    personLocation = personlocation,
                    personContact = personcontact,
                    PersonEmergencyView = personemerg,
                    personIdentifier = pid,
                    patient = pt

                });

            }
            catch(Exception ex)
            {



                return Result<GetPersonDetailsResponse>.Invalid(ex.Message);

            }
        }
    }
}
