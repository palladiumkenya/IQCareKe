using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class EditAppointmentCommand:IRequest<Result<EditAppointmentCommandResponse>>
    {

    }

    public class EditAppointmentCommandResponse
    {
        public int Id { get; set; }
    }
}
