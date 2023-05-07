using MediatR;

namespace DDDPOC.SharedKernel
{
    public abstract class BaseIntegrationEvent : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
