using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float mass = 100;
    public int id = 0;
    public static PlayerManager instance;
    public bool isMoving = false;
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
    void Update()
    {
        if (isMoving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            SendDirection(direction);
        }
    }

    private void SendDirection(Vector2 direction)
    {
        Debug.Log(direction);
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes(direction.x));
        message.AddRange(BitConverter.GetBytes(direction.y));
        ConnectionManager.instance.udpClient.Send(message.ToArray(), message.Count);
    }

    public void Grow(int mass)
    {
        Debug.Log("Grow");
        this.speed -= mass*0.05f;
        Debug.Log("Mass = " + mass);
        Debug.Log("This.mass = " + this.mass);
        Debug.Log("mass/this.mass= " + mass / this.mass);
        float scale = transform.localScale.x + mass/this.mass;
        this.mass += mass;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
