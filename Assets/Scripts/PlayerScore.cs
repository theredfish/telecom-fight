using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
    public int score = 0;
	// Use this for initialization
	void Start () {
        score = 0;
	}

    public void getOneKill()
    {
        score++;
    }

    public int getScore()
    {
        return this.score;
    }
}
