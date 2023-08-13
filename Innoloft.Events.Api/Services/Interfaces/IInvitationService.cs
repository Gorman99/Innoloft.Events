using Innoloft.Events.Api.Data.Cache;
using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;

namespace Innoloft.Events.Api.Services.Interfaces;

public interface IInvitationService
{
    Task<ApiResponse<List<Invitaion>>> GetInvitations(int eventId);
    Task<ApiResponse<object>> SendInvitation(InvitationRequest request);
    Task<ApiResponse<object>> RequestInvitation(ParticipateRequest request, int userId);
    Task<ApiResponse<Invitaion>> AcceptInvitation(int userId, int eventId);
}