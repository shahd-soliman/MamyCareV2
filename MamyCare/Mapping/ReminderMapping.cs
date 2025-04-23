using MamyCare.Contracts.Hospitals;
using MamyCare.Contracts.Reminders;
using MamyCare.Data.Migrations;
using Mapster;

namespace MamyCare.Mapping
{
    public class ReminderMapping() : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            TypeAdapterConfig<ReminderRequest, Reminder>
          .NewConfig()
          .Map(dest => dest.Title, src => src.Title)
          .Map(dest => dest.Description, src => src.Description)
          .Map(dest => dest.Date, src => src.Date);

            TypeAdapterConfig<Reminder, ReminderResponse>
          .NewConfig()
          .Map(dest => dest.id, src => src.Id)
          .Map(dest => dest.Title, src => src.Title)
          .Map(dest => dest.Description, src => src.Description)
          .Map(dest => dest.IsActive, src => src.IsActive)
          .Map(dest => dest.Date, src => src.Date);
        }


    }
}
