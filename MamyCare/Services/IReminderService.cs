using MamyCare.Contracts.Reminders;
using MamyCare.Entities;

namespace MamyCare.Services
{
    public interface IReminderService
    {
       Task<Result> AddReminder(ReminderRequest reminder , int BabyId , int motherid);
        Task<Result<ReminderResponse>> GetById( int reminderid);
         Task<Result<List<ReminderResponse>>> GetAll(int BabyId);

        Task<Result> Delete(int reminderid , CancellationToken cancellationToken);
        Task<Result> Update(ReminderRequest request, int reminderid, CancellationToken cancellationToken);
          Task<Result> Dedctivate(int reminderid, CancellationToken cancellationToken);
        Task<Result> Activate(int reminderid, CancellationToken cancellationToken);

        
    }
}
