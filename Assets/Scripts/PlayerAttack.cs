using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator animator;
    private BoxCollider2D sword;

    private void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.gameObject.GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.animator.SetTrigger("attack");
        }
    }


    void Attack()
    {
        this.sword.enabled = true;
    }

    void StopAttack()
    {
        this.sword.enabled = false;
        this.animator.ResetTrigger("attack");
    }
}
