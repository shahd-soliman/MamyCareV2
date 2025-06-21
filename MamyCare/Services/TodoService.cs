using MamyCare.Contracts.Reminders;
using MamyCare.Entities;
using MamyCare.Errors;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MamyCare.Services
{
    public class TodoService(ApplicationDbContext context) : ITodo
    {
        private readonly ApplicationDbContext _context = context;

      


        public async Task<Result<List<TodoResponse>>> GetAll(int BabyId)
        {
            var Todos = await _context.Todos.Where(x => x.BabyId == BabyId).ToListAsync();
            if (Todos.Count == 0)
                return Result.Failure<List<TodoResponse>>(ReminderErrors.NUllTodo);
            else
            {
                var response = Todos.Adapt<List<TodoResponse>>();
                return Result.Success<List<TodoResponse>>(response);

            }
        }

        public async Task<Result<TodoResponse>> GetById(int TodoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == TodoId);
            if (todo != null)
            {
                var response = todo.Adapt<TodoResponse>();

                return Result.Success(response);
            }
            return Result.Failure<TodoResponse>(ReminderErrors.InvalidTodo);
        }
        public async Task<Result> Add(TodoRequest request, int BabyId, int motherid)
        {

            var baby = await _context.Babies.FirstOrDefaultAsync(x => x.motherId == motherid && x.id == BabyId);

            if (baby == null)
            {
                return Result.Failure(ReminderErrors.InvalidTodo);
            }


            var todo = request.Adapt<Todo>();
            todo.Isdone=false;
            todo.BabyId = BabyId;
            if (todo != null)
            {
                await _context.Todos.AddAsync(todo);
                await _context.SaveChangesAsync();
                return Result.Success();
            }

            else return Result.Failure(ReminderErrors.InvalidTodo);
        }

        public async Task<Result> Update(TodoRequest request, int TodoId, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == TodoId);
            if (todo ==null)
            {
                return Result.Failure(ReminderErrors.NUllTodo);
            }
            if (request!=null)
            {
                todo.Description = request.Description;
                todo.Date =request.Date;
                await _context.SaveChangesAsync(cancellationToken);

            }

            return Result.Success();
        }

        public async Task<Result> Delete(int TodoId, CancellationToken cancellationToken)
        {

            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id==TodoId);

            if (todo!=null)
            {
                _context.Remove(todo);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.InvalidTodo);
            }
        }
        public async Task<Result> Activate(int TodoId, CancellationToken cancellationToken)
        {
            var reminder = await _context.Todos.FirstOrDefaultAsync(x => x.Id == TodoId);
            if (reminder != null)
            {
                reminder.Isdone = true;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.NUllTodo);
            }
        }


        public async Task<Result> Deactivate(int TodoId, CancellationToken cancellationToken)
        {
            var reminder = await _context.Todos.FirstOrDefaultAsync(x => x.Id == TodoId);
            if (reminder !=null)
            {
                reminder.Isdone = false;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.NUllTodo);
            }
        }
    }
}
