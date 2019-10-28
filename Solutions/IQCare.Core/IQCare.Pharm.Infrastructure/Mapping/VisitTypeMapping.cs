using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.Infrastructure.Mapping
{
   public  class VisitTypeMapping : IEntityTypeConfiguration<VisitType>
    {
        public void Configure(EntityTypeBuilder<VisitType> builder)
        {
            builder.ToTable("mst_VisitType").HasKey(c => c.VisitTypeID);
        }
    }
}
