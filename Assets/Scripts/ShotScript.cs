using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

    public int id;
    public Transform groudCheck;
	private bool grounded = false;
	private float groundRadius = 0.2f;
    [Header("The gameobject on what the player is grounded")]
    public LayerMask whatIsGround;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 10); // 20sec
    }


    // Update is called once per frame
    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(this.groudCheck.position, this.groundRadius, this.whatIsGround);
        if (grounded) 
            Destroy(this.gameObject);
    }
    public void SetPlayerShoot(int id)
    {
        this.id = id;
    }
}
