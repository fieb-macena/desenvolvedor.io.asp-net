using System.Data.Common;
using EFC.Domain;
using EFC.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EFC;

class Program
{

    static void Main()
    {
        //db.Database.Migrate();
        //InserirDados();
        //InserirDadosEmMassa();
        //ConsultarDados();
        //CadastrarPedido();
        //ConsultarPedidoCarregamentoAdiantado();
        //Atualizar();
        DeletarRegistro();
    }

    private static void ConsultarPedidoCarregamentoAdiantado()
    {
        Data.ApplicationContext db = new();
        var pedidos = db.Pedidos.Include(p => p.Itens).ToList();

        Console.WriteLine(pedidos.Count);
    }

    private static void InserirDados()
    {
        using var db = new Data.ApplicationContext();

        var produto = new Produto
        {
            Descricao = "teste",
            CodigoBarras = "1234567",
            Valor = 100,
            TipoProduto = TipoProduto.MercadoriaParaRevenda,
            Ativo = true
        };

        db.Produtos.Add(produto);
        //outras alternativas de inserção
        //db.Set<Produto>().Add(produto);
        //db.Entry(produto).State = EntityState.Added;
        //db.Add(produto);

        var registros = db.SaveChanges();
        Console.WriteLine($"Quantidade de registros salvos:{registros}");
    }

    private static void InserirDadosEmMassa()
    {
        var produto = new Produto
        {
            Descricao = "teste",
            CodigoBarras = "1234567",
            Valor = 100,
            TipoProduto = TipoProduto.MercadoriaParaRevenda,
            Ativo = true
        };

        var cliente = new Cliente
        {
            Nome = "Joalisson",
            Telefone = "11945268452",
            CEP = "06436300",
            Cidade = "Barueri",
            Estado = "SP"
        };

        Cliente[] clientes = [
            new Cliente {
            Nome = "Pedro",
            Telefone = "11945268452",
            CEP = "06436300",
            Cidade = "Barueri",
            Estado = "SP"
        },
        new Cliente {
            Nome = "Joana",
            Telefone = "11945268452",
            CEP = "06436300",
            Cidade = "Barueri",
            Estado = "SP"
        },
        new Cliente {
            Nome = "Marcos",
            Telefone = "11945268452",
            CEP = "06436300",
            Cidade = "Barueri",
            Estado = "SP"
        }
        ];

        using Data.ApplicationContext db = new();
        db.AddRange(clientes);

        var registros = db.SaveChanges();
        Console.WriteLine($"Registros alterados {registros}");
    }

    public static void ConsultarDados()
    {
        Data.ApplicationContext db = new();
        var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
        var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();
    }

    private static void CadastrarPedido()
    {
        Data.ApplicationContext db = new();

        var cliente = db.Clientes.FirstOrDefault();
        var produto = db.Produtos.FirstOrDefault();

        var pedido = new Pedido
        {
            ClienteId = cliente.Id,
            IniciadoEm = DateTime.Now,
            FinalizadoEm = DateTime.Now,
            Observacao = "Pedido Teste",
            Status = StatusPedido.Analise,
            TipoFrete = TipoFrete.SemFrete,
            Itens =
            [
                new PedidoItem
                {
                    ProdutoId = produto.Id,
                    Desconto = 0,
                    Quantidade = 1,
                    Valor = 10,
                },
                new PedidoItem{
                ProdutoId = produto.Id,
                    Desconto = 0,
                    Quantidade = 1,
                    Valor = 10,
                }
            ]
        };

        db.Add(pedido);
        db.SaveChanges();
    }

    private static void DeletarRegistro()
    {
        Data.ApplicationContext db = new();

        var cliente = new Cliente
        {
            Id = 4
        };

        //var cliente = db.Clientes.Find(4);

        db.Remove(cliente);
        db.SaveChanges();
    }
    private static void Atualizar()
    {
        Data.ApplicationContext db = new();
        var cliente = db.Clientes.Find(1);
        cliente.Nome = "VICTOR MACENA";
        //db.Clientes.Update(cliente);

        //atualização de dados sem rastreamento
        var cliente2 = new Cliente
        {
            Id = 2
        };

        var clienteUpdate = new
        {
            Nome = "Joana Macena",
            Telefone = "11956231245"
        };

        db.Attach(cliente2);
        db.Entry(cliente2).CurrentValues.SetValues(clienteUpdate);


        db.SaveChanges();
    }
}

