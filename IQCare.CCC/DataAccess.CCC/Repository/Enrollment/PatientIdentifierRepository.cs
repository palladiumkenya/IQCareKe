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
   public class PatientIdentifierRepository:BaseRepository<PatientIdentifier>,IPatientIdentifierRepository
   {
       private readonly GreencardContext _context;

       public PatientIdentifierRepository():this(new GreencardContext())
       {
           
       }

       public PatientIdentifierRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }
   }
}
