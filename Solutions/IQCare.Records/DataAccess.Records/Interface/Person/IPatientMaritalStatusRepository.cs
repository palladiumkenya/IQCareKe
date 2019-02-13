using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using Entities.PatientCore;
namespace DataAccess.Records.Interface
{
    public interface IPatientMaritalStatusRepository:IRepository<PatientMaritalStatus>
    {
    }
}
