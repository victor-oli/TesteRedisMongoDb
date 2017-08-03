using System.Configuration;

namespace TesteRedisMongoDb.Redis
{
	public class ProdutoInMemoryDao
	{public static void Set(ProdutoInMemory p)
		{
			var pool = SingletonRedisPool.getInstance();

			using (var client = pool.CreateClientAsync(ConfigurationManager
				.ConnectionStrings["RedisConn"].ConnectionString).Result)
			{
				client.SetAsync($"Produto:{p.Nome}", p);
			}
		}

		public static ProdutoInMemory GetById(string id)
		{
			var pool = SingletonRedisPool.getInstance();

			var produto = new ProdutoInMemory();
			using (var client = pool.CreateClientAsync(ConfigurationManager
				.ConnectionStrings["RedisConn"].ConnectionString).Result)
			{
				produto = client.GetAsync($"Produto:{id}").Result.As<ProdutoInMemory>();
			}

			return produto;
		}

		public static ProdutoInMemory GetByNome(string nome)
		{
			var pool = SingletonRedisPool.getInstance();

			var produto = new ProdutoInMemory();
			using (var client = pool.CreateClientAsync(ConfigurationManager
				.ConnectionStrings["RedisConn"].ConnectionString).Result)
			{
				produto = client.GetAsync($"Produto:{nome}").Result.As<ProdutoInMemory>();
			}

			return produto;
		}
	}
}