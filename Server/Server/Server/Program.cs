using System;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UdpClient udpServer = new UdpClient(11000);

            var remoteEP = new IPEndPoint(IPAddress.Any, 11000);
            var data = udpServer.Receive(ref remoteEP); // listen on port 11000
            Console.Write("receive data from " + remoteEP.ToString());
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
            int id = 1;
            udpServer.Send(BitConverter.GetBytes(id), 4, remoteEP); // reply back
            while (true)
            {
                var directionBytes = udpServer.Receive(ref remoteEP); // listen on port 11000
                Console.Write("receive data from " + remoteEP.ToString());
                float x, y;
                try
                {
                    x = BitConverter.ToSingle(directionBytes, 0); // Convert the bytes to a string
                    y = BitConverter.ToSingle(directionBytes, 4);
                    Console.WriteLine($"x = {x}; y = {y}");
                }
                catch
                {
                    throw new Exception("Could not read value of type '2 floats'!");
                }
            }
        }
    }
}
