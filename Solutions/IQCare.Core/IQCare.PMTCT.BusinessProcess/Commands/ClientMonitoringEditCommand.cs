using System;
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class ClientMonitoringEditCommand: IRequest<Result<ClientMonitoringCommandEditResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ViralLoadSampleTaken { get; set; }
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
    public class ClientMonitoringCommandEditResponse
    {
        public int resultId { get; set; }
    }
}