namespace Application.Events
{
    public record UserCreatedEvent(string UserId) : IEventOrMessage;
}
