using System.ComponentModel.DataAnnotations;

namespace Innoloft.Events.Api.DTO;

public class AcceptInvitationRequest
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int EventId { get; set; }
}