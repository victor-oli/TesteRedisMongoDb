using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using TesteRedisMongoDb.Mongo;
using TesteRedisMongoDb.Redis;

namespace TesteRedisMongoDb.Ui
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoManager.Config(ConfigurationManager.AppSettings["MongoDbConnection"].ToString(),
                ConfigurationManager.AppSettings["MongoDbName"].ToString());

            while (true)
            {
                Console.Write("Digite (B) para buscar e (C) para cadastrar: ");

                string option = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(option))
                {
                    if (option.ToUpper().Equals("B"))
                    {
                        while (true)
                        {
                            Console.Write("Digite o nome do produto: ");

                            string nomeDoProduto = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(nomeDoProduto))
                            {
                                ProdutoInMemory produto = BuscarProduto(nomeDoProduto);

                                if (produto != null)
                                    Console.WriteLine("Produto: " + JsonConvert.SerializeObject(produto));
                                else
                                    Console.WriteLine("O produto digitado não existe.");

                                break;
                            }
                            else
                                break;
                        }
                    }
                    else if (option.ToUpper().Equals("C"))
                    {
                        while (true)
                        {
                            Console.Write("Digite o nome do produto: ");

                            string nomeDoProduto = Console.ReadLine();

                            Console.Write("Digite o valor do produto: ");

                            decimal valorDoProduto = Convert.ToDecimal(Console.ReadLine());

                            CadastrarProduto(nomeDoProduto, valorDoProduto);

                            Console.WriteLine("Produto cadastrado com sucesso.");

                            break;
                        }
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }

        private static void CadastrarProduto(string nome, decimal valor)
        {
            IMongoCollection<Produto> collection = MongoManager.open<Produto>("Produto");
            Produto produto = new Produto
            {
                Nome = nome,
                Valor = valor
            };

            collection.InsertOne(produto);

            var produtoNaMemoria = new ProdutoInMemory
            {
                Id = produto.Id.ToString(),
                Nome = produto.Nome,
                Valor = produto.Valor
            };

            ProdutoInMemoryDao.Set(produtoNaMemoria);
        }

        private static ProdutoInMemory BuscarProduto(string nome)
        {
            ProdutoInMemory produtoNaMemoria = ProdutoInMemoryDao.GetByNome(nome);

            if (produtoNaMemoria == null)
            {
                IMongoCollection<Produto> collection = MongoManager.open<Produto>("Produto");
                Produto produto = collection.Find(x => x.Nome.Equals(nome)).FirstOrDefault();

                if (produto == null)
                    return null;

                produtoNaMemoria = new ProdutoInMemory
                {
                    Id = produto.Id.ToString(),
                    Nome = produto.Nome,
                    Valor = produto.Valor
                };

                ProdutoInMemoryDao.Set(produtoNaMemoria);
            }

            return produtoNaMemoria;
        }
    }
}