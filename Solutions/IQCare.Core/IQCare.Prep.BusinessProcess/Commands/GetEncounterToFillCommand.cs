using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class GetEncounterToFillCommand:IRequest<Result<ResponseEncounter>>
    {
        public int PatientId { get; set; }

        public DateTime VisitDate { get; set; }

        public string EmrMode { get; set; }
    }

    public class ResponseEncounter
    {
        public PrepFormsView PrepFormsView { get; set; }

        public string EncounterType { get; set; }
    }
}
