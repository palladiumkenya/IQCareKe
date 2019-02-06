using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class GetLabTestsCommand : IRequest<Result<IEnumerable<LabTestViewModel>>>
    {
        public string[] LabTests { get; set; }
    }

    public class LabTestViewModel
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string LoincCode { get; set; }
        public bool IsGroup { get; set; }
        public int DepartmentId { get; set; }
        public int ParameterCount { get; set; }
        public bool Active { get; set; }
    }
}