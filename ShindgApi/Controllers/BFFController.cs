using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShindgApi.Model.Comm;

namespace ShindgApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1")]
    public class BFFController : ControllerBase
    {
        private readonly ILogger<BFFController> _logger;

        public BFFController(ILogger<BFFController> logger)
        {
            _logger = logger;
        }

        [HttpGet("creator-dashboard")]
        public Dashboard CreatorDashboard()
        {
            return new Dashboard()
            {
                Currency = "EUR",
                IssuedNFTs = 100,
                Events = new EventOverview[]
                {
                    new EventOverview()
                    {
                        Currency = "EUR",
                        IssuedNFTs=10,
                        Name= "Prague Meetup 2023",
                        NftsOnSale = 0,
                        Revenue = 100,
                    },
                    new EventOverview()
                    {
                        Currency = "EUR",
                        IssuedNFTs=100,
                        Name= "Prague Meetup 2024",
                        NftsOnSale = 2,
                        Revenue = 1000
                    },
                }
            };
        }

        [HttpGet("event-check-in")]
        public CheckInResult EventCheckIn()
        {
            return new CheckInResult()
            {
                Result = true
            };
        }
        [HttpGet("event-check-out")]
        public CheckInResult EventCheckOut()
        {
            return new CheckInResult()
            {
                Result = false,
                Error = "Not in the event"
            };
        }
    }
}
