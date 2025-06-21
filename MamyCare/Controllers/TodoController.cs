using MamyCare.Abstractions;
using MamyCare.Contracts.Reminders;
using MamyCare.Entities;
using MamyCare.Errors;
using MamyCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MamyCare.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TodoController (ApplicationDbContext context , ITodo Todo) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ITodo _todoService = Todo;

        [HttpPost("Add/{BabyId}")]
        public async Task<IActionResult> Add( int BabyId , TodoRequest request)
        {


            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = int.Parse(userIdString!);
            var mother = _context.Mothers.FirstOrDefault(x => x.UserId == id);
            await _todoService.Add(request, BabyId, mother!.Id);
            return Ok();
        }


        [HttpGet("GetById/{TodoId}")]
        public async Task<ActionResult<TodoResponse>> GetById(int TodoId)
        {
            var todo = await _todoService.GetById(TodoId);

            if (!todo.IsSuccess)
                return BadRequest(ReminderErrors.NUllTodo); 

            return Ok(todo.Value);
        }

        [HttpGet("GetAll/{Babyid}")]
        public async Task<ActionResult<List<TodoResponse>>> GetAll(int Babyid)
        {


            var todos = await _todoService.GetAll(Babyid);
            if (todos == null)
                return BadRequest(ReminderErrors.InvalidTodo);
            else
                return Ok(todos.Value);
        }

        [HttpDelete("Delete/{TodoId}")]
        public async Task<IActionResult> Delete(int TodoId ,CancellationToken cancellationToken)
        {
            var result = await _todoService.Delete(TodoId, cancellationToken);
            return result.IsSuccess
              ? Ok()
              : result.ToProblem(400);

        }

        [HttpPut("Update/{Todoid}")]
        public async Task<IActionResult> Update(TodoRequest request, int Todoid, CancellationToken cancellationToken)
        {

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(userIdString!);
            var mother = _context.Mothers.FirstOrDefault(x => x.UserId == id);
            await _todoService.Update(request, Todoid, cancellationToken);
            return Ok();
        }


        [HttpPut("Deactivate/{Todoid}")]
        public async Task<IActionResult> Deactivate(int Todoid, CancellationToken cancellationToken)
        {


            var result = await _todoService.Deactivate(Todoid, cancellationToken);

            if (result == null)
                return BadRequest();
            else
                return Ok();

        }
        [HttpPut("Activate/{Todoid}")]
        public async Task<IActionResult> Activate(int Todoid, CancellationToken cancellationToken)
        {

            var result = await _todoService.Activate(Todoid, cancellationToken);

            if (result == null)
                return BadRequest();
            else
                return Ok();
        }

    }
}
