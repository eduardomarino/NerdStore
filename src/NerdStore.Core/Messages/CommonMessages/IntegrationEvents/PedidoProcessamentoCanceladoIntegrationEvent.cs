using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class PedidoProcessamentoCanceladoIntegrationEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public ListaProdutosPedidoDTO ProdutosPedido { get; private set; }

    public PedidoProcessamentoCanceladoIntegrationEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedidoDTO produtosPedido)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
        ProdutosPedido = produtosPedido;
    }
}
