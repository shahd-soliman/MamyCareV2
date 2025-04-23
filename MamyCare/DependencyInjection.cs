using MamyCare.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using MamyCare.Seetings;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MamyCare
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencies(this IServiceCollection services , IConfiguration configuration) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
         

            services.AddDbContext(configuration);
            services.AddAuthConfig(configuration);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<IReminderService, ReminderService>();


            services.AddHttpContextAccessor();
            services.AddScoped<IEmailSender, EmailSenderService>();
            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;

        }


        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<Jwtoptions>(configuration.GetSection("Jwtoptions"));

            services.AddOptions<Jwtoptions>()
                   .Bind(configuration.GetSection("JWT"))
                   .ValidateDataAnnotations()
                   .ValidateOnStart();
            var jwtSetting = configuration.GetSection(nameof(Jwtoptions.SectionName)).Get<Jwtoptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetSection(nameof(Jwtoptions.SectionName)).Get<Jwtoptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSetting!.Issuer,
                        ValidAudience = jwtSetting!.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting!.Key))
                    };
                });



            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();
            return services;
        }



        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mapcongig = new TypeAdapterConfig();
            mapcongig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mapcongig));

            return services;
        }

    }
}

