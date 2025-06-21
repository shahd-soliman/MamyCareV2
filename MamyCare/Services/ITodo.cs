using MamyCare.Contracts.Reminders;

namespace MamyCare.Services
{
    public interface ITodo
    {
        Task<Result> Add(TodoRequest reminder, int BabyId, int motherid);
        Task<Result<TodoResponse>> GetById(int TodoId);
        Task<Result<List<TodoResponse>>> GetAll(int BabyId);
        Task<Result> Delete(int TodoId, CancellationToken cancellationToken);
        Task<Result> Update(TodoRequest request, int TodoId, CancellationToken cancellationToken);
        Task<Result> Deactivate(int TodoId, CancellationToken cancellationToken);
        Task<Result> Activate(int TodoId, CancellationToken cancellationToken);
    }
}
