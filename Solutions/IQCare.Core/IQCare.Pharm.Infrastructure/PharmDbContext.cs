using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IQCare.Pharm.Core.Models;
using IQCare.SharedKernel.Infrastructure;

namespace IQCare.Pharm.Infrastructure
{
   public class PharmDbContext :BaseContext
    {

        public PharmDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyEntityTypeConfigsFromAssembly(Assembly.GetAssembly(typeof(PharmDbContext)));
            modelBuilder.Query<DrugListPoco>();
            modelBuilder.Query<PatientCurrentRegimenTracker>();
            modelBuilder.Query<Regimen>();
            modelBuilder.Query<DrugBatch>();
            modelBuilder.Query<PharmacyPtnPK>();
            modelBuilder.Query<PatientARTStartDate>();

        }
    }
}
