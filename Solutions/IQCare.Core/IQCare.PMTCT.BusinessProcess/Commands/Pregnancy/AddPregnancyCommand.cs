using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
   public class AddPregnancyCommand : IRequest<Library.Result<AddPregnancyCommandResult>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? Lmp { get; set; }
        public DateTime? Edd { get; set; }
        public decimal? Gestation { get; set; }
        public int? Gravidae { get; set; }
        public int? Parity { get; set; }
        public int? Parity2 { get; set; }
        public decimal ? AgeAtMenarche { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AddPregnancyCommandResult
    {
        public int PregnancyId { get; set; }
    }

}
