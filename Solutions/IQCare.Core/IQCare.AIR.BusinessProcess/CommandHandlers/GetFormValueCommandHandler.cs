using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.BusinessProcess.CommandHandlers
{
    public class GetFormValueCommandHandler:IRequestHandler<GetFormValueCommand,Result<GetFormValueResponse>>
    {

    }
}
