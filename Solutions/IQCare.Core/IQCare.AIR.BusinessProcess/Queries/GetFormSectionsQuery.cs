using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
namespace IQCare.AIR.BusinessProcess.Queries
{
   public  class GetFormSectionsQuery :IRequest<Result<List<SectionViewModel>>>
    {
        public int Id { get; set; }
    }

    public class SectionViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; }

        public Boolean Active { get; set; }
    }
}
