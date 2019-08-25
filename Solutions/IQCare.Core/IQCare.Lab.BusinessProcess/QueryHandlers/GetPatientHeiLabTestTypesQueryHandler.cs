using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Lab.BusinessProcess.QueryHandlers
{
    public class GetPatientHeiLabTestTypesQueryHandler : IRequestHandler<GetPatientHeiLabTestTypesQuery, Result<List<HeiLabTests>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        private readonly IMapper _mapper;
        public GetPatientHeiLabTestTypesQueryHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {
            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<HeiLabTests>>> Handle(GetPatientHeiLabTestTypesQuery request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {
                    var result = await _labUnitOfWork.Repository<HeiLabTests>()
                        .Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<HeiLabTests>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"An error occured while trying hei lab test types");
                    return Result<List<HeiLabTests>>.Invalid($"An error occured while trying hei lab test types");
                }
            }
        }
    }
}