﻿using AutoMapper;
using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using Serilog;

namespace IQCare.Lab.BusinessProcess.QueryHandlers
{
    public class GetLabTestResultsQueryHandler : IRequestHandler<GetLabTestResults,Result<List<LabTestResultViewModel>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        private readonly IMapper _mapper;
        public GetLabTestResultsQueryHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {
            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<LabTestResultViewModel>>> Handle(GetLabTestResults request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {
                    var labTestResults =  !string.IsNullOrEmpty(request.LabOrderStatus)
                        ? _labUnitOfWork.Repository<PatientLabTracker>()
                            .Get(x =>x.PatientId == request.PatientId && x.Results == request.LabOrderStatus)
                        : _labUnitOfWork.Repository<PatientLabTracker>().Get(x => x.PatientId == request.PatientId);
                        
                    var labTestModel = _mapper.Map<List<LabTestResultViewModel>>(labTestResults.OrderByDescending(x=>x.CreateDate));

                    return Task.FromResult(Result<List<LabTestResultViewModel>>.Valid(labTestModel));
                }
                catch (Exception ex)
                {
                    Log.Error(ex,"An error occured while getting lab test results");
                    return Task.FromResult(Result<List<LabTestResultViewModel>>.Invalid(ex.Message));
                }
            }          
        }
    }
}
