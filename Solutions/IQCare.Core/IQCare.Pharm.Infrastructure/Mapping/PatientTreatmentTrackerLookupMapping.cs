using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;
using System.Linq;
namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class PatientTreatmentTrackerLookupMapping : IEntityTypeConfiguration<PatientTreamentTrackerLookup>
    {
        public void Configure(EntityTypeBuilder<PatientTreamentTrackerLookup> builder)
        {

            builder.ToTable("PatientTreatmentTrackerView").HasKey(x => x.Id);
        }
    }
}
