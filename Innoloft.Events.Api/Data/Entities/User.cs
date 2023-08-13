namespace Innoloft.Events.Api.Data.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? ImageUrl { get; set; }
    public string? Website { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public List<EventParticipant> Participants { get; set; }
    public List<Event> Events { get; set; }
}