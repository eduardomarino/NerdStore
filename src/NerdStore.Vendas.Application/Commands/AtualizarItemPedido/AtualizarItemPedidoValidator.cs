using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.AtualizarItemPedido;

public class AtualizarItemPedidoValidator : AbstractValidator<AtualizarItemPedidoCommand>
{
    public AtualizarItemPedidoValidator()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade miníma de um item é 1");

        RuleFor(c => c.Quantidade)
            .LessThan(16)
            .WithMessage("A quantidade máxima de um item é 15");
    }
}
