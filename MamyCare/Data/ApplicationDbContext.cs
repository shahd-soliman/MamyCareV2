using MamyCare.Entities.MamyCare.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ess;
using System.Reflection;

namespace MamyCare.Data
{
    public class ApplicationDbContext  : IdentityDbContext<ApplicationUser , IdentityRole<int> , int >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Mother> Mothers { get; set; }
        public DbSet<Baby> Babies { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<FavouriteHospital> FavouriteHospitals { get; set; }
        public DbSet<GovernorateHospital> GovernorateHospitals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<NutritionalValue> NutritionalValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Recipe>()
    .HasOne(r => r.NutritionalValues)
    .WithOne()
    .HasForeignKey<Recipe>(r => r.NutritionalValuesId)
    .OnDelete(DeleteBehavior.Cascade); // أو Restrict حسب المطلوب


            base.OnModelCreating(modelBuilder);
        }
    }
}
