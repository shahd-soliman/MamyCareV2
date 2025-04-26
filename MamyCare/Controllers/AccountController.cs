using MamyCare.Contracts.User;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MamyCare.Abstractions;
using System.Security.Claims;


namespace MamyCare.Controllers;

[Route("Account")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("GetMotherProfile")]
    public async Task<IActionResult>GetProfile()
    {
     var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var id = int.Parse(userIdString!);
        var result = await _userService.GetMotherProfile(id);

        return Ok(result.Value);
    }

    [HttpPut("UpdateMotherProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var id = int.Parse(userIdString!);
        var result = await _userService.UpdateProfile(id, request);
        return NoContent();
    }

    [HttpPut("Change-Password")]

    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var id = int.Parse(userIdString!);
        var result = await _userService.ChangePassword(id , request);
        return result.IsSuccess ?  NoContent() : result.ToProblem(400);
    }
    [HttpPost("AddBaby")]
    public async Task<IActionResult> AddBaby([FromForm] AddBabyRequest request, CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var id = int.Parse(userIdString!);
        var result = await _userService.AddBaby(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem(400);
    }

    [HttpGet("ChooseBaby")]
    public async Task<IActionResult> ChooseBaby([FromQuery] int BabyId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = int.Parse(userIdString!);
        var result = await _userService.ChooseBaby(id, BabyId);
        return result.IsSuccess
               ? Ok(result.Value)
               : result.ToProblem(400);
    }

    [HttpPut("updateBabyProfile")]
    public async Task<IActionResult> UpdateBaby([FromForm] UpdateBabyProfileRequest request, CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = int.Parse(userIdString!);
        var result = await _userService.UpdateBaby(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem(400);
    }

    [HttpGet("GetBabyProfile")]
    public async Task<ActionResult<GetBabyProfileResponse>> GetBabyProfile()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var id = int.Parse(userIdString!);
        var result = await _userService.GetBabyProfile(id);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    }

}



