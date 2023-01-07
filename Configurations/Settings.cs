using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Configurations
{
    public class Settings
    {
        public string AllowedHosts { get; set; }
        public string StockBaseUrl { get; set; }

        public int CacheExpiryTimeinMin { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public double TotalAmount { get; set; }
        public int DayToShowGraphData { get; set; }
        public int FetchPreviousDays { get; set; }
    }
}
