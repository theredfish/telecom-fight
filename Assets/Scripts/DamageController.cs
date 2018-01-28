using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private int id;
    private Animator animator;

    public AudioClip deathAudio;
    private AudioSource audioSource;

    private PlayerController playerController;

    public GameObject[] lifes;

    private void Start()
    {
        this.id = this.gameObject.GetComponent<PlayerController>().id;
        this.animator = GetComponentInParent<Animator>();
        this.playerController = this.gameObject.GetComponentInParent<PlayerController>();
        this.audioSource = this.GetComponentInParent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("sword"))
        {
            PlayerController ennemy = coll.gameObject.GetComponentInParent<PlayerController>();
            if (ennemy != null && ennemy.id != id && ennemy.isAlive && this.playerController.isAlive && !this.playerController.isImmortal())
            {
                Debug.Log("dead : " + this.playerController.id);
                updateScore(coll.gameObject);
                Dead(ennemy.id);
            }
        }
        ShotScript shot = coll.gameObject.GetComponent<ShotScript>();
        if (shot != null && this.id != shot.id && this.playerController.isAlive && !this.playerController.isImmortal())
        {
            updateScore(shot.gameObject);
            Dead(shot.id);
            Destroy(shot.gameObject);
        }
    }

    public void updateScore(GameObject player)
    {
        PlayerScore pscore = player.GetComponent<PlayerScore>();
        if (pscore != null)
        {
            pscore.GetOneKill();
        }
    }

    public void Dead(int ennemyID)
    {
        this.playerController.DesactivateControl();
        //Debug.Log("Player " + ennemyID  + " kill Player " + id);
        this.playerController.life--;
        this.playerController.StopAttack();
        this.lifes[this.playerController.life].SetActive(false);
        this.animator.SetTrigger("dead");
        this.audioSource.clip = deathAudio;
        audioSource.Play();
        if(this.playerController.life <= 0)
        {
            FinalDead();
        }
    }

    public void FinalDead()
    {
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        if(gameController != null)
        {
            gameController.FinalDead(this.gameObject);
        }
    }
}
