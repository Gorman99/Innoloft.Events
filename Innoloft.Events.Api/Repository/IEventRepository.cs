using Innoloft.Events.Api.Data.Entities;
using Innoloft.Events.Api.Models;

namespace Innoloft.Events.Api.Repository;

public interface IEventRepository
{
    Task<Event?> GetEvent(int eventId);
    Task<Event> AddEvent(Event request);
    Task DeleteEvent (Event @event);
    Task UpdateEvent(Event @event);
    Task<PagedResult<Event>> GetEvents(BaseFilter filter, int userId);
}