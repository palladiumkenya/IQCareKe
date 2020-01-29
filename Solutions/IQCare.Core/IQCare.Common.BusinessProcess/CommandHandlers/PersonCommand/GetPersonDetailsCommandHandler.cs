using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonDetailsCommandHandler : IRequestHandler<GetPersonDetailsCommand, Result<PatientLookupView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientLookupView>> Handle(GetPersonDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append("SELECT ISNULL(ROW_NUMBER() OVER(ORDER BY PersonId ASC), -1) AS RowID, * ");
                    sql.Append(" FROM ( ");
                    sql.Append("SELECT P.[Id] PersonId, P.DeleteFlag, PT.Id PatientId, PT.ptn_pk, CAST(DECRYPTBYKEY(P.[FirstName]) AS VARCHAR(50)) AS[FirstName], CAST(DECRYPTBYKEY(P.[MidName]) AS VARCHAR(50)) AS[MidName],");
                    sql.Append("CAST(DECRYPTBYKEY(P.[LastName]) AS VARCHAR(50)) AS[LastName], P.Sex, Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'), ");
                    sql.Append("DateOfBirth = ISNULL(P.DateOfBirth, PT.DateOfBirth), PT.[DobPrecision], PatientType = CASE(SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PT.PatientType AND MasterName = 'PatientType') WHEN 'New' THEN 'NEW' WHEN 'Transfer-In' THEN 'TRANSFER-IN' WHEN 'Transit' THEN 'TRANSIT' ELSE '' END, ");
                    sql.Append("CAST(DECRYPTBYKEY(PT.[NationalId]) AS VARCHAR(50)) AS[NationalId], ISNULL(P.RegistrationDate, PT.RegistrationDate) AS[RegistrationDate], PE.EnrollmentDate, pni.IdentifierValue, ");
                    sql.Append("SE.Id ServiceAreaId, SE.Name ServiceAreaName, CAST(DECRYPTBYKEY(PC.PhysicalAddress) AS VARCHAR(50)) AS PhysicalAddress, CAST(DECRYPTBYKEY(PC.MobileNumber) AS VARCHAR(50)) AS MobileNumber, ");
                    sql.Append("PMS.MaritalStatusId, MaritalStatusName = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PMS.MaritalStatusId AND MasterName = 'MaritalStatus'), PL.LandMark, County = (select TOP 1 CountyName from County where WardId = PL.Ward), ");
                    sql.Append("SubCounty = (select TOP 1 Subcountyname from County where WardId = PL.Ward), Ward = (select TOP 1 WardName from County where WardId = PL.Ward) ");
                    sql.Append("FROM[dbo].[Person] P LEFT JOIN dbo.Patient AS PT ON P.Id = PT.PersonId LEFT JOIN dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId LEFT JOIN dbo.PatientIdentifier AS pni ON pni.PatientId = PT.Id and PE.Id = pni.PatientEnrollmentId ");
                    sql.Append("LEFT JOIN dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id LEFT JOIN dbo.ServiceArea SE ON SE.Id = PE.ServiceAreaId LEFT JOIN dbo.PersonContact PC ON PC.PersonId = P.Id LEFT JOIN[dbo].[PatientMaritalStatus] PMS ON PMS.PersonId = P.Id ");
                    sql.Append("LEFT JOIN[dbo].[PersonLocation] PL ON PL.PersonId = P.Id ) A");
                    sql.Append($" WHERE A.PersonId = {request.PersonId};");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql.ToString());
                    result.ForEach(item =>
                    {
                        item.CalculateYourAge();
                        item.CalculateAgeInMonths();
                    });

                    return Result<PatientLookupView>.Valid(result.FirstOrDefault());
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientLookupView>.Invalid(e.Message);
                }
            }
        }
    }
}