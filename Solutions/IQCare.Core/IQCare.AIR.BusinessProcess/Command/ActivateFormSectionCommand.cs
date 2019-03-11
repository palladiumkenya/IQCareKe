using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.BusinessProcess.Command
{
    public class ActivateFormSectionCommand : IRequest<Result<ActivateFormSectionResponse>>
    {
        public List<SectionModel> SectionList { get; set; }
    }

    public class SectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Boolean Active { get; set; }
    }

    public class ActivateFormSectionResponse
    {
        public string Message { get; set; }
    }
}
