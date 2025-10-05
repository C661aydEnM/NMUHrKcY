// 代码生成时间: 2025-10-06 01:48:24
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenCover;
using OpenCover.Framework;
using OpenCover.Framework.Model;

namespace CoverageAnalysis
{
    /// <summary>
    /// Class responsible for performing code coverage analysis.
    /// </summary>
    public class CoverageAnalysisService
    {
        private readonly ILogger<CoverageAnalysisService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverageAnalysisService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public CoverageAnalysisService(ILogger<CoverageAnalysisService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Analyzes the code coverage for the specified test run.
        /// </summary>
        /// <param name="testResults">The test results.</param>
        /// <param name="coverageResults">The coverage results.</param>
        public void AnalyzeCoverage(FileInfo[] testResults, FileInfo[] coverageResults)
        {
            if (testResults == null || coverageResults == null)
            {
                _logger.LogError("Test results or coverage results cannot be null.");
                throw new ArgumentException("Test results or coverage results cannot be null.");
            }

            var session = new Session("CoverageAnalysis", new FileSystemStorage());
            session.RegisterSymbol(".*\Test\.dll"); // Register test assemblies
            session.RegisterSymbol(".*\YourProject\.dll"); // Register project assemblies
            session.Filter += (file) => !file.FullName.Contains("YourProject.Tests"); // Exclude test assemblies from coverage

            foreach (var result in testResults)
            {
                if (!result.Exists)
                {
                    _logger.LogWarning($"Test result file {result.FullName} does not exist.");
                    continue;
                }

                session.RegisterFile(result.FullName);
            }

            foreach (var result in coverageResults)
            {
                if (!result.Exists)
                {
                    _logger.LogWarning($"Coverage result file {result.FullName} does not exist.");
                    continue;
                }

                session.ReadProbeData(result.FullName);
            }

            var coverage = session.GetCoverageData();
            _logger.LogInformation($"Total coverage: {coverage.Percentage}%");

            // Save coverage results to file
            var coverageReport = new CoverageReport(session);
            coverageReport.Save("coverage.xml");

            _logger.LogInformation("Coverage analysis completed successfully.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddTransient<CoverageAnalysisService>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var coverageAnalysisService = serviceProvider.GetService<CoverageAnalysisService>();
                if (coverageAnalysisService == null)
                {
                    Console.WriteLine("Coverage analysis service is not registered.");
                    return;
                }

                var testResults = new FileInfo[] { new FileInfo("path\	o\	est\results.xml") };
                var coverageResults = new FileInfo[] { new FileInfo("path\	o\coverage\results.xml\) };
                coverageAnalysisService.AnalyzeCoverage(testResults, coverageResults);
            }
        }
    }
}
