﻿using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.IniciarPedido;

public class IniciarPedidoCommand : Command
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Total { get; private set; }
    public string NomeCartao { get; private set; }
    public string NumeroCartao { get; private set; }
    public string ExpiracaoCartao { get; private set; }
    public string CvvCartao { get; private set; }

    public IniciarPedidoCommand(Guid pedidoId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
    {
        PedidoId = pedidoId;
        ClienteId = clienteId;
        Total = total;
        NomeCartao = nomeCartao;
        NumeroCartao = numeroCartao;
        ExpiracaoCartao = expiracaoCartao;
        CvvCartao = cvvCartao;
    }

    public override bool EhValido()
    {
        ValidationResult = new IniciarPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
