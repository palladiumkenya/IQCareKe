using System;
using DataAccess.Context;
using Entities.CCC.Tanners;
using System.Collections.Generic;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IRecordTannersStagingRepository: IRepository<TannersStaging>
    {
        List<TannersStaging> getRecordTannersStaging(int patientId);
    }
}
