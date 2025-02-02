using MediatR;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Pagamentos.Business.Interfaces;

namespace NerdStore.Pagamentos.Business.Events;

public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoIntegrationEvent>
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentoEventHandler(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    public async Task Handle(PedidoEstoqueConfirmadoIntegrationEvent message, CancellationToken cancellationToken)
    {
        var pagamentoPedido = new PagamentoPedidoDTO
        {
            PedidoId = message.PedidoId,
            ClienteId = message.ClienteId,
            Total = message.Total,
            NomeCartao = message.NomeCartao,
            NumeroCartao = message.NumeroCartao,
            ExpiracaoCartao = message.ExpiracaoCartao,
            CvvCartao = message.CvvCartao
        };

        await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
    }
}
