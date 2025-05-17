using MamyCare.Contracts.BabyFeature;
using MamyCare.Errors;
using Mapster;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace MamyCare.Services
{
    public class GallaryService(ApplicationDbContext context , IOptions<ServerSettings> options,IHttpContextAccessor httpContextAccessor) : IGallaryService
    {
        private readonly ApplicationDbContext _context=context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly string _baseUrl = options.Value.BaseUrl;


        public async Task<Result<List<GallaryResponse>>> GetAll()
        {
            var baby = GetCurrentActiveBaby();
            if (baby == null)
                return Result.Failure<List<GallaryResponse>>(BabyFeaturesErrorr.Nomatchedbaby);

            var gallaries = await _context.Gallaries
                .Where(x => x.BabyId == baby.id)
                .ToListAsync();
            if(gallaries.Count==0)
                return Result.Failure<List<GallaryResponse>>(BabyFeaturesErrorr.Gallary);

            var response = gallaries.Adapt<List<GallaryResponse>>();
            foreach(var item in response)
            {
                item.ImageUrl=$"{_baseUrl}{item.ImageUrl}";
            }

            return Result.Success(response);
        }


        public async Task<Result<GallaryResponse>> GetById(int id )
        {
            var image = await _context.Gallaries.FirstOrDefaultAsync(x => x.Id==id);
            if (image==null)
            return Result.Failure<GallaryResponse>(BabyFeaturesErrorr.EmptyGallary);
            var response = image.Adapt<GallaryResponse>();
            response.ImageUrl =$"{_baseUrl}{response.ImageUrl}";
            return Result.Success(response);
        }

        public async Task<Result> Create(GallaryRequest request , CancellationToken cancellationToken)
        {


            var baby = GetCurrentActiveBaby();

            var ImageUrl = string.Empty;
            if (request==null)
            {
                return Result.Failure(BabyFeaturesErrorr.EmptyGallary);
            }
            if (request.Image != null)
            {
                if (request.Image != null && request.Image.Length > 0)
                {
                    var folderPath = Path.Combine("wwwroot", "Gallary");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Image.CopyToAsync(stream);
                    }
                    ImageUrl = $"Gallary/{fileName}";

                }
            }

            var Gallary = new Gallary()
            {
                BabyId= baby!.id,
                ImageUrl = ImageUrl,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };
            _context.Gallaries.Add(Gallary);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Update(string Description, int id, CancellationToken cancellationToken)
        {
            var gallary = await _context.Gallaries.FindAsync(id);
            if (gallary == null)
                return Result.Failure(BabyFeaturesErrorr.EmptyGallary);


            gallary.Description =Description;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }


        public async Task<Result> Delete(int id, CancellationToken cancellationToken)
        {
            var image = await _context.Gallaries.FirstOrDefaultAsync(x => x.Id== id);
            if (image!=null)
            {
                _context.Gallaries.Remove(image!);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            return Result.Failure(BabyFeaturesErrorr.EmptyGallary);
        }

        private Baby? GetCurrentActiveBaby()
        {
            var userStringId = _httpContextAccessor.HttpContext!.User!.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userStringId)) return null;

            var userId = int.Parse(userStringId);
            var mother = _context.Mothers.FirstOrDefault(x => x.UserId == userId);
            if (mother == null) return null;

            var baby = _context.Babies.FirstOrDefault(x => x.motherId == mother.Id && x.IsActive==true);
            return baby!;
        }



    }
}
