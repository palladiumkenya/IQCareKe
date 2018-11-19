﻿using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class GetPatientCounsellingViewCommand: IRequest<Result<List<PatientCounsellingView>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}