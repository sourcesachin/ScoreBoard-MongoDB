using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderboardMicroServices.Model;
using LeaderboardMicroServices.Repository;
using Microsoft.AspNetCore.Mvc;
using LeaderboardMicroServices.Helper;
namespace LeaderboardMicroServices.Controllers
{
    [LbAuthorizeAttribute]
    [ApiController]
    public class LbServiceController : ControllerBase
    {
        readonly ILeaderBoard _leaderBoard;
        public LbServiceController(ILeaderBoard leaderBoard)
        {
            _leaderBoard = leaderBoard;
        }

        [HttpPost]
        [Route("addscore")]
        public IActionResult AddScore([FromBody] PlayerScore playerScore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _leaderBoard.AddScore(playerScore);
            return Ok(playerScore);
        }

        [HttpGet]
        [Route("LeaderBoad")]
        public IActionResult LeaderBoad()
        {
            string matchname = String.Empty;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["matchname"]))
                matchname = HttpContext.Request.Query["matchname"];

            string time = String.Empty;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["time"]))
                time = HttpContext.Request.Query["time"];

            var scores = new PlayerScoreViewModel()
            {
                stats = _leaderBoard.LeaderBoardfilter(matchname, time).Select(x => new PlayerScore
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Rank = x.Rank,
                    Kills = x.Kills,
                    Score = x.Score,
                    EnteryDate = x.EnteryDate

                }).ToList()
            };

            if (scores.stats == null)
            {
                return NotFound("Nothing found!");
            }
            return Ok(scores);
        }


        [HttpGet]
        [Route("playersStats/{id}")]
        public IActionResult playersStats(String id)
        {
            var scores = new PlayerScoreViewModel()
            {
                PlayersStats = _leaderBoard.playersStats(id).Select(x => new PlayerScore
                {
                    Id = x.Id,
                    MatchName = x.MatchName,
                    Kills = x.Kills,
                    Score = x.Score,
                    EnteryDate = x.EnteryDate
                }).ToArray()
            };
            if (scores.PlayersStats == null)
            {
                return NotFound("Nothing found!");
            }
            return Ok(scores);
        }

        [HttpGet]
        [Route("playersStats")]
        public IActionResult playersStats()
        {
            string matchname = String.Empty;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["matchname"]))
                matchname = HttpContext.Request.Query["matchname"]; string time = String.Empty;

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["time"]))
                time = HttpContext.Request.Query["time"];

            var scores = new PlayerScoreViewModel()
            {
                PlayersStats = _leaderBoard.playersStatsfilter(matchname, time).Select(x => new PlayerScore
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Kills = x.Kills,
                    Score = x.Score,
                    EnteryDate = x.EnteryDate

                }).ToArray()
            };

            if (scores.PlayersStats == null)
            {
                return NotFound("Nothing found!");
            }
            return Ok(scores);
        }


    }
}
