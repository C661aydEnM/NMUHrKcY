// 代码生成时间: 2025-09-29 16:17:37
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CampusManagementPlatform
{
    // 定义校园管理平台的启动类
    public class Program
    {
        // 程序入口点，负责构建和运行ASP.NET Core Web应用
        public static void Main(string[] args)
        {
            // 创建web主机构建器，配置服务和中间件
            CreateHostBuilder(args).Build().Run();
        }

        // 创建和配置主机，注册服务和中间件
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    // 定义ASP.NET Core Web应用的配置类
    public class Startup
    {
        // 配置服务容器
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // 添加MVC控制器服务
        }

        // 配置请求管道中间件
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // 将HTTP请求映射到控制器上
            });
        }
    }

    // 示例控制器，用于管理学生信息
    [ApiController]
    [Route("[controller]/[action]