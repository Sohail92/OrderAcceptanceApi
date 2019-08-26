using OrderAcceptanceApi.Models;

namespace OrderAcceptanceApi.Helpers
{
    internal static class SellOrderHelper
    {
        /// <summary>
        /// In this method we validate the sell order. For the purpose of this task we are simply checking the currency pair is valid
        /// and the maximum selling price has a value.
        /// This could be extended to check the Expiry Date hasnt passed or the partner id is valid etc.
        /// </summary>
        internal static bool IsSellOrderValid(SellOrder order)
        {
            bool isSellOrderValid = false;

            if (OrderHelper.IsValidCurrencyPair(order.CurrencyPair))
                if(order.MaximumSellPrice > 0)
                    isSellOrderValid = true;

            return isSellOrderValid;
        }
    }
}
