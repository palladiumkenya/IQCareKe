using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
   public class AddPregnancyCommand : IRequest<Result<AddPregnancyCommandResult>>
    {
        public PatientPregnancy pregnancy;
    }

    public class AddPregnancyCommandResult
    {
        public int PregnancyId { get; set; }
        public int PatientId { get; set; }
    }

}
