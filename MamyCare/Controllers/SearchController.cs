using MamyCare.Contracts;
using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.Hospitals;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MamyCare.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SearchController(
        IMotherFeaturesService motherFeaturesService,
        IHospitalService hospitalService,
        IBabyFeaturesService babyFeaturesService
    ) : ControllerBase
    {
        private readonly IMotherFeaturesService _motherFeaturesService = motherFeaturesService;
        private readonly IHospitalService _hospitalService = hospitalService;
        private readonly IBabyFeaturesService _babyFeaturesService = babyFeaturesService;

        [HttpGet]
        public async Task<ActionResult<UnifiedSearchResponse>> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query cannot be empty.");

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString!);

            var hospitals = (await _hospitalService.SearchHospitals(q, userId))
                .Where(h => h.Title.StartsWith(q, StringComparison.OrdinalIgnoreCase)).ToList();
            var articles = (await _motherFeaturesService.SearchArticles(q))
                .Where(a => a.Title.StartsWith(q, StringComparison.OrdinalIgnoreCase)).ToList();
            var activities = (await _babyFeaturesService.ArabicActivitiesGetAll())
                .Where(a => a.title != null && a.title.StartsWith(q, StringComparison.OrdinalIgnoreCase)).ToList();

            var response = new UnifiedSearchResponse
            {
                Hospitals = hospitals,
                Articles = articles,
                Activities = activities
            };

            return Ok(response);
        }
    }
}
