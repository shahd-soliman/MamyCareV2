using MamyCare.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using MamyCare.Seetings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace MamyCare
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
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
            services.AddScoped<IBabyFeaturesService, BabyFeaturesService>();
            services.AddScoped<IMotherFeaturesService, MotherFeaturesService>();
            services.Configure<ServerSettings>(configuration.GetSection("ServerSettings"));


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
            // إحضار إعدادات JWT من appsettings.json
            services.Configure<Jwtoptions>(configuration.GetSection("Jwtoptions"));

            services.AddOptions<Jwtoptions>()
                   .Bind(configuration.GetSection("JWT"))
                   .ValidateDataAnnotations()
                   .ValidateOnStart();

            var jwtSetting = configuration.GetSection("JWT").Get<Jwtoptions>();

            // إعداد المصادقة للاعتماد فقط على JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
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

            // إعداد AddIdentity بدون أي ارتباط بالكوكي
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                // إعدادات كلمات المرور، المستخدمين، إلخ
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;

                // إعدادات المستخدمين
                options.User.RequireUniqueEmail = true;

                // تعطيل التأكيد المطلوب للحساب
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

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

