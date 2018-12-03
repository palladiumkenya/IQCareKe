using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class BaselineAntenatalCareMapping : IEntityTypeConfiguration<BaselineAntenatalCare>
    {
        public void Configure(EntityTypeBuilder<BaselineAntenatalCare> builder)
        {
            builder.ToTable("BaselineAntenatalCare")
                .HasKey(c => c.Id);
        }
    }
}                      