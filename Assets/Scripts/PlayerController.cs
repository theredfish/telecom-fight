using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
    private Animator animator;
    private BoxCollider2D sword;
    private GameObject[] spawnPoints;

    public bool isAlive = true;

    public int life = 3;

	private bool facingRight = true;
	private bool grounded = false;
    private bool wallCheck = false;
	private float groundRadius = 0.2f;
    private bool doubleJump = false;
    private float scaleBulletMaxX;
    private float respawnCooldown;
	[Header("Player ID (tmp must be set by the gameManager)")]
	public int id;

	[Header("The maximum speed movement")]
    public float maxSpeed = 10f;

	[Header("The jump force")]
    public float jumpForce = 700f;

    [Header("Slide Speed")]
    public float slideSpeed = -3f;

    [Header("Jump Force Wall multiply by jump force")]
    public Vector2 slideForceJump = new Vector2(1f,1.2f);

	[Header("The gameobject to check if the player is grounded")]
    public Transform groudCheck;

    [Header("The gameobject to check if the player is grounded on the side")]
    public Transform groundSideCheck;
    
	[Header("The gameobject on what the player is grounded")]
    public LayerMask whatIsGround;

    public static float MAXSPEEDY = -30f;

    public float respawnTime = 1.5f;
    [Header("Projectile")]
    public float shootingRate = 2f;

    public GameObject projectile;

    private Transform bulletCharge;

    private SpriteRenderer spriteRenderer;
    private Color color;
    public AudioSource jumpAudio, attackAudio, respawnAudio;

    private float shootCooldown;


    // Use this for initialization
    void Start () {
		this.rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        this.animator = this.gameObject.GetComponent<Animator>();
        this.sword = this.GetComponentsInChildren<BoxCollider2D>()[1];
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        respawnCooldown = respawnTime;
        this.bulletCharge = this.GetComponentsInChildren<SpriteRenderer>()[1].transform;

        this.scaleBulletMaxX = this.bulletCharge.transform.localScale.x;
        if (!this.sword.tag.Equals ("sword")) {
			Debug.Log ("Be carreful GetComponentsInChildren NOT working");
		}

        this.shootCooldown = 0f;
        this.spawnPoints = GameObject.FindGameObjectsWithTag("respawnPoint");
		DontDestroyOnLoad (gameObject);
    }

    private void Update() {
		Vector2 jump = new Vector2 (0, jumpForce);
       
        // Jump
		if (Input.GetButtonDown("JumpP" + id) && (grounded || !doubleJump) && isAlive )
        {
            doubleJump = true;
			this.rb2d.AddForce(jump);
            jumpAudio.Play();
        }

        //CoolDown shoot
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
            Vector3 v3 = this.bulletCharge.transform.localScale;
            v3.x = shootCooldown < 0.0f ?  scaleBulletMaxX : (1.0f - (shootCooldown / shootingRate)) * scaleBulletMaxX;
            this.bulletCharge.transform.localScale = v3;
        }

        if (respawnCooldown>0)
        {
            respawnCooldown -= Time.deltaTime;
            float percentage = 1.0f - (respawnCooldown/respawnTime);
            spriteRenderer.color = respawnTime < 0.0f ? color : new Color(percentage*color.r,percentage*color.g,percentage*color.b,percentage*color.a);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(this.groudCheck.position, this.groundRadius, this.whatIsGround);
        animator.SetBool("fall", !grounded);

        if (grounded) doubleJump = false;

        if (isAlive)
        {
            if (!grounded)
            {
                wallCheck = Physics2D.OverlapCircle(this.groundSideCheck.position, this.groundRadius, this.whatIsGround);
                if (wallCheck)
                    {
                        HandleWallSliding();
                    }
            }
            float move = Input.GetAxis("HorizontalP" + id);

            if (Mathf.Abs(move) < 0.3f) move = 0;

            this.rb2d.velocity = (rb2d.velocity.y < MAXSPEEDY) ?  new Vector2(move * maxSpeed, MAXSPEEDY) : new Vector2(move * maxSpeed, this.rb2d.velocity.y);

            animator.SetBool("walk", move != 0);

            if (move > 0 && !facingRight) {
                Flip();
            } else if (move < 0 && facingRight) {
                Flip();
            }

            if (respawnCooldown<0)
            {
                if (Input.GetButtonDown("AttackP" + id))
                {
                    this.animator.SetTrigger("attack");
                    attackAudio.Play();
                }

                if (Input.GetButtonDown("FireP" + id) && CanFire)
                {
                    Fire(Input.GetAxis("VerticalP" + id));
                    Vector3 v3 = this.bulletCharge.transform.localScale;
                    v3.x = 0.0f;
                    this.bulletCharge.transform.localScale = v3;
                }
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
        this.respawnCooldown = respawnTime;
        respawnAudio.Play();

    }

    void Fire(float axis)
    {
        shootCooldown = shootingRate;
        var shotobject = Instantiate(projectile) as GameObject;
        
        // Assign position
        shotobject.transform.position = transform.position;

        ShotScript shot = shotobject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.SetPlayerShoot(this.id);
        }

        // Make the weapon shot always towards it
        MoveScript move = shotobject.GetComponent<MoveScript>();
        
        if (move != null)
        {
            move.direction = (facingRight)? this.transform.right : -this.transform.right; // towards in 2D space is the right of the sprite
        }
        move.impulsionDirection(axis);
    }

    public bool CanFire
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }


    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, slideSpeed);
        if (Input.GetButtonDown("JumpP" + id))
        {
            jumpAudio.Play();
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-slideForceJump.x, slideForceJump.y) * jumpForce);
            }
            else
            {
                rb2d.AddForce(slideForceJump*jumpForce);
            }

        }
        
    }

	public void SetAlive(bool heartbit) {
		this.isAlive = heartbit;
	}

    public bool isImmortal()
    {
        return respawnCooldown > 0;
    }
}
