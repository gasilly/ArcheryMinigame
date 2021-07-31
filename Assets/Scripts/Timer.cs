using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeStart = 70;
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            textBox.text = Mathf.Round(timeStart).ToString();
        }
    }
}
