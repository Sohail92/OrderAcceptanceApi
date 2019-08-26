using System;

namespace OrderAcceptanceApi.Models
{
    public class Order
    {
        public Guid id { get { return new Guid(); }}

        /// <summary>
        /// Currency pair in the format currency1/currency2 e.g. GBP/USD.
        /// </summary>
        public string CurrencyPair { get; set; }

        /// <summary>
        /// Amount of the currency buying or selling.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The time the request to buy or sell will expire if not fulfilled.
        /// </summary>
        public DateTime ExpiryDateTime { get; set; }

        /// <summary>
        /// An identifer for a partner
        /// </summary>
        public string PartnerId { get; set; }
    }
}
