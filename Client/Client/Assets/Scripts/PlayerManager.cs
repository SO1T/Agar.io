﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public int mass = 1;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    public void Grow(int mass)
    {
        Debug.Log("Grow");
        this.mass += mass;
        float scale = 0.1f * mass;
        transform.localScale += new Vector3(scale, scale, scale);
    }
}
