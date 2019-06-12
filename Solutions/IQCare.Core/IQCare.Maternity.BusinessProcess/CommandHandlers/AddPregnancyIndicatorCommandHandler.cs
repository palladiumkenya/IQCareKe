using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddPregnancyIndicatorCommandHandler : IRequestHandler<AddPregnancyIndicatorCommand, Result<PregnancyIndicatorResult>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;

        public AddPregnancyIndicatorCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PregnancyIndicatorResult>> Handle(AddPregnancyIndicatorCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var result = await _maternityUnitOfWork.Repository<PregnancyIndicator>().Get(x =>
                            x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId)
                        .ToListAsync();

                    if (result.Count > 0)
                    {
                        result[0].LMP = request.LMP;
                        result[0].EDD = request.EDD;
                        result[0].PregnancyStatusId = request.PregnancyStatusId;
                        result[0].ANCProfile = request.ANCProfile;
                        result[0].ANCProfileDate = request.ANCProfileDate;
                        result[0].VisitDate = request.VisitDate;

                        _maternityUnitOfWork.Repository<PregnancyIndicator>().Update(result[0]);
                        await _maternityUnitOfWork.SaveAsync();

                        return Result<PregnancyIndicatorResult>.Valid(new PregnancyIndicatorResult()
                        {
                            Message = "Successfully updated patient pregnancy indicator"
                        });
                    }

                    PregnancyIndicator pregnancyIndicator = new PregnancyIndicator()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        LMP = request.LMP,
                        EDD = request.EDD,
                        PregnancyStatusId = request.PregnancyStatusId,
                        ANCProfile = request.ANCProfile,
                        ANCProfileDate = request.ANCProfileDate,
                        VisitDate = request.VisitDate,
                        Active = request.Active,
                        DeleteFlag = request.DeleteFlag,
                        CreateDate = request.CreateDate,
                        CreatedBy = request.CreatedBy,
                        AuditData = request.AuditData
                    };

                    await _maternityUnitOfWork.Repository<PregnancyIndicator>().AddAsync(pregnancyIndicator);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<PregnancyIndicatorResult>.Valid(new PregnancyIndicatorResult()
                    {
                        Message = "Successfully added patient pregnancy indicator"
                    });
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not save pregnancy indicator for pregnancy result. Exception: {ex.Message}, InnerException: {ex.InnerException}");
                    return Result<PregnancyIndicatorResult>.Invalid($"Could not save pregnancy indicator for pregnancy result");
                }
            }
        }
    }
}