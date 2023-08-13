using System.Net;
using Bogus;
using Innoloft.Events.Api.Data.Cache;
using Innoloft.Events.Api.Data.Entities;
using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Repository;
using Innoloft.Events.Api.Services.Interfaces;
using Innoloft.Events.Api.Services.Providers;
using Innoloft.Events.Api.Tests.MockData;
using Newtonsoft.Json;
using NSubstitute;

namespace Innoloft.Events.Api.Tests.Service;

public class InvitationServiceTests
{
    
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventParticipantRepository _eventParticipantRepository;
    private readonly IRedisCacheManager _redisCacheManager;
    private readonly InvitationService _invitationService;

    public InvitationServiceTests()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _eventParticipantRepository = Substitute.For<IEventParticipantRepository>();
        _redisCacheManager = Substitute.For<IRedisCacheManager>();
      
        _invitationService = new InvitationService(_redisCacheManager, _eventRepository, _userRepository, _eventParticipantRepository);
    }

    [Fact]
    public async Task GetInvitations_EventExists_ReturnsSuccessApiResponse()
    {
        // Arrange
        var eventId = 1;
        
        var dBEvent = new Event
        {
            // Set event properties as needed
        };
        _eventRepository.GetEvent(eventId).Returns(dBEvent);

        // Mock RedisCacheManager to return keys and data
        var keyPattern = $"Event:Invitation:{eventId}:*";
        var keys = new List<string> { "key1", "key2" };
        _redisCacheManager.ScanKeysAsync(keyPattern).Returns(keys);

        var invitationData = JsonConvert.SerializeObject(new Invitaion
        {
            
        });
        _redisCacheManager.GetAsync(Arg.Any<string>()).Returns(invitationData);

        // Act
        var response = await _invitationService.GetInvitations(eventId);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
    }

    [Fact]
    public async Task SendInvitation_ValidData_ReturnsSuccessApiResponse()
    {
        // Arrange
        var eventId = 1;
        var dBEvent = FakeData.GetEventGenerator().Generate();
        dBEvent.Id = 1;
            
        _eventRepository.GetEvent(eventId).Returns(dBEvent);

       
        var request = new InvitationRequest
        {
            EventId = eventId,
            Users = new List<int> { 1 } 
        };

        var mockUser = FakeData.GetUserGenerator().Generate();
        mockUser.Company = new Company
        {
             Name = "Test",
             CatchPhrase = "TS",
             BS = "tt"
        };
        mockUser.Id = 1;
        
        _userRepository.GetUser(Arg.Any<int>()).Returns(mockUser);

        // Act
        var response = await _invitationService.SendInvitation(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
        // Additional assertions based on your implementation
    }

   
}