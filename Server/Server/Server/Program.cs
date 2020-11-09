using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UdpClient udpServer = new UdpClient(11000);

            while (true)
            {
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
            }
        }
    }
}
