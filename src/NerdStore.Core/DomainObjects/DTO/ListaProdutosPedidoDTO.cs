namespace NerdStore.Core.DomainObjects.DTO;

public class ListaProdutosPedidoDTO
{
    public Guid PedidoId { get; set; }
    public ICollection<ItemDTO> Itens { get; set; }
}

public class ItemDTO
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }
}
