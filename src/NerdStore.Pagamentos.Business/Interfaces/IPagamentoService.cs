using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Pagamentos.Business.Interfaces;

public interface IPagamentoService
{
    Task<Transacao> RealizarPagamentoPedido(PagamentoPedidoDTO pagamentoPedido);
}
