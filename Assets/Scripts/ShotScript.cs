using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

    public int id;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 10); // 20sec
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetPlayerShoot(int id)
    {
        this.id = id;
    }
}
