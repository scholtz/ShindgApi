using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShindgApi.Model.Comm
{
    public class Dashboard
    {
        /// <summary>
        /// Sum of all issued NFTs
        /// </summary>
        public int IssuedNFTs { get; set; }
        /// <summary>
        /// Sum of revenue for all events
        /// </summary>
        public decimal Revenue { get; set; }
        /// <summary>
        /// Currency of the revenue
        /// </summary>
        public string Currency { get; set;}
        /// <summary>
        /// List of user events
        /// </summary>
        public EventOverview[] Events { get; set; }
    }
}
