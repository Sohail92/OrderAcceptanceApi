namespace OrderAcceptanceApi.Models
{
    public class BuyOrder : Order
    {
        /// <summary>
        /// The minimum exchange rate that the buyer is willing to accept.
        /// </summary>
        public decimal MinAcceptedExchangeRate { get; set; }

    }
}
