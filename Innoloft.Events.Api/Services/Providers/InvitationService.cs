using System.Net;
using Innoloft.Events.Api.Data.Cache;
using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Repository;
using Innoloft.Events.Api.Services.Interfaces;
using Mapster;
using Newtonsoft.Json;

namespace Innoloft.Events.Api.Services.Providers;

public class InvitationService : IInvitationService
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventParticipantRepository _eventParticipant;
    private readonly IRedisCacheManager _redisCacheManager;

    public InvitationService(IRedisCacheManager redisCacheManager,  IEventRepository eventRepository, IUserRepository userRepository, IEventParticipantRepository eventParticipant)
    {
        _redisCacheManager = redisCacheManager;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
        _eventParticipant = eventParticipant;
    }

    public async Task<ApiResponse<List<Invitaion>>> GetInvitations(int eventId)
    {
        var dBEvent = await _eventRepository.GetEvent(eventId);

        if (dBEvent == null)
        {
            return new ApiResponse<List<Invitaion>>((int) HttpStatusCode.NotFound,false, "Event not find",default);
        }

        var listInvitations = new List<Invitaion>();
        var keyPattern = $"Event:Invitation:{eventId}:*";

        var keys = await _redisCacheManager.ScanKeysAsync(keyPattern);

        foreach (var key in keys)
        {
            var dataStr = await _redisCacheManager.GetAsync(key);
            var invitaionData = JsonConvert.DeserializeObject<Invitaion>(dataStr);
            listInvitations.Add(invitaionData);
        }
        return new ApiResponse<List<Invitaion>>((int) HttpStatusCode.OK,true, listInvitations);
    }

    public async Task<ApiResponse<object>> SendInvitation(InvitationRequest request)
    {
        var dBEvent = await _eventRepository.GetEvent(request.EventId);


        if (dBEvent == null)
        {
            return new ApiResponse<object>(false, "Event not found");
        }

       
        var eventDayInterval = (dBEvent.EndDate - dBEvent.StartDate.AddDays(1)).Days;
        foreach (var user in request.Users)
        {
            var dBUser = await _userRepository.GetUser(user);

            if (dBUser == null)
            {
                return new ApiResponse<object>((int) HttpStatusCode.NotFound,false, "user not found", default);
            }
            
            var key = $"Event:Invitation:{dBEvent.Id}:{dBUser.Id}";
            var invitation = new Invitaion
            {
                IsReceive = false,
                Invitee = new Invitee
                {
                    Name = dBUser.Name,
                    CompanyName = dBUser.Company.Name,
                    UserId = dBUser.Id
                },
                EventId = dBEvent.Id
            };
            await _redisCacheManager.SetAsync(key, JsonConvert.SerializeObject(invitation),
                TimeSpan.FromDays(eventDayInterval));
            
        }

        return new ApiResponse<object>((int) HttpStatusCode.OK,true, default);
    }

    public async Task<ApiResponse<object>> RequestInvitation(ParticipateRequest request, int userId)
    {
        var dBEvent = await _eventRepository.GetEvent(request.EventId);

        if (dBEvent == null)
        {
            return new ApiResponse<object>((int) HttpStatusCode.NotFound,false, "Event not found");
        }

        var eventDayInterval = (dBEvent.EndDate - dBEvent.StartDate.AddDays(1)).Days;

        var dBUser = await _userRepository.GetUser(userId);

        if (dBUser == null)
        {
            return new ApiResponse<object>((int) HttpStatusCode.NotFound,false, "user not found", default);
        }

        var key = $"Event:Invitation:{dBEvent.Id}:{dBUser.Id}";
        var invitation = new Invitaion
        {
            IsReceive = true,
            Invitee = new Invitee
            {
                Name = dBUser.Name,
                CompanyName = dBUser.Company.Name,
                UserId = dBUser.Id
            },
            Message = request.Message.Adapt<Message>(),
            EventId = dBEvent.Id
        };

        await _redisCacheManager.SetAsync(key, JsonConvert.SerializeObject(invitation),
            TimeSpan.FromDays(eventDayInterval));
        return new ApiResponse<object>((int) HttpStatusCode.OK,true, default);
    }

    public async Task<ApiResponse<Invitaion>> AcceptInvitation(int userId ,int eventId)
    {
        var dBEvent = await _eventRepository.GetEvent(eventId);

        if (dBEvent == null)
        {
            return new ApiResponse<Invitaion>((int) HttpStatusCode.NotFound,false, "Event not find",default);
        }

       
        var keyPattern = $"Event:Invitation:{eventId}:{userId}";

        var dataStr = await _redisCacheManager.GetAsync(keyPattern);
        var invitation = JsonConvert.DeserializeObject<Invitaion>(dataStr);
        
        if (invitation == null)
        {
            return new ApiResponse<Invitaion>((int) HttpStatusCode.NotFound,false, "Event not find",default);

        }

        await _eventParticipant.AddParticipant(invitation.Invitee.UserId, eventId);

        await _redisCacheManager.DeleteKey(keyPattern);
        return new ApiResponse<Invitaion>((int) HttpStatusCode.OK,true,invitation);

    }

   
}