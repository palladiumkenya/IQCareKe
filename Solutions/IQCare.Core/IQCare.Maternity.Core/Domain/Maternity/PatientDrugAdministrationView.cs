using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PatientDrugAdministrationView
    {
        public PatientDrugAdministrationView()
        {

        }

        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public string StrDrugAdministered { get; set; }
        public int DrugAdministered { get; private set; }
        public int Value { get; private set; }
        public string StrValue { get; set; }
        public string Description { get; private set; }
        public bool DeleteFlag { get; private set; }
        public int ? CreatedBy { get; private set; }
        public DateTime ? DateCreated { get; private set; }
    }
}
