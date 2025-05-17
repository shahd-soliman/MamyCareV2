using Azure;
using MamyCare.abstraction;
using MamyCare.Contracts.Authentication;
using MamyCare.Helpers;
using Mapster;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace MamyCare.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailSender _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _baseUrl;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IJwtProvider jwtProvider,
            ILogger<AuthService> logger,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IOptions<ServerSettings> options)
        {
            _userManager = userManager;
            _context = context;
            _jwtProvider = jwtProvider;
            _logger = logger;
            _emailService = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _baseUrl = options.Value.BaseUrl;
        }


        public async Task<Result<AuthResponse>> Register(RequestRegister request, CancellationToken cancellationToken)
        {
            string BabyImageUrl = string.Empty;
            string MotherImageUrl = string.Empty;
            var userExist = await _userManager.FindByEmailAsync(request.Email);
            if (userExist != null)
            {
                return Result.Failure<AuthResponse>(UserErrors.DuplicatedEmail);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    var error = result.Errors.First();
                    return Result.Failure<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
                }

                if (request.MotherImage is not null)
                {
                    var extension = Path.GetExtension(request.MotherImage.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "MotherProfilePicture");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    var path = Path.Combine(directoryPath, imageName);
                    using var stream = System.IO.File.Create(path);
                    await request.MotherImage.CopyToAsync(stream);
                    MotherImageUrl = Path.Combine("MotherProfilePicture", imageName).Replace("\\", "/");

                    var fileExists = System.IO.File.Exists(path);
                    
                }

                if (request.BabyImage is not null)
                {
                    var extension = Path.GetExtension(request.BabyImage.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    try
                    {
                        var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "BabyProfilePicture");
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        var path = Path.Combine(directoryPath, imageName);
                        using var stream = System.IO.File.Create(path);
                        await request.BabyImage.CopyToAsync(stream);
                        BabyImageUrl = Path.Combine("BabyProfilePicture", imageName).Replace("\\", "/");

                        var fileExists = System.IO.File.Exists(path);
                        Console.WriteLine($"File created successfully: {fileExists}");
                        Console.WriteLine($"WebRootPath: {_webHostEnvironment.WebRootPath}");
                        Console.WriteLine($"ContentRootPath: {_webHostEnvironment.ContentRootPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving image: {ex.Message}");
                    }
                }

                var mother = new Mother
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsMarried = request.IsMarried,
                    UserId = user.Id,
                    ImageUrl = MotherImageUrl,
                    Babies = new List<Baby>
                    {
                        new Baby
                        {
                            BirthDate = request.BirthDate,
                            BabyName = request.babyName,
                            gender = request.gender,
                            ProfilePicUrl = BabyImageUrl
                        }
                    }
                };

                await _context.Mothers.AddAsync(mother, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var (token, minutes) = _jwtProvider.GenerateJwtToken(user);
                await _userManager.UpdateAsync(user);
                mother = await _context.Mothers
                    .Include(x => x.Babies).Where(x => x.Babies.Any(b => b.IsActive == true))
                    .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
                
                var Babiesresponse =mother!.Babies.Adapt<List<BabyResponse>>();
                foreach (var item in Babiesresponse)
                {
                    item.imageurl= $"{_baseUrl}{item.imageurl}";
                }

                var response = new AuthResponse(user.Id, mother!.FirstName!, user.Email, token, Babiesresponse, $"{_baseUrl}{mother.ImageUrl}");
              
                return Result<AuthResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result.Failure<AuthResponse>(new Error("Exception", ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        public async Task<Result<AuthResponse>> Login(string Email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
            }

            var (token, minutes) = _jwtProvider.GenerateJwtToken(user);
            await _userManager.UpdateAsync(user);

            var mother = await _context.Mothers
                .Include(x => x.Babies).Where(x => x.Babies.Any(b => b.IsActive == true))
                .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);

            var Babiesresponse = mother!.Babies.Adapt<List<BabyResponse>>();
            foreach (var item in Babiesresponse)
            {
                item.imageurl= $"{_baseUrl}{item.imageurl}";
            }


            var response = new AuthResponse(user.Id, mother!.FirstName!, user.Email, token, Babiesresponse, $"{_baseUrl}{mother.ImageUrl}");
            return Result<AuthResponse>.Success(response);
        }

        public async Task<Result> SendForgetPasswordEmail(ForgetPasswordRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("code:{code} ", code);
            await SendEmailResetPassword(user, code);
            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }

        private async Task SendEmailConfirmationAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var emailBody = EmailBodyBuilder.GenerateBodyBuilder("EmailConfirmation",
                new Dictionary<string, string>
                {
                    {"user.FirstName", user.Mother.FirstName!},
                    {"user.LastName", user.Mother.LastName!},
                    {"{Code}", code},
                    {"{safeLink}", $"{origin}/User/ConfirmEmail?UserId={user.Id}&Code={code}"}
                });
            await _emailService.SendEmailAsync(user.Email!, "Confirm your email", emailBody);
        }

        private async Task SendEmailResetPassword(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var emailBody = EmailBodyBuilder.GenerateBodyBuilder("ForgetPassword",
                new Dictionary<string, string>
                {
                    {"{{name}}", user.Mother.FirstName!},
                    {"{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}"}
                });
            await _emailService.SendEmailAsync(user.Email!, "Reset Password", emailBody);
        }
    }
}