using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
    private Animator animator;
    private BoxCollider2D sword;
    private GameObject[] spawnPoints;
	private bool isAlive = true;
	private bool facingRight = true;
	private bool grounded = false;
	private float groundRadius = 0.2f;
    private bool doubleJump = false;

	[Header("Player ID (tmp must be set by the gameManager)")]
	public int id;

	[Header("The maximum speed movement")]
    public float maxSpeed = 10f;

	[Header("The jump force")]
    public float jumpForce = 700f;

	[Header("The gameobject to check if the player is grounded")]
    public Transform groudCheck;
    
	[Header("The gameobject on what the player is grounded")]
    public LayerMask whatIsGround;

    public static float MAXSPEEDY = -30f;

    // Use this for initialization
    void Start () {
		this.rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.GetComponentsInChildren<BoxCollider2D>()[1];

		if (!this.sword.tag.Equals ("sword")) {
			Debug.Log ("Be carreful GetComponentsInChildren NOT working");
		}

        this.spawnPoints = GameObject.FindGameObjectsWithTag("respawnPoint");
    }

    private void Update() {
		Vector2 jump = new Vector2 (0, jumpForce);

        // Jump
		if (Input.GetButtonDown("JumpP" + id) && (grounded || !doubleJump) && isAlive )
        {
            doubleJump = true;
			this.rb2d.AddForce(jump);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(this.groudCheck.position, this.groundRadius, this.whatIsGround);
        if (grounded) doubleJump = false;

        if (isAlive)
        {
            float move = Input.GetAxis("HorizontalP" + id);

            this.rb2d.velocity = (rb2d.velocity.y < MAXSPEEDY) ?  new Vector2(move * maxSpeed, MAXSPEEDY) : new Vector2(move * maxSpeed, this.rb2d.velocity.y);

            if (move > 0 && !facingRight) {
                Flip();
            } else if (move < 0 && facingRight) {
                Flip();
            }


            if (Input.GetButtonDown("AttackP" + id))
            {
                this.animator.SetTrigger("attack");
            }
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Attack() {
        this.sword.enabled = true;
    }

    void StopAttack() {
        this.sword.enabled = false;
        this.animator.ResetTrigger("attack");
    }

    void DesactivateControl() {
       this.isAlive = false;
    }

    void ActivateControl() {
        this.isAlive = true;
    }

    void Respawn() {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        this.gameObject.transform.position = spawnPoints[randomSpawnPoint].transform.position;
        this.animator.ResetTrigger("dead");

    }
}
