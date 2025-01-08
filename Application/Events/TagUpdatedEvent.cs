namespace Application.Events
{
    public record TagUpdatedEvent(Guid TagId, string TagName) : IEventOrMessage;
}
