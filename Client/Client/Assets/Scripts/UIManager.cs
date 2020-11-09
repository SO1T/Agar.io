using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public InputField usernameField;

    public void StartGame()
    {
        ConnectionManager.instance.MakeConnection();

        SendUsername();
         
        PlayerManager.instance.id = ReceiveId();

        DisableMenuUI();

        PlayerManager.instance.isMoving = true;
    }

    private void SendUsername()
    {
        string username = usernameField.text;
        // send data
        ConnectionManager.instance.udpClient.Send(Encoding.ASCII.GetBytes(username), username.Length);
    }

    private int ReceiveId()
    {
        // then receive data
        var receivedData = ConnectionManager.instance.udpClient.Receive(ref ConnectionManager.instance.serverEndPoint);

        Debug.Log("receive data from " + ConnectionManager.instance.serverEndPoint.ToString());
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

    private void DisableMenuUI()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
    }
}
