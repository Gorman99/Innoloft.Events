using Innoloft.Events.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Events.Api.Data.Context;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) :
        base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<GeoLocation> GeoLocations { get; set; }
    public DbSet<Event?> Events { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventParticipant>().HasKey(sc => new { sc.EventId, sc.UserId });

        modelBuilder.Entity<Company>().HasData( new Company
        {
            Name = "Romaguera-Crona",
            CatchPhrase = "Multi-layered client-server neural-net",
            BS = "harness real-time e-markets",
            Id = 1
        });
        modelBuilder.Entity<GeoLocation>().HasData(new GeoLocation
        {
            Lat = "-37.3159",
            Lng = "81.1496",
            Id = 1
        }); 
        
        
        modelBuilder.Entity<Address>().HasData(new Address
        {
            Street = "Kulas Light",
            Suite = "Apt. 556",
            City = "Gwenborough",
            ZipCode = "92998-3874",
            GeoLocationId = 1,
            Id = 1
        });
         modelBuilder.Entity<User>().HasData(new User
         {
             Name = "Leanne Graham",
             Id = 1,
             UserName = "Bret",
             Email = "Sincere@april.biz",
             Phone = "1-770-736-8031 x56442",
             Website = "hildegard.org",
             AddressId = 1,
             CompanyId = 1
         });
        
    }

}