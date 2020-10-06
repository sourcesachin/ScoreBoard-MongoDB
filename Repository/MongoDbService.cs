using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderboardMicroServices.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LeaderboardMicroServices.Repository
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database = null;
        public MongoDbService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoConnection:ConnectionString").Value);
            if (client != null)
                _database = client.GetDatabase(config.GetSection("MongoConnection:DataBase").Value);
        }

        public IMongoCollection<PlayerScore> _playerScore
        {
            get
            {
                return _database.GetCollection<PlayerScore>("PlayerScore");
            }
        }
    }
}
