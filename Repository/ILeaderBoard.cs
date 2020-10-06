using LeaderboardMicroServices.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardMicroServices.Repository
{
     public interface ILeaderBoard
    {
        PlayerScore AddScore(PlayerScore playerScore);
        IEnumerable<PlayerScore> playersStats(String id);
        IEnumerable<PlayerScore> playersStatsfilter(String matchname,String time);
        IEnumerable<PlayerScore> LeaderBoardfilter(String matchname, String time);
    }
}
