using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using StocksApp.Configurations;
using StocksApp.Services.StocksAPI;
using StocksApp.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Filters
{
    public class CustomBaseFilter : IAsyncActionFilter
    {
        private readonly Settings _settings;
        private readonly SessionData _sessionData;
        private readonly IStocksAPIClient _stocksAPIClient;

        public CustomBaseFilter(IOptions<Settings> options, SessionData sessionData, IStocksAPIClient stocksAPIClient)
        {
            this._settings = options.Value;
            this._sessionData = sessionData;
            this._stocksAPIClient = stocksAPIClient;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await PopulateSessionData();

            await next();

        }

        private async Task PopulateSessionData()
        {
            _sessionData.UserName = _settings.UserName;
            _sessionData.UserID = _settings.UserID;
            _sessionData.TotalAmount = _settings.TotalAmount;

            _sessionData.Team = await _stocksAPIClient.GetSFLTeamScore();

            if (_sessionData.Team.status == "success")
            {
                _sessionData.RemainingAmount = _settings.TotalAmount - _sessionData.Team.teamscore.totalbuyamount;
                if (_sessionData.Team.teamscore.stockinfo.Count > 0)
                {
                   _sessionData.TotalPickCount =(int)_sessionData.Team.teamscore.stockinfo.Select(x => x.Value).Sum(x => x.quantity);
                    _sessionData.AlreadyParticipated = true;
                    _sessionData.ContestDate = _sessionData.Team.teamscore.cdate;
                }
            }
            else
            {
                _sessionData.RemainingAmount = _settings.TotalAmount;
                _sessionData.TotalPickCount = 0;
                _sessionData.AlreadyParticipated = false;
                _sessionData.ContestDate = DateTime.Now.Date.AddDays(_settings.FetchPreviousDays).ToString("dd-MM-yyyy");
            }
        }
    }
}
