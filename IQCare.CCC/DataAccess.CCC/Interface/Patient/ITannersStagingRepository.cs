using System;
using DataAccess.Context;
using Entities.CCC.Tanners;
using System.Collections.Generic;

namespace DataAccess.CCC.Interface.Patient
{
    public interface ITannersStagingRepository : IRepository<PatientTannersStaging>
    {
        List<PatientTannersStaging> getTannersStaging(int patientId);
        List<PatientTannersStaging> getPubicHair(int patientId, int pubicHair);
        List<PatientTannersStaging> getBreastsGenitals(int patientId, int breastsGenitals);
        List<PatientTannersStaging> getTannersHighestDate(int patientId, DateTime tannersStagingDate);
    }
}