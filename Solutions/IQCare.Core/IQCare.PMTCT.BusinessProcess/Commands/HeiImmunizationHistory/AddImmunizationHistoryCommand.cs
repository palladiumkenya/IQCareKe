using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory
{
    public class AddImmunizationHistoryCommand :IRequest<Result<Vaccination>>
    {
        public Vaccination Vaccination;
    }
}
