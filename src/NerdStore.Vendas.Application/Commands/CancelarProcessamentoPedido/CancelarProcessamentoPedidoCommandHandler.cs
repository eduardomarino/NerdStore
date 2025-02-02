using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Domain.Interfaces;

namespace NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedido;

public class CancelarProcessamentoPedidoCommandHandler : IRequestHandler<CancelarProcessamentoPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public CancelarProcessamentoPedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
    {
        _pedidoRepository = pedidoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Handle(CancelarProcessamentoPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!ValidarComando(message))
            return false;

        var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

        if (pedido == null)
        {
            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
            return false;
        }

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
