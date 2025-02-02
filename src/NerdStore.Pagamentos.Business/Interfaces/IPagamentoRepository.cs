using NerdStore.Core.Data;

namespace NerdStore.Pagamentos.Business.Interfaces;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);

    void AdicionarTransacao(Transacao transacao);
}
