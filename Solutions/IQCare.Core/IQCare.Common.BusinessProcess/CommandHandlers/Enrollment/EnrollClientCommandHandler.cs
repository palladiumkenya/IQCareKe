using System;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class EnrollClientCommandHandler : IRequestHandler<EnrollClientCommand, Result<EnrollClientResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public EnrollClientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<EnrollClientResponse>> Handle(EnrollClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                await registerPersonService.DynamicEnrollment(request.ClientEnrollment.PatientId,
                    request.ClientEnrollment.ServiceAreaId, request.ClientEnrollment.CreatedBy,
                    request.ClientEnrollment.DateOfEnrollment, request.ClientEnrollment.ServiceIdentifiersList);

                /*var patientIdentifier = await registerPersonService.EnrollPatient(request.ClientEnrollment.EnrollmentNo,
                    request.ClientEnrollment.PatientId, request.ClientEnrollment.ServiceAreaId,
                    request.ClientEnrollment.CreatedBy, request.ClientEnrollment.DateOfEnrollment);*/

                GetPatientDetails patientDetails = new GetPatientDetails(_unitOfWork);


                var patientLookup = await patientDetails.GetPatientByPatientId(request.ClientEnrollment.PatientId);

                if (patientLookup.Count > 0 && (patientLookup[0].ptn_pk == 0 || patientLookup[0].ptn_pk == null))
                {
                    var dobPrecision = "EXACT";
                    var dob = DateTime.Now;
                    if(patientLookup[0].DobPrecision.HasValue)
                        dobPrecision = patientLookup[0].DobPrecision.Value ? "ESTIMATED" : "EXACT";

                    if (patientLookup[0].DateOfBirth.HasValue)
                        dob = patientLookup[0].DateOfBirth.Value;


                    var response = await registerPersonService.InsertIntoBlueCard(
                        patientLookup[0].FirstName,
                        patientLookup[0].LastName,
                        patientLookup[0].MidName,
                        request.ClientEnrollment.DateOfEnrollment,
                        patientLookup[0].MaritalStatusName,
                        patientLookup[0].PhysicalAddress,
                        patientLookup[0].MobileNumber,
                        patientLookup[0].Gender,
                        dobPrecision,
                        dob,
                        request.ClientEnrollment.CreatedBy,
                        request.ClientEnrollment.PosId
                        );

                    if (response.Count > 0)
                    {
                        await registerPersonService.UpdatePatient(request.ClientEnrollment.PatientId,
                            request.ClientEnrollment.DateOfEnrollment, request.ClientEnrollment.PosId);
                    }
                }


                return Result<EnrollClientResponse>.Valid(new EnrollClientResponse()
                {
                    Message = "Success"
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result<EnrollClientResponse>.Invalid(e.Message);
            }
        }
    }
}