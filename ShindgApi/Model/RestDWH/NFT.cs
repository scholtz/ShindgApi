using RestDWH.Base.Attributes;
using RestDWH.Elastic.Attributes.Endpoints;
using RestDWHBase.Attributes.Endpoints;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShindgApi.Model.RestDWH
{
    /// <summary>
    /// Event
    /// </summary>
    [RestDWHEndpointGet]
    [RestDWHEndpointGetById]
    [RestDWHEndpointUpsert]
    [RestDWHEndpointPatch]
    [RestDWHEndpointDelete]
    [RestDWHEndpointProperties]
    [RestDWHEndpointElasticQuery]
    [RestDWHEndpointElasticPropertiesQuery]
    [RestDWHEndpointPost]
    [RestDWHEntity("Event NFTs", typeof(NFTEvents), apiName: "event-nft")]
    public class NFT
    {
        /// <summary>
        /// Event Name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Specific seat at the event
        /// </summary>
        public string Seat { get; set; }
        /// <summary>
        /// Area of the seat
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// NFT price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Event url address
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// NFT state: ON_SALE | NOT_CHECKED_IN | CHECKED_IN | CHECKED_OUT
        /// </summary>
        public string State { get; set; }
    }
}
