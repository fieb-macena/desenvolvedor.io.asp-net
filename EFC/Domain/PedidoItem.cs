namespace EFC.Domain;

public class PedidoItem
{
    public int Id {get; set;}
    public int PedidoId {get; set;}
    public Pedido Pedido {get; set;}
    public Pedido ProdutoId {get; set;}
    public Produto Produto {get; set;}
    public int Quantidade {get;set;}
    public string Observacao {get;set;}
    public ICollection<PedidoItem> Itens {get;set;}
}