
using MamyCare.Data;
using MamyCare.Seetings;
using Microsoft.Extensions.DependencyInjection;

namespace MamyCare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);

            builder.Services.AddMapping();
            var app = builder.Build();
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            Console.WriteLine($"Email: {emailSettings?.Mail}, Host: {emailSettings?.Host}, Port: {emailSettings?.Port}");
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            


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
