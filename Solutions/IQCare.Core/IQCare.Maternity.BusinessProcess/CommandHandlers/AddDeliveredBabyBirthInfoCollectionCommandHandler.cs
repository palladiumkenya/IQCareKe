﻿using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddDeliveredBabyBirthInfoCollectionCommandHandler : IRequestHandler<AddDeliveredBabyBirthInfoCollectionCommand, Result<DeliveredBabyBirthInfoResult>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        public AddDeliveredBabyBirthInfoCollectionCommandHandler(IMaternityUnitOfWork maternityUnitOfWork,
        IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<DeliveredBabyBirthInfoResult>> Handle(AddDeliveredBabyBirthInfoCollectionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.DeliveredBabyBirthInfoCollection == null)
                    return Result<DeliveredBabyBirthInfoResult>.Invalid("Delivered baby bith info not found");

                foreach (var deliveredBabyBirthInfo in request.DeliveredBabyBirthInfoCollection)
                {
                    var deliveredBabyBirthInformation = _mapper.Map<DeliveredBabyBirthInformation>(deliveredBabyBirthInfo);
                    await _maternityUnitOfWork.Repository<DeliveredBabyBirthInformation>().AddAsync(deliveredBabyBirthInformation);

                    if (deliveredBabyBirthInfo.ApgarScores != null)
                    {
                        var apgarScores = deliveredBabyBirthInfo.ApgarScores.Select(x => new DeliveredBabyApgarScore(x.ApgarScoreId, deliveredBabyBirthInformation.Id, x.Score)).ToList();

                        await _maternityUnitOfWork.Repository<DeliveredBabyApgarScore>().AddRangeAsync(apgarScores);
                    }

                }

                await _maternityUnitOfWork.SaveAsync();

                return Result<DeliveredBabyBirthInfoResult>.Valid(new DeliveredBabyBirthInfoResult
                {
                    PatientDeliveryInformationId = request.DeliveredBabyBirthInfoCollection[0].PatientDeliveryInformationId
                });

            }
            catch (Exception ex)
            {
                Log.Error("An error ocured while adding delivered baby bith information", ex);
                return Result<DeliveredBabyBirthInfoResult>.Invalid(ex.Message);
            }
        }
    }
}
