using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderAcceptanceApi.Helpers;
using OrderAcceptanceApi.Interfaces;
using OrderAcceptanceApi.Models;

namespace OrderAcceptanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // DB Logger Interface - the repo is injected with Constructor injection using the built in .NET Core Mechanism.
        private readonly ILogToDB _dbLogger;

        public OrderController(ILogToDB dbLogger)
        {
            _dbLogger = dbLogger;
        }

        [HttpPost]
        [Route("AddBuyOrder")]
        public async Task<ActionResult<string>> AddBuyOrderAsync(BuyOrder order)
        {
            string result = string.Empty;
            try
            {
                // validate the buy order here before deciding to accept it. E.g. check if the currency pair is valid. 
                if (BuyOrderHelper.IsBuyOrderValid(order))
                {
                    await _dbLogger.LogBuyOrderToDB(order);
                    return $"Buy order: {order.CurrencyPair} {order.Amount} {order.MinAcceptedExchangeRate} {order.ExpiryDateTime} successfully added to CarbBank systems";
                }
            }
            catch (Exception ex)
            {
                _ = _dbLogger.LogError(ex);
                result = "An error occurred whilst processing your request. This has been logged with CarbBank, please try again later.";
            }
            return result;
        }

        [HttpPost]
        [Route("AddSellOrder")]
        public async Task<ActionResult<string>> AddSellOrderAsync(SellOrder order)
        {
            string result = string.Empty;
            try
            {
                // validate the sell order here before deciding to accept it. E.g. check if the currency pair is valid. 
                if (SellOrderHelper.IsSellOrderValid(order))
                {
                    await _dbLogger.LogSellOrderToDB(order);
                    result = $"Sell order: {order.CurrencyPair} {order.Amount} {order.MaximumSellPrice} {order.ExpiryDateTime} successfully added to CarbBank systems";
                }
            }
            catch (Exception ex)
            {
                _ = _dbLogger.LogError(ex);
                result = "An error occurred whilst processing your request. This has been logged with CarbBank, please try again later.";
            }
            return result;
        }
    }
}
