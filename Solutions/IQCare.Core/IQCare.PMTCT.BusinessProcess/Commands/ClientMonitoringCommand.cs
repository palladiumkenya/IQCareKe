using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class ClientMonitoringCommand : IRequest<Result<ClientMonitoringCommandResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int WhoStage { get; set; }
        public int? FacilityId { get; set; }
        public int? ServiceAreaId { get; set; }
        public string ClinicalNotes { get; set; }
        public int ScreeningTypeId { get; set; }
        public Boolean ScreeningDone { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int screenedTB { get; set; }
        public int cacxMethod { get; set; }
        public int cacxResult { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ClientMonitoringCommandResponse
    {
        public int resultId { get; set; }
    }
}
