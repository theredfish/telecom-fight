using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {

    Animator animator;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.StopSmoke();
	}
	
    public void Smoke()
    {
        Color a = this.spriteRenderer.color;
        a.a = 255f;
        this.spriteRenderer.color = a;
        this.animator.SetTrigger("smoke");
    }

    void StopSmoke()
    {
        Color a = this.spriteRenderer.color;
        a.a = 0f;
        this.spriteRenderer.color = a;
        this.animator.ResetTrigger("smoke");
    }
}
