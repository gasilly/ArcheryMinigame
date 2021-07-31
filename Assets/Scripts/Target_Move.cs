using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Move : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public Vector2 direction = new Vector2(1,0);
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (direction * speed * Time.deltaTime));
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        direction.x = direction.x * -1;
    }
    */

}
