﻿using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedidoEstornarEstoque;

public class CancelarProcessamentoPedidoEstornarEstoqueCommand : Command
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public CancelarProcessamentoPedidoEstornarEstoqueCommand(Guid pedidoId, Guid clienteId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}
