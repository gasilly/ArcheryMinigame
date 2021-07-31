using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    public float timeToLive;
    public bool arrowFiredRecently = false;
    public Rigidbody2D rb;
    public float startTime;
    void Start()
    {
        startTime = Time.time; 
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        Destroy(gameObject, timeToLive);
    }
}
