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
    [Route("/MotherFeatures")]
    [ApiController]
    public class MotherFeaturesController(IMotherFeaturesService motherFeaturesService) : ControllerBase
    {
        private readonly IMotherFeaturesService _motherFeaturesService = motherFeaturesService;

        //Activiteis
        [HttpGet("ArabicArticles")]
        public async Task<ActionResult<List<ArticleResponse>>> ArabicArticlesGetAll()
        {
            var Articles = await _motherFeaturesService.ArabicArticlesGetAll();
            if (Articles == null || Articles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Articles);
        }
        [HttpGet("EnglishArticles")]
        public async Task<ActionResult<List<ArticleResponse>>> EnglishArticlesGetAll()
        {
            var Articles = await _motherFeaturesService.EnglishArticlesGetAll();
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
        [HttpGet("Podcasts/{podcastid}")]
        public async Task<ActionResult<PodcastResponse>> PodcastGetById(int podcastid)
        {
            var podcast = await _motherFeaturesService.PodcastsGetById(podcastid);
            if (podcast == null)
            {
                return BadRequest();
            }
            return Ok(podcast);
        }

        [HttpGet("ArabicPodcasts")]
        public async Task<ActionResult<List<PodcastResponse>>> ArabicPodcasts()
        {
            var podcasts = await _motherFeaturesService.ArabicPodcastGetAll();
            if (podcasts == null || podcasts.Count == 0)
            {
                return BadRequest();
            }
            return Ok(podcasts);
        }
        [HttpGet("EnglishPodcasts")]
        public async Task<ActionResult<List<PodcastResponse>>> EnglishPodcasts()
        {
            var podcasts = await _motherFeaturesService.EnglishPodcastGetAll();
            if (podcasts == null || podcasts.Count == 0)
            {
                return BadRequest();
            }
            return Ok(podcasts);




        }
    }
}


