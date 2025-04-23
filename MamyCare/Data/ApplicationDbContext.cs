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
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
