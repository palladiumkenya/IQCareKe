using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
   public  class PersonContactViewMapping : IEntityTypeConfiguration<PersonContactView>
    {
        void IEntityTypeConfiguration<PersonContactView>.Configure(EntityTypeBuilder<PersonContactView> builder)
        {
            builder.ToTable("PersonContactView")
                .HasKey(c => c.Id);
        }
    }
}
