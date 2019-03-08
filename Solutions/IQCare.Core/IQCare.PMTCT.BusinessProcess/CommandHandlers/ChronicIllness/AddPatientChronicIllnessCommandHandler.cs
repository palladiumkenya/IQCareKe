using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.ChronicIllness
{
    public class AddPatientChronicIllnessCommandHandler : IRequestHandler<AddPatientChronicIllnessCommand, Result<PatientChronicIllness>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientChronicIllnessCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientChronicIllness>> Handle(AddPatientChronicIllnessCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientChronicIllness> patientChronicIllness = new List<PatientChronicIllness>();

                    List<PatientChronicIllness> patientChronicIllnessExists = _unitOfWork
                        .Repository<PatientChronicIllness>().Get(x =>
                            x.PatientId == request.PatientChronicIllnesses[0].PatientId && !x.DeleteFlag)
                        .ToList();
                    if (request.PatientChronicIllnesses.Count > 0)
                    {
                        foreach (var item in request.PatientChronicIllnesses)
                        {
                            bool itemExist = patientChronicIllnessExists.Exists(x =>
                                x.PatientId == item.PatientId && x.ChronicIllness == item.ChronicIllness &&
                                x.OnsetDate.ToString() == item.OnsetDate.ToString());
                            if (!itemExist)
                            {
                                PatientChronicIllness patientChronic = new PatientChronicIllness()
                                {
                                    Active = 0,
                                    ChronicIllness = item.ChronicIllness,
                                    CreateBy = item.CreateBy,
                                    DeleteFlag = item.DeleteFlag,
                                    Dose = item.Dose,
                                    Duration = item.Duration,
                                    OnsetDate = item.OnsetDate,
                                    Treatment = item.Treatment,
                                    PatientId = item.PatientId,
                                    PatientMasterVisitId = item.PatientMasterVisitId,
                                };
                           
                                patientChronicIllness.Add(patientChronic);
                            }     
                        }

                        await _unitOfWork.Repository<PatientChronicIllness>().AddRangeAsync(patientChronicIllness);
                        await _unitOfWork.SaveAsync();
                        return Result<PatientChronicIllness>.Valid(request.PatientChronicIllnesses[0]);
                    }
                    else
                    {
                        return Result<PatientChronicIllness>.Valid(new PatientChronicIllness());
                    }

                    
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientChronicIllness>.Invalid(e.Message + e.InnerException);
                }
               
            }
        }
    }
}