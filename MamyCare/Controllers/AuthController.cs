using Azure;
using MamyCare.abstraction;
using MamyCare.Contracts.Authentication;
using MamyCare.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MamyCare.Abstractions;


namespace MamyCare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService , IJwtProvider jwtProvider , UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly IAuthService _authService=authService;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RequestRegister request, CancellationToken cancellationToken)
        {
            var response = await _authService.Register(request, cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : response.ToProblem(400);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin request, CancellationToken cancellationToken)
        {
            var response = await _authService.Login(request.Email,  request.Password , cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : response.ToProblem(400);

        }

        //[HttpPost("Test")]
        //public async Task<IActionResult> Test(string email , string passowrd)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    var (tokenn , minutes) = _jwtProvider.GenerateJwtToken(user);
        //    return Ok(tokenn );
        //}
       // [HttpPost("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(EmailConfirmRequest request)
        //{
        //    var result = await _authService.ConfirmEmail(request);
        //    return result.IsSuccess ? Ok() : result.ToProblem(401);
        //}
        //[HttpPost("resend-confirm-email")]
        //public async Task<IActionResult> ResendConfirmEmail(ResendEmailConfirmRequest request)
        //{
        //    var result = await _authService.ResendConfirmEmail(request);
        //    return result.IsSuccess ? Ok() : result.ToProblem(401);
        //}
        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgetPasswordEmail(ForgetPasswordRequest request)
        {
            var result = await _authService.SendForgetPasswordEmail(request);
            return result.IsSuccess ? Ok() : result.ToProblem(401);
        }
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);
            return result.IsSuccess ? Ok() : result.ToProblem(401);
        }
    }
}
