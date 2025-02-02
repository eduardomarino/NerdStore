using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedido;
using NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedidoEstornarEstoque;
using NerdStore.Vendas.Application.Commands.FinalizarPedido;

namespace NerdStore.Vendas.Application.Events;

public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoProdutoAtualizadoEvent>,
        INotificationHandler<PedidoProdutoRemovidoEvent>,
        INotificationHandler<VoucherAplicadoPedidoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoIntegrationEvent>,
        INotificationHandler<PagamentoRealizadoIntegrationEvent>,
        INotificationHandler<PagamentoRecusadoIntegrationEvent>,
        INotificationHandler<PedidoFinalizadoEvent>
{

    private readonly IMediatorHandler _mediatorHandler;

    public PedidoEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    public Task Handle(PedidoRascunhoIniciadoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoAtualizadoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoItemAdicionadoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoProdutoAtualizadoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoProdutoRemovidoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(VoucherAplicadoPedidoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(PedidoEstoqueRejeitadoIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
    }

    public async Task Handle(PagamentoRealizadoIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
    }

    public async Task Handle(PagamentoRecusadoIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
    }

    public Task Handle(PedidoFinalizadoEvent message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
