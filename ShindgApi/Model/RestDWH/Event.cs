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
    [RestDWHEntity("Event", typeof(EventEvents), apiName: "event")]
    public class Event
    {
        /// <summary>
        /// Event Name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Time of the event
        /// </summary>
        public DateTimeOffset Time { get; set; }
        /// <summary>
        /// Event location
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// ARC72 smart contract id
        /// </summary>
        public ulong NftId { get; set; }
        /// <summary>
        /// Event ticket price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Event price in currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Event state: Published|NotPublished
        /// </summary>
        public string State { get; set; }
    }
}
