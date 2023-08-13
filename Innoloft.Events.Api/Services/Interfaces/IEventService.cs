using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;

namespace Innoloft.Events.Api.Services.Interfaces;

public interface IEventService
{
    Task<ApiResponse<PagedResult<GetEventsResponse>>> GetEvents(BaseFilter filter, int userId);
    Task<ApiResponse<GetEventDetailResponse>> GetEvent(int eventId);
    Task<ApiResponse<GetEventsResponse>> AddEvent(AddEventRequest request);
    Task<ApiResponse<GetEventsResponse>> UpdateEvent(AddEventRequest request, int eventId);
    Task<ApiResponse<GetEventsResponse>> DeleteEvent(int eventId);
}