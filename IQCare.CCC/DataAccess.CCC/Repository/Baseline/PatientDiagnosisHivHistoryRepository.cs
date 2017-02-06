using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientDiagnosisHivHistoryRepository:BaseRepository<DiagnosisArvHistory>,IPatientDiagnosisHivHistoryRepository
    {
        private readonly GreencardContext _context;

        public PatientDiagnosisHivHistoryRepository() : this(new GreencardContext())
        {
            
        }

        public PatientDiagnosisHivHistoryRepository(GreencardContext context) : base(context)
        {
            _context = context;
        } 
    }
}
