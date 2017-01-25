using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
   public class PatientEntrypointRepository:BaseRepository<PatientEntryPoint>,IPatientEntryPointRepository
   {
       private readonly GreencardContext _context;

       public PatientEntrypointRepository() : this(new GreencardContext())
       {
           
       }

       public PatientEntrypointRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }
   }
}
