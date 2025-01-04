namespace Application.Events
{
    public record PostLikeChangedEvent(Guid PostId, string UserId, bool IsLiked) : IEventOrMessage;
}
