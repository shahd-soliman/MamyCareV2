using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamyCare.Data.EntitiesConfiguration
{
    public class FavouriteHospitalConfiguration : IEntityTypeConfiguration<FavouriteHospital>
    {
        public void Configure(EntityTypeBuilder<FavouriteHospital> builder)
        {
            builder.HasKey(fh => new { fh.motherId, fh.hospitalId });
            builder.HasOne(fh => fh.Mother)
                   .WithMany(m => m.FavouriteHospitals)
                   .HasForeignKey(fh => fh.motherId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(fh => fh.Hospital)
                   .WithMany(h => h.FavouriteHospitals)
                   .HasForeignKey(fh => fh.hospitalId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
