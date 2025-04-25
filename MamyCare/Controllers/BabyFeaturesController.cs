using MamyCare.Contracts.BabyFeature;
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
    public class BabyFeaturesController(IBabyFeaturesService BabyFeaturesService  ) : ControllerBase
    {
        private readonly IBabyFeaturesService _babyFeaturesService = BabyFeaturesService;

        //Activiteis
        [HttpGet("Activites")]
        public async Task<ActionResult<List<ActivityResponse>>> GetAllActivities()
        {
            var activities = await _babyFeaturesService.ActivitiesGetAll();
            if (activities == null || activities.Count == 0)
            {
                return BadRequest();
            }
            return Ok(activities);
        }
        [HttpGet("Activities/{activityid}")]
         public async Task<ActionResult<ActivityResponse>> GetActivityById(int activityid)
        {
            var activity = await _babyFeaturesService.ActivityGetById(activityid);
            if (activity == null)
            {
                return BadRequest();
            }
            return Ok(activity);
        }


        //Recipes
        [HttpGet("Recipes")]
        public async Task<ActionResult<List<RecipeResponse>>> RecipeGetAll()
        {
            var Recipes = await _babyFeaturesService.RecipesGetAll();
            if (Recipes == null || Recipes.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Recipes);
        }
        [HttpGet("Recipes/{Reciprid}")]
        public async Task<ActionResult<RecipeResponse>> RecipeGetById(int Reciprid)
        {
            var recipe = await _babyFeaturesService.RecipeGetById(Reciprid);
            if (recipe == null)
            {
                return BadRequest();
            }
            return Ok(recipe);
        }
    }
}


