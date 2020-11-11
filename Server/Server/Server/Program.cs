using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UdpClient udpServer = new UdpClient(11000);

            var remoteEP = new IPEndPoint(IPAddress.Any, 11000);
          
            remoteEP = ReceiveUsername(udpServer, remoteEP);

            Console.Write("receive data from " + remoteEP.ToString());
            int id = 1;
            udpServer.Send(BitConverter.GetBytes(id), 4, remoteEP); // reply back
            DirectionPositionInteraction(udpServer, remoteEP);
        }

        private static void DirectionPositionInteraction(UdpClient udpServer, IPEndPoint remoteEP)
        {
            Player player = new Player();
            DateTime _nextLoop = DateTime.Now;
            while (true)
            {
                while (_nextLoop < DateTime.Now)
                {
                    Vector2 direction = ReceiveDirection(udpServer, remoteEP);
                    SendPosition(udpServer, direction, player, remoteEP);
                    _nextLoop = _nextLoop.AddMilliseconds(20);
                    if (_nextLoop > DateTime.Now)
                    {
                        // If the execution time for the next tick is in the future, aka the server is NOT running behind
                        Thread.Sleep(_nextLoop - DateTime.Now); // Let the thread sleep until it's needed again.
                    }
                }
            }
        }

        private static IPEndPoint ReceiveUsername(UdpClient udpServer, IPEndPoint remoteEP)
        {
            var data = udpServer.Receive(ref remoteEP); // listen on port 11000
            string value;
            try
            {
                value = Encoding.ASCII.GetString(data, 0, data.Length); // Convert the bytes to a string
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
            Console.WriteLine(value);
            return remoteEP;
        }

        private static Vector2 ReceiveDirection(UdpClient udpServer, IPEndPoint remoteEP)
        {
                var directionBytes = udpServer.Receive(ref remoteEP); // listen on port 11000
                Console.Write("receive data from " + remoteEP.ToString());
                float x, y;
                try
                {
                    x = BitConverter.ToSingle(directionBytes, 0); // Convert the bytes to a string
                    y = BitConverter.ToSingle(directionBytes, 4);
                    Console.WriteLine($"x = {x}; y = {y}");
                    return new Vector2(x, y);
                }
                catch
                {
                    throw new Exception("Could not read value of type '2 floats'!");
                }
            
        }

        private static void SendPosition(UdpClient udpServer, Vector2 direction, Player player, IPEndPoint remoteEP)
        {
            Vector2 position = player.GetPosition(direction);
            Console.WriteLine(position);
            List<byte> message = new List<byte>();
            message.AddRange(BitConverter.GetBytes(position.X));
            message.AddRange(BitConverter.GetBytes(position.Y));
            udpServer.Send(message.ToArray(), message.Count, remoteEP);
        }
    }
}
