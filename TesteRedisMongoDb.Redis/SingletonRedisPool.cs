using RedisBoost;

namespace TesteRedisMongoDb.Redis
{
	public abstract class SingletonRedisPool
	{
		private static IRedisClientsPool _Instance;

		public static IRedisClientsPool getInstance()
		{
			if (_Instance == null)
				_Instance = RedisClient.CreateClientsPool();

			return _Instance;
		}
	}
}