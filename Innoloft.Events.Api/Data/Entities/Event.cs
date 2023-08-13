namespace Innoloft.Events.Api.Data.Entities;

public class Event: BaseEntity
{
   
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string TimeZone { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<EventParticipant> Participants { get; set; }
}