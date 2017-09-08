using System;
using System.Linq;
using MongoDB.Driver;
using TesteRedisMongoDb.Mongo;
using TesteRedisMongoDb.Redis;

namespace TesteRedisMongoDb.Ui
{
	class Program
	{
		static void Main(string[] args)
		{
			ProdutoInMemory galaxy8NaMemoria = ProdutoInMemoryDao.GetByNome("Galaxy 8");
			Produto galaxy8NoMongo = null;

			if (galaxy8NaMemoria == null)
			{
				var collection = new MongoManager<Produto>().open("Produto");

				galaxy8NoMongo = collection.Find(x => x.Nome.Equals("Galaxy 8")).FirstOrDefault();

				galaxy8NaMemoria = new ProdutoInMemory
				{
					Id = galaxy8NoMongo.Id.ToString(),
					Nome = galaxy8NoMongo.Nome,
					Valor = galaxy8NoMongo.Valor
				};

				ProdutoInMemoryDao.Set(galaxy8NaMemoria);
			}

			Console.WriteLine($"{galaxy8NaMemoria.Nome} - {galaxy8NaMemoria.Valor}");

			Console.ReadKey();
		}
	}
}