using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPersonLocationCommandHandler : IRequestHandler<AddPersonLocationCommand, Result<AddPersonLocationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonLocationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonLocationResponse>> Handle(AddPersonLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    PersonLocation personLocation = new PersonLocation()
                    {
                        PersonId = request.PersonId,
                        County = request.CountyId,
                        SubCounty = request.SubCountyId,
                        Ward = request.WardId,
                        Village = request.Village,
                        Location = "",
                        SubLocation = "",
                        LandMark = request.LandMark,
                        NearestHealthCentre = "",
                        Active = false,
                        DeleteFlag = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.UserId
                    };

                    await _unitOfWork.Repository<PersonLocation>().AddAsync(personLocation);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPersonLocationResponse>.Valid(new AddPersonLocationResponse()
                    {
                        PersonLocationId = personLocation.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPersonLocationResponse>.Invalid(e.Message);
            }
        }
    }
}