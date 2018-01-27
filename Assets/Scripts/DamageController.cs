using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private int id;
    private Animator animator;
    public AudioSource deathAudio;

    private void Start()
    {
        this.id = this.gameObject.GetComponent<PlayerController>().id;
        this.animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("sword"))
        {
            PlayerController ennemy = coll.gameObject.GetComponentInParent<PlayerController>();
            if (ennemy != null && ennemy.id != id)
            {
                updateScore(coll.gameObject);
                Dead(ennemy.id);
            }
        }
        ShotScript shot = coll.gameObject.GetComponent<ShotScript>();
        if (shot != null && this.id != shot.id )
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
        Debug.Log("Player " + id + " kill by Player " + ennemyID);
        this.animator.SetTrigger("dead");
        deathAudio.Play();
    }
}
