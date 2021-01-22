using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plathorm : MonoBehaviour
{
    public Manager manager;
    public float speed;
    public Rigidbody2D rb;
    
    void Start()
    {
        
    }

    void Update()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = Vector2.right * (direction * speed);
    }

    public void SetManager(Manager nice)
    {
        manager = nice;
    }
}
