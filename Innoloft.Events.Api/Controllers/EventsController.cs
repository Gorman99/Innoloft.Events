using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Innoloft.Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    ///  Add an event
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddEvent(  AddEventRequest request)
    {
       
        var result = await _eventService.AddEvent(request);

        return StatusCode(result.StatusCode, result);
    }

    
    /// <summary>
    ///  Update event
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{eventId}")]
    public async Task<IActionResult> UpdateEvent(int eventId,AddEventRequest request)
    {
        var result = await _eventService.UpdateEvent(request, eventId);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Delete an  event
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        var result = await _eventService.DeleteEvent(eventId);
        return StatusCode(result.StatusCode, result);
    }
    
    /// <summary>
    ///  Get Event by Id
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet("detail/{eventId}")]
    public async Task<IActionResult> GetEvent(int eventId)
    {
        var result = await _eventService.GetEvent(eventId);
        return StatusCode(result.StatusCode, result);
    } 
    
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetEvents(int userId, [FromQuery]BaseFilter filter)
    {
        var result = await _eventService.GetEvents(filter,userId);
        return StatusCode(result.StatusCode, result);
    }
    
    
}