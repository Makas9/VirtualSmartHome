using Microsoft.AspNetCore.Mvc;
using Devicedemo.Models;
using Devicedemo.Data;

namespace Devicedemo.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepo _repository;

        public DevicesController(IDeviceRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<Device> GetDevice()
        {
            var device = _repository.GetDevice();

            return Ok(device);
        }

        [HttpPost]
        public ActionResult<Device> UpdateDevice(Device device)
        {
            _repository.UpdateDevice(device);
            return Ok(device);
        }

    }
}