using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedido;

public class CancelarProcessamentoPedidoCommand : Command
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid clienteId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}
