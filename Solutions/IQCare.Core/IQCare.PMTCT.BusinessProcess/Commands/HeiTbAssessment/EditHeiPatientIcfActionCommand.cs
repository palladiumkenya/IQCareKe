﻿using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment
{
    public class EditHeiPatientIcfActionCommand: IRequest<Result<HEiPatientIcfAction>>
    {
        public HEiPatientIcfAction HEiPatientIcfAction { get; set; }
    }
}