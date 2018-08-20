using System;
using System.Collections.Generic;
using System.Text;
 using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class ContactsListViewMapping : IEntityTypeConfiguration<ContactsListView>
    {
        public void Configure(EntityTypeBuilder<ContactsListView> builder)
        {
            builder.ToTable("ContactsListView").HasKey(c => c.Id);
        }
    }
    
}
