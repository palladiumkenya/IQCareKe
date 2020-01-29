using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class OtzCompletedModulesViewMapping : IEntityTypeConfiguration<OtzCompletedModulesView>
    {
        public void Configure(EntityTypeBuilder<OtzCompletedModulesView> builder)
        {
            builder.ToTable("OtzCompletedModules").HasKey(c => c.Id);
        }
    }
}
