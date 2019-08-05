using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class AddHeiLabTestsDoneCommandHandler : IRequestHandler<AddHeiLabTestsDoneCommand, Result<AddHeiLabTestsDoneResponse>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        private readonly IMapper _mapper;

        public AddHeiLabTestsDoneCommandHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {
            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddHeiLabTestsDoneResponse>> Handle(AddHeiLabTestsDoneCommand request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {

                    if (request.HeiLabTestTypes.Any())
                    {
                        List<HeiLabTests> heiLabTestses = new List<HeiLabTests>();
                        request.HeiLabTestTypes.ForEach(x => heiLabTestses.Add(new HeiLabTests
                        {
                            PatientId = request.PatientId,
                            HeiLabTestTypeId = x.Id,
                            LabOrderId = request.LabOrderId
                        }));

                        await _labUnitOfWork.Repository<HeiLabTests>().AddRangeAsync(heiLabTestses);
                        await _labUnitOfWork.SaveAsync();
                    }

                    return Result<AddHeiLabTestsDoneResponse>.Valid(new AddHeiLabTestsDoneResponse()
                    {
                        Message = "Successfully added Hei Lab Test Types"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, $"An error occured while trying to add Hei Lab Tests Types");
                    return Result<AddHeiLabTestsDoneResponse>.Invalid($"An error occured while trying to add Hei Lab Tests Types");
                }
            }
        }
    }
}