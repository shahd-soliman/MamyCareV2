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

          
            // جلب إعدادات البريد الإلكتروني من appsettings.json
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            Console.WriteLine($"Email: {emailSettings?.Mail}, Host: {emailSettings?.Host}, Port: {emailSettings?.Port}");

            // إضافة الخدمات
            builder.Services.AddDependencies(builder.Configuration);
            builder.Services.AddMapping();

            var app = builder.Build();

            // تهيئة قاعدة البيانات والبذور
            using (var scope = app.Services.CreateScope())
            {
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
            }

            // تكوين الـ Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware لحذف الكوكي
            app.Use(async (context, next) =>
            {
                // حذف الكوكي قبل معالجة الطلب
                if (context.Response.Cookies != null)
                {
                    context.Response.Cookies.Delete(".AspNetCore.Identity.Application");
                    context.Response.Headers.Append("Set-Cookie", ".AspNetCore.Identity.Application=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; HttpOnly; SameSite=Strict");
                }

                await next.Invoke();
            });

            app.MapControllers();

            app.Run();
        }
    }
}