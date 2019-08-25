using System;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class AddServiceEntryPointCommand : IRequest<Result<ServiceEntryPoint>>
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public int EntryPointId { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }
    }
}