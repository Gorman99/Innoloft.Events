namespace Innoloft.Events.Api.Data.Cache;

public class Invitaion
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Message? Message { get; set; }
    public Invitee Invitee { get; set; }
    public int EventId { get; set; }
    public bool IsReceive { get; set; } = false;
}