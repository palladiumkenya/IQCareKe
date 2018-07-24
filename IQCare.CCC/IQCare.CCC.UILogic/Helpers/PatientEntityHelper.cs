using Entities.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Helpers
{
    public sealed class PatientEntityHelper
    {
        public static PatientEntity MapFromPatientPersonView(PatientPersonViewEntity entity)
        {
            return new PatientEntity()
            {
                Id = entity.Id,

                PersonId = entity.PersonId,
                ptn_pk = entity.ptn_pk,
                PatientIndex = entity.PatientIndex,
                FacilityId = entity.FacilityId,
                PatientType = entity.PatientType,
                Active = entity.Active,
                DateOfBirth = entity.DateOfBirth,
                NationalId = entity.NationalId,
                DobPrecision = entity.DobPrecision

            };
        }
    }
}
