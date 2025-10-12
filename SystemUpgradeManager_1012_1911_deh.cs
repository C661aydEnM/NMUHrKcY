// 代码生成时间: 2025-10-12 19:11:49
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

// 控制器负责处理系统升级相关的请求
[ApiController]
[Route("[controller]/[action]"]
public class SystemUpgradeManagerController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _updateUrl;
    private readonly string _apiKey;

    // 构造函数注入HttpClient和更新API的密钥和URL
    public SystemUpgradeManagerController(HttpClient httpClient, string apiKey, string updateUrl)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _updateUrl = updateUrl ?? throw new ArgumentNullException(nameof(updateUrl));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }

    // 获取系统升级状态
    [HttpGet]
    public async Task<ActionResult<string>> GetUpgradeStatus()
    {
        try
        {
            var response = await _httpClient.GetAsync(_updateUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException e)
        {
            return StatusCode(500, $"Error fetching upgrade status: {e.Message}");
        }
    }

    // 触发系统升级
    [HttpPost]
    public async Task<ActionResult<string>> TriggerUpgrade()
    {
        try
        {
            var response = await _httpClient.PostAsync(_updateUrl, new StringContent(_apiKey, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException e)
        {
            return StatusCode(500, $"Error triggering upgrade: {e.Message}");
        }
    }

    // 验证升级文件的哈希值
    [HttpPost]
    public ActionResult<string> VerifyUpgradeFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            using (var stream = file.OpenReadStream())
            {
                using (var hash = SHA256.Create())
                {
                    byte[] fileBytes = hash.ComputeHash(stream);
                    var fileHash = BitConverter.ToString(fileBytes).Replace("-", "").ToLower();
                    return Ok(fileHash);
                }
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error verifying file hash: {e.Message}");
        }
    }
}
