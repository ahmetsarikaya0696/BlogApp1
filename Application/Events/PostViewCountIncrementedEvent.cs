namespace Application.Events
{
    public record PostViewCountIncrementedEvent(Guid Id) : IEventOrMessage;
}
