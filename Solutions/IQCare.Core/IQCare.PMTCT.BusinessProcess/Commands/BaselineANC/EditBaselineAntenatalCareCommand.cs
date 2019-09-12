using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.BaselineANC
{
    public class EditBaselineAntenatalCareCommand: IRequest<Result<BaselineAntenatalCare>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PregnancyId { get; set; }
        public int HivStatusBeforeAnc { get; set; }
        public int? TreatedForSyphilis { get; set; }
        public int? TestedForSyphilis { get; set; }
        public int BreastExamDone { get; set; }
        public int? SyphilisTestUsed { get; set; }
        public int? SyphilisResults { get; set; }
    }
}