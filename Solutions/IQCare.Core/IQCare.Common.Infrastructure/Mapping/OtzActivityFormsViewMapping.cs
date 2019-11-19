using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class OtzActivityFormsViewMapping : IEntityTypeConfiguration<OtzActivityFormsView>
    {
        public void Configure(EntityTypeBuilder<OtzActivityFormsView> builder)
        {
            builder.ToTable("OtzActivityForms").HasKey(c => c.Id);
        }
    }
}
