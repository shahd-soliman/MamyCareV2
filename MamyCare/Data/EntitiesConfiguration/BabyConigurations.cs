using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamyCare.Data.EntitiesConfiguration
{
    public class BabyConfigurations : IEntityTypeConfiguration<Baby>
    {
        public void Configure(EntityTypeBuilder<Baby> builder)
        {
            builder.HasKey(b => b.id);
            builder.Property(b => b.BabyName)
                   .IsRequired();

            builder.Property(b => b.BirthDate)
                   .IsRequired()
                   .HasColumnType("date");

         
        }
    }
}
