namespace Application.Events
{
    public record PostUpdatedEvent(Guid PostId, string UserId, List<Guid> TagIds) : IEventOrMessage;
}
