using System.ComponentModel.DataAnnotations;

namespace Innoloft.Events.Api.DTO;

public class InvitationRequest
{
    [Required]
    public int EventId { get; set; }
    public List<int> Users { get; set; } = new List<int>();
}

public class ParticipateRequest
{
    [Required]
    public int EventId { get; set; }
    public ParticipateMessage  Message{ get; set; }
}

public class ParticipateMessage
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
}