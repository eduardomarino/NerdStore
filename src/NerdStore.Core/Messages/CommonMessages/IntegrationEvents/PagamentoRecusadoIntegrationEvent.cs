﻿using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class PagamentoRecusadoIntegrationEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid PagamentoId { get; private set; }
    public Guid TransacaoId { get; private set; }
    public decimal Total { get; private set; }

    public PagamentoRecusadoIntegrationEvent(Guid pedidoId, Guid clienteId, Guid pagamentoId, Guid transacaoId, decimal total)
    {
        AggregateId = pagamentoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
        PagamentoId = pagamentoId;
        TransacaoId = transacaoId;
        Total = total;
    }
}