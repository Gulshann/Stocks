using StocksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.ViewModels
{
    public class StocksFeedViewModel
    {
        public string date { get; set; }
        public Contest contest { get; set; }
        public bool isHoliday { get; set; }
    }

    public class PlayViewModel
    {
        public string date { get; set; }
        public Dictionary<string, double> stockPrices { get; set; }

        public Dictionary<string, double> profitList { get; set; }
    }

    public class GraphViewModel
    {
        public string companyName { get; set; }
        public Dictionary<string, double> stockPrice { get; set; }
        public double maxAxis { get; set; }
    }

    public class LearboardViewModel
    {
        public Leaderboard Leaderboard { get; set; }
        public Team Team { get; set; }
        public Dictionary<string, double> profitList { get; set; }
    }
}
