using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class AddPatientPartnerProfileCommandHandler : IRequestHandler<AddPatientPartnerProfileCommand, Result<PatientProfileResponse>>
    {


        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        public string Msg { get; set; }

        public AddPatientPartnerProfileCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PatientProfileResponse>> Handle(AddPatientPartnerProfileCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    if (request.patientPartnerProfiles.Count > 0)
                    {
                        foreach (var patientprofile in request.patientPartnerProfiles)
                        {

                            if (patientprofile.Id > 0)
                            {
                                var result = await _prepUnitOfWork.Repository<PatientPartnerProfile>()
                                .FindByIdAsync(patientprofile.Id);
                                if (result != null)
                                {
                                    
                                    result.PatientId = request.PatientId;
                                  
                                    result.NumberofChildren = patientprofile.NumberofChildren;
                                    result.PartnerARTStartDate = patientprofile.PartnerARTStartDate;
                                    if(!String.IsNullOrEmpty(patientprofile.SexWithoutCondoms)== true)
                                    {
                                        result.SexWithoutCondoms = Convert.ToInt32(patientprofile.SexWithoutCondoms);
                                    }
                                    
                                    result.HIVSeroDiscordantDuration = patientprofile.HIVSeroDiscordantDuration;
                                    result.HivPositiveStatusDate = patientprofile.HivPositiveStatusDate;
                                    if (!String.IsNullOrEmpty(patientprofile.CCCEnrollment) == true)
                                    {
                                        result.CCCEnrollment = Convert.ToInt32(patientprofile.CCCEnrollment);
                                    }
                                    result.CCCNumber = patientprofile.CCCNumber;
                                    result.DeleteFlag = patientprofile.DeleteFlag;


                                    _prepUnitOfWork.Repository<PatientPartnerProfile>().Update(result);
                                    await _prepUnitOfWork.SaveAsync();



                                }
                                else
                                {
                                    PatientPartnerProfile plf = new PatientPartnerProfile();
                                    plf.PatientId = request.PatientId;
                                   
                                    plf.NumberofChildren = patientprofile.NumberofChildren;
                                    plf.PartnerARTStartDate = patientprofile.PartnerARTStartDate;
                                    if (!String.IsNullOrEmpty(patientprofile.SexWithoutCondoms) == true)
                                    {
                                        plf.SexWithoutCondoms = Convert.ToInt32(patientprofile.SexWithoutCondoms);

                                    }
                                    plf.HIVSeroDiscordantDuration = patientprofile.HIVSeroDiscordantDuration;
                                    plf.HivPositiveStatusDate = patientprofile.HivPositiveStatusDate;
                                    if (!String.IsNullOrEmpty(patientprofile.CCCEnrollment) == true)
                                    {
                                        plf.CCCEnrollment = Convert.ToInt32(patientprofile.CCCEnrollment);
                                    }
                                    plf.CCCNumber = patientprofile.CCCNumber;
                                    plf.DeleteFlag = patientprofile.DeleteFlag;
                                    plf.CreatedBy = patientprofile.CreatedBy;
                                    plf.CreateDate = DateTime.Now;

                                    await _prepUnitOfWork.Repository<PatientPartnerProfile>().AddAsync(plf);
                                    await _prepUnitOfWork.SaveAsync();


                                }

                            }
                            else
                            {
                                PatientPartnerProfile plf = new PatientPartnerProfile();
                                plf.PatientId = request.PatientId;
                               
                                plf.NumberofChildren = patientprofile.NumberofChildren;
                                plf.PartnerARTStartDate = patientprofile.PartnerARTStartDate;
                                if (!String.IsNullOrEmpty(patientprofile.SexWithoutCondoms) == true)
                                {
                                    plf.SexWithoutCondoms = Convert.ToInt32(patientprofile.SexWithoutCondoms);

                                }
                                plf.HIVSeroDiscordantDuration = patientprofile.HIVSeroDiscordantDuration;
                                plf.HivPositiveStatusDate = patientprofile.HivPositiveStatusDate;
                                if (!String.IsNullOrEmpty(patientprofile.CCCEnrollment) == true)
                                {
                                    plf.CCCEnrollment = Convert.ToInt32(patientprofile.CCCEnrollment);
                                }
                                plf.CCCNumber = patientprofile.CCCNumber;
                                plf.DeleteFlag = patientprofile.DeleteFlag;
                                plf.CreatedBy = patientprofile.CreatedBy;
                                plf.CreateDate = DateTime.Now;

                                await _prepUnitOfWork.Repository<PatientPartnerProfile>().AddAsync(plf);
                                await _prepUnitOfWork.SaveAsync();

                            }
                        }

                        Msg += "The hiv status partner profile has been saved successfully";


                    }


                    return Result<PatientProfileResponse>.Valid(new PatientProfileResponse()
                    {
                        Message = Msg
                    });
                }
                catch (Exception ex)
                {
                    String message = $"An error occured while saving partner hiv profile status request for patientId {request.PatientId}";
                    Log.Error(ex, message);

                    return Result<PatientProfileResponse>.Invalid(message);
                }

            }
        }

    }
}
