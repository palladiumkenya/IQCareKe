﻿using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
   public  class GetLatestCareEndingDetailsCommand :IRequest<Result<PatientCareEnding>>
    {
         public int PatientId { get; set; }


    }


}
