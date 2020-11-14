using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
namespace Server
{
    public interface IGameManager
    {
        public void StartGame();
        public void UpdateField(UdpClient udpServer, IPEndPoint remoteEP);
    }
    public class GameManager : IGameManager
    {
        public static ConcurrentBag<Player> players = new ConcurrentBag<Player>();
        private readonly IIdGenerator idGenerator;
        private readonly IConnectionManager connectionManager;
        public GameManager(IIdGenerator idGenerator, IConnectionManager connectionManager)
        {
            this.idGenerator = idGenerator;
            this.connectionManager = connectionManager;
        }
        public void StartGame()
        {
            UdpClient udpServer = new UdpClient(11000);

            var remoteEP = new IPEndPoint(IPAddress.Any, 11000);

            remoteEP = connectionManager.ReceiveUsername(udpServer, remoteEP);

            Console.Write("receive data from " + remoteEP.ToString());
            int id = idGenerator.GetId();
            udpServer.Send(BitConverter.GetBytes(id), 4, remoteEP); // reply back
            players.Add(new Player { Id = id, radius = 1 });
            UpdateField(udpServer, remoteEP);
        }
        public void UpdateField(UdpClient udpServer, IPEndPoint remoteEP)
        {
            DateTime _nextLoop = DateTime.Now;

            Thread thread = new Thread(() => connectionManager.SendFoodPosition(udpServer, remoteEP));
            thread.Start();
            while (true)
            {
                while (_nextLoop < DateTime.Now)
                {
                    foreach (var player in players)
                    {
                        UpdatePlayerPosition(udpServer, remoteEP, player);
                    }
                    _nextLoop = _nextLoop.AddMilliseconds(20);
                    if (_nextLoop > DateTime.Now)
                    {
                        // If the execution time for the next tick is in the future, aka the server is NOT running behind
                        Thread.Sleep(_nextLoop - DateTime.Now); // Let the thread sleep until it's needed again.
                    }
                }
            }
        }

        private void UpdatePlayerPosition(UdpClient udpServer, IPEndPoint remoteEP, Player player)
        {
            Vector2 direction = connectionManager.ReceiveDirection(udpServer, remoteEP);
            connectionManager.SendPosition(udpServer, direction, player, remoteEP);
        }
    }
}
