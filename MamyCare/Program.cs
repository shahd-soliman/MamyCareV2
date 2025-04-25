
using MamyCare.Data;
using MamyCare.DataSeed;
using MamyCare.Seetings;
using Microsoft.Extensions.DependencyInjection;

namespace MamyCare
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);

            builder.Services.AddMapping();
            var app = builder.Build();
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            Console.WriteLine($"Email: {emailSettings?.Mail}, Host: {emailSettings?.Host}, Port: {emailSettings?.Port}");
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);




            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                await context.Database.MigrateAsync(); 
                await ContextDataSeed.SeedAsync(context); 
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }


            builder.Configuration.AddUserSecrets<Program>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseCors();
            app.UseAuthentication();


            app.UseAuthorization();

            app.MapControllers();

          //  app.UseExceptionHandler();

            app.Run();

        }
    }
}
