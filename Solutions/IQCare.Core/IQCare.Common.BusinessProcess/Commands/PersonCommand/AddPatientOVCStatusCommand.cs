using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;


namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPatientOVCStatusCommand : IRequest<Result<AddPatientOVCStatusResponse>>
    {
        public int PersonId { get; set; }

        public int GuardianId { get; set; }
        public bool Orphan { get; set; }
        public bool InSchool { get; set; }
        public bool Active { get; set; }
        public bool Deleteflag { get; set; }
        public int CreatedBy { get; set; }
    }

     public class AddPatientOVCStatusResponse
    {
        public int OVCStatusId{ get; set; }
    }
}
