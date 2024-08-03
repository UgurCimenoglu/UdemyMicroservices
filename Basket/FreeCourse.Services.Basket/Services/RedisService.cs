using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;

        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string host, int port, string password)
        {
            _host = host;
            _port = port;
            _password = password;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port},password={_password}");

        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
