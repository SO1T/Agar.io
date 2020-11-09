using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float mass = 100;
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
        this.speed -= mass*0.05f;
        Debug.Log("Mass = " + mass);
        Debug.Log("This.mass = " + this.mass);
        Debug.Log("mass/this.mass= " + mass / this.mass);
        float scale = transform.localScale.x + mass/this.mass;
        this.mass += mass;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
