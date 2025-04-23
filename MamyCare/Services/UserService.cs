using MamyCare.Contracts.User;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace MamyCare.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        public async Task<Result<GetProfileResponse>> GetMotherProfile(int userid)
        {
            var user = await _userManager.Users
                .Include(u => u.Mother)
                .Where(u => u.Id == userid)
                .SingleOrDefaultAsync();

            var profile = user.Adapt<GetProfileResponse>();
            return Result.Success(profile);
        }

        public async Task<Result> UpdateProfile(int userid, UpdateProfileRequest request)
        {
            var mother = await _context.Mothers
                .FirstOrDefaultAsync(m => m.UserId == userid);

            if (mother != null)
            {
                mother.FullName = request.FullName;
                mother.Phone = request.phone;

                if (request.Image is not null)
                {
                    if (mother.ImageUrl is not null)
                    {
                        var oldPath = Path.Combine(_WebHostEnvironment.WebRootPath, mother.ImageUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    var extension = Path.GetExtension(request.Image.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    var folderPath = Path.Combine(_WebHostEnvironment.WebRootPath, "MotherProfilePicture");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var path = Path.Combine(folderPath, imageName);

                    using var stream = System.IO.File.Create(path);
                    await request.Image.CopyToAsync(stream);

                    mother.ImageUrl = Path.Combine("MotherProfilePicture", imageName).Replace("\\", "/");
                }

                await _context.SaveChangesAsync();
            }

            return Result.Success();
        }

        public async Task<Result> ChangePassword(int userid, ChangePasswordRequest request)
        {
            var userIdString = userid.ToString();
            var user = await _userManager.FindByIdAsync(userIdString);
            var result = await _userManager.ChangePasswordAsync(user!, request.OldPassword, request.NewPassword);
            await _signInManager.RefreshSignInAsync(user);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCode: 400));
        }

        public async Task<Result> AddBaby(int UserId, AddBabyRequest request, CancellationToken cancellationToken)
        {
            string imageurl = string.Empty;
            if (request.BabyImage is not null)
            {
                var extension = Path.GetExtension(request.BabyImage.FileName);
                var imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(_WebHostEnvironment.WebRootPath, "BabyProfilePicture", imageName);
                using var stream = System.IO.File.Create(path);
                await request.BabyImage.CopyToAsync(stream);
                imageurl = Path.Combine("BabyProfilePicture", imageName).Replace("\\", "/");
            }

            var mother = _context.Mothers.FirstOrDefault(m => m.UserId == UserId);
            var baby = new Baby
            {
                BabyName = request.BabyName,
                BirthDate = request.BirthDate,
                gender = request.Gender,
                ProfilePicUrl = imageurl,
                IsActive = true,
                motherId = mother!.Id
            };
            await _context.Babies.AddAsync(baby, cancellationToken);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<Baby>> ChooseBaby(int UserId, int BabyId)
        {
            var mother = await _context.Mothers
                .Include(m => m.Babies)
                .FirstOrDefaultAsync(m => m.UserId == UserId);

            var baby = await _context.Babies.FirstOrDefaultAsync(b => b.id == BabyId);

            baby!.IsActive = true;

            foreach (var b in mother!.Babies)
            {
                if (b.id == BabyId)
                    continue;

                b.IsActive = false;
            }

            await _context.SaveChangesAsync();

            return Result.Success(baby);
        }

        public async Task<Result> UpdateBaby(int UserId, UpdateBabyProfileRequest request, CancellationToken cancellationToken)
        {
            var mother = await _context.Mothers
                .FirstOrDefaultAsync(m => m.UserId == UserId);

            var baby = await _context.Babies.FirstOrDefaultAsync(b => b.motherId == mother!.Id && b.IsActive == true);
            if (baby != null)
            {
                baby.BabyName = request.BabyName;
                baby.BirthDate = request.BirthDate;

                if (request.Image is not null)
                {
                    if (baby.ProfilePicUrl is not null)
                    {
                        var oldPath = Path.Combine(_WebHostEnvironment.WebRootPath, baby.ProfilePicUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    var extension = Path.GetExtension(request.Image.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    var folderPath = Path.Combine(_WebHostEnvironment.WebRootPath, "BabyProfilePicture");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var path = Path.Combine(folderPath, imageName);
                    using var stream = System.IO.File.Create(path);
                    await request.Image.CopyToAsync(stream);

                    baby.ProfilePicUrl = Path.Combine("BabyProfilePicture", imageName).Replace("\\", "/");
                }

                await _context.SaveChangesAsync();
            }

            return Result.Success();
        }
    }
}
