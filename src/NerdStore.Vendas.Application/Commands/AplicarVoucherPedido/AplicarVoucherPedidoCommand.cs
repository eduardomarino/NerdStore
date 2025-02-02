using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.AplicarVoucherPedido;

public class AplicarVoucherPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public string CodigoVoucher { get; private set; }

    public AplicarVoucherPedidoCommand(Guid clienteId, string codigoVoucher)
    {
        ClienteId = clienteId;
        CodigoVoucher = codigoVoucher;
    }

    public override bool EhValido()
    {
        ValidationResult = new AplicarVoucherPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
