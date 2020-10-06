using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderboardMicroServices.Healper;
using LeaderboardMicroServices.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LeaderboardMicroServices.Repository
{
    public class LeaderBoard : ILeaderBoard
    {
        private readonly MongoDbService _db = null;
        public LeaderBoard(IConfiguration config)
        {
            _db = new MongoDbService(config);
        }
        public PlayerScore AddScore(PlayerScore playerScore)
        {
            try
            {
                _db._playerScore.InsertOne(playerScore);
            }
            catch (Exception ex) { throw ex; }
            return playerScore;
        }

        public IEnumerable<PlayerScore> LeaderBoardfilter(string matchname, string time)
        {
            DateTime start,end;
            var filter = Builders<PlayerScore>.Filter.Eq(x=>x.MatchName,matchname);
            switch (time)
            {
                case "daily":
                    start = DateTime.Now.Date;
                    end =  DateTime.Now.Date.AddDays(1);
                    filter = filter & Builders<PlayerScore>.Filter.Gte(x => x.EnteryDate, start) &
         Builders<PlayerScore>.Filter.Lt(x => x.EnteryDate, end);
                    break;
                case "weekly":
                    start = DateTime.Now.Date;
                    end = DateTime.Now.Date.AddDays(7);
                    filter = filter & Builders<PlayerScore>.Filter.Gte(x => x.EnteryDate, start) &
         Builders<PlayerScore>.Filter.Lte(x => x.EnteryDate, end);
                    break;
            }
            try
            {
                return _db._playerScore.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public IEnumerable<PlayerScore> playersStatsfilter(string matchname, string time)
        {
            DateTime start, end;
            var filter = Builders<PlayerScore>.Filter.Eq(x => x.MatchName, matchname);
            switch (time)
            {
                case "daily":
                    start = DateTime.Now.Date;
                    end = DateTime.Now.Date.AddDays(1);
                    filter = filter & Builders<PlayerScore>.Filter.Gte(x => x.EnteryDate, start) &
         Builders<PlayerScore>.Filter.Lt(x => x.EnteryDate, end);
                    break;
                case "weekly":
                    start = DateTime.Now.Date;
                    end = DateTime.Now.Date.AddDays(7);
                    filter = filter & Builders<PlayerScore>.Filter.Gte(x => x.EnteryDate, start) &
         Builders<PlayerScore>.Filter.Lte(x => x.EnteryDate, end);
                    break;
            }
            try
            {
                return _db._playerScore.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PlayerScore> playersStats(string id)
        {
            var filter = Builders<PlayerScore>.Filter.Eq(x=>x.UserName, id);
            try
            {
                return _db._playerScore.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
