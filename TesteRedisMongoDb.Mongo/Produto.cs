using MongoDB.Bson;

namespace TesteRedisMongoDb.Mongo
{
	public class Produto
	{
		public ObjectId Id { get; set; }
		public string Nome { get; set; }
		public decimal Valor { get; set; }
	}
}