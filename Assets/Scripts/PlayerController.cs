﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool isAlive = true;

    private Rigidbody2D rb2d;

    private Animator animator;
    private BoxCollider2D sword;

    private GameObject[] spawnPoints;

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    bool grounded = false;
    public Transform groudCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

	[Header("Player ID (tmp must be set by the gameManager")]
	public int id;



    // Use this for initialization
    void Start () {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.GetComponentsInChildren<BoxCollider2D>()[1];
        if (!this.sword.tag.Equals("sword"))
            Debug.Log("Be carreful GetComponentsInChildren NOT working");

        this.spawnPoints = GameObject.FindGameObjectsWithTag("respawnPoint");
        this.rb2d = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Jump
        if (grounded && Input.GetButtonDown("JumpP" + id) && isAlive)
        {
            this.rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(this.groudCheck.position, this.groundRadius, this.whatIsGround);

        if (isAlive)
        {
            float move = Input.GetAxis("HorizontalP" + id);
            this.rb2d.velocity = new Vector2(move * maxSpeed, this.rb2d.velocity.y);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

            if (Input.GetButtonDown("AttackP" + id))
            {
                this.animator.SetTrigger("attack");
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

    void DesactivateControl()
    {
       this.isAlive = false;
    }

    void ActivateControl()
    {
        this.isAlive = true;
    }

    void Respawn()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        this.gameObject.transform.position = spawnPoints[randomSpawnPoint].transform.position;
        this.animator.ResetTrigger("dead");

    }
}
