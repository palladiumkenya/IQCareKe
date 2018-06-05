using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class UpdatePatientScreeningCommand : IRequest<Result<UpdatePatientScreeningResponse>>
    {
        public List<KeyValuePair<string, int>> ScreeningType { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int UserId { get; set; }
    }

    public class UpdatePatientScreeningResponse
    {
        public bool isUpdateSuccessful { get; set; }
    }
}