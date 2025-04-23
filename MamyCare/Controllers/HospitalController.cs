using Azure;
using MamyCare.Abstractions;
using MamyCare.Contracts.Hospitals;
using MamyCare.Errors;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MamyCare.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
        public class HospitalController(IHospitalService hospitalService) : ControllerBase
    {
        private readonly IHospitalService _hospitalService = hospitalService;

        [HttpGet("GetAllGovernorates")]
        public async Task<ActionResult<List<GetGovernoratesResponse>>> GetAllGovernorates()
        {
            return await _hospitalService.GetAllGovernorates();
        }

        [HttpGet("GetAllHospitals")]
        public async Task<ActionResult<List<GetHospitalsResponse>>> GetAllHospitals()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = int.Parse(userIdString!);
            return await _hospitalService.GetAllHospitals(id);
        }

        [HttpGet("GetAllFilteredHospitals/{GovernorateId}")]
        public async Task<ActionResult<List<GetHospitalsResponse>>> GetAllFilteredHospitals(int GovernorateId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = int.Parse(userIdString!);
            var result = await _hospitalService.GetAllFilteredHospitals(GovernorateId , id);
            if (result == null || result.Count == 0)
            {
                return BadRequest("No hospitals found for the specified governorate.");
            }

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<List<GetHospitalsResponse>>> GetById(int hospitalid)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = int.Parse(userIdString!);
            var result = await _hospitalService.GetById(hospitalid , id);
            if (result == null)
            {
                return NotFound("No hospitals found for the specified ID.");
            }

            return Ok(result);
        }

        [HttpPost("AddToFav/{hospitalId}")]
        public async Task<IActionResult> AddToFav(int hospitalId , CancellationToken cancellationToken)
        {
            var userIdString =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = int.Parse(userIdString!);

            var result = await _hospitalService.AddToFav(hospitalId, userId , cancellationToken);


            return result.IsSuccess ?
                 Ok(): result.ToProblem(400);

        }


        [HttpGet("GetFavouriteHospitals")]
        public async Task<ActionResult<List<GetHospitalsResponse>>> GetFav()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = int.Parse(userIdString!);
            var result = await _hospitalService.GetFAv(userId);
            
           if(result == null)
            {
                return NoContent();
            }
            return result.Value;

        }

        [HttpDelete("DeleteFav/{hospitalId}")]
        public async Task<IActionResult> DeleteFav(int hospitalId, CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = int.Parse(userIdString!);

            var result = await _hospitalService.DeleteFav(hospitalId, userId, cancellationToken);
            
           return Ok();
        }

    }
}

