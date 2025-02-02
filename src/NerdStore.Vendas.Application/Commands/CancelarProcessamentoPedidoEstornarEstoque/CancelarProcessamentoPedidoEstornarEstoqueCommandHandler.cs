using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Domain.Interfaces;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedidoEstornarEstoque;

public class CancelarProcessamentoPedidoEstornarEstoqueCommandHandler : IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public CancelarProcessamentoPedidoEstornarEstoqueCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
    {
        _pedidoRepository = pedidoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand message, CancellationToken cancellationToken)
    {
        if (!ValidarComando(message))
            return false;

        var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

        if (pedido == null)
        {
            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
            return false;
        }

        var itensList = new List<ItemDTO>();
        pedido.PedidoItems.ForEach(i => itensList.Add(new ItemDTO { Id = i.ProdutoId, Quantidade = i.Quantidade }));
        var listaProdutosPedido = new ListaProdutosPedidoDTO { PedidoId = pedido.Id, Itens = itensList };

        pedido.AdicionarEvento(new PedidoProcessamentoCanceladoIntegrationEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido));
        pedido.TornarRascunho();

        return await _pedidoRepository.UnitOfWork.Commit();
    }

    private bool ValidarComando(Command message)
    {
        if (message.EhValido())
            return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        return false;
    }
}
