using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NNetwork.API.Dtos;
using NNetwork.API.MappingProfiles;
using NNetwork.Domain.Services;
using NNetwork.KerasApplication.Networks;

namespace NNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly ILogger<NetworkController> _logger;
        private readonly INetworkService _networkService;
        private readonly IMapper _mapper;

        public NetworkController(ILogger<NetworkController> logger,
            INetworkService networkService)
        {
            _logger = logger;
           _networkService = networkService;
            var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile(new NetworkMapper()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var networks = await _networkService.GetAll();
            return Ok(networks);
        }

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetInfo([FromRoute] Guid id)
        {
            var networkInfo = await _networkService.GetNetworkAllInfo(id);

            return Ok(networkInfo);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NetworkInitializerDto networkDto)
        {
            var model = _mapper.Map<NetworkInitializationModel>(networkDto);
            var result = await _networkService.CreateNetworkAsync(model);
            return Ok(result);
        }

        [HttpPost("predict/{networkId}")]
        public async Task<IActionResult> Classify([FromRoute] Guid networkId)
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            //var imgByteArray = Convert.FromBase64String(image.File);
            //System.IO.File.WriteAllBytes(fileName, imgByteArray);

            using (FileStream outputFileStream = new FileStream(fileName, FileMode.Create))
            {
                file.OpenReadStream().CopyTo(outputFileStream);
            }

            var result = await _networkService.PredictImage(networkId, fileName);

            System.IO.File.Delete(fileName);

            return Ok(result);
        }
    }
}
