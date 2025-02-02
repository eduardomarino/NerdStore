using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.AtualizarItemPedido;

public class AtualizarItemPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public AtualizarItemPedidoCommand(Guid clienteId, Guid produtoId, int quantidade)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public override bool EhValido()
    {
        ValidationResult = new AtualizarItemPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
