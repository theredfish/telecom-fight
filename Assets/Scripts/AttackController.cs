using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    private Animator animator;
    private BoxCollider sword;

    private void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.gameObject.GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }


    
}
