using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PregnancyMapping : IEntityTypeConfiguration<Pregnancy>
    {
        public void Configure(EntityTypeBuilder<Pregnancy> builder)
        {
            builder.ToTable(nameof(Pregnancy));
        }
    }
}
