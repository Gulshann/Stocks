using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using StocksApp.Configurations;
using StocksApp.Models;
using StocksApp.Services.Cache;
using StocksApp.Services.SerializeService;
using StocksApp.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StocksApp.Services.StocksAPI
{
    public class StocksAPIClient : IStocksAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISerializer _serializer;
        private readonly ICacheService _cacheService;
        private readonly SessionData _sessionData;
        private readonly Settings _settings;

        public StocksAPIClient(IHttpClientFactory httpClientFactory, ISerializer serializer, IOptions<Settings> options, ICacheService cacheService, SessionData sessionData)
        {
            this._httpClientFactory = httpClientFactory;
            this._serializer = serializer;
            this._cacheService = cacheService;
            this._sessionData = sessionData;
            this._settings = options.Value;
        }

        public async Task<ContestStatus> GetFSLContestStatus()
        {
            string url = _settings.StockBaseUrl + "getsflcontest";
            return await GetAsync<ContestStatus>(url);
        }

        public async Task<ContestJoinInfo> JoinFSLContest(Order orderJSON)
        {
            //var jsonObject = @"{""contestdate"":""05-01-2023"",""teamname"":""sfluser71"",""selection"":{""totalbuyamount"":434900,""buyinfo"":{""BAJFINANCE"":{""buyprice"":6610,""quantity"":50,""amount"":330500},""HCLTECH"":{""buyprice"":1044,""quantity"":100,""amount"":104400}}}}";
            string url = _settings.StockBaseUrl + "joinsflcontest";
            try
            {
                var httpClient = this._httpClientFactory.CreateClient("Stocks");

                var jsonObject = _serializer.Serialize<Order>(orderJSON);

                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = await httpClient.PostAsync(url, content);

                var responseAsString = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return this._serializer.Deserialize<ContestJoinInfo>(responseAsString);
                }

                throw new HttpRequestException(apiResponse?.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, Contest>> GetFSLPreContestFeed()
        {
            var todayDate = DateTime.Now.ToShortDateString();
            List<Contest> contestList = new List<Contest>();

            string cacheKey = "contestfeed_" + todayDate;
            var cacheResult = await _cacheService.GetAsync<Dictionary<string, Contest>>(cacheKey);

            if (cacheResult != null)
            {
                return cacheResult;
            }

            string url = _settings.StockBaseUrl + "getsflprecontestfeed";
            Dictionary<string, Contest> prevDayContest = new Dictionary<string, Contest>();


            var date = DateTime.Now.AddDays(_settings.DayToShowGraphData).Date;


            while (date <= DateTime.Now.Date.AddDays(_settings.FetchPreviousDays))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    var contestFeedData = await PostAsync<Contest>(url, date.ToShortDateString());

                    prevDayContest.Add(date.ToShortDateString(), contestFeedData);
                }
                date = date.AddDays(1);
            }

            await _cacheService.InsertAsync(cacheKey, prevDayContest, _settings.CacheExpiryTimeinMin);

            return prevDayContest;
        }

        public async Task<Leaderboard> GetSFLLeaderboard()
        {
            var todayDate = DateTime.Now.AddDays(_settings.FetchPreviousDays).ToShortDateString();

            string cacheKey = "Leaderboard_" + todayDate;

            var cacheResult = await _cacheService.GetAsync<Leaderboard>(cacheKey);
            if (cacheResult != null)
            {
                return cacheResult;
            }

            string url = _settings.StockBaseUrl + "getsflleaderboard";
            return await PostAsync<Leaderboard>(url, todayDate);
        }

        public async Task<Team> GetSFLTeamScore(string teamname = "")
        {
            Team team = new Team();

            var todayDate = DateTime.Now.AddDays(_settings.FetchPreviousDays).ToShortDateString();

            string cacheKey = "teamScore_" + teamname + todayDate;
            var cacheResult = await _cacheService.GetAsync<Team>(cacheKey);

            if (cacheResult != null)
            {
                return cacheResult;
            }

            //var jsonObject = @"{""contestdate"":""05-01-2023"",""teamname"":""sfluser1""}";
            string url = _settings.StockBaseUrl + "getsflteamscore";
            try
            {
                TeamInfo teamInfo = new TeamInfo()
                {
                    teamname = !string.IsNullOrEmpty(teamname) ? teamname : _sessionData.UserID,
                    contestdate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToShortDateString()
                };
                var jsonObject = _serializer.Serialize<TeamInfo>(teamInfo);

                var httpClient = this._httpClientFactory.CreateClient("Stocks");

                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = await httpClient.PostAsync(url, content);

                var responseAsString = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    team = this._serializer.Deserialize<Team>(responseAsString);
                    await _cacheService.InsertAsync(cacheKey, team, _settings.CacheExpiryTimeinMin);
                    return team;
                }

                throw new HttpRequestException(apiResponse?.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var httpClient = this._httpClientFactory.CreateClient("Stocks");

                HttpResponseMessage apiResponse = await httpClient.GetAsync(url);

                var responseAsString = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return this._serializer.Deserialize<T>(responseAsString);
                }

                throw new HttpRequestException(apiResponse?.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<T> PostAsync<T>(string url, string date)
        {

            //var jsonObject = @"{""contestdate"":""05-01-2023"",""teamname"":""sfluser71"",""selection"":{""totalbuyamount"":434900,""buyinfo"":{""BAJFINANCE"":{""buyprice"":6610,""quantity"":50,""amount"":330500},""HCLTECH"":{""buyprice"":1044,""quantity"":100,""amount"":104400}}}}";
            //var jsonObject = @"{""contestdate"":""05-01-2023"",""teamname"":""sfluser1""}";
            try
            {
                var httpClient = this._httpClientFactory.CreateClient("Stocks");

                HttpContent httpContent = new StringContent(date);

                var content = new MultipartFormDataContent();
                content.Add(httpContent, "contestdate");

                HttpResponseMessage apiResponse = await httpClient.PostAsync(url, content);

                var responseAsString = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return this._serializer.Deserialize<T>(responseAsString);
                }

                throw new HttpRequestException(apiResponse?.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
