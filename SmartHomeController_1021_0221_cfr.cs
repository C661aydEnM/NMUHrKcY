// 代码生成时间: 2025-10-21 02:21:14
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SmartHomeApp.Controllers
{
    // Define a controller for smart home operations.
    [ApiController]
    [Route("[controller]/[action]")]
    public class SmartHomeController : ControllerBase
    {
        private readonly ILogger<SmartHomeController> _logger;
        private readonly ISmartHomeService _smartHomeService;

        // Constructor injection for dependency injection.
        public SmartHomeController(ILogger<SmartHomeController> logger, ISmartHomeService smartHomeService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _smartHomeService = smartHomeService ?? throw new ArgumentNullException(nameof(smartHomeService));
        }

        // GET api/smarthome/lightstatus
        // Returns the status of the smart lights.
        [HttpGet]
        public ActionResult<LightStatus> GetLightStatus()
        {
            try
            {
                // Retrieve the light status from the service layer.
                var lightStatus = _smartHomeService.GetLightStatus();
                return Ok(lightStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving light status");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/smarthome/togglelight
        // Toggles the smart lights on or off.
        [HttpPost]
        public ActionResult ToggleLight([FromBody] ToggleLightRequest request)
        {
            try
            {
                // Validate the request.
                if (request == null)
                {
                    return BadRequest("Request body is missing");
                }

                // Pass the request to the service layer to toggle the lights.
                _smartHomeService.ToggleLight(request.RoomId);
                return Ok("Light toggled successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling light");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Define a request model for toggling lights.
        public class ToggleLightRequest
        {
            public int RoomId { get; set; }
        }

        // Define a model for the light status.
        public class LightStatus
        {
            public int RoomId { get; set; }
            public bool IsOn { get; set; }
        }
    }
}
