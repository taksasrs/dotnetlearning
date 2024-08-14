using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TestAPI.Data.Dtos;
using TestAPI.Helpers;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<DataHub> _hub;
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger, IHubContext<DataHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

       [HttpGet("send/graph1")]
        public IActionResult GetTest(string chat, string author)
        {
            var data = new MessageDto();

            data.message = chat;

            data.author = author;

            Console.WriteLine("qwertyuio",data.dateTime);

            var result = _hub.Clients.All.SendAsync("chartStation1", data);
            return Ok(new { Status = "Send To Graph 1 Completed" });
        }

        [HttpGet("send/graph2")]
        public IActionResult GetTest2(string chat, string author)
        {
            var data = new MessageDto();

            data.message = chat;

            data.author = author;

            var result = _hub.Clients.All.SendAsync("chartStation2", data);
            return Ok(new { Status = "Send To Graph 2 Completed" });
        }
    }
}