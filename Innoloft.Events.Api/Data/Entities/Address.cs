namespace Innoloft.Events.Api.Data.Entities;

public class Address : BaseEntity
{
    public string Street { get; set; }
    public string Suite { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public int GeoLocationId { get; set; }
    public GeoLocation GeoLocation { get; set; }
}