using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class DeliveredBabyApgarScoreViewMapping : IEntityTypeConfiguration<DeliveredBabyApgarScoreView>
    {
        public void Configure(EntityTypeBuilder<DeliveredBabyApgarScoreView> builder)
        {
            builder.ToTable(nameof(DeliveredBabyApgarScoreView)).HasKey(x=>x.Id);
        }
    }
}
