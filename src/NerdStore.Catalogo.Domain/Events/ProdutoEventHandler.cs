﻿using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalogo.Domain.Events;

public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>,
                                   INotificationHandler<PedidoIniciadoIntegrationEvent>,
                                   INotificationHandler<PedidoProcessamentoCanceladoIntegrationEvent>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueService _estoqueService;
    private readonly IMediatorHandler _mediatorHandler;

    public ProdutoEventHandler(IProdutoRepository produtoRepository, IEstoqueService estoqueService, IMediatorHandler mediatorHandler)
    {
        _produtoRepository = produtoRepository;
        _estoqueService = estoqueService;
        _mediatorHandler = mediatorHandler;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(message.AggregateId);

        // Enviar um email para aquisicao de mais produtos.
    }

    public async Task Handle(PedidoIniciadoIntegrationEvent message, CancellationToken cancellationToken)
    {
        var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

        if (result)
        {
            await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoIntegrationEvent(message.PedidoId, message.ClienteId, message.Total, message.ProdutosPedido, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));
        }
        else
        {
            await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoIntegrationEvent(message.PedidoId, message.ClienteId));
        }
    }

    public async Task Handle(PedidoProcessamentoCanceladoIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);
    }
}
