using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Models
{
    public class ContestJoinInfo
    {
        public string status { get; set; }
        public string info { get; set; }
    }

    public class User
    {
        public string member { get; set; }
        public int rank { get; set; }
        public double score { get; set; }
    }

    public class Leaderboard
    {
        public List<User> leaderboard { get; set; }
        public string status { get; set; }
    }

    public class ContestStatus
    {
        public string status { get; set; }
        public string contestdate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>

    public class Contestfeed
    {
        public Contestfeed()
        {
        }
        public string name { get; set; }
        public string state { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, double> data { get; set; }
        public string feedtime { get; set; }
    }

    public class Data
    {
        public Data()
        {
            keyValuePairs = new Dictionary<string, double>();
        }
        Dictionary<string, double> keyValuePairs { get; set; }
    }

    public class Contest
    {
        public string status { get; set; }
        public Contestfeed contestfeed { get; set; }
    }

    ///

    public class Stockinfo
    {
        public double buyprice { get; set; }
        public int quantity { get; set; }
        public double amount { get; set; }
        public double currentprice { get; set; }
        public double currentamount { get; set; }
        public double gain { get; set; }
    }

    public class Stocks
    {
        public Dictionary<string, Stockinfo> stocksList { get; set; }
    }

    public class Teamscore
    {
        public string teamname { get; set; }
        public string cdate { get; set; }
        public string state { get; set; }
        public double totalbuyamount { get; set; }
        public Dictionary<string, Stockinfo> stockinfo { get; set; }
    }

    public class Team
    {
        public int rank { get; set; }
        public double score { get; set; }
        public string status { get; set; }
        public string teamname { get; set; }
        public Teamscore teamscore { get; set; }
    }

    public class Buyinfo
    {
        public int buyprice { get; set; }
        public int quantity { get; set; }
        public int amount { get; set; }
    }

    public class Order
    {
        public string contestdate { get; set; }
        public string teamname { get; set; }
        public Selection selection { get; set; }
    }

    public class Selection
    {
        public int totalbuyamount { get; set; }
        public Dictionary<string, Buyinfo> buyinfo { get; set; }
    }

    public class TeamInfo
    {
        public string teamname { get; set; }
        public string contestdate { get; set; }
    }


    public class StockTrend
    {
        public string StockName { get; set; }
        public double BuyAmount { get; set; }
        public double CurrentAmount { get; set; }
        public int Quantity { get; set; }
        public double Profit { get; set; }
    }

}
