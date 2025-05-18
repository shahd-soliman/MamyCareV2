using MamyCare.Contracts.User;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Azure;
using MamyCare.Entities;
using MamyCare.Contracts.Authentication;

namespace MamyCare.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _baseUrl;


        public UserService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment, IOptions<ServerSettings> options)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _baseUrl = options.Value.BaseUrl;

        }

        public async Task<Result<GetProfileResponse>> GetMotherProfile(int userid)
        {
            var user = await _userManager.Users
                .Include(u => u.Mother)
                .Where(u => u.Id == userid)
                .SingleOrDefaultAsync();
            var profile = user.Adapt<GetProfileResponse>();
            profile.ImageUrl = $"{_baseUrl}{profile.ImageUrl}";
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
                        var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, mother.ImageUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    var extension = Path.GetExtension(request.Image.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "MotherProfilePicture");

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
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "BabyProfilePicture", imageName);
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
            if (mother.Babies.Count > 1)
            {
                foreach (var b in mother.Babies)
                {
                    if(b.id == baby.id)
                        continue;
                    b.IsActive = false;
                }
            }

            await _context.SaveChangesAsync();
           

            return Result.Success();
        }

        public async Task<Result<chooseBabyResponse>> ChooseBaby(int UserId, int BabyId)
        {
            var mother = await _context.Mothers
                .Include(m => m.Babies)
                .FirstOrDefaultAsync(m => m.UserId == UserId);


            var baby = await _context.Babies.FirstOrDefaultAsync(b => b.id == BabyId);

            baby!.IsActive = true;
            if(mother!.Babies.Count>1)
            {
                foreach (var b in mother!.Babies)
                {
                    if (b.id == BabyId)

                        continue;

                    b.IsActive = false;
                }
            }
            

            await _context.SaveChangesAsync();
            var response = new chooseBabyResponse
            {
                BabyName = baby.BabyName,
                BirthDate = baby.BirthDate,
                BabyImageUrl = baby.ProfilePicUrl != null ? $"{_baseUrl}{baby.ProfilePicUrl}" : null
            };


            return Result.Success(response);
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
                        var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, baby.ProfilePicUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    var extension = Path.GetExtension(request.Image.FileName);
                    var imageName = $"{Guid.NewGuid()}{extension}";
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "BabyProfilePicture");

                    var path = Path.Combine(folderPath, imageName);
                    using var stream = System.IO.File.Create(path);
                    await request.Image.CopyToAsync(stream);

                    baby.ProfilePicUrl = $"BabyProfilePicture/{imageName}";
                }

                await _context.SaveChangesAsync();
            }

            return Result.Success();
        }

        public async Task<Result<GetBabyProfileResponse>> GetBabyProfile(int userid)
        {
            var mother = await _context.Mothers
                .Include(m => m.Babies)
                .FirstOrDefaultAsync(m => m.UserId == userid);

            var baby = await _context.Babies.Where(b=>b.IsActive==true).FirstOrDefaultAsync(x=>x.motherId== mother.Id);
            var profile = baby.Adapt<GetBabyProfileResponse>();
            if(baby!.ProfilePicUrl != null)
                profile!.imageurl = $"{_baseUrl}{profile.imageurl}";
            return Result.Success(profile);
        }

    }
}