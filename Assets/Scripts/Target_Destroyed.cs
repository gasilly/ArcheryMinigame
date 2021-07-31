using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Destroyed : MonoBehaviour
{
    private bool missed = false;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        if(this.tag == "stationary target"){ //After the time has past the target has been missed and should despawn
            Invoke("TargetMissed", Game_Board_Manager.stationaryDespawnTime);
        }
        else if(this.tag == "moving target"){ 
            Invoke("TargetMissed", Game_Board_Manager.movingDespawnTime);
        }
        else if(this.tag == "bonus target"){ 
            Invoke("TargetMissed", Game_Board_Manager.bonusDespawnTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other) //If the target collides with something check if its an arrow and if so what region was hit
    {
        if (other.tag != "arrow" || missed == true) 
        {
            return;
        }
        if (this.tag == "bonus target"){
            Timer.timeStart += 5;
        }
        CancelInvoke();
        ScoreCalculation();
        anim.SetBool("targetDestroyed", true); //Remove the target and the arrow
        Destroy(other.gameObject);
    }

    void TargetMissed()
    {
        anim.SetBool("targetMissed", true); //Use the retreat animation
        missed = true;
        if(this.tag != "bonus target"){
            Score_Manager.bullseye = 0;
        }
    }

    void ScoreCalculation()
    {
        if(this.tag != "bonus target"){
            Score_Manager.bullseye++;
            Score_Manager.score += 25 * Score_Manager.bullseye;
        }
    }

    void EnableDestruction() //Enable the hit box for the target
    { 
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void DisableDestruction(){ //Disable the hitbox for the target
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
