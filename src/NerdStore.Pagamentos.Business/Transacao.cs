﻿using NerdStore.Core.DomainObjects;
using NerdStore.Pagamentos.Business.Enums;

namespace NerdStore.Pagamentos.Business;

public class Transacao : Entity
{
    public Guid PedidoId { get; set; }
    public Guid PagamentoId { get; set; }
    public decimal Total { get; set; }
    public StatusTransacao StatusTransacao { get; set; }

    // EF. Rel.
    public Pagamento Pagamento { get; set; }
}
