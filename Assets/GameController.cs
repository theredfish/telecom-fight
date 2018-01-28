using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject[] players;
    private bool[] deads;

    // Use this for initialization
    void Start () {
       players = GameObject.FindGameObjectsWithTag("Player");
       deads = new bool[players.Length];
       for (int i = 0; i < players.Length; i++)
            deads[i] = false;

    }

    public void FinalDead(GameObject player)
    {
        GameObject lastAlive = null;
        int alive = 0 ;
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].Equals(player))
            {
                deads[i] = true;
                player.SetActive(false);
            }
            if (!deads[i])
            {
                alive++;
                lastAlive = players[i];
            }
        }
        
        if(alive <= 0)
        {
            Egalite();
            return;
        }
        else if(alive <= 1)
        {
            Winner(lastAlive);
            return;
        }
    }

    void Egalite()
    {
        Debug.Log("Egalite");
    }

    void Winner(GameObject lastAlive)
    {
        Debug.Log("Gagnant : " + lastAlive.GetComponent<PlayerController>().id);
    }

    void ReLaunch()
    {

    }

}
