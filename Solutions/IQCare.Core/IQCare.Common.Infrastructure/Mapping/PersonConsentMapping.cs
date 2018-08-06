using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class    PersonConsentMapping: IEntityTypeConfiguration<PersonConsent>
    {
        public void Configure(EntityTypeBuilder<PersonConsent> builder)
        {
            builder.ToTable("PersonConsent").HasKey(c => c.Id);
        }
    }
}
