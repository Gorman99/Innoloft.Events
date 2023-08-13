using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Services.Interfaces;
using Innoloft.Events.Api.Services.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Innoloft.Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{
    private readonly IInvitationService _invitationService;
    
    public InvitationsController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    
    /// <summary>
    ///  get all invitation to an event 
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetInvitations(int eventId)
    {
        var result = await _invitationService.GetInvitations(eventId);
        return StatusCode(result.StatusCode, result);
    }
    
    
    /// <summary>
    ///  request  to participant in an event
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost("participate/{userId}")]
    public async Task<IActionResult> ParticipateInvitation([FromBody]ParticipateRequest request, int userId)
    {
        var result = await _invitationService.RequestInvitation(request, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    /// <summary>
    /// send invitation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost("send")]
    public async Task<IActionResult> SendInvitation(InvitationRequest request)
    {
        var result = await _invitationService.SendInvitation(request);
        return StatusCode(result.StatusCode, result);
    }
    
    /// <summary>
    ///  Accept invitation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("accept")]
    public async Task<IActionResult> AcceptInvitation(AcceptInvitationRequest request)
    {
        var result = await _invitationService.AcceptInvitation(request.UserId,request.EventId);
        return StatusCode(result.StatusCode, result);
    }
    
    
    
}