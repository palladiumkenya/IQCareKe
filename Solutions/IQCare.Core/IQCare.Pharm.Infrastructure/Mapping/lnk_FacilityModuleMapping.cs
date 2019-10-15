using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;
using System.Linq;

namespace IQCare.Pharm.Infrastructure.Mapping
{
   public  class lnk_FacilityModuleMapping : IEntityTypeConfiguration<lnk_FacilityModule>
    {
        public void Configure(EntityTypeBuilder<lnk_FacilityModule> builder)
        {

            builder.ToTable("lnk_FacilityModule").HasKey(x => new { x.FacilityID, x.ModuleID });
        }
    }
}
