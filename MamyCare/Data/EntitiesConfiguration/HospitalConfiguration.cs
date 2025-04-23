using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamyCare.Data.EntitiesConfiguration
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Title).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Description).IsRequired().HasMaxLength(500);
            
        }
    }
}
