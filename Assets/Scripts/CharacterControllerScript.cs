using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5;
    public float arrowDelay; //time between shots
    public GameObject arrowPrefab;
    public bool arrowFiredRecently = false;
    private Vector2 horizontal;
    private Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Timer.timeStart >= 0 && arrowFiredRecently == false) //Shoot an arrow if they have not recently and the game is not over
        {
            Instantiate(arrowPrefab, rb.position, Quaternion.identity);
            arrowFiredRecently = true;
            Invoke("arrowReset", arrowDelay);
        }

    }

    void FixedUpdate()
    {
        horizontal = new Vector2(Input.GetAxisRaw("Horizontal"), 0); //Get the horizontal movement  direction and translate it to the characters new position on the screen.
        rb.MovePosition(rb.position + (horizontal * speed * Time.deltaTime));
    }
    
    void arrowReset() //reset the arrow buffer so the player can fire again
    {
        arrowFiredRecently = false;
    }
}
