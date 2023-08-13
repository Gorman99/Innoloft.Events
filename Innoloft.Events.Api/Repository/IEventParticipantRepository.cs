namespace Innoloft.Events.Api.Repository;

public interface IEventParticipantRepository
{
    Task AddParticipant (int userId , int eventId);
}