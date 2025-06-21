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

     
      

        [HttpGet("ArabicVideos")]
        public async Task<ActionResult<List<VideosResponse>>> ArabicVideos()
        {
            var Videos = await _motherFeaturesService.ArabicVideosGetAll();
            if (Videos == null || Videos.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Videos);
        }

        [HttpGet("EnglishVideos")]
        public async Task<ActionResult<List<VideosResponse>>> EnglishVideos()
        {
            var Videos = await _motherFeaturesService.EnglishVideossGetAll();
            if (Videos == null || Videos.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Videos);
        }

        [HttpGet("TipsAndtricks")]
        public async Task<ActionResult<List<TipsandtricksResponse>>> TipsAndtricksGetAll()
        {
            var tricks = await _motherFeaturesService.TipsAndTricksGetAll();
            if (tricks == null )
            {
                return BadRequest();
            }
            return Ok(tricks.Value);
        }
           [HttpGet("Videos/{VideoId}")]
        public async Task<ActionResult<VideosResponse>> VideoGetById(int VideoId)
        {
            var Video = await _motherFeaturesService.VideoGetById(VideoId);
            if (Video == null)
            {
                return BadRequest();
            }
            return Ok(Video);
        }

        [HttpGet("TipsAndTricks/{TrickId}")]
        public async Task<ActionResult<TipsandtricksResponse>> TrickeGetById(int TrickId)
        {
            var trick = await _motherFeaturesService.TipsAndTricksGetById(TrickId);
            if (trick == null)
            {
                return BadRequest();
            }
            return Ok(trick.Value);
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

        [HttpGet("ArabicVideos/Top4")]
        public async Task<ActionResult<List<VideosResponse>>> ArabicVideosTop10()
        {
            var Videos = await _motherFeaturesService.ArabicVideosGetTop(4);
            if (Videos == null || Videos?.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Videos);
        }

        [HttpGet("ArabicArticles/Top4")]
        public async Task<ActionResult<List<ArticleResponse>>> ArabicArticlesTop10()
        {
            var articles = await _motherFeaturesService.ArabicArticlesGetAll();
            if (articles == null || articles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(articles.Take(4));
        }

        [HttpGet("EnglishArticles/Top4")]
        public async Task<ActionResult<List<ArticleResponse>>> EnglishArticlesTop10()
        {
            var articles = await _motherFeaturesService.EnglishArticlesGetAll();
            if (articles == null || articles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(articles.Take(4));
        }

        [HttpGet("ArabicPodcasts/Top4")]
        public async Task<ActionResult<List<PodcastResponse>>> ArabicPodcastsTop10()
        {
            var podcasts = await _motherFeaturesService.ArabicPodcastGetAll();
            if (podcasts == null || podcasts.Count == 0)
            {
                return BadRequest();
            }
            return Ok(podcasts.Take(4));
        }

        [HttpGet("EnglishPodcasts/Top4")]
        public async Task<ActionResult<List<PodcastResponse>>> EnglishPodcastsTop10()
        {
            var podcasts = await _motherFeaturesService.EnglishPodcastGetAll();
            if (podcasts == null || podcasts.Count == 0)
            {
                return BadRequest();
            }
            return Ok(podcasts.Take(4));
        }

        [HttpGet("EnglishVideos/Top4")]
        public async Task<ActionResult<List<VideosResponse>>> EnglishVideosTop10()
        {
            var videos = await _motherFeaturesService.EnglishVideossGetAll();
            if (videos == null || videos.Count == 0)
            {
                return BadRequest();
            }
            return Ok(videos.Take(4));
        }

        [HttpGet("TipsAndtricks/Top4")]
        public async Task<ActionResult<List<TipsandtricksResponse>>> TipsAndtricksTop10()
        {
            var tricksResult = await _motherFeaturesService.TipsAndTricksGetAll();
            if (!tricksResult.IsSuccess || tricksResult.Value == null || tricksResult.Value.Count == 0)
            {
                return BadRequest();
            }
            return Ok(tricksResult.Value.Take(4));
        }

        [HttpGet("Articles/Search")]
        public async Task<ActionResult<List<ArticleResponse>>> SearchArticles([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Search query cannot be empty.");
            }
            var articles = await _motherFeaturesService.SearchArticles(q);
            if (articles == null || articles?.Count == 0)
            {
                return NotFound();
            }
            return Ok(articles);
        }
    }
}


