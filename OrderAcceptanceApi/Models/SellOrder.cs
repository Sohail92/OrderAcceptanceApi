namespace OrderAcceptanceApi.Models
{
    public class SellOrder : Order
    {
        /// <summary>
        /// The maximum price that the customer would sell the second currency in the currency pair.
        /// </summary>
        public decimal MaximumSellPrice { get; set; }
    }
}
