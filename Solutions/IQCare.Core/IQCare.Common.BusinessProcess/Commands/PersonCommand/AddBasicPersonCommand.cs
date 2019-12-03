using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;



namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
   public  class AddBasicPersonCommand : IRequest<Result<RegisterClientResponse>>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int  Sex { get; set; }

        public int CreatedBy { get; set; }

        public int FacilityId { get; set; }

       
    }



}
