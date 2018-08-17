using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
   public class PersonListViewMapping : IEntityTypeConfiguration<PersonListView>
    {
        public void Configure(EntityTypeBuilder<PersonListView> builder)
        {

            builder.ToTable("PersonListView")
                .HasKey(c => c.Id);
        }
    }
}
