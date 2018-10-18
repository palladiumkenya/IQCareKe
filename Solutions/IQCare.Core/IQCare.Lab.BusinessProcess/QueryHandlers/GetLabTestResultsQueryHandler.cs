using AutoMapper;
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

namespace IQCare.Lab.BusinessProcess.QueryHandlers
{
    public class GetLabTestResultsQueryHandler : IRequestHandler<GetLabTestResults, List<LabTestResultViewModel>>
    {
        ILabUnitOfWork _labUnitOfWork;
        IMapper _mapper;
        public GetLabTestResultsQueryHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {
            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }
        public Task<List<LabTestResultViewModel>> Handle(GetLabTestResults request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                var labTestResults = _labUnitOfWork.Repository<PatientLabTracker>().Get(x => x.PatientId == request.PatientId).AsEnumerable();
                var labTestModel = _mapper.Map<List<LabTestResultViewModel>>(labTestResults);

                return Task.FromResult(labTestModel);
            }          
        }
    }
}
