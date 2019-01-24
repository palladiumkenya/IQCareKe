using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class MaternalDrugAdministration
    {
        public MaternalDrugAdministration()
        {

        }
        public MaternalDrugAdministration(int patientId, int masterVisitId, int drugAdministered, int value, 
            string description, int createdBy)
        {
            PatientId = patientId;
            PatientMasterVisitId = masterVisitId;
            DrugAdministered = drugAdministered;
            Value = value;
            Description = description;
            CreatedBy = createdBy;
            DateCreated = DateTime.Now;
        }
        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public int DrugAdministered { get; private set; }
        public int Value { get; private set; }
        public string Description { get; private set; }
        public bool DeleteFlag { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime DateCreated { get; private set; }

        public void DeactivateDrugAdministration() => DeleteFlag = true;

        public void Update(int drugAdministered, int value, string description)
        {
            DrugAdministered = drugAdministered;
            Value = value;
            Description = description;
        }
    }
}
