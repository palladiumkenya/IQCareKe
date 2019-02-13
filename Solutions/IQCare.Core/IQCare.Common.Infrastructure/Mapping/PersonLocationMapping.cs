﻿using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonLocationMapping : IEntityTypeConfiguration<PersonLocation>
    {
        void IEntityTypeConfiguration<PersonLocation>.Configure(EntityTypeBuilder<PersonLocation> builder)
        {
            builder.ToTable("PersonLocation")
                .HasKey(c => c.Id);
        }
    }
}