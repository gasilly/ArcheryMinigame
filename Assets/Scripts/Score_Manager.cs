using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public static int score;
    public static int bullseye;
    private Text score_text;
    private Text bullseye_text;
    void Awake()
    {
        score_text = GetComponent<Text>();
        bullseye_text = GetComponent<Text>();
        score = 0;
        bullseye = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Score_Updater();
    }
    void Score_Updater()
    {
        if(this.gameObject.name == "Bullseye")
        {
            bullseye_text.text = "Combo: " + bullseye.ToString();
        }
        else if(this.gameObject.name == "Score")
        {
            score_text.text = "Score: " + score.ToString();
        }
    }
}
