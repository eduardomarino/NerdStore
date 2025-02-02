using MediatR;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Pagamentos.Business.Interfaces;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Data.Repository;
using NerdStore.Pagamentos.Data;
using NerdStore.Vendas.Application.Commands.AdicionarItemPedido;
using NerdStore.Vendas.Application.Commands.AplicarVoucherPedido;
using NerdStore.Vendas.Application.Commands.AtualizarItemPedido;
using NerdStore.Vendas.Application.Commands.RemoverItemPedido;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Data;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Domain.Interfaces;
using NerdStore.Pagamentos.AntiCorruption.Interfaces;
using NerdStore.Pagamentos.AntiCorruption;
using ConfigurationManager = NerdStore.Pagamentos.AntiCorruption.ConfigurationManager;
using IConfigurationManager = NerdStore.Pagamentos.AntiCorruption.Interfaces.IConfigurationManager;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Pagamentos.Business.Events;
using NerdStore.Vendas.Application.Commands.IniciarPedido;
using NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedido;
using NerdStore.Vendas.Application.Commands.CancelarProcessamentoPedidoEstornarEstoque;
using NerdStore.Vendas.Application.Commands.FinalizarPedido;
using NerdStore.Core.Data.EventSourcing;
using EventSourcing;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Domain Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Event Sourcing
        services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

        // Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<CatalogoContext>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

        services.AddScoped<INotificationHandler<PedidoIniciadoIntegrationEvent>, ProdutoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoIntegrationEvent>, ProdutoEventHandler>();

        // Vendas
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPedidoQueries, PedidoQueries>();
        services.AddScoped<VendasContext>();

        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, AdicionarItemPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, AplicarVoucherPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, AtualizarItemPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, CancelarProcessamentoPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, CancelarProcessamentoPedidoEstornarEstoqueCommandHandler>();
        services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, FinalizarPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, IniciarPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, RemoverItemPedidoCommandHandler>();

        services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoFinalizadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoProdutoAtualizadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoProdutoRemovidoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<VoucherAplicadoPedidoEvent>, PedidoEventHandler>();

        services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoIntegrationEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PagamentoRealizadoIntegrationEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PagamentoRecusadoIntegrationEvent>, PedidoEventHandler>();

        // Pagamentos
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<IConfigurationManager, ConfigurationManager>();
        services.AddScoped<PagamentoContext>();

        services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoIntegrationEvent>, PagamentoEventHandler>();
    }
}
