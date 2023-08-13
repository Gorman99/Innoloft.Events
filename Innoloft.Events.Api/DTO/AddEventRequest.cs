using System.ComponentModel.DataAnnotations;

namespace Innoloft.Events.Api.DTO;

public class AddEventRequest
{
    [Required]
    public string Title { get; set; }
    [Required]

    public DateTime StartDate { get; set; }
    [Required]

    public DateTime EndDate { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]

    public string TimeZone { get; set; }
    [Required]

    public int  UserId { get; set; }

}

