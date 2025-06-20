using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.Hospitals;
using MamyCare.Contracts.MotherFeatures;

namespace MamyCare.Contracts
{
    public class UnifiedSearchResponse
    {
        public List<GetHospitalsResponse> Hospitals { get; set; } = new();
        public List<ArticleResponse> Articles { get; set; } = new();
        public List<ActivityResponse> Activities { get; set; } = new();
        // Add more as needed (e.g., Podcasts, Videos, etc.)
    }
}
