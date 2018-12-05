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
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class GetLabTestsCommandHandler : IRequestHandler<GetLabTestsCommand, Result<IEnumerable<LabTestViewModel>>>
    {
        private readonly ILabUnitOfWork _labUnitOfwork;
        private readonly IMapper _mapper;

        public GetLabTestsCommandHandler(ILabUnitOfWork labUnitOfwork, IMapper mapper)
        {
            _labUnitOfwork = labUnitOfwork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<LabTestViewModel>>> Handle(GetLabTestsCommand request, CancellationToken cancellationToken)
        {
            using (_labUnitOfwork)
            {
                try
                {
                    var labTests = request.LabTests == null
                        ? await _labUnitOfwork.Repository<LabTest>().GetAllAsync()
                        : _labUnitOfwork.Repository<LabTest>().Get(x => request.LabTests.Contains(x.Name));

                    var labTestsViewModel = _mapper.Map<IEnumerable<LabTestViewModel>>(labTests);

                    return Result<IEnumerable<LabTestViewModel>>.Valid(labTestsViewModel);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<IEnumerable<LabTestViewModel>>.Invalid(e.Message);
                }
            }
        }
    }
}