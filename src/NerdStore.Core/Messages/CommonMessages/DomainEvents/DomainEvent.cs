using MediatR;

namespace NerdStore.Core.Messages.CommonMessages.DomainEvents;

// Eventos onde as tratativas são ações que vão além da responsabilidade do domínio
public class DomainEvent : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
        Timestamp = DateTime.Now;
    }
}
