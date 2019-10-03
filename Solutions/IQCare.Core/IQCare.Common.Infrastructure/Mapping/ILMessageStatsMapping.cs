using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class ILMessageStatsMapping : IEntityTypeConfiguration<ILMessageStats>
    {
        public void Configure(EntityTypeBuilder<ILMessageStats> builder)
        {
            builder.ToTable("vw_ILMessageStats").HasKey(c => c.RowID);
        }
    }
}
