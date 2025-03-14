﻿using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Catalogo.Domain;

public interface IEstoqueService : IDisposable
{
    Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
    Task<bool> DebitarListaProdutosPedido(ListaProdutosPedidoDTO lista);
    Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    Task<bool> ReporListaProdutosPedido(ListaProdutosPedidoDTO lista);
}
