using RestDWH.Base.Attributes;
using RestDWH.Elastic.Attributes.Endpoints;
using RestDWHBase.Attributes.Endpoints;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShindgApi.Model.RestDWH
{
    /// <summary>
    /// Discussion thread messages
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
    [RestDWHEntity("Discussion thread messages", typeof(MessageEvents), apiName: "inbox-message")]
    public class Message
    {
        public string ThreadId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        public DateTimeOffset Time { get; set; }
    }
}
