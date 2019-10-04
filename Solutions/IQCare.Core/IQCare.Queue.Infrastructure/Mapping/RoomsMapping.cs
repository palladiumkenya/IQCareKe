using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
   public class RoomsMapping:IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.ToTable("Rooms").HasKey(c => c.Id);
        }
           
    }
}
