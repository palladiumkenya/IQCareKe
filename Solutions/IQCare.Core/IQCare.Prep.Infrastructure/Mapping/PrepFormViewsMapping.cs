using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace IQCare.Prep.Infrastructure.Mapping
{
   public  class PrepFormViewsMapping : IEntityTypeConfiguration<PrepFormsView>
    {
        public void Configure(EntityTypeBuilder<PrepFormsView> builder)
        {
            builder.ToTable("PREP_FormsView")
                .HasKey(c => c.RowID);
        }
    }
}
