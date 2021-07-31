using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Bonus : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "arrow")
        {
            Timer.timeStart += 5;
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }
}
