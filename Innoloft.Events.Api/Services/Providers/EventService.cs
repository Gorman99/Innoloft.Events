using System.Net;
using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Repository;
using Innoloft.Events.Api.Services.Interfaces;
using Mapster;

namespace Innoloft.Events.Api.Services.Providers;

public class EventService : IEventService
{
    //private readonly EventsDbContext _dbContext;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;

    public EventService( IEventRepository eventRepository, IUserRepository userRepository)
    {
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<PagedResult<GetEventsResponse>>> GetEvents(BaseFilter filter, int userId)
    {
        var events = await _eventRepository.GetEvents(filter, userId);

        var data = events.Results.Select(s => s.Adapt<GetEventsResponse>()).ToList();

        var result = events.Adapt<PagedResult<GetEventsResponse>>();
        result.Results = data;
        return new ApiResponse<PagedResult<GetEventsResponse>>((int)HttpStatusCode.OK,true, "Success", result);
    }

    public async Task<ApiResponse<GetEventDetailResponse>> GetEvent(int id)
    {
        var eventDb = await _eventRepository.GetEvent(id);

        if (eventDb == null)
        {
            return new ApiResponse<GetEventDetailResponse>((int)HttpStatusCode.NotFound,false, "Event not found", null);
        }

        var result = eventDb.Adapt<GetEventDetailResponse>();
        result.ContactPerson = new ContactPerson
        {
            Name = eventDb.User.Name,
            ImageUrl = eventDb.User.ImageUrl,
            CompanyName = eventDb.User.Company.Name
        };
        return new ApiResponse<GetEventDetailResponse>( (int)HttpStatusCode.OK,true, "Success", result);
    }


    public async Task<ApiResponse<GetEventsResponse>> AddEvent(AddEventRequest request)
    {
        /*var day = (request.EndDate - request.StartDate).Days;
        if (day < 1)
        {
            return new ApiResponse<GetEventsResponse>((int)HttpStatusCode.BadRequest,false, "user not found", default);

        }*/
        var user = await _userRepository.GetUser(request.UserId);
        if (user == null)
        {
            return new ApiResponse<GetEventsResponse>((int)HttpStatusCode.NotFound,false, "you can not sudek", default);

        }
        var newEvent = request.Adapt<Data.Entities.Event>();

        var result = await _eventRepository.AddEvent(newEvent);
        return new ApiResponse<GetEventsResponse>((int)HttpStatusCode.OK,true, "Success", result.Adapt<GetEventsResponse>());

       
    }

    public async Task<ApiResponse<GetEventsResponse>> UpdateEvent(AddEventRequest request, int eventId)
    {

        var user = await _userRepository.GetUser(request.UserId);
        if (user == null)
        {
            return new ApiResponse<GetEventsResponse>( (int)HttpStatusCode.NotFound,false, "User not found",default);

        }

        var dBEvent = await _eventRepository.GetEvent(eventId);
        if (dBEvent == null)
        {
            return new ApiResponse<GetEventsResponse>( (int)HttpStatusCode.NotFound,false, default);
        }

        dBEvent = request.Adapt(dBEvent);
        await _eventRepository.UpdateEvent(dBEvent);
        return new ApiResponse<GetEventsResponse>((int)HttpStatusCode.OK,true, dBEvent.Adapt<GetEventsResponse>());
    }

    public async Task<ApiResponse<GetEventsResponse>> DeleteEvent(int eventId)
    {
        var dBEvent = await _eventRepository.GetEvent(eventId);
        if (dBEvent == null)
        {
            return new ApiResponse<GetEventsResponse>((int)HttpStatusCode.NotFound ,false, default);
        }
        await _eventRepository.DeleteEvent(dBEvent);

        return new ApiResponse<GetEventsResponse>( (int)HttpStatusCode.OK,true, "Success", dBEvent.Adapt<GetEventsResponse>());
    }
}