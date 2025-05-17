using MamyCare.abstraction;
using MamyCare.Contracts.BabyFeature;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MamyCare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class GallaryController(IGallaryService gallaryService) : ControllerBase
    {
        private readonly IGallaryService _gallaryService = gallaryService;

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] GallaryRequest request, CancellationToken cancellationToken)
        {
            var response = await _gallaryService.Create(request, cancellationToken);

            return response.IsSuccess ? NoContent() : BadRequest();
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGallaryRequestcs request, CancellationToken cancellationToken)
        {
            var response = await _gallaryService.Update(request.Description, request.id, cancellationToken);

            return response.IsSuccess ? NoContent() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _gallaryService.GetAll();
            return response.IsSuccess ? Ok(response.Value) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _gallaryService.GetById(id);
            return response.IsSuccess ? Ok(response.Value) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
        {
            var response = await _gallaryService.Delete(id, cancellationToken);
            return response.IsSuccess ? NoContent() : BadRequest();
        }




    }
}
