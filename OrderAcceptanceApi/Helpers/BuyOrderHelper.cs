using OrderAcceptanceApi.Models;

namespace OrderAcceptanceApi.Helpers
{
    internal class BuyOrderHelper
    {
        /// <summary>
        /// In this method we validate the buy order. For the purpose of this task we are simply checking the currency pair is valid
        /// and the minimum accepted exchange rate has a value.
        /// This could be extended to check the Expiry Date hasnt passed or the partner id is valid etc.
        /// </summary>
        internal static bool IsBuyOrderValid(BuyOrder order)
        {
            bool isBuyOrderValid = false;

            if (OrderHelper.IsValidCurrencyPair(order.CurrencyPair))
                if (order.MinAcceptedExchangeRate > 0)
                    isBuyOrderValid = true;

            return isBuyOrderValid;
        }
    }
}
