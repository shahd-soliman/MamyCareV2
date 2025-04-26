using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MamyCare.Services
{
    public class MotherFeaturesService (ApplicationDbContext context , IOptions<ServerSettings> optioins): IMotherFeaturesService
    {
        private readonly ApplicationDbContext _context=context;
        private readonly string _baseUrl = optioins.Value.BaseUrl;
        public async Task<List<ArticleResponse>> ArticlesGetAll()
        {
            var Articles = await _context.Articles.ToListAsync();
            var response = Articles.Adapt<List<ArticleResponse>>();

            foreach (var activity in response)
            {
                activity.ImageUrl = $"{_baseUrl}{activity.ImageUrl}";
            }

            return response;
        }


        public async Task<ArticleResponse> ArticlesGetById(int Articleid)
        {
            var Articles = await _context.Articles.FirstOrDefaultAsync(x=>x.Id== Articleid);
            var response = Articles.Adapt<ArticleResponse>();


            response.ImageUrl = $"{_baseUrl}{response.ImageUrl}";

            return response;

        }
    }
}
