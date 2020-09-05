using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;

namespace DataBase
{
    public static class DataBase
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {

            string db = Environment.GetEnvironmentVariable("URLDB");
            string cacheConnection = db; 
            ConfigurationOptions option = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { cacheConnection }
            };
            return ConnectionMultiplexer.Connect(option);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

    }
}
