namespace Domain.Constants
{
    public class ServiceBusConstants
    {
        public const string PostCreatedEventQueueName = "blog.app.postcreated.event.queue";
        public const string PostUpdatedEventQueueName = "blog.app.postupdated.event.queue";
        public const string PostDeletedEventQueueName = "blog.app.postdeleted.event.queue";
        public const string PostViewCountIncrementedEventQueueName = "blog.app.postviewcountincremented.event.queue";
        public const string PostLikeChangedEventQueueName = "blog.app.postlikechanged.event.queue";
        public const string UserCreatedEventQueueName = "blog.app.usercreated.event.queue";
    }
}
