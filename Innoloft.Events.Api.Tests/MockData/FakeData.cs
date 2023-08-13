using Bogus;
using Innoloft.Events.Api.Data.Entities;

namespace Innoloft.Events.Api.Tests.MockData;

public static class FakeData
{
    public static Faker<Event> GetEventGenerator()
    {
        return new Faker<Event>()
            .RuleFor(e => e.Id, f => f.IndexFaker)
            .RuleFor(e => e.Title, f => f.Lorem.Sentence())
            .RuleFor(e => e.Description, f => f.Lorem.Paragraph())
            .RuleFor(e => e.StartDate, f => f.Date.Future())
            .RuleFor(e => e.EndDate, (f, e) => f.Date.Future(5, e.StartDate))
            .RuleFor(e => e.TimeZone, f => f.Random.ArrayElement(new[] { "UTC", "GMT", "PST", "EST" }))
            .RuleFor(e => e.UserId, f => f.Random.Number(1, 100));
    }

    public static Faker<User> GetUserGenerator()
    {
        return new Faker<User>()
            .RuleFor(user => user.Name, f => f.Name.FullName())
            .RuleFor(user => user.UserName, f => f.Internet.UserName())
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.UserName))
            .RuleFor(user => user.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(user => user.Website, f => f.Internet.Url());
    }  
    
}