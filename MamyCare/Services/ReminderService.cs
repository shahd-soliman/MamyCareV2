using Azure;
using MamyCare.Contracts.Reminders;
using MamyCare.Errors;
using Mapster;

namespace MamyCare.Services
{
    public class ReminderService(ApplicationDbContext context) : IReminderService
    {
        private readonly ApplicationDbContext _context=context;
        public async Task<Result> AddReminder(ReminderRequest request ,int BabyId , int motherid)
        {

            var baby = await _context.Babies.FirstOrDefaultAsync(x => x.motherId == motherid && x.id == BabyId);

            if (baby == null)
            {
                return Result.Failure(ReminderErrors.InvalidReminder);
            }

            
            var reminder = request.Adapt<Reminder>();
            reminder.BabyId = BabyId;
            if (reminder != null)
            {
                await _context.Reminders.AddAsync(reminder);
                await _context.SaveChangesAsync();
                return Result.Success();
            }

            else return Result.Failure(ReminderErrors.InvalidReminder);
        }
        public async Task<Result<ReminderResponse>> GetById( int reminderid)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(x => x.Id == reminderid);
            if (reminder != null)
            {
                var response = reminder.Adapt<ReminderResponse>();

                return Result.Success(response);
            }
            return Result.Failure<ReminderResponse>(ReminderErrors.InvalidReminder);

        }
        public async Task<Result<List<ReminderResponse>>> GetAll(int BabyId)
        {
            var reminders = await _context.Reminders.Where(x => x.BabyId == BabyId).ToListAsync();
            if (reminders.Count == 0)
                return Result.Failure<List<ReminderResponse>>(ReminderErrors.NUllReminders);
            else
            {
                var response = reminders.Adapt<List<ReminderResponse>>();
                return Result.Success<List<ReminderResponse>>(response);

            }


        }

        public async Task<Result> Delete(int reminderid , CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(x=>x.Id==reminderid);
            if(reminder!=null)
            {
                _context.Remove(reminder);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.InvalidReminder);
            }
             
        }

        public async Task<Result> Update(ReminderRequest request, int reminderid, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(x => x.Id == reminderid);
            if(reminder ==null)
            {
                return Result.Failure(ReminderErrors.NUllReminders);
            }
            if(request!=null)
            {
                reminder.Title = request.Title;
                reminder.Description = request.Description;
                reminder.Date = request.Date;
                await _context.SaveChangesAsync(cancellationToken);

            }

            return Result.Success();
        }

        public async Task<Result> Dedctivate(int reminderid, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(x => x.Id == reminderid);
          if(reminder !=null)
            {
                reminder.IsActive = false;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.NUllReminders);
            }
        }
        public async Task<Result> Activate(int reminderid, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(x => x.Id == reminderid);
            if (reminder != null)
            {
                reminder.IsActive = true;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReminderErrors.NUllReminders);
            }
        }
        }
}
