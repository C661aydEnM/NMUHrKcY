// 代码生成时间: 2025-10-07 02:14:23
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AiVersionManagement
{
    // Controller for AI version management
    [ApiController]
    [Route("api/ai/version")]
    public class AiVersionController : ControllerBase
    {
        private readonly ILogger<AiVersionController> _logger;

        // Constructor injection for logger
        public AiVersionController(ILogger<AiVersionController> logger)
        {
            _logger = logger;
        }

        // Get all AI model versions
        [HttpGet]
        public ActionResult<List<string>> GetAllVersions()
        {
            try
            {
                // Assuming versions are stored in a file named 'versions.txt'
                string versionsPath = "versions.txt";
                if (!File.Exists(versionsPath))
                {
                    return NotFound("No versions file found.");
                }

                var versions = File.ReadAllLines(versionsPath).ToList();
                return Ok(versions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting AI versions.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Add a new AI model version
        [HttpPost]
        public ActionResult<string> AddVersion([FromBody] string version)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(version))
                {
                    return BadRequest("Version cannot be null or whitespace.");
                }

                // Assuming versions are stored in a file named 'versions.txt'
                string versionsPath = "versions.txt";
                if (File.Exists(versionsPath))
                {
                    File.AppendAllText(versionsPath, $"
{version}");
                }
                else
                {
                    File.WriteAllText(versionsPath, version);
                }

                return Ok(version);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new AI version.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
