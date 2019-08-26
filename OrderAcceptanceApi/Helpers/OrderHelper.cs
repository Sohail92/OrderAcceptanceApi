namespace OrderAcceptanceApi.Helpers
{
    /// <summary>
    /// Used for simple logic that is common between both Sell and Buy orders.
    /// </summary>
    internal class OrderHelper
    {
        /// <summary>
        /// For the purpose of this example we are simply checking the currency pair contains a '/' and is 7 in length
        /// Extensions could check against allowed currency pairs e.g. in a DB table.
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <returns></returns>
        internal static bool IsValidCurrencyPair(string currencyPair)
        {
            return currencyPair.Contains("/") && currencyPair.Length == 7;
        }
    }
}
