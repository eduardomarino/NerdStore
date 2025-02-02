using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.RemoverItemPedido;

public class RemoverItemPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverItemPedidoCommand(Guid clienteId, Guid produtoId)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }

    public override bool EhValido()
    {
        ValidationResult = new RemoverItemPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
