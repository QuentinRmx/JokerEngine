using System;
using System.Net.Sockets;
using System.Text;

namespace JokerCore
{
    public class JokerClient : IDisposable
    {
        private TcpClient _tcpClient;

        private NetworkStream _inOutStream;

        public bool IsConnected => (_tcpClient != null) && _tcpClient.Connected;

        public JokerClient(string server, int port = 13000)
        {
            Connect(server, port);
        }


        /// <summary>
        /// Connect to the given IP address and port and returns the connection state.
        /// </summary>
        /// <param name="server">IP address to connect to.</param>
        /// <param name="port">Port of the server.</param>
        /// <returns>True if the connection has been successfully opened.</returns>
        public bool Connect(string server, int port)
        {
            try
            {
                // Create a TcpClient.
                _tcpClient = new TcpClient(server, port);
                // Get a client stream for reading and writing.
                _inOutStream = _tcpClient.GetStream();
                return IsConnected;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return false;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Close everything.
            _inOutStream?.Close();
            _tcpClient?.Close();
            _tcpClient?.Dispose();
            _inOutStream?.Dispose();
        }

        public string Send(string message)
        {
            // String to store the response ASCII representation.
            string responseData = string.Empty;
            try
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = Encoding.ASCII.GetBytes(message);
                // Send the message to the connected TcpServer.
                _inOutStream.Write(data, 0, data.Length);
                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new byte[256];
                Console.WriteLine("Sent: {0}", message);

                // Read the first batch of the TcpServer response bytes.
                int bytes = _inOutStream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return responseData;
        }
    }
}