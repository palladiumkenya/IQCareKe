using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.BusinessProcess.Commands.DrugAdministration
{
  public  class AddDrugAdministrationCommand:IRequest<Result<AddDrugAdministrationResponse>>
    {
        public PatientDrugAdministration patientDrugAdministration;
    }

   public class AddDrugAdministrationResponse
    {
        public int Id { get; set; }
    }
}
