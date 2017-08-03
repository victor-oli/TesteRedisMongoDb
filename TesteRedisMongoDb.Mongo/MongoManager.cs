using System.Configuration;
using MongoDB.Driver;

namespace TesteRedisMongoDb.Mongo
{
	public class MongoManager<T> where T : class
	{
		private IMongoClient _client;
		private IMongoDatabase _db;
		private IMongoCollection<T> _collection;

		public IMongoCollection<T> open(string collectionName)
		{
			_client = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnection"].ToString());
			_db = _client.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"].ToString());
			_collection = _db.GetCollection<T>(collectionName);

			return _collection;
		}
	}
}