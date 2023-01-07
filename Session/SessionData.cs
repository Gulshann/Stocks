using StocksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Session
{
    public class SessionData
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public double TotalAmount { get; set; }
        public double RemainingAmount { get; set; }
        public Team Team { get; set; }
        public Order Order { get; set; }
        public int TotalPickCount { get; set; }
    }
}
