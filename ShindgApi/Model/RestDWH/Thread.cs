using RestDWH.Base.Attributes;
using RestDWH.Elastic.Attributes.Endpoints;
using RestDWHBase.Attributes.Endpoints;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShindgApi.Model.RestDWH
{
    /// <summary>
    /// Discussion threads
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
    [RestDWHEntity("Discussion threads", typeof(ThreadEvents), apiName: "inbox-thread")]
    public class Thread
    {
        /// <summary>
        /// Thread title
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        public DateTimeOffset Time { get; set; }
    }
}
