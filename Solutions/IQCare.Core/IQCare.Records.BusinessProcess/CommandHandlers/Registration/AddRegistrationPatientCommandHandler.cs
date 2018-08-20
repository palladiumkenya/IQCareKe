using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Records.BusinessProcess.Command;
using MediatR;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Registration
{
    public class AddRegistrationPatientCommandHandler : IRequestHandler<AddRegistrationPatientCommand, Result<AddRegistrationPatientResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        int patientid;
        public AddRegistrationPatientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddRegistrationPatientResponse>> Handle(AddRegistrationPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    //var sqlPatient = "exec pr_OpenDecryptedSession;" +
                    //                 "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                    //                 $"Values(0, {request.PersonId}, {DateTime.Now.Year + '-' + request.PersonId}, 258, 13028, 1," +
                    //                 $"'{request.DateOfBirth.ToString("yyyy-MM-dd")}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, 1, GETDATE()," +
                    //                 $"NULL, 1);" +
                    //                 $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                    //                 $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                    //                 $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                    //                 $"exec [dbo].[pr_CloseDecryptedSession];";

                    //var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient);

                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    if (request.PersonId > 0)
                    {
                        var patient = await registerPersonService.GetPatientByPersonId(request.PersonId);

                        if (patient != null)
                        {
                            var pat = await registerPersonService.UpdateRegistrationPatient(request.PersonId, request.RegistrationDate, request.NationalId, request.DateOfBirth, request.UserId);
                            patientid = pat.Id;
                        }
                        else
                        {
                            var patadd = await registerPersonService.AddRegistrationPatient(request.PersonId, request.RegistrationDate, request.NationalId, request.DateOfBirth, request.UserId);
                            patientid = patadd.Id;
                        }
                      
                    }
                    else
                    {
                        var patient = await registerPersonService.AddRegistrationPatient(request.PersonId, request.RegistrationDate, request.NationalId, request.DateOfBirth, request.UserId);
                        patientid = patient.Id;
                    }
                    _unitOfWork.Dispose();

                    return Result<AddRegistrationPatientResponse>.Valid(new AddRegistrationPatientResponse()
                    {
                        PatientId = patientid
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddRegistrationPatientResponse>.Invalid(e.Message);
            }
        }
    }
}