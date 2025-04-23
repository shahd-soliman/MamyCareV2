using Azure;
using MamyCare.abstraction;
using MamyCare.Contracts.Authentication;
using MamyCare.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;



namespace MamyCare.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        ApplicationDbContext context, IJwtProvider jwtProvider,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthService> logger,
       IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment webHostEnvironment
        ) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ILogger _logger = logger;
        private readonly IEmailSender _emailService = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IWebHostEnvironment _WebHostEnvironment= webHostEnvironment;

       


        public async Task<Result<AuthResponse>> Register(RequestRegister request, CancellationToken cancellationToken)
        {
            string BabyImageUrl= string.Empty;
            string MotherImageUrl= string.Empty;
            var UserExist = await _userManager.FindByEmailAsync(request.Email);
            if (UserExist != null)
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
                   
                    var extention = Path.GetExtension(request.MotherImage.FileName);
                    var ImageName = $"{Guid.NewGuid()}{extention}";
                   
                        var directoryPath = Path.Combine(_WebHostEnvironment.WebRootPath, "MotherProfilePicture");
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        var path = Path.Combine(directoryPath, ImageName);
                        using var stream = System.IO.File.Create(path);
                        await request.MotherImage.CopyToAsync(stream);
                        MotherImageUrl = Path.Combine("MotherProfilePicture", ImageName).Replace("\\", "/");

                        var fileExists = System.IO.File.Exists(path);
                        Console.WriteLine($"File created successfully: {fileExists}");
                        Console.WriteLine($"WebRootPath: {_WebHostEnvironment.WebRootPath}");
                        Console.WriteLine($"ContentRootPath: {_WebHostEnvironment.ContentRootPath}");
                    
                
                }

                if (request.BabyImage is not null)
                {
                    var extention = Path.GetExtension(request.BabyImage.FileName);
                    var ImageName = $"{Guid.NewGuid()}{extention}";
                    try
                    {
                        var directoryPath = Path.Combine(_WebHostEnvironment.WebRootPath, "BabyProfilePicture");
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        var path = Path.Combine(directoryPath, ImageName);
                        using var stream = System.IO.File.Create(path);
                        await request.BabyImage.CopyToAsync(stream);
                        BabyImageUrl = Path.Combine("BabyProfilePicture", ImageName).Replace("\\", "/");

                        var fileExists = System.IO.File.Exists(path);
                        Console.WriteLine($"File created successfully: {fileExists}");
                        Console.WriteLine($"WebRootPath: {_WebHostEnvironment.WebRootPath}");
                        Console.WriteLine($"ContentRootPath: {_WebHostEnvironment.ContentRootPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving image: {ex.Message}");
                        // يمكنك تسجيل الخطأ أو إعادة توجيهه حسب الحاجة
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
                    ProfilePicUrl=BabyImageUrl
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
                var response = new AuthResponse(user.Id, mother!.FirstName!, user.Email, token, mother.Babies, mother.ImageUrl);
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

            if (await _userManager.FindByEmailAsync(Email) is not { } user)
            {
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
            }
            var result = await _signInManager.PasswordSignInAsync(Email, password, false, false);
            if (result.Succeeded)
            {
                var (token, minutes) = _jwtProvider.GenerateJwtToken(user);
                await _userManager.UpdateAsync(user);
                var mother = await _context.Mothers
                    .Include(x => x.Babies).Where(x =>x.Babies.Any(b=>b.IsActive==true))
                    .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
                var response = new AuthResponse(user.Id,mother!.FirstName!, user.Email, token,mother.Babies ,mother.ImageUrl );
                return Result<AuthResponse>.Success(response);
            }
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
        }

        //public async Task<Result> ConfirmEmail(EmailConfirmRequest request)
        //{
        //    if (await _userManager.FindByIdAsync(request.UserId.ToString()) is not { } user)
        //    {
        //        return Result.Failure(UserErrors.InvalidCode);
        //    }
        //    if (user.EmailConfirmed)
        //    {
        //        return Result.Failure(UserErrors.EmailAlreadyConfirmed);
        //    }

        //    var code = request.Code;

        //    try
        //    {
        //        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        //    }
        //    catch (FormatException)
        //    {
        //        return Result.Failure(UserErrors.InvalidCode);
        //    }

        //    var result = await _userManager.ConfirmEmailAsync(user, code);

        //    if (result.Succeeded)
        //        return Result.Success();

        //    var error = result.Errors.First();

        //    return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

        //}
        //public async Task<Result> ResendConfirmEmail(ResendEmailConfirmRequest request)
        //{
        //    if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
        //        return Result.Success();

        //    if (user.EmailConfirmed)
        //        return Result.Failure(UserErrors.EmailAlreadyConfirmed);

        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //    _logger.LogInformation("code:{code} ", code);
        //    await SendEmailConfirmationAsync(user, code);
        //    return Result.Success();

        //}

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
            var EmailBody = EmailBodyBuilder.GenerateBodyBuilder("EmailConfirmation",
                new Dictionary<string, string>
                {
                    {"user.FirstName" , user.Mother.FirstName!},
                    {"user.LastName" , user.Mother.LastName!},
                    {"{Code}" , code},
                    {"{safeLink}" ,$"{origin}/User/ConfirmEmail?UserId={user.Id}&Code={code}"}

                });
            await _emailService.SendEmailAsync(user.Email!, "Confirm your email", EmailBody);
        }
        private async Task SendEmailResetPassword(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var EmailBody = EmailBodyBuilder.GenerateBodyBuilder("ForgetPassword",
                new Dictionary<string, string>
                {
                    {"{{name}}" , user.Mother.FirstName!},

                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }

                });
            await _emailService.SendEmailAsync(user.Email!, "Reset Password", EmailBody);
        }

    }
}