using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonLocationViewMapping: IEntityTypeConfiguration<PersonLocationView>
    {
        public void Configure(EntityTypeBuilder<PersonLocationView> builder)
        {
            builder.ToTable("Api_PatientLocationView")
                .HasKey(c => c.PersonId);
        }
    }
}
