using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public int minutes = 2;
    public int second = 0;
    public Text countdownText;
	// Use this for initialization
	void Start () {
        updateText();
        StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        updateText();
        if (minutes < 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text=("Times Up !");
        }
	}

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (second == 0)
            {
                second = 59;
                minutes--;
            }
            else
            {
                second--;
            }
        }
    }

    void updateText()
    {
        if (second == 0)
        {
            countdownText.text = (minutes + ":00");
        }
        countdownText.text = (minutes + ":" + second);
    }
}
