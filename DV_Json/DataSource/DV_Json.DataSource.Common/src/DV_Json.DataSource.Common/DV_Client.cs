using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace DV_Json.DataSource.Common
{
    public class DV_Client
    {
        private readonly string server;
        private readonly int port;

        public DV_Client(string server, int port)
        {
            this.server = server;
            this.port = port;
        }

        public async Task SendMessageToServerTaskAsync(string message)
        {
            await ConnectAsync(server, port, message);
        }

        private async Task ConnectAsync(String server, int port, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(server, port);

                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    //  Stream stream = client.GetStream();

                    using (NetworkStream stream = client.GetStream())
                    {

                        // Send the message to the connected TcpServer. 
                        stream.Write(data, 0, data.Length);

                        Console.WriteLine("Sent: {0}", message);

                        // Receive the TcpServer.response.

                        // Buffer to store the response bytes.
                        data = new Byte[256];

                        // String to store the response ASCII representation.
                        String responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("Received: {0}", responseData);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
