using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Core.Models.HEI;

namespace IQCare.PMTCT.Services.Interface
{
    public interface IHeiService
    {
        Task<HeiFeeding> AddHeiFeeding(HeiFeeding heiFeeding);
        Task<HeiFeeding> EditHeiFeeding(HeiFeeding heiFeeding);
        Task<HeiFeeding> GetHeiFeeding(int id);
        Task<HeiFeeding> DeleteHeiFeeding(int patientId);
        Task<List<HeiFeeding>> GetAllHeiFeeding(int patientId);
    }
}
