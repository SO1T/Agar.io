using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public UdpClient udpClient = new UdpClient();
    public IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);//server endpoint

    public static ConnectionManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void MakeConnection()
    {
        udpClient.Connect(serverEndPoint);
    }


    public void SendDirection(Vector2 direction)
    {
        Debug.Log(direction);
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes(direction.x));
        message.AddRange(BitConverter.GetBytes(direction.y));
        udpClient.Send(message.ToArray(), message.Count);
    }


    public void SendUsername(string username)
    {
        udpClient.Send(Encoding.ASCII.GetBytes(username), username.Length);
    }

    public int ReceiveId()
    {
        // then receive data
        var receivedData = udpClient.Receive(ref serverEndPoint);

        Debug.Log("receive data from " + serverEndPoint.ToString());
        int id = 0;
        if (receivedData.Length > 3)
        {
            // If there are unread bytes
            id = BitConverter.ToInt32(receivedData, 0); // Convert the bytes to an int
        }
        else
        {
            throw new Exception("Could not read value of type 'int'!");
        }

        Debug.Log(id);
        return id;
    }

    public Vector2 ReceivePosition()
    {
        // then receive data
        int packetNumber;
        Vector2 position = ReceivePacket(out packetNumber);
        while (packetNumber != 0)
        {
            FoodManager.instance.InstantiateFood(position);
            position = ReceivePacket(out packetNumber);
        }
        Debug.Log(position);
        return position;
    }

    private Vector2 ReceivePacket(out int packetNumber)
    {
        var receivedData = udpClient.Receive(ref serverEndPoint);

        Debug.Log("receive data from " + serverEndPoint.ToString());
        Vector2 position;
        packetNumber = BitConverter.ToInt32(receivedData, 0);
        position.x = BitConverter.ToSingle(receivedData, 4);
        position.y = BitConverter.ToSingle(receivedData, 8);
        return position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
