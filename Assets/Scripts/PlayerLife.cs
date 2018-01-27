using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("sword"))
        {
            Debug.Log("hit");
        }
    }

}
