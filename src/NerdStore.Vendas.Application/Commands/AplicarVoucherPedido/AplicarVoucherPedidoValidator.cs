using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.AplicarVoucherPedido;

public class AplicarVoucherPedidoValidator : AbstractValidator<AplicarVoucherPedidoCommand>
{
    public AplicarVoucherPedidoValidator()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.CodigoVoucher)
            .NotEmpty()
            .WithMessage("O código do voucher não pode ser vazio");
    }
}
