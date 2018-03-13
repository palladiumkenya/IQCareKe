using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers
{
    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result<RegisterClientResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        public RegisterClientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<RegisterClientResponse>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("FirstName", SqlDbType.VarChar) { Value = request.Person.FirstName },
                    new SqlParameter("MidName", SqlDbType.VarChar) { Value = request.Person.MiddleName },
                    new SqlParameter("LastName", SqlDbType.VarChar) { Value = request.Person.LastName },
                    new SqlParameter("DateOfBirth", SqlDbType.DateTime) { Value = request.Person.DateOfBirth },
                    new SqlParameter("DobPrecision", SqlDbType.Bit) { Value = true },
                    new SqlParameter("UserId", SqlDbType.Int) { Value = 1 },
                    new SqlParameter("Sex", SqlDbType.Int) { Value = request.Person.Sex },
                };

                int personId = await _unitOfWork.Repository<Person>().ExecWithStoreProcedureAsync(
                    "[dbo].[Person_Insert] @FirstName, @MidName, @LastName, " +
                    "@Sex, @DateOfBirth, @DobPrecision, @UserId", parameters);

                SqlParameter[] patientParams =
                {
                    new SqlParameter("PersonId", SqlDbType.VarChar) { Value = personId },
                    new SqlParameter("ptn_pk", SqlDbType.VarChar) { Value = null },
                    new SqlParameter("PatientIndex", SqlDbType.VarChar) { Value = DateTime.Now.Year + '-' + personId },
                    new SqlParameter("DateOfBirth", SqlDbType.DateTime) { Value = request.Person.DateOfBirth },
                    new SqlParameter("NationalId", SqlDbType.VarChar) { Value = "99999999" },
                    new SqlParameter("FacilityId", SqlDbType.Int) { Value = 1 },
                    new SqlParameter("UserId", SqlDbType.Int) { Value = 1 },
                    new SqlParameter("Active", SqlDbType.Int) { Value = true },
                    new SqlParameter("PatientType", SqlDbType.Int) { Value = 1 },
                    new SqlParameter("DobPrecision", SqlDbType.Bit) { Value = false },
                };

                await _unitOfWork.Repository<Person>().ExecWithStoreProcedureAsync(
                    "[dbo].[Patient_Insert] @PersonId, @ptn_pk, @PatientIndex, @DateOfBirth, " +
                    "@NationalId, @FacilityId, @UserId, @Active, @PatientType, @DobPrecision", patientParams);


                return Result<RegisterClientResponse>.Valid(new RegisterClientResponse {PersonId = personId});

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}