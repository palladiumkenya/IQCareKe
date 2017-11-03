using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Interface.Interoperability
{
    public interface IPatientAppointmentMessageRepository:IRepository<PatientAppointmentMessage>
    {
    }
}
