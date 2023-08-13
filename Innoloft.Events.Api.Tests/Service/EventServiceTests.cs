

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Innoloft.Events.Api.Data.Entities;
using Innoloft.Events.Api.DTO;
using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Repository;
using Innoloft.Events.Api.Services.Providers;
using Xunit;
using NSubstitute;
using Shouldly; // Shouldly library for fluent assertions
using Mapster;   // Mapster library for object mapping
namespace Innoloft.Events.Api.Tests.Service;
public class EventServiceTests
{
    
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly EventService _eventService;

    public EventServiceTests()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _eventService = new EventService(_eventRepository, _userRepository);
    }

    [Fact]
    public async Task GetEvents_ReturnsApiResponseWithEvents()
    {
        // Arrange
        var filter = new BaseFilter();
        var userId = 123;
        var eventsFromRepo = new PagedResult<Event>(); // Your event data
        _eventRepository.GetEvents(Arg.Any<BaseFilter>(), Arg.Any<int>())
            .Returns(eventsFromRepo);

        // Act
        var result = await _eventService.GetEvents(filter, userId);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        result.IsSuccessFul.ShouldBeTrue();
        result.Message.ShouldBe("Success");
        result.Data.ShouldNotBeNull();
        result.Data.Results.ShouldBeEmpty();
    }

   
    [Fact]
    public async Task AddEvent_ValidUser_ReturnsSuccessApiResponse()
    {
        // Arrange
        var request = new AddEventRequest
        {
            UserId = 1,
        };

        var userFromRepo = new User
        {
            Id = 1
        };
        _userRepository.GetUser(request.UserId).Returns(userFromRepo);

        var newEvent = new Data.Entities.Event
        {
            
        };
        _eventRepository.AddEvent(Arg.Any<Data.Entities.Event>()).Returns(newEvent);

        // Act
        var response = await _eventService.AddEvent(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
    }
    
    [Fact]
    public async Task AddEvent_InvalidUser_ReturnsNotFoundApiResponse()
    {
        // Arrange
        var request = new AddEventRequest
        {
            UserId = 1
        };
        _userRepository.GetUser(request.UserId).Returns((User)null);

        // Act
        var response = await _eventService.AddEvent(request);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
    }
    
    
    [Fact]
    public async Task UpdateEvent_ValidUserAndEvent_ReturnsSuccessApiResponse()
    {
        // Arrange
        var eventId = 1;
        var request = new AddEventRequest
        {
            UserId = 1,
            // Set other properties as needed
        };

        var userFromRepo = new User
        {
            // Set user properties as needed
        };
        _userRepository.GetUser(request.UserId).Returns(userFromRepo);

        var eventFromRepo = new Data.Entities.Event
        {
            // Set event properties as needed
        };
        _eventRepository.GetEvent(eventId).Returns(eventFromRepo);

        var updatedEvent = new Data.Entities.Event
        {
            // Set updated event properties as needed
        };
        _eventRepository.UpdateEvent(Arg.Any<Data.Entities.Event>());

        // Act
        var response = await _eventService.UpdateEvent(request, eventId);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
        // Additional assertions based on your implementation
    }

    [Fact]
    public async Task UpdateEvent_InvalidUser_ReturnsNotFoundApiResponse()
    {
        // Arrange
        var eventId = 1;
        var request = new AddEventRequest
        {
            UserId = 1,
            // Set other properties as needed
        };
        _userRepository.GetUser(request.UserId).Returns((User)null);

        // Act
        var response = await _eventService.UpdateEvent(request, eventId);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
        // Additional assertions based on your implementation
    }

    [Fact]
    public async Task UpdateEvent_EventNotFound_ReturnsNotFoundApiResponse()
    {
        // Arrange
        var eventId = 1;
        var request = new AddEventRequest
        {
            UserId = 1,
        };
        var userFromRepo = new User
        {
           Id = 1
        };
        _userRepository.GetUser(request.UserId).Returns(userFromRepo);
        _eventRepository.GetEvent(eventId).Returns((Data.Entities.Event)null);

        // Act
        var response = await _eventService.UpdateEvent(request, eventId);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
        // Additional assertions based on your implementation
    }

    [Fact]
    public async Task DeleteEvent_EventExists_ReturnsSuccessApiResponse()
    {
        // Arrange
        var eventId = 1;
        var eventFromRepo = new Data.Entities.Event
        {
            Id = 1
        };
        _eventRepository.GetEvent(eventId).Returns(eventFromRepo);

        // Act
        var response = await _eventService.DeleteEvent(eventId);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
    }
    [Fact]
    public async Task DeleteEvent_EventNotFound_ReturnsNotFoundApiResponse()
    {
        // Arrange
        var eventId = 1;
        _eventRepository.GetEvent(eventId).Returns((Data.Entities.Event)null);

        // Act
        var response = await _eventService.DeleteEvent(eventId);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessFul);
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
    }
  
}




    
    

