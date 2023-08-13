using Innoloft.Events.Api.Data.Context;
using Innoloft.Events.Api.Data.Entities;

namespace Innoloft.Events.Api.Repository;

public class EventParticipantRepository : IEventParticipantRepository
{
    private EventsDbContext _dbContext;

    public EventParticipantRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddParticipant (int userId , int eventId)
    {
        await _dbContext.EventParticipants.AddAsync(new EventParticipant{EventId = eventId, UserId = userId});
        await _dbContext.SaveChangesAsync();
        
    }
    
}