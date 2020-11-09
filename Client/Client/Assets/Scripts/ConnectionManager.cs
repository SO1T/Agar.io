using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
