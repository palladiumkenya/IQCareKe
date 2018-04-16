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
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                    int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;
                    var patientMasterVisit = new PatientMasterVisit()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        ServiceId = request.ClientEnrollment.ServiceAreaId,
                        Start = DateTime.Now,
                        End = null,
                        Active = false,
                        VisitDate = DateTime.Now,
                        VisitType = visitType,
                        Status = 1,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy
                    };

                    await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                    await _unitOfWork.SaveAsync();

                    var patientEnrollment = new PatientEnrollment()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        ServiceAreaId = request.ClientEnrollment.ServiceAreaId,
                        EnrollmentDate = request.ClientEnrollment.DateOfEnrollment,
                        EnrollmentStatusId = 0,
                        TransferIn = false,
                        CareEnded = false,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy,
                        CreateDate = DateTime.Now

                    };

                    await _unitOfWork.Repository<PatientEnrollment>().AddAsync(patientEnrollment);
                    await _unitOfWork.SaveAsync();

                    var patientIdentifier = new PatientIdentifier()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        PatientEnrollmentId = patientEnrollment.Id,
                        IdentifierTypeId = 8,
                        IdentifierValue = request.ClientEnrollment.EnrollmentNo,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy,
                        CreateDate = DateTime.Now,
                        Active = true

                    };

                    await _unitOfWork.Repository<PatientIdentifier>().AddAsync(patientIdentifier);
                    await _unitOfWork.SaveAsync();

                    GetPatientDetails patientDetails = new GetPatientDetails(_unitOfWork);
                    LookupLogic lookupLogic = new LookupLogic(_unitOfWork);

                    var patientLookup = await patientDetails.GetPatientByPatientId(request.ClientEnrollment.PatientId);

                    if (patientLookup.Count > 0)
                    {
                        Facility facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                        var referralId = await lookupLogic.GetDecodeIdByName("VCT", 17);
                        var maritalStatusId = await lookupLogic.GetDecodeIdByName(patientLookup[0].MaritalStatusName, 17);
                        var address = patientLookup[0].PhysicalAddress == null ? " " : patientLookup[0].PhysicalAddress;
                        var phone = patientLookup[0].MobileNumber == null ? " " : patientLookup[0].MobileNumber;
                        var dobPrecision = patientLookup[0].DobPrecision ? 1 : 0;

                        var gender = 0;
                        if (patientLookup[0].Gender == "Male")
                        {
                            gender = 16;
                        }
                        else if (patientLookup[0].Gender == "Female")
                        {
                            gender = 17;
                        }

                        StringBuilder sql = new StringBuilder();
                        sql.Append("exec pr_OpenDecryptedSession;");
                        sql.Append("Insert Into mst_Patient(FirstName, LastName, MiddleName, LocationID, PatientEnrollmentID, ReferredFrom, RegistrationDate, Sex, DOB, DobPrecision, MaritalStatus, Address, Phone, UserID, PosId, Status, DeleteFlag, CreateDate,MovedToPatientTable)");
                        sql.Append("Values(");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].FirstName}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].LastName}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].MidName}'),");
                        sql.Append($"'{facility.FacilityID}',");
                        sql.Append("' ',");
                        sql.Append($"'{referralId}',");
                        sql.Append($"'{request.ClientEnrollment.DateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sql.Append($"'{gender}',");
                        sql.Append($"'{patientLookup[0].DateOfBirth.ToString("yyyy-MM-dd")}',");
                        sql.Append($"'{dobPrecision}',");
                        sql.Append($"'{maritalStatusId}',");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{address}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{phone}'),");
                        sql.Append($"'{request.ClientEnrollment.CreatedBy}',");
                        sql.Append($"'{facility.PosID}',");
                        sql.Append("0,");
                        sql.Append("0,");
                        sql.Append($"'{request.ClientEnrollment.DateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sql.Append("1");
                        sql.Append(");");

                        sql.Append("SELECT Ptn_Pk, CAST(DECRYPTBYKEY([FirstName]) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY([LastName]) AS VARCHAR(50)) AS LastName, LocationID FROM [dbo].[mst_Patient] WHERE [Ptn_Pk] = SCOPE_IDENTITY();");
                        sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                        var result = await _unitOfWork.Repository<MstPatient>().FromSql(sql.ToString());

                        StringBuilder sqlBuilder = new StringBuilder();
                        sqlBuilder.Append("Insert Into Lnk_PatientProgramStart(Ptn_pk, ModuleId, StartDate, UserID, CreateDate)");
                        sqlBuilder.Append("Values(");
                        sqlBuilder.Append($"'{result[0].Ptn_Pk}',");
                        sqlBuilder.Append("283,");
                        sqlBuilder.Append($"'{request.ClientEnrollment.DateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sqlBuilder.Append($"'{request.ClientEnrollment.CreatedBy}',");
                        sqlBuilder.Append($"'{request.ClientEnrollment.DateOfEnrollment.ToString("yyyy-MM-dd")}'");
                        sqlBuilder.Append(");");

                        var insertResult = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlBuilder.ToString());

                        StringBuilder sqlPatient = new StringBuilder();
                        sqlPatient.Append($"UPDATE Patient SET ptn_pk = '{result[0].Ptn_Pk}' WHERE Id = '{request.ClientEnrollment.PatientId}';");
                        var updateResult = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlPatient.ToString());
                    }

                    trans.Commit();

                    _unitOfWork.Dispose();

                    return Result<EnrollClientResponse>.Valid(new EnrollClientResponse
                    {
                        IdentifierValue = request.ClientEnrollment.EnrollmentNo,
                        IdentifierId = patientIdentifier.Id
                    });
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Result<EnrollClientResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}