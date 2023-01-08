using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Controllers
{

    [ServiceFilter(typeof(CustomBaseFilter))]
    public class StocksController : Controller
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IStocksAPIClient _stocksAPIClient;
        private readonly ISerializer _serializer;
        private readonly IOptions<Settings> options;
        private readonly SessionData _sessionData;
        private readonly ICacheService _cacheService;
        private readonly Settings _settings;

        public StocksController(ILogger<StocksController> logger, IStocksAPIClient stocksAPIClient, ISerializer serializer, IOptions<Settings> options, SessionData sessionData, ICacheService cacheService)
        {
            this._logger = logger;
            this._stocksAPIClient = stocksAPIClient;
            this._serializer = serializer;
            this.options = options;
            this._sessionData = sessionData;
            this._cacheService = cacheService;
            this._settings = options.Value;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                _logger.LogInformation("Stocks Fantasy League home page.");
                var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);

                _logger.LogInformation($"Current Date - {todayDate.ToString("dd-MM-yyyy")}");

                var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

                StocksFeedViewModel stocksFeedViewModel = new StocksFeedViewModel();
                stocksFeedViewModel.date = todayDate.ToString("dd-MM-yyyy");

                if (todayDate.DayOfWeek != DayOfWeek.Saturday && todayDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    _logger.LogInformation($"Stock Market is operating today : {todayDate.DayOfWeek}");
                    stocksFeedViewModel.isHoliday = false;
                    stocksFeedViewModel.contest = currentDateFeedData[stocksFeedViewModel.date];
                }
                else
                {
                    _logger.LogInformation($"Stock Market is closed today : {todayDate.DayOfWeek}");

                    stocksFeedViewModel.isHoliday = true;
                }

                _logger.LogInformation($"Feed Data : {_serializer.Serialize<Dictionary<string, Contest>>(currentDateFeedData)}");

                return View("home", stocksFeedViewModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> Play()
        {
            try
            {
                var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);

                _logger.LogInformation($"Play Contest : {todayDate.ToString("dd-MM-yyyy")}");

                var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

                _logger.LogInformation($"Feed Data : {_serializer.Serialize<Dictionary<string, Contest>>(currentDateFeedData)}");

                List<DateTime> contestList = currentDateFeedData.Select(x => DateTime.ParseExact(x.Key, "dd-MM-yyyy", CultureInfo.InvariantCulture)).OrderByDescending(x => x.Date).ToList();
                var prevDayStockPrices = currentDateFeedData[contestList[1].ToString("dd-MM-yyyy")];

                PlayViewModel playViewModel = new PlayViewModel();
                playViewModel.date = todayDate.ToString("dd-MM-yyyy");

                playViewModel.stockPrices = currentDateFeedData[playViewModel.date].contestfeed.data;

                Dictionary<string, double> profitDict = new Dictionary<string, double>();

                string cacheKey = "Profit_" + playViewModel.date;

                var contestDetail = await _stocksAPIClient.GetFSLContestStatus();
                if(contestDetail.status!="success" || contestDetail.contestdate != playViewModel.date)
                {
                    playViewModel.IsContestLive = false;
                }
                else
                {
                    playViewModel.IsContestLive = true;
                }

                var cacheValue = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
                if (cacheValue != null)
                {
                    _logger.LogInformation($"{cacheKey} is available in the cache.");
                    profitDict = cacheValue;
                }
                else
                {
                    _logger.LogInformation($"{cacheKey} is not available in the cache so building it again.");
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
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Contest(Order orderJSON)
        {
            var res = await _stocksAPIClient.JoinFSLContest(orderJSON);

            if (res.status == "success")
            {
                //Refreshing the session data.
                await _stocksAPIClient.GetSFLTeamScore();

                return Json(res);
            }
            return null;
        }

        public IActionResult Confirm()
        {
            return View();
        }

        public async Task<IActionResult> Games()
        {
            try
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

                string cacheKey = "Profit_" + DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("dd-MM-yyyy");

                var cacheResult = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
                if (cacheResult != null)
                {
                    learboardViewModel.profitList = cacheResult;
                }
                else
                {
                    learboardViewModel.profitList = await PopulateDict();
                }

                ViewBag.ContestDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).Day;
                ViewBag.ContestMonth = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("MMM");

                return View(learboardViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Performance(string teamname)
        {

            //TODO - In case of a non active teamnam, we should not allow to show this page.
            try
            {
                var team = await _stocksAPIClient.GetSFLTeamScore(teamname);

                if (team.teamscore == null)
                {
                    return RedirectToAction("Index");
                }

                List<StockTrend> stockTrend = new List<StockTrend>();

                foreach (var item in team.teamscore.stockinfo.Keys)
                {
                    StockTrend st = new StockTrend();
                    st.StockName = item;
                    st.CurrentAmount = team.teamscore.stockinfo[item].currentamount;
                    st.BuyAmount = team.teamscore.stockinfo[item].quantity * team.teamscore.stockinfo[item].buyprice;
                    st.Quantity = (int)(team.teamscore.stockinfo[item].quantity);
                    st.Profit = Math.Round(((st.CurrentAmount - st.BuyAmount) * 100) / st.BuyAmount, 2);
                    stockTrend.Add(st);
                }

                ViewBag.UserID = teamname;
                ViewBag.ContestDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).Day;
                ViewBag.ContestMonth = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("MMM");

                //var serializeData = _serializer.Serialize<Dictionary<string, int>>(tempDict);

                return View(stockTrend);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        private async Task<Dictionary<string, double>> PopulateDict()
        {
            try
            {
                var todayDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays);
                var currentDateFeedData = await _stocksAPIClient.GetFSLPreContestFeed();

                List<DateTime> contestList = currentDateFeedData.Select(x => DateTime.ParseExact(x.Key, "dd-MM-yyyy", CultureInfo.InvariantCulture)).OrderByDescending(x => x.Date).ToList();
                var prevDayStockPrices = currentDateFeedData[contestList[1].ToString("dd-MM-yyyy")];

                var stockPriceList = currentDateFeedData[todayDate.ToString("dd-MM-yyyy")].contestfeed.data;

                Dictionary<string, double> profitDict = new Dictionary<string, double>();

                string cacheKey = "Profit_" + todayDate.ToString("dd-MM-yyyy");

                var cacheValue = await _cacheService.GetAsync<Dictionary<string, double>>(cacheKey);
                if (cacheValue != null)
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Graph(string companyName)
        {
            try
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
                ViewBag.StockName = companyName;
                return View(graphViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}
