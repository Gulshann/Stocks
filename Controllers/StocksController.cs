using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Configurations;
using StocksApp.Filters;
using StocksApp.Models;
using StocksApp.Services.Cache;
using StocksApp.Services.SerializeService;
using StocksApp.Services.StocksAPI;
using StocksApp.Session;
using StocksApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Controllers
{

    [ServiceFilter(typeof(CustomBaseFilter))]
    public class StocksController : Controller
    {
        private readonly IStocksAPIClient _stocksAPIClient;
        private readonly ISerializer _serializer;
        private readonly IOptions<Settings> options;
        private readonly SessionData _sessionData;
        private readonly ICacheService _cacheService;
        private readonly Settings _settings;

        public StocksController(IStocksAPIClient stocksAPIClient, ISerializer serializer, IOptions<Settings> options, SessionData sessionData, ICacheService cacheService)
        {
            this._stocksAPIClient = stocksAPIClient;
            this._serializer = serializer;
            this.options = options;
            this._sessionData = sessionData;
            this._cacheService = cacheService;
            this._settings = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);
            var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

            StocksFeedViewModel stocksFeedViewModel = new StocksFeedViewModel();
            stocksFeedViewModel.date = todayDate.ToShortDateString();

            if (todayDate.DayOfWeek != DayOfWeek.Saturday && todayDate.DayOfWeek != DayOfWeek.Sunday)
            {
                stocksFeedViewModel.isHoliday = false;
                stocksFeedViewModel.contest = currentDateFeedData[stocksFeedViewModel.date];
            }
            else
            {
                stocksFeedViewModel.isHoliday = true;
            }

            return View("home", stocksFeedViewModel);
        }

        public async Task<IActionResult> Play()
        {
            var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);
            var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

            List<DateTime> contestList = currentDateFeedData.Select(x => DateTime.Parse(x.Key)).OrderByDescending(x => x.Date).ToList();
            var prevDayStockPrices = currentDateFeedData[contestList[1].ToShortDateString()];

            PlayViewModel playViewModel = new PlayViewModel();
            playViewModel.date = todayDate.ToShortDateString();

            playViewModel.stockPrices = currentDateFeedData[playViewModel.date].contestfeed.data;

            Dictionary<string, double> profitDict = new Dictionary<string, double>();

            string cacheKey = "Profit_" + playViewModel.date;

            var cacheValue = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
            if (cacheKey != null)
            {
                profitDict = cacheValue;
            }
            else
            {
                foreach (var item in playViewModel.stockPrices)
                {
                    double prevPrice = prevDayStockPrices.contestfeed.data[item.Key];
                    double priceDiff = item.Value - prevPrice;
                    double changePercentage = (priceDiff * 100 / prevPrice);
                    profitDict.Add(item.Key, Math.Round(changePercentage, 2));
                }

                await _cacheService.InsertAsync(cacheKey, profitDict, _settings.CacheExpiryTimeinMin);
            }

            playViewModel.profitList = profitDict;

            ViewBag.ContestDate = playViewModel.date;
            ViewBag.UserName = "user";

            return View(playViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Contest(Order orderJSON)
        {
            var res=await _stocksAPIClient.JoinFSLContest(orderJSON);

            if (res.status == "success")
            {
                return Ok(res);
            }
            return null;
        }

        public IActionResult Confirm()
        {
            return View();
        }

        public async Task<IActionResult> Games()
        {
            var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);
            LearboardViewModel learboardViewModel = new LearboardViewModel();

            Team team = new Team();
            if (_sessionData.Team.teamscore != null)
            {
                team = _sessionData.Team;
            }
            else
            {
                await _stocksAPIClient.GetSFLTeamScore();
            }

            learboardViewModel.Team = team;

            learboardViewModel.Leaderboard = await _stocksAPIClient.GetSFLLeaderboard();

            string cacheKey = "Profit_" + DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToShortDateString();

            var cacheResult = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
            if (cacheResult != null)
            {
                learboardViewModel.profitList = cacheResult;
            }
            else
            {
                learboardViewModel.profitList= await PopulateDict();
            }

            ViewBag.ContestDate= DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).Day;
            ViewBag.ContestMonth = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("MMM");

            return View(learboardViewModel);
        }

        public async Task<IActionResult> Performance(string teamname)
        {
            var team = await _stocksAPIClient.GetSFLTeamScore(teamname);

            Dictionary<string, double> tempDict = new Dictionary<string, double>();

            foreach(var item in team.teamscore.stockinfo.Keys) {
                var totalAmount= team.teamscore.stockinfo[item].quantity * team.teamscore.stockinfo[item].currentprice;
                tempDict.Add(item, totalAmount);
            }

            ViewBag.UserID = teamname;
            ViewBag.ContestDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).Day;
            ViewBag.ContestMonth = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("MMM");

            //var serializeData = _serializer.Serialize<Dictionary<string, int>>(tempDict);

            return View(tempDict);
        }

        private async Task<Dictionary<string, double>> PopulateDict()
        {
            var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);
            var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

            List<DateTime> contestList = currentDateFeedData.Select(x => DateTime.Parse(x.Key)).OrderByDescending(x => x.Date).ToList();
            var prevDayStockPrices = currentDateFeedData[contestList[1].ToShortDateString()];

            var stockPriceList = currentDateFeedData[todayDate.ToShortDateString()].contestfeed.data;

            Dictionary<string, double> profitDict = new Dictionary<string, double>();

            string cacheKey = "Profit_" + todayDate.ToShortDateString();

            var cacheValue = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
            if (cacheKey != null)
            {
                profitDict = cacheValue;
            }
            else
            {
                foreach (var item in stockPriceList)
                {
                    double prevPrice = prevDayStockPrices.contestfeed.data[item.Key];
                    double priceDiff = item.Value - prevPrice;
                    double changePercentage = (priceDiff * 100 / prevPrice);
                    profitDict.Add(item.Key, Math.Round(changePercentage, 2));
                }

                await _cacheService.InsertAsync(cacheKey, profitDict, _settings.CacheExpiryTimeinMin);
            }

            return profitDict;
        }

        public async Task<IActionResult> Graph(string companyName)
        {
            var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();
            Dictionary<string, double> priceList = new Dictionary<string, double>();
            foreach (var item in currentDateFeedData)
            {
                if (item.Value.status == "success" && item.Value.contestfeed.data.ContainsKey(companyName))
                {
                    var price = item.Value.contestfeed.data[companyName];
                    priceList.Add(item.Key, price);
                }
            }

            GraphViewModel graphViewModel = new GraphViewModel();
            graphViewModel.companyName = companyName;
            graphViewModel.stockPrice = priceList;

            graphViewModel.maxAxis = priceList.Count > 0 ? priceList.Select(x => x.Value).Max() : 100;

            return View(graphViewModel);
        }

    }
}
