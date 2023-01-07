using StocksApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StocksApp.Services.StocksAPI
{
    public interface IStocksAPIClient
    {
        Task<ContestStatus> GetFSLContestStatus();
        Task<ContestJoinInfo> JoinFSLContest(Order orderJSON);
        Task<Dictionary<string, Contest>> GetFSLPreContestFeed();
        Task<Leaderboard> GetSFLLeaderboard();
        Task<Team> GetSFLTeamScore(string teamname = "");
    }
}