﻿namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class PedidoEstoqueRejeitadoIntegrationEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public PedidoEstoqueRejeitadoIntegrationEvent(Guid pedidoId, Guid clienteId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}
