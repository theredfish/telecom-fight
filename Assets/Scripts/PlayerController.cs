using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	private float _gravity = 1.0f;
	private CharacterController controller;
	private Vector2 movement = Vector2.zero;
	private bool isForward = true;
	private bool isBackward = false;
    private Animator animator;
    private BoxCollider sword;
    private bool isAlive = true;
    private GameObject[] spawnPoints;
    private static float MAXMOVEMENTY = -35.0f;

    [Header("The jump force")]
	public float jumpForce = 5f;

	[Header("The horizontal move speed")]
	public float moveSpeed = 5f;

	[Header("Player ID (tmp must be set by the gameManager")]
	public int id;



    // Use this for initialization
    void Start () {
		this.controller = GetComponent<CharacterController> ();
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.gameObject.GetComponentInChildren<BoxCollider>();
        this.spawnPoints = GameObject.FindGameObjectsWithTag("respawnPoint");
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            // First we get the horizontal movement
            movement.x = Input.GetAxis("HorizontalP" + id) * this.moveSpeed;

            if(id == 2)
            Debug.Log(this.controller.isGrounded + " " + id);

            // Then we get the vertical movement
            if (controller.isGrounded)
            {
                movement.y = 0;

                // Jump
                if (Input.GetButtonDown("JumpP" + id))
                {
                    movement.y = jumpForce;
                }

            }
            else
            {
                if(movement.y>=MAXMOVEMENTY)
                    movement.y -= _gravity;
            }

            if (Input.GetButtonDown("AttackP" + id))
            {
                this.animator.SetTrigger("attack");
            }

            Walk(movement);

           
        }
    }

	void Walk(Vector2 movement) {
        this.controller.Move(movement * Time.deltaTime);
		float axis = Input.GetAxis("HorizontalP" + id);

		if (axis < 0) {
			if (isForward) {
				transform.Rotate(new Vector2(0, -180));
				isForward = false;
				isBackward = true;
			}

		} else if (axis > 0) {
			if (isBackward) {
				transform.Rotate (new Vector3 (0, 180));
				isForward = true;
				isBackward = false;
			}
		} else
        {
            //idle animation
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
