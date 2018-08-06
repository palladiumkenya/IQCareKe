using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.Commands
{
    public class EnrollPatientCommand:IRequest<Result<AddEnrollPatientResponse>>
    {
        public int facilityId { get; set; }
        public int EntryPointId { get; set; }
        public string nationalId { get; set; }

        public string EnrollmentDate { get; set; }
        public int PatientType { get; set; }

        public string DobPrecision { get; set; }

        public Dictionary<string,string> identifiersList { get; set; }
        public int PersonId { get; set; }
        public string DateofBirth { get; set; }
        public int UserId { get; set; }
        
    }

    public class AddEnrollPatientResponse
    {

        public int IdentifierId { get; set; }
        public string IdentifierValue { get; set; }

        public string Message { get; set; }

        public int PatientId { get; set; }
    }
}
