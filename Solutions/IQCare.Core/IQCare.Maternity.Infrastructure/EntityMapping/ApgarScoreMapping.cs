using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class ApgarScoreMapping : IEntityTypeConfiguration<DeliveredBabyApgarScore>
    {
        public void Configure(EntityTypeBuilder<DeliveredBabyApgarScore> builder)
        {
            builder.ToTable("DeliveredBabyApgarScore").HasKey(x=>x.Id);
        }
    }
}
