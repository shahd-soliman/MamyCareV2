using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using MamyCare.Entities;
using MamyCare.Errors;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MamyCare.Services
{
    public class MotherFeaturesService(ApplicationDbContext context, IOptions<ServerSettings> optioins) : IMotherFeaturesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly string _baseUrl = optioins.Value.BaseUrl;
        public async Task<List<ArticleResponse>> ArabicArticlesGetAll()
        {
            var Articles = await _context.Articles.Where(x => x.IsArabic==true).ToListAsync();
            var response = Articles.Adapt<List<ArticleResponse>>();

            foreach (var activity in response)
            {
                activity.ImageUrl = $"{_baseUrl}{activity.ImageUrl}";
            }

            return response;
        }


        public async Task<List<ArticleResponse>> EnglishArticlesGetAll()
        {
            var Articles = await _context.Articles.Where(x => x.IsArabic==false).ToListAsync();
            var response = Articles.Adapt<List<ArticleResponse>>();

            foreach (var activity in response)
            {
                activity.ImageUrl = $"{_baseUrl}{activity.ImageUrl}";
            }

            return response;
        }

        public async Task<ArticleResponse> ArticlesGetById(int Articleid)
        {
            var Articles = await _context.Articles.FirstOrDefaultAsync(x => x.Id== Articleid);
            var response = Articles.Adapt<ArticleResponse>();


            response.ImageUrl = $"{_baseUrl}{response.ImageUrl}";

            return response;

        }

        public async Task<List<PodcastResponse>> ArabicPodcastGetAll()
        {
            var Podcasts = await _context.Podcasts.Where(x => x.IsArabic==true).ToListAsync();
            var response = Podcasts.Adapt<List<PodcastResponse>>();
            return response;

        }
        public async Task<List<PodcastResponse>> EnglishPodcastGetAll()
        {
            var Podcasts = await _context.Podcasts.Where(x => x.IsArabic==false).ToListAsync();
            var response = Podcasts.Adapt<List<PodcastResponse>>();
            return response;

        }

        public async Task<PodcastResponse> PodcastsGetById(int podcastid)
        {
            var Podcasts = await _context.Podcasts.FirstOrDefaultAsync(x => x.Id== podcastid);
            var response = Podcasts.Adapt<PodcastResponse>();
            return response;
        }

        public async Task<List<VideosResponse>> ArabicVideosGetAll()
        {
            var Videos = await _context.Videos.Where(x => x.IsArabic==true).ToListAsync();
            var response = Videos.Adapt<List<VideosResponse>>();
            return response;
        }

        public async Task<List<VideosResponse>> EnglishVideossGetAll()
        {
            var Videos = await _context.Videos.Where(x => x.IsArabic==false).ToListAsync();
            var response = Videos.Adapt<List<VideosResponse>>();
            return response;
        }

        public async Task<VideosResponse> VideoGetById(int VideoId)
        {
            var Videos = await _context.Videos.FirstOrDefaultAsync(x => x.Id== VideoId);
            var response = Videos.Adapt<VideosResponse>();
            return response;
        }

        public async Task<Result<List<TipsandtricksResponse>>> TipsAndTricksGetAll()
        {
            var tricks = await _context.TipsAndTricks.Include(x => x.Tips!).ThenInclude(t => t.Points)  .ToListAsync();
            if (tricks.Count==0)
                return Result.Failure<List<TipsandtricksResponse>>(BabyFeaturesErrorr.EmptyTipsandTricks);
            var response = tricks.Adapt<List<TipsandtricksResponse>>();
            foreach(var item in response)
            {
                item.ImagePath=$"{_baseUrl}{item.ImagePath}";
            }
            return Result.Success(response);

        }

        public async Task<Result<TipsandtricksResponse>> TipsAndTricksGetById(int id)
        {
            var trick = await _context.TipsAndTricks.Include(x => x.Tips!).ThenInclude(t => t.Points).FirstOrDefaultAsync(x=>x.Id==id);
            var response = trick.Adapt<TipsandtricksResponse>();
            response.ImagePath = $"{_baseUrl}{response.ImagePath}";
            return Result.Success(response);
        }
    }
}
