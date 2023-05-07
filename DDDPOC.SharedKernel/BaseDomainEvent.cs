using MediatR;

namespace DDDPOC.SharedKernel
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
        public string Message { get; protected set; }

    }
}
