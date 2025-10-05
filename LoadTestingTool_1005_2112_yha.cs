// 代码生成时间: 2025-10-05 21:12:54
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// A simple load testing tool to perform HTTP requests against a specified URL.
/// </summary>
public class LoadTestingTool
{
    private readonly HttpClient _httpClient;
    private readonly string _targetUrl;

    /// <summary>
    /// Initializes a new instance of the LoadTestingTool class.
    /// </summary>
    /// <param name="targetUrl">The URL to send HTTP requests to.</param>
    public LoadTestingTool(string targetUrl)
    {
        _httpClient = new HttpClient();
        _targetUrl = targetUrl;
    }

    /// <summary>
    /// Performs a load test by sending multiple HTTP GET requests to the target URL.
    /// </summary>
    /// <param name="concurrency">The number of concurrent requests.</param>
    /// <param name="totalRequests">The total number of requests to send.</param>
    /// <returns>A list of response times in milliseconds.</returns>
    public async Task<List<double>> PerformLoadTest(int concurrency, int totalRequests)
    {
        List<double> responseTimes = new List<double>();
        var tasks = new List<Task>();

        for (int i = 0; i < totalRequests; i++)
        {
            tasks.Add(SendRequest(concurrency, responseTimes));
        }

        await Task.WhenAll(tasks);
        return responseTimes;
    }

    /// <summary>
    /// Sends a single HTTP GET request to the target URL and records the response time.
    /// </summary>
    /// <param name="concurrency">The number of concurrent requests.</param>
    /// <param name="responseTimes">A list to store response times.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SendRequest(int concurrency, List<double> responseTimes)
    {
        try
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var response = await _httpClient.GetAsync(_targetUrl);
            response.EnsureSuccessStatusCode();
            stopwatch.Stop();
            responseTimes.Add(stopwatch.Elapsed.TotalMilliseconds);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request failed: {e.Message}");
        }
    }
}
