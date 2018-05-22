using McMaster.Extensions.CommandLineUtils;
using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TLSTest
{
    public sealed class TLSTest
    {
        public static Task Main(string[] args) => CommandLineApplication.ExecuteAsync<TLSTest>(args);

        [Option(Description ="IPAddress:Port i.e. 127.0.0.1:443")]
        public string IPAddressPort { get; }

        private string IPAddress => IPAddressPort.Substring(0, IPAddressPort.IndexOf(":"));
        private int Port => Int32.Parse(IPAddressPort.Substring(IPAddressPort.IndexOf(":") + 1));

        private async Task OnExecuteAsync()
        {
            var task1 = TryTLS(System.Security.Authentication.SslProtocols.Tls12, IPAddress, Port,1);
            var task2 = TryTLS(System.Security.Authentication.SslProtocols.Tls12, IPAddress, Port,2);
            await Task.WhenAll(task1, task2);           
        }

        private  async Task TryTLS(System.Security.Authentication.SslProtocols protocol,string ipAddress,int port,int connectionNumber)
        {
            Console.WriteLine($"Attemping {protocol.ToString()} connection{connectionNumber} to {ipAddress}:{port}");
            try
            {
                await ConnectSSLAsync(ipAddress, port, protocol);
                Console.WriteLine(DateTime.UtcNow + " " + protocol.ToString() + $" Success{ connectionNumber}");
            }
            catch (Exception)
            {

                Console.WriteLine(protocol.ToString() + " Failed");
            }
        }

        private  async Task ConnectSSLAsync(string host, int port, System.Security.Authentication.SslProtocols sslProtocols)
        {
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port);
            var tcpStream = tcpClient.GetStream();
            var sslStream = new SslStream(tcpStream, false, (sender, certificate, chain, errors) =>
            {
                return true;
            });
            await sslStream.AuthenticateAsClientAsync(host, null, sslProtocols, false);
            //   tcpClient.Dispose();
        }
    }
}