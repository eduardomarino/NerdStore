using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.RemoverItemPedido;

public class RemoverItemPedidoValidator : AbstractValidator<RemoverItemPedidoCommand>
{
    public RemoverItemPedidoValidator()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");
    }
}
