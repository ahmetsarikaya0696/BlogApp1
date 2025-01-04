namespace Application.Events
{
    public record PostCreatedEvent(Guid PostId, string UserId, List<Guid> TagIds) : IEventOrMessage;
}
