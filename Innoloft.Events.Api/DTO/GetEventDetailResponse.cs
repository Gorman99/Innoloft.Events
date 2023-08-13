namespace Innoloft.Events.Api.DTO;

public class GetEventDetailResponse
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContactPerson ContactPerson { get; set; } 
}