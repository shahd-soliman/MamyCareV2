using Azure;
using MamyCare.Abstractions;
using MamyCare.Contracts.Reminders;
using MamyCare.Entities;
using MamyCare.Errors;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MamyCare.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReminderController(IReminderService service, ApplicationDbContext context) : ControllerBase
    {
        private readonly IReminderService _reminderService = service;
        private readonly ApplicationDbContext _context = context;
        [HttpPost("Add/{BabyId}")]
        public async Task<IActionResult> AddReminder(ReminderRequest request, int BabyId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = int.Parse(userIdString!);
            var mother = _context.Mothers.FirstOrDefault(x => x.UserId == id);
            await _reminderService.AddReminder(request, BabyId , mother!.Id);
            return Ok();
        }
        [HttpGet("GetById/{reminderId}")]
        public async Task<ActionResult<ReminderResponse>> GetById(int reminderId)
        {
            var reminder = await _reminderService.GetById(reminderId);

            if (!reminder.IsSuccess)
                return BadRequest(ReminderErrors.NUllReminders); // أو NotFound(reminder.Error)

            return Ok(reminder.Value);
        }

        [HttpGet("GetAll/{Babyid}")]
        public async Task<ActionResult<List<ReminderResponse>>> GetAll(int Babyid)
        {
            var reminders = await _reminderService.GetAll(Babyid);
            if (reminders == null)
                return BadRequest(ReminderErrors.InvalidReminder);
            else
                return Ok(reminders.Value);
        }

        [HttpDelete("Delete/{reminderId}")]
        public async Task<IActionResult> Delete(int reminderId ,CancellationToken cancellationToken)
        {
            var result = await _reminderService.Delete(reminderId , cancellationToken);
            return result.IsSuccess
              ? Ok()
              : result.ToProblem(400);
        }

        [HttpPut("EditReminder/{Reminderid}")]
        public async Task<IActionResult> Update(ReminderRequest request, int Reminderid , CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(userIdString!);
            var mother = _context.Mothers.FirstOrDefault(x => x.UserId == id);
            await _reminderService.Update(request, Reminderid , cancellationToken);
            return Ok();
        }


        [HttpPut("Deactivate/{reminderid}")]
        public async Task<IActionResult> Dedctivate(int reminderid, CancellationToken cancellationToken)
        {
            var result = await _reminderService.Dedctivate(reminderid, cancellationToken);

            if (result == null)
                return BadRequest();
            else
                return Ok();

        }
        [HttpPut("Activate/{reminderid}")]
        public async Task<IActionResult> Activate(int reminderid, CancellationToken cancellationToken)
        {
            var result = await _reminderService.Activate(reminderid, cancellationToken);

            if (result == null)
                return BadRequest();
            else
                return Ok();

        }

    }
}