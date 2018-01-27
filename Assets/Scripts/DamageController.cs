using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private int id;
    private Animator animator;

    private void Start()
    {
        this.id = GetComponentInParent<PlayerController>().id;
        this.animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("sword"))
        {
            PlayerController ennemy = coll.gameObject.GetComponentInParent<PlayerController>();
            if (ennemy != null && ennemy.id != id)
            {
                Dead(ennemy.id);
            }
        }
    }

    public void Dead(int ennemyID)
    {
        Debug.Log("Player " + id + " kill by Player " + ennemyID);
        //Add scoring
        this.animator.SetTrigger("dead");
    }
}
