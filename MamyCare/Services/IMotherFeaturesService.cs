using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;

namespace MamyCare.Services
{
    public interface IMotherFeaturesService
    {
        Task<List<ArticleResponse>> ArticlesGetAll();

        Task<ArticleResponse> ArticlesGetById(int Articleid);

    }
}
