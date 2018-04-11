using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, Result<AddPatientResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientResponse>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var sqlPatient = "exec pr_OpenDecryptedSession;" +
                                     "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                                     $"Values(0, {request.PersonId}, {DateTime.Now.Year + '-' + request.PersonId}, 258, 13028, 1," +
                                     $"'{request.DateOfBirth}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, 1, GETDATE()," +
                                     $"NULL, 1);" +
                                     $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                                     $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                                     $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                                     $"exec [dbo].[pr_CloseDecryptedSession];";

                    var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient);

                    _unitOfWork.Dispose();

                    return Result<AddPatientResponse>.Valid(new AddPatientResponse()
                    {
                        PatientId = patientInsert[0].Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPatientResponse>.Invalid(e.Message);
            }
        }
    }
}