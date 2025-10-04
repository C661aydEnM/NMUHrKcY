// 代码生成时间: 2025-10-05 02:52:27
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

// 一个简单的模拟闪电网络节点的C#程序
namespace LightningNetworkDemo
{
    /// <summary>
    /// 闪电网络节点类，负责处理入站和出站连接以及交易
    /// </summary>
    public class LightningNode
    {
        private TcpListener listener;
        private bool isRunning = true;

        /// <summary>
        /// 构造函数，初始化TCP监听器
        /// </summary>
        /// <param name="port">节点监听的端口号</param>
        public LightningNode(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// 启动节点，开始监听连接
        /// </summary>
        public void Start()
        {
            listener.Start();
            Console.WriteLine($"Node started and listening on port {listener.LocalEndpoint}.");
            ListenForClients();
        }

        /// <summary>
        /// 监听来自其他节点的连接
        /// </summary>
        private async void ListenForClients()
        {
            try
            {
                while (isRunning)
                {
                    var clientTask = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("You've got a connection!");
                    var client = clientTask;
                    Task.Run(() => HandleClient(client));
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理来自客户端的请求
        /// </summary>
        /// <param name="client">TCP客户端</param>
        private async void HandleClient(TcpClient client)
        {
            try
            {
                using (client)
                {
                    NetworkStream stream = client.GetStream();
                    var buffer = new byte[1024];
                    int bytesRead;

                    // 读取来自客户端的数据
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        Console.WriteLine($"Received message: {message}");

                        // 此处可以添加处理消息的代码，例如交易验证等
                        // ...
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
        }

        /// <summary>
        /// 停止节点，停止监听连接
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            listener.Stop();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var node = new LightningNode(8000);
            node.Start();

            Console.WriteLine("Press any key to exit...");
            await Task.Run(() => Console.ReadKey());

            node.Stop();
        }
    }
}
