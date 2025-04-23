using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamyCare.Data.EntitiesConfiguration
{
    public class MotherConfiguration : IEntityTypeConfiguration<Mother>
    {
        public void Configure(EntityTypeBuilder<Mother> builder)
        {

            builder.HasKey(m => m.Id);  
            builder.Property(m => m.IsMarried).IsRequired();

            builder.HasMany(m => m.Babies)
                   .WithOne(b => b.Mother)
                   .HasForeignKey(b => b.motherId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.User)
                   .WithOne(u => u.Mother)
                   .HasForeignKey<Mother>(m => m.UserId)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
