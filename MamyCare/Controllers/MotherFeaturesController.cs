using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using MamyCare.Errors;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamyCare.Controllers
{
    [Authorize]
    [Route("/")]
    [ApiController]
    public class MotherFeaturesController(IMotherFeaturesService motherFeaturesService) : ControllerBase
    {
        private readonly IMotherFeaturesService _motherFeaturesService = motherFeaturesService;

        //Activiteis
        [HttpGet("Articles")]
        public async Task<ActionResult<List<ArticleResponse>>> ArticlesGetAll()
        {
            var Articles = await _motherFeaturesService.ArticlesGetAll();
            if (Articles == null || Articles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Articles);
        }
        [HttpGet("Articles/{articleId}")]
        public async Task<ActionResult<ArticleResponse>> ArticlesGetById(int articleId)
        {
            var Article = await _motherFeaturesService.ArticlesGetById(articleId);
            if (Article == null)
            {
                return BadRequest();
            }
            return Ok(Article);
        }


    }
}


