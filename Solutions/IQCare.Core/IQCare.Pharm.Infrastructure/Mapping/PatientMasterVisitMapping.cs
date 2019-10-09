using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.Infrastructure.Mapping
{
   public  class PatientMasterVisitMapping : IEntityTypeConfiguration<PatientVisit>
    {
        public void Configure(EntityTypeBuilder<PatientVisit> builder)
        {
            builder.ToTable("ord_Visit").HasKey(c => c.Visit_Id);
        }
    }
}
