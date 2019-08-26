using OrderAcceptanceApi.Models;
using System;
using System.Threading.Tasks;

namespace OrderAcceptanceApi.Interfaces
{
    public interface ILogToDB
    {
        Task LogBuyOrderToDB(BuyOrder order);

        Task LogSellOrderToDB(SellOrder order);

        Task LogError(Exception ex);
    }
}
