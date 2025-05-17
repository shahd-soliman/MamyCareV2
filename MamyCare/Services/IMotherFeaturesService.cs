using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;

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


    }
}
