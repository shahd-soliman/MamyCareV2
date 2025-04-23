using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamyCare.Data.EntitiesConfiguration
{
    public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Date).IsRequired();




        }

    }
}
