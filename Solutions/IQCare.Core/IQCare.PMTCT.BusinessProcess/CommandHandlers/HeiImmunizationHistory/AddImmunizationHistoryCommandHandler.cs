using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiImmunizationHistory
{
    public class AddImmunizationHistoryCommandHandler: IRequestHandler<AddImmunizationHistoryCommand, Result<VaccinationResponse>>
   {
       private readonly IPmtctUnitOfWork _unitOfWork;

       public AddImmunizationHistoryCommandHandler(IPmtctUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public async Task<Result<VaccinationResponse>> Handle(AddImmunizationHistoryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<Vaccination> vaccinations= new List<Vaccination>();

                    foreach (var vaccine in request.Vaccinations)
                    {
                        var vacinneItem= new Vaccination()
                        {
                            AppointmentId = vaccine.AppointmentId,
                            Active = vaccine.Active,
                            CreateDate = vaccine.CreateDate,
                            CreatedBy = vaccine.CreatedBy,
                            DeleteFlag = vaccine.DeleteFlag,
                            PatientId = vaccine.PatientId,
                            PatientMasterVisitId = vaccine.PatientMasterVisitId,
                            Period = vaccine.Period,
                            VaccineDate = vaccine.VaccineDate,
                            VaccineStage = vaccine.VaccineStage,
                            Vaccine = vaccine.Vaccine,                          
                        };
                        vaccinations.Add(vacinneItem);
                    }

                    await _unitOfWork.Repository<Vaccination>().AddRangeAsync(vaccinations);
                    await _unitOfWork.SaveAsync();
                    return Result<VaccinationResponse>.Valid(new VaccinationResponse()
                    {
                        Message = "Vaccination Added successfully"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<VaccinationResponse>.Invalid(e.Message);
                }
            }
           
        }
    }
}
