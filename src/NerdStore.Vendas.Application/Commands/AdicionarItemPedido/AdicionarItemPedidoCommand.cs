﻿using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.AdicionarItemPedido;

public class AdicionarItemPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string Nome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public override bool EhValido()
    {
        ValidationResult = new AdicionarItemPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
