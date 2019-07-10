using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddPregnancyLogCommandHandler : IRequestHandler<AddPregnancyLogCommand, Result<AddPregnancyLogResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;

        public AddPregnancyLogCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddPregnancyLogResponse>> Handle(AddPregnancyLogCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    if (request.Id.HasValue)
                    {
                        var pregnancy = await _maternityUnitOfWork.Repository<PregnancyLog>().FindByIdAsync(request.Id.Value);
                        pregnancy.LMP = request.LMP;
                        pregnancy.EDD = request.EDD;
                        pregnancy.Outcome = request.Outcome;
                        pregnancy.DateOfOutcome = request.DateOfOutcome;

                        _maternityUnitOfWork.Repository<PregnancyLog>().Update(pregnancy);
                        await _maternityUnitOfWork.SaveAsync();
                    }
                    else
                    {
                        PregnancyLog pregnancyLog = new PregnancyLog()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            LMP = request.LMP,
                            EDD = request.EDD,
                            Outcome = request.Outcome,
                            DateOfOutcome = request.DateOfOutcome,
                            CreatedBy = request.CreatedBy,
                            CreateDate = DateTime.Now,
                            DeleteFlag = 0,
                            Active = true
                        };

                        await _maternityUnitOfWork.Repository<PregnancyLog>().AddAsync(pregnancyLog);
                        await _maternityUnitOfWork.SaveAsync();
                    }

                    return Result<AddPregnancyLogResponse>.Valid(new AddPregnancyLogResponse()
                    {
                        Message = "Successfully added pregnancy log"
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while trying add patient pregnancy log");
                    return Result<AddPregnancyLogResponse>.Invalid($"An error occured while trying add patient pregnancy log");
                }
            }
        }
    }
}