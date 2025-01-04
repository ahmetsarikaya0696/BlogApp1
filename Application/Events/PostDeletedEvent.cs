namespace Application.Events
{
    public record PostDeletedEvent(Guid Id) : IEventOrMessage;
}
