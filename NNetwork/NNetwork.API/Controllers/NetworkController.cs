using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NNetwork.API.Dtos;
using NNetwork.Domain.Services;

namespace NNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly ILogger<NetworkController> _logger;
        private readonly INetworkService _networkService;

        public NetworkController(ILogger<NetworkController> logger,
        INetworkService networkService)
        {
            _logger = logger;
            _networkService = networkService;
        }

        [HttpPost]
        public Task<IActionResult> Create([FromBody] NetworkInitializerDto networkDto)
        {
            return null;
        }
    }
}
