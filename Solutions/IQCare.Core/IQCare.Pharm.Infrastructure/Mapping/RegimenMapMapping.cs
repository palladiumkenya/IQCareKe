using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;
namespace IQCare.Pharm.Infrastructure.Mapping
{
   public class RegimenMapMapping: IEntityTypeConfiguration<RegimenMap>
    {
        public void Configure(EntityTypeBuilder<RegimenMap> builder)
    {
        builder.ToTable("dtl_RegimenMap").HasKey(c => c.RegimenMap_Pk);
    }
}
}
