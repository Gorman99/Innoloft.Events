using Innoloft.Events.Api.Data.Context;
using Innoloft.Events.Api.Data.Entities;
using Innoloft.Events.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Events.Api.Repository;

public class EventRepository : IEventRepository
{
    private readonly EventsDbContext _dbContext;

    public EventRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public async Task<Event?> GetEvent(int eventId)
    {
        return await _dbContext.Events.Include( u => u.User).ThenInclude( d => d.Company)
            .FirstOrDefaultAsync( e => e.Id.Equals(eventId));
    }
    
     
    public async Task<Event> AddEvent(Event request)
    {
       await _dbContext.Events.AddAsync( request);
       await _dbContext.SaveChangesAsync();
       return request;
    }

   

    public async Task DeleteEvent (Event @event)
    {
        _dbContext.Events.Remove(@event);
        await _dbContext.SaveChangesAsync();

    }
    
    public async Task UpdateEvent(Event @event)
    {
        _dbContext.Events.Remove(@event);
        await _dbContext.SaveChangesAsync();

    }


    public async Task<PagedResult<Event>> GetEvents(BaseFilter filter, int userId)
    {
        var events = await _dbContext.Events.Where(u => u.UserId.Equals(userId)).AsNoTracking()
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .OrderByDescending(c => c.CreatedAt)
            
            .ToListAsync();
        var totalEvent = await _dbContext.Events.CountAsync();

        var data = new PagedResult<Event>
        {
            PageSize = filter.PageSize,
            PageIndex = filter.Page,
            Results = events,
            TotalCount = totalEvent
        };

        return data;
    }
    
}