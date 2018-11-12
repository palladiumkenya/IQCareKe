using System;
using System.Collections.Generic;
using Entities.CCC.Tanners;

namespace Interface.CCC
{
    public interface ITannersStaging
    {
        int AddTannersStaging(PatientTannersStaging T);
        List<PatientTannersStaging> getTannersStaging(int patientId);
        void DeleteTanners(int Id);
        List<PatientTannersStaging> getPubicHair(int patientId, int pubicHair);
        List<PatientTannersStaging> getBreastsGenitals(int patientId, int breatsGenitals);
        List<PatientTannersStaging> getTannersHighestDate(int patientId, DateTime tannersStagingDate);
        int recordTannersStaging(TannersStaging R);
        List<TannersStaging> getRecordTannersStaging(int patientId);
        int updateRecordTannersStaging(TannersStaging TS);
    }
}
