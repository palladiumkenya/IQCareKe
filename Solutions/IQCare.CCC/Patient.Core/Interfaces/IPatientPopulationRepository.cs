using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Core.Interfaces
{
    public  interface IPatientPopulationRepository:IRepository<IPatientRelationshipRepository>
    {

    }
}
