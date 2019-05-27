using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class AddHivReConfirmatoryTestsCommandHandler : IRequestHandler<AddHivReConfirmatoryTestsCommand, Result<HivReConfirmatoryTestsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddHivReConfirmatoryTestsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<HivReConfirmatoryTestsResponse>> Handle(AddHivReConfirmatoryTestsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var reConfirmatoryTests = await _unitOfWork.Repository<HIVReConfirmatoryTest>()
                        .Get(x => x.PersonId == request.PersonId).ToListAsync();
                    if (reConfirmatoryTests.Count > 0)
                    {
                        reConfirmatoryTests[0].DeleteFlag = true;
                        _unitOfWork.Repository<HIVReConfirmatoryTest>().Update(reConfirmatoryTests[0]);
                        await _unitOfWork.SaveAsync();
                    }

                    HIVReConfirmatoryTest hivReConfirmatory = new HIVReConfirmatoryTest()
                    {
                        PersonId = request.PersonId,
                        TypeOfTest = request.TypeOfTest,
                        TestResult = request.TestResult,
                        TestResultDate = request.TestResultDate,
                        DeleteFlag = false,
                        CreatedBy = request.CreatedBy,
                        CreateDate = DateTime.Now
                    };
                    await _unitOfWork.Repository<HIVReConfirmatoryTest>().AddAsync(hivReConfirmatory);
                    await _unitOfWork.SaveAsync();

                    return Result<HivReConfirmatoryTestsResponse>.Valid(new HivReConfirmatoryTestsResponse()
                    {
                        Message = "Successfully saved patient hiv-reconfirmatory tests"
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"An error occurred while saving patient hiv reconfirmatory tests " + e.Message + $" for personid: {request.PersonId}");
                    return Result<HivReConfirmatoryTestsResponse>.Invalid($"An error occurred while saving patient hiv reconfirmatory tests " + e.Message + $" for personid: {request.PersonId}");
                }
            }
            throw new System.NotImplementedException();
        }
    }
}