using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShindgApi.Model.Comm
{
    public class EventOverview
    {
        /// <summary>
        /// Event name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number of issued nfts for the specific event
        /// </summary>
        public int IssuedNFTs { get; set; }
        /// <summary>
        /// Count of NFTs for this event which were not sold to the public yet
        /// </summary>
        public int NftsOnSale { get; set; }
        /// <summary>
        /// Revenue
        /// </summary>
        public decimal Revenue { get; set; }
        /// <summary>
        /// Currency of the revenue
        /// </summary>
        public string Currency { get; set; }
    }
}
