using System;
using System.Net;
using System.Net.Sockets;
using JokerCore;

namespace JokerServer
{
    public class JokerServer
    {
        private TcpListener _server = null;
        private int _port;

        private CombatManager _combatManager;

        public JokerServer()
        {
            _combatManager = new CombatManager();
        }

        public void Launch(int port = 13000)
        {
            Console.WriteLine("initializing Joker Engine...");
            try
            {
                _port = port;
                IPAddress localAddr = IPAddress.Parse("192.168.0.196");

                _server = new TcpListener(localAddr, _port);
                Console.WriteLine("Launching Joker Server...");
                _server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[256];
                string data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = _server.AcceptTcpClient();
                    Console.WriteLine("A player has join the lobby.");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Player 2 sent: {0}", data);

                        // Process the data sent by the client.
                        data = "wesh mg";

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Player 1 sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                _server.Stop();
            }

            Console.WriteLine("\nServer stopped, press any key to close this window...");
            Console.Read();
        }
    }
}