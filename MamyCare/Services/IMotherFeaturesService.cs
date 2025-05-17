using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using System.Threading.Tasks;

namespace MamyCare.Services
{
    public interface IMotherFeaturesService
    {
        Task<List<ArticleResponse>> ArabicArticlesGetAll();
        Task<List<ArticleResponse>> EnglishArticlesGetAll();
        Task<ArticleResponse> ArticlesGetById(int Articleid);
        Task<List<PodcastResponse>> ArabicPodcastGetAll();
        Task<List<PodcastResponse>> EnglishPodcastGetAll();
        Task<PodcastResponse> PodcastsGetById(int podcastid);

        Task<List<VideosResponse>> ArabicVideosGetAll();
        Task<List<VideosResponse>> EnglishVideossGetAll();
        Task<VideosResponse> VideoGetById(int VideoId);
        Task<Result<List<TipsandtricksResponse>>> TipsAndTricksGetAll();
        Task<Result<TipsandtricksResponse>> TipsAndTricksGetById(int id);




    }
}
